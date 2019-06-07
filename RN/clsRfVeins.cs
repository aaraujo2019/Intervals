using System;
using System.Data;
using System.Data.SqlClient;



public class clsRfVeins
{
    private DataAccess.ManagerDA oData = new DataAccess.ManagerDA();

    public DataTable getRfVeinsCodes()
    {
        try
        {
            DataSet dtRfVeinsCodesAll = new DataSet();
            SqlParameter[] arr = oData.GetParameters(0);
            dtRfVeinsCodesAll = oData.ExecuteDataset("[dbo].[usp_getRfVeinsCodes]", arr, CommandType.StoredProcedure);
            return dtRfVeinsCodesAll.Tables[0];
        }
        catch (Exception eX)
        {
            throw new Exception("Error in dtRfVeinsCodesAll: " + eX.Message);
        }
    }

}