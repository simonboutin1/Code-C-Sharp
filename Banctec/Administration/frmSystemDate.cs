using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using BancTec.PCR2P.Core.BusinessLogic.Administration;
using BancTec.PCR2P.Core.DatabaseModel;
using BancTec.PCR2P.Core.DatabaseModel.Administration;
using NetCommunTools;

namespace Administration
{
    //Needed so that the form can be called in Visual Basic (the next 4 lines)
    [ComVisible(true)]
    [Guid("12E9E36B-362E-49AF-B4C3-46237108110B")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Administration.frmSystemDate")]

    public partial class frmSystemDate : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmSystemDate).Name;

        //Needed so that the form can be called in Visual Basic (the next 2 lines)
        bool _isInit = false;
        public bool IsInitialized { get { return _isInit; } }

        ILoggerBtec logger;
        ResourceManager rm;
        IBtecDB btecDB;

        string _sLogginUserID = "";
        string _sLoggedPWD = "";
        string _sLoggedDSN = "";
        int _intUsersAuthorizationLevel;
        
        const string RADIO_DEFAULT = "radioDefault";
        const string RADIO_FOLLOWING = "radioFollowing";

        bool isSomethingChanged = false;
        bool isFormClosedByUser = false;

        public frmSystemDate() { }

        //Needed so that the form can be called in Visual Basic
        public void Init(string sLoggedUserID, string sPWD, string sDSN, int intLanguage, int intUsersAuthorizationLevel)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {

                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmSystemDate));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmSystemDate));
                }

                //For traces
                logger.Debug(location + " Starting...");

                //For traces
                logger.Info(location + "Purpose: Needed so that the form can be called in Visual Basic");

                //For traces
                logger.Debug(location + "Parameters:");
                logger.Debug(location + "  sLoggedUserID-> " + sLoggedUserID);
                logger.Debug(location + "  sPWD-> " + "*****");
                logger.Debug(location + "  sDSN-> " + sDSN);
                logger.Debug(location + "  intLanguage-> " + intLanguage.ToString());
                logger.Debug(location + "  intUsersAuthorizationLevel-> " + intUsersAuthorizationLevel.ToString());

                //If we want to force the form to appear in French. Remove these 2 lines if we want English (default)
