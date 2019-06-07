using DataAccess;
using Entities.Person;
using RN.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RN.RulesBussines
{
    public static class LoadLog
    {
        private static ManagerDA oData = new ManagerDA();

        public static int Register(DateTime Fecha, string Usuario, string IpLocal, string IpPublica, string SerialHDD, string Maquina, string Proceso, string Tipo)
        {
            try
            {
                SqlParameter[] ParamLog = GetParameters(8);
                ParamLog[0].ParameterName = "@date";
                ParamLog[0].Value = Fecha;
                ParamLog[1].ParameterName = "@IdUser";
                ParamLog[1].Value = Usuario;
                ParamLog[2].ParameterName = "@IpLocal";
                ParamLog[2].Value = IpLocal;
                ParamLog[3].ParameterName = "@IpPublic";
                ParamLog[3].Value = IpPublica;
                ParamLog[4].ParameterName = "@SerialHDD";
                ParamLog[4].Value = SerialHDD;
                ParamLog[5].ParameterName = "@Machine";
                ParamLog[5].Value = Maquina;
                ParamLog[6].ParameterName = "@Procces";
                ParamLog[6].Value = Proceso;
                ParamLog[7].ParameterName = "@Type";
                ParamLog[7].Value = Tipo;
                return oData.ExecuteNonQuery("LogOperations", ParamLog, System.Data.CommandType.StoredProcedure);
            }
            catch (Exception Exc)
            {

                MessageBox.Show("Error saving LogProcess. " + Exc.Message, "Controlled message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        public static object Exist_DB(string query, string HoleID,decimal FromV)
        {
            try
            {
                object oRes;
                SqlParameter[] arr = oData.GetParameters(2);
                arr[0].ParameterName = "@HoleID";
                arr[0].Value = HoleID;
                arr[1].ParameterName = "@SKDHSamples";
                arr[1].Value = FromV;
              
                oRes = oData.ExecuteScalar(query, arr, CommandType.Text);

                return oRes;


            }
            catch (Exception eX)
            {
                throw new Exception("Save error LabSubmitIn. " + eX.Message); ;
            }
        }


        public static object update_Role(int roleID, string nameForms, string controlID, int InVisible, int Disabled)
        {
            
            try
            {
                object oRes;
                SqlParameter[] arr = oData.GetParameters(5);
                arr[0].ParameterName = "@RoleID";
                arr[0].Value = roleID;
                arr[1].ParameterName = "@PageName";
                arr[1].Value = nameForms;
                arr[2].ParameterName = "@ControlID";
                arr[2].Value = controlID;
                arr[3].ParameterName = "@invisible";
                arr[3].Value = InVisible;
                arr[4].ParameterName = "@disabled";
                arr[4].Value = Disabled;

                oRes = oData.ExecuteNonQuery("spUpdateControlToRole", arr, CommandType.StoredProcedure);

                return oRes;


            }
            catch (Exception eX)
            {
                throw new Exception("Save error LabSubmitIn. " + eX.Message); ;
            }
        }


        public static object Delete_Role(string spName, int USERID, int roleID, bool userRole)
        {

            try
            {
                if (userRole)
                {
                    object oRes;
                    SqlParameter[] arr = oData.GetParameters(2);
                    arr[0].ParameterName = "@USERID";
                    arr[0].Value = USERID;
                    arr[1].ParameterName = "@RoleID";
                    arr[1].Value = roleID;
                    oRes = oData.ExecuteNonQuery(spName, arr, CommandType.StoredProcedure);



                    return oRes;
                }
                else
                {
                    object oRes;
                    SqlParameter[] arr = oData.GetParameters(1);
                    arr[0].ParameterName = "@RoleID";
                    arr[0].Value = roleID;
                    oRes = oData.ExecuteNonQuery(spName, arr, CommandType.StoredProcedure);

                    return oRes;
                }
                    

            }
            catch (Exception eX)
            {
                throw new Exception("Delete error User and Role. " + eX.Message); ;
            }
        }

        public static object Delete_Control_Role(string spName, string page, string control)
        {
            try
            {                                  
                    SqlParameter[] arr = oData.GetParameters(2);
                    arr[0].ParameterName = "@FKPage";
                    arr[0].Value = page;
                    arr[1].ParameterName = "@FKControlID";
                    arr[1].Value = control;
                return  oData.ExecuteNonQuery(spName, arr, CommandType.StoredProcedure);
                 
            }
            catch (Exception eX)
            {
                throw new Exception("Delete error User and Role. " + eX.Message); ;
            }
        }



        public static int alterdataBase(string query)
        {
            try
            {
                SqlParameter[] ParamLog = GetParameters(0);
                return oData.ExecuteNonQuery(query, ParamLog, System.Data.CommandType.Text);
            }
            catch (Exception Exc)
            {

                MessageBox.Show("Error saving LogProcess. " + Exc.Message, "Controlled message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
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

        public static SqlParameter[] Parametros_Update_Rol(int roleID, string nameForm, string controlID, int InVisible, int Disabled, int ContenedorPeqMineria, int ContenedorZandor, int ContenedorOtros)
        {
            SqlParameter[] ParamSQL = new SqlParameter[8];

            ParamSQL[0] = new SqlParameter("@RoleID", roleID);
            ParamSQL[1] = new SqlParameter("@PageName", nameForm);
            ParamSQL[2] = new SqlParameter("@ControlID", controlID);

            ParamSQL[3] = new SqlParameter("@invisible", InVisible);
            ParamSQL[4] = new SqlParameter("@disabled", Disabled);
            ParamSQL[5] = new SqlParameter("@ContenedorPeqMineria", ContenedorPeqMineria);

            ParamSQL[6] = new SqlParameter("@ContenedorZandor", ContenedorZandor);
            ParamSQL[7] = new SqlParameter("@ContenedorOtros", ContenedorOtros);

            return ParamSQL;
        }

        public static DataTable GetRoles(string queryString)
        {
            SqlConnection con = GetConnection();


            DataSet ds = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(queryString, con);
            dataAdapter.Fill(ds, "usersInRoles");

            con.Close();
            return ds.Tables[0];
        }

        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(GetConectionString());
            con.Open();
            return con;
        }

        private static string GetConectionString()
        {
           return ConfigurationManager.ConnectionStrings["CN_ZandorCapital"].ConnectionString;
        }

        public static List<Rol_Permission> GetPermissiinRol(string SP_Consulta, string nameUser, string PageMaster)
        {
            SqlParameter[] Parametros_Consulta = new SqlParameter[2];


            Parametros_Consulta[0] = new SqlParameter("@Name", nameUser);
            Parametros_Consulta[1] = new SqlParameter("@FKPage", PageMaster);

            SqlCommand Command = new SqlCommand(SP_Consulta, GetConnection());
            Command.CommandType = CommandType.StoredProcedure;

            foreach (var item in Parametros_Consulta)
                Command.Parameters.Add(item).Value = item.Value;

            using (IDataReader reader = Command.ExecuteReader())
            {
                List<Rol_Permission> permisosRoles = new List<Rol_Permission>();
                IList<Rol_Permission> list = permisosRoles;
                list.LoadFromReader(reader);

                reader.Close();
                return list.ToList();
            }
            return null;
        }

    }
}
