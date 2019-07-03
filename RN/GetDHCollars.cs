using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RN
{
    public  static class GetDHCollars
    {
        public static DataTable getDHCollars(string sHoleID)
        {
            try
            {

                DataSet dtDHCollars = new DataSet();
                SqlParameter[] arr = GetParameters(0);
                //arr[0].ParameterName = "@HoleID";
                //arr[0].Value = sHoleID;
                dtDHCollars = ExecuteDataset("usp_DH_HoleID_LIST", arr, CommandType.StoredProcedure);
                return dtDHCollars.Tables[0];

            }
            catch (Exception eX)
            {
                throw new Exception("Error in DHcollars: " + eX.Message);
            }
        }
        public static DataTable getDHHoleID(string sHoleID)
        {
            try
            {

                DataSet dtDHCollars = new DataSet();
                SqlParameter[] arr = GetParameters(1);
                arr[0].ParameterName = "@HoleID";
                arr[0].Value = sHoleID;
                dtDHCollars = ExecuteDataset("usp_DH_Hole_List_Filter", arr, CommandType.StoredProcedure);
                return dtDHCollars.Tables[0];

            }
            catch (Exception eX)
            {
                throw new Exception("Error in DHcollars By HoleId: " + eX.Message);
            }
        }

        public static DataTable getRecordsBySource(string source)
        {
            try
            {

                DataSet dtDHCollars = new DataSet();
                SqlParameter[] arr = GetParameters(1);
                arr[0].ParameterName = "@source";
                arr[0].Value = source;
                dtDHCollars = ExecuteDataset("[dbo].[usp_DH_Source_List_Filter]", arr, CommandType.StoredProcedure);
                return dtDHCollars.Tables[0];

            }
            catch (Exception eX)
            {
                throw new Exception("Error in DHcollars By Source : " + eX.Message);
            }
        }

        /* Modificado Alvaro Araujo 06/06/2019*/
        public static DataTable getRecordsByTarget(string target, string sources)
        {
            try
            {

                DataSet dtDHCollars = new DataSet();
                SqlParameter[] arr = GetParameters(2);
                arr[0].ParameterName = "@target";
                arr[0].Value = target;
                arr[1].ParameterName = "@sources";
                arr[1].Value = sources; 
                //dtDHCollars = ExecuteDataset("[dbo].[usp_DH_Target_List_Filter]", arr, CommandType.StoredProcedure);
                dtDHCollars = ExecuteDataset("[dbo].[usp_DH_Target_List_Filter_Param]", arr, CommandType.StoredProcedure);
                return dtDHCollars.Tables[0];

            }
            catch (Exception eX)
            {
                throw new Exception("Error in DHcollars by Target : " + eX.Message);
            }
        }

        public static DataTable get_Holes_Sources_Targets_lst()
        {
            try
            {

                DataSet dtHolesSourcesTargets = new DataSet();
                SqlParameter[] arr = GetParameters(0);
                dtHolesSourcesTargets = ExecuteDataset("[dbo].[usp_get_holes_sources_targets_lst]", arr, CommandType.StoredProcedure);
                return dtHolesSourcesTargets.Tables[0];

            }
            catch (Exception eX)
            {
                throw new Exception("Error in DHcollars: " + eX.Message);
            }
        }

        public static int LockUnlockHole(String holeId, bool locked)
        {
            try
            {
                DataSet dtDHCollars = new DataSet();
                SqlParameter[] arr = GetParameters(2);
                arr[0].ParameterName = "@holeId";
                arr[0].Value = holeId;
                arr[1].ParameterName = "@locked";
                arr[1].Value = locked;

                return ExecuteNonQuery("[dbo].[usp_update_DHCollars_locked]", arr, CommandType.StoredProcedure);

            }
            catch (Exception eX)
            {
                throw new Exception("Error in DHcollars: " + eX.Message);
            }
        }

        public static SqlParameter[] GetParameters(int cantidad)
        {
            List<SqlParameter> arr = new List<SqlParameter>();
            int contador;
            for (contador = 0; contador < cantidad; contador++)
            {
                arr.Add(new SqlParameter());
            }
            return arr.ToArray();
        }

        public static DataSet ExecuteDataset(string query, SqlParameter[] arr, CommandType tipo)
        {
            SqlConnection con = Conexion.OpenConexion();
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

        public static int ExecuteNonQuery(string query, SqlParameter[] arr, CommandType tipo)
        {
            SqlConnection conn = Conexion.OpenConexion();
            SqlCommand cmd = conn.CreateCommand();

            try
            {
                
                cmd.CommandText = query;
                cmd.CommandType = tipo;
                SetParameters(cmd, arr);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();

                return result;
            }
            finally
            {
                conn.Close();
            }
        }
        private static void SetParameters(SqlCommand cmd, SqlParameter[] arr)
        {
            if (arr != null)
            {
                foreach (SqlParameter obj in arr)
                    cmd.Parameters.Add(obj);
            }
        }
    }


}
