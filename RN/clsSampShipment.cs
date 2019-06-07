using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;
using Entities.Config;
using RN.DataAccess;
using System.Windows.Forms;

public class clsSampShipment
{
    public int iID;
    public string sCode;
    public string sDescription;
    public bool bStatus;
    public string sObservation;
    public long lCost;


    private DataAccess.ManagerDA oData = new DataAccess.ManagerDA();

    //Implementar Solucion
    public DataTable getSampShipment()
    {
        try
        {
            SqlParameter[] arr = oData.GetParameters(1);
            arr[0].ParameterName = "@Code";
            arr[0].Value = sCode;


            using (IDataReader reader = oData.GetContactsReaderGeneric("usp_Rfsampleofnature_List_All", arr, CommandType.StoredProcedure))
            {
                List<Ent_Prefix> contacts = new List<Ent_Prefix>();
                IList<Ent_Prefix> list = contacts;
                list.LoadFromReader(reader);

                return new DataTable();
            }

        }
        catch (Exception eX)
        {
            throw new Exception("Error in SampShipment: " + eX.Message);
        }
    }

    public DataTable getSampShipmentAll()
    {
        try
        {
            DataSet dtSampShipment = new DataSet();
            SqlParameter[] arr = oData.GetParameters(0);
            dtSampShipment = oData.ExecuteDataset("usp_RfTypeSampShipment_List_All", arr, CommandType.StoredProcedure);
            return dtSampShipment.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in SampShipment: " + eX.Message);
        }
    }

    public IList<Ent_Prefix> getSampShipmentAllEnt()
    {
        try
        {
            SqlParameter[] arr = oData.GetParameters(0);

            using (IDataReader reader = oData.GetContactsReaderGeneric("usp_RfTypeSampShipment_List_All", arr, CommandType.StoredProcedure))
            {
                List<Ent_Prefix> contacts = new List<Ent_Prefix>();
                IList<Ent_Prefix> list = contacts;
                list.LoadFromReader(reader);

                return list;
            }
        }
        catch (Exception eX)
        {
            throw new Exception("Error in SampShipment: " + eX.Message);
        }
    }

   

}

