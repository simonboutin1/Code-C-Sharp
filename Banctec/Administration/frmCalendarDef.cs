//2013-05 Simon Boutin MANTIS# 18750 : Convert LBTables from VB to C#

using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class frmCalendarDef : Form, ICalendar
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmCalendarDef).Name;

        const string TAB_SITE = "TabSite";
        const string TAB_JOB = "TabJob";

        const int SATURDAY = 6;
        const int SUNDAY = 0;

        const string ACTION_EDIT = "Edit";
        const string ACTION_ADD = "Add";
        const string ACTION_COPY = "Copy";

        int CURRENT_YEAR = DateTime.Today.Year;
        int CURRENT_MONTH = DateTime.Today.Month;
        int CURRENT_DAY = DateTime.Today.Day;

        public int newYear;

        public int? currentValueCboSite;
        int? currentValueCboSiteYear;
        int? currentValueCboSiteMonth;

        public int? currentValueCboJob;
        int? currentValueCboJobYear;
        int? currentValueCboJobMonth;

        bool isSomethingChangedSite;
        bool isSomethingChangedJob;
        
        bool isCboSiteChanged = false;
        bool isCboJobChanged = false;

        bool isFormClosedByUser = false;

        DataTable captureSiteTable;
        DataTable siteYearTable;
        DataTable siteCalendarCustomTable;
        DataTable siteCalendarOriginalTable;

        DataTable jobYearTable;
        DataTable jobCalendarCustomTable;
        DataTable jobCalendarOriginalTable;

        CustomCalendar siteCalendar;
        CustomCalendar jobCalendar;
        CaptureSiteDefinitionData csdData;
        
        int userCaptureSite = -1;
        string userCaptureSiteDesc = "";

        string currentTab;

        ILoggerBtec logger;
        ResourceManager rm;
        IBtecDB btecDB;

        string _sLogginUserID = "";

        public frmCalendarDef(IBtecDB btecDBParent, string sLoggedUserID)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmCalendarDef));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmCalendarDef));
                }

                //For traces
                logger.Debug(location + " Starting...");

                //We must get the resource manager after setting the culture.
                rm = SingletonResourcesManager.Instance.getResourceManager();              

                _sLogginUserID = sLoggedUserID;
                btecDB = btecDBParent;

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

        //Initializes some components at the opening of the form
        private void frmCalendar_Load(object sender, EventArgs e)
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

                create_siteCalendarOriginalTable();
                create_jobCalendarOriginalTable();

                //For traces   
                logger.Debug(location + "Before line: generateSiteTab();");

                generateSiteTab();

                //For traces   
                logger.Debug(location + "Before line: generateJobTab();");
                
                generateJobTab();

                tabCalendar.SelectedTab = tabCalendar.TabPages[TAB_SITE];
                currentTab = TAB_SITE;

                //For traces
                logger.Debug(location + " Ending...");

                //displays the default cursor
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                //For traces
                logger.Error(location + ex.Message);

                //Informs the user that an error occurs
                MessageBox.Show(rm.GetString("GenericErrorOccured") + " : " + ex.Message, rm.GetString("GenericErrorOccuredCaption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Creates the siteCalendarOriginalTable DataTable
        void create_siteCalendarOriginalTable()
        {
            siteCalendarOriginalTable = new DataTable();

            siteCalendarOriginalTable.Columns.Add("CALENDAR_DATE", typeof(string));
            siteCalendarOriginalTable.Columns.Add("BUSINESS_DATE", typeof(string));
            siteCalendarOriginalTable.Columns.Add("CAPTURE_SITE", typeof(int));
        }

        //Creates the jobCalendarOriginalTable DataTable
        void create_jobCalendarOriginalTable()
        {
            jobCalendarOriginalTable = new DataTable();

            jobCalendarOriginalTable.Columns.Add("CALENDAR_DATE", typeof(string));
            jobCalendarOriginalTable.Columns.Add("POSTING_DATE", typeof(string));
            jobCalendarOriginalTable.Columns.Add("CALENDAR_ID", typeof(int));
        }

        //Generates the content of the Site tab
        void generateSiteTab()
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
            logger.Info(location + "Purpose: Generates the content of the Site tab");

            //For traces   
            logger.Debug(location + "Before line: CaptureSiteDefinition csd = new CaptureSiteDefinition(btecDB);");
            logger.Debug(location + "        and: csdData = csd.getUserInfo(_sLogginUserID);");

            CaptureSiteDefinition csd = new CaptureSiteDefinition(btecDB);
            csdData = csd.getUserInfo(_sLogginUserID);

            if (csdData == null)
            {
                MessageBox.Show(String.Format(rm.GetString("msgHomeSiteNotDefined"), _sLogginUserID),
                                rm.GetString("titleUnableToOpen"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                this.Close();
            }
            else
            {
                userCaptureSite = csdData.GetIntValue(csdData.SITE_ID);
                userCaptureSiteDesc = csdData.GetStringValue(csdData.DESCRIPTION);
            }

            //For traces   
            logger.Debug(location + "Before line: fillCboMonth(cboSiteMonth);");

            fillCboMonth(cboSiteMonth, TAB_SITE);

            //For traces   
            logger.Debug(location + "        and: fillCboSite();");

            fillCboSite();

            enableAttributes(TAB_SITE, false);

            //For traces   
            logger.Debug(location + "Before line: siteCalendar = new CustomCalendar();");
            logger.Debug(location + "        and: tableLayoutPanel2.Controls.Add(siteCalendar.calendar, 0, 1);");

            DrawingControl.SuspendDrawing(this);

            siteCalendar = new CustomCalendar(this);
            tableLayoutPanel2.Controls.Add(siteCalendar.calendar, 0, 1);

            DrawingControl.ResumeDrawing(this);

            //For traces   
            logger.Debug(location + "Before line: initializeTabSiteValues();");

            initializeTabSiteValues();

            //For traces
            logger.Debug(location + " Ending...");

            //displays the default cursor
            Cursor.Current = Cursors.Default;
        }

        //Generates the content of the Job tab
        void generateJobTab()
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
            logger.Info(location + "Purpose: Generates the content of the Job tab");

            //For traces   
            logger.Debug(location + "Before line: fillCboMonth(cboJobMonth, TAB_JOB);");

            fillCboMonth(cboJobMonth, TAB_JOB);

            //For traces   
            logger.Debug(location + "Before line: fillCboJob();");

            fillCboJob();

            enableAttributes(TAB_JOB, false);

            //For traces   
            logger.Debug(location + "Before line: jobCalendar = new CustomCalendar(this);");
            logger.Debug(location + "        and: tableLayoutPanel6.Controls.Add(jobCalendar.calendar, 0, 1);");

            DrawingControl.SuspendDrawing(this);

            jobCalendar = new CustomCalendar(this);
            tableLayoutPanel6.Controls.Add(jobCalendar.calendar, 0, 1);

            DrawingControl.ResumeDrawing(this);

            //For traces   
            logger.Debug(location + "Before line: initializeTabJobValues();");

            initializeTabJobValues();

            //For traces
            logger.Debug(location + " Ending...");

            //displays the default cursor
            Cursor.Current = Cursors.Default;
        }

        //Selects a default day on a calendar
        void selectDefaultDay(CustomCalendar calendar, int? currentValueCboMonth)
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
            logger.Info(location + "Purpose: Selects a default day on a calendar");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  currentValueCboMonth-> " + (currentValueCboMonth == null ? "NULL" : currentValueCboMonth.ToString()));

            if (currentValueCboMonth == CURRENT_MONTH)
            {
                //To prevent a bug if we have february 29
                if (calendar.selectDay(CURRENT_DAY) == null)
                {
                    calendar.selectDay(CURRENT_DAY - 1);
                }
            }
            else
            {
                calendar.selectDay(1);
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Initializes all the Site's tab values
        private void initializeTabSiteValues()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Initializes all the Site's tab values");

            currentValueCboSiteYear = CURRENT_YEAR;

            cboSite.SelectedValueChanged -= new System.EventHandler(cboSite_SelectedValueChanged);
            cboSite.SelectedValue = userCaptureSite;
            cboSite.SelectedValueChanged += new System.EventHandler(cboSite_SelectedValueChanged);

            currentValueCboSite = (int?)cboSite.SelectedValue;

            isCboSiteChanged = true;

            //For traces   
            logger.Debug(location + "Before line: fillCboSiteYear();");

            fillCboSiteYear();

            //For traces   
            logger.Debug(location + "Before line: assignYearCboSite();");

            assignYearCboSite();
            
            currentValueCboSiteYear = (int?)cboSiteYear.SelectedValue;
            currentValueCboSiteMonth = (int?)cboSiteMonth.SelectedValue;

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Initializes all the Job's tab values
        private void initializeTabJobValues()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Initializes all the Job's tab values");

            currentValueCboJobYear = CURRENT_YEAR;

            if (cboJob.Items.Count > 0)
            {
                cboJob.SelectedValueChanged -= new System.EventHandler(cboJob_SelectedValueChanged);
                cboJob.SelectedIndex = 0;
                cboJob.SelectedValueChanged += new System.EventHandler(cboJob_SelectedValueChanged);

                enableJobButtons(true);
            }
            else
            {
                jobCalendar.clean();
                enableJobButtons(false);
            }

            currentValueCboJob = (int?)cboJob.SelectedValue;

            isCboJobChanged = true;

            //For traces   
            logger.Debug(location + "Before line: fillCboJobYear();");

            fillCboJobYear();

            //For traces   
            logger.Debug(location + "Before line: assignYearCboJob();");

            assignYearCboJob();

            currentValueCboJobYear = (int?)cboJobYear.SelectedValue;
            currentValueCboJobMonth = (int?)cboJobMonth.SelectedValue;

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Assigns a year to the cboSiteYear object based on cboSite
        private bool assignYearCboSite()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Assigns a year to the cboSiteYear object based on cboSite");

            if (cboSiteYear.Items.Count == 0)
            {
                if (currentValueCboSite == userCaptureSite)
                {
                    MessageBox.Show(rm.GetString("msgSiteYearNotDefined"),
                                    rm.GetString("titleYearNotDefined"),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    //For traces   
                    logger.Debug(location + "Before line: createSiteYear(CURRENT_YEAR);");

                    createSiteYear(CURRENT_YEAR);

                    //For traces   
                    logger.Debug(location + "Before line: saveYear(siteCalendarOriginalTable, TAB_SITE);");

                    saveYear(siteCalendarOriginalTable, TAB_SITE);

                    //For traces   
                    logger.Debug(location + "Before line: fillCboSiteYear();");

                    fillCboSiteYear();
                    
                    cboSiteYear.SelectedIndex = 0;
                    currentValueCboSiteYear = (int?)cboSiteYear.SelectedValue;

                    //For traces   
                    logger.Debug(location + "Before line: initializeYear(TAB_SITE);");

                    initializeYear(TAB_SITE);

                    int? selectedMonth = (int?)cboSiteMonth.SelectedValue;
                    cboSiteMonth.SelectedValue = CURRENT_MONTH;

                    //Need this condition if the SelectedValueChanged event was not call
                    if (selectedMonth == (int?)cboSiteMonth.SelectedValue)
                    {
                        //For traces   
                        logger.Debug(location + "Before line: displayMonth(siteCalendar,...);");

                        displayMonth(siteCalendar,
                                     currentValueCboSiteYear,
                                     currentValueCboSiteMonth,
                                     TAB_SITE);

                        //For traces   
                        logger.Debug(location + "Before line: selectDefaultDay(siteCalendar, currentValueCboSiteMonth);");

                        selectDefaultDay(siteCalendar, currentValueCboSiteMonth);
                    }
                }
                else
                {
                    MessageBox.Show(String.Format(rm.GetString("msgCannotCreateCalendar"),userCaptureSiteDesc),
                                    rm.GetString("titleCannotCreate"),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    return false;
                }
            }
            else
            {
                cboSiteYear.SelectedValueChanged -= new System.EventHandler(cboSiteYear_SelectedValueChanged);

                if (WidgetsTools.ifFindComboValue(cboSiteYear, (int)currentValueCboSiteYear))
                {
                    cboSiteYear.SelectedValue = currentValueCboSiteYear;
                }
                else if (WidgetsTools.ifFindComboValue(cboSiteYear, CURRENT_YEAR))
                {
                    cboSiteYear.SelectedValue = CURRENT_YEAR;
                }
                else
                {
                    cboSiteYear.SelectedIndex = 0;
                }

                cboSiteYear.SelectedValueChanged += new System.EventHandler(cboSiteYear_SelectedValueChanged);

                cboSiteYear_SelectedValueChanged(null, null);
            }

            //For traces
            logger.Debug(location + " Ending...");

            return true;
        }

        //Assigns a year to the cboJobYear object based on cboJob
        private bool assignYearCboJob()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Assigns a year to the cboJobYear object based on cboJob");

            if (cboJobYear.Items.Count == 0)
            {
                if (currentValueCboJob != null)
                {
                    MessageBox.Show(rm.GetString("msgJobYearNotDefined"),
                                    rm.GetString("titleYearNotDefined"),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    //For traces   
                    logger.Debug(location + "Before line: createJobYear(CURRENT_YEAR);");

                    createJobYear(CURRENT_YEAR);

                    //For traces   
                    logger.Debug(location + "Before line: saveYear(jobCalendarOriginalTable, TAB_JOB);");
                    
                    saveYear(jobCalendarOriginalTable, TAB_JOB);

                    //For traces   
                    logger.Debug(location + "Before line: fillCboJobYear();");

                    fillCboJobYear();
                    
                    cboJobYear.SelectedIndex = 0;
                    currentValueCboJobYear = (int?)cboJobYear.SelectedValue;

                    //For traces   
                    logger.Debug(location + "Before line: initializeYear(TAB_JOB);");

                    initializeYear(TAB_JOB);

                    int? selectedMonth = (int?)cboJobMonth.SelectedValue;
                    cboJobMonth.SelectedValue = CURRENT_MONTH;

                    //Need this condition if the SelectedValueChanged event was not call
                    if (selectedMonth == (int?)cboJobMonth.SelectedValue)
                    {
                        //For traces   
                        logger.Debug(location + "Before line: displayMonth(jobCalendar,...);");

                        displayMonth(jobCalendar,
                                     currentValueCboJobYear,
                                     currentValueCboJobMonth,
                                     TAB_JOB);

                        //For traces   
                        logger.Debug(location + "Before line: selectDefaultDay(jobCalendar, currentValueCboJobMonth);");

                        selectDefaultDay(jobCalendar, currentValueCboJobMonth);
                    }
                }
                else
                {
                    currentValueCboJobYear = null;
                }
            }
            else
            {
                cboJobYear.SelectedValueChanged -= new System.EventHandler(cboJobYear_SelectedValueChanged);

                if (WidgetsTools.ifFindComboValue(cboJobYear, (int)currentValueCboJobYear))
                {
                    cboJobYear.SelectedValue = currentValueCboJobYear;
                }
                else if (WidgetsTools.ifFindComboValue(cboJobYear, CURRENT_YEAR))
                {
                    cboJobYear.SelectedValue = CURRENT_YEAR;
                }
                else
                {
                    cboJobYear.SelectedIndex = 0;
                }

                cboJobYear.SelectedValueChanged += new System.EventHandler(cboJobYear_SelectedValueChanged);

                cboJobYear_SelectedValueChanged(null,null);
            }

            //For traces
            logger.Debug(location + " Ending...");

            return true;
        }

        //Creates a year in reverse order in the siteCalendarOriginalTable data table for a specific year
        private void createSiteYear(int year)
        {
            DateTime myDate;
            DateTime bufferDate = new DateTime(year, 12, 30);
            string calendarDate;
            string businessDate;
            int nbDays;
            int weekDay;

            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Creates a year in reverse order in the siteCalendarOriginalTable data table for a specific year");

            //displays an hourglass cursor
            Cursor.Current = Cursors.WaitCursor;

            siteCalendarOriginalTable.Clear();

            //Obtains the very last business date in a buffer
            do
            {
                bufferDate = bufferDate.AddDays(1);
                weekDay = (int)bufferDate.DayOfWeek;
            } while (weekDay == SATURDAY || weekDay == SUNDAY);

            //Fills the siteCalendarOriginalTable data table in reverse order
            for (int month = 12; month >= 1; month--)
            {
                nbDays = DateTime.DaysInMonth(year, month);

                for (int day = nbDays; day >= 1; day--)
                {
                    myDate = new DateTime(year, month, day);
                    weekDay = (int)myDate.DayOfWeek;

                    if (!(weekDay == SATURDAY || weekDay == SUNDAY))
                    {
                        bufferDate = myDate;
                    }

                    calendarDate = myDate.ToString("yyyyMMdd");
                    businessDate = bufferDate.ToString("yyyyMMdd");

                    siteCalendarOriginalTable.Rows.Add(calendarDate,
                                                       businessDate,
                                                       (int)currentValueCboSite);
                }
            }

            //For traces
            logger.Debug(location + " Ending...");

            //displays the default cursor
            Cursor.Current = Cursors.Default;
        }

        //Creates a year in reverse order in the jobCalendarOriginalTable data table for a specific year
        private void createJobYear(int year)
        {
            DateTime myDate;
            string calendarDate;
            string postingDate;
            int nbDays;

            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Creates a year in reverse order in the jobCalendarOriginalTable data table for a specific year");

            //displays an hourglass cursor
            Cursor.Current = Cursors.WaitCursor;

            jobCalendarOriginalTable.Clear();

            //Fills the jobCalendarOriginalTable data table in reverse order
            for (int month = 12; month >= 1; month--)
            {
                nbDays = DateTime.DaysInMonth(year, month);

                for (int day = nbDays; day >= 1; day--)
                {
                    myDate = new DateTime(year, month, day);

                    calendarDate = myDate.ToString("yyyyMMdd");
                    postingDate = calendarDate;

                    jobCalendarOriginalTable.Rows.Add(calendarDate,
                                                      postingDate,
                                                      (int)currentValueCboJob);
                }
            }

            //For traces
            logger.Debug(location + " Ending...");

            //displays the default cursor
            Cursor.Current = Cursors.Default;
        }

        //Transferts the year in reverse order from a CalendarCustomTable data table to a CalendarOriginalTable data table
        private void transfertYear(DataTable calendarOriginalTable,
                                   DataTable calendarCustomTable,
                                   int? currentValueCboYear,
                                   int? currentValueCboId,
                                   string tab)
        {
            DataRow row;
            DateTime bufferDate = new DateTime((int)currentValueCboYear + 1, 1, 1);
            string calendarDate;
            string normalDate;
            int normalDay;
            int nbDays;
            string field = "";

            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Transferts the year in reverse order from a CalendarCustomTable data table to a CalendarOriginalTable data table");

            //displays an hourglass cursor
            Cursor.Current = Cursors.WaitCursor;

            calendarOriginalTable.Clear();

            normalDate = bufferDate.ToString("yyyyMMdd");
            nbDays = calendarCustomTable.Rows.Count;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  tab-> " + tab);
            logger.Debug(location + "  nbDays-> " + nbDays.ToString()); 
            
            switch (tab)
            {
                case TAB_SITE:
                    field = "WORKING_DAY";
                    break;
                case TAB_JOB:
                    field = "POSTING_DAY";
                    break;
            }

            //For traces   
            logger.Debug(location + "Before line: for (int i = nbDays - 1; i >= 0; i--)");

            //Transferts data in reverse order from a CalendarCustomTable to a CalendarOriginalTable
            for (int i = nbDays - 1; i >= 0; i--)
            {
                row = calendarCustomTable.Rows[i];
                calendarDate = row["CALENDAR_DATE"].ToString();
                normalDay = int.Parse(row[field].ToString());

                if (normalDay == 1)
                {
                    normalDate = calendarDate;
                }

                calendarOriginalTable.Rows.Add(calendarDate,
                                               normalDate,
                                               (int)currentValueCboId);
            }

            //For traces
            logger.Debug(location + " Ending...");

            //displays the default cursor
            Cursor.Current = Cursors.Default;
        }

        //Saves an entire year in the database
        private void saveYear(DataTable calendarOriginalTable,
                              string tab)
        {
            int nbDays = calendarOriginalTable.Rows.Count;
            string calendarDate;
            string normalDate;
            int id;
            DataRow row;

            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Saves an entire year in the database");

            //displays an hourglass cursor
            Cursor.Current = Cursors.WaitCursor;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  tab-> " + tab);
            logger.Debug(location + "  nbDays-> " + nbDays.ToString());

            //For traces   
            logger.Debug(location + "Before line: for (int i = nbDays - 1; i >= 0; i--)");

            //Saves each individual row in reverse order from a CalendarOriginalTable to the database
            for (int i = nbDays - 1; i >= 0; i--)
            {
                row = calendarOriginalTable.Rows[i];
                calendarDate = row["CALENDAR_DATE"].ToString();

                switch (tab)
                {
                    case TAB_SITE:
                        CalendarTable ct = new CalendarTable(btecDB);

                        normalDate = row["BUSINESS_DATE"].ToString();
                        id = int.Parse(row["CAPTURE_SITE"].ToString());

                        ct.saveDate(calendarDate, normalDate, id);
                        break;
                    case TAB_JOB:
                        JobCalendars jc = new JobCalendars(btecDB);

                        normalDate = row["POSTING_DATE"].ToString();
                        id = int.Parse(row["CALENDAR_ID"].ToString());

                        jc.saveDate(calendarDate, normalDate, id);
                        break;
                }
            }

            //For traces
            logger.Debug(location + " Ending...");

            //displays the default cursor
            Cursor.Current = Cursors.Default;
        }

        //Fills a cboMonth object with values for a specific ComboBox
        private void fillCboMonth(ComboBox cboMonth, string tab)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Fills a cboMonth object with values for a specific ComboBox");

            var dataSource = new List<ComboboxItem>();
            int i = 1;

            //For traces   
            logger.Debug(location + "Before line: foreach (var month in Thread.CurrentThread.CurrentCulture.DateTimeFormat.MonthNames.Take(12))");

            foreach (var month in Thread.CurrentThread.CurrentCulture.DateTimeFormat.MonthNames.Take(12))
            {
                dataSource.Add(new ComboboxItem() { Text = month, Value = i });
                i++;
            }

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  tab-> " + tab);

            switch (tab)
            {
                case TAB_SITE:
                    cboMonth.SelectedValueChanged -= new System.EventHandler(this.cboSiteMonth_SelectedValueChanged);
                    break;
                case TAB_JOB:
                    cboMonth.SelectedValueChanged -= new System.EventHandler(this.cboJobMonth_SelectedValueChanged);
                    break;
            }

            //For traces   
            logger.Debug(location + "Before line: cboMonth.DataSource = dataSource;");

            cboMonth.DataSource = dataSource;
            cboMonth.DisplayMember = "Text";
            cboMonth.ValueMember = "Value";

            switch (tab)
            {
                case TAB_SITE:
                    cboMonth.SelectedValueChanged += new System.EventHandler(this.cboSiteMonth_SelectedValueChanged);
                    break;
                case TAB_JOB:
                    cboMonth.SelectedValueChanged += new System.EventHandler(this.cboJobMonth_SelectedValueChanged);
                    break;
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Fills the cboSite object with values
        private void fillCboSite()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Fills the cboSite object with values");

            var dataSource = new List<ComboboxItem>();
            string text;
            int siteID;

            logger.Debug(location + "Before line: AdminCaptureSiteDefinition acsd = new AdminCaptureSiteDefinition(btecDB);");
            logger.Debug(location + "        and: captureSiteTable = acsd.getAllDescription();");

            AdminCaptureSiteDefinition acsd = new AdminCaptureSiteDefinition(btecDB);
            captureSiteTable = acsd.getAllDescription();

            if (captureSiteTable != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in captureSiteTable.Rows)");

                //fills the cboSite's data source
                foreach (DataRow row in captureSiteTable.Rows)
                {
                    siteID = int.Parse((row["SITE_ID"] ?? "-1").ToString());
                    text = (row["DESCRIPTION"] ?? "").ToString();

                    if (siteID == userCaptureSite)
                    {
                        text += " (" + rm.GetString("Local") + ")";
                    }
                    else
                    {
                        text += " (" + rm.GetString("Remote") + ")";
                    }

                    dataSource.Add(new ComboboxItem() { Text = text, Value = siteID });
                }
            }

            this.cboSite.SelectedValueChanged -= new System.EventHandler(this.cboSite_SelectedValueChanged);

            //For traces   
            logger.Debug(location + "Before line: cboSite.DataSource = dataSource;");

            cboSite.DataSource = dataSource;
            cboSite.DisplayMember = "Text";
            cboSite.ValueMember = "Value";

            this.cboSite.SelectedValueChanged += new System.EventHandler(this.cboSite_SelectedValueChanged);

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Fills the cboJob object with values
        private void fillCboJob()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Fills the cboJob object with values");

            var dataSource = new List<ComboboxItem>();
            int calendarId;
            string idDesc;

            //For traces   
            logger.Debug(location + "Before line: JobCalendarsDef jcd = new JobCalendarsDef(btecDB);");
            logger.Debug(location + "        and: DataTable jobTable = jcd.getAllDescription();");

            JobCalendarsDef jcd = new JobCalendarsDef(btecDB);
            DataTable jobTable = jcd.getAllDescription();

            if (jobTable != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in jobTable.Rows)");

                //Fills the cboJob's data source
                foreach (DataRow row in jobTable.Rows)
                {
                    calendarId = int.Parse((row["CALENDAR_ID"] ?? "-1").ToString());
                    idDesc = (row["ID_DESC"] ?? "").ToString();

                    dataSource.Add(new ComboboxItem() { Text = idDesc, Value = calendarId });
                }

                enableJobButtons(true);
            }
            else
            {
                enableJobButtons(false);
            }
            
            this.cboJob.SelectedValueChanged -= new System.EventHandler(this.cboJob_SelectedValueChanged);

            //For traces   
            logger.Debug(location + "Before line: cboJob.DataSource = dataSource;");

            cboJob.DataSource = dataSource;
            cboJob.DisplayMember = "Text";
            cboJob.ValueMember = "Value";

            this.cboJob.SelectedValueChanged += new System.EventHandler(this.cboJob_SelectedValueChanged);

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Fills the cboSiteYear object with values
        private void fillCboSiteYear()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Fills the cboSiteYear object with values");

            var dataSource = new List<ComboboxItem>();
            int? siteID = (int?)cboSite.SelectedValue;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  siteID-> " + siteID.ToString());

            //For traces   
            logger.Debug(location + "Before line: if (siteID != null)");

            if (siteID != null)
            {
                //For traces   
                logger.Debug(location + " After line: if (siteID != null)");

                string strYear;
                int intYear;

                //For traces   
                logger.Debug(location + "Before line: CalendarTable ct = new CalendarTable(btecDB);");
                logger.Debug(location + "        and: siteYearTable = ct.getYears((int)siteID);");

                CalendarTable ct = new CalendarTable(btecDB);
                siteYearTable = ct.getYears((int)siteID);

                if (siteYearTable != null)
                {
                    //For traces   
                    logger.Debug(location + "Before line: foreach (DataRow row in siteYearTable.Rows)");

                    foreach (DataRow row in siteYearTable.Rows)
                    {
                        strYear = (row["YEAR"] ?? "-1").ToString();
                        intYear = int.Parse(strYear);

                        dataSource.Add(new ComboboxItem() { Text = strYear, Value = intYear });
                    }
                }
            }

            this.cboSiteYear.SelectedValueChanged -= new System.EventHandler(this.cboSiteYear_SelectedValueChanged);

            //For traces   
            logger.Debug(location + "Before line: cboSiteYear.DataSource = dataSource;");

            cboSiteYear.DataSource = dataSource;
            cboSiteYear.DisplayMember = "Text";
            cboSiteYear.ValueMember = "Value";

            this.cboSiteYear.SelectedValueChanged += new System.EventHandler(this.cboSiteYear_SelectedValueChanged);

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Fills the cboJobYear object with values
        private void fillCboJobYear()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Fills the cboJobYear object with values");

            var dataSource = new List<ComboboxItem>();
            int? jobID = (int?)cboJob.SelectedValue;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  jobID-> " + jobID.ToString());

            //For traces   
            logger.Debug(location + "Before line: if (jobID != null)");

            if (jobID != null)
            {
                //For traces   
                logger.Debug(location + " After line: if (jobID != null)");

                string strYear;
                int intYear;

                //For traces   
                logger.Debug(location + "Before line: JobCalendars jc = new JobCalendars(btecDB);");
                logger.Debug(location + "        and: jobYearTable = jc.getYears((int)jobID);");

                JobCalendars jc = new JobCalendars(btecDB);
                jobYearTable = jc.getYears((int)jobID);

                if (jobYearTable != null)
                {
                    //For traces   
                    logger.Debug(location + "Before line: foreach (DataRow row in jobYearTable.Rows)");

                    foreach (DataRow row in jobYearTable.Rows)
                    {
                        strYear = (row["YEAR"] ?? "-1").ToString();
                        intYear = int.Parse(strYear);

                        dataSource.Add(new ComboboxItem() { Text = strYear, Value = intYear });
                    }
                }
            }

            this.cboJobYear.SelectedValueChanged -= new System.EventHandler(this.cboJobYear_SelectedValueChanged);

            //For traces   
            logger.Debug(location + "Before line: cboJobYear.DataSource = dataSource;");

            cboJobYear.DataSource = dataSource;
            cboJobYear.DisplayMember = "Text";
            cboJobYear.ValueMember = "Value";

            this.cboJobYear.SelectedValueChanged += new System.EventHandler(this.cboJobYear_SelectedValueChanged);

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Allows to fill the DisplayMember and ValueMember properties of some combo box
        private class ComboboxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }
        }

        //Occurs when the cboSite selected value is changed
        private void cboSite_SelectedValueChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the cboSite selected value is changed");

                int? selectedValue = (int?)cboSite.SelectedValue;

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  selectedValue-> " + selectedValue.ToString());

                //For traces   
                logger.Debug(location + "Before line: if (currentValueCboSite != selectedValue)");

                if (currentValueCboSite != selectedValue)
                {
                    //For traces   
                    logger.Debug(location + " After line: if (currentValueCboSite != selectedValue)");

                    //For traces   
                    logger.Debug(location + "Before line: askForSaveSite(rm.GetString(\"msgSaveChanges3\"), MessageBoxButtons.YesNo);");

                    askForSaveSite(rm.GetString("msgSaveChanges3"), MessageBoxButtons.YesNo);

                    //For traces   
                    logger.Debug(location + "Before line: fillCboSiteYear();");

                    fillCboSiteYear();

                    int? lastCurrentValueCboSite = currentValueCboSite;

                    currentValueCboSite = selectedValue;

                    isCboSiteChanged = true;

                    if (assignYearCboSite() == false)
                    {
                        //For traces   
                        logger.Debug(location + " After line: if (assignYearCboSite() == false)");

                        currentValueCboSite = lastCurrentValueCboSite;
                        cboSite.SelectedValue = currentValueCboSite;

                        //For traces   
                        logger.Debug(location + "Before line: fillCboSiteYear();");

                        fillCboSiteYear();

                        //For traces   
                        logger.Debug(location + "Before line: assignYearCboSite();");
                        
                        assignYearCboSite();

                        tabCalendar.Focus();
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

        //Occurs when the cboSiteMonth selected value is changed
        private void cboSiteMonth_SelectedValueChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the cboSiteMonth selected value is changed");
                
                int? selectedValue = (int?)cboSiteMonth.SelectedValue;

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  selectedValue-> " + selectedValue.ToString());

                //For traces   
                logger.Debug(location + "Before line: if (currentValueCboSiteMonth != selectedValue)");

                if (currentValueCboSiteMonth != selectedValue)
                {
                    currentValueCboSiteMonth = selectedValue;

                    if (currentValueCboSiteYear != null)
                    {
                        //For traces   
                        logger.Debug(location + "Before line: displayMonth(siteCalendar,...);");

                        displayMonth(siteCalendar,
                                         currentValueCboSiteYear,
                                         currentValueCboSiteMonth,
                                         TAB_SITE);

                        //For traces   
                        logger.Debug(location + "Before line: selectDefaultDay(siteCalendar, currentValueCboSiteMonth);");

                        selectDefaultDay(siteCalendar, currentValueCboSiteMonth);
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

        //Occurs when the cboSiteYear selected value is changed
        private void cboSiteYear_SelectedValueChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the cboSiteYear selected value is changed");
                
                int? selectedYear = (int?)cboSiteYear.SelectedValue;
                int? selectedMonth = (int?)cboSiteMonth.SelectedValue;

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  selectedYear-> " + selectedYear.ToString());
                logger.Debug(location + "  selectedMonth-> " + selectedMonth.ToString());

                //For traces   
                logger.Debug(location + "Before line: if (currentValueCboSiteYear != selectedYear || isCboSiteChanged)");

                if (currentValueCboSiteYear != selectedYear || isCboSiteChanged)
                {
                    //For traces   
                    logger.Debug(location + " After line: if (currentValueCboSiteYear != selectedYear || isCboSiteChanged)");

                    //For traces   
                    logger.Debug(location + "Before line: askForSaveSite(rm.GetString(\"msgSaveChanges3\"), MessageBoxButtons.YesNo);");

                    askForSaveSite(rm.GetString("msgSaveChanges3"), MessageBoxButtons.YesNo);

                    currentValueCboSiteYear = selectedYear;

                    //For traces   
                    logger.Debug(location + "Before line: initializeYear(TAB_SITE);");

                    initializeYear(TAB_SITE);

                    cboSiteMonth.SelectedValue = CURRENT_MONTH;

                    //Need this condition if the SelectedValueChanged event was not call
                    if (selectedMonth == (int?)cboSiteMonth.SelectedValue)
                    {
                        //For traces   
                        logger.Debug(location + "Before line: displayMonth(siteCalendar,...);");

                        displayMonth(siteCalendar,
                                     currentValueCboSiteYear,
                                     currentValueCboSiteMonth,
                                     TAB_SITE);

                        //For traces   
                        logger.Debug(location + "Before line: selectDefaultDay(siteCalendar, currentValueCboSiteMonth);");

                        selectDefaultDay(siteCalendar, currentValueCboSiteMonth);
                    }

                    isCboSiteChanged = false;
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

        //Asks the user whether he wants save the changes in the Site tab and saves if applicable
        private DialogResult askForSaveSite(string message, MessageBoxButtons buttons)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Asks the user whether he wants save the changes in the Site tab and saves if applicable");

            DialogResult result = DialogResult.None;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  isSomethingChangedSite-> " + isSomethingChangedSite.ToString());

            if (isSomethingChangedSite)
            {
                //For traces   
                logger.Debug(location + " After line: if (isSomethingChangedSite)");

                //For traces   
                result = MessageBox.Show(message,
                                         rm.GetString("titleSaveChanges2"),
                                         buttons,
                                         MessageBoxIcon.None);

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  result-> " + result.ToString());

                switch (result)
                {
                    case DialogResult.OK:
                    case DialogResult.Yes:
                        //For traces   
                        logger.Debug(location + "Before line: transfertYear(siteCalendarOriginalTable,,...);");

                        transfertYear(siteCalendarOriginalTable,
                                      siteCalendarCustomTable,
                                      currentValueCboSiteYear,
                                      currentValueCboSite,
                                      TAB_SITE);

                        //For traces   
                        logger.Debug(location + "Before line: saveYear(siteCalendarOriginalTable, TAB_SITE);");

                        saveYear(siteCalendarOriginalTable, TAB_SITE);
                        
                        enableAttributes(TAB_SITE, false);
                        break;
                    case DialogResult.Cancel:
                        break;
                    case DialogResult.No:
                        enableAttributes(TAB_SITE, false);
                        break;
                }
            }

            //For traces
            logger.Debug(location + " Ending...");

            return result;
        }

        //Asks the user whether he wants save the changes in the Job tab and saves if applicable
        private DialogResult askForSaveJob(string message, MessageBoxButtons buttons)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Asks the user whether he wants save the changes in the Job tab and saves if applicable");

            DialogResult result = DialogResult.None;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  isSomethingChangedJob-> " + isSomethingChangedJob.ToString());

            if (isSomethingChangedJob)
            {
                //For traces   
                result = MessageBox.Show(message,
                                         rm.GetString("titleSaveChanges2"),
                                         buttons,
                                         MessageBoxIcon.None);

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  result-> " + result.ToString());

                switch (result)
                {
                    case DialogResult.OK:
                    case DialogResult.Yes:
                        //For traces   
                        logger.Debug(location + "Before line: transfertYear(jobCalendarOriginalTable,...);");

                        transfertYear(jobCalendarOriginalTable,
                                      jobCalendarCustomTable,
                                      currentValueCboJobYear,
                                      currentValueCboJob,
                                      TAB_JOB);

                        //For traces   
                        logger.Debug(location + "Before line: saveYear(jobCalendarOriginalTable, TAB_JOB);");

                        saveYear(jobCalendarOriginalTable, TAB_JOB);

                        enableAttributes(TAB_JOB, false);
                        break;
                    case DialogResult.Cancel:
                        break;
                    case DialogResult.No:
                        enableAttributes(TAB_JOB, false);
                        break;
                }
            }

            //For traces
            logger.Debug(location + " Ending...");

            return result;
        }

        //Initializes a calendar custom data table with new values from the database
        private void initializeYear(string tab)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Initializes a calendar custom data table with new values from the database");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  tab-> " + tab);

            switch (tab)
            {
                case TAB_SITE:
                    //For traces   
                    logger.Debug(location + "Before line: CalendarTable ct = new CalendarTable(btecDB);");
                    logger.Debug(location + "        and: siteCalendarCustomTable = ct.getCalendar(currentValueCboSiteYear, currentValueCboSite);");

                    CalendarTable ct = new CalendarTable(btecDB);
                    siteCalendarCustomTable = ct.getCalendar(currentValueCboSiteYear, currentValueCboSite);

                    enableAttributes(TAB_SITE, false);
                    break;
                case TAB_JOB:
                    //For traces   
                    logger.Debug(location + "Before line: JobCalendars jc = new JobCalendars(btecDB);");
                    logger.Debug(location + "        and: jobCalendarCustomTable = jc.getCalendar(currentValueCboJobYear, currentValueCboJob);");

                    JobCalendars jc = new JobCalendars(btecDB);
                    jobCalendarCustomTable = jc.getCalendar(currentValueCboJobYear, currentValueCboJob);

                    enableAttributes(TAB_JOB, false);
                    break;
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Enable some attributes to True or False for a specific tab
        private void enableAttributes(string tab, bool value)
        {
            switch (tab)
            {
                case TAB_SITE:
                    isSomethingChangedSite = value;

                    btnSiteCancel.Enabled = value;
                    btnSiteSave.Enabled = value;
                    break;
                case TAB_JOB:
                    isSomethingChangedJob = value;

                    btnJobCancel.Enabled = value;
                    btnJobSave.Enabled = value;
                    break;
            }
        }

        //Enable some buttons to True or False for the Job tab
        private void enableJobButtons(bool isEnabled)
        {
            btnJobAddYear.Enabled = isEnabled;
            btnJobEdit.Enabled = isEnabled;
            btnJobRemove.Enabled = isEnabled;
            btnJobCopy.Enabled = isEnabled;
            btnJobJobs.Enabled = isEnabled;
        }

        //Displays a month for a specific year in a calendar
        private void displayMonth(CustomCalendar calendar,
                                  int? currentValueCboYear,
                                  int? currentValueCboMonth,
                                  string tab)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Displays a month for a specific year in a calendar");

            //For traces   
            logger.Debug(location + "Before line: calendar.clean();");
        
            DrawingControl.SuspendDrawing(this);

            calendar.clean();

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  currentValueCboYear-> " + currentValueCboYear.ToString());
            logger.Debug(location + "  currentValueCboMonth-> " + currentValueCboMonth.ToString());

            if (currentValueCboYear != null)
            {
                //For traces   
                logger.Debug(location + " After line: if (currentValueCboYear != null)");

                //For traces   
                logger.Debug(location + "Before line: calendar.initializeMonth((int)currentValueCboYear, (int)currentValueCboMonth);");

                calendar.initializeMonth((int)currentValueCboYear,
                                         (int)currentValueCboMonth);
            }

            string field = "";
            DataRow[] monthRows = null;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  tab-> " + tab);

            switch (tab)
            {
                case TAB_SITE:
                    field = "WORKING_DAY";
                    monthRows = siteCalendarCustomTable.Select("CALENDAR_MONTH = " + ((int)currentValueCboSiteMonth).ToString());
                    break;
                case TAB_JOB:
                    field = "POSTING_DAY";
                    monthRows = jobCalendarCustomTable.Select("CALENDAR_MONTH = " + ((int)currentValueCboJobMonth).ToString());
                    break;
            }

            if (monthRows != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in monthRows)");

                //Displays all holidays
                foreach (DataRow row in monthRows)
                {
                    if (Convert.ToInt32(row[field]) == 0)
                    {
                        calendar.setNormalDay(Convert.ToInt32(row["CALENDAR_DAY"]), false);
                    }
                }
            }

            DrawingControl.ResumeDrawing(this);

            tabCalendar.Focus();

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Used to suspend drawing controls temporarily and so reduce the delay
        class DrawingControl
        {
            [DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

            private const int WM_SETREDRAW = 11;

            public static void SuspendDrawing(Control parent)
            {
                SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
            }

            public static void ResumeDrawing(Control parent)
            {
                SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
                parent.Refresh();
            }
        }

        //Updates the Site day state (normal day or not) of a day
        public void updateDay(int day)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Updates the Site day state (normal day or not) of a day");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  currentTab-> " + currentTab);
            logger.Debug(location + "  day-> " + day.ToString());

            switch (currentTab)
            {
                case TAB_SITE:
                    if (siteCalendar.isNormalDay(day) == true)
                    {
                        siteCalendar.setNormalDay(day, false);
                        updateSiteCalendarTable(day, false);
                    }
                    else
                    {
                        siteCalendar.setNormalDay(day, true);
                        updateSiteCalendarTable(day, true);
                    }

                    enableAttributes(TAB_SITE, true);
                    break;
                case TAB_JOB:
                    if (jobCalendar.isNormalDay(day) == true)
                    {
                        jobCalendar.setNormalDay(day, false);
                        updateJobCalendarTable(day, false);
                    }
                    else
                    {
                        jobCalendar.setNormalDay(day, true);
                        updateJobCalendarTable(day, true);
                    }

                    enableAttributes(TAB_JOB, true);
                    break;
            }

            tabCalendar.Focus();

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Updates the siteCalendarCustomTable data table for a specific day
        private void updateSiteCalendarTable(int day, bool isWorkingDay)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Updates the siteCalendarCustomTable data table for a specific day");

            DataRow row = siteCalendarCustomTable.Select("CALENDAR_MONTH = " + ((int)currentValueCboSiteMonth).ToString() +
                                                         " and CALENDAR_DAY = " + day.ToString()).First();

            row["WORKING_DAY"] = (isWorkingDay ? 1 : 0);

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Updates the jobCalendarCustomTable data table for a specific day
        private void updateJobCalendarTable(int day, bool isPostingDay)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Updates the jobCalendarCustomTable data table for a specific day");

            DataRow row = jobCalendarCustomTable.Select("CALENDAR_MONTH = " + ((int)currentValueCboJobMonth).ToString() +
                                                        " and CALENDAR_DAY = " + day.ToString()).First();

            row["POSTING_DAY"] = (isPostingDay ? 1 : 0);

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Occurs on the KeyDown event of the tabCalendar object
        private void tabCalendar_KeyDown(object sender, KeyEventArgs e)
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
                logger.Info(location + "Purpose: Occurs on the KeyDown event of the tabCalendar object");

                if (tabCalendar.Focused)
                {
                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  e.KeyCode-> " + e.KeyCode.ToString());

                    bool isKeyEventHandled = false;

                    if (tabCalendar.SelectedTab == tabCalendar.TabPages[TAB_SITE] && 
                        currentValueCboSiteYear != null)
                    {
                        //For traces   
                        logger.Debug(location + " After line: if (tabCalendar.SelectedTab == tabCalendar.TabPages[TAB_SITE] && currentValueCboSiteYear != null)");

                        isKeyEventHandled = handleKeyEvent(siteCalendar,
                                                           cboSiteMonth,
                                                           e);
                    }
                    else if (tabCalendar.SelectedTab == tabCalendar.TabPages[TAB_JOB] &&
                             currentValueCboJobYear != null)
                    {
                        //For traces   
                        logger.Debug(location + " After line: else if (tabCalendar.SelectedTab == tabCalendar.TabPages[TAB_JOB] && currentValueCboJobYear != null)");

                        isKeyEventHandled = handleKeyEvent(jobCalendar,
                                                           cboJobMonth,
                                                           e);
                    }

                    if (isKeyEventHandled) e.Handled = true;
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

        //Makes the user interact with a calendar according to keyboard events
        private bool handleKeyEvent(CustomCalendar calendar,
                                    ComboBox cboMonth,
                                    KeyEventArgs e)
        {
            int selectedDay = calendar.getDay();
            int selectedMonth = calendar.getMonth();
            int selectedYear = calendar.getYear();
            int newDay;
            int maxDay;

            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Makes the user interact with a calendar according to keyboard events");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  e.KeyCode-> " + e.KeyCode.ToString());

            //For traces   
            logger.Debug(location + "Before line: switch (e.KeyCode)");

            switch (e.KeyCode)
            {
                case Keys.Space:
                    //For traces   
                    logger.Debug(location + "Before line: updateDay(selectedDay);");

                    updateDay(selectedDay);

                    return true;
                    break;
                case Keys.Down:
                    if (calendar.selectDay(selectedDay + 7) == null)
                    {
                        if (selectedMonth < 12)
                        {
                            cboMonth.SelectedValue = selectedMonth + 1;

                            maxDay = calendar.getNbDaysInMonth(selectedYear, selectedMonth);
                            newDay = 7 - (maxDay - selectedDay);

                            //For traces   
                            logger.Debug(location + "Before line: calendar.selectDay(newDay);");

                            calendar.selectDay(newDay);
                        }
                    }

                    return true;
                    break;
                case Keys.Up:
                    if (calendar.selectDay(selectedDay - 7) == null)
                    {
                        if (selectedMonth > 1)
                        {
                            cboMonth.SelectedValue = selectedMonth - 1;

                            maxDay = calendar.getNbDaysInMonth(selectedYear, selectedMonth - 1);
                            newDay = (selectedDay - 7) + maxDay;

                            //For traces   
                            logger.Debug(location + "Before line: calendar.selectDay(newDay);");

                            calendar.selectDay(newDay);
                        }
                    }

                    return true;
                    break;
                case Keys.Left:
                    if (calendar.selectDay(selectedDay - 1) == null)
                    {
                        if (selectedMonth > 1)
                        {
                            cboMonth.SelectedValue = selectedMonth - 1;

                            maxDay = calendar.getNbDaysInMonth(selectedYear, selectedMonth - 1);

                            //For traces   
                            logger.Debug(location + "Before line: calendar.selectDay(maxDay);");
                            
                            calendar.selectDay(maxDay);
                        }
                    }

                    return true;
                    break;
                case Keys.Right:
                    if (calendar.selectDay(selectedDay + 1) == null)
                    {
                        if (selectedMonth < 12)
                        {
                            cboMonth.SelectedValue = selectedMonth + 1;

                            //For traces   
                            logger.Debug(location + "Before line: calendar.selectDay(1);");

                            calendar.selectDay(1);
                        }
                    }

                    return true;
                    break;
            }

            //For traces
            logger.Debug(location + " Ending...");

            return false;
        }

        //Occurs when the [Cancel] site button is clicked
        private void btnSiteCancel_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Cancel] site button is clicked");

                //For traces   
                logger.Debug(location + "Before line: initializeYear(TAB_SITE);");

                initializeYear(TAB_SITE);

                int selectedDay = siteCalendar.getDay();

                //For traces   
                logger.Debug(location + "Before line: displayMonth(siteCalendar,...);");

                displayMonth(siteCalendar,
                                currentValueCboSiteYear,
                                currentValueCboSiteMonth,
                                TAB_SITE);

                //For traces   
                logger.Debug(location + "Before line: siteCalendar.selectDay(selectedDay);");

                siteCalendar.selectDay(selectedDay);

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

        //Occurs when the [Save] site button is clicked
        private void btnSiteSave_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Save] site button is clicked");

                //For traces   
                logger.Debug(location + "Before line: askForSaveSite(rm.GetString(\"msgSaveChanges2\"), MessageBoxButtons.OKCancel);");

                askForSaveSite(rm.GetString("msgSaveChanges2"), MessageBoxButtons.OKCancel);

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

        //Occurs when a tab page is being selected
        private void tabCalendar_Selecting(object sender, TabControlCancelEventArgs e)
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
                logger.Info(location + "Purpose: Occurs when a tab page is being selected");

                bool isInitialize = false;

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  e.TabPage-> " + e.TabPage.ToString());

                if (e.TabPage == tabCalendar.TabPages[TAB_SITE])
                {
                    //For traces   
                    logger.Debug(location + "Before line: isInitialize = initializeTab(siteCalendar,...);");

                    isInitialize = initializeTab(siteCalendar,
                                                 currentValueCboSiteYear,
                                                 currentValueCboSiteMonth,
                                                 TAB_SITE);
                }
                else if (e.TabPage == tabCalendar.TabPages[TAB_JOB])
                {
                    //For traces   
                    logger.Debug(location + "Before line: isInitialize = initializeTab(jobCalendar,...);");

                    isInitialize = initializeTab(jobCalendar,
                                                 currentValueCboJobYear,
                                                 currentValueCboJobMonth,
                                                 TAB_JOB);
                }

                if (isInitialize == false) e.Cancel = true;

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

        //Initializes the selected tab if applicable
        private bool initializeTab(CustomCalendar calendar,
                                   int? currentValueCboYear,
                                   int? currentValueCboMonth,
                                   string tab)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Initializes the selected tab if applicable");

            DialogResult result = DialogResult.None;

            string oldTab = currentTab;
            currentTab = tab;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  oldTab-> " + oldTab);

            //For traces   
            logger.Debug(location + "Before line: switch (oldTab)");

            switch (oldTab)
            {
                case TAB_SITE:
                    result = askForSaveSite(rm.GetString("msgSaveChanges3"), MessageBoxButtons.YesNoCancel);
                    break;
                case TAB_JOB:
                    result = askForSaveJob(rm.GetString("msgSaveChanges3"), MessageBoxButtons.YesNoCancel);
                    break;
            }

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  result-> " + result.ToString());

            //For traces   
            logger.Debug(location + "Before line: switch (result)");

            switch (result)
            {
                case DialogResult.No:
                case DialogResult.OK:
                case DialogResult.Yes:
                    //For traces   
                    logger.Debug(location + "Before line: initializeYear(tab);");

                    initializeYear(tab);

                    //For traces   
                    logger.Debug(location + "Before line: displayMonth(calendar,...);");

                    displayMonth(calendar,
                                 currentValueCboYear,
                                 currentValueCboMonth,
                                 tab);

                    //For traces   
                    logger.Debug(location + "Before line: selectDefaultDay(calendar, currentValueCboMonth);");

                    selectDefaultDay(calendar, currentValueCboMonth);
                    break;
                case DialogResult.Cancel:
                    currentTab = oldTab;

                    return false;
                    break;
            }

            //For traces
            logger.Debug(location + " Ending...");

            return true;
        }

        //Occurs when the [Add year] site button is clicked
        private void btnSiteAddYear_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Add year] site button is clicked");

                //For traces   
                logger.Debug(location + "Before line: frmCalendarAddYear _frm = new frmCalendarAddYear(this, btecDB, TAB_SITE);");
                logger.Debug(location + "        and: DialogResult result = _frm.ShowDialog(this);");

                //Opens the child form containing for adding a year
                frmCalendarAddYear _frm = new frmCalendarAddYear(this, btecDB, TAB_SITE);

                DialogResult result = _frm.ShowDialog(this);
                _frm.Dispose();

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  result-> " + result.ToString());

                if (result == DialogResult.OK)
                {
                    //For traces   
                    logger.Debug(location + " After line: if (result == DialogResult.OK)");

                    //For traces   
                    logger.Debug(location + "Before line: createSiteYear(newYear);");

                    createSiteYear(newYear);

                    //For traces   
                    logger.Debug(location + "Before line: saveYear(siteCalendarOriginalTable, TAB_SITE);");
                
                    saveYear(siteCalendarOriginalTable, TAB_SITE);

                    //For traces   
                    logger.Debug(location + "Before line: fillCboSiteYear();");

                    fillCboSiteYear();

                    this.cboSiteYear.SelectedValueChanged -= new System.EventHandler(this.cboSiteYear_SelectedValueChanged);
                    cboSiteYear.SelectedValue = newYear;
                    this.cboSiteYear.SelectedValueChanged += new System.EventHandler(this.cboSiteYear_SelectedValueChanged);

                    currentValueCboSiteYear = (int?)cboSiteYear.SelectedValue;

                    //For traces   
                    logger.Debug(location + "Before line: initializeYear(TAB_SITE);");

                    initializeYear(TAB_SITE);

                    int? selectedMonth = (int?)cboSiteMonth.SelectedValue;
                    cboSiteMonth.SelectedValue = CURRENT_MONTH;

                    //Need this condition if the SelectedValueChanged event was not call
                    if (selectedMonth == (int?)cboSiteMonth.SelectedValue)
                    {
                        //For traces   
                        logger.Debug(location + "Before line: displayMonth(siteCalendar,...);");

                        displayMonth(siteCalendar,
                                     currentValueCboSiteYear,
                                     currentValueCboSiteMonth,
                                     TAB_SITE);

                        //For traces   
                        logger.Debug(location + "Before line: selectDefaultDay(siteCalendar, currentValueCboSiteMonth);");

                        selectDefaultDay(siteCalendar, currentValueCboSiteMonth);
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

        //Occurs when the form is closed
        private void frmCalendar_FormClosing(object sender, FormClosingEventArgs e)
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
                logger.Debug(location + "  isSomethingChangedSite-> " + isSomethingChangedSite.ToString());
                logger.Debug(location + "  isSomethingChangedJob-> " + isSomethingChangedJob.ToString());

                if (isSomethingChangedSite || isSomethingChangedJob)
                {
                    //For traces
                    logger.Debug(location + "  isFormClosedByUser-> " + isFormClosedByUser.ToString());

                    //If the form does not ever be closed by the user
                    if (!isFormClosedByUser)
                    {
                        //For traces   
                        logger.Debug(location + " After line: if (!isFormClosedByUser)");

                        DialogResult result = MessageBox.Show(rm.GetString("MsgQuitWithoutSaving"), 
                                                              rm.GetString("titleQuitWithoutSaving"),
                                                              MessageBoxButtons.YesNo,
                                                              MessageBoxIcon.None);

                        //For traces
                        logger.Debug(location + "Values:");
                        logger.Debug(location + "  result-> " + result.ToString());

                        switch (result)
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

         //Occurs when the [Close] site button is clicked
        private void btnSiteClose_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Close] site button is clicked");

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  isSomethingChangedSite-> " + isSomethingChangedSite.ToString());

                if (isSomethingChangedSite)
                {
                    //For traces   
                    logger.Debug(location + " After line: if (isSomethingChangedSite)");

                    DialogResult result = MessageBox.Show(rm.GetString("MsgQuitWithoutSaving"),
                                                          rm.GetString("titleQuitWithoutSaving"),
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.None);

                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  result-> " + result.ToString());

                    switch (result)
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
                    logger.Debug(location + " After line: if (isSomethingChangedSite)...else");

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

        //Occurs when the [Jobs] job button is clicked
        private void btnJobJobs_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Jobs] job button is clicked");

                //For traces   
                logger.Debug(location + "Before line: frmCalendarAssign _frm = new frmCalendarAssign(btecDB);");
                logger.Debug(location + "        and: DialogResult result = _frm.ShowDialog(this);");

                //Opens the child form (next 2 lines)
                frmCalendarAssign _frm = new frmCalendarAssign(btecDB, (int)currentValueCboJob);

                DialogResult result = _frm.ShowDialog(this);
                _frm.Dispose();

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

        //Occurs when the cboJobMonth selected value is changed
        private void cboJobMonth_SelectedValueChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the cboJobMonth selected value is changed");
                
                int? selectedValue = (int?)cboJobMonth.SelectedValue;

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  selectedValue-> " + selectedValue.ToString());

                //For traces   
                logger.Debug(location + "Before line: if (currentValueCboJobMonth != selectedValue)");

                if (currentValueCboJobMonth != selectedValue)
                {
                    currentValueCboJobMonth = selectedValue;

                    if (currentValueCboJobYear != null)
                    {
                        //For traces   
                        logger.Debug(location + "Before line: displayMonth(jobCalendar,...);");

                        displayMonth(jobCalendar,
                                     currentValueCboJobYear,
                                     currentValueCboJobMonth,
                                     TAB_JOB);

                        //For traces   
                        logger.Debug(location + "Before line: selectDefaultDay(jobCalendar, currentValueCboJobMonth);");

                        selectDefaultDay(jobCalendar, currentValueCboJobMonth);
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

        //Occurs when the cboJob selected value is changed
        private void cboJob_SelectedValueChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the cboJob selected value is changed");

                int? selectedValue = (int?)cboJob.SelectedValue;

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  selectedValue-> " + selectedValue.ToString());

                //For traces   
                logger.Debug(location + "Before line: if (currentValueCboJob != selectedValue)");

                if (currentValueCboJob != selectedValue)
                {
                    //For traces   
                    logger.Debug(location + " After line: if (currentValueCboJob != selectedValue)");

                    //For traces   
                    logger.Debug(location + "Before line: askForSaveJob(rm.GetString(\"msgSaveChanges3\"), MessageBoxButtons.YesNo);");

                    askForSaveJob(rm.GetString("msgSaveChanges3"), MessageBoxButtons.YesNo);

                    //For traces   
                    logger.Debug(location + "Before line: fillCboJobYear();");

                    fillCboJobYear();

                    int? lastCurrentValueCboJob = currentValueCboJob;

                    currentValueCboJob = selectedValue;

                    if (currentValueCboJob != null)
                    {
                        enableJobButtons(true);
                    }
                    else
                    {
                        enableJobButtons(false);
                    }

                    isCboJobChanged = true;

                    //For traces   
                    logger.Debug(location + "Before line: assignYearCboJob();");

                    assignYearCboJob();
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

        //Occurs when the cboJobYear selected value is changed
        private void cboJobYear_SelectedValueChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the cboJobYear selected value is changed");

                int? selectedYear = (int?)cboJobYear.SelectedValue;
                int? selectedMonth = (int?)cboJobMonth.SelectedValue;

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  selectedYear-> " + selectedYear.ToString());
                logger.Debug(location + "  selectedMonth-> " + selectedMonth.ToString());

                //For traces   
                logger.Debug(location + "Before line: if (currentValueCboJobYear != selectedYear || isCboJobChanged)");

                if (currentValueCboJobYear != selectedYear || isCboJobChanged)
                {
                    //For traces   
                    logger.Debug(location + " After line: if (currentValueCboJobYear != selectedYear || isCboJobChanged)");

                    //For traces   
                    logger.Debug(location + "Before line: askForSaveJob(rm.GetString(\"msgSaveChanges3\"), MessageBoxButtons.YesNo);");

                    askForSaveJob(rm.GetString("msgSaveChanges3"), MessageBoxButtons.YesNo);

                    currentValueCboJobYear = selectedYear;

                    //For traces   
                    logger.Debug(location + "Before line: initializeYear(TAB_JOB);");

                    initializeYear(TAB_JOB);

                    cboJobMonth.SelectedValue = CURRENT_MONTH;

                    //Need this condition if the SelectedValueChanged event was not call
                    if (selectedMonth == (int?)cboJobMonth.SelectedValue)
                    {
                        //For traces   
                        logger.Debug(location + "Before line: displayMonth(jobCalendar,...);");

                        displayMonth(jobCalendar,
                                     currentValueCboJobYear,
                                     currentValueCboJobMonth,
                                     TAB_JOB);

                        //For traces   
                        logger.Debug(location + "Before line: selectDefaultDay(jobCalendar, currentValueCboJobMonth);");

                        selectDefaultDay(jobCalendar, currentValueCboJobMonth);
                    }

                    isCboJobChanged = false;
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

        //Occurs when the [Add year] job button is clicked
        private void btnJobAddYear_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Add year] job button is clicked");

                //For traces   
                logger.Debug(location + "Before line: frmCalendarAddYear _frm = new frmCalendarAddYear(this, btecDB, TAB_JOB);");
                logger.Debug(location + "        and: DialogResult result = _frm.ShowDialog(this);");

                //Opens the child form containing for adding a year
                frmCalendarAddYear _frm = new frmCalendarAddYear(this, btecDB, TAB_JOB);

                DialogResult result = _frm.ShowDialog(this);
                _frm.Dispose();

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  result-> " + result.ToString());

                if (result == DialogResult.OK)
                {
                    //For traces   
                    logger.Debug(location + " After line: if (result == DialogResult.OK)");

                    //For traces   
                    logger.Debug(location + "Before line: createJobYear(newYear);");

                    createJobYear(newYear);

                    //For traces   
                    logger.Debug(location + "Before line: saveYear(jobCalendarOriginalTable, TAB_JOB);");
                
                    saveYear(jobCalendarOriginalTable, TAB_JOB);

                    //For traces   
                    logger.Debug(location + "Before line: fillCboJobYear();");

                    fillCboJobYear();

                    this.cboJobYear.SelectedValueChanged -= new System.EventHandler(this.cboJobYear_SelectedValueChanged);
                    cboJobYear.SelectedValue = newYear;
                    this.cboJobYear.SelectedValueChanged += new System.EventHandler(this.cboJobYear_SelectedValueChanged);

                    currentValueCboJobYear = (int?)cboJobYear.SelectedValue;

                    //For traces   
                    logger.Debug(location + "Before line: initializeYear(TAB_JOB);");

                    initializeYear(TAB_JOB);

                    int? selectedMonth = (int?)cboJobMonth.SelectedValue;
                    cboJobMonth.SelectedValue = CURRENT_MONTH;

                    //Need this condition if the SelectedValueChanged event was not call
                    if (selectedMonth == (int?)cboJobMonth.SelectedValue)
                    {
                        //For traces   
                        logger.Debug(location + "Before line: displayMonth(jobCalendar,...);");

                        displayMonth(jobCalendar,
                                     currentValueCboJobYear,
                                     currentValueCboJobMonth,
                                     TAB_JOB);

                        //For traces   
                        logger.Debug(location + "Before line: selectDefaultDay(jobCalendar, currentValueCboJobMonth);");

                        selectDefaultDay(jobCalendar, currentValueCboJobMonth);
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

        //Occurs when the [Save] job button is clicked
        private void btnJobSave_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Save] job button is clicked");

                //For traces   
                logger.Debug(location + "Before line: askForSaveJob(rm.GetString(\"msgSaveChanges2\"), MessageBoxButtons.OKCancel);");

                askForSaveJob(rm.GetString("msgSaveChanges2"), MessageBoxButtons.OKCancel);

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

        //Occurs when the [Cancel] job button is clicked
        private void btnJobCancel_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Cancel] job button is clicked");

                //For traces   
                logger.Debug(location + "Before line: initializeYear(TAB_JOB);");

                initializeYear(TAB_JOB);

                int selectedDay = jobCalendar.getDay();

                //For traces   
                logger.Debug(location + "Before line: displayMonth(jobCalendar,...);");

                displayMonth(jobCalendar,
                                currentValueCboJobYear,
                                currentValueCboJobMonth,
                                TAB_JOB);

                //For traces   
                logger.Debug(location + "Before line: jobCalendar.selectDay(selectedDay);");

                jobCalendar.selectDay(selectedDay);

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

        //Occurs when the [Close] job button is clicked
        private void btnJobClose_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Close] job button is clicked");

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  isSomethingChangedJob-> " + isSomethingChangedJob.ToString());

                if (isSomethingChangedJob)
                {
                    //For traces   
                    logger.Debug(location + " After line: if (isSomethingChangedJob)");

                    DialogResult result = MessageBox.Show(rm.GetString("MsgQuitWithoutSaving"),
                                                          rm.GetString("titleQuitWithoutSaving"),
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.None);

                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  result-> " + result.ToString());

                    switch (result)
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
                    logger.Debug(location + " After line: if (isSomethingChangedJob)...else");

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

        //Occurs when the [Edit] job button is clicked
        private void btnJobEdit_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Edit] job button is clicked");

                //For traces   
                logger.Debug(location + "Before line: initializeFormIfDontSave();");

                initializeFormIfDontSave();

                //For traces   
                logger.Debug(location + "Before line: frmJobEdit _frm = new frmJobEdit(btecDB, ACTION_EDIT, currentValueCboJob);");
                logger.Debug(location + "        and: DialogResult result = _frm.ShowDialog(this);");

                //Opens the child form containing the job corresponding to the calendar Id (next 2 lines)
                frmCalendarEditJob _frm = new frmCalendarEditJob(btecDB, ACTION_EDIT, currentValueCboJob);

                DialogResult result = _frm.ShowDialog(this);
                _frm.Dispose();

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  result-> " + result.ToString());

                if (result == DialogResult.OK)
                {
                    //For traces   
                    logger.Debug(location + "Before line: fillCboJob();");

                    fillCboJob();

                    this.cboJob.SelectedValueChanged -= new System.EventHandler(this.cboJob_SelectedValueChanged);
                    cboJob.SelectedValue = currentValueCboJob;
                    this.cboJob.SelectedValueChanged += new System.EventHandler(this.cboJob_SelectedValueChanged);
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

        //Occurs when the [Add] job button is clicked
        private void btnJobAdd_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Add] job button is clicked");

                initializeFormIfDontSave();

                //For traces   
                logger.Debug(location + "Before line: frmJobEdit _frm = new frmJobEdit(btecDB, ACTION_ADD, null);");
                logger.Debug(location + "        and: DialogResult result = _frm.ShowDialog(this);");

                //Opens the child form containing the job corresponding to the calendar Id (next 2 lines)
                frmCalendarEditJob _frm = new frmCalendarEditJob(btecDB, ACTION_ADD, null);

                DialogResult result = _frm.ShowDialog(this);
                _frm.Dispose();

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  result-> " + result.ToString());

                if (result == DialogResult.OK)
                {
                    //For traces   
                    logger.Debug(location + "Before line: initializeNewJob();");

                    initializeNewJob();
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

        //Occurs when the [Copy] job button is clicked
        private void btnJobCopy_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Copy] job button is clicked");

                //For traces   
                logger.Debug(location + "Before line: initializeFormIfDontSave();");

                initializeFormIfDontSave();

                //For traces   
                logger.Debug(location + "Before line: frmJobEdit _frm = new frmJobEdit(btecDB, ACTION_COPY, currentValueCboJob);");
                logger.Debug(location + "        and: DialogResult result = _frm.ShowDialog(this);");

                //Opens the child form containing the job corresponding to the calendar Id (next 2 lines)
                frmCalendarEditJob _frm = new frmCalendarEditJob(btecDB, ACTION_COPY, currentValueCboJob);

                DialogResult result = _frm.ShowDialog(this);
                _frm.Dispose();

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  result-> " + result.ToString());

                if (result == DialogResult.OK)
                {
                    //For traces   
                    logger.Debug(location + "Before line: initializeNewJob();");

                    initializeNewJob();
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

        //Initializes the form with values from the database if the user don't want to save his changes
        private void initializeFormIfDontSave()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Initializes the form with values from the database if the user don't want to save his changes");

            //For traces   
            logger.Debug(location + "Before line: DialogResult ifSave = askForSaveJob(rm.GetString(\"msgSaveChanges3\"), MessageBoxButtons.YesNo);");

            DialogResult ifSave = askForSaveJob(rm.GetString("msgSaveChanges3"), MessageBoxButtons.YesNo);

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  ifSave-> " + ifSave.ToString());

            if (ifSave == DialogResult.No)
            {
                //For traces   
                logger.Debug(location + "Before line: initializeYear(TAB_JOB);");

                initializeYear(TAB_JOB);

                //For traces   
                logger.Debug(location + "Before line: displayMonth(jobCalendar,...);");

                displayMonth(jobCalendar,
                             currentValueCboJobYear,
                             currentValueCboJobMonth,
                             TAB_JOB);

                //For traces   
                logger.Debug(location + "Before line: selectDefaultDay(jobCalendar, currentValueCboJobMonth);");

                selectDefaultDay(jobCalendar, currentValueCboJobMonth);
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Initializes the form to display the new job calendar
        private void initializeNewJob()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Initializes the form to display the new job calendar");

            //For traces   
            logger.Debug(location + "Before line: fillCboJob();");

            fillCboJob();

            //For traces   
            logger.Debug(location + "Before line: JobCalendarsDef jcd = new JobCalendarsDef(btecDB);");
            logger.Debug(location + "        and: int newID = jcd.getMaxID();");

            JobCalendarsDef jcd = new JobCalendarsDef(btecDB);
            int newID = jcd.getMaxID();

            this.cboJob.SelectedValueChanged -= new System.EventHandler(this.cboJob_SelectedValueChanged);
            cboJob.SelectedValue = newID;
            this.cboJob.SelectedValueChanged += new System.EventHandler(this.cboJob_SelectedValueChanged);

            cboJob_SelectedValueChanged(null, null);

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Occurs when the [Remove] job button is clicked
        private void btnJobRemove_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Remove] job button is clicked");

                //displays an hourglass cursor
                Cursor.Current = Cursors.WaitCursor;

                string msgPart1 = rm.GetString("msgDeleteCalendar1of2") + "\n" + "\n";
                string msgPart2 = "\n" + "\n" + rm.GetString("msgDeleteCalendar2of2");

                //For traces   
                logger.Debug(location + "Before line: JobCalendarsDef jcd = new JobCalendarsDef(btecDB);");
                logger.Debug(location + "        and: string description = jcd.getDescription(currentValueCboJob);");

                JobCalendarsDef jcd = new JobCalendarsDef(btecDB);
                string description = jcd.getDescription(currentValueCboJob);

                string message = msgPart1 + currentValueCboJob.ToString() + " : " + description + msgPart2;

                DialogResult result = MessageBox.Show(message,
                                                      rm.GetString("titleDeleteOneCalendar"),
                                                      MessageBoxButtons.OKCancel,
                                                      MessageBoxIcon.Exclamation);

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  result-> " + result.ToString());

                switch (result)
                {
                    case DialogResult.OK:
                        //For traces   
                        logger.Debug(location + "Before line: AdminStatementDefinition asd = new AdminStatementDefinition(btecDB);");

                        AdminStatementDefinition asd = new AdminStatementDefinition(btecDB);

                        if (asd.isJobCalendarIdInUse(currentValueCboJob))
                        {
                            //For traces   
                            logger.Debug(location + " After line: if (asd.isJobCalendarIdInUse(currentValueCboJob))");

                            DialogResult result2 = MessageBox.Show(rm.GetString("msgCalendarInUse"),
                                                                   rm.GetString("titleDeleteOneCalendar"),
                                                                   MessageBoxButtons.OKCancel,
                                                                   MessageBoxIcon.Exclamation);

                            //For traces
                            logger.Debug(location + "Values:");
                            logger.Debug(location + "  result2-> " + result2.ToString());

                            switch (result2)
                            {
                                case DialogResult.OK:
                                    //For traces   
                                    logger.Debug(location + "Before line: asd.setJobCalendarIdToNull(currentValueCboJob);");

                                    asd.setJobCalendarIdToNull(currentValueCboJob);
                                    break;
                                case DialogResult.Cancel:
                                    return;
                                    break;
                            }
                        }

                        //For traces   
                        logger.Debug(location + "Before line: JobCalendars jc = new JobCalendars(btecDB);");

                        JobCalendars jc = new JobCalendars(btecDB);

                        //For traces   
                        logger.Debug(location + "Before line: jc.deleteJobCalendars(currentValueCboJob);");

                        jc.deleteJobCalendars(currentValueCboJob);

                        //For traces   
                        logger.Debug(location + "Before line: jcd.deleteJobCalendarsDef(currentValueCboJob);");

                        jcd.deleteJobCalendarsDef(currentValueCboJob);

                        //For traces   
                        logger.Debug(location + "Before line: fillCboJob();");

                        fillCboJob();

                        //For traces   
                        logger.Debug(location + "Before line: initializeTabJobValues();");

                        initializeTabJobValues();
                        break;
                    case DialogResult.Cancel:
                        break;
                }

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
    }
}