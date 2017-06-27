using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using BancTec.PCR2P.Core.BusinessLogic.Administration;
using BancTec.PCR2P.Core.DatabaseModel.Administration;
using NetCommunTools;

namespace Administration
{
    public partial class frmCalendarEditJob : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmCalendarEditJob).Name;

        const string ACTION_EDIT = "Edit";
        const string ACTION_ADD = "Add";
        const string ACTION_COPY = "Copy";

        int? calendarID;
        int? newCalendarID = null;
        
        string action;
        bool isSomethingChanged = false;
        bool isFormClosedByUser = false;

        IBtecDB btecDB;
        ResourceManager rm;
        ILoggerBtec logger;

        public frmCalendarEditJob(IBtecDB btecDBParent, string actionParent, int? calendarIDParent)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmCalendarEditJob));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmCalendarEditJob));
                }

                //For traces
                logger.Debug(location + " Starting...");

                //For traces
                logger.Debug(location + "Parameters:");
                logger.Debug(location + "  actionParent-> " + actionParent.ToString());
                logger.Debug(location + "  calendarIDParent-> " + calendarIDParent.ToString());

                //We must get the resource manager after setting the culture.
                rm = SingletonResourcesManager.Instance.getResourceManager();

                //For traces   
                logger.Debug(location + "Before line: InitializeComponent();");

                //Initializes all the components of the form
                InitializeComponent();

                btecDB = btecDBParent;
                action = actionParent;
                calendarID = calendarIDParent;

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
        private void frmEditJob_Load(object sender, EventArgs e)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //displays an hourglass cursor
                Cursor.Current = Cursors.WaitCursor;

                //For traces
                logger.Debug(location + " Starting...");

                //For traces
                logger.Info(location + "Purpose: Initializes some components at the opening of the form");

                EnableControl.TextBox(txtID, false);

                //For traces   
                logger.Debug(location + "Before line: JobCalendarsDef jcd = new JobCalendarsDef(btecDB);");

                JobCalendarsDef jcd = new JobCalendarsDef(btecDB);

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  action-> " + action);

                switch (action)
                {
                    case ACTION_EDIT:
                        this.Text = rm.GetString("editJob");

                        txtID.Text = calendarID.ToString();
                        
                        //For traces   
                        logger.Debug(location + "Before line: txtDescription.Text = jcd.getDescription(calendarID);");

                        txtDescription.Text = jcd.getDescription(calendarID);

                        btnAction.Text = rm.GetString("btnOK");
                        break;
                    case ACTION_ADD:
                        this.Text = rm.GetString("addJob");

                        //For traces   
                        logger.Debug(location + "Before line: newCalendarID = jcd.getMaxID() + 1;");

                        newCalendarID = jcd.getMaxID() + 1;

                        txtID.Text = newCalendarID.ToString();
                        txtDescription.Text = null;

                        btnAction.Text = rm.GetString("btnAdd");
                        break;
                    case ACTION_COPY:
                        this.Text = rm.GetString("copyJob");

                        //For traces   
                        logger.Debug(location + "Before line: newCalendarID = jcd.getMaxID() + 1;");

                        newCalendarID = jcd.getMaxID() + 1;

                        txtID.Text = newCalendarID.ToString();
                        txtDescription.Text = null;

                        btnAction.Text = rm.GetString("btnAdd");
                        break;
                }

                //To detect a change in the form
                txtDescription.TextChanged += new EventHandler(this.somethingChanged);
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

        //Occurs when the [Action] button is clicked
        private void btnAction_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Action] button is clicked");

                if (validateForm())
                {
                    //For traces   
                    logger.Debug(location + " After line: if (validateForm())");

                    if (isSomethingChanged)
                    {
                        //For traces   
                        logger.Debug(location + " After line: if (isSomethingChanged)");

                        DialogResult result = MessageBox.Show(rm.GetString("msgSaveChanges2"),
                                                              rm.GetString("titleSaveChanges2"),
                                                              MessageBoxButtons.OKCancel,
                                                              MessageBoxIcon.None);

                        switch (result)
                        {
                            case DialogResult.OK:
                                //For traces   
                                logger.Debug(location + "Before line: saveForm();");

                                saveForm();

                                isFormClosedByUser = true;

                                //For the parent form knows that there was a change in the child data
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                                break;
                            case DialogResult.Cancel:
                                break;
                        }
                    }
                    else
                    {
                        isFormClosedByUser = true;

                        //For the parent form knows that there was no change in the child data
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
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

        //Saves the form's values in the database
        private void saveForm()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //displays an hourglass cursor
            Cursor.Current = Cursors.WaitCursor;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Saves the form's values in the database");

            string buffer;

            buffer = txtDescription.Text.Trim();
            string description = buffer.Substring(0, Math.Min(buffer.Length, 60));

            //For traces   
            logger.Debug(location + "Before line: JobCalendarsDef jcd = new JobCalendarsDef(btecDB);");

            JobCalendarsDef jcd = new JobCalendarsDef(btecDB);

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  action-> " + action);
            logger.Debug(location + "  calendarID-> " + (calendarID == null ? "NULL" : calendarID.ToString()));
            logger.Debug(location + "  newCalendarID-> " + (newCalendarID == null ? "NULL" : newCalendarID.ToString()));
            logger.Debug(location + "  description-> " + description);

            switch (action)
            {
                case ACTION_EDIT:
                    //For traces   
                    logger.Debug(location + "Before line: jcd.saveCalendar((int)calendarID, description);");

                    jcd.saveCalendar((int)calendarID, description);
                    break;
                case ACTION_ADD:
                    //For traces   
                    logger.Debug(location + "Before line: jcd.saveCalendar((int)newCalendarID, description);");

                    jcd.saveCalendar((int)newCalendarID, description);
                    break;
                case ACTION_COPY:
                    //For traces   
                    logger.Debug(location + "Before line: jcd.saveCalendar((int)newCalendarID, description);");
                    logger.Debug(location + "        and: jcd.copyCalendar((int)calendarID, (int)newCalendarID);");

                    jcd.saveCalendar((int)newCalendarID, description);
                    jcd.copyCalendar((int)calendarID, (int)newCalendarID);
                    break;
            }

            //For traces
            logger.Debug(location + " Ending...");

            //displays the default cursor
            Cursor.Current = Cursors.Default;
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

            string description = txtDescription.Text.Trim();

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  description-> " + description);

            //Validation 1
            if (String.IsNullOrEmpty(description))
            {
                //For traces   
                logger.Debug(location + " After line: if (String.IsNullOrEmpty(description))");

                //Set the focus to the Year field
                txtDescription.Focus();

                MessageBox.Show(rm.GetString("msgDescriptionCalendar"),
                                rm.GetString("TitleMsgValidationError"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return false;
            }

            //For traces
            logger.Debug(location + " Ending...");

            return true;
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

        //Occurs when the form is closed
        private void frmJobEdit_FormClosing(object sender, FormClosingEventArgs e)
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

                        DialogResult result = MessageBox.Show(rm.GetString("MsgQuitWithoutSaving"), rm.GetString("titleQuitWithoutSaving"), MessageBoxButtons.YesNo, MessageBoxIcon.None);

                        //For traces
                        logger.Debug(location + "Values:");
                        logger.Debug(location + "  result-> " + result.ToString());

                        switch (result)
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