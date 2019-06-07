using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;


public class clsLabsumitIn
{
    public string sOpcion;
    public string sSubmit;
    public int iCon;
    public string sSample;
    public int iSack;
    public int iWeight;
    public Int64 iId;


    public static DataTable dtIn = new DataTable();

    private DataAccess.ManagerDA oData = new DataAccess.ManagerDA();

    public DataTable getLabSubmitIn()
    {
        try
        {
            DataSet dtLabSubmitIn = new DataSet();
            SqlParameter[] arr = oData.GetParameters(2);
            //arr(0).ParameterName = "@Id"
            //arr(0).Value = 2
            arr[0].ParameterName = "@Opcion";
            arr[0].Value = sOpcion;
            arr[1].ParameterName = "@Submit";
            arr[1].Value = sSubmit;
            dtLabSubmitIn = oData.ExecuteDataset("usp_LabSumitIn_List", arr, CommandType.StoredProcedure);
            return dtLabSubmitIn.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in LabSubmitIn: " + eX.Message);
        }
    }

    public string LabSubmitIn_Add()
    {
        try
        {
            object oRes;
            SqlParameter[] arr = oData.GetParameters(7);
            arr[0].ParameterName = "@Opcion";
            arr[0].Value = sOpcion;
            arr[1].ParameterName = "@Submit";
            arr[1].Value = sSubmit;
            arr[2].ParameterName = "@Con";
            arr[2].Value = iCon;
            arr[3].ParameterName = "@Sample";
            arr[3].Value = sSample;
            arr[4].ParameterName = "@Sack";
            arr[4].Value = iSack;
            arr[5].ParameterName = "@Weight";
            arr[5].Value = iWeight;
            arr[6].ParameterName = "@DHSamplesID";
            arr[6].Value = iId; 
            oRes = oData.ExecuteScalar("usp_LabSumitIn_Insert", arr, CommandType.StoredProcedure);

            return oRes.ToString();


        }
        catch (Exception eX)
        {
            throw new Exception("Save error LabSubmitIn. " + eX.Message); ;
        }
    }

    public string LabSubmitIn_Delete()
    {
        try
        {
            object oRes;
            SqlParameter[] arr = oData.GetParameters(1);
            arr[0].ParameterName = "@Submit";
            arr[0].Value = sSubmit;
            oRes = oData.ExecuteScalar("usp_LabSumitIn_Delete", arr, CommandType.StoredProcedure);

            return oRes.ToString();

        }
        catch (Exception eX)
        {
            throw new Exception("Delete error LabSubmitIn. " + eX.Message); ;
        }
    }

    public DataTable getLabSubmitInBySampleReAssay()
    {
        try
        {
            DataSet dtLabSubmitIn = new DataSet();
            SqlParameter[] arr = oData.GetParameters(1);
            //arr(0).ParameterName = "@Id"
            //arr(0).Value = 2
            arr[0].ParameterName = "@Sample";
            arr[0].Value = sSample;
            dtLabSubmitIn = oData.ExecuteDataset("usp_LabSumitIn_ListBySample", arr, CommandType.StoredProcedure);
            return dtLabSubmitIn.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in LabSubmitInBySampleReAssay: " + eX.Message);
        }
    }

}

