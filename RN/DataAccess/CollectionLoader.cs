using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Diagnostics.CodeAnalysis;

namespace RN.DataAccess
{
    /// <summary>
    /// Clase que permite cargar una lista a partir de un DataReader.
    /// </summary>
    /// <remarks>Esta clase contiene métodos que a partir de un IDataReader, 
    /// que tiene ciertos datos de la BD, y una clase cuyas propiedades mapean
    /// todos los campos del IDataReader, podemos obtener una lista de objetos del
    /// tipo de la clase que contienen los datos del DataReader.
    /// La principal caracteristica de la clase es la geralidad que ofrece, ya que
    /// es independiente del numero de campos que tenga el IDataReader y del 
    /// tipo de cada uno de estos datos.</remarks>
    public static class ListLoader
    {
        /// <summary>
        /// Este metodo extensor carga una lista (list) a partir de elementos del tipo itemType que estan contenidos en el DataReader.
        /// </summary>
        /// <param name="list">Coleccion donde se van a almacenar los objetos obtenidos.</param>
        /// <param name="reader">Objeto que contiene los datos obtenidos de la BD.</param>
        /// <param name="itemType">Tipo de los elementos de la lista.</param>
        [SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails", Justification = "Las excepciones TargetInvocationException despistan mucho, es mejor lanzar la InnerException que da una información más clara del error")]
        public static void LoadFromReader(this IList list, IDataReader reader, Type itemType)
        {

            Type type = typeof(ListLoader);

            //Se invoca al metodo LoadFromReader(IList, IDataReader) a traves de reflection.
            MethodInfo mi = type.GetMethod("LoadFromReader", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(IList), typeof(IDataReader) }, null);
            mi = mi.MakeGenericMethod(new Type[] { itemType });
            try
            {
                mi.Invoke(null, new object[] { list, reader });
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException == null)
                {
                    throw;
                }
                else
                {
                    //Si falla nos mostrara el mensaje real del metodo que se ha invocado
                    throw ex.InnerException;
                }
            }
        }

        /// <summary>
        /// Metodo extensor que carga una lista de objetos del tipo T a partir de un DataReader.
        /// </summary>
        /// <typeparam name="T">Tipo de los items que van a contener la lista.</typeparam>
        /// <param name="list">Lista a rellenar. Está tipada.</param>
        /// <param name="reader">Objeto que contiene los datos que se quieres obtener en la lista.</param>
        public static void LoadFromReader<T>(this IList<T> list, IDataReader reader) where T : new()
        {
            LoadFromReaderImplementation<T>(list, reader);
        }

        /// <summary>
        /// Metodo extensor que carga una lista de objetos del tipo T a partir de un DataReader.
        /// </summary>
        /// <typeparam name="T">Tipo de los items que van a contener la lista.</typeparam>
        /// <param name="list">Lista a rellenar.</param>
        /// <param name="reader">Objeto que contiene los datos que se quieres obtener en la lista.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Rendimiento: este método es más rápido que la sobrecarga LoadFromReader(this IList list, IDataReader reader, Type itemType) la cual requiere invocación de método mediante Reflection")]
        public static void LoadFromReader<T>(this IList list, IDataReader reader) where T : new()
        {
            LoadFromReaderImplementation<T>(list, reader);
        }
        
        /// <summary>
        /// Metodo que realmente tiene el codigo necesario para cargar el DataReader en la lista de objetos.
        /// </summary>
        /// <typeparam name="T">Tipo del que serán los elementos de la lista a cargar.</typeparam>
        /// <param name="list">Lista a cargar.</param>
        /// <param name="reader">Estructura de la cual se obtendran los datos.</param>
        private static void LoadFromReaderImplementation<T>(object list, IDataReader reader) where T : new()
        {
            //Si alguno de los cast devuelve null, ya sabemos el tipo de la lista.
            IList<T> genericList = list as IList<T>;
            IList nonGenericList = list as IList;
            if (genericList == null && nonGenericList == null)
            {
                throw new ArgumentException("list debe implementar IList o IList<T>", "list");
            }
            
            //El indice del array corresponde al ordinal de cada campo en el IDataReader.
            //Esto hace que la obtencion del método Setter que le corresponde a cada campo
            //sea inmediata.
            //El tipo T es una clase cuyas propiedades tienen el mismo nombre que los 
            //campos de la BD (que estan en el IDataReader).
            PropertySetter[] setters = GetPropertySetters(reader, typeof(T));
            
            while (reader.Read())
            {
                //Crea un objeto de tipo T con los datos del IDataReader ya introducidos.
                T item = CreateItemFromReader<T>(reader, setters);
                
                if (genericList != null)
                {
                    genericList.Add(item);
                }
                else
                {
                    nonGenericList.Add(item);
                }
            }
        }
                        
