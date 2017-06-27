using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using BancTec.PCR2P.Core.BusinessLogic.Administration;
using BancTec.PCR2P.Core.DatabaseModel;
using BancTec.PCR2P.Core.DatabaseModel.Administration;

using NetCommunTools;

namespace Administration
{
    public partial class frmEndPointEdit : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmEndPointEdit).Name;

        IBtecDB btecDB;
        long endPointID = 0;
        bool isSomethingChanged = false;
        bool isFormClosedByUser = false;

        ResourceManager rm;
        ILoggerBtec logger;

        public frmEndPointEdit(IBtecDB btecDBParent, string title, long endPointIDParent)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmEndPointEdit));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmEndPointEdit));
                }

                //For traces
                logger.Debug(location + " Starting...");

                //For traces
                logger.Debug(location + "Parameters:");
                logger.Debug(location + "  title-> " + title);
                logger.Debug(location + "  calendarIDParent-> " + endPointIDParent.ToString());

                //We must get the resource manager after setting the culture.
                rm = SingletonResourcesManager.Instance.getResourceManager();

                //For traces   
                logger.Debug(location + "Before line: InitializeComponent();");

                //Initializes all the components of the form
                InitializeComponent();

                this.Text = title;

                btecDB = btecDBParent;
                endPointID = endPointIDParent;

                //For traces
                logger.Debug(location + " Ending...");
            }
            catch (Exception ex)
            {
                //For traces
                logger.Error(location + ex.Message);

                //Informs the user that an error occurs
                MessageBox.Show(rm.GetString("GenericErrorOccured") + " : " + ex.Message, rm.GetString("GenericErrorOccuredCaption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Initializes some components at the opening of the form
        private void frmEndPointsEdit_Load(object sender, EventArgs e)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //For traces
                logger.Debug(location + " Starting...");

                //For traces
                logger.Info(location + "Purpose: Initializes some components at the opening of the form");

                //displays an hourglass cursor
                Cursor.Current = Cursors.WaitCursor;

                logger.Debug(location + "Before line: AddressDefinition ad = new AddressDefinition(btecDB);");
                logger.Debug(location + "        and: DataTable dtFrom = ad.GetDataTableAddressInfo();");

                AddressDefinition ad = new AddressDefinition(btecDB);
                DataTable dtFrom = ad.GetDataTableAddressInfo();

                cboFromAddress.DataSource = dtFrom;
                cboFromAddress.DisplayMember = "NAME";
                cboFromAddress.ValueMember = "ADDRESS_ID";

                DataTable dtTo = ad.GetDataTableAddressInfo();
                //To add a blank line in the cboToAddress object list
                dtTo.Rows.InsertAt(dtTo.NewRow(), 0);

                cboToAddress.DataSource = dtTo;
                cboToAddress.DisplayMember = "NAME";
                cboToAddress.ValueMember = "ADDRESS_ID";

                //For traces   
                logger.Debug(location + "Before line: EndPoint ep = new EndPoint(btecDB);");
                logger.Debug(location + "        and: EndPointData epData = ep.GetEndPointInfo(endPointID);");

                EndPoint ep = new EndPoint(btecDB);
                EndPointData epData = ep.GetEndPointInfo(endPointID);

                string description = null;
                long fromAddress = 0;
                long toAddress = 0;
                string transit = null;
                string bankAccount = null;
                string pcCode = null;
                string institution = null;

                if (epData != null)
                {
                    string strFromAddress = epData.GetStringValue(epData.ADDRESS_ID1);

                    //To set the value in the cboFromAddress object
                    if (!string.IsNullOrEmpty(strFromAddress))
                    {
                        fromAddress = long.Parse(strFromAddress);

                        cboFromAddress.SelectedValue = fromAddress;
                    }

                    string strToAddress = epData.GetStringValue(epData.ADDRESS_ID2);

                    //To set the value in the cboToAddress object
                    if (!string.IsNullOrEmpty(strToAddress))
                    {
                        toAddress = long.Parse(strToAddress);

                        cboToAddress.SelectedValue = toAddress;
                    }

                    description = epData.GetStringValue(epData.DESCRIPTION);
                    transit = epData.GetStringValue(epData.TRANSIT);
                    bankAccount = epData.GetStringValue(epData.BANK_ACCOUNT);
                    pcCode = epData.GetStringValue(epData.PC_CODE);
                    institution = epData.GetStringValue(epData.INSTITUTION);
                }

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  endPointID-> " + endPointID.ToString());
                logger.Debug(location + "  description-> " + description);
                logger.Debug(location + "  cboFromAddress-> " + (cboFromAddress.Text ?? ""));
                logger.Debug(location + "  cboToAddress-> " + (cboToAddress.Text ?? ""));
                logger.Debug(location + "  transit-> " + transit);
                logger.Debug(location + "  bankAccount-> " + bankAccount);
                logger.Debug(location + "  pcCode-> " + pcCode);
                logger.Debug(location + "  institution-> " + institution);

                labelEndPointID.Text = endPointID.ToString();
                txtDescription.Text = description;
                txtR_T.Text = transit;
                txtBankAccount.Text = bankAccount;
                txtPCCode.Text = pcCode;
                txtInstitution.Text = institution;

                //To detect a change in the form (next 7 lines)
                txtDescription.TextChanged += new EventHandler(this.somethingChanged);
                cboFromAddress.TextChanged += new EventHandler(this.somethingChanged);
                cboToAddress.TextChanged += new EventHandler(this.somethingChanged);
                txtR_T.TextChanged += new EventHandler(this.somethingChanged);
                txtBankAccount.TextChanged += new EventHandler(this.somethingChanged);
                txtPCCode.TextChanged += new EventHandler(this.somethingChanged);
                txtInstitution.TextChanged += new EventHandler(this.somethingChanged);

                //displays the default cursor
                Cursor.Current = Cursors.Default;

                //For traces
                logger.Debug(location + " Ending...");
            }
            catch (Exception ex)
            {
                //For traces
                logger.Error(location + ex.Message);

                //Informs the user that an error occurs
                MessageBox.Show(rm.GetString("GenericErrorOccured") + " : " + ex.Message, rm.GetString("GenericErrorOccuredCaption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //occurs when any field is modified
        private void somethingChanged(object sender, EventArgs e)
        {
            isSomethingChanged = true;
        }

        //Occurs when the [Cancel] button is clicked
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //For traces
                logger.Debug(location + " Starting...");

                //For traces
                logger.Info(location + "Purpose: Occurs when the [Cancel] button is clicked");

                isFormClosedByUser = true;

                //For the parent form knows that there was no change in the child data
                this.DialogResult = DialogResult.Cancel;
                this.Close();

                //For traces
                logger.Debug(location + " Ending...");
            }
            catch (Exception ex)
            {
                //For traces
                logger.Error(location + ex.Message);

                //Informs the user that an error occurs
                MessageBox.Show(rm.GetString("GenericErrorOccured") + " : " + ex.Message, rm.GetString("GenericErrorOccuredCaption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Occurs when the [OK] button is clicked
        private void btnOK_Click(object sender, EventArgs e)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //For traces
                logger.Debug(location + " Starting...");

                //For traces
                logger.Info(location + "Purpose: Occurs when the [OK] button is clicked");

                if (!string.IsNullOrEmpty(txtDescription.Text.Trim()))
                {
                    //For traces   
                    logger.Debug(location + " After line: if (!string.IsNullOrEmpty(txtDescription.Text.Trim()))");

                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  isSomethingChanged-> " + isSomethingChanged.ToString());

                    if (isSomethingChanged)
                    {
                        //For traces   
                        logger.Debug(location + " After line: if (isSomethingChanged)");

                        //displays an hourglass cursor
                        Cursor.Current = Cursors.WaitCursor;

                        DialogResult result = MessageBox.Show(rm.GetString("msgSaveChanges2"),
                                                              rm.GetString("titleSaveChanges2"),
                                                              MessageBoxButtons.OKCancel,
                                                              MessageBoxIcon.None);

                        switch (result)
                        {
                            case DialogResult.OK:
                                string buffer;

                                buffer = txtDescription.Text.Trim();
                                string description = buffer.Substring(0, Math.Min(buffer.Length, 100));

                                string addressID1 = cboFromAddress.SelectedValue.ToString();
                                addressID1 = string.IsNullOrEmpty(addressID1) ? "null" : addressID1;

                                string addressID2 = cboToAddress.SelectedValue.ToString();
                                addressID2 = string.IsNullOrEmpty(addressID2) ? "null" : addressID2;

                                buffer = txtR_T.Text.Trim();
                                string transit = buffer.Substring(0, Math.Min(buffer.Length, 15));

                                buffer = txtBankAccount.Text.Trim();
                                string bankAccount = buffer.Substring(0, Math.Min(buffer.Length, 25));

                                buffer = txtPCCode.Text.Trim();
                                string pcCode = buffer.Substring(0, Math.Min(buffer.Length, 15));

                                buffer = txtInstitution.Text.Trim();
                                string institution = buffer.Substring(0, Math.Min(buffer.Length, 5));

                                EndPoint ep = new EndPoint(btecDB);

                                //For traces
                                logger.Debug(location + "Values:");
                                logger.Debug(location + "  endPointID-> " + endPointID.ToString());
                                logger.Debug(location + "  description-> " + description);
                                logger.Debug(location + "  addressID1-> " + addressID1);
                                logger.Debug(location + "  addressID2-> " + addressID2);
                                logger.Debug(location + "  transit-> " + transit);
                                logger.Debug(location + "  bankAccount-> " + bankAccount);
                                logger.Debug(location + "  pcCode-> " + pcCode);
                                logger.Debug(location + "  institution-> " + institution);

                                //For traces   
                                logger.Debug(location + "Before line: ep.SaveEndPoint(endPointID, description, addressID1, addressID2, transit, bankAccount, pcCode, institution);");

                                //save the end point
                                ep.SaveEndPoint(endPointID,
                                                description,
                                                addressID1,
                                                addressID2,
                                                transit,
                                                bankAccount,
                                                pcCode,
                                                institution);

                                isFormClosedByUser = true;

                                //For the parent form knows that there was a change in the child data
                                this.DialogResult = DialogResult.OK;
//                                this.Close();
                                break;
                            case DialogResult.Cancel:
                                break;
                        }

                        //displays the default cursor
                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        isFormClosedByUser = true;

                        //For the parent form knows that there was no change in the child data
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    }
                }
                else
                {
                    //Set the focus to the Description field
                    txtDescription.Focus();

                    MessageBox.Show(rm.GetString("msgDescriptionEndPoint"),
                                    rm.GetString("TitleMsgValidationError"),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }

                //For traces
                logger.Debug(location + " Ending...");
            }
            catch (Exception ex)
            {
                //For traces
                logger.Error(location + ex.Message);

                //Informs the user that an error occurs
                MessageBox.Show(rm.GetString("GenericErrorOccured") + " : " + ex.Message, rm.GetString("GenericErrorOccuredCaption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Occurs when the form is closed
        private void frmEndPointsEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //For traces
                logger.Debug(location + " Starting...");

                //For traces
                logger.Info(location + "Purpose: Occurs when the form is closed");

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  isSomethingChanged-> " + isSomethingChanged.ToString());

                if (isSomethingChanged)
                {
                    //For traces
                    logger.Debug(location + "  isFormClosedByUser-> " + isFormClosedByUser.ToString());

                    //If the form does not ever be closed by the user
                    if (!isFormClosedByUser)
                    {
                        //For traces   
                        logger.Debug(location + " After line: if (!isFormClosedByUser)");

                        DialogResult selectButton = MessageBox.Show(rm.GetString("MsgQuitWithoutSaving"), rm.GetString("titleQuitWithoutSaving"), MessageBoxButtons.YesNo, MessageBoxIcon.None);

                        //For traces
                        logger.Debug(location + "Values:");
                        logger.Debug(location + "  result-> " + selectButton.ToString());

                        switch (selectButton)
                        {
                            case DialogResult.Yes:
                                //For the parent form knows that there was no change in the child data
                                this.DialogResult = DialogResult.Cancel;

                                //Closes the form
                                break;
                            case DialogResult.No:
                                //Doesn't close the form
                                e.Cancel = true;
                                break;
                        }
                    }
                }

                //For traces
                logger.Debug(location + " Ending...");
            }
            catch (Exception ex)
            {
                //For traces
                logger.Error(location + ex.Message);

                //Informs the user that an error occurs
                MessageBox.Show(rm.GetString("GenericErrorOccured") + " : " + ex.Message, rm.GetString("GenericErrorOccuredCaption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
