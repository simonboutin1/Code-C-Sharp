//2013-07 Simon Boutin MANTIS# 18750 : Convert LBTables from VB to C#

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using BancTec.PCR2P.Core.BusinessLogic.Administration;
using BancTec.PCR2P.Core.DatabaseModel.Administration;
using NetCommunTools;

namespace Administration
{
    public partial class frmCalendarAssign : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmCalendarAssign).Name;

        IBtecDB btecDB;
        bool isSomethingChanged = false;
        bool isFormClosedByUser = false;

        int calendarID;

        public DataTable availableJobsTable;
        public DataTable selectedJobsTable;

        ResourceManager rm;
        ILoggerBtec logger;

        public frmCalendarAssign(IBtecDB btecDBParent, int calendarIDParent)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmCalendarAssign));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmCalendarAssign));
                }

                //For traces
                logger.Debug(location + " Starting...");

                //We must get the resource manager after setting the culture.
                rm = SingletonResourcesManager.Instance.getResourceManager();

                //For traces   
                logger.Debug(location + "Before line: InitializeComponent();");

                //Initializes all the components of the form
                InitializeComponent();

                btecDB = btecDBParent;
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
        private void frmCalendarAssign_Load(object sender, EventArgs e)
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

                string[] AvailableColumnHeaderText = {rm.GetString("colIdNo"), rm.GetString("colDescription")};

                //Specifies the number of desired columns for the dgvAvailableJobs object
                dgvAvailableJobs.ColumnCount = 2;

                //Specifies the dgvAvailableJobs width to calculate the columnns width
                double dgvAvailableJobsWidth = dgvAvailableJobs.Width;

                //Specifies all the columns width of the dgvAvailableJobs object 
                //The sum of all the constants must equal 1
                double[] AvailableColumnWidth = {dgvAvailableJobsWidth * 0.2,
                                                 dgvAvailableJobsWidth * 0.8};

                for (int i = 0; i < dgvAvailableJobs.ColumnCount; i++)
                {
                    dgvAvailableJobs.Columns[i].HeaderText = AvailableColumnHeaderText[i];
                    dgvAvailableJobs.Columns[i].Width = (int)Math.Round(AvailableColumnWidth[i]);
                    //specifies that we can sort this column
                    dgvAvailableJobs.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvAvailableJobs.Columns[i].ValueType = typeof(string);
                }

                dgvAvailableJobs.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAvailableJobs.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAvailableJobs.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvAvailableJobs.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                //Force a column to be sorted by défault
                dgvAvailableJobs.Sort(dgvAvailableJobs.Columns[0], ListSortDirection.Ascending);

                string[] SelectedColumnHeaderText = {rm.GetString("colIdNo"),
                                                     rm.GetString("colDescription")};

                //Specifies the number of desired columns for the dgvSelectedJobs object
                dgvSelectedJobs.ColumnCount = 2;

                //Specifies the dgvSelectedJobs width to calculate the columnns width
                double dgvSelectedJobsWidth = dgvSelectedJobs.Width;

                //Specifies all the columns width of the dgvSelectedJobs object 
                //The sum of all the constants must equal 1
                double[] SelectedColumnWidth = {dgvSelectedJobsWidth * 0.2,
                                                dgvSelectedJobsWidth * 0.8};

                for (int i = 0; i < dgvSelectedJobs.ColumnCount; i++)
                {
                    dgvSelectedJobs.Columns[i].HeaderText = SelectedColumnHeaderText[i];
                    dgvSelectedJobs.Columns[i].Width = (int)Math.Round(SelectedColumnWidth[i]);
                    //specifies that we can sort this column
                    dgvSelectedJobs.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvSelectedJobs.Columns[i].ValueType = typeof(string);
                }

                dgvSelectedJobs.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvSelectedJobs.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvSelectedJobs.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvSelectedJobs.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                //Force a column to be sorted by défault
                dgvSelectedJobs.Sort(dgvSelectedJobs.Columns[0], ListSortDirection.Ascending);

                //For traces   
                logger.Debug(location + "Before line: fillCboIdDesc();");

                fillCboIdDesc();

                cboIdDesc.SelectedValue = calendarID;

                //For traces   
                logger.Debug(location + "Before line: initializeFormValues();");

                //Initializes all the form values
                initializeFormValues();

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

        //Initializes all the form values
        private void initializeFormValues()
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
            logger.Info(location + "Purpose: Initializes all the form values");

            //For traces   
            logger.Debug(location + "Before line: refreshAvailableJobsData();");

            refreshAvailableJobsData();

            //For traces   
            logger.Debug(location + "Before line: refreshDgvAvailableJobs);");

            refreshDgvAvailableJobs();

            //For traces   
            logger.Debug(location + "Before line: refreshSelectedJobsData();");

            refreshSelectedJobsData();

            //For traces   
            logger.Debug(location + "Before line: refreshDgvSelectedJobs();");

            refreshDgvSelectedJobs();

            isSomethingChanged = false;
            btnOK.Enabled = false;

            //Enables or disables the 4 transfer buttons
            enableTransferButtons();

            //For traces
            logger.Debug(location + " Ending...");

            //displays the default cursor
            Cursor.Current = Cursors.Default;
        }

        //Refreshes the available jobs DataTable with the most recent data
        void refreshAvailableJobsData()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the available jobs DataTable with the most recent data");

            //For traces   
            logger.Debug(location + "Before line: AvailableJobs aj = new AvailableJobs(btecDB);");
            logger.Debug(location + "        and: availableJobsTable = aj.getAvailableJobsFromCalendarId(calendarID);");

            AvailableJobs aj = new AvailableJobs(btecDB);
            availableJobsTable = aj.getAvailableJobsFromCalendarId(calendarID);

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Refreshes the dgvAvailableJobs object with the most recent data
        public void refreshDgvAvailableJobs()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the dgvAvailableJobs object with the most recent data");

            //Erases all data from the dgvAvailableJobs object
            dgvAvailableJobs.Rows.Clear();

            //Get the current sorted column and its direction (next 2 lines)
            int currentSortedColumnIndex = dgvAvailableJobs.SortedColumn.Index;
            ListSortDirection direction = (dgvAvailableJobs.SortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);

            if (availableJobsTable != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in availableJobsTable.Rows)");

                //Fill the availableJobsTable object with values
                foreach (DataRow row in availableJobsTable.Rows)
                {
                    //Adds a row of records in the dgvAvailableJobs object
                    dgvAvailableJobs.Rows.Add((row["STATEMENT_ID"] ?? "").ToString(),
                                              (row["DESCRIPTION"] ?? "").ToString());
                }
            }

            //Sort the dgvAvailableJobs object like before
            dgvAvailableJobs.Sort(dgvAvailableJobs.Columns[currentSortedColumnIndex], direction);

            //Put the focus on the fisrt row
            if (dgvAvailableJobs.Rows.Count > 0)
            {
                dgvAvailableJobs.CurrentCell = dgvAvailableJobs[0, 0];
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Refreshes the selected jobs DataTable with the most recent data
        void refreshSelectedJobsData()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the selected jobs DataTable with the most recent data");

            //For traces   
            logger.Debug(location + "Before line: SelectedJobs sj = new SelectedJobs(btecDB);");
            logger.Debug(location + "        and: selectedJobsTable = sj.getSelectedJobs(calendarID);");

            SelectedJobs sj = new SelectedJobs(btecDB);
            selectedJobsTable = sj.getSelectedJobs(calendarID);

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Refreshes the dgvSelectedJobs object with the most recent data
        public void refreshDgvSelectedJobs()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the dgvSelectedJobs object with the most recent data");

            //Erases all data from the dgvSelectedJobs object
            dgvSelectedJobs.Rows.Clear();

            //Get the current sorted column and its direction (next 2 lines)
            int currentSortedColumnIndex = dgvSelectedJobs.SortedColumn.Index;
            ListSortDirection direction = (dgvSelectedJobs.SortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);

            if (selectedJobsTable != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in selectedJobsTable.Rows)");

                //Fill the selectedJobsTable object with values
                foreach (DataRow row in selectedJobsTable.Rows)
                {
                    //Adds a row of records in the dgvSelectedAccessCodes object
                    dgvSelectedJobs.Rows.Add((row["STATEMENT_ID"] ?? "").ToString(),
                                             (row["DESCRIPTION"] ?? "").ToString());
                }
            }

            //Sort the dgvSelectedJobs object like before
            dgvSelectedJobs.Sort(dgvSelectedJobs.Columns[currentSortedColumnIndex], direction);

            //Put the focus on the fisrt row
            if (dgvSelectedJobs.Rows.Count > 0)
            {
                dgvSelectedJobs.CurrentCell = dgvSelectedJobs[0, 0];
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Enables or disables the 4 transfer buttons
        private void enableTransferButtons()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Enables or disables the 4 transfer buttons");

            enableMoveOneRightButton();
            enableMoveOneLeftButton();
            enableMoveAllRightButton();
            enableMoveAllLeftButton();

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Enable or disable the MoveOneRight button
        private void enableMoveOneRightButton()
        {
            btnMoveOneRight.Enabled = (dgvAvailableJobs.CurrentRow == null ? false : true);
        }

        //Enable or disable the MoveOneLeft button
        private void enableMoveOneLeftButton()
        {
            btnMoveOneLeft.Enabled = (dgvSelectedJobs.CurrentRow == null ? false : true);
        }

        //Enable or disable the MoveAllRight button
        private void enableMoveAllRightButton()
        {
            btnMoveAllRight.Enabled = (dgvAvailableJobs.RowCount == 0 ? false : true);
        }

        //Enable or disable the MoveAllLeft button
        private void enableMoveAllLeftButton()
        {
            btnMoveAllLeft.Enabled = (dgvSelectedJobs.RowCount == 0 ? false : true);
        }

        //Fills the cboIdDesc object with values
        private void fillCboIdDesc()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Fills the cboIdDesc object with values");

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

                //Fills the CboIdDesc's data source
                foreach (DataRow row in jobTable.Rows)
                {
                    calendarId = int.Parse((row["CALENDAR_ID"] ?? "-1").ToString());
                    idDesc = (row["ID_DESC"] ?? "").ToString();

                    dataSource.Add(new ComboboxItem() { Text = idDesc, Value = calendarId });
                }
            }

            this.cboIdDesc.SelectedValueChanged -= new System.EventHandler(this.cboIdDesc_SelectedValueChanged);

            //For traces   
            logger.Debug(location + "Before line: cboIdDesc.DataSource = dataSource;");

            cboIdDesc.DataSource = dataSource;
            cboIdDesc.DisplayMember = "Text";
            cboIdDesc.ValueMember = "Value";

            this.cboIdDesc.SelectedValueChanged += new System.EventHandler(this.cboIdDesc_SelectedValueChanged);

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Allows to fill the DisplayMember and ValueMember properties of some combo box
        private class ComboboxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }
        }

        //Occurs when the selected value of the cboIdDesc object is changed
        private void cboIdDesc_SelectedValueChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the selected value of the cboIdDesc object is changed");

                if ((int)cboIdDesc.SelectedValue != calendarID)
                {
                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  isSomethingChanged-> " + isSomethingChanged.ToString());

                    if (isSomethingChanged)
                    {
                        //For traces   
                        logger.Debug(location + " After line: if (isSomethingChanged)");

                        DialogResult result = MessageBox.Show(rm.GetString("msgSaveChanges3"),
                                                              rm.GetString("titleSaveChanges2"),
                                                              MessageBoxButtons.OKCancel,
                                                              MessageBoxIcon.None);

                        //For traces
                        logger.Debug(location + "Values:");
                        logger.Debug(location + "  result-> " + result.ToString());

                        switch (result)
                        {
                            case DialogResult.OK:
                                //For traces   
                                logger.Debug(location + "Before line: saveForm();");

                                saveForm();
                                break;
                            case DialogResult.Cancel:
                                break;
                        }
                    }

                    calendarID = (int)cboIdDesc.SelectedValue;

                    //For traces   
                    logger.Debug(location + "Before line: initializeFormValues();");

                    initializeFormValues();
                }
            }
            catch (Exception ex)
            {
                //For traces
                logger.Error(location + ex.Message);

                //Informs the user that an error occurs
                MessageBox.Show(rm.GetString("GenericErrorOccured") + " : " + ex.Message, rm.GetString("GenericErrorOccuredCaption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Allow to format the display of the dgvAvailableJobs columns
        private void dgvAvailableJobs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
                logger.Info(location + "Purpose: Allow to format the display of the dgvAvailableJobs columns");

                // Check the value of the e.ColumnIndex property if you want to apply this formatting only to some columns
                if (e.Value != null)
                {
                    if (e.ColumnIndex == 0)
                    {
                        e.Value = e.Value.ToString().PadLeft(4, '0');
                        e.FormattingApplied = true;
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

        //Allow to format the display of the dgvSelectedJobs columns
        private void dgvSelectedJobs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
                logger.Info(location + "Purpose: Allow to format the display of the dgvSelectedJobs columns");

                // Check the value of the e.ColumnIndex property if you want to apply this formatting only to some columns
                if (e.Value != null)
                {
                    if (e.ColumnIndex == 0)
                    {
                        e.Value = e.Value.ToString().PadLeft(4, '0');
                        e.FormattingApplied = true;
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

        //Transfer an available job to the selected group
        private void btnMoveOneRight_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Transfer an available job to the selected group");

                string currentJob = dgvAvailableJobs.CurrentRow.Cells[0].Value.ToString();
                if (string.IsNullOrEmpty(currentJob)) { throw new Exception("A cell of the ID column of the dgvAvailableJobs object is empty."); }

                DataRow currentTableRowAvailable = availableJobsTable.Select("STATEMENT_ID = " + currentJob).First();

                //For traces   
                logger.Debug(location + "Before line: selectedJobsTable.ImportRow(currentTableRowAvailable);");

                //Add the current row
                selectedJobsTable.ImportRow(currentTableRowAvailable);

                //For traces   
                logger.Debug(location + "Before line: availableJobsTable.Rows.Remove(currentTableRowAvailable);");

                //Remove the current row
                availableJobsTable.Rows.Remove(currentTableRowAvailable);

                //For traces   
                logger.Debug(location + "Before line: adjustAfterTransfer();");

                adjustAfterTransfer();

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

        //Do some adjustements after a transfer of jobs
        private void adjustAfterTransfer()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Do some adjustements after a transfer of jobs");

            //For traces   
            logger.Debug(location + "Before line: refreshDgvAvailableJobs();");
            logger.Debug(location + "        and: refreshDgvSelectedJobs();");

            refreshDgvAvailableJobs();
            refreshDgvSelectedJobs();

            isSomethingChanged = true;

            btnOK.Enabled = true;

            //Enables or disables the 4 transfer buttons
            enableTransferButtons();

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Transfer all available jobs to the selected group
        private void btnMoveAllRight_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Transfer all available jobs to the selected group");

                if (availableJobsTable != null)
                {
                    //For traces   
                    logger.Debug(location + "Before line: foreach (DataRow row in availableJobsTable.Rows)");

                    foreach (DataRow row in availableJobsTable.Rows)
                    {
                        //Add the current row
                        selectedJobsTable.ImportRow(row);
                    }
                }

                availableJobsTable.Clear();

                //For traces   
                logger.Debug(location + "Before line: adjustAfterTransfer();");

                adjustAfterTransfer();

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

        //Transfer a selected job to the available group
        private void btnMoveOneLeft_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Transfer a selected job to the available group");

                string currentJob = dgvSelectedJobs.CurrentRow.Cells[0].Value.ToString();
                if (string.IsNullOrEmpty(currentJob)) { throw new Exception("A cell of the ID column of the dgvSelectedJobs object is empty."); }

                DataRow currentTableRowSelected = selectedJobsTable.Select("STATEMENT_ID = " + currentJob).First();

                //For traces   
                logger.Debug(location + "Before line: availableJobsTable.ImportRow(currentTableRowSelected);");

                //Add the current row
                availableJobsTable.ImportRow(currentTableRowSelected);

                //For traces   
                logger.Debug(location + "Before line: selectedJobsTable.Rows.Remove(currentTableRowSelected);");

                //Remove the current row
                selectedJobsTable.Rows.Remove(currentTableRowSelected);

                //For traces   
                logger.Debug(location + "Before line: adjustAfterTransfer();");

                adjustAfterTransfer();

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

        //Transfer all selected jobs to the available group
        private void btnMoveAllLeft_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Transfer all selected jobs to the available group");

                if (selectedJobsTable != null)
                {
                    //For traces   
                    logger.Debug(location + "Before line: foreach (DataRow row in selectedJobsTable.Rows)");

                    foreach (DataRow row in selectedJobsTable.Rows)
                    {
                        //Add the current row
                        availableJobsTable.ImportRow(row);
                    }
                }

                selectedJobsTable.Clear();

                //For traces   
                logger.Debug(location + "Before line: adjustAfterTransfer();");

                adjustAfterTransfer();

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

                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  result-> " + result.ToString());

                    switch (result)
                    {
                        case DialogResult.OK:
                            //For traces   
                            logger.Debug(location + "Before line: saveForm();");

                            saveForm();

                            isFormClosedByUser = true;

                            this.Close();
                            break;
                        case DialogResult.Cancel:
                            break;
                    }

                    //displays the default cursor
                    Cursor.Current = Cursors.Default;
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

        //Saves all the form's data
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
            logger.Info(location + "Purpose: Saves all the form's data");

            //For traces   
            logger.Debug(location + "Before line: SelectedJobs sj = new SelectedJobs(btecDB);");
            logger.Debug(location + "        and: sj.deleteSelectedJobs(calendarID);");

            SelectedJobs sj = new SelectedJobs(btecDB);

            //delete the previous selected jobs
            sj.deleteSelectedJobs(calendarID);

            string statementID;

            if (selectedJobsTable != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in selectedJobsTable.Rows)");

                //Saves all data from the selected jobs DataTable 
                foreach (DataRow row in selectedJobsTable.Rows)
                {
                    statementID = (row["STATEMENT_ID"] ?? "-1").ToString();

                    //Saves the data in the STATEMENT_DEFINITION table
                    sj.saveSelectedJobsInfo(calendarID, int.Parse(statementID));
                }
            }

            //For traces
            logger.Debug(location + " Ending...");

            //displays the default cursor
            Cursor.Current = Cursors.Default;
        }

        //Occurs when the form is closed
        private void frmCalendarAssign_FormClosing(object sender, FormClosingEventArgs e)
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
                    logger.Debug(location + " After line: if (isSomethingChanged)");

                    //For traces
                    logger.Debug(location + "Values:");
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
    }
}