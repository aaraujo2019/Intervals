using DataAccess;
using Entities.Topography;
using RN.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RN.Topography
{
    public class GetDataTopography
    {
        private static ManagerDA oData = new ManagerDA();


        public static IList<MasterTopography> getMaterTopography(string SP_Consulta)
        {
            SqlParameter[] Parametros_Consulta = new SqlParameter[0];
            //Parametros_Consulta[0] = new SqlParameter("@Op", Op);
            //Parametros_Consulta[1] = new SqlParameter("@ParametroChar", ParametroChar);
            //Parametros_Consulta[2] = new SqlParameter("@ParametroInt", ParametroInt);
            //Parametros_Consulta[3] = new SqlParameter("@ParametroNuemric", "0");

            SqlConnection con = Conexion.OpenConexion();
            SqlCommand Command = new SqlCommand(SP_Consulta, con);
            Command.CommandType = CommandType.StoredProcedure;

            foreach (var item in Parametros_Consulta)
                Command.Parameters.Add(item).Value = item.Value;

            using (IDataReader reader = Command.ExecuteReader())
            {
                List<MasterTopography> muestroCalidad = new List<MasterTopography>();
                IList<MasterTopography> list = muestroCalidad;
                list.LoadFromReader(reader);

                con.Close();
                return list.ToList();
            }

            con.Close();
            return null;
        }
        public static IList<Ent_Get_Labor_Thopo> getLaborTopography(string SP_Consulta)
        {
            SqlParameter[] Parametros_Consulta = new SqlParameter[0];
            //Parametros_Consulta[0] = new SqlParameter("@Op", Op);
            //Parametros_Consulta[1] = new SqlParameter("@ParametroChar", ParametroChar);
            //Parametros_Consulta[2] = new SqlParameter("@ParametroInt", ParametroInt);
            //Parametros_Consulta[3] = new SqlParameter("@ParametroNuemric", "0");

            SqlConnection con = Conexion.OpenConexion();
            SqlCommand Command = new SqlCommand(SP_Consulta, con);
            Command.CommandType = CommandType.StoredProcedure;

            foreach (var item in Parametros_Consulta)
                Command.Parameters.Add(item).Value = item.Value;

            using (IDataReader reader = Command.ExecuteReader())
            {
                List<Ent_Get_Labor_Thopo> muestroCalidad = new List<Ent_Get_Labor_Thopo>();
                IList<Ent_Get_Labor_Thopo> list = muestroCalidad;
                list.LoadFromReader(reader);

                con.Close();
                return list.ToList();
            }

            con.Close();
            return null;
        }

        public static int InsertMoveTopograp(string spName, string Id_Lab, int CEast, int CNorth, int CElevant, int Ccode, string Code_Lab, string Detail_Id, string Point_Type, string Condition, string CodeTopo, string NameFile, DateTime DateEvent, string AditionalAct)
        {
            try
            {
                SqlParameter[] arr = GetParameters(13);
                arr[0].ParameterName = "@Id_Lab";
                arr[0].Value = Id_Lab;

                arr[1].ParameterName = "@CEast";
                arr[1].Value = CEast;

                arr[2].ParameterName = "@CNorth";
                arr[2].Value = CNorth;

                arr[3].ParameterName = "@CElevant";
                arr[3].Value = CElevant;

                arr[4].ParameterName = "@Ccode";
                arr[4].Value = Ccode;

                arr[5].ParameterName = "@Code_Lab";
                arr[5].Value = Code_Lab;

                arr[6].ParameterName = "@Detail_Id";
                arr[6].Value = Detail_Id;

                arr[7].ParameterName = "@Point_Type";
                arr[7].Value = Point_Type;

                arr[8].ParameterName = "@Condition";
                arr[8].Value = Condition;

                arr[9].ParameterName = "@CodeTopo";
                arr[9].Value = CodeTopo;

                arr[10].ParameterName = "@NameFile";
                arr[10].Value = NameFile;

                arr[11].ParameterName = "@DateEvent";
                arr[11].Value = DateEvent;  

                arr[12].ParameterName = "@AditionalAct";
                arr[12].Value = AditionalAct; 


                
                return oData.ExecuteNonQuery(spName, arr, System.Data.CommandType.StoredProcedure);
            }
            catch (Exception Exc)
            {

                MessageBox.Show("Error saving LogProcess. " + Exc.Message, "Controlled message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }



        public static int InsertMaster(string spName, string code, string description, string CodeMaster, string type, string conditionId, string comment)
        {
            try
            {
                SqlParameter[] arr = GetParameters(6);
                arr[0].ParameterName = "@Code";
                arr[0].Value = code;

                arr[1].ParameterName = "@Description";
                arr[1].Value = description;

                arr[2].ParameterName = "@CodeMaster";
                arr[2].Value = CodeMaster;

                arr[3].ParameterName = "@Type";
                arr[3].Value = type;

                arr[4].ParameterName = "@ConditionId";
                arr[4].Value = conditionId;

                arr[5].ParameterName = "@Comment";
                arr[5].Value = comment;
                return oData.ExecuteNonQuery(spName, arr, System.Data.CommandType.StoredProcedure);
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

        public static int DeleteLaborTopography(string spName, string Cod)
        {
            try
            {

                SqlParameter[] arr = GetParameters(1);
                arr[0].ParameterName = "@Cod";
                arr[0].Value= Cod;
                return oData.ExecuteNonQuery(spName, arr, System.Data.CommandType.StoredProcedure);
            }
            catch (Exception Exc)
            {

                MessageBox.Show("Error Delete LogProcess. " + Exc.Message, "Controlled message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public static int InsertLaborTopography(string spName, string Cod, string NomLab, string Niv, string TLab, string ABLab, string Orien, string Nomen, string Dire, string Zon, string Veta, string Fase, string CCos, string Mina, string TSec)
        {
            try
            {

                SqlParameter[] arr = GetParameters(14);
                arr[0].ParameterName = "@Cod";
                arr[0].Value = Cod;

                arr[1].ParameterName = "@NomLab";
                arr[1].Value = NomLab;

                arr[2].ParameterName = "@Niv";
                arr[2].Value = Niv;

                arr[3].ParameterName = "@TLab";
                arr[3].Value = TLab;

                arr[4].ParameterName = "@ABLab";
                arr[4].Value = ABLab;

                arr[5].ParameterName = "@Orien";
                arr[5].Value = Orien;

                arr[6].ParameterName = "@Nomen";
                arr[6].Value = Nomen;

                arr[7].ParameterName = "@Dire";
                arr[7].Value = Dire;

                arr[8].ParameterName = "@Zon";
                arr[8].Value = Zon;

                arr[9].ParameterName = "@Veta";
                arr[9].Value = Veta;

                arr[10].ParameterName = "@Fase";
                arr[10].Value = Fase;
                arr[11].ParameterName = "@CCos";
                arr[11].Value = CCos;

                arr[12].ParameterName = "@Mina";
                arr[12].Value = Mina;

                arr[13].ParameterName = "@TSec";
                arr[13].Value = TSec;
                return oData.ExecuteNonQuery(spName, arr, System.Data.CommandType.StoredProcedure);
            }
            catch (Exception Exc)
            {

                MessageBox.Show("Error saving LogProcess. " + Exc.Message, "Controlled message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

    }
}
