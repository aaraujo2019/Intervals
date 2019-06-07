using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;
using Entities.Config;
using RN.DataAccess;
using System.Windows.Forms;

public class clsLabSubmit
{

    public string sOpcion;
    public string sSubmit;
    public string sDateShipment;
    public string sWorkOrder;
    public string sLabCode;
    public string sPreparationLab;
    public string sAnaliticalLab;
    public string sDispatchedBy;
    public string sSampleType;
    public string sPrepCode;
    public string sAnalisysCode;
    public string sMetAnalisysCode;
    public string sOtherAnalisysCode;
    public string sInstructions;
    public string sReturnResults;
    public int iNoBags;
    public string sObservations;
    public string sSampleNature;
    public string sElement;
    public string sTypeSumit;
    public string sHoleID;
    public string sIdentificationUser;


    private DataAccess.ManagerDA oData = new DataAccess.ManagerDA();


    public IList<Ent_Prefix> getLabSubmitEntities()
    {
        try
        {
            DataSet dtLabSubmit = new DataSet();
            SqlParameter[] arr = oData.GetParameters(2);
             arr[0].ParameterName = "@Opcion";
            arr[0].Value = sOpcion;
            arr[1].ParameterName = "@Submit";
            arr[1].Value = sSubmit;

            using (IDataReader reader = oData.GetContactsReaderGeneric("usp_LabSumit_List", arr, CommandType.StoredProcedure))
            {
                List<Ent_Prefix> contacts = new List<Ent_Prefix>();
                IList<Ent_Prefix> list = contacts;
                list.LoadFromReader(reader);
                return list;
            }
        }
        catch (Exception eX)
        {
            throw new Exception("Error in LabSubmit: " + eX.Message);
        }
    }

    public DataTable getLabSubmit()
    {
        try
        {
            DataSet dtLabSubmit = new DataSet();
            SqlParameter[] arr = oData.GetParameters(2);
            //arr(0).ParameterName = "@Id"
            //arr(0).Value = 2
            arr[0].ParameterName = "@Opcion";
            arr[0].Value = sOpcion;
            arr[1].ParameterName = "@Submit";
            arr[1].Value = sSubmit;




            dtLabSubmit = oData.ExecuteDataset("usp_LabSumit_List", arr, CommandType.StoredProcedure);
            //ds = oDAtos.ExecuteDataset("usp_Datos_ListByID", arr, CommandType.StoredProcedure)
            return dtLabSubmit.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in LabSubmit: " + eX.Message);
        }
    }

    public string LabSubmit_Add()
    {
        try
        {
            object oRes;
            SqlParameter[] arr = oData.GetParameters(21);
            arr[0].ParameterName = "@Opcion";
            arr[0].Value = sOpcion;
            arr[1].ParameterName = "@Submit";
            arr[1].Value = sSubmit;
            arr[2].ParameterName = "@DateShipment";
            arr[2].Value = sDateShipment;
            //arr[3].ParameterName = "@WorkOrder";
            //arr[3].Value = sWorkOrder;
            arr[3].ParameterName = "@LabCode";
            arr[3].Value = sLabCode;
            arr[4].ParameterName = "@PreparationLab";
            arr[4].Value = sPreparationLab;
            arr[5].ParameterName = "@AnaliticalLab";
            arr[5].Value = sAnaliticalLab;
            arr[6].ParameterName = "@DispatchedBy";
            arr[6].Value = sDispatchedBy;
            arr[7].ParameterName = "@SampleType";
            arr[7].Value = sSampleType;
            arr[8].ParameterName = "@PrepCode";
            arr[8].Value = sPrepCode;
            arr[9].ParameterName = "@AnalisysCode";
            arr[9].Value = sAnalisysCode;
            arr[10].ParameterName = "@MetAnalisysCode";
            arr[10].Value = sMetAnalisysCode;
            arr[11].ParameterName = "@OtherAnalisysCode";
            arr[11].Value = sOtherAnalisysCode;
            arr[12].ParameterName = "@Instructions";
            arr[12].Value = sInstructions;
            arr[13].ParameterName = "@ReturnResults";
            arr[13].Value = sReturnResults;
            arr[14].ParameterName = "@NoBags";
            arr[14].Value = iNoBags;
            arr[15].ParameterName = "@Observations";
            arr[15].Value = sObservations;
            arr[16].ParameterName = "@SampleNature";
            arr[16].Value = sSampleNature;
            arr[17].ParameterName = "@Element";
            arr[17].Value = sElement;
            arr[18].ParameterName = "@TypeSumit";
            arr[18].Value = sTypeSumit;
            arr[19].ParameterName = "@HoleID";
            arr[19].Value = sHoleID;
            arr[20].ParameterName = "@IdentificationUser";
            arr[20].Value = clsRf.sIdentification;
            oRes = oData.ExecuteScalar("usp_LabSumit_Insert", arr, CommandType.StoredProcedure);
            //ds = oDAtos.ExecuteDataset("usp_Datos_ListByID", arr, CommandType.StoredProcedure)
            return oRes.ToString();


        }
        catch (Exception eX)
        {
            throw new Exception("Save error LabSubmit. " + eX.Message); ;
        }
    }

    public DataTable getLabSubmit_Dispatchedby()
    {
        try
        {
            DataSet dtLabSubmit = new DataSet();
            SqlParameter[] arr = oData.GetParameters(1);
            arr[0].ParameterName = "@DispatchedBy";
            arr[0].Value = sDispatchedBy;
            dtLabSubmit = oData.ExecuteDataset("usp_LabSubmitGetDispatchedby", arr, CommandType.StoredProcedure);

            return dtLabSubmit.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in LabSubmit: " + eX.Message);
        }
    }

    public int alterdataBase(string query)
    {
        try
        {
            SqlParameter[] ParamLog = oData.GetParameters(0);
            return oData.ExecuteNonQuery(query, ParamLog, System.Data.CommandType.Text);
        }
        catch (Exception Exc)
        {

            MessageBox.Show("Error saving LogProcess. " + Exc.Message, "Controlled message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return -1;
        }
    }


    public  object Exist_DB(string query, string jobno, string sample,string param1, string param2)
    {
        try
        {
            object oRes;
            SqlParameter[] arr = oData.GetParameters(2);
            arr[0].ParameterName = param1;
            arr[0].Value = jobno;
            arr[1].ParameterName = param2 ;
            arr[1].Value = sample;

            oRes = oData.ExecuteScalar(query, arr, CommandType.Text);
            
            return oRes;

        }
        catch (Exception eX)
        {
            throw new Exception("Save error LabSubmitIn. " + eX.Message); ;
        }
    }
}




