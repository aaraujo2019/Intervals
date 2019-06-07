using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RN.DataAccess
{
public  static  class DataAccess
    {
        /// <summary>
        /// Objeto de conexion
        /// </summary>
    
        protected internal enum FillMode
        {
            DataSet,
            DataTable,
            Scalar,
            Reader
        }
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

        private static T CreateItemFromReader<T>(IDataReader reader, PropertySetter[] setters) where T : new()
        {
            //Sabemos con seguridad que se puede hacer por la restriccion.
            T item = new T();

            int fieldCount = reader.FieldCount;

            for (int fieldOrdinal = 0; fieldOrdinal < fieldCount; fieldOrdinal++)
            {
                PropertySetter setter = setters[fieldOrdinal];
                if (setter != null)
                {
                    object fieldValue = reader.IsDBNull(fieldOrdinal) ? null : reader.GetValue(fieldOrdinal);
                    //Se invoca como si fuese un metodo y le establece el valor fieldvalue.
                    setter(item, fieldValue);
                }
            }
            return item;
        }
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


        /*internal virtual T GetData<T>(string sql, ParameterValues args, DbTransaction transaction, T result, string tableName, FillMode fillMode) where T : new()
        {
            if (result == null)
                result = new T();

            // Garantiza que la conexión está abierta. Se cierra cuando se finaliza el objeto DatabaseManager()
            // por reutilización de la conexión.
            OpenConnection();

            using (DbCommand command = GetCommand(Connection))
            {
                try
                {
                    if (transaction != null)
                        command.Transaction = transaction;

                    EnableReadUncommitted(command, transaction);
                    PrepareCommand(command, sql, args);

                    if (fillMode == FillMode.Scalar)
                    {
                        object data = command.ExecuteScalar();
                        result = (T)data;
                    }
                    else
                    {
                        if (fillMode == FillMode.DataSet)
                        {
                            using (DbDataAdapter adapter = GetAdapter(command))
                            {
                                if (fillMode == FillMode.DataSet)
                                    if (tableName != null)
                                        adapter.Fill(result as DataSet, tableName);
                                    else
                                        adapter.Fill(result as DataSet);

                                else if (fillMode == FillMode.DataTable)
                                    adapter.Fill(result as DataTable);
                            }
                        }
                        else
                        {
                            IDataReader reader = null;
                            OpenConnection();
                            try
                            {                              
                                if (transaction != null)
                                    command.Transaction = transaction;

                                EnableReadUncommitted(command, transaction);

                                PrepareCommand(command, sql, args);
                                reader = command.ExecuteReader(behavior);
                            }
                            catch (Exception err)
                            {
                                throw HandleProviderException(sql, args, err, true);
                            }
                            return reader;
                        }
                    }
                }
                catch (Exception err)
                {
                    throw HandleProviderException(sql, args, err, true);
                }
            }
            return result;
        }*/

        #region Metodos de conexión
        /// <summary>
        /// Abre la conexion con la base de datos
        /// </summary>
      

        /// <summary>
        /// Cierra la conexion con la base de datos
        /// </summary>
       
        #endregion

    }
}