//                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fr-CA");
//                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr-CA");

                //To enable this form to be multilingual. The "Localizable" propertie must be True for the form (the next 2 lines)
                Thread.CurrentThread.CurrentCulture = new CultureInfo((intLanguage == 2 ? "fr-CA" : "en-US"));
                Thread.CurrentThread.CurrentUICulture = new CultureInfo((intLanguage == 2 ? "fr-CA" : "en-US"));

                //We must get the resource manager after setting the culture.
                rm = Utilities.GetResourceManager("Administration.Properties.Resources", this.GetType().Assembly);

                //For traces   
                logger.Debug(location + "Before line: InitializeComponent();");

                //Initializes all the components of the form
                InitializeComponent();

                _sLogginUserID = sLoggedUserID;
                _intUsersAuthorizationLevel = intUsersAuthorizationLevel;
                _sLoggedDSN = sDSN;
                _sLoggedPWD = sPWD;

                if (string.IsNullOrEmpty(_sLogginUserID) || string.IsNullOrEmpty(_sLoggedPWD) || string.IsNullOrEmpty(_sLoggedDSN))
                {
                    MessageBox.Show(rm.GetString("UserOrPasswordIsNull"), rm.GetString("MissingInfo"));
                    this.Close();
                }
                else
                {
                    //For traces   
                    logger.Debug(location + "Before line: IniFile ini = new IniFile(Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), \"lockbox.ini\"));");
                    logger.Debug(location + "        and: btecDB = IBtecDBFactory.GetDefaultDb(new ConnectionProvider(_sLoggedDSN, ini.ReadValue(\"Database\", \"TNS\", \"\"), _sLogginUserID, _sLoggedPWD));");

                    IniFile ini = new IniFile(Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "lockbox.ini"));
                    btecDB = IBtecDBFactory.GetDefaultDb(new ConnectionProvider(_sLoggedDSN, ini.ReadValue("Database", "TNS", ""), _sLogginUserID, _sLoggedPWD));
                }

                _isInit = true;

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
        private void frmSystemDate_Load(object sender, EventArgs e)
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

                //For traces   
                logger.Debug(location + "Before line: ApplicationParameter ap = new ApplicationParameter(btecDB);");
                logger.Debug(location + "        and: ApplicationParameterData apData = ap.GetApplicationParameters();");

                ApplicationParameter ap = new ApplicationParameter(btecDB);
                ApplicationParameterData apData = ap.GetApplicationParameters();

                //Add the CheckedChanged event for all radio buttons in the grpProcessingDate group
                foreach (Control control in grpProcessingDate.Controls)
                {
                    RadioButton radioButton = control as RadioButton;

                    if (radioButton != null)
                    {
                        radioButton.CheckedChanged += new EventHandler(this.radioButton_CheckedChanged);
                        radioButton.CheckedChanged += new EventHandler(this.somethingChanged);
                    }
                }

                dtpSystemDate.ValueChanged += new EventHandler(this.somethingChanged);

                string strDate = apData.GetValue("OVERRIDE_SYSTEM_DATE").ToString();

                if (string.IsNullOrEmpty(strDate))
                {
                    strDate = (DateTime.Now).ToString("yyyyMMdd");

                    radioDefault.Checked = true;
                }
                else
                {
                    radioFollowing.Checked = true;
                }

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  strDate-> " + strDate);

                //Used to force the display of a specific date format in a DateTimePicker (next 2 lines)
                dtpSystemDate.Format = DateTimePickerFormat.Custom;
                dtpSystemDate.CustomFormat = "dd MMMM yyyy";

                dtpSystemDate.Value = DateTime.ParseExact(strDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

                isSomethingChanged = false;

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

        //Occurs when the checked radio button is changed
        void radioButton_CheckedChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the checked radio button is changed");

                RadioButton radioButton = sender as RadioButton;

                if (radioButton != null)
                {
                    if (radioButton.Checked)
                    {
                        if (radioButton.Name == RADIO_FOLLOWING)
                        {
                            dtpSystemDate.Enabled = true;
                        }
                        else
                        {
                            dtpSystemDate.Enabled = false;
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

        //Occurs when any field is modified
        private void somethingChanged(object sender, EventArgs e)
        {
            isSomethingChanged = true;
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

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  isSomethingChanged-> " + isSomethingChanged.ToString());

                if (isSomethingChanged)
                {
                    //For traces   
                    logger.Debug(location + " After line: if (isSomethingChanged)");

                    //displays an hourglass cursor
                    Cursor.Current = Cursors.WaitCursor;

                    DialogResult selectedButton = MessageBox.Show(rm.GetString("msgSaveChanges2"),
                                                            rm.GetString("titleSaveChanges2"),
                                                            MessageBoxButtons.YesNoCancel,
                                                            MessageBoxIcon.Question);

                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  selectedButton-> " + selectedButton.ToString());

                    switch (selectedButton)
                    {
                        case DialogResult.Yes:
                            //For traces   
                            logger.Debug(location + "Before line: saveForm();");

                            saveForm();

                            isFormClosedByUser = true;

                            this.Close();
                            break;
                        case DialogResult.No:
                            isFormClosedByUser = true;

                            this.Close();
                            break;
                        case DialogResult.Cancel:
                            break;
                    }

                    //displays the default cursor
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    //For traces   
                    logger.Debug(location + " After line: if (isSomethingChanged)...else");

                    isFormClosedByUser = true;

                    this.Close();
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

        //Saves all the form's data
        private void saveForm()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Saves all the form's data");

            string date = string.Empty;

            if (getSelectedRadioButton() == RADIO_FOLLOWING)
            {
                date = dtpSystemDate.Value.ToString("yyyyMMdd");
            }

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  date-> " + date);

            //For traces   
            logger.Debug(location + "Before line: ApplicationParameterCustum apc = new ApplicationParameterCustum(btecDB);");
            logger.Debug(location + "        and: apc.saveOverrideSystemDate(date);");

            ApplicationParameterCustum apc = new ApplicationParameterCustum(btecDB);
            apc.saveOverrideSystemDate(date);

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Allows to know which radio button is currently selected
        private string getSelectedRadioButton()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Allows to know which radio button is currently selected");

            var checkedButton = grpProcessingDate.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  checkedButton.Name-> " + checkedButton.Name);

            //For traces
            logger.Debug(location + " Ending...");

            return checkedButton.Name;
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

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  isSomethingChanged-> " + isSomethingChanged.ToString());

                if (isSomethingChanged)
                {
                    //For traces   
                    logger.Debug(location + " After line: if (isSomethingChanged)");

                    DialogResult selectedButton = MessageBox.Show(rm.GetString("MsgQuitWithoutSaving"),
                                                          rm.GetString("titleQuitWithoutSaving"),
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Question);

                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  selectedButton-> " + selectedButton.ToString());

                    switch (selectedButton)
                    {
                        case DialogResult.Yes:
                            isFormClosedByUser = true;

                            this.Close();
                            break;
                        case DialogResult.No:
                            break;
                    }
                }
                else
                {
                    //For traces   
                    logger.Debug(location + " After line: if (isSomethingChanged)...else");

                    isFormClosedByUser = true;

                    this.Close();
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
        private void frmSystemDate_FormClosing(object sender, FormClosingEventArgs e)
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
                logger.Debug(location + "  isFormClosedByUser-> " + isFormClosedByUser.ToString());

                if (isSomethingChanged)
                {
                    //For traces   
                    logger.Debug(location + " After line: if (isSomethingChanged)");
  
                    //If the form does not ever be closed by the user
                    if (!isFormClosedByUser)
                    {
                        //For traces   
                        logger.Debug(location + " After line: if (!isFormClosedByUser)");

                        DialogResult selectedButton = MessageBox.Show(rm.GetString("MsgQuitWithoutSaving"), rm.GetString("titleQuitWithoutSaving"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        //For traces
                        logger.Debug(location + "Values:");
                        logger.Debug(location + "  selectedButton-> " + selectedButton.ToString());

                        switch (selectedButton)
                        {
                            case DialogResult.Yes:
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
