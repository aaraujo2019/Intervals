using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RN.RulesBussines
{
 public   class Adress_IP
    {
        public static string Local()
        {
            string Direccion = string.Empty;
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    Direccion = ip.ToString();
                }
            }
            return Direccion.Trim();
        }

        public static string Publica()
        {
            string Direccion = String.Empty;
            try
            {
                string webIP = "https://cual-es-mi-ip-publica.com/";
                string sDownCad, scad, scad2;
                WebClient DwnIP = new WebClient();
                sDownCad = DwnIP.DownloadString(webIP);
                char[] sborr1 = { '<', 'h', 't', 'm', 'l', '>', 'e', 'a', 'd', 'i', 'b', 'o', 'd', 'y', 'C', 'u', 'r', 'n', ' ', 'I', 'P', 'c', 'k', '/', 'A', 's', ':' };
                scad = sDownCad.TrimStart(sborr1);
                scad2 = scad.Replace("</body>", "");
                int index = scad2.IndexOf("IP Publica:");
                Direccion = scad2.Substring(index+38,14);
            }
            catch (Exception Error)
            {
                Direccion = "0.0.0.0";
                //System.Windows.Forms.MessageBox.Show(Error.Message + Environment.NewLine + "Se envia 0.0.0.0 Como Direccion IpPublica"); ;
            }

            return  Direccion;


        }

        public static string SerialNumberDisk()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM  Win32_PhysicalMedia");
            string SerialNumberDisk = string.Empty;
            int sw = 0;
            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    if (wmi["SerialNumber"] == null)
                        SerialNumberDisk = "None";
                    else
                    {
                        SerialNumberDisk = wmi["SerialNumber"].ToString().Trim();
                        break;
                    }
                }
                catch (Exception Error)
                {
                    System.Windows.Forms.MessageBox.Show(Error.Message);
                }
            }
            return SerialNumberDisk;
        }

    }
}
