using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Intercept_Intervals.UI
{
    public partial class exportForm : Form
    {
        private string _exportType = string.Empty;
        private string _sources = string.Empty;
        private string _target = string.Empty;

        public string Target { get { return _target; } }
        public string Source { get { return _sources; } }
        public string ExportType
        {
            get
            {
                return _exportType;
            }
        }


        public exportForm()
        {
            InitializeComponent();
        }

        private void exportForm_Load(object sender, EventArgs e)
        {
            if (rdBtnHoleId.Checked == true)
            {
                lvwSource.Enabled = false;
                cbxTarget.Enabled = false;
            }
            else
            {
                lvwSource.Enabled = true;
                cbxTarget.Enabled = true;
            }

            DataTable dtHolesSourcesTargets = RN.GetDHCollars.get_Holes_Sources_Targets_lst();
            var sourceList = dtHolesSourcesTargets.AsEnumerable()
                                  .Select(row => new
                                  {
                                      source = row.Field<string>("Source")
                                  }).Distinct();

            foreach (var item in sourceList.ToList())
            {
                lvwSource.Items.Add(item.source);
            }

            /* Modificado Alvaro Araujo 06/06/2019*/

            DataRow nuevoRegistro = dtHolesSourcesTargets.NewRow();
            nuevoRegistro["Target"] = "---Select---";
            nuevoRegistro["Code_Target"] = "-999";

            dtHolesSourcesTargets.Rows.Add(nuevoRegistro);

            var targetList = dtHolesSourcesTargets.AsEnumerable()
                               .Select(row => new
                               {
                                   target = row.Field<string>("Target"),
                                   code = row.Field<string>("Code_Target")
                               }).Distinct()
                               .OrderByDescending(x => x.target).Reverse();
            
            cbxTarget.DataSource = targetList.ToList();
            cbxTarget.DisplayMember = "target";
            cbxTarget.ValueMember = "code";
            cbxTarget.SelectedValue = "-999";

            /* ********************************** */
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {

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
                var rdbtn = grboxExport.Controls.OfType<RadioButton>()
                         .FirstOrDefault(n => n.Checked);
                this._exportType = (rdbtn as RadioButton).Tag.ToString();

                if (this.ExportType == "HOLE")
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

                var selectedTags = this.lvwSource.CheckedItems
                                     .Cast<ListViewItem>()
                                     .Select(x => x.Text);

                if (this._exportType == "SOURCE")
                {
                    if (selectedTags.Count() == 0)
                    {
                        MessageBox.Show("Must select at least one source.");
                    }
                    else if (selectedTags.Count() > 1)
                    {
                        MessageBox.Show("Just one source must be selected.");
                    }
                    else
                    {
                        this._sources = selectedTags.ToArray()[0];
                        /* Modificado Alvaro Araujo 06/06/2019*/
                        this._target = cbxTarget.SelectedValue.ToString().TrimEnd();

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }

                if (this._exportType == "TARGET")
                {
                    if (selectedTags.Count() > 0 && cbxTarget.SelectedValue.ToString() != "-999")
                    {
                        this._sources = selectedTags.ToArray()[0];
                        /* Modificado Alvaro Araujo 03/07/2019*/
                        this._target = cbxTarget.SelectedValue.ToString().TrimEnd();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Must select at least one Target and one Source.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void rdBtnHoleId_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnHoleId.Checked == true)
            {
                lvwSource.Enabled = false;
                cbxTarget.Enabled = false;
            }
            else
            {
                lvwSource.Enabled = true;
                cbxTarget.Enabled = true;
            }
        }

    }
}
