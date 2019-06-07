using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

public class clsLabSumitInterval
{
    public string sOpcion;
    public string sSubmit;
    public string sSampleFrom;
    public string sSampleTo;
    public long lTotalSample;
    //public string sSampleType;
    //public string sPrepCode;
    //public string sAnalisysCode;
    //public string sElement;
    //public string sSampleNature;

    public static DataTable dtIntervals = new DataTable();

    private DataAccess.ManagerDA oData = new DataAccess.ManagerDA();

    public DataTable getLabSubmitInterval()
    {
        try
        {
            DataSet dtLabSubmitInterval = new DataSet();
            SqlParameter[] arr = oData.GetParameters(2);
            //arr(0).ParameterName = "@Id"
            //arr(0).Value = 2
            arr[0].ParameterName = "@Opcion";
            arr[0].Value = sOpcion;
            arr[1].ParameterName = "@Submit";
            arr[1].Value = sSubmit;
            dtLabSubmitInterval = oData.ExecuteDataset("usp_LabSumitInterval_List", arr, CommandType.StoredProcedure);
            return dtLabSubmitInterval.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in LabSubmitInterval: " + eX.Message);
        }
    }

    public string LabSubmitInterval_Add()
    {
        try
        {
            object oRes;
            SqlParameter[] arr = oData.GetParameters(5);
            arr[0].ParameterName = "@Opcion";
            arr[0].Value = sOpcion;
            arr[1].ParameterName = "@Submit";
            arr[1].Value = sSubmit;
            arr[2].ParameterName = "@SampleFrom";
            arr[2].Value = sSampleFrom;
            arr[3].ParameterName = "@SampleTo";
            arr[3].Value = sSampleTo;
            arr[4].ParameterName = "@TotalSample";
            arr[4].Value = lTotalSample;
            //arr[5].ParameterName = "@SampleType";
            //arr[5].Value = sSampleType;
            //arr[6].ParameterName = "@PrepCode";
            //arr[6].Value = sPrepCode;
            //arr[7].ParameterName = "@AnalisysCode";
            //arr[7].Value = sAnalisysCode;
            //arr[8].ParameterName = "@Element";
            //arr[8].Value = sElement;
            //arr[9].ParameterName = "@SampleNature";
            //arr[9].Value = sSampleNature;
            oRes = oData.ExecuteScalar("usp_LabSumitInterval_Insert", arr, CommandType.StoredProcedure);
            
            return oRes.ToString();


        }
        catch (Exception eX)
        {
            throw new Exception("Save error LabSubmitInterval. " + eX.Message); ;
        }
    }

    public string LabSubmitInterval_Delete()
    {
        try
        {
            object oRes;
            SqlParameter[] arr = oData.GetParameters(1);
            arr[0].ParameterName = "@Submit";
            arr[0].Value = sSubmit;
            oRes = oData.ExecuteScalar("usp_LabSumitInterval_Delete", arr, CommandType.StoredProcedure);

            return oRes.ToString();

        }
        catch (Exception eX)
        {
            throw new Exception("Delete error LabSubmitInterval. " + eX.Message); ;
        }
    }

}

