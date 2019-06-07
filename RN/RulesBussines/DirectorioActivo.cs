using System;
using System.DirectoryServices;
using System.Text;
using System.Windows.Forms;

namespace RN
{
    public static class DirectorioActivo
    {
        public static object ExisteUsuarioAD(string dominio, string usuario)
        {

            DirectoryEntry directoryEntry = new DirectoryEntry(string.Format("LDAP://{0}", dominio));
            DirectorySearcher _dSearch = new DirectorySearcher(directoryEntry, string.Format("(SAMAccountName={0})", usuario));
            var _result = _dSearch.FindOne();
            return _result;
        }

        public static bool Autenticar(string dominio, string usuario, string pwd, string path)
        {
            //Armamos la cadena completa de dominio y usuario
            string domainAndUsername = dominio + @"\" + usuario;
            //Creamos un objeto DirectoryEntry al cual le pasamos el URL, dominio/usuario y la contraseña
            DirectoryEntry entry = new DirectoryEntry(path, domainAndUsername, pwd);
            try
            {
                DirectorySearcher search = new DirectorySearcher(entry);
                //Verificamos que los datos de logeo proporcionados son correctos
                SearchResult result = search.FindOne();
                if (result == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
        }

        public static String GetGroups(string BuscarUsuario, string path)
        {
            // Creamos un objeto DirectoryEntry para conectarnos al directorio activo
            DirectoryEntry adsRoot = new DirectoryEntry(path);

            // Creamos un objeto DirectorySearcher para hacer una búsqueda en el directorio activo
            DirectorySearcher adsSearch = new DirectorySearcher(adsRoot);
            string GrupoRetorno = string.Empty;
            try
            {
                // Ponemos como filtro que busque el usuario actual
                adsSearch.Filter = "samAccountName=" + BuscarUsuario.Trim();
                //adsSearch.Filter = "samAccountName=" + Environment.GetEnvironmentVariable("USERNAME");

                // Extraemos la primera coincidencia
                SearchResult oResult;
                oResult = adsSearch.FindOne();

                // Obtenemos el objeto de ese usuario
                DirectoryEntry usuario = oResult.GetDirectoryEntry();

                // Obtenemos la lista de SID de los grupos a los que pertenece
                usuario.RefreshCache(new string[] { "tokenGroups" });

                // Creamos una variable StringBuilder donde ir añadiendo los SID para crear un filtro de búsqueda
                StringBuilder sids = new StringBuilder();
                sids.Append("(|");
                foreach (byte[] sid in usuario.Properties["tokenGroups"])
                {
                    sids.Append("(objectSid=");
                    for (int indice = 0; indice < sid.Length; indice++)
                    {
                        sids.AppendFormat("\\{0}", sid[indice].ToString("X2"));
                    }
                    sids.AppendFormat(")");
                }
                sids.Append(")");

                // Creamos un objeto DirectorySearcher con el filtro antes generado y buscamos todas la coincidencias
                DirectorySearcher ds = new DirectorySearcher(adsRoot, sids.ToString());
                SearchResultCollection src = ds.FindAll();

                // Recorremos toda la lista de grupos devueltos y seleccionamos a la primera coincidencia con el grupo de Celulares
                int sw = 0;
                int first;
                int last;
               
                foreach (SearchResult sr in src)
                {
                    String sGrupo = (String)sr.Properties["samAccountName"][0];
                    // A partir de aquí hacer lo que corresponda con cada grupo         ...
                    //MessageBox.Show(sGrupo);
                    if (sw == 0)
                    {
                        first = sGrupo.ToLower().IndexOf("presupuesto");                        
                        if (first >= 0)
                        {
                           
                            GrupoRetorno = sGrupo;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return GrupoRetorno;
        }
    }
}
