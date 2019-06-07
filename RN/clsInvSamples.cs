using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;
using RN.DataAccess;
using Entities.Config;

public class clsInvSamples
{
    public string sOpcion;
    public int? iId_User;
    public string sDateAssign;
    public string sFromAssign;
    public string sToAssign;
    public string sType;
    public Int64 iSKInvSamplesControls;
    public Int64 iSKInvSamples;

    private DataAccess.ManagerDA oData = new DataAccess.ManagerDA();

    public string Inv_Samples_Mass(string _sTabla, DataTable _dtData)
    {
        try
        {
            oData.UpdateBulkCopy(_sTabla, _dtData);
            return "OK";
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getInvSamples_ListRange(Int64 _iFrom, Int64 _iTo)
    {
        try
        {
            DataSet dtSamplesList = new DataSet();
            SqlParameter[] arr = oData.GetParameters(2);
            arr[0].ParameterName = "@FromSKInvSamples";
            arr[0].Value = _iFrom;
            arr[1].ParameterName = "@ToSKInvSamples";
            arr[1].Value = _iTo;
            dtSamplesList = oData.ExecuteDataset("usp_Inv_Samples_Control_List_Range", arr, CommandType.StoredProcedure);
            return dtSamplesList.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in InvSamples_ListRange: " + eX.Message);
        }
    }


        
 
    public IList<Entities.Person.SampleControl> getSamplesAssign()
    {
        try
        {
            SqlParameter[] arr = oData.GetParameters(2);
            arr[0].ParameterName = "@Opcion";
            arr[0].Value = sOpcion;
            arr[1].ParameterName = "@Id_User";
            arr[1].Value = iId_User;


            using (IDataReader reader = oData.GetContactsReaderGeneric("usp_Inv_Samples_Control_List", arr, CommandType.StoredProcedure))
            {
                List<Entities.Person.SampleControl> contacts = new List<Entities.Person.SampleControl>();
                IList<Entities.Person.SampleControl> list = contacts;
                list.LoadFromReader(reader);

                return list;
            }
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

  

    public DataSet getInvSamples_List()
    {
        try
        {
            DataSet dtSamplesList = new DataSet();
            SqlParameter[] arr = oData.GetParameters(2);
            arr[0].ParameterName = "@Opcion";
            arr[0].Value = sOpcion;
            arr[1].ParameterName = "@Id_User";
            arr[1].Value = iId_User;
            dtSamplesList = oData.ExecuteDataset("usp_Inv_Samples_Control_List", arr, CommandType.StoredProcedure);
            return dtSamplesList;

        }
        catch (Exception eX)
        {
            throw new Exception("Error in InvSamples_List: " + eX.Message);
        }
    }

    public string InvSamplesControl_Add()
    {
        try
        {

            object oRes;
            SqlParameter[] arr = oData.GetParameters(6);
            arr[0].ParameterName = "@Opcion";
            arr[0].Value = sOpcion;
            arr[1].ParameterName = "@Id_User";
            arr[1].Value = iId_User;
            arr[2].ParameterName = "@Date_Assign";
            arr[2].Value = sDateAssign;
            arr[3].ParameterName = "@FromAssign";
            arr[3].Value = sFromAssign;
            arr[4].ParameterName = "@ToAssign";
            arr[4].Value = sToAssign;
            arr[5].ParameterName = "@SKInvSamplesControls";
            arr[5].Value = iSKInvSamplesControls;

            oRes = oData.ExecuteScalar("usp_Inv_Samples_Control_Insert", arr, CommandType.StoredProcedure);
            return oRes.ToString();

        }
        catch (Exception eX)
        {
            throw new Exception("Save error Inv_Samples_Control. " + eX.Message); ;
        }
    }

    public string InvSamples_Update()
    {
        try
        {
            object oRes;
            SqlParameter[] arr = oData.GetParameters(2);
            arr[0].ParameterName = "@Type";
            arr[0].Value = sType;
            arr[1].ParameterName = "@SKInvSamples";
            arr[1].Value = iSKInvSamples;

            oRes = oData.ExecuteScalar("usp_Inv_Samples_Update", arr, CommandType.StoredProcedure);
            return oRes.ToString();
        }
        catch (Exception eX)
        {
            throw new Exception("Update error Inv_Samples. " + eX.Message); ;
        }
    }
        

}

