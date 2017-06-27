//2013-07 Simon Boutin MANTIS# 18750 : Convert LBTables from VB to C#

using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using AboutBox_dotNet;
using BancTec.PCR2P.Core.DatabaseModel;
using BancTec.PCR2P.Core.DatabaseModel.Login;
using NetCommunTools;
using BancTec.PCR2P.Core.DatabaseModel.Administration;

namespace Administration
{
    public partial class frmMainMenu : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmMainMenu).Name;

        const int FCATID_TABLES = 20;
        const string REGCOD_NORTH_AMERICA = "000";
        const string REGCOD_USA = "001";

        ILoggerBtec logger;
        ResourceManager rm;
        IBtecDB btecDB;

        string _sLogginUserID = "";
        string _language = "";
        int _userAuthorizationLevel = 0;
//        bool isFormClosedByUser = false;

        frmAddressDef addressDef = null;
        frmEndPointDef endPoints = null;
        frmSortPatternDef sortPatternDef = null;
        frmWorkTypeDef workTypeDef = null;
        frmJobGroupingDef jobGroupingDef = null;
        frmUserProfileDef userProfileDef = null;
        frmAccessGroupDef accessGroupDef = null;
        frmSystemDateDef systemDate = null;
        frmCalendarDef calendar = null;
        frmExchangeRateDef viewExchangeRate = null;
        AboutBox aboutBox = null;
        frmAboutIcons aboutIcons = null;

        public frmMainMenu()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {

                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmMainMenu));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmMainMenu));
                }

                //For traces
                logger.Debug(location + " Starting...");
                
                PCRLogin pcrLogin = null;
                pcrLogin = new PCRLogin(typeof(frmMainMenu).Assembly.FullName);

                if (!pcrLogin.Login(LOGIN_SOURCE.LOGIN_MODULE_PCR2P))
                {
                    pcrLogin.Dispose();

                    //For traces   
                    logger.Debug(location + "Login returned false, exit");

                    Environment.Exit(0);
                }

                _sLogginUserID = pcrLogin.GetUserName;
                btecDB = IBtecDBFactory.GetDefaultDb(pcrLogin.ConnProvider);

                pcrLogin.Dispose();
                pcrLogin = null;
                
                //Needed so the form's KeyDown event can be call
                KeyPreview = true;

                //For traces   
                logger.Debug(location + "Before line: IniFile ini = new IniFile(Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), \"lockbox.ini\"));");

                IniFile ini = new IniFile(Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "lockbox.ini"));

                //For traces   
                logger.Debug(location + "Before line: LanguageDefinition ld = new LanguageDefinition(btecDB);");
                logger.Debug(location + "        and: LanguageDefinitionData ldData = ld.getLanguage(_sLogginUserID);");

                LanguageDefinition ld = new LanguageDefinition(btecDB);
                LanguageDefinitionData ldData = ld.getLanguage(_sLogginUserID);
                
                _language = ldData.GetStringValue(ldData.LANGUAGE_ID);

                //To enable this form to be multilingual. The "Localizable" propertie must be True for the form (next 2 lines)
                Thread.CurrentThread.CurrentCulture = new CultureInfo(_language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(_language);

                //We must get the resource manager after setting the culture.
                rm = SingletonResourcesManager.Instance.getResourceManager();

                //For traces   
                logger.Debug(location + "Before line: InitializeComponent();");

                //Initializes all the components of the form
                InitializeComponent();

                //For traces
                logger.Debug(location + " Ending...");
            }
            catch (Exception ex)
            {
                //For traces
                logger.Error(location + ex.Message);

                if (rm == null)
                {
                    //Informs the user that an error occurs
                    MessageBox.Show("An error occured you may gather more information from the traces." + " : " +
                                    ex.Message, "Sorry for the inconvenience.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Informs the user that an error occurs
                    MessageBox.Show(rm.GetString("GenericErrorOccured") + " : " + 
                                    ex.Message, rm.GetString("GenericErrorOccuredCaption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Start.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }

        private void ShowMe()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Maximized;
            }

            // make our form jump to the top of everything
            //bool top = TopMost;
            // make our form jump to the top of everything
            //TopMost = true;
            // set it back to whatever it was
            //TopMost = top;

            //Focus();
            this.BringToFront();
            this.Activate();
        }

        [DllImport("user32.dll", EntryPoint = "keybd_event", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern void keybd_event(byte vk, byte scan, int flags, int extrainfo);

        //Occurs when the mnuItmAboutIcons menu item is clicked
        private void mnuItmAboutIcons_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the mnuItmAboutIcons menu item is clicked");

                if (isFormAlreadyOpen(typeof(frmAboutIcons)))
                {
                    aboutIcons.Select();
                }
                else
                {
                    aboutIcons = new frmAboutIcons(btecDB);

                    aboutIcons.MdiParent = this;

                    //For traces   
                    logger.Debug(location + "Before line: aboutIcons.Show();");
                
                    aboutIcons.Show();
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

        //Occurs when the F1 key is pressed
        private void frmAdministration_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            displayHelp();
        }

        //Occurs when the mnuItmHelp menu item is clicked
        private void mnuItmHelp_Click(object sender, EventArgs e)
        {
            displayHelp();
        }

        //Displays the help documentation about the Administration module
        private void displayHelp()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Displays the help documentation about the Administration module");

            HelpPdf help = new HelpPdf("Administration", _language);

            //For traces   
            logger.Debug(location + "Before line: help.DisplayHelp();");

            help.DisplayHelp();

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Occurs when the mnuItmAboutAdministration menu item is clicked
        private void mnuItmAboutAdministration_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the mnuItmAboutAdministration menu item is clicked");

                if (isFormAlreadyOpen(typeof(AboutBox)))
                {
                    aboutBox.Select();
                }
                else
                {
                    Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                    string moduleName = rm.GetString("Administration");

                    aboutBox = new AboutBox(moduleName, version.ToString());

                    aboutBox.MdiParent = this;

                    //For traces   
                    logger.Debug(location + "Before line: aboutBox.Show();");
                
                    aboutBox.Show();
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

        //Occurs when the [Calendars] button is clicked
        private void btnCalendars_Click(object sender, EventArgs e)
        {
            openFrmCalendar();
        }

        //Occurs when the mnuItmCalendars menu item is clicked
        private void mnuItmCalendars_Click(object sender, EventArgs e)
        {
            openFrmCalendar();
        }

        //Opens the frmCalendar form
        private void openFrmCalendar()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Opens the frmCalendar form");

            if (isFormAlreadyOpen(typeof(frmCalendarDef)))
            {
                calendar.Select();
            }
            else
            {
                calendar = new frmCalendarDef(btecDB, _sLogginUserID);

                calendar.MdiParent = this;

                //For traces   
                logger.Debug(location + "Before line: calendar.Show();");
                
                calendar.Show();
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Determines if a specified form is open
        private bool isFormAlreadyOpen(Type FormType)
        {
            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.GetType() == FormType)
                    return true;
            }

            return false;
        }

        //Occurs when the form is closing
        private void frmAdministration_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeAdministration();
        }

        //Occurs when the mnuItmQuit menu item is clicked
        private void mnuItmQuit_Click(object sender, EventArgs e)
        {
            closeAdministration();
        }

        //Defitively close the Administration module
        private void closeAdministration()
        {
            Environment.Exit(0);
        }

        //Occurs when the mnuItmAddress menu item is clicked
        private void mnuItmAddress_Click(object sender, EventArgs e)
        {
            openFrmAddressDef();
        }

        //Occurs when the [Address] button is clicked
        private void btnAddress_Click(object sender, EventArgs e)
        {
            openFrmAddressDef();
        }

        //Opens the frmAddressDef form
        private void openFrmAddressDef()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Opens the frmAddressDef form");

            if (isFormAlreadyOpen(typeof(frmAddressDef)))
            {
                addressDef.Select();
            }
            else
            {
                addressDef = new frmAddressDef(btecDB);

                addressDef.MdiParent = this;

                //For traces   
                logger.Debug(location + "Before line: addressDef.Show();");
                
                addressDef.Show();
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Occurs when the mnuItmEndPoint menu item is clicked
        private void mnuItmEndPoint_Click(object sender, EventArgs e)
        {
            openFrmEndPoints();
        }

        //Occurs when the [End Point] button is clicked
        private void btnEndPoint_Click(object sender, EventArgs e)
        {
            openFrmEndPoints();
        }

        //Opens the frmEndPoints form
        private void openFrmEndPoints()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Opens the frmEndPoints form");

            if (isFormAlreadyOpen(typeof(frmEndPointDef)))
            {
                endPoints.Select();
            }
            else
            {
                endPoints = new frmEndPointDef(btecDB);

                endPoints.MdiParent = this;

                //For traces   
                logger.Debug(location + "Before line: endPoints.Show();");
                
                endPoints.Show();
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Occurs when the mnuItmSortPattern menu item is clicked
        private void mnuItmSortPattern_Click(object sender, EventArgs e)
        {
            openFrmSortPatternDef();
        }

        //Occurs when the [Sort Pattern] button is clicked
        private void btnSortPattern_Click(object sender, EventArgs e)
        {
            openFrmSortPatternDef();
        }

        //Opens the frmSortPatternDef form
        private void openFrmSortPatternDef()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Opens the frmSortPatternDef form");

            if (isFormAlreadyOpen(typeof(frmSortPatternDef)))
            {
                sortPatternDef.Select();
            }
            else
            {
                sortPatternDef = new frmSortPatternDef(btecDB);

                sortPatternDef.MdiParent = this;

                //For traces   
                logger.Debug(location + "Before line: sortPatternDef.Show();");

                sortPatternDef.Show();
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Occurs when the mnuItmWorkType menu item is clicked
        private void mnuItmWorkType_Click(object sender, EventArgs e)
        {
            openFrmWorkTypeDef();
        }

        //Occurs when the [Work Type] button is clicked
        private void btnWorkType_Click(object sender, EventArgs e)
        {
            openFrmWorkTypeDef();
        }

        //Opens the frmWorkTypeDef form
        private void openFrmWorkTypeDef()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Opens the frmWorkTypeDef form");

            if (isFormAlreadyOpen(typeof(frmWorkTypeDef)))
            {
                workTypeDef.Select();
            }
            else
            {
                workTypeDef = new frmWorkTypeDef(btecDB);

                workTypeDef.MdiParent = this;

                //For traces   
                logger.Debug(location + "Before line: workTypeDef.Show();");
                
                workTypeDef.Show();
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Occurs when the mnuItmJobGrouping menu item is clicked
        private void mnuItmJobGrouping_Click(object sender, EventArgs e)
        {
            openFrmJobGroupingDef();
        }

        //Occurs when the [Job Grouping] button is clicked
        private void btnJobGrouping_Click(object sender, EventArgs e)
        {
            openFrmJobGroupingDef();
        }

        //Opens the frmJobGroupingDef form
        private void openFrmJobGroupingDef()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Opens the frmJobGroupingDef form");

            if (isFormAlreadyOpen(typeof(frmJobGroupingDef)))
            {
                jobGroupingDef.Select();
            }
            else
            {
                jobGroupingDef = new frmJobGroupingDef(btecDB);

                jobGroupingDef.MdiParent = this;

                //For traces   
                logger.Debug(location + "Before line: jobGroupingDef.Show();");

                jobGroupingDef.Show();
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Occurs when the mnuItmUserProfile menu item is clicked
        private void mnuItmUserProfile_Click(object sender, EventArgs e)
        {
            openFrmUserProfileDef();
        }

        //Occurs when the [User Profile] button is clicked
        private void btnUserProfile_Click(object sender, EventArgs e)
        {
            openFrmUserProfileDef();
        }

        //Opens the frmUserProfileDef form
        private void openFrmUserProfileDef()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Opens the frmUserProfileDef form");

            if (isFormAlreadyOpen(typeof(frmUserProfileDef)))
            {
                userProfileDef.Select();
            }
            else
            {
                userProfileDef = new frmUserProfileDef(btecDB, _sLogginUserID, _userAuthorizationLevel);

                userProfileDef.MdiParent = this;

                //For traces   
                logger.Debug(location + "Before line: userProfileDef.Show();");
                
                userProfileDef.Show();
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Occurs when the mnuItmAccessGroup menu item is clicked
        private void mnuItmAccessGroup_Click(object sender, EventArgs e)
        {
            openFrmAccessGroupDef();
        }

        //Occurs when the [Access Group] button is clicked
        private void btnAccessGroup_Click(object sender, EventArgs e)
        {
            openFrmAccessGroupDef();
        }

        //Opens the frmAccessGroupDef form
        private void openFrmAccessGroupDef()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Opens the frmAccessGroupDef form");

            if (isFormAlreadyOpen(typeof(frmAccessGroupDef)))
            {
                accessGroupDef.Select();
            }
            else
            {
                accessGroupDef = new frmAccessGroupDef(btecDB);

                accessGroupDef.MdiParent = this;

                //For traces   
                logger.Debug(location + "Before line: accessGroupDef.Show();");
                
                accessGroupDef.Show();
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Occurs when the mnuItmSystemDate menu item is clicked
        private void mnuItmSystemDate_Click(object sender, EventArgs e)
        {
            openFrmSystemDate();
        }

        //Occurs when the [System Date] button is clicked
        private void btnSystemDate_Click(object sender, EventArgs e)
        {
            openFrmSystemDate();
        }

        //Opens the frmSystemDate form
        private void openFrmSystemDate()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Opens the frmSystemDate form");

            if (isFormAlreadyOpen(typeof(frmSystemDateDef)))
            {
                systemDate.Select();
            }
            else
            {
                systemDate = new frmSystemDateDef(btecDB);

                systemDate.MdiParent = this;

                //For traces   
                logger.Debug(location + "Before line: systemDate.Show();");
                
                systemDate.Show();
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Occurs when the mnuItmExchangeRate menu item is clicked
        private void mnuItmExchangeRate_Click(object sender, EventArgs e)
        {
            openFrmViewExchangeRate();
        }

        //Occurs when the [Exchange Rate] button is clicked
        private void btnExchangeRate_Click(object sender, EventArgs e)
        {
            openFrmViewExchangeRate();
        }

        //Opens the frmViewExchangeRate form
        private void openFrmViewExchangeRate()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Opens the frmViewExchangeRate form");

            if (isFormAlreadyOpen(typeof(frmExchangeRateDef)))
            {
                viewExchangeRate.Select();
            }
            else
            {
                viewExchangeRate = new frmExchangeRateDef(btecDB);

                viewExchangeRate.MdiParent = this;

                //For traces   
                logger.Debug(location + "Before line: viewExchangeRate.Show();");
                
                viewExchangeRate.Show();
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Initializes some components at the opening of the form
        private void frmMainMenu_Load(object sender, EventArgs e)
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

                disableFormsAccess();
                enableFormsAccess();
    
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

        //Enables the access to the forms if the user have the specific rights
        private void enableFormsAccess()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Enables the access to the forms if the user have the specific rights");

            //displays an hourglass cursor
            Cursor.Current = Cursors.WaitCursor;

            int functionId;
            int authorizationPreference;

            //For traces   
            logger.Debug(location + "Before line: AdminAppUserFunction aauf = new AdminAppUserFunction(btecDB);");
            logger.Debug(location + "        and: AppUserFunctionData[] aufData = aauf.GetAppUserFunctionFromCategory(_sLogginUserID, FCATID_TABLES);");

            AdminAppUserFunction aauf = new AdminAppUserFunction(btecDB);
            AppUserFunctionData[] aufData = aauf.GetAppUserFunctionFromCategory(_sLogginUserID, FCATID_TABLES);

            //For traces   
            logger.Debug(location + "Before line: ApplicationParameter ap = new ApplicationParameter(btecDB);");
            logger.Debug(location + "        and: ApplicationParameterData apData = ap.GetApplicationParameters();");

            ApplicationParameter ap = new ApplicationParameter(btecDB);
            ApplicationParameterData apData = ap.GetApplicationParameters();

            string regionCode = apData.GetStringValue(ApplicationParameterData.REGION_CODE);

            if (string.IsNullOrEmpty(regionCode)) regionCode = REGCOD_NORTH_AMERICA;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  regionCode-> " + regionCode);

            //For traces   
            logger.Debug(location + "Before line: foreach (AppUserFunctionData row in aufData)");

            foreach (AppUserFunctionData row in aufData)
            {
                functionId = row.GetIntValue(row.FUNCTION_ID);
                authorizationPreference = int.Parse(row.GetStringValue(row.AUTHORIZATION_PREFERENCE));

                switch (functionId)
                {
                    case 202: //Calendars
                        if (authorizationPreference != 0)
                        {
                            mnuItmCalendars.Enabled = true;
                            btnCalendars.Enabled = true;
                        }
                        break;
                    case 203: //Exchange Rate
                        if (authorizationPreference != 0 &&
                            regionCode == REGCOD_NORTH_AMERICA)
                        {
                            mnuItmExchangeRate.Enabled = true;
                            btnExchangeRate.Enabled = true;
                        }
                        break;
                    case 204: //User Profile
                        _userAuthorizationLevel = authorizationPreference;

                        if (authorizationPreference != 0)
                        {
                            mnuItmUserProfile.Enabled = true;
                            btnUserProfile.Enabled = true;
                        }
                        break;
                    case 208: //Sort Pattern
                        if (authorizationPreference != 0)
                        {
                            mnuItmSortPattern.Enabled = true;
                            btnSortPattern.Enabled = true;
                        }
                        break;
                    case 209: //Work Type
                        if (authorizationPreference != 0)
                        {
                            mnuItmWorkType.Enabled = true;
                            btnWorkType.Enabled = true;
                        }
                        break;
                    case 210: //End Point
                        if (authorizationPreference != 0)
                        {
                            mnuItmEndPoint.Enabled = true;
                            btnEndPoint.Enabled = true;
                        }
                        break;
                    case 211: //Address
                        if (authorizationPreference != 0)
                        {
                            mnuItmAddress.Enabled = true;
                            btnAddress.Enabled = true;
                        }
                        break;
                    case 212: //System Date
                        if (authorizationPreference != 0)
                        {
                            mnuItmSystemDate.Enabled = true;
                            btnSystemDate.Enabled = true;
                        }
                        break;
                    case 217: //Access Group
                        if (authorizationPreference != 0)
                        {
                            mnuItmAccessGroup.Enabled = true;
                            btnAccessGroup.Enabled = true;
                        }
                        break;
                    case 218: //Job Grouping
                        if (authorizationPreference != 0)
                        {
                            mnuItmJobGrouping.Enabled = true;
                            btnJobGrouping.Enabled = true;
                        }
                        break;
                }
            }

            //displays the default cursor
            Cursor.Current = Cursors.Default;

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Disables by default all access to the forms
        private void disableFormsAccess()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Disables by default all access to the forms");

            mnuItmAddress.Enabled = false;
            btnAddress.Enabled = false;

            mnuItmEndPoint.Enabled = false;
            btnEndPoint.Enabled = false;

            mnuItmSortPattern.Enabled = false;
            btnSortPattern.Enabled = false;

            mnuItmWorkType.Enabled = false;
            btnWorkType.Enabled = false;

            mnuItmJobGrouping.Enabled = false;
            btnJobGrouping.Enabled = false;

            mnuItmUserProfile.Enabled = false;
            btnUserProfile.Enabled = false;

            mnuItmAccessGroup.Enabled = false;
            btnAccessGroup.Enabled = false;

            mnuItmSystemDate.Enabled = false;
            btnSystemDate.Enabled = false;

            mnuItmCalendars.Enabled = false;
            btnCalendars.Enabled = false;

            mnuItmExchangeRate.Enabled = false;
            btnExchangeRate.Enabled = false;

            //For traces
            logger.Debug(location + " Ending...");
        }
    }
}