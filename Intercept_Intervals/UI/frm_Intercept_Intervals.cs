using Entities.Person;
using Intervals.Common;
using RN.RulesBussines;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Intercept_Intervals.UI
{/// <summary>
 /// Formulario que permite la carga de pozos e identificar sus intervalos 
 /// </summary>
 /// 


    public partial class frm_Intercept_Intervals : Form
    {
        DataTable dtCollars;
        private string excel_template_name = ConfigurationManager.AppSettings["excel_template_name"].ToString();

        private bool isAuditor = false;
        private bool isSaved = false;
        private bool islocked = false;


        #region constructor
        public frm_Intercept_Intervals()
        {
            InitializeComponent();
            //FillCmb();
            fillComboBoxVhmod();
        }
        public frm_Intercept_Intervals(string User)
        {
            InitializeComponent();
            FillCmb();
            fillComboBoxVhmod();

            this.Usuario = User.Trim();
            GlobalValue.Permissions = LoadLog.GetPermissiinRol("SPGet_RolesForUser", this.Usuario, this.Name);

            this.Permission = GlobalValue.Permissions;
            ValidatePermission(this.Controls);
        }

        #endregion constructor

        #region Propiedades - Variables
        DataTable generalData = new DataTable();
        public bool valueModificate = false;
        public bool valueFirstRecuperation = false;
        public bool containeRegister = false;

        public List<Rol_Permission> Permission;

        public string Usuario { get; set; }
        public string IpLocal { get; set; }
        public string IpPublica { get; set; }
        public string SerialHDD { get; set; }

        public bool IsAuditor
        {
            set
            {
                isAuditor = value;
            }
        }



        #endregion

        #region entidades de calculo
        public class matriz1
        {
            public decimal value { get; set; }
        }
        public class matriz2
        {
            public decimal value { get; set; }
        }
        public class matriz3
        {
            public decimal value { get; set; }
        }

        public class matrizResult
        {
            public decimal value { get; set; }
        }

        public class matrizTo
        {
            public string value { get; set; }
        }

        public class matrizMod
        {
            public string value { get; set; }
        }
        #endregion 

        private void frm_Intercept_Intervals_Load(object sender, EventArgs e)
        {
            Clear();

            this.cbxHole.SelectedIndexChanged += new System.EventHandler(this.cmbHoleID_SelectedIndexChanged);
            dtgDetailHoleID.CellValueChanged +=
             new DataGridViewCellEventHandler(dtgDetailHoleID_CellValueChanged);
            dtgDetailHoleID.CurrentCellDirtyStateChanged +=
                 new EventHandler(dtgDetailHoleID_CurrentCellDirtyStateChanged);


            btnLock.Visible = isAuditor;
        }

        private void dtgDetailHoleID_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dtgDetailHoleID.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                dtgDetailHoleID.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dtgDetailHoleID_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToBoolean(dtgDetailHoleID.Rows[e.RowIndex].Cells[0].Value))
            {
                dtgDetailHoleID.Rows[e.RowIndex].Cells[0].Value = 0;
                fillgridCalculated();
            }

        }

        #region Metodos

        private void ValidatePermission(Control.ControlCollection controlCollection)
        {
            foreach (Control c in controlCollection)
            {
                if (c.Controls.Count > 0)
                {
                    ValidatePermission(c.Controls);
                }
                if (c is MenuStrip)
                {
                    MenuStrip menuStrip = c as MenuStrip;
                    ShowToolStipItems(menuStrip.Items);
                }

                if (c is Button || c is ComboBox || c is TextBox ||
                    c is ListBox || c is DataGridView || c is RadioButton ||
                    c is RichTextBox || c is TabPage || c is TextBox || c is GroupBox)
                {

                    Rol_Permission valueFilter = Permission.Where(e => e.fkcontrolid == c.Name).FirstOrDefault();

                    if (valueFilter != null)
                    {
                        if (valueFilter.Invisible > 0)
                            c.Visible = false;
                        else
                            c.Visible = true;

                        if (valueFilter.Disabled > 0)
                            c.Enabled = false;
                        else
                            c.Enabled = true;
                    }
                }
            }
        }

        private void ShowToolStipItems(ToolStripItemCollection toolStripItems)
        {
            foreach (ToolStripMenuItem mi in toolStripItems)
            {
                mi.ToolTipText = mi.Name;

                if (mi.DropDownItems.Count > 0)
                {
                    ShowToolStipItems(mi.DropDownItems);
                }

                Rol_Permission valueFilter = Permission.Where(e => e.fkcontrolid == mi.Name).FirstOrDefault();

                if (valueFilter != null)
                {
                    if (valueFilter.Invisible > 0)
                        mi.Visible = false;
                    else
                        mi.Visible = true;

                    if (valueFilter.Disabled > 0)
                        mi.Enabled = false;
                    else
                        mi.Enabled = true;
                }
            }
        }


        /// <summary>
        /// Limpia todos los objetos involucrados
        /// </summary>
        private void Clear()
        {
            cbxHole.SelectedIndex = 0;
            dtgValueCalculate.Rows.Clear();
            dtgDetailHoleID.DataSource = getEmptyRow();
            txtComent.Text = string.Empty;
            btnExport.Enabled = false;
        }

        /// <summary>
        /// Exporta objeto dataGrid a excel
        /// </summary>
        /// <param name="HoldId">Código del pozo</param>

        #endregion

        #region Eventos
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Clear();
        }


        private void setExplorationgridFormat()
        {
            for (int i = 0; i < dtgDetailHoleID.Rows.Count; i++)
            {
                object auppm = dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Value;
                decimal decimalauppm = -1;
                bool flagAuppm = auppm != null ? Decimal.TryParse(auppm.ToString(), out decimalauppm) : false;
                dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["au_ppm"].Index].Style.BackColor = flagAuppm ? setFormatColumnAu_ppm(decimalauppm) : Color.Empty;

                object agppm = dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Value;
                decimal decimalagppm = -1;
                bool flagAgppm = agppm != null ? Decimal.TryParse(agppm.ToString(), out decimalagppm) : false;
                dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["ag_ppm"].Index].Style.BackColor = flagAgppm ? setFormatColumnAg_ppm(decimalagppm) : Color.Empty;

                object lithology = dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["lithology"].Index].Value;
                dtgDetailHoleID.Rows[i].Cells[dtgDetailHoleID.Columns["lithology"].Index].Style.BackColor = lithology != null ? setFormatColumnlithology(lithology.ToString().Trim()) : Color.Empty;
            }

        }

        private Color setFormatColumnAu_ppm(Decimal data)
        {
            if (data > 0 && data < 1.999M)
                return System.Drawing.ColorTranslator.FromHtml("#D9D9D9");

            if (data >= 2M && data < 2.999M)
                return ColorTranslator.FromHtml("#00FFFF");

            if (data >= 3M && data < 3.999M)
                return ColorTranslator.FromHtml("#00FF00");

            if (data >= 4M && data <= 7.999M)
                return ColorTranslator.FromHtml("#FFFF00");

            if (data >= 8M && data <= 11.999M)
                return ColorTranslator.FromHtml("#FFC000");

            if (data >= 12M && data <= 19.999M)
                return ColorTranslator.FromHtml("#FF0000");

            return ColorTranslator.FromHtml("#FF00FF");

        }

        private Color setFormatColumnAg_ppm(Decimal data)
        {

            if (data > 0 && data < 3.999M)
                ColorTranslator.FromHtml("#D9D9D9");

            if (data >= 4M && data < 5.999M)
                return ColorTranslator.FromHtml("#00FFFF");

            if (data >= 6M && data < 7.999M)
                return ColorTranslator.FromHtml("#00FF00");

            if (data >= 8M && data <= 15.999M)
                return ColorTranslator.FromHtml("#FFFF00");

            if (data >= 16M && data <= 23.999M)
                return ColorTranslator.FromHtml("#FFC000");

            if (data >= 24M && data <= 39.999M)
                return ColorTranslator.FromHtml("#FF0000");

            return ColorTranslator.FromHtml("#FF00FF");
        }

        private Color setFormatColumnlithology(String data)
        {
            Color cellcolor;
            switch (data)
            {
                case "VBX":
                case "VEN":
                case "VNA":
                    cellcolor = Color.Red;
                    break;
                case "FLT":
                case "FLG":
                case "BHF":
                case "BXF":
                    cellcolor = Color.RoyalBlue;
                    break;
                case "HA":
                    cellcolor = Color.Green;
                    break;

                default:
                    cellcolor = Color.Empty;
                    break;
            }

            return cellcolor;
        }

        private DataTable getEmptyRow()
        {
            DataTable emptytb = new DataTable();

            String[] headers = "Selection,Vn_mod,from,to,lenght,sampno,lithology,au_ppm,ag_ppm,dhid,jobno,lab,aufaa_ppm,aufagr_gpt,agfa_ppm".Split(',');

            foreach (string header in headers)
            {
                DataColumn dgtxtCol = new DataColumn() { ColumnName = header };

                emptytb.Columns.Add(dgtxtCol);
            }

            return emptytb;
        }

        private void cmbHoleID_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSavedItems.Visible = false;
            btnExport.Enabled = false;

            ComboBox cbx = (ComboBox)sender;
            if (cbx.SelectedIndex != 0)
            {
                dtgValueCalculate.Rows.Clear();
                dtCollars = RN.GetDHCollars.getDHHoleID(cbx.SelectedItem.ToString());
                var culturaCol = CultureInfo.GetCultureInfo("es-CO");
                DateTime dateReporte = Convert.ToDateTime(DateTime.Now, culturaCol);
                LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, "Return Hole Id for Code: " + cbxHole.Text, "View information");
                dtgDetailHoleID.DataSource = dtCollars;
                dtgDetailHoleID.AutoResizeColumns();
                dtgDetailHoleID.Columns[dtgDetailHoleID.Columns["SKDHSamples"].Index].Visible = false;
                dtgDetailHoleID.Columns[dtgDetailHoleID.Columns["FromV"].Index].Visible = false;
                dtgDetailHoleID.Columns[dtgDetailHoleID.Columns["ToV"].Index].Visible = false;
                dtgDetailHoleID.Columns[dtgDetailHoleID.Columns["Length_Grade"].Index].Visible = false;
                dtgDetailHoleID.Columns[dtgDetailHoleID.Columns["Au_Grade"].Index].Visible = false;
                dtgDetailHoleID.Columns[dtgDetailHoleID.Columns["Ag_Grade"].Index].Visible = false;
                dtgDetailHoleID.Columns[dtgDetailHoleID.Columns["TotalRegister"].Index].Visible = false;
                dtgDetailHoleID.Columns[dtgDetailHoleID.Columns["IsSaved"].Index].Visible = false;
                dtgDetailHoleID.Columns[dtgDetailHoleID.Columns["IsLocked"].Index].Visible = false;
                if (dtCollars.Rows.Count > 0)
                {
                    isSaved = (dtCollars.Select("IsSaved = 1").Count() > 0) ? true : false;
                    islocked = (dtCollars.Select("IsLocked = 1").Count() > 0) ? true : false;

                    dtgDetailHoleID.Enabled = true;
                    setColumsReadOnly();
                    setExplorationgridFormat();
                    if (isSaved)
                    {
                        loadgridCalculated();
                        btnExport.Enabled = true;
                    }
                    else if (dtCollars.Select("Selection = 1").Count() > 0)
                    {
                        fillgridCalculated();
                        lblSavedItems.Visible = true;
                    }
                    else
                    {
                        dtgValueCalculate.Rows.Add(dtCollars.Rows.Count);
                    }
                }

                dtgDetailHoleID.Focus();
            }
        }

        private void setColumsReadOnly()
        {
            for (int i = 2; i < dtgDetailHoleID.Columns.Count; i++)
            {
                dtgDetailHoleID.Columns[i].ReadOnly = true;
            }
        }

        private void loadgridCalculated()
        {
            int recordbyvnmod = 0;
            for (int i = 0; i < dtCollars.Rows.Count; i++)
            {
                if (Convert.ToInt16(dtCollars.Rows[i][0]) == 1)
                {
                    dtgValueCalculate.Rows.Insert(i, dtCollars.Rows[i][10], dtCollars.Rows[i][9], dtCollars.Rows[i][15], dtCollars.Rows[i][16], dtCollars.Rows[i][17], dtCollars.Rows[i][18], dtCollars.Rows[i][19], dtCollars.Rows[i][20], dtCollars.Rows[i][1], dtCollars.Rows[i][21]);

                    if (!string.IsNullOrEmpty(dtCollars.Rows[i][20].ToString()))
                        recordbyvnmod = Convert.ToInt32(dtCollars.Rows[i][20]) - 1;
                    if (recordbyvnmod > 0)
                    {
                        dtgValueCalculate.Rows.Add(recordbyvnmod);
                        i = i + recordbyvnmod;
                    }
                }
                else
                {
                    dtgValueCalculate.Rows.Add(1);
                }
            }

            if (dtCollars.Rows.Count > 0)
                btnExport.Enabled = true;
        }
        private void fillgridCalculated()
        {
            dtgValueCalculate.Rows.Clear();
            DataRow acumulative_row = dtCollars.NewRow();
            int counter = 0, rowcounter = dtCollars.Rows.Count, idx = -1;
            string vnmod = string.Empty;
            foreach (DataRow row in dtCollars.Rows)
            {
                idx++;
                dtgValueCalculate.Rows.Add(1);
                //Selected == 1
                if (Convert.ToInt16(row.ItemArray[0]) == 1)
                {
                    if (counter == 0)
                    {
                        initDatarow(acumulative_row, row, idx);
                        counter++;
                    }
                    else
                    {
                        if (acumulative_row[1].ToString() == row[1].ToString())
                        {
                            decimal au = 0, ag = 0, len;
                            counter++;
                            acumulative_row[3] = row["to"];
                            acumulative_row[4] = Convert.ToDecimal(row["to"]) - Convert.ToDecimal(acumulative_row[2]);
                            acumulative_row[5] = row["au_ppm"];
                            acumulative_row[6] = row["ag_ppm"];
                            acumulative_row[7] = Convert.ToDecimal(acumulative_row[7]) + ((Decimal.TryParse(row["au_ppm"].ToString(), out au) && Decimal.TryParse(row["lenght"].ToString(), out len)) ? au * len : 0);
                            acumulative_row[8] = Convert.ToDecimal(acumulative_row[8]) + ((Decimal.TryParse(row["ag_ppm"].ToString(), out au) && Decimal.TryParse(row["lenght"].ToString(), out len)) ? ag * len : 0);

                            acumulative_row[12] = counter;

                        }
                        else
                        {
                            dtgValueCalculate.Rows.Insert(Convert.ToInt32(acumulative_row[13]), acumulative_row[10], acumulative_row[9], acumulative_row[2], acumulative_row[3], acumulative_row[4], (Convert.ToDecimal(acumulative_row[7]) / Convert.ToDecimal(acumulative_row[4])).ToString("##,0.000"), (Convert.ToDecimal(acumulative_row[8]) / Convert.ToDecimal(acumulative_row[4])).ToString("##,0.000"), acumulative_row[12], acumulative_row[1], acumulative_row[21]);
                            dtgValueCalculate.Rows.RemoveAt(dtgValueCalculate.Rows.Count - 1);

                            initDatarow(acumulative_row, row, idx);
                            counter = 1;
                        }

                    }

                    if (idx == (rowcounter - 1))
                    {
                        dtgValueCalculate.Rows.Insert(Convert.ToInt32(acumulative_row[13]), acumulative_row[10], acumulative_row[9], acumulative_row[2], acumulative_row[3], acumulative_row[4], (Convert.ToDecimal(acumulative_row[7]) / Convert.ToDecimal(acumulative_row[4])).ToString("##,0.000"), (Convert.ToDecimal(acumulative_row[8]) / Convert.ToDecimal(acumulative_row[4])).ToString("##,0.000"), acumulative_row[12], acumulative_row[1], acumulative_row[21]);
                        dtgValueCalculate.Rows.RemoveAt(dtgValueCalculate.Rows.Count - 1);
                    }
                }
                else
                {
                    //add empty row
                    if (counter > 0)
                    {
                        dtgValueCalculate.Rows.Insert(Convert.ToInt32(acumulative_row[13]), acumulative_row[10], acumulative_row[9], acumulative_row[2], acumulative_row[3], acumulative_row[4], (Convert.ToDecimal(acumulative_row[7]) / Convert.ToDecimal(acumulative_row[4])).ToString("##,0.000"), (Convert.ToDecimal(acumulative_row[8]) / Convert.ToDecimal(acumulative_row[4])).ToString("##,0.000"), acumulative_row[12], acumulative_row[1], acumulative_row[21]);
                        dtgValueCalculate.Rows.RemoveAt(dtgValueCalculate.Rows.Count - 1);
                    }



                    counter = 0;

                }

            }

            dtgValueCalculate.AutoResizeColumns();
            if (dtCollars.Rows.Count > 0)
                btnExport.Enabled = true;

        }

        private void initDatarow(DataRow newDatarow, DataRow dr, int idx)
        {
            decimal au = 0, ag = 0, len;
            newDatarow[1] = dr[1];
            newDatarow[2] = dr[2];
            newDatarow[3] = dr[3];
            newDatarow[4] = dr[4];
            newDatarow[5] = dr[7];
            newDatarow[6] = dr[8];
            newDatarow[7] = Decimal.TryParse(dr[7].ToString(), out au) && Decimal.TryParse(dr[4].ToString(), out len) ? au * len : 0;
            newDatarow[8] = Decimal.TryParse(dr[8].ToString(), out ag) && Decimal.TryParse(dr[4].ToString(), out len) ? ag * len : 0;
            newDatarow[9] = dr[9];
            newDatarow[10] = dr[10];
            newDatarow[11] = dr[11];
            newDatarow[12] = 1;
            newDatarow[13] = idx;
            newDatarow[14] = string.Empty;
            newDatarow[21] = dr[21];

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (islocked)
            {
                MessageBox.Show("The Hole was locked for changes.");
                return;
            }


            if (e.ColumnIndex == 0)
            {
                if (string.IsNullOrEmpty(dtgDetailHoleID.Rows[e.RowIndex].Cells[1].Value.ToString()))
                {
                    MessageBox.Show("You must select a Vn_mod value.");
                    return;
                }

                //el valor anterior es diferente de vacio y es diferente al que esta seleccionado
                if (e.RowIndex > 0 && !string.IsNullOrEmpty(dtgDetailHoleID.Rows[e.RowIndex - 1].Cells[1].Value.ToString()))
                {
                    if (dtgDetailHoleID.Rows[e.RowIndex].Cells[1].Value.ToString() != dtgDetailHoleID.Rows[e.RowIndex - 1].Cells[1].Value.ToString())
                    {
                        MessageBox.Show("The current Vn_mod does't match with the previos Vn_mod.");
                        dtgDetailHoleID.Rows[e.RowIndex].Cells[1].Value = string.Empty;
                        return;
                    }

                }

                dtCollars.Rows[e.RowIndex][e.ColumnIndex] = !Convert.ToBoolean(dtCollars.Rows[e.RowIndex][e.ColumnIndex]);
                if (!Convert.ToBoolean(dtCollars.Rows[e.RowIndex][e.ColumnIndex]))
                    dtgDetailHoleID.Rows[e.RowIndex].Cells[1].Value = String.Empty;

                dtgValueCalculate.Rows.Clear();
                fillgridCalculated();
            }


        }

        private void cmbHoleID_SelectedIndexChanged_old(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cbxHole.Text))
            {
                DataTable dtCollars = RN.GetDHCollars.getDHHoleID(cbxHole.Text);

                dtgDetailHoleID.DataSource = dtCollars;
                dtgDetailHoleID.AutoResizeColumns();
                dtgDetailHoleID.Columns[dtgDetailHoleID.Columns["SKDHSamples"].Index].Visible = false;
                containeRegister = false;



                var culturaCol = CultureInfo.GetCultureInfo("es-CO");
                DateTime dateReporte = Convert.ToDateTime(DateTime.Now, culturaCol);

                LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, "Return Hole Id for Code: " + cbxHole.Text, "View information");

                setExplorationgridFormat();

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dtgValueCalculate.RowCount > 1)
            {
                string contextValue = string.Empty;
                try
                {
                    foreach (DataGridViewRow row in dtgDetailHoleID.Rows)
                    {
                        if (row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value) && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                        {
                            if (row.Cells[1].Value == null)
                            {
                                MessageBox.Show("Select all value Column Vn_mod this select");
                                return;
                            }
                        }
                    }
                    var culturaCol = CultureInfo.GetCultureInfo("es-CO");
                    DateTime dateReporte = Convert.ToDateTime(DateTime.Now, culturaCol);

                    foreach (DataGridViewRow row in dtgDetailHoleID.Rows)
                    {
                        if (row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value) && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                        {
                            contextValue = "UPDATE [dbo].[DH_Samples]   SET      Vn_mod = '" + row.Cells[1].Value.ToString() + "' WHERE SKDHSamples =" + Convert.ToInt32(row.Cells[dtgDetailHoleID.Columns["SKDHSamples"].Index].Value) + " and HoleID ='" + row.Cells[dtgDetailHoleID.Columns["dhid"].Index].Value.ToString() + "'";
                            LoadLog.alterdataBase(contextValue);

                            LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, contextValue, "update");
                        }
                        else
                        {
                            if (row.Cells[dtgDetailHoleID.Columns["Vn_mod_codes"].Index].Value != null && !String.IsNullOrEmpty(row.Cells[dtgDetailHoleID.Columns["Vn_mod_codes"].Index].Value.ToString()))
                            {
                                contextValue = "UPDATE [dbo].[DH_Samples]   SET      Vn_mod = Null WHERE SKDHSamples =" + Convert.ToInt32(row.Cells[dtgDetailHoleID.Columns["SKDHSamples"].Index].Value) + " and HoleID ='" + row.Cells[dtgDetailHoleID.Columns["dhid"].Index].Value.ToString() + "'";
                                LoadLog.alterdataBase(contextValue);
                                LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, contextValue, "update");

                            }
                        }
                    }


                    foreach (DataGridViewRow row in dtgValueCalculate.Rows)
                    {
                        if (row.Cells[1].Value != null && !String.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                        {
                            contextValue = String.Format("SELECT Count(1) FROM DH_IntercepInterval  WHERE HoleID = @HoleID and SKDHSamples= @SKDHSamples");
                            object count = LoadLog.Exist_DB(contextValue, row.Cells[1].Value.ToString(), Convert.ToDecimal(row.Cells[9].Value));
                            LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, contextValue, "Search");

                            if (Convert.ToInt32(count) > 0)
                            {
                                string valueDescrioption = string.Empty;
                                if (!string.IsNullOrEmpty(txtComent.Text.Trim()))
                                {
                                    //valueDescrioption = string.Concat(txtComent.Text, " Con intervale inicial de ", Convert.ToDecimal(row.Cells[2].Value), " hasta ", valueTo);
                                    contextValue = "update dbo.DH_IntercepInterval set Tov=" + Convert.ToDecimal(row.Cells[3].Value) + ",Length_Grade=" + Convert.ToDecimal(row.Cells[4].Value) + ", Au_Grade=" + Convert.ToDecimal(row.Cells[5].Value) + ",Ag_Grade= " + Convert.ToDecimal(row.Cells[6].Value) + ",Comments='" + valueDescrioption + "',TotalRegister=" + Convert.ToInt32(row.Cells[7].Value) + " , Date_Event ='" + dateReporte.ToString() + "' where HoleID ='" + row.Cells[1].Value.ToString() + "' AND SKDHSamples =" + Convert.ToDecimal(row.Cells[9].Value);
                                    LoadLog.alterdataBase(contextValue);
                                    LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, contextValue, "Update");

                                }
                                else
                                {
                                    MessageBox.Show("Comment is required for the rank update");
                                    return;
                                }
                            }
                            else
                            {
                                contextValue = "INSERT INTO dbo.DH_IntercepInterval(HoleID,Fromv,Tov,Length_Grade,Au_Grade,Ag_Grade,Comments,TotalRegister,Vn_mod,SKDHSamples,Date_Event)VALUES(" + "'" + row.Cells[1].Value.ToString() + "'," + Convert.ToDecimal(row.Cells[2].Value) + " ," + Convert.ToDecimal(row.Cells[3].Value) + "," + Convert.ToDecimal(row.Cells[4].Value) + " ," + Convert.ToDecimal(row.Cells[5].Value) + "," + Convert.ToDecimal(row.Cells[6].Value) + ",'" + txtComent.Text + "'," + Convert.ToInt32(row.Cells[7].Value) + ",'" + row.Cells[8].Value.ToString() + "'," + row.Cells[9].Value.ToString() + ",'" + dateReporte + "')";
                                LoadLog.alterdataBase(contextValue);
                                LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, contextValue, "Update");

                            }
                        }
                    }

                    MessageBox.Show("The Calculated items were save successful.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtComent.Text = String.Empty;
                    lblSavedItems.Visible = false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("There is not data to save.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            ExportProcess();
            btnExport.Enabled = true;
        }

        private void ExportProcess()
        {
            try
            {
                exportForm popup = new exportForm();
                popup.StartPosition = FormStartPosition.CenterParent;
                DialogResult dialogResult = popup.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    switch (popup.ExportType)
                    {
                        case "HOLE":
                            exportByHoleId();
                            break;
                        case "SOURCE":
                            //exportBySource(popup.Source);
                            exportByTarget(popup.Target, popup.Source);
                            break;
                        case "TARGET":
                            exportByTarget(popup.Target, popup.Source);
                            break;
                        default:
                            break;
                    }
                    return;
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }

                //exportIntercept(cbxSource.Text);
                var culturaCol = CultureInfo.GetCultureInfo("es-CO");
                DateTime dateReporte = Convert.ToDateTime(DateTime.Now, culturaCol);

                LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, "Generate Report", "Report");
                Clear();
            }
            catch (Exception ex)
            { throw ex; }
        }

        /* Modificado Alvaro Araujo 06/06/2019*/
        private void exportByTarget(string target, string sources)
        {
            DataTable dtCollarsByTarget = RN.GetDHCollars.getRecordsByTarget(target, sources);
            if (dtCollarsByTarget.Rows.Count > 0)
                exportDataTableBySourceOrTarget(dtCollarsByTarget, "Exploration Target: " + target + " and sources : " + sources.Trim('\''));
            else
            {
                MessageBox.Show("There are no items to export for  target : " + target);
            }
        }

        private void exportBySource(string source)
        {
            DataTable dtCollarsBySource = RN.GetDHCollars.getRecordsBySource(source);
            if (dtCollarsBySource.Rows.Count > 0)
                exportDataTableBySourceOrTarget(dtCollarsBySource, "Exploration Source: " + source);
            else
            {
                MessageBox.Show("There are no items to export for Source : " + source);
            }
        }

        private void exportByHoleId()
        {
            if (cbxHole.SelectedIndex != 0)
            {
                if (dtCollars.Rows.Count > 0)
                    exportDataTableByHoleId(dtCollars, "Exploration Hold Id: " + cbxHole.SelectedItem.ToString());
                else
                {
                    MessageBox.Show("There are no items to export for Hole id : " + cbxHole.SelectedItem.ToString());
                }
            }
            else
            {
                MessageBox.Show("You must select a Hold Id to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void exportDataTableBySourceOrTarget(DataTable dt, string description)
        {
            try
            {
                //QCReport
                Excel.Application oXL;
                Excel._Workbook oWB;
                Excel._Worksheet oSheet;

                ProgressBar progress = new ProgressBar();
                Thread backgroundThread = new Thread(
                                                 new ThreadStart(() =>
                                                 {
                                                     oXL = new Excel.Application();
                                                     string pathArchive = string.Concat(Application.StartupPath, "\\", excel_template_name);
                                                     oWB = oXL.Workbooks.Open(pathArchive, 0, true, 5,
                                                     Type.Missing, Type.Missing, false, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, null, null);

                                                     oSheet = (Excel._Worksheet)oWB.Sheets[1];

                                                     oSheet.Cells[4, 7] = description;

                                                     int initialRowCellidx = 9, idx = 0;

                                                     if (dt.Rows.Count > 0)
                                                     {
                                                         foreach (DataRow row in dt.Rows)
                                                         {
                                                             if (Convert.ToBoolean(row[0]))
                                                             {
                                                                 oSheet.Cells[initialRowCellidx, 1] = "*";

                                                                 var cellsRange1To9 = oSheet.Range[
                                                                                oSheet.Cells[initialRowCellidx, 1],
                                                                                oSheet.Cells[initialRowCellidx, 9]];


                                                                 var cells11 = oSheet.Range[
                                                                               oSheet.Cells[initialRowCellidx, 11],
                                                                               oSheet.Cells[initialRowCellidx, 11]];

                                                                 cellsRange1To9.Interior.Color = Color.LightBlue;
                                                                 cells11.Interior.Color = Color.LightBlue;
                                                             }

                                                             oSheet.Cells[initialRowCellidx, 2] = row[10].ToString();
                                                             oSheet.Cells[initialRowCellidx, 3] = row[9].ToString();
                                                             oSheet.Cells[initialRowCellidx, 4] = row[2].ToString();
                                                             oSheet.Cells[initialRowCellidx, 5] = row[3].ToString();
                                                             oSheet.Cells[initialRowCellidx, 6] = row[4].ToString();
                                                             oSheet.Cells[initialRowCellidx, 7] = row[5].ToString();
                                                             oSheet.Cells[initialRowCellidx, 8] = row[6].ToString();
                                                             oSheet.Cells[initialRowCellidx, 9] = (row[1] != null) ? row[1].ToString() : "";

                                                             //var cell10 = oSheet.Range[
                                                             //                         oSheet.Cells[initialRowCellidx, 10],
                                                             //                         oSheet.Cells[initialRowCellidx, 10]];

                                                             oSheet.Cells[initialRowCellidx, 10] = row[7].ToString();
                                                             oSheet.Cells[initialRowCellidx, 11] = row[8].ToString();

                                                             oSheet.Cells[initialRowCellidx, 12] = (row[15] != null) ? row[15].ToString() : "";
                                                             oSheet.Cells[initialRowCellidx, 13] = (row[16] != null) ? row[16].ToString() : "";
                                                             oSheet.Cells[initialRowCellidx, 14] = (row[17] != null) ? row[17].ToString() : "";
                                                             oSheet.Cells[initialRowCellidx, 15] = (row[18] != null) ? row[18].ToString() : "";
                                                             oSheet.Cells[initialRowCellidx, 16] = (row[19] != null) ? row[19].ToString() : "";

                                                             idx++;
                                                             initialRowCellidx++;
                                                         }
                                                     }

                                                     oXL.Visible = true;
                                                     oXL.UserControl = true;
                                                     progress.BeginInvoke(new Action(() => progress.Close()));
                                                 }));
                backgroundThread.Start();
                progress.ShowDialog();
            }
            catch (Exception ex)
            {
                if (ex.Message == "cannot be find table 0.")
                {
                    MessageBox.Show("There aren't data to show.");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void exportDataTableByHoleId(DataTable dt, string description)
        {
            ProgressBar progress = new ProgressBar();

            try
            {
                //QCReport
                Excel.Application oXL;
                Excel._Workbook oWB;
                Excel._Worksheet oSheet;

                Thread backgroundThread = new Thread(
                                                 new ThreadStart(() =>
                                                 {
                                                     oXL = new Excel.Application();
                                                     string pathArchive = string.Concat(Application.StartupPath, "\\", excel_template_name);
                                                     oWB = oXL.Workbooks.Open(pathArchive, 0, true, 5,
                                                     Type.Missing, Type.Missing, false, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, null, null);

                                                     oSheet = (Excel._Worksheet)oWB.Sheets[1];
                                                     oSheet.Cells[4, 7] = description;
                                                     int initialRowCellidx = 9, idx = 0;
                                                     if (dt.Rows.Count > 0)
                                                     {
                                                         foreach (DataRow row in dt.Rows)
                                                         {
                                                             if (Convert.ToBoolean(row[0]))
                                                             {
                                                                 oSheet.Cells[initialRowCellidx, 1] = "*";

                                                                 var cellsRange1To9 = oSheet.Range[
                                                                                oSheet.Cells[initialRowCellidx, 1],
                                                                                oSheet.Cells[initialRowCellidx, 9]];


                                                                 var cells11 = oSheet.Range[
                                                                               oSheet.Cells[initialRowCellidx, 11],
                                                                               oSheet.Cells[initialRowCellidx, 11]];

                                                                 cellsRange1To9.Interior.Color = Color.LightBlue;
                                                                 cells11.Interior.Color = Color.LightBlue;
                                                             }

                                                             oSheet.Cells[initialRowCellidx, 2] = row[10].ToString();
                                                             oSheet.Cells[initialRowCellidx, 3] = row[9].ToString();
                                                             oSheet.Cells[initialRowCellidx, 4] = row[2].ToString();
                                                             oSheet.Cells[initialRowCellidx, 5] = row[3].ToString();
                                                             oSheet.Cells[initialRowCellidx, 6] = row[4].ToString();
                                                             oSheet.Cells[initialRowCellidx, 7] = row[5].ToString();
                                                             oSheet.Cells[initialRowCellidx, 8] = row[6].ToString();
                                                             oSheet.Cells[initialRowCellidx, 9] = (row[1] != null) ? row[1].ToString() : "";

                                                             //var cell10 = oSheet.Range[
                                                             //                         oSheet.Cells[initialRowCellidx, 10],
                                                             //                         oSheet.Cells[initialRowCellidx, 10]];

                                                             oSheet.Cells[initialRowCellidx, 10] = row[7].ToString();
                                                             oSheet.Cells[initialRowCellidx, 11] = row[8].ToString();

                                                             oSheet.Cells[initialRowCellidx, 12] = (dtgValueCalculate.Rows[idx].Cells[2].Value != null) ? dtgValueCalculate.Rows[idx].Cells[2].Value.ToString() : "";
                                                             oSheet.Cells[initialRowCellidx, 13] = (dtgValueCalculate.Rows[idx].Cells[3].Value != null) ? dtgValueCalculate.Rows[idx].Cells[3].Value.ToString() : "";
                                                             oSheet.Cells[initialRowCellidx, 14] = (dtgValueCalculate.Rows[idx].Cells[4].Value != null) ? dtgValueCalculate.Rows[idx].Cells[4].Value.ToString() : "";
                                                             oSheet.Cells[initialRowCellidx, 15] = (dtgValueCalculate.Rows[idx].Cells[5].Value != null) ? dtgValueCalculate.Rows[idx].Cells[5].Value.ToString() : "";
                                                             oSheet.Cells[initialRowCellidx, 16] = (dtgValueCalculate.Rows[idx].Cells[6].Value != null) ? dtgValueCalculate.Rows[idx].Cells[6].Value.ToString() : "";

                                                             idx++;
                                                             initialRowCellidx++;
                                                         }
                                                     }

                                                     oXL.Visible = true;
                                                     oXL.UserControl = true;

                                                     progress.BeginInvoke(new Action(() => progress.Close()));
                                                 }));
                backgroundThread.Start();
                progress.ShowDialog();
            }
            catch (Exception ex)
            {
                progress.Close();
                if (ex.Message == "No se puede encontrar la tabla 0.")
                {
                    MessageBox.Show("There aren't data to show.");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        #region fill ComboBoxs
        private void fillComboBoxVhmod()
        {
            clsRfVeins rfveinsCodes = new clsRfVeins();
            DataTable dtrfveinsCodes = rfveinsCodes.getRfVeinsCodes();

            DataGridViewComboBoxColumn cbBoxColumn = dtgDetailHoleID.Columns[1] as DataGridViewComboBoxColumn;

            cbBoxColumn.DataSource = new BindingSource(dtrfveinsCodes, null);

            cbBoxColumn.DisplayMember = "Code";
        }

        private void FillCmb()
        {
            try
            {
                if (string.IsNullOrEmpty(this.IpLocal))
                    this.IpLocal = Adress_IP.Local();

                if (string.IsNullOrEmpty(this.IpPublica))
                    this.IpPublica = Adress_IP.Publica();

                if (string.IsNullOrEmpty(this.SerialHDD))
                    this.SerialHDD = Adress_IP.SerialNumberDisk();

                if (string.IsNullOrEmpty(this.Usuario))
                    this.Usuario = Adress_IP.SerialNumberDisk();
                var culturaCol = CultureInfo.GetCultureInfo("es-CO");
                DateTime dateReporte = Convert.ToDateTime(DateTime.Now, culturaCol);


                LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, "Search HoleID", "Consulta");
                this.cbxHole.DropDownStyle = ComboBoxStyle.DropDownList;
                DataTable dtHolesSourcesTargets = RN.GetDHCollars.get_Holes_Sources_Targets_lst();

                var holesList = dtHolesSourcesTargets.AsEnumerable()
                                   .Select(row => new
                                   {
                                       pozo = row.Field<string>("Pozo")
                                   }).Distinct();


                var sourceList = dtHolesSourcesTargets.AsEnumerable()
                                   .Select(row => new
                                   {
                                       source = row.Field<string>("Source")
                                   }).Distinct();

                var targetList = dtHolesSourcesTargets.AsEnumerable()
                                   .Select(row => new
                                   {
                                       target = row.Field<string>("Target"),
                                       code = row.Field<string>("Code_Target")
                                   }).Distinct()
                                   .OrderByDescending(x => x.target);


                cbxHole.Items.Add("Select a Hole ID");
                foreach (var item in holesList.ToList())
                {
                    cbxHole.Items.Add(item.pozo);
                }
                cbxHole.DisplayMember = "Pozo";
                cbxHole.ValueMember = "Pozo";
                cbxHole.DropDownStyle = ComboBoxStyle.DropDown;
                cbxHole.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbxHole.AutoCompleteSource = AutoCompleteSource.ListItems;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #endregion

        private void btnLock_Click(object sender, EventArgs e)
        {
            if (isSaved)
            {
                DialogResult result = islocked ? MessageBox.Show("The selected hole has been blocked, press Yes to unblock the hole.", "Warning", MessageBoxButtons.YesNo) : MessageBox.Show("The selected hole has not been blocked, press Yes to block the hole.", "Warning", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    RN.GetDHCollars.LockUnlockHole(cbxHole.SelectedItem.ToString(), !islocked);

                    MessageBox.Show(String.Format("The selected hole was {0} sucessfully.", islocked ? "unblocked" : "blocked"), "Information", MessageBoxButtons.OK);
                    islocked = !islocked;
                }
            }
            else
            {
                MessageBox.Show("The selected hole has not been saved, Pleased save the calculated values before block the hole.", "Warning", MessageBoxButtons.OK);
            }
        }

        private void frm_Intercept_Intervals_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
