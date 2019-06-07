using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

public class clsCHSamples
{
    public string sOpcion;
    public string sCHId;

    private DataAccess.ManagerDA oData = new DataAccess.ManagerDA();

    public DataTable getCHSamplesDistinct()
    {
        try
        {
            DataSet dtCHSamplesAll = new DataSet();
            SqlParameter[] arr = oData.GetParameters(0);
            dtCHSamplesAll = oData.ExecuteDataset("usp_CH_Samples_ListDistinct", arr, CommandType.StoredProcedure);
            return dtCHSamplesAll.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in CHSamplesDistinct: " + eX.Message);
        }
    }


    public DataTable getCHSamples()
    {
        try
        {
            DataSet dtCHSamples = new DataSet();
            SqlParameter[] arr = oData.GetParameters(2);
            arr[0].ParameterName = "@Opcion";
            arr[0].Value = sOpcion;
            arr[1].ParameterName = "@ChId";
            arr[1].Value = sCHId;
            dtCHSamples = oData.ExecuteDataset("usp_CH_Samples_List", arr, CommandType.StoredProcedure);
            return dtCHSamples.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in CHSamples: " + eX.Message);
        }
    }


    public DataTable getCHSamplesBySample(string _Samp1, string _Samp2)
    {
        try
        {
      
            DataSet dtDHSamples = new DataSet();
            SqlParameter[] arr = oData.GetParameters(3);
            arr[0].ParameterName = "@Samp1";
            arr[0].Value = _Samp1;
            arr[1].ParameterName = "@Samp2";
            arr[1].Value = _Samp2;
            arr[2].ParameterName = "@ChID";
            arr[2].Value = sCHId;
        
            dtDHSamples = oData.ExecuteDataset1("usp_CH_Samples_ListSample", arr, CommandType.StoredProcedure);

        
            return dtDHSamples.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in getCHSamplesById: " + eX.Message);
        }
    }

    public DataTable getCHSamplesById(string _Id1, string _Id2)
    {
        try
        {
            DataSet dtDHSamples = new DataSet();
            SqlParameter[] arr = oData.GetParameters(3);
            arr[0].ParameterName = "@Id1";
            arr[0].Value = _Id1;
            arr[1].ParameterName = "@Id2";
            arr[1].Value = _Id2;
            arr[2].ParameterName = "@ChID";
            arr[2].Value = sCHId;
            dtDHSamples = oData.ExecuteDataset("usp_CH_Samples_ListDHID", arr, CommandType.StoredProcedure);
            return dtDHSamples.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in getCHSamplesById: " + eX.Message);
        }
    }

    public DataTable getCHSamples_getRangeValid(string _sIni, string _sFin, string _Id)
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
            dtDHSamples = oData.ExecuteDataset("usp_CH_Samples_ListRange", arr, CommandType.StoredProcedure);
            return dtDHSamples.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in CHSamples_getRangeValid: " + eX.Message);
        }
    }

}

