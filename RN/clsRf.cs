using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using RN.DataAccess;
using Entities.Person;
using Entities.Config;

public class clsRf
{
    private DataAccess.ManagerDA oData = new DataAccess.ManagerDA();

    public static string sUser;
    public static string sIdentification;
    public static string sIdGrupo;
    public static string cConnection;
    public static bool valueIntercpt;

    public static bool valueTopografish;
    public static DataSet dsPermisos;
    public int iIdProject;


    public DataTable getShipmgreaterthan10days()
    {
        try
        {
            DataSet dtDHShip = new DataSet();
            SqlParameter[] arr = oData.GetParameters(0);
            dtDHShip = oData.ExecuteDataset("usp_Envios_ListMayor10dias", arr, CommandType.StoredProcedure);
            return dtDHShip.Tables[0];
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public AutoCompleteStringCollection AutoCompleteCmb(DataTable _dtAutoComplete, string _sRow)
    {
        try
        {

            AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();
            foreach (DataRow row in _dtAutoComplete.Rows)
            {
                stringCol.Add(Convert.ToString(row[_sRow]));
            }

            return stringCol;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getRfTypeSampleQaqc()
    {
        try
        {
            DataSet dsRfTypeSample = new DataSet();
            SqlParameter[] arr = oData.GetParameters(0);
            dsRfTypeSample = oData.ExecuteDataset("usp_RfTypeSampleQaqc_List", arr, CommandType.StoredProcedure);
            return dsRfTypeSample.Tables[0];
        }
        catch (Exception eX)
        {

            throw new Exception(eX.Message);
        }
    }

    public IList<Ent_Prefix> getRfTypeSampleQaqcEntity()
    {
        try
        {
            SqlParameter[] arr = oData.GetParameters(0);

            using (IDataReader reader = oData.GetContactsReaderGeneric("usp_RfTypeSampleQaqc_List", arr, CommandType.StoredProcedure))
            {
                List<Ent_Prefix> contacts = new List<Ent_Prefix>();
                IList<Ent_Prefix> list = contacts;
                list.LoadFromReader(reader);

                return list;

            }
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    


    public DataTable getRfTypeSample()
    {
        try
        {
            DataSet dsRfTypeSample = new DataSet();
            SqlParameter[] arr = oData.GetParameters(0);
            dsRfTypeSample = oData.ExecuteDataset("usp_DH_RfTypeSample_List", arr, CommandType.StoredProcedure);
            return dsRfTypeSample.Tables[0];
        }
        catch (Exception eX)
        {

            throw new Exception(eX.Message);
        }
    }

    public IList<Ent_User> getUsersEntity()
    {
        try
        {
            SqlParameter[] arr = oData.GetParameters(1);
            arr[0].ParameterName = "@sUsuario";
            arr[0].Value = "QAQC";
           
            using (IDataReader reader = oData.GetContactsReaderGeneric("usp_getUsuarios_PORTAL", arr, CommandType.StoredProcedure))
            {
                List<Ent_User> contacts = new List<Ent_User>();
                IList<Ent_User> list = contacts;
                list.LoadFromReader(reader);

                return list;

            }
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }


    public DataTable getUsers(string _sUser)
    {
        try
        {
            DataSet dsUser = new DataSet();
            SqlParameter[] arr = oData.GetParameters(1);
            arr[0].ParameterName = "@sUsuario";
            arr[0].Value = _sUser;
            dsUser = oData.ExecuteDataset("usp_getUsuarios_PORTAL", arr, CommandType.StoredProcedure);
            return dsUser.Tables[0];
        }
        catch (Exception eX)
        {

            throw new Exception(eX.Message);
        }
    }
    public DataTable getVersionProject1()
    {
        try
        {
            DataSet dtVersion = new DataSet();
            SqlParameter[] arr = oData.GetParameters(1);
            arr[0].ParameterName = "@Id";
            arr[0].Value = iIdProject;
            dtVersion = oData.ExecuteDataset("usp_getProject", arr, CommandType.StoredProcedure);
            //
            using (IDataReader reader = oData.GetContactsReader())
            {
                List<version1> contacts = new List<version1>();
                IList<version1> list = contacts;
                list.LoadFromReader(reader);

                // return contacts;
            }
            return dtVersion.Tables[0];

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public class version1
    {
        public int IDProject { get; set; }
        public string Project { get; set; }

        public string version { get; set; }
    }

    public IList<Ent_version> getVersionProject()
    {
        try
        {
            SqlParameter[] arr = oData.GetParameters(1);
            arr[0].ParameterName = "@Id";
            arr[0].Value = iIdProject;

            using (IDataReader reader = oData.GetContactsReaderGeneric("usp_getProject", arr, CommandType.StoredProcedure))
            {
                List<Ent_version> contacts = new List<Ent_version>();
                IList<Ent_version> list = contacts;
                list.LoadFromReader(reader);

                return list;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }



    public DataTable getLabResult(string _sOpcion, string _sSubmit)
    {
        {
            try
            {
                DataSet dtQCReport = new DataSet();
                SqlParameter[] arr = oData.GetParameters(2);
                arr[0].ParameterName = "@Opcion";
                arr[0].Value = _sOpcion;
                arr[1].ParameterName = "@Submit";
                arr[1].Value = _sSubmit;
                dtQCReport = oData.ExecuteDataset("usp_LabResult_List", arr, CommandType.StoredProcedure);
                return dtQCReport.Tables[0];
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }

    public DataTable getQCReport(string _sJobno)
    {
        {
            try
            {
                DataSet dtQCReport = new DataSet();
                SqlParameter[] arr = oData.GetParameters(1);
                arr[0].ParameterName = "@jobno";
                arr[0].Value = _sJobno;
                dtQCReport = oData.ExecuteDataset("usp_QCReport_List", arr, CommandType.StoredProcedure);
                return dtQCReport.Tables[0];
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }

    public string InsertTrans(string _sTABLE, string _TRANS, string _LOGINTRANS, string _DATOSTRANS)
    {
        try
        {
            object oRes;
            SqlParameter[] arr = oData.GetParameters(4);
            arr[0].ParameterName = "@sTABLE";
            arr[0].Value = _sTABLE;
            arr[1].ParameterName = "@TRANS";
            arr[1].Value = _TRANS;
            arr[2].ParameterName = "@LOGINTRANS";
            arr[2].Value = _LOGINTRANS;
            arr[3].ParameterName = "@DATOSTRANS";
            arr[3].Value = _DATOSTRANS;
            oRes = oData.ExecuteScalar("[usp_TransactionsAdd]", arr, CommandType.StoredProcedure);
            return oRes.ToString();

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public string ExecSQL(string _sSQL)
    {
        try
        {
            object oRes;
            SqlParameter[] arr = oData.GetParameters(1);
            arr[0].ParameterName = "@SQL";

            arr[0].Value = _sSQL;
            oRes = oData.ExecuteScalar("usp_Dinamico_SQL", arr, CommandType.StoredProcedure);
            return oRes.ToString();
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }

    //[usp_RfQCConversion_List]
    public DataTable getRfQCConversion()
    {
        try
        {
            DataSet dtRfQCConversion = new DataSet();
            SqlParameter[] arr = oData.GetParameters(0);
            dtRfQCConversion = oData.ExecuteDataset("usp_RfQCConversion_List", arr, CommandType.StoredProcedure);
            return dtRfQCConversion.Tables[0];
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    //usp_RfWorker_ListByCred
    public DataTable getRfWorkerCred(string _sCod, string _sPass)
    {
        try
        {
            DataSet dtRfWorker = new DataSet();
            SqlParameter[] arr = oData.GetParameters(2);
            arr[0].ParameterName = "@Cod";
            arr[0].Value = _sCod;
            arr[1].ParameterName = "@Password";
            arr[1].Value = _sPass;
            dtRfWorker = oData.ExecuteDataset("usp_RfWorker_ListByCred", arr, CommandType.StoredProcedure);
            return dtRfWorker.Tables[0];
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }


    public IList<Ent_Prefix> getRfPrefixGeneric()
    {
        try
        {
            SqlParameter[] arr = oData.GetParameters(0);


            using (IDataReader reader = oData.GetContactsReaderGeneric("usp_Prefix_List", arr, CommandType.StoredProcedure))
            {
                List<Ent_Prefix> contacts = new List<Ent_Prefix>();
                IList<Ent_Prefix> list = contacts;
                list.LoadFromReader(reader);

                return list;
            }
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }


    public DataTable getRfPrefix()
    {
        try
        {
            DataSet dtRfPrefix = new DataSet();
            SqlParameter[] arr = oData.GetParameters(0);
            dtRfPrefix = oData.ExecuteDataset("usp_Prefix_List", arr, CommandType.StoredProcedure);
            return dtRfPrefix.Tables[0];


        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public DataTable getRfWorker()
    {
        try
        {
            DataSet dtRfWorker = new DataSet();
            SqlParameter[] arr = oData.GetParameters(0);
            dtRfWorker = oData.ExecuteDataset("usp_RfWorker_List", arr, CommandType.StoredProcedure);
            return dtRfWorker.Tables[0];
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public DataTable getRfCodeLab()
    {
        try
        {
            DataSet dtRfCodeLab = new DataSet();
            SqlParameter[] arr = oData.GetParameters(0);
            dtRfCodeLab = oData.ExecuteDataset("usp_RfCodeLab_List", arr, CommandType.StoredProcedure);
            return dtRfCodeLab.Tables[0];
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    //
    public DataTable getRfPreparationCode()
    {
        try
        {
            DataSet dtRfPreparationCode = new DataSet();
            SqlParameter[] arr = oData.GetParameters(0);
            dtRfPreparationCode = oData.ExecuteDataset("usp_RfPreparationCode_List", arr, CommandType.StoredProcedure);
            return dtRfPreparationCode.Tables[0];
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    //
    public DataTable getRfAnalysisCodeLab()
    {
        try
        {
            DataSet dtRfAnalysisCodeLab = new DataSet();
            SqlParameter[] arr = oData.GetParameters(0);
            dtRfAnalysisCodeLab = oData.ExecuteDataset("usp_RfAnalysisCodeLab_List", arr, CommandType.StoredProcedure);
            return dtRfAnalysisCodeLab.Tables[0];
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }


    public IList<Ent_User> getUsersGeneric(string _sLogin)
    {
        try
        {
            SqlParameter[] arr = oData.GetParameters(1);
            arr[0].ParameterName = "@sLogin";
            arr[0].Value = _sLogin;
            using (IDataReader reader = oData.GetContactsReaderGeneric("usp_getUsersSubpartners_PORTAL", arr, CommandType.StoredProcedure))
            {
                List<Ent_User> user = new List<Ent_User>();
                IList<Ent_User> list = user;
                list.LoadFromReader(reader);
                return list;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getUsersPortal(string _sLogin)
    {
        try
        {
            DataSet dtUsersPortal = new DataSet();
            SqlParameter[] arr = oData.GetParameters(1);
            arr[0].ParameterName = "@sLogin";
            arr[0].Value = _sLogin;
            dtUsersPortal = oData.ExecuteDataset("usp_getUsersSubpartners_PORTAL", arr, CommandType.StoredProcedure);
            return dtUsersPortal.Tables[0];
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }





    //Permisos por formulario
    public DataSet getFormsByGrupoAll(string _IdGrupo)
    {
        try
        {
            DataSet dsFormsGroup = new DataSet();
            SqlParameter[] arr = oData.GetParameters(1);
            arr[0].ParameterName = "@idGrupo";
            arr[0].Value = _IdGrupo;
            dsFormsGroup = oData.ExecuteDataset("usp_getFormByGrupoAll_PORTAL", arr, CommandType.StoredProcedure);
            return dsFormsGroup;
        }
        catch (Exception eX)
        {

            throw new Exception(eX.Message);
        }
    }


    //Permisos por formulario
    public DataTable getFormsByGrupo(string _sIdGrupo, string _sIDGrupo)
    {
        try
        {
            DataSet dtFormsGroup = new DataSet();
            SqlParameter[] arr = oData.GetParameters(2);
            arr[0].ParameterName = "@idGrupo";
            arr[0].Value = _sIdGrupo;
            arr[1].ParameterName = "@ID_Project";
            arr[1].Value = _sIDGrupo;
            dtFormsGroup = oData.ExecuteDataset("usp_getFormulariosByGrupo", arr, CommandType.StoredProcedure);
            return dtFormsGroup.Tables[0];
        }
        catch (Exception eX)
        {

            throw new Exception(eX.Message);
        }
    }

    //[]
    //Permisos en cada formulario por cada accion (insertar, modificar, eliminar ...)
    public DataTable getPermisosFormsByGrupo(string _IdGrupo)
    {
        try
        {
            DataSet dtPermFormsGroup = new DataSet();
            SqlParameter[] arr = oData.GetParameters(1);
            arr[0].ParameterName = "@idGrupo";
            arr[0].Value = _IdGrupo;
            dtPermFormsGroup = oData.ExecuteDataset("usp_getPermisosFormByGrupo_PORTAL", arr, CommandType.StoredProcedure);
            return dtPermFormsGroup.Tables[0];
        }
        catch (Exception eX)
        {

            throw new Exception(eX.Message);
        }
    }

}

