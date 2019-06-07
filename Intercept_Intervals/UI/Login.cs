using Intercept_Intervals.UI;
using RN;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace EnviosColombiaGold
{
    public partial class Login : Form
    {

        Configuration conf = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
        clsRf oRf = new clsRf();
        bool bAct = false;
        private string domain = ConfigurationManager.AppSettings["domain"].ToString();
        private string activeDirectoryUlr = ConfigurationManager.AppSettings["activeDirectoryUlr"].ToString();
        private string conn = ConfigurationManager.ConnectionStrings["GZC"].ToString();
        public Login()
        {
            InitializeComponent();
        }

        
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DirectorioActivo.Autenticar(this.domain, txtUser.Text.ToString(), txtPwd.Text.ToString(), this.activeDirectoryUlr))
                {
                    
                    clsRf.cConnection = this.conn;
                    var value = oRf.getUsersGeneric(txtUser.Text.ToString());
                    if (value.ToList().Count > 0)
                    {
                        if (value.Select(c => c.activo_User).Count() == 0)
                        {
                            MessageBox.Show("Disabled User", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (value.Where(r => r.id_Grupo.Equals(3) && r.activo_User).Count() > 0)
                        {
                            if (value.Select(c => c.login_User.ToString().ToUpper()).FirstOrDefault().ToString().ToUpper() == txtUser.Text.ToString().ToUpper())
                            {
                                clsRf.sUser = txtUser.Text.ToString();
                                clsRf.sIdentification = value.Select(c => c.id_User).FirstOrDefault().ToString();
                                clsRf.sIdGrupo = value.Select(c => c.idGrupo_User).FirstOrDefault().ToString();

                                var intercept = value.Where(s => s.id_Grupo.Equals(22) && s.activo_User);

                                if (intercept.Count() > 0)
                                    clsRf.valueIntercpt = true;

                                var IsAuditor = value.Where(s => s.nombre_Grupo.Equals("Auditor") && s.activo_User);

                                frm_Intercept_Intervals oPpal = new frm_Intercept_Intervals(clsRf.sUser);

                                if (IsAuditor.Count() > 0)
                                    oPpal.IsAuditor = true;

                                oPpal.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("user without privileges", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            if (value.Where(r => r.id_Grupo.Equals(22) && r.activo_User).Count() > 0)
                            {
                                if (value.Where(r => r.id_Grupo.Equals(22)).Select(c => c.login_User.ToString().ToUpper()).FirstOrDefault().ToString().ToUpper() == txtUser.Text.ToString().ToUpper())
                                {
                                    clsRf.valueIntercpt = true;
                                    clsRf.sUser = txtUser.Text.ToString();
                                    frmSplash oSplash = new frmSplash();
                                    oSplash.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("user without privileges", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                if (value.Where(r => r.id_Grupo.Equals(7) && r.activo_User).Count() > 0)
                                {
                                    if (value.Where(r => r.id_Grupo.Equals(7)).Select(c => c.login_User.ToString().ToUpper()).FirstOrDefault().ToString().ToUpper() == txtUser.Text.ToString().ToUpper())
                                    {
                                        clsRf.valueTopografish = true;
                                        clsRf.sUser = txtUser.Text.ToString();
                                        frmSplash oSplash = new frmSplash();
                                        oSplash.Show();
                                        this.Hide();
                                    }
                                }

                                else
                                    MessageBox.Show("user without privileges in group", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }


                }
                else
                {
                    MessageBox.Show("User not active in Active Directory", "Shipment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public static string GetSHA1(String texto)
        {
            try
            {
                SHA1 sha1 = SHA1CryptoServiceProvider.Create();
                Byte[] textOriginal = ASCIIEncoding.Default.GetBytes(texto);
                Byte[] hash = sha1.ComputeHash(textOriginal);
                StringBuilder cadena = new StringBuilder();
                foreach (byte i in hash)
                {
                    cadena.AppendFormat("{0:x2}", i);
                }
                return cadena.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (bAct == true)
            {
                Process.Start("Actualizar.bat");
            }
        }


        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPwd.Focus();
            }
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnAceptar_Click(null, null);
            }
        }
    }
}
