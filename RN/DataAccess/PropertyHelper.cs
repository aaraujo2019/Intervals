using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Reflection.Emit;
using System.Web;
using System.Collections.ObjectModel;

namespace RN.DataAccess
{

    /// <summary>
    /// Definicion de la firma de los métodos Setters a los que los que los objetos
    /// delegados pueden referenciar.
    /// </summary>
    /// <param name="instance">Objeto de tipo anónimo desde el cual se invoca el método dinámico.</param>
    /// <param name="value">Valor que se va a establecer a la propiedad.</param>
    public delegate void PropertySetter(object instance, object value);

    /// <summary>
    /// Definicion de la firma de los métodos Getters a los que los que los objetos
    /// delegados pueden referenciar.
    /// </summary>
    /// <param name="instance">Objeto de tipo anónimo desde el cual se invoca el método dinámico.</param>
    /// <returns>Objeto de tipo anónimo que devuelve el get.</returns>
    public delegate object PropertyGetter(object instance);

    /// <summary>
    /// Interfaz Diccionario para almacenar los Setters dinámicos.
    /// </summary>
    /// <remarks>
    /// Es un diccionario cuya clave es ti po string es el nombre de la propiedad y el valor es el propoerty setter.
    /// </remarks>
    public interface IPropertySetterDictionary
        : IDictionary<string, PropertySetter>
    {
    }

    /// <summary>
    /// Clase de tipo Diccionario para almacenar los Setters dinámicos. 
    /// Nos permite devolver un diccionario de solo lectura para evitar que 
    /// alguien pueda modificarlo externamente.
    /// </summary>
    /// <remarks></remarks>
    internal class PropertySetterDictionary
        : ReadOnlyDictionary<string, PropertySetter>,
        IPropertySetterDictionary
    {
        public PropertySetterDictionary(Dictionary<string, PropertySetter> dictionary) : base(dictionary)
        { 
        }
    }

    /// <summary>
    /// Interfaz Diccionario para almacenar los Getters dinámicos.
    /// </summary>
    /// <remarks></remarks>
    public interface IPropertyGetterDictionary
        : IDictionary<string, PropertyGetter>
    {
    }

    /// <summary>
    /// Clase de tipo Diccionario para almacenar los Getters dinámicos. 
    /// Nos permite devolver un diccionario de solo lectura para evitar que 
    /// alguien pueda modificarlo externamente.
    /// </summary>
    /// <remarks></remarks>
    internal class PropertyGetterDictionary
        : ReadOnlyDictionary<string, PropertyGetter>,
        IPropertyGetterDictionary
    {
        public PropertyGetterDictionary(Dictionary<string, PropertyGetter> dictionary)
            : base(dictionary)
        {
        }
    }


    /// <summary>
    /// Clase para la creacion dinámica de los metodos getters y setters.
    /// </summary>
    /// <remarks>Clase que contiene los métodos necesarios para a partir de un conjuto de propiedades
    /// crear sus metodos getters y setters dinámicamente y devolverlos como en un 
    /// diccionario de solo lectura, en el que la clave será el nombre de la propiedad y 
    /// el valor un puntero al método.</remarks>
    public static class PropertyHelper
    {

        #region Métodos que trabajan con Caché

        /// <summary>
        /// Añade a la caché los setters de un tipo determinado por itemType.
        /// </summary>
        /// <param name="setters">Diccionario de Setters a guardar en caché.</param>
        /// <param name="itemType">Tipo del objeto que contiene el diccionario.</param>
        private static void AddSettersToCache(PropertySetterDictionary setters, Type itemType )
        {
            HttpRuntime.Cache["PropertySetters_" + itemType.FullName] = setters;
        }

        /// <summary>
        /// Obtiene de la caché los setters del tipo determinado por itemType.
        /// </summary>
        /// <param name="itemType">Tipo del objeto que contiene el diccionario.</param>
        /// <returns>Diccionario de Setters obtenidos de la caché.</returns>
        private static PropertySetterDictionary GetSettersFromCache(Type itemType)
        {
            return (PropertySetterDictionary)HttpRuntime.Cache["PropertySetters_" + itemType.FullName];
        }

        /// <summary>
        /// Añade a la caché los Getters de un tipo determinado por itemType.
        /// </summary>
        /// <param name="getters">Diccionario de Getters a guardar en caché.</param>
        /// <param name="itemType">Tipo del objeto que contiene el diccionario.</param>
        private static void AddGettersToCache(PropertyGetterDictionary getters, Type itemType)
        {
            HttpRuntime.Cache["PropertyGetters_" + itemType.FullName] = getters;
        }

