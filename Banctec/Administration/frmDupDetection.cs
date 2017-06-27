using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using NetCommunTools;
using BancTec.PCR2P.Core.BusinessLogic.Administration;
namespace Administration
{
    [ComVisible(true)]
    [Guid("8F91AC22-13C4-47a9-9BD6-16C3C977730B")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Administration.frmDupDetection")]
    public partial class frmDupDetection : Form
    {
        private const string ClassName = "BancTec.PCR2P.Core.BusinessLogic.Administration.frmDupDetection";
        private ILoggerBtec m_logger;
        private bool _isInit = false;
        public bool IsInitialized { get { return _isInit; } }
        private DuplicateDetection m_controller = null;
        public DuplicateDetection Controller { get { return m_controller; } }

        public frmDupDetection()
        {
            //InitializeComponent();
            try
            {
                string _slocation = ClassName + "." + MethodBase.GetCurrentMethod().Name.ToString() + LogManagerBtec.TraceSeparator;
                m_logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmDupDetection));
                if (string.IsNullOrEmpty(m_logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(System.IO.Path.Combine(Environment.CurrentDirectory, "Parmatec.Ini"), "Administration.exe", "");
                    m_logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmDupDetection));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Init(string sLoggedUserID, string sPWD, string sDSN, int intLanguage, int intUsersAuthorizationLevel)
        {
            string _location = ClassName + ".Init(" + sLoggedUserID + ", " + intLanguage.ToString() + ", " + intUsersAuthorizationLevel.ToString() + ")";

            try
            {
                /* m_logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmDupDetection));
                 if (string.IsNullOrEmpty(m_logger.LogFileName))
                 {
                     LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(System.IO.Path.Combine(Environment.CurrentDirectory, "Parmatec.Ini"), "Administration.exe", "");
                     m_logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmDupDetection));
                 }*/
                m_logger.Debug(_location + "Starting... Instantiating Controller class");
                m_controller = new DuplicateDetection(sLoggedUserID, sPWD, sDSN);
                m_logger.Debug(_location + "Controller class instantiated... Adding modes to Combo Box");
                InitializeComponent();
                cboDupMode.Items.Add(new DupMode(DuplicateMode.UnmatchEnvelope));
                cboDupMode.Items.Add(new DupMode(DuplicateMode.RejectTrans));
                Refresh();
                m_logger.Debug(_location + " Ending...");
                _isInit = true;
            }
            catch (Exception ex)
            {
                Utilities.LogError(m_logger, ex, _location);
            }
        }
        private void Refresh()
        {
            string _slocation = ClassName + "." + MethodBase.GetCurrentMethod().Name.ToString() + LogManagerBtec.TraceSeparator;
            cboDupMode.SelectedIndex = m_controller.GetDupMode();
            m_logger.Debug(_slocation + "Get current Dup Mode: " + ((DuplicateMode)cboDupMode.SelectedIndex).ToString());

            txtDaysLookBack.Text = m_controller.GetDays();
            m_logger.Debug(_slocation + "Get days looked back: " + txtDaysLookBack.Text);

            cboStatus.DataSource = m_controller.GetStatus();
            cboStatus.DisplayMember = "Status_desc";
            cboStatus.ValueMember = "Status";
            if ((DuplicateMode)cboDupMode.SelectedIndex == DuplicateMode.RejectTrans) { cboStatus.Enabled = false; }
            cboStatus.SelectedValue = m_controller.GetSavedStatus();
            m_logger.Debug(_slocation + "Get Batch restage Status: " + cboStatus.SelectedText.ToString());

            cboDocID.DataSource = m_controller.GetTrpDocs();

            cboDocID.DisplayMember = m_controller.TrpDocDisplay;
            cboDocID.ValueMember = m_controller.TrpDocValue;

            m_logger.Debug(_slocation + "Get List of filter out RTs");
            dgvRTs.DataSource = m_controller.GetRts();
            dgvRTs.Columns[m_controller.RTColumnName].HeaderText = "Routing And Transit";
            dgvRTs.Columns[m_controller.BankAccountColumnName].HeaderText = "Bank Account";
            dgvRTs.Refresh();

            m_logger.Debug(_slocation + "Get List of filter out Doc IDs");
            lstDocIDs.DataSource = m_controller.GetDocs();
            lstDocIDs.DisplayMember = m_controller.DocColumnName;
            lstDocIDs.Refresh();

            chkActivate.Checked = m_controller.GetActive();
            if (chkActivate.Checked == true) { chkActivate.CheckState = CheckState.Checked; }
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            string _slocation = ClassName + "." + MethodBase.GetCurrentMethod().Name.ToString() + LogManagerBtec.TraceSeparator;
            try
            {
                m_logger.Debug(_slocation + " Starting...");
                Cursor.Current = Cursors.WaitCursor;
                m_controller.SaveData(chkActivate.Checked, (DuplicateMode)cboDupMode.SelectedIndex,
                    txtDaysLookBack.Text, cboStatus.SelectedValue.ToString(), txtRejectReason.Text);
                Refresh();
                Cursor.Current = Cursors.Default;
                m_logger.Debug(_slocation + " Ending...");
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                m_logger.Error(_slocation, ex);
                MessageBox.Show(ex.Message, "Error while saving data. More relevant information are available the trace files.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            string _slocation = ClassName + "." + MethodBase.GetCurrentMethod().Name.ToString() + LogManagerBtec.TraceSeparator;
            m_logger.Debug(_slocation + "See ya...");
            m_logger = null;
            m_controller = null;
            this.Close();
            this.Dispose();
        }
        private void cmdAddRT_Click(object sender, EventArgs e)
        {
            string _slocation = ClassName + "." + MethodBase.GetCurrentMethod().Name.ToString() + LogManagerBtec.TraceSeparator;
            try
            {
                m_logger.Debug(_slocation + " Starting...");
                dgvRTs.DataSource = m_controller.AddRt(txtRT.Text, txtBankAcct.Text);
                //dgvRTs.DisplayMember = m_controller.RTColumnName;
                dgvRTs.Refresh();
                txtRT.Text = "";
                txtRT.Focus();
                m_logger.Debug(_slocation + " Ending...");
            }
            catch (Exception ex)
            {
                m_logger.Error(_slocation, ex);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmdRemoveRT_Click(object sender, EventArgs e)
        {
            string _slocation = ClassName + "." + MethodBase.GetCurrentMethod().Name.ToString() + LogManagerBtec.TraceSeparator;
            try
            {
                m_logger.Debug(_slocation + " Starting...");
                if (dgvRTs.SelectedRows.Count != 0)
                {
                    dgvRTs.DataSource = m_controller.RemoveRt(dgvRTs.SelectedRows[0].Cells[m_controller.RTColumnName].Value.ToString(),
                        dgvRTs.SelectedRows[0].Cells[m_controller.BankAccountColumnName].Value.ToString());
                    //dgvRTs.DisplayMember = m_controller.RTColumnName;
                    dgvRTs.Refresh();
                    txtRT.Text = "";
                    txtBankAcct.Text = "";
                }
                m_logger.Debug(_slocation + " Ending...");
            }
            catch (Exception ex)
            {
                m_logger.Error(_slocation, ex);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmdAddDocID_Click(object sender, EventArgs e)
        {
            string _slocation = ClassName + "." + MethodBase.GetCurrentMethod().Name.ToString() + LogManagerBtec.TraceSeparator;
            try
            {
                m_logger.Debug(_slocation + " Starting...");
                if (cboDocID.SelectedValue != null)
                {
                    lstDocIDs.DataSource = m_controller.AddDoc(cboDocID.SelectedValue.ToString());
                    lstDocIDs.DisplayMember = m_controller.DocColumnName;
                    lstDocIDs.Refresh();
                    cboDocID.Focus();
                }
                m_logger.Debug(_slocation + " Ending...");
            }
            catch (Exception ex)
            {
                m_logger.Error(_slocation, ex);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmdRemoveDocID_Click(object sender, EventArgs e)
        {
            string _slocation = ClassName + "." + MethodBase.GetCurrentMethod().Name.ToString() + LogManagerBtec.TraceSeparator;

            try
            {
                m_logger.Debug(_slocation + " Starting...");
                if (lstDocIDs.SelectedItem != null)
                {
                    lstDocIDs.DataSource = m_controller.RemoveDoc(((DataRowView)lstDocIDs.SelectedItem)[0].ToString());
                    lstDocIDs.DisplayMember = m_controller.DocColumnName;
                    lstDocIDs.Refresh();
                }
                m_logger.Debug(_slocation + " Ending...");
            }
            catch (Exception ex)
            {
                m_logger.Error(_slocation, ex);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void NumericValHandler(object sender, CancelEventArgs e)
        {
            string _slocation = ClassName + "." + MethodBase.GetCurrentMethod().Name.ToString() + LogManagerBtec.TraceSeparator;
            m_logger.Debug(_slocation + " Starting...");
            if (sender is TextBox)
            {
                try
                {
                    m_logger.Debug(_slocation + "Sender is TextBox verify...");
                    if (!string.IsNullOrEmpty(((TextBox)sender).Text)) { TextValidations.IsNumeric(((TextBox)sender).Text); }
                }
                catch (Exception ex)
                {
                    m_logger.Error(_slocation, ex);
                    MessageBox.Show(ex.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ((TextBox)sender).Focus();
                }
            }
            m_logger.Debug(_slocation + " Ending...");
        }
        private void chkActivate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActivate.Checked == true)
            {
                chkActivate.CheckState = CheckState.Checked;
                //dgvRTs.Enabled = true;
                groupBox1.Enabled = true;
            }
            else
            {
                groupBox1.Enabled = false;
                //dgvRTs.Enabled = false;
                chkActivate.CheckState = CheckState.Unchecked;
            }
        }
        private void cboDupMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((DuplicateMode)cboDupMode.SelectedIndex == DuplicateMode.RejectTrans) { cboStatus.Enabled = false; }
            else { cboStatus.Enabled = true; }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
