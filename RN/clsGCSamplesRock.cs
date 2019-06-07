using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;



public class clsGCSamplesRock
{
    public string sOpcion;

    private DataAccess.ManagerDA oData = new DataAccess.ManagerDA();

    public DataTable getGCSamplesRockAll()
    {
        try
        {
            DataSet dtGCSamplesRock = new DataSet();
            SqlParameter[] arr = oData.GetParameters(1);
            arr[0].ParameterName = "@Opcion";
            arr[0].Value = sOpcion;
            dtGCSamplesRock = oData.ExecuteDataset("usp_GC_SamplesRock_List", arr, CommandType.StoredProcedure);
            return dtGCSamplesRock.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in GCSamplesRockAll: " + eX.Message);
        }
    }

    //[usp_GC_SamplesRock_ListSample]
    public DataTable getGC_SamplesRockBySample(string _Sample)
    {
        try
        {
            DataSet dtGC_SamplesRock = new DataSet();
            SqlParameter[] arr = oData.GetParameters(1);
            arr[0].ParameterName = "@Sample";
            arr[0].Value = _Sample;
            dtGC_SamplesRock = oData.ExecuteDataset("usp_GC_SamplesRock_ListSample", arr, CommandType.StoredProcedure);
            return dtGC_SamplesRock.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in getdtGC_SamplesRockBySample: " + eX.Message);
        }
    }
    
    public DataTable getGC_SamplesRockById(string _Id1, string _Id2)
    {
        try
        {
            DataSet dtGC_SamplesRock = new DataSet();
            SqlParameter[] arr = oData.GetParameters(2);
            arr[0].ParameterName = "@Id1";
            arr[0].Value = _Id1;
            arr[1].ParameterName = "@Id2";
            arr[1].Value = _Id2;
            dtGC_SamplesRock = oData.ExecuteDataset("usp_GC_SamplesRock_ListDHID", arr, CommandType.StoredProcedure);
            return dtGC_SamplesRock.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in getdtGC_SamplesRockById: " + eX.Message);
        }
    }

    public DataTable getGC_SamplesRock_getRangeValid(string _sIni, string _sFin, string _Id)
    {
        try
        {
            DataSet dtGC_SamplesRock = new DataSet();
            SqlParameter[] arr = oData.GetParameters(3);
            arr[0].ParameterName = "@SampleIni";
            arr[0].Value = _sIni;
            arr[1].ParameterName = "@SampleFin";
            arr[1].Value = _sFin;
            arr[2].ParameterName = "@Id";
            arr[2].Value = int.Parse(_Id);
            dtGC_SamplesRock = oData.ExecuteDataset("usp_GC_SamplesRock_ListRange", arr, CommandType.StoredProcedure);
            return dtGC_SamplesRock.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in dtGC_SamplesRock_getRangeValid: " + eX.Message);
        }
    }


}