        /// <summary>
        /// Obtiene de la caché los Getters del tipo determinado por itemType.
        /// </summary>
        /// <param name="itemType">Tipo del objeto que contiene el diccionario.</param>
        /// <returns>Diccionario de Getters obtenidos de la caché.</returns>
        private static PropertyGetterDictionary GetGettersFromCache(Type itemType)
        {
            return (PropertyGetterDictionary)HttpRuntime.Cache["PropertyGetters_" + itemType.FullName];
        }

        #endregion


        #region Setters

        /// <summary>
        /// Método que genera los setters de un determinado tipo de objeto (clase) y los
        /// devuelve en un diccionario que contiene todas las propiedades de la clase
        /// junto con los métodos creados dinámicamente.
        /// </summary>
        /// <param name="type">Tipo (Clase) del que se obtendran las propiedades.</param>
        /// <returns>Diccionario de duplas (NombrePropiedad, MétodoSetDinámico).</returns>
        public static IPropertySetterDictionary GetPropertySetters(Type type)
        {
            return GetPropertySetters(type, true);
        }

        /// <summary>
        /// Método que genera los setters de un determinado tipo de objeto (clase) y los
        /// devuelve en un diccionario que contiene todas las propiedades de la clase
        /// junto con los métodos creados dinámicamente.
        /// </summary>
        /// <param name="type">Tipo (Clase) del que se obtendran las propiedades.</param>
        /// <param name="cache">Si es true, el Diccionario creado se alamacenará en caché,
        /// en caso contrario, no se almacenará en caché.</param>
        /// <returns>Diccionario de duplas (NombrePropiedad, MétodoSetDinámico).</returns>
        public static IPropertySetterDictionary GetPropertySetters(Type type, bool cache)
        {
            //Si los Setters ya habian sido creados se devuelven.
            PropertySetterDictionary setters = GetSettersFromCache(type);
            if (setters != null) return setters;
            
            //Lo primero obtengo las propiedades de ese tipo (clase).
            //Cada PropertyInfo contiene el tipo y el nombre de una propiedad de la clase (type).
            PropertyInfo[] props = type.GetProperties();

            //Diccionario en el que la clave es el Nombre de la propiedad y el valor el método Set obtenido por reflection
            Dictionary<string, PropertySetter> internalSetters = new Dictionary<string, PropertySetter>(props.Length);

            foreach (PropertyInfo pi in props)
            {
                //Obtenemos en 'setter' el método set dinámico para el atributo o propiedad 'pi'.
                PropertySetter setter = GetPropertySetter(pi);
                if (setter != null)
                {
                    internalSetters.Add(pi.Name, setter);
                }
            }

            setters = new PropertySetterDictionary(internalSetters);

            if (cache)
            {
                AddSettersToCache(setters, type);
            }
            return setters;
        }
        
        /// <summary>
        /// Método que obtiene el Setter de una propiedad especificada 'propertyName'
        /// del tipo (Clase).
        /// </summary>
        /// <param name="type">Tipo de datos (Clase) en el que se busacará la propiedad 'propertyName'.</param>
        /// <param name="propertyName">Nombre de la propiedad que nos interesa del tipo.</param>
        /// <returns>Puntero al método Set para la propiedad 'propertyName' del tipo 'type'.</returns>
        public static PropertySetter GetPropertySetter(Type type, string propertyName)
        {
            PropertyInfo pi = type.GetProperty(propertyName);
            if (pi == null)
            {
                return null;
            }
            return GetPropertySetter(pi);
        }


        /// <summary>
        /// Método que obtiene el Setter de de una propiedad cuya informacion se
        /// pasa por parámtro.
        /// </summary>
        /// <param name="pi">Informacion de la propiedad de la cual se quiere obtener
        /// el método Set dinámico.</param>
        /// <returns>Puntero al método Set para la propiedad especificada en ProperyInfo.</returns>
        public static PropertySetter GetPropertySetter(PropertyInfo pi)
        {
            //Si la propiedad es de solo lectura no tendrá metodo Set.
            MethodInfo mi = pi.GetSetMethod();
            if (mi == null) return null;

            DynamicMethod dm = new DynamicMethod("Set" + pi.Name + "PropertyValue", null, new Type[] { typeof(object), typeof(object) }, pi.DeclaringType);
            
            //Se crea el codigo IL (Código ensamblador para .NET)
            ILGenerator il = dm.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Castclass, pi.DeclaringType);
            il.Emit(OpCodes.Ldarg_1);
           
