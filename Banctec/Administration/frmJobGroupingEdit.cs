//2013-07 Simon Boutin MANTIS# 18750 : Convert LBTables from VB to C#

using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using BancTec.PCR2P.Core.BusinessLogic.Administration;
using BancTec.PCR2P.Core.DatabaseModel;
using BancTec.PCR2P.Core.DatabaseModel.Administration;
using NetCommunTools;

namespace Administration
{
    public partial class frmJobGroupingEdit : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmJobGroupingEdit).Name;

        const string ACTION_ADD = "Add";
        const string ACTION_EDIT = "Edit";

        const int NAME_LENGTH = 100;

        IBtecDB btecDB;
        bool isSomethingChanged = false;
        bool isFormClosedByUser = false;

        int? groupId;
        bool isEndOfJobActive;
        string action;

        public DataTable availableJobsTable;
        public DataTable selectedJobsTable;

        ResourceManager rm;
        ILoggerBtec logger;
        
        public frmJobGroupingEdit(IBtecDB btecDBParent, string actionParent, int? groupIdParent, bool isEndOfJobActiveParent)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmJobGroupingEdit));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmJobGroupingEdit));
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
                action = actionParent;
                groupId = groupIdParent;
                isEndOfJobActive = isEndOfJobActiveParent;

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
        private void frmJobGroupingEdit_Load(object sender, EventArgs e)
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

                string[] AvailableColumnHeaderText = { rm.GetString("colIdNo"), rm.GetString("colDescription") };

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

                checkEndOfJob.Visible = isEndOfJobActive;

                switch (action)
                {
                    case ACTION_EDIT:
                        this.Text = rm.GetString("titleEditJobGroup");
                        break;
                    case ACTION_ADD:
                        this.Text = rm.GetString("titleAddJobGroup");
                        break;
                }

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
            logger.Debug(location + "Before line: StatementGroup sg = new StatementGroup(btecDB);");

            StatementGroup sg = new StatementGroup(btecDB);
            
            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  action-> " + action);

            //adjusts the form appearance depending whether group id value is NULL or not
            if (groupId == null)
            {
                txtName.Text = null;
                checkOperationManager.Checked = false;
                if (isEndOfJobActive) checkEndOfJob.Checked = false;
            }
            else
            {
                //For traces   
                logger.Debug(location + "Before line: StatementGroupData sgData = sg.GetStatementGroup((long)groupId);");

                StatementGroupData sgData = sg.GetStatementGroup((long)groupId);

                txtName.Text = sgData.GetStringValue(sgData.GROUP_DESCRIPTION);

                //For traces   
                logger.Debug(location + "Before line: StatementGroupCategory sgc = new StatementGroupCategory(btecDB);");

                StatementGroupCategory sgc = new StatementGroupCategory(btecDB);

                //For traces   
                logger.Debug(location + "Before line: checkOperationManager.Checked = sgc.isCategoryExists((int)groupId, frmJobGroupingDef.FCATID_OPERATIONS);");

                checkOperationManager.Checked = sgc.isCategoryExists((int)groupId, frmJobGroupingDef.FCATID_OPERATIONS);

                if (isEndOfJobActive)
                {
                    //For traces   
                    logger.Debug(location + "Before line: checkEndOfJob.Checked = sgc.isCategoryExists((int)groupId, frmJobGroupingDef.FCATID_EOJ);");

                    checkEndOfJob.Checked = sgc.isCategoryExists((int)groupId, frmJobGroupingDef.FCATID_EOJ);
                }
            }

            txtName.TextChanged += new EventHandler(somethingChanged);
            checkOperationManager.CheckedChanged += new EventHandler(somethingChanged);
            if (isEndOfJobActive) checkEndOfJob.CheckedChanged += new EventHandler(somethingChanged);

            //For traces   
            logger.Debug(location + "Before line: refreshAvailableJobsData();");

            refreshAvailableJobsData();

            //For traces   
            logger.Debug(location + "Before line: refreshDgvAvailableJobs();");

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

        //Creates the availableJobsTable DataTable
        void create_availableJobsTable()
        {
            availableJobsTable = new DataTable();

            availableJobsTable.Columns.Add("STATEMENT_ID", typeof(int));
            availableJobsTable.Columns.Add("DESCRIPTION", typeof(string));
        }

        //Creates the selectedJobsTable DataTable
        void create_selectedJobsTable()
        {
            selectedJobsTable = new DataTable();

            selectedJobsTable.Columns.Add("STATEMENT_ID", typeof(int));
            selectedJobsTable.Columns.Add("DESCRIPTION", typeof(string));
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
            logger.Debug(location + "        and: availableJobsTable = aj.getAvailableJobsFromGroupId(groupId);");

            AvailableJobs aj = new AvailableJobs(btecDB);
            availableJobsTable = aj.getAvailableJobsFromGroupId(groupId);

            if (availableJobsTable == null)
            {
                create_availableJobsTable();
            }

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
            logger.Debug(location + "Before line: AdminStatementGroupDefinition asgd = new AdminStatementGroupDefinition(btecDB);");
            logger.Debug(location + "        and: selectedJobsTable = asgd.getDescription(groupId);");

            AdminStatementGroupDefinition asgd = new AdminStatementGroupDefinition(btecDB);
            selectedJobsTable = asgd.getDescription(groupId);

            if (selectedJobsTable == null)
            {
                create_selectedJobsTable();
            }

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

        //occurs when any field is modified
        private void somethingChanged(object sender, EventArgs e)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: occurs when any field is modified");

            isSomethingChanged = true;

            btnOK.Enabled = true;

            //For traces
            logger.Debug(location + " Ending...");
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

                //Check the value of the e.ColumnIndex property if you want to apply this formatting only to some columns
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

                //Check the value of the e.ColumnIndex property if you want to apply this formatting only to some columns
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

                                this.DialogResult = DialogResult.OK;
                                this.Close();
                                break;
                            case DialogResult.Cancel:
                                break;
                        }

                        //displays the default cursor
                        Cursor.Current = Cursors.Default;
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

            //displays an hourglass cursor
            Cursor.Current = Cursors.WaitCursor;

            string buffer = txtName.Text.Trim();
            string name = buffer.Substring(0, Math.Min(buffer.Length, NAME_LENGTH));

            //For traces   
            logger.Debug(location + "Before line: StatementGroupCategory sgc = new StatementGroupCategory(btecDB);");
            logger.Debug(location + "        and: StatementGroup sg = new StatementGroup(btecDB);");
            logger.Debug(location + "        and: StatementGroupDefinition sgd = new StatementGroupDefinition(btecDB);");

            StatementGroupCategory sgc = new StatementGroupCategory(btecDB);
            AdminStatementGroup asg = new AdminStatementGroup(btecDB);
            StatementGroupDefinition sgd = new StatementGroupDefinition(btecDB);

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  action-> " + action);

            switch (action)
            {
                case ACTION_EDIT:
                    //For traces   
                    logger.Debug(location + "Before line: sgc.deleteStatementGroupCategory((long)groupId);");

                    sgc.deleteStatementGroupCategory((long)groupId);

                    //For traces   
                    logger.Debug(location + "Before line: sgd.deleteStatementGroupDefinition((long)groupId);");
                    
                    sgd.deleteStatementGroupDefinition((long)groupId);
                    break;
                case ACTION_ADD:
                    long maxGroupId = asg.getMaxGroupID();

                    groupId = (int)(maxGroupId == 0 ? 100 : maxGroupId + 1);
                    break;
            }

            //For traces   
            logger.Debug(location + "Before line: asg.saveStatementGroupDescription((int)groupId, name);");

            asg.saveStatementGroupDescription((int)groupId, name);

            if (selectedJobsTable.Rows.Count > 0)
            {
                string strStatementId = "";
                long statementId = 0;

                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in selectedJobsTable.Rows)");

                foreach (DataRow row in selectedJobsTable.Rows)
                {
                    strStatementId = (row["STATEMENT_ID"] ?? "").ToString();
                    if (string.IsNullOrEmpty(strStatementId)) { throw new Exception("A row of the STATEMENT_ID column of the selectedJobsTable data table is empty."); }
                    else { statementId = long.Parse(strStatementId); }

                    sgd.SaveStatementGroupDefinition((long)groupId, statementId);
                }
            }

            if (checkOperationManager.Checked)
            {
                //For traces   
                logger.Debug(location + "Before line: sgc.InsertStatementGroupCategory((long)groupId, frmJobGroupingDef.FCATID_OPERATIONS);");

                sgc.InsertStatementGroupCategory((long)groupId, frmJobGroupingDef.FCATID_OPERATIONS);
            }

            if (isEndOfJobActive)
            {
                if (checkEndOfJob.Checked)
                {
                    //For traces   
                    logger.Debug(location + "Before line: sgc.InsertStatementGroupCategory((long)groupId, frmJobGroupingDef.FCATID_EOJ);");

                    sgc.InsertStatementGroupCategory((long)groupId, frmJobGroupingDef.FCATID_EOJ);
                }
            }

            //For traces
            logger.Debug(location + " Ending...");

            //displays the default cursor
            Cursor.Current = Cursors.Default;
        }

        //Performs all the form's validation
        private bool validateForm()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Performs all the form's validation");

            string buffer = txtName.Text.Trim();
            string name = buffer.Substring(0, Math.Min(buffer.Length, NAME_LENGTH));

            //For traces   
            logger.Debug(location + "Before line: //Validation 1");

            //Validation 1
            if (string.IsNullOrEmpty(name))
            {
                //Set the focus to the Name field
                txtName.Focus();

                MessageBox.Show(rm.GetString("msgNameGroup"),
                                rm.GetString("TitleMsgValidationError"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return false;
            }

            //For traces   
            logger.Debug(location + "Before line: //Validation 2");

            //Validation 2
            if (name.IndexOfAny(new char[] {'\'',
                                            ';',
                                            '\\',
                                            '/',
                                            ':',
                                            '*',
                                            '?',
                                            '"',
                                            '<',
                                            '>',
                                            '@',
                                            '%',
                                            '^',
                                            '(',
                                            ')',
                                            '!',
                                            '#',
                                            '&'}) > 0)
            {
                //Set the focus to the Name field
                txtName.Focus();

                MessageBox.Show(rm.GetString("msgNotSpecialCharGroupName"),
                                rm.GetString("TitleMsgValidationError"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return false;
            }

            //For traces   
            logger.Debug(location + "Before line: //Validation 3");

            //Validation 3
            AdminStatementGroup asg = new AdminStatementGroup(btecDB);

            if (asg.isDescriptionInUse(groupId,name))
            {
                //Set the focus to the Name field
                txtName.Focus();

                MessageBox.Show(rm.GetString("msgGroupNameExists"),
                                rm.GetString("TitleMsgValidationError"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return false;
            }

            //For traces   
            logger.Debug(location + "Before line: //Validation 4");

            //Validation 4
            if (dgvSelectedJobs.Rows.Count == 0)
            {
                MessageBox.Show(rm.GetString("msgMustHaveJobsSelected"),
                                rm.GetString("TitleMsgValidationError"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return false;
            }

            //For traces   
            logger.Debug(location + "Before line: //Validation 5");

            //Validation 5
            if (isEndOfJobActive)
            {
                if (selectedJobsTable.Rows.Count > 0)
                {
                    string strStatementId = "";
                    long statementId = 0;
                    long firstStatementId = 0;
                    string statementIdList = "";
                    long? firstJobCalendarId = null;
                    long? otherJobCalendarId = null;
                    string strOtherJobCalendarId = "";
                    Boolean isFirstIteration = true;

                    //For traces   
                    logger.Debug(location + "Before line: AdminStatementDefinition asd = new AdminStatementDefinition(btecDB);");
                    logger.Debug(location + "        and: StatementDefinition sd = new StatementDefinition(btecDB);");
                    logger.Debug(location + "        and: StatementDefinitionData sdData = null;");

                    AdminStatementDefinition asd = new AdminStatementDefinition(btecDB);
                    StatementDefinition sd = new StatementDefinition(btecDB);
                    StatementDefinitionData sdData = null;

                    //For traces   
                    logger.Debug(location + "Before line: foreach (DataRow row in selectedJobsTable.Rows)");

                    foreach (DataRow row in selectedJobsTable.Rows)
                    {
                        strStatementId = (row["STATEMENT_ID"] ?? "").ToString();
                        if (string.IsNullOrEmpty(strStatementId)) { throw new Exception("A row of the STATEMENT_ID column of the selectedJobsTable data table is empty."); }
                        else { statementId = long.Parse(strStatementId); }

                        sdData = sd.GetStatementDefinition(statementId);

                        if (sdData != null)
                        {
                            strOtherJobCalendarId = sdData.GetStringValue(sdData.JOB_CALENDAR_ID);
                            if (string.IsNullOrEmpty(strOtherJobCalendarId)) { otherJobCalendarId = null; }
                            else { otherJobCalendarId = long.Parse(strOtherJobCalendarId); }

                            if (!string.IsNullOrEmpty(statementIdList)) statementIdList += ", ";

                            statementIdList += statementId.ToString();

                            if (isFirstIteration)
                            {
                                firstJobCalendarId = otherJobCalendarId;
                                firstStatementId = statementId;

                                isFirstIteration = false;
                            }
                            else
                            {
                                if (otherJobCalendarId != null)
                                {
                                    if (firstJobCalendarId != otherJobCalendarId)
                                    {
                                        MessageBox.Show(string.Format(rm.GetString("msgCalendarDiff"),
                                                                      "\n\n",
                                                                      firstStatementId.ToString(),
                                                                      (firstJobCalendarId == null ? "null" : firstJobCalendarId.ToString()),
                                                                      statementId.ToString(),
                                                                      otherJobCalendarId.ToString()),
                                                        rm.GetString("TitleMsgValidationError"),
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error);

                                        return false;
                                    }
                                }
                                else if (firstJobCalendarId != null)
                                {
                                    MessageBox.Show(string.Format(rm.GetString("msgCalendarMissing"),
                                                                  "\n\n",
                                                                  statementId.ToString(),
                                                                  firstJobCalendarId.ToString()),
                                                    rm.GetString("TitleMsgValidationError"),
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Error);

                                    return false;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Format(rm.GetString("msgCalendarNotFound"), statementId.ToString()),
                                            rm.GetString("TitleMsgValidationError"),
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);

                            return false;
                        }
                    }

                    if (firstJobCalendarId == null)
                    {
                        //For traces   
                        logger.Debug(location + "Before line: bool? isSameSite = asd.isJobsHaveSameSite(statementIdList);");

                        bool? isSameSite = asd.isJobsHaveSameSite(statementIdList);

                        if (isSameSite == false)
                        {
                            MessageBox.Show(rm.GetString("msgCalendarDiffSite"),
                                            rm.GetString("TitleMsgValidationError"),
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);

                            return false;
                        }
                    }
                }
            }

            //For traces
            logger.Debug(location + " Ending...");

            return true;
        }

        //Occurs when the form is closed
        private void frmJobGroupingEdit_FormClosing(object sender, FormClosingEventArgs e)
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