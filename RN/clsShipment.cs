using Entities.Config;
using RN.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class clsShipment
{
    public int iID;
    public string sCode;
    public string sDescription;
    public bool bStatus;
    public string sObservation;


    private DataAccess.ManagerDA oData = new DataAccess.ManagerDA();

    public DataTable getShipment()
    {
        try
        {
            DataSet dtShipment = new DataSet();
            SqlParameter[] arr = oData.GetParameters(1);
            arr[0].ParameterName = "@Code";
            arr[0].Value = sCode;
            dtShipment = oData.ExecuteDataset("usp_RfTypeShipment_List", arr, CommandType.StoredProcedure);
            return dtShipment.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in Shipment: " + eX.Message);
        }
    }

    public IList<Ent_Prefix> getRfCodeLabListEntity()
    {
        try
        {
            SqlParameter[] arr = oData.GetParameters(0);

            using (IDataReader reader = oData.GetContactsReaderGeneric("usp_RfCodeLab_List", arr, CommandType.StoredProcedure))
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

    public IList<Ent_Prefix> getRfAnalysisCodeLabEntity()
    {
        try
        {
            SqlParameter[] arr = oData.GetParameters(0);

            using (IDataReader reader = oData.GetContactsReaderGeneric("usp_RfAnalysisCodeLab_List", arr, CommandType.StoredProcedure))
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

    public IList<Ent_Prefix> getRfPreparationCodeEntity()
    {
        try
        {
            SqlParameter[] arr = oData.GetParameters(0);

            using (IDataReader reader = oData.GetContactsReaderGeneric("usp_RfPreparationCode_List", arr, CommandType.StoredProcedure))
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

    public IList<Ent_Prefix> getuRfCodeLabEntity()
    {
        try
        {
            SqlParameter[] arr = oData.GetParameters(0);

            using (IDataReader reader = oData.GetContactsReaderGeneric("usp_RfCodeLab_List", arr, CommandType.StoredProcedure))
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


    public IList<Ent_Prefix> getCHSamplesEntity(string sOpcion,string sCHId)
    {
        try
        {
            SqlParameter[] arr = oData.GetParameters(2);
            arr[0].ParameterName = "@Opcion";
            arr[0].Value = sOpcion;
            arr[1].ParameterName = "@ChId";
            arr[1].Value = sCHId;

            using (IDataReader reader = oData.GetContactsReaderGeneric("usp_CH_Samples_List", arr, CommandType.StoredProcedure))
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

    public IList<Ent_Prefix> getLoadDispatchedbyEntity()
    {
        try
        {
            SqlParameter[] arr = oData.GetParameters(0);

            using (IDataReader reader = oData.GetContactsReaderGeneric("usp_RfWorker_List", arr, CommandType.StoredProcedure))
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


    public IList<Ent_Prefix> getShipmentAllEntity()
    {
        try
        {
            SqlParameter[] arr = oData.GetParameters(0);

            using (IDataReader reader = oData.GetContactsReaderGeneric("usp_RfTypeShipment_List_All", arr, CommandType.StoredProcedure))
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


    public DataTable getShipmentAll()
    {
        try
        {
            DataSet dtShipment = new DataSet();
            SqlParameter[] arr = oData.GetParameters(0);

            dtShipment = oData.ExecuteDataset("usp_RfTypeShipment_List_All", arr, CommandType.StoredProcedure);
            return dtShipment.Tables[0];

        }
        catch (Exception eX)
        {
            throw new Exception("Error in Shipment: " + eX.Message);
        }
    }

}