            if (pi.PropertyType.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, pi.PropertyType);
            }
            else
            {
                il.Emit(OpCodes.Castclass, pi.PropertyType);
            }
            il.Emit(OpCodes.Callvirt, mi);
            il.Emit(OpCodes.Ret);

            return (PropertySetter)dm.CreateDelegate(typeof(PropertySetter));
        }

        #endregion


        #region Getters

        /// <summary>
        /// Método que genera los Getters de un determinado tipo de objeto (Clase) y los
        /// devuelve en un diccionario que contiene todas las propiedades de la clase
        /// junto con los métodos creados dinámicamente.
        /// </summary>
        /// <param name="type">Tipo (Clase) del que se obtendran las propiedades.</param>
        /// <returns>Diccionario de duplas (NombrePropiedad, MétodoGetDinámico).</returns>
        public static IPropertyGetterDictionary GetPropertyGetters(Type type)
        {
            return GetPropertyGetters(type, true);
        }


        /// <summary>
        /// Método que genera los Getters de un determinado tipo de objeto (clase) y los
        /// devuelve en un diccionario que contiene todas las propiedades de la clase
        /// junto con los métodos creados dinámicamente.
        /// </summary>
        /// <param name="type">Tipo (Clase) del que se obtendran las propiedades.</param>
        /// <param name="cache">Si es true, el Diccionario creado se alamacenará en caché,
        /// en caso contrario, no se almacenará en caché.</param>
        /// <returns>Diccionario de duplas (NombrePropiedad, MétodoGetDinámico).</returns>
        public static IPropertyGetterDictionary GetPropertyGetters(Type type, bool cache)
        {
            //Si los Getters ya habian sido creados se devuelven
            PropertyGetterDictionary getters = GetGettersFromCache(type);
            if (getters != null) return getters;

            //Lo primero obtengo las propiedades de ese tipo (clase).
            //Cada PropertyInfo contiene el tipo y el nombre de una propiedad de la clase (type).
            PropertyInfo[] props = type.GetProperties();

            //Diccionario en el que la clave es el Nombre de la propiedad y el valor el método Get obtenido por reflection
            Dictionary<string, PropertyGetter> internalGetters = new Dictionary<string, PropertyGetter>(props.Length);

            foreach (PropertyInfo pi in props)
            {
                //Obtenemos en 'setter' el método Get dinámico para el atributo o propiedad 'pi'.
                PropertyGetter getter = GetPropertyGetter(pi);
                if (getter != null)
                {
                    internalGetters.Add(pi.Name, getter);
                }
            }
            getters = new PropertyGetterDictionary(internalGetters);

            if (cache)
            {
                AddGettersToCache(getters, type);
            }
            return getters;
        }

        /// <summary>
        /// Método que obtiene el Getter de una propiedad especificada 'propertyName'
        /// del tipo (Clase).
        /// </summary>
        /// <param name="type">Tipo de datos (Clase) en el que se busacará la propiedad 'propertyName'.</param>
        /// <param name="propertyName">Nombre de la propiedad que nos interesa del tipo.</param>
        /// <returns>Puntero al método Get para la propiedad 'propertyName' del tipo 'type'.</returns>
        public static PropertyGetter GetPropertyGetter(Type type, string propertyName)
        {
            PropertyInfo pi = type.GetProperty(propertyName);
            if (pi == null)
            {
                return null;
            }
            return GetPropertyGetter(pi);
        }

        /// <summary>
        /// Método que obtiene el Getter de de una propiedad cuya información se
        /// pasa por parámtro.
        /// </summary>
        /// <param name="pi">Informacion de la propiedad de la cual se quiere obtener
        /// el método Get dinámico.</param>
        /// <returns>Puntero al método Set para la propiedad especificada en ProperyInfo.</returns>
        public static PropertyGetter GetPropertyGetter(PropertyInfo pi)
        {
            //Si la propiedad es de solo escritura no tendrá metodo Get.
            MethodInfo mi = pi.GetGetMethod();
            if (mi == null) return null;

            DynamicMethod dm = new DynamicMethod("Get" + pi.Name + "PropertyValue", typeof(object), new Type[] { typeof(object) }, pi.DeclaringType);

            //Se crea el codigo IL (Código ensamblador para .NET)
            ILGenerator il = dm.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Castclass, pi.DeclaringType);
            il.Emit(OpCodes.Callvirt, mi);

            if (pi.PropertyType.IsValueType)
            {
                il.Emit(OpCodes.Box, pi.PropertyType);
            }

            il.Emit(OpCodes.Ret);

            return (PropertyGetter)dm.CreateDelegate(typeof(PropertyGetter));
        }

        #endregion
    }
}
