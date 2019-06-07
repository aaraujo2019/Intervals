using RN.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RN
{
    public class Conexion
    {
        public static List<DataConection> getConection()
        {
            string cadena = "";
            string key = "";
            List<DataConection> conections = new List<DataConection>();
            int Line = 0;
            try
            {

                var cn = ConfigurationManager.ConnectionStrings;
                foreach (var item in cn)
                {
                    if (((System.Configuration.ConfigurationElement)item).LockItem)
                    {
                        DataConection conection = new DataConection();
                        key = ((System.Configuration.ConnectionStringSettings)item).Name;
                        if (!key.Contains("LocalSqlServer"))
                        {
                            if (!key.Contains("SqlProvider"))
                            {
                                conection.Description = key;
                                conections.Add(conection);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return conections;
        }

        public static string[] getConectionArray()
        {
            string cadena = "";
            string key = "";
            List<DataConection> conections = new List<DataConection>();

            int Line = 0;
            try
            {
                System.IO.StreamReader File = new System.IO.StreamReader(@"C:\WINDOWS\ConnServerNP.ini");
                while ((cadena = File.ReadLine()) != null)
                {
                    DataConection conection = new DataConection();
                    if (cadena.Contains("Key"))
                    {
                        int index = cadena.IndexOf('=');
                        key = cadena.Substring(index + 1, cadena.Length - index - 1).Trim();
                        conection.Description = key;
                        conections.Add(conection);
                    }
                    Line++;
                }
                File.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return null;
        }


        public static SqlConnection OpenConexion()
        {

            SqlConnection Conn = new SqlConnection();
            bool isConnection = false;

            Conn.ConnectionString = ConfigurationManager.ConnectionStrings["CN_ZandorCapital"].ToString();

            try
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
                else
                    Conn.Open();

                return Conn;

            }
            catch (SqlException Ex)
            {
                // MessageBox.Show("Error de comunicacion:  " + Ex.Errors, "Informe de la aplicacion.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            return Conn;

        }
    }
}
