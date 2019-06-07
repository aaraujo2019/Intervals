using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;


public class clsDHSamples
{
    public string sOpcion;
    public string sHoleID;
    public string lFrom;
    public string lTo;

    private DataAccess.ManagerDA oData = new DataAccess.ManagerDA();

    public DataTable getDHSamplesAll()
    {
        try
        {
            DataSet dtDHSamplesAll = new DataSet();
            SqlParameter[] arr = oData.GetParameters(0);
            dtDHSamplesAll = oData.ExecuteDataset("usp_DH_Samples_ListAll", arr, CommandType.StoredProcedure);
            return dtDHSamplesAll.Tables[0];
        }
        catch (Exception eX)
        {
            throw new Exception("Error in DHSamplesAll: " + eX.Message);
        }
    }

    public DataTable getDHSamplesDistinct()
    {
        try
        {
            DataSet dtDHSamplesAll = new DataSet();
            SqlParameter[] arr = oData.GetParameters(0);
            dtDHSamplesAll = oData.ExecuteDataset("usp_DH_Samples_ListDistinct", arr, CommandType.StoredProcedure);
            return dtDHSamplesAll.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in DHSamplesDistinct: " + eX.Message);
        }
    }

    public DataTable getDHSamplesIDSamp(string _sSample)
    {
        try
        {
            DataSet dtDHSamplesIDSamp = new DataSet();
            SqlParameter[] arr = oData.GetParameters(1);
            arr[0].ParameterName = "@sampleId";
            arr[0].Value = _sSample;
            dtDHSamplesIDSamp = oData.ExecuteDataset("usp_DH_Samples_ListSampleId", arr, CommandType.StoredProcedure);
            return dtDHSamplesIDSamp.Tables[0];
        }
        catch (Exception eX)
        {
            throw new Exception("Error in DHSamplesIDSamp: " + eX.Message);
        }
    }

    public DataTable getDHSamples_getRangeValid(string _sIni, string _sFin, string _Id)
    {
        try
        {
            DataSet dtDHSamples = new DataSet();
            SqlParameter[] arr = oData.GetParameters(3);
            arr[0].ParameterName = "@SampleIni";
            arr[0].Value = _sIni;
            arr[1].ParameterName = "@SampleFin";
            arr[1].Value = _sFin;
            arr[2].ParameterName = "@Id";
            arr[2].Value = int.Parse(_Id);
            dtDHSamples = oData.ExecuteDataset("usp_DH_Samples_ListRange", arr, CommandType.StoredProcedure);
            return dtDHSamples.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in DHSamples_getRangeValid: " + eX.Message);
        }
    }


    public DataTable getDHSamples()
    {
        try
        {
            DataSet dtDHSamples = new DataSet();
            SqlParameter[] arr = oData.GetParameters(2);
            arr[0].ParameterName = "@Opcion";
            arr[0].Value = sOpcion;
            arr[1].ParameterName = "@HoleId";
            arr[1].Value = sHoleID;
            dtDHSamples = oData.ExecuteDataset("usp_DH_Samples_List", arr, CommandType.StoredProcedure);
            return dtDHSamples.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in DHSamples: " + eX.Message);
        }
    }

    
    public DataTable getDHSamplesBySamp(string _sample1, string _sample2)
    {
        try
        {
            DataSet dtDHSamples = new DataSet();
            SqlParameter[] arr = oData.GetParameters(3);
            arr[0].ParameterName = "@Sample1";
            arr[0].Value = _sample1;
            arr[1].ParameterName = "@Sample2";
            arr[1].Value = _sample2;
            arr[2].ParameterName = "@HoleID";
            arr[2].Value = sHoleID;
            dtDHSamples = oData.ExecuteDataset("usp_DH_Samples_ListSample", arr, CommandType.StoredProcedure);
            return dtDHSamples.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in getDHSamplesBySamp: " + eX.Message);
        }
    }

    public DataTable getDHSamplesById(string _Id1, string _Id2)
    {
        try
        {
            DataSet dtDHSamples = new DataSet();
            SqlParameter[] arr = oData.GetParameters(3);
            arr[0].ParameterName = "@Id1";
            arr[0].Value = _Id1;
            arr[1].ParameterName = "@Id2";
            arr[1].Value = _Id2;
            arr[2].ParameterName = "@HoleID";
            arr[2].Value = sHoleID;
            dtDHSamples = oData.ExecuteDataset("usp_DH_Samples_ListDHID", arr, CommandType.StoredProcedure);
            return dtDHSamples.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in getDHSamplesById: " + eX.Message);
        }
    }

    //[usp_DH_Samples_ListFromTo]
    public DataTable getDHSamplesFromTo()
    {
        try
        {
            DataSet dtDHSamples = new DataSet();
            SqlParameter[] arr = oData.GetParameters(3);
            arr[0].ParameterName = "@From";
            arr[0].Value = lFrom;
            arr[1].ParameterName = "@To";
            arr[1].Value = lTo;
            arr[2].ParameterName = "@HoleID";
            arr[2].Value = sHoleID;
            dtDHSamples = oData.ExecuteDataset("usp_DH_Samples_ListFromTo", arr, CommandType.StoredProcedure);
            return dtDHSamples.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in DHSamplesFromTo: " + eX.Message);
        }
    }

}

