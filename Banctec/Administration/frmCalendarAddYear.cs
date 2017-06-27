//2013-05 Simon Boutin MANTIS# 18750 : Convert LBTables from VB to C#

using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using BancTec.PCR2P.Core.BusinessLogic.Administration;
using NetCommunTools;
using BancTec.PCR2P.Core.DatabaseModel.Administration;

namespace Administration
{
    public partial class frmCalendarAddYear : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmWorkTypeEdit).Name;

        const string TAB_SITE = "TabSite";
        const string TAB_JOB = "TabJob";

        IBtecDB btecDB;
        ResourceManager rm;
        ILoggerBtec logger;

        frmCalendarDef parentForm;
        string tab;

        bool isFormClosedByUser = false;

        int newYear = DateTime.Today.Year;

        public frmCalendarAddYear(frmCalendarDef parent, IBtecDB btecDBParent, string parentTab)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmWorkTypeEdit));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmWorkTypeEdit));
                }

                //For traces
                logger.Debug(location + " Starting...");

                //We must get the resource manager after setting the culture.
                rm = SingletonResourcesManager.Instance.getResourceManager();

                //For traces   
                logger.Debug(location + "Before line: InitializeComponent();");

                //Initializes all the components of the form
                InitializeComponent();

                parentForm = parent;
                btecDB = btecDBParent;
                tab = parentTab;

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
        private void frmCalendarAddYear_Load(object sender, EventArgs e)
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

                txtYear.Text = newYear.ToString();

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

                if (validateForm())
                {
                    //For traces   
                    logger.Debug(location + " After line: if (validateForm())");

                    parentForm.newYear = int.Parse(txtYear.Text.Trim());

                    isFormClosedByUser = true;

                    this.DialogResult = DialogResult.OK;
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

        //Performs all form's validation
        private bool validateForm()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Performs all form's validation");

            string strYear = txtYear.Text.Trim();
            int year;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  strYear-> " + strYear);

            //Validation 1
            if (!int.TryParse(strYear, out year))
            {
                //For traces   
                logger.Debug(location + " After line: if (!int.TryParse(strYear, out year))");

                //Set the focus to the Year field
                txtYear.Focus();

                MessageBox.Show(rm.GetString("msgYearInteger"),
                                rm.GetString("TitleMsgValidationError"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return false;
            }

            //Validation 2
            if (year < 1 || year > 9998)
            {
                //For traces   
                logger.Debug(location + " After line: if (year < 1 || year > 9998)");

                //Set the focus to the Year field
                txtYear.Focus();

                MessageBox.Show(rm.GetString("msgYearRange1to9998"),
                                rm.GetString("TitleMsgValidationError"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return false;
            }

            //Validation 3
            switch (tab)
            {
                case TAB_SITE:
                    //For traces   
                    logger.Debug(location + "Before line: CalendarTable ct = new CalendarTable(btecDB);");

                    CalendarTable ct = new CalendarTable(btecDB);

                    if (ct.getCalendar(year, parentForm.currentValueCboSite) != null)
                    {
                        //For traces   
                        logger.Debug(location + " After line: if (ct.getCalendar(year, parentForm.currentValueCboSite) != null)");

                        //Set the focus to the Year field
                        txtYear.Focus();

                        MessageBox.Show(String.Format(rm.GetString("msgSiteYearAlreadyExists"), year.ToString()),
                                        rm.GetString("TitleMsgValidationError"),
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

                        return false;
                    }
                    break;
                case TAB_JOB:
                    //For traces   
                    logger.Debug(location + "Before line: JobCalendars jc = new JobCalendars(btecDB);");

                    JobCalendars jc = new JobCalendars(btecDB);

                    if (jc.getCalendar(year, parentForm.currentValueCboJob) != null)
                    {
                        //For traces   
                        logger.Debug(location + " After line: if (jc.getCalendar(year, parentForm.currentValueCboJob) != null)");

                        //Set the focus to the Year field
                        txtYear.Focus();

                        MessageBox.Show(String.Format(rm.GetString("msgJobYearAlreadyExists"), year.ToString()),
                                        rm.GetString("TitleMsgValidationError"),
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

                        return false;
                    }
                    break;
            }

            //For traces
            logger.Debug(location + " Ending...");

            return true;
        }

        //Occurs when the form is closed
        private void frmCalendarAddYear_FormClosing(object sender, FormClosingEventArgs e)
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

                if (isFormClosedByUser == false)
                {
                    this.DialogResult = DialogResult.Cancel;
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
