using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ManagerDA
    {
        private string GetConectionString()
        {
            return ConfigurationManager.ConnectionStrings["CN_ZandorCapital"].ConnectionString;
        }

        public void UpdateBulkCopy(string _sTabla, DataTable _dtData)
        {

            SqlBulkCopy sbc = new SqlBulkCopy(GetConectionString());
            sbc.DestinationTableName = _sTabla.ToString();
            sbc.WriteToServer(_dtData);

            sbc.Close();

        }


        public SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(GetConectionString());
            con.Open();
            return con;
        }

        public SqlParameter[] GetParameters(int cantidad)
        {
            List<SqlParameter> arr = new List<SqlParameter>();
            int contador;
            for (contador = 0; contador < cantidad; contador++)
            {
                arr.Add(new SqlParameter());
            }
            return arr.ToArray();
        }

        private void SetParameters(SqlCommand cmd, SqlParameter[] arr)
        {
            if (arr != null)
            {
                foreach (SqlParameter obj in arr)
                    cmd.Parameters.Add(obj);
            }
        }

        public int ExecuteNonQuery(string query, SqlParameter[] arr, CommandType tipo)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = query;
            cmd.CommandType = tipo;
            SetParameters(cmd, arr);
            int cantidad = cmd.ExecuteNonQuery();
            con.Close();
            return cantidad;
        }

        public object ExecuteScalar(string query, SqlParameter[] arr, CommandType tipo)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = query;
            cmd.CommandType = tipo;
            SetParameters(cmd, arr);
            object valor = cmd.ExecuteScalar();
            con.Close();
            return valor;
        }
        public IDataReader GetContactsReader()
        {
            SqlCommand Command = new SqlCommand("usp_getProject", GetConnection());
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter[] Parametros_Consulta = new SqlParameter[1];

            Parametros_Consulta[0] = new SqlParameter("@Id", "01");
            foreach (var item in Parametros_Consulta)
                Command.Parameters.Add(item).Value = item.Value;
            SqlDataReader Reader;
            return Reader = Command.ExecuteReader();
        }
        public string ExecuteNonQueryS(string query, SqlParameter[] arr, CommandType tipo)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = query;
            cmd.CommandType = tipo;
            SetParameters(cmd, arr);
            string sRes = cmd.ExecuteNonQuery().ToString();
            con.Close();
            return sRes;
        }
        public static void CloseConexion(SqlCommand Conn)
        {
            try
            {
                if (Conn.Connection.State == ConnectionState.Open)

                {
                    Conn.Connection.Close();
                    Conn.Dispose();
                    Conn = null;
                }
            }
            catch (SqlException Ex)
            {


            }
        }

        public IDataReader GetContactsReaderGeneric(string query, SqlParameter[] arr, CommandType tipo)
        {
            using (var Command = new SqlCommand(query, RN.Conexion.OpenConexion()))
            {
                Command.CommandType = tipo;
                SetParameters(Command, arr);
                SqlDataReader Reader;
                Reader = Command.ExecuteReader();
                return Reader;
            }
        }

        public DataSet ExecuteDataset(string query, SqlParameter[] arr, CommandType tipo)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = query;
            cmd.CommandType = tipo;
            SetParameters(cmd, arr);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            adap.Fill(ds);
            adap.Dispose();
            con.Close();
            return ds;
        }


        public DataSet ExecuteDataset1(string query, SqlParameter[] arr, CommandType tipo)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = query;
            cmd.CommandType = tipo;
            SetParameters(cmd, arr);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            adap.Fill(ds);
            adap.Dispose();
            con.Close();
            return ds;
        }



    }
}
