//2013-05 Simon Boutin MANTIS# 18750 : Convert LBTables from VB to C#

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using BancTec.PCR2P.Core.DatabaseModel;
using NetCommunTools;
using BancTec.PCR2P.Core.DatabaseModel.Administration;

namespace Administration
{
    public partial class frmJobGroupingDef : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmJobGroupingDef).Name;

        const string ACTION_EDIT = "Edit";
        const string ACTION_ADD = "Add";

        public static int FCATID_OPERATIONS = 15;
        public static int FCATID_EOJ = 101;

        DataTable jobGroupsTable;
        DataTable jobsTable;
       
        bool isEndOfJobActive;

        ILoggerBtec logger;
        ResourceManager rm;
        IBtecDB btecDB;

        public frmJobGroupingDef(IBtecDB btecDBParent)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmJobGroupingDef));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmJobGroupingDef));
                }

                //For traces
                logger.Debug(location + " Starting...");

                //We must get the resource manager after setting the culture.
                rm = SingletonResourcesManager.Instance.getResourceManager();

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
        private void frmJobGroupingDef_Load(object sender, EventArgs e)
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

                //Set the minimum size of Panel 2 here to prevent a Visual Studio bug
                splitContainer1.Panel2MinSize = 177;

                //Set the minimum size of the form here to prevent a Visual Studio bug
                this.MinimumSize = new Size(450, 372);

                isEndOfJobActive = getIfEndOfJobActive();

                //Specifies the number of desired columns for the dgvJobGroups object
                dgvJobGroups.ColumnCount = (isEndOfJobActive ? 4 : 3);

                string[] columnHeaderText = {rm.GetString("colName"),
                                             rm.GetString("colOperationManager"),
                                             rm.GetString("colEndOfJob")};

                //Specifies the dgvJobGroups width to calculate the columnns width
                double dgvJobGroupsWidth = dgvJobGroups.Width;

                //Specifies all the columns width of the dgvWorkType object 
                //The sum of all the constants must equal 1

                double[] columnWidth;

                if (isEndOfJobActive)
                {
                    columnWidth = new double[3];

                    columnWidth[0] = dgvJobGroupsWidth * 0.500;
                    columnWidth[1] = dgvJobGroupsWidth * 0.250;
                    columnWidth[2] = dgvJobGroupsWidth * 0.250;
                }
                else
                {
                    columnWidth = new double[2];

                    columnWidth[0] = dgvJobGroupsWidth * 0.750;
                    columnWidth[1] = dgvJobGroupsWidth * 0.250;
                }

                dgvJobGroups.Columns[0].Visible = false;

                for (int i = 1; i < dgvJobGroups.ColumnCount; i++)
                {
                    dgvJobGroups.Columns[i].HeaderText = columnHeaderText[i - 1];
                    dgvJobGroups.Columns[i].Width = (int)Math.Round(columnWidth[i - 1]);
                    //specifies that we can sort this column
                    dgvJobGroups.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvJobGroups.Columns[i].ValueType = typeof(string);
                }

                dgvJobGroups.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvJobGroups.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                dgvJobGroups.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvJobGroups.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                if (isEndOfJobActive)
                {
                    dgvJobGroups.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvJobGroups.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                //Specifies the number of desired columns for the dgvJobs object
                dgvJobs.ColumnCount = 2;

                string[] columnHeaderText2 = {rm.GetString("colIdNo"),
                                              rm.GetString("colDescription")};

                //Specifies the dgvJobs width to calculate the columnns width
                double dgvJobsWidth = dgvJobs.Width;

                //Specifies all the columns width of the dgvJobs object 
                //The sum of all the constants must equal 1
                double[] columnWidth2 = {dgvJobsWidth * 0.200,
                                         dgvJobsWidth * 0.800};

                for (int i = 0; i < dgvJobs.ColumnCount; i++)
                {
                    dgvJobs.Columns[i].HeaderText = columnHeaderText2[i];
                    dgvJobs.Columns[i].Width = (int)Math.Round(columnWidth2[i]);
                    //specifies that we can sort this column
                    dgvJobs.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvJobs.Columns[i].ValueType = typeof(string);
                }

                dgvJobs.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvJobs.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvJobs.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvJobs.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                //For traces   
                logger.Debug(location + "Before line: refreshJobGroupsData();");

                refreshJobGroupsData();

                //For traces   
                logger.Debug(location + "Before line: refreshDgvJobGroups();");

                refreshDgvJobGroups();

                enableEditRemoveJobGroupsButtons();

                setTitleDgvJobs();

                //For traces   
                logger.Debug(location + "Before line: refreshJobsData();");

                refreshJobsData();

                //For traces   
                logger.Debug(location + "Before line: refreshDgvJobs();");

                refreshDgvJobs();

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

        //Refreshes the jobs DataTable with the most recent data
        void refreshJobsData()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the jobs DataTable with the most recent data");

            //For traces   
            logger.Debug(location + "Before line: AdminStatementGroupDefinition asgd = new AdminStatementGroupDefinition(btecDB);");

            AdminStatementGroupDefinition asgd = new AdminStatementGroupDefinition(btecDB);

            if (dgvJobGroups.CurrentRow == null)
            {
                jobsTable = null;
            }
            else
            {
                int groupId = (int)dgvJobGroups.CurrentRow.Cells[0].Value;

                //For traces   
                logger.Debug(location + "Before line: jobsTable = asgd.getDescription(groupId);");

                jobsTable = asgd.getDescription(groupId);
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //set the displayed title for the dgvJobs object
        public void setTitleDgvJobs()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: set the displayed title for the dgvJobs object");

            DataGridViewRow currentRow = dgvJobGroups.CurrentRow;

            string titleJobs = rm.GetString("JobGroupingJobs");

            if (currentRow != null)
            {
                string strCurrentName = dgvJobGroups.CurrentRow.Cells[1].Value.ToString();
                if (string.IsNullOrEmpty(strCurrentName)) { throw new Exception("The field GROUP_DESCRIPTION in the dgvJobGroups object is empty."); }

                titleJobs += " " + strCurrentName;
            }

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  titleJobs-> " + titleJobs);

            grpBoxJobs.Text = titleJobs;

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Enables or disables the [Edit] and [Remove] job groups buttons
        public void enableEditRemoveJobGroupsButtons()
        {
            if (dgvJobGroups.CurrentRow == null)
            {
                btnEditJobGroups.Enabled = false;
                btnRemoveJobGroups.Enabled = false;
            }
            else
            {
                btnEditJobGroups.Enabled = true;
                btnRemoveJobGroups.Enabled = true;
            }
        }

        //Determines if the End Of Job field is active
        bool getIfEndOfJobActive()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Determines if the End Of Job field is active");

            //For traces   
            logger.Debug(location + "Before line: AppParamExt ape = new AppParamExt(btecDB);");
            logger.Debug(location + "        and: AppParamExtData apeData = ape.GetAppSetting(\"EOJ\", \"MODE\", \"-1\");");
            logger.Debug(location + "        and: string value = apeData.GetStringValue(apeData.VAL1);");

            AppParamExt ape = new AppParamExt(btecDB);
            AppParamExtData apeData = ape.GetAppSetting("EOJ", "MODE", "-1");
            string value = apeData.GetStringValue(apeData.VAL1);

            //For traces
            logger.Debug(location + " Ending...");

            return (value == "1" ? true : false);
        }

        //Creates the jobGroupsTable DataTable
        void create_jobGroupsTable()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Creates the jobGroupsTable DataTable");

            jobGroupsTable = new DataTable();

            jobGroupsTable.Columns.Add("GROUP_ID", typeof(int));
            jobGroupsTable.Columns.Add("GROUP_DESCRIPTION", typeof(string));
            jobGroupsTable.Columns.Add("OPER_MANAG", typeof(string));

            if (isEndOfJobActive)
            {
                jobGroupsTable.Columns.Add("END_OF_JOB", typeof(string));
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Refreshes the job groups DataTable with the most recent data
        void refreshJobGroupsData()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the job groups DataTable with the most recent data");

            //For traces   
            logger.Debug(location + "Before line: StatementGroup sg = new StatementGroup(btecDB);");
            logger.Debug(location + "        and: StatementGroupData[] sgData = sg.GetStatementGroup();");

            StatementGroup sg = new StatementGroup(btecDB);
            StatementGroupData[] sgData = sg.GetStatementGroup();

            logger.Debug(location + "        and: StatementGroupCategory sgc = new StatementGroupCategory(btecDB);");

            StatementGroupCategory sgc = new StatementGroupCategory(btecDB);

            string description;
            int groupId;
            string operationManager;
            string endOfJob;

            if (sgData != null)
            {
                create_jobGroupsTable();

                //For traces   
                logger.Debug(location + "Before line: for (int i = 0; i < sgData.Count<StatementGroupData>(); i++)");

                //Fill the jobGroupsTable data table with values
                for (int i = 0; i < sgData.Count<StatementGroupData>(); i++)
                {
                    groupId = sgData[i].GetIntValue(sgData[i].GROUP_ID);
                    description = sgData[i].GetStringValue(sgData[i].GROUP_DESCRIPTION);
                    operationManager = (sgc.isCategoryExists(groupId, FCATID_OPERATIONS) ? rm.GetString("Yes") : "");

                    if (isEndOfJobActive)
                    {
                        endOfJob = (sgc.isCategoryExists(groupId, FCATID_EOJ) ? rm.GetString("Yes") : "");

                        //Adds a row of records in the jobGroupsTable data table
                        jobGroupsTable.Rows.Add(groupId,
                                                description,
                                                operationManager,
                                                endOfJob);
                    }
                    else
                    {
                        //Adds a row of records in the jobGroupsTable data table
                        jobGroupsTable.Rows.Add(groupId,
                                                description,
                                                operationManager);
                    }
                }
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Refreshes the dgvJobGroups object with the most recent data
        public void refreshDgvJobGroups()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the dgvJobGroups object with the most recent data");

            //Erases all data from the dgvJobGroups object
            dgvJobGroups.Rows.Clear();

            if (jobGroupsTable != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in jobGroupsTable.Rows)");

                //Fill the dgvJobGroups object with values
                foreach (DataRow row in jobGroupsTable.Rows)
                {
                    if (isEndOfJobActive)
                    {
                        //Adds a row of records in the dgvJobGroups object
                        dgvJobGroups.Rows.Add(row["GROUP_ID"],
                                              row["GROUP_DESCRIPTION"],
                                              row["OPER_MANAG"],
                                              row["END_OF_JOB"]);
                    }
                    else
                    {
                        //Adds a row of records in the dgvJobGroups object
                        dgvJobGroups.Rows.Add(row["GROUP_ID"],
                                              row["GROUP_DESCRIPTION"],
                                              row["OPER_MANAG"]);
                    }
                }
            }

            //Force a column to be sorted by défault
            dgvJobGroups.Sort(dgvJobGroups.Columns[1], ListSortDirection.Ascending);

            //Put the focus on the fisrt row
            if (dgvJobGroups.Rows.Count > 0)
            {
                dgvJobGroups.CurrentCell = dgvJobGroups[1, 0];
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Refreshes the dgvJobs object with the most recent data
        public void refreshDgvJobs()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the dgvJobs object with the most recent data");

            //Erases all data from the dgvJobs object
            dgvJobs.Rows.Clear();

            if (jobsTable != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in jobsTable.Rows)");

                //Fill the dgvJobs object with values
                foreach (DataRow row in jobsTable.Rows)
                {
                    //Adds a row of records in the dgvJobGroups object
                    dgvJobs.Rows.Add(row["STATEMENT_ID"],
                                     row["DESCRIPTION"]);
                }
            }

            //Force a column to be sorted by défault
            dgvJobs.Sort(dgvJobs.Columns[0], ListSortDirection.Ascending);

            //Put the focus on the fisrt row
            if (dgvJobs.Rows.Count > 0)
            {
                dgvJobs.CurrentCell = dgvJobs[0, 0];
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Allow to format the display of the dgvJobs columns
        private void dgvJobs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
                logger.Info(location + "Purpose: Allow to format the display of the dgvJobs columns");

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

        //Occurs when the [Close] button is clicked
        private void btnClose_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Close] button is clicked");

                //For traces   
                logger.Debug(location + "Before line: this.Close();");

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

        //Occurs when the [Edit] Job Groups button is clicked
        private void btnEditJobGroups_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Edit] Job Groups button is clicked");

                DataGridViewRow currentRow = dgvJobGroups.CurrentRow;

                //For traces   
                logger.Debug(location + "Before line: editingJobGroups(currentRow, ACTION_EDIT);");

                editingJobGroups(currentRow, ACTION_EDIT);

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

        //Gets the group Id and opens the frmJobGroupingEdit form for a specific action
        private void editingJobGroups(DataGridViewRow currentRow, string action)
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
            logger.Info(location + "Purpose: Gets the group Id and opens the frmJobGroupingEdit form for an edit action");

            int? groupId = null;
            int currentIndex = 0;

            if (action == ACTION_EDIT)
            {
                currentIndex = currentRow.Index;

                string strGroupId = (dgvJobGroups[0, currentIndex].Value ?? "").ToString();

                if (string.IsNullOrEmpty(strGroupId)) { throw new Exception("A cell of the GROUP_ID column of the dgvJobGroups object is empty."); }
                else { groupId = int.Parse(strGroupId); }
            }

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  action-> " + action.ToString());
            logger.Debug(location + "  groupId-> " + (groupId == null ? "NULL" : groupId.ToString()));

            //For traces   
            logger.Debug(location + "Before line: frmJobGroupingEdit _frm = new frmJobGroupingEdit(btecDB, action, groupId, isEndOfJobActive);");
            logger.Debug(location + "        and: DialogResult result = _frm.ShowDialog(this);");

            //Opens the child form containing the jobs corresponding to the group Id (next 2 lines)
            frmJobGroupingEdit _frm = new frmJobGroupingEdit(btecDB, action, groupId, isEndOfJobActive);

            DialogResult result = _frm.ShowDialog(this);
            _frm.Dispose();

            if (result == DialogResult.OK)
            {
                //For traces   
                logger.Debug(location + "Before line: refreshJobGroupsData();");

                refreshJobGroupsData();

                //For traces   
                logger.Debug(location + "Before line: refreshDgvJobGroups();");

                refreshDgvJobGroups();

                if (action == ACTION_EDIT) 
                { 
                    dgvJobGroups.CurrentCell = dgvJobGroups[1, currentIndex]; 
                }
                else 
                { 
                    enableEditRemoveJobGroupsButtons(); 
                }

                setTitleDgvJobs();

                //For traces   
                logger.Debug(location + "Before line: refreshJobsData();");

                refreshJobsData();

                //For traces   
                logger.Debug(location + "Before line: refreshDgvJobs();");

                refreshDgvJobs();
            }

            //For traces
            logger.Debug(location + " Ending...");

            //displays the default cursor
            Cursor.Current = Cursors.Default;
        }

        //Occurs when a line in the dgvJobGroups object is double-clicked
        private void dgvJobGroups_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                logger.Info(location + "Purpose: Occurs when a line in the dgvJobGroups object is double-clicked");

                DataGridViewRow currentRow = (sender as DataGridView).CurrentRow;

                if (currentRow != null)
                {
                    //For traces   
                    logger.Debug(location + "Before line: editingJobGroups(currentRow, ACTION_EDIT);");

                    editingJobGroups(currentRow, ACTION_EDIT);
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

        //Occurs when the [Add] Job Groups button is clicked
        private void btnAddJobGroups_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Add] Job Groups button is clicked");

                DataGridViewRow currentRow = dgvJobGroups.CurrentRow;

                //For traces   
                logger.Debug(location + "Before line: editingJobGroups(currentRow, ACTION_ADD);");

                editingJobGroups(currentRow, ACTION_ADD);

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

        //Occurs when the [Remove] Job Groups button is clicked
        private void btnRemoveJobGroups_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Remove] Job Groups button is clicked");

                if (dgvJobGroups.CurrentRow != null)
                {
                    int currentIndex = dgvJobGroups.CurrentRow.Index;
                    int groupId = (int)dgvJobGroups[0, currentIndex].Value;
                    string description = (dgvJobGroups[1, currentIndex].Value ?? "").ToString();

                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  currentIndex-> " + currentIndex.ToString());
                    logger.Debug(location + "  groupId-> " + groupId.ToString());
                    logger.Debug(location + "  description-> " + description);

                    string message = "";
                    string jobGroup = "";

                    message += rm.GetString("msgDeleteJobGroup1of2");
                    message += "\n" + "\n";

                    jobGroup += String.IsNullOrEmpty(description) ? "" : description + "\n";

                    message += String.IsNullOrEmpty(jobGroup)     ? "" : jobGroup + "\n";
                    message += rm.GetString("msgDeleteJobGroup2of2");
                        
                    DialogResult result = MessageBox.Show(message,
                                                          rm.GetString("titleDeleteJobGroup"), 
                                                          MessageBoxButtons.OKCancel,
                                                          MessageBoxIcon.Exclamation);

                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  result-> " + result.ToString());

                    switch (result)
                    {
                        case DialogResult.OK:
                            //displays an hourglass cursor
                            Cursor.Current = Cursors.WaitCursor;

                            //For traces   
                            logger.Debug(location + "Before line: StatementGroupCategory sgc = new StatementGroupCategory(btecDB);");
                            logger.Debug(location + "        and: sgc.deleteStatementGroupCategory(groupId);");

                            StatementGroupCategory sgc = new StatementGroupCategory(btecDB);
                            sgc.deleteStatementGroupCategory(groupId);

                            //For traces   
                            logger.Debug(location + "Before line: StatementGroupDefinition sgd = new StatementGroupDefinition(btecDB);");
                            logger.Debug(location + "        and: sgd.deleteStatementGroupDefinition(groupId);");

                            StatementGroupDefinition sgd = new StatementGroupDefinition(btecDB);
                            sgd.deleteStatementGroupDefinition(groupId);

                            //For traces   
                            logger.Debug(location + "Before line: StatementGroup sg = new StatementGroup(btecDB);");
                            logger.Debug(location + "        and: sg.deleteStatementGroup(groupId);");

                            StatementGroup sg = new StatementGroup(btecDB);
                            sg.deleteStatementGroup(groupId);

                            //For traces   
                            logger.Debug(location + "Before line: refreshJobGroupsData();");

                            refreshJobGroupsData();

                            //For traces   
                            logger.Debug(location + "Before line: refreshDgvJobGroups();");

                            refreshDgvJobGroups();

                            enableEditRemoveJobGroupsButtons(); 

                            setTitleDgvJobs();

                            //For traces   
                            logger.Debug(location + "Before line: refreshJobsData();");

                            refreshJobsData();

                            //For traces   
                            logger.Debug(location + "Before line: refreshDgvJobs();");

                            refreshDgvJobs();

                            //displays the default cursor
                            Cursor.Current = Cursors.Default;
                            break;
                        case DialogResult.Cancel:
                            break;
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

        //Occurs when a cell of the dgvJobGroups object is selected
        private void dgvJobGroups_CurrentCellChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when a cell of the dgvJobGroups object is selected");

                //displays an hourglass cursor
                Cursor.Current = Cursors.WaitCursor;

                setTitleDgvJobs();

                //For traces   
                logger.Debug(location + "Before line: refreshJobsData();");

                refreshJobsData();

                //For traces   
                logger.Debug(location + "Before line: refreshDgvJobs();");

                refreshDgvJobs();

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