        /// <summary>
        /// Metodo que crea un elemento de tipo T y le introduce los valores de
        /// los campos del IDataReader con los setters.
        /// </summary>
        /// <typeparam name="T">Tipo (Clase) que se instanciará para rellenarla con los datos del IDataReader.</typeparam>
        /// <param name="reader">Estructura que contiene los datos obtenidos de la BD.</param>
        /// <param name="setters">Métodos necesarios para establecer todos los valores de las 
        /// propiedades del objeto de tipo T.</param>
        /// <returns>Objeto de tipo T rellenado con los datos.</returns>
        private static T CreateItemFromReader<T>(IDataReader reader, PropertySetter[] setters) where T : new()
        {
            //Sabemos con seguridad que se puede hacer por la restriccion.
            T item = new T();

            int fieldCount = reader.FieldCount;

            for (int fieldOrdinal = 0; fieldOrdinal < fieldCount; fieldOrdinal++)
            {
                try
                {
                    PropertySetter setter = setters[fieldOrdinal];
                    if (setter != null)
                    {
                        object fieldValue = reader.IsDBNull(fieldOrdinal) ? null : reader.GetValue(fieldOrdinal);
                        //Se invoca como si fuese un metodo y le establece el valor fieldvalue.
                        setter(item, fieldValue);
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
                
            }
            return item;
        }


        /// <summary>
        /// Representa un mapeo entre los ordinales de los campos del IDataReader y los Setters.
        /// Devuelve un array en el que su índice y el de los ordinales de los campos del 
        /// IDataReader tienen que coincidir.
        /// </summary>
        /// <remarks>Es importante que el vecor de Setter esté ordenado de forma que 
        /// si el campo número cero del IDataReader se llama ContactID, en la posicion
        /// número cero del vector exista un Setter llamado SetContactID...() para 
        /// que a la hora de invocar a los métodos dinámicos, esten claramente identificados.</remarks>
        /// <param name="reader">Estructura de datos que contiene los datos obtenidos de la BD.</param>
        /// <param name="itemType">Tipo de datos cuyas propiedades deben estar mapeadas con los
        /// campos de la BD.</param>
        /// <returns>Vector de Setters.</returns>
        private static PropertySetter[] GetPropertySetters(IDataReader reader, Type itemType)
        {
            int fieldCount = reader.FieldCount;

            PropertySetter[] propertySetters = new PropertySetter[fieldCount];

            //Posiblemente esta en la cache y lo devuelve, sino se crea y se devuelve.
            IPropertySetterDictionary settersDictionary = PropertyHelper.GetPropertySetters(itemType);

            //Recorremos todos los campos del IDataReader
            for (int fieldOrdinal = 0; fieldOrdinal < fieldCount; fieldOrdinal++)
            {
                PropertySetter setter;

                //Mapeo entre ordinal del campo y el nombre de la propiedad de la clase.
                //Aqui es importante que los campos del IDataReader se llamen exactamente
                //igual que las propiedades de la clase. Puesto que se buscará el nombre del
                //campo en el diccionario creado a partir de las propiedades de la clase.
                if (settersDictionary.TryGetValue(reader.GetName(fieldOrdinal), out setter))
                {
                    propertySetters[fieldOrdinal] = setter;
                }
            }
            return propertySetters;
        }

        /// <summary>
        /// Representa un mapeo entre los ordinales de los campos del IDataReader y los Getters.
        /// Devuelve un array en el que su índice y el de los ordinales de los campos del 
        /// IDataReader tienen que coincidir.
        /// </summary>
        /// <param name="reader">Estructura de datos que contiene los datos obtenidos de la BD.</param>
        /// <param name="itemType">Tipo de datos cuyas propiedades deben estar mapeadas con los
        /// campos de la BD.</param>
        /// <returns>Vector de Getters.</returns>
        private static PropertyGetter[] GetPropertyGetters(IDataReader reader, Type itemType)
        {
            int fieldCount = reader.FieldCount;

            PropertyGetter[] propertyGetters = new PropertyGetter[fieldCount];

            //Posiblemente esta en la cache y lo devuelve, sino se crea y se devuelve.
            IPropertyGetterDictionary gettersDictionary = PropertyHelper.GetPropertyGetters(itemType);

            //Recorremos todos los campos del IDataReader
            for (int fieldOrdinal = 0; fieldOrdinal < fieldCount; fieldOrdinal++)
            {
                PropertyGetter getter;

                //Mapeo entre ordinal del campo y el nombre de la propiedad de la clase.
                //Aqui es importante que los campos del IDataReader se llamen exactamente
                //igual que las propiedades de la clase. Puesto que se buscará el nombre del
                //campo en el diccionario creado a partir de las propiedades de la clase.
                if (gettersDictionary.TryGetValue(reader.GetName(fieldOrdinal), out getter))
                {
                    propertyGetters[fieldOrdinal] = getter;
                }
            }
            return propertyGetters;
        }


        /// <summary>
        /// Metodo de prueba.
        /// </summary>
        public static void MostrarTipo<T>(T item, IDataReader reader)
        {
            PropertyGetter[] getters = GetPropertyGetters(reader, typeof(T));

            Type type = item.GetType();

            PropertyInfo[] props = type.GetProperties();

            int propCount = props.Length;

            System.Console.Out.WriteLine("Clase: "+item.GetType().FullName);
            for (int prop = 0; prop < propCount; prop++)
            {
                PropertyGetter getter = getters[prop];
                if (getter != null)
                {
                    //Se invoca como si fuese un metodo y le establece el valor fieldvalue.
                    object valor = getter(item);
                    System.Console.Out.Write("Propiedad: "+props[prop].Name+" ");
                    System.Console.Out.WriteLine("Valor: " + valor.ToString());
                }
            }
            System.Console.Out.WriteLine("---------------------------");
        }

        
    }
}
