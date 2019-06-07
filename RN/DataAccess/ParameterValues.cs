using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RN.DataAccess
{
    public sealed class ParameterValues : IEnumerable<KeyValuePair<object, DbType>>
    {
        /// <summary>
        /// La lista en donde se guardan los pares que contienen el valor del parámetro y su tipo en base de datos
        /// </summary>
        private List<KeyValuePair<object, DbType>> list = new List<KeyValuePair<object, DbType>>();

        /// <summary>
        /// Agrega un valor del parámetro y su tipo de base de datos a esta colección
        /// </summary>
        /// <param name="val">El valor del parámetro. Puede ser null</param>
        /// <param name="type">El tipo de parámetro en base de datos</param>
        public void Add(object val, DbType type)
        {
            KeyValuePair<object, DbType> pair = new KeyValuePair<object, DbType>(val, type);
            list.Add(pair);
        }

        /// <summary>
        /// Obtiene el enumerador para soportar la interfaz IEnumerable
        /// </summary>
        /// <returns>El enumerador que soporta la interfaz IEnumerable</returns>
        IEnumerator<KeyValuePair<object, DbType>> IEnumerable<KeyValuePair<object, DbType>>.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        /// <summary>
        /// Obtiene el enumerador para soportar la interfaz IEnumerable
        /// </summary>
        /// <returns>El enumerador que soporta la interfaz IEnumerable</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
