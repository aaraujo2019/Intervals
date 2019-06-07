using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;


public class clsAssay
{
    public string sSubmit;
    public string sSample;
    public string sJob;

    private DataAccess.ManagerDA oData = new DataAccess.ManagerDA();

    public string AssayLabresult_Delete()
    {
        try
        {
            object oRes;
            SqlParameter[] arr = oData.GetParameters(1);
            arr[0].ParameterName = "@JobNo";
            arr[0].Value = sJob;
            oRes = oData.ExecuteScalar("usp_LabResultAssay_Delete", arr, CommandType.StoredProcedure);

            return oRes.ToString();

        }
        catch (Exception eX)
        {
            throw new Exception("Del error AssayLabresult_Del. " + eX.Message); ;
        }
        
    }

    public string Assay_Edit()
    {
        try
        {
            object oRes;
            SqlParameter[] arr = oData.GetParameters(2);
            arr[0].ParameterName = "@Sample";
            arr[0].Value = sSample;
            arr[1].ParameterName = "@Submit";
            arr[1].Value = sSubmit;
            oRes = oData.ExecuteScalar("usp_Assay_Edit_qaqc", arr, CommandType.StoredProcedure);

            return oRes.ToString();

        }
        catch (Exception eX)
        {
            throw new Exception("Save error Assay_Edit. " + eX.Message); ;
        }
    }
}

