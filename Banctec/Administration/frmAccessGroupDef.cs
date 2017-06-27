//2013-05 Simon Boutin MANTIS# 16396 : Convert LBTables from VB to C#

using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using BancTec.PCR2P.Core.DatabaseModel;
using BancTec.PCR2P.Core.DatabaseModel.Administration;
using NetCommunTools;

namespace Administration
{
    public partial class frmAccessGroupDef : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmAccessGroupDef).Name;

        const string ACTION_EDIT = "Edit";
        const string ACTION_ADD = "Add";
        const string ACTION_REMOVE = "Remove";
        const string ACTION_COPY = "Copy";

        const string DEFAULT_ACCESS_GROUP = "000000";

        ILoggerBtec logger;
        ResourceManager rm;
        IBtecDB btecDB;

        public frmAccessGroupDef(IBtecDB btecDBParent)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmAccessGroupDef));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmAccessGroupDef));
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
        private void frmAccessGroupDef_Load(object sender, EventArgs e)
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

                //Specifies the number of desired columns for the dgvAccessGroup object
                dgvAccessGroup.ColumnCount = 2;

                string[] columnHeaderText = {rm.GetString("colIdNo"),
                                             rm.GetString("colDescription")};

                //Specifies the dgvAccessGroup width to calculate the columnns width
                double dgvAccessGroupWidth = dgvAccessGroup.Width;

                //Specifies all the columns width of the dgvAccessGroup object 
                //The sum of all the constants must equal 1
                double[] columnWidth = {dgvAccessGroupWidth * 0.222,
                                        dgvAccessGroupWidth * 0.778};

                for (int i = 0; i < dgvAccessGroup.ColumnCount; i++)
                {
                    dgvAccessGroup.Columns[i].HeaderText = columnHeaderText[i];
                    dgvAccessGroup.Columns[i].Width = (int)Math.Round(columnWidth[i]);
                    //specifies that we can sort this column
                    dgvAccessGroup.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvAccessGroup.Columns[i].ValueType = typeof(string);
                }

                dgvAccessGroup.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAccessGroup.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAccessGroup.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvAccessGroup.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                //Force a column to be sorted by défault
                dgvAccessGroup.Sort(dgvAccessGroup.Columns[0], ListSortDirection.Ascending);

                //Allows selection of full row with multiple selection (next 2 lines)
                dgvAccessGroup.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvAccessGroup.MultiSelect = true;

                //For traces   
                logger.Debug(location + "Before line: refreshDgvAccessGroup();");

                refreshDgvAccessGroup();

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

        //Refreshes the dgvAccessGroup object with the most recent data
        public void refreshDgvAccessGroup()
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
                logger.Info(location + "Purpose: Refreshes the dgvAccessGroup object with the most recent data");

                //displays an hourglass cursor
                Cursor.Current = Cursors.WaitCursor;

                //For traces   
                logger.Debug(location + "Before line: dgvAccessGroup.Rows.Clear();");

                //Erases all data from the dgvAccessGroup object
                dgvAccessGroup.Rows.Clear();

                //For traces   
                logger.Debug(location + "Before line: AccessGroupDef ag = new AccessGroupDef(btecDB);");
                logger.Debug(location + "        and: AccessGroupDefData[] agData = ag.GetAccessGroupDefinition();");

                AccessGroupDef ag = new AccessGroupDef(btecDB);
                AccessGroupDefData[] agData = ag.GetAccessGroupDefinition();

                //Get the current sorted column and its direction (next 2 lines)
                int currentSortedColumnIndex = dgvAccessGroup.SortedColumn.Index;
                ListSortDirection direction = (dgvAccessGroup.SortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);

                if (agData != null)
                {
                    //For traces   
                    logger.Debug(location + "Before line: for (int value = 0; value < agData.Count<AccessGroupDefData>(); value++)");

                    //Fill the dgvAccessGroup object with values
                    for (int i = 0; i < agData.Count<AccessGroupDefData>(); i++)
                    {
                        dgvAccessGroup.Rows.Add(agData[i].GetStringValue(agData[i].ACCESS_GROUP_ID),
                                                agData[i].GetStringValue(agData[i].DESCRIPTION));
                    }
                }

                //Sort the dgvAccessGroup object like before
                dgvAccessGroup.Sort(dgvAccessGroup.Columns[currentSortedColumnIndex], direction);

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  dgvAccessGroup.Rows.Count-> " + dgvAccessGroup.Rows.Count.ToString());

                //To have the first row selected
                if (dgvAccessGroup.Rows.Count > 0)
                {
                    dgvAccessGroup.CurrentCell = dgvAccessGroup[0, 0];
                }

                //For traces   
                logger.Debug(location + "Before line: enableEditRemoveWorkTypeButtons();");

                enableEditRemoveCopyButtons();

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

        //Enables or disables the [Edit], [Remove] and [Copy] buttons
        private void enableEditRemoveCopyButtons()
        {
            if (dgvAccessGroup.CurrentRow == null)
            {
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
                btnCopy.Enabled = false;
            }
            else
            {
                bool isDefaultAccessGroup = false;
                int selectedRowCount = dgvAccessGroup.SelectedRows.Count;
                string accessGroupID;

                for (int i = 0; i < selectedRowCount; i++)
                {
                    accessGroupID = dgvAccessGroup.SelectedRows[i].Cells[0].Value.ToString();

                    //if the default access group is selected
                    if (accessGroupID == DEFAULT_ACCESS_GROUP)
                    {
                        isDefaultAccessGroup = true;
                    }
                }

                if (selectedRowCount > 1)
                {
                    btnEdit.Enabled = false;
                    btnRemove.Enabled = !isDefaultAccessGroup & true;
                    btnCopy.Enabled = false;
                }
                else
                {
                    btnEdit.Enabled = !isDefaultAccessGroup & true;
                    btnRemove.Enabled = !isDefaultAccessGroup & true;
                    btnCopy.Enabled = true;
                }
            }
        }

        //Gets the access group Id and opens the frmAccessGroupEdit form for a specific action
        private void editingAccessGroup(DataGridViewRow currentRow, string action)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            if (currentRow != null)
            {
                //displays an hourglass cursor
                Cursor.Current = Cursors.WaitCursor;

                int currentIndex = currentRow.Index;

                //For traces
                logger.Debug(location + " Starting...");

                //For traces
                logger.Info(location + "Purpose: Gets the access group Id and opens the frmAccessGroupEdit form for a specific action");

                //For traces
                logger.Debug(location + "Parameters:");
                logger.Debug(location + "  action-> " + action);

                string accessGroupID = (dgvAccessGroup[0, currentIndex].Value ?? "").ToString();

                if (string.IsNullOrEmpty(accessGroupID)) { throw new Exception("A cell of the ID column of the dgvAccessGroup object is empty."); }
                
                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  accessGroupID-> " + accessGroupID);

                //if this is not the default access group
                if (accessGroupID != DEFAULT_ACCESS_GROUP)
                {
                    //For traces   
                    logger.Debug(location + "Before line: frmAccessGroupEdit _frm = new frmAccessGroupEdit(btecDB, action, accessGroupID);");
                    logger.Debug(location + "        and: DialogResult result = _frm.ShowDialog(this);");

                    //Opens the child form containing the access group definition corresponding to the access group Id (next 2 lines)
                    frmAccessGroupEdit _frm = new frmAccessGroupEdit(btecDB, action, accessGroupID);

                    DialogResult result = _frm.ShowDialog(this);
                    _frm.Dispose();

                    if (result == DialogResult.OK)
                    {
                        //For traces   
                        logger.Debug(location + "Before line: refreshDgvAccessGroup();");

                        refreshDgvAccessGroup();

                        if (action == ACTION_EDIT) { dgvAccessGroup.CurrentCell = dgvAccessGroup[0, currentIndex]; }
                    }
                }

                //For traces
                logger.Debug(location + " Ending...");

                //displays the default cursor
                Cursor.Current = Cursors.Default;
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

        //Occurs when the [Add] button is clicked
        private void btnAdd_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Add] button is clicked");

                //displays an hourglass cursor
                Cursor.Current = Cursors.WaitCursor;

                //For traces   
                logger.Debug(location + "Before line: frmAccessGroupEdit _frm = new frmAccessGroupEdit(btecDB, ACTION_ADD, null);");
                logger.Debug(location + "        and: DialogResult result = _frm.ShowDialog(this);");

                //Opens the child form with the new sort pattern Id (next 2 lines)
                frmAccessGroupEdit _frm = new frmAccessGroupEdit(btecDB, ACTION_ADD, null);

                DialogResult result = _frm.ShowDialog(this);
                _frm.Dispose();

                if (result == DialogResult.OK)
                {
                    //For traces   
                    logger.Debug(location + "Before line: refreshDgvAccessGroup();");

                    refreshDgvAccessGroup();

                    //To have the first row selected
                    if (dgvAccessGroup.Rows.Count > 0)
                    {
                        dgvAccessGroup.CurrentCell = dgvAccessGroup[0, 0];
                    }
                }

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

        //Occurs when the [Edit] button is clicked
        private void btnEdit_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Edit] button is clicked");

                DataGridViewRow currentRow = dgvAccessGroup.CurrentRow;

                //For traces   
                logger.Debug(location + "Before line: editingAccessGroup(currentRow, ACTION_EDIT);");

                editingAccessGroup(currentRow, ACTION_EDIT);

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

        //Occurs when a line in the dgvAccessGroup object is double-clicked
        private void dgvAccessGroup_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                logger.Info(location + "Purpose: Occurs when a line in the dgvAccessGroup object is double-clicked");

                DataGridViewRow currentRow = (sender as DataGridView).CurrentRow;

                if (currentRow != null)
                {
                    int selectedRowCount = dgvAccessGroup.SelectedRows.Count;

                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  selectedRowCount-> " + selectedRowCount.ToString());

                    if (selectedRowCount == 1)
                    {
                        //For traces   
                        logger.Debug(location + "Before line: editingAccessGroup(row, ACTION_EDIT);");

                        editingAccessGroup(currentRow, ACTION_EDIT);
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

        //Occurs when the [Remove] button is clicked
        private void btnRemove_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Remove] button is clicked");

                if (dgvAccessGroup.CurrentRow != null)
                {
                    //displays an hourglass cursor
                    Cursor.Current = Cursors.WaitCursor;

                    bool isAccessGroupDeleted = false;
                    bool isCancelled = false;
                    int selectedRowCount = dgvAccessGroup.SelectedRows.Count;

                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  selectedRowCount-> " + selectedRowCount.ToString());

                    if (selectedRowCount == 1)
                    {
                        isAccessGroupDeleted = deleteAccessGroup(0, 
                                                                 "titleDeleteOneAccessGroup", 
                                                                 MessageBoxButtons.OKCancel,
                                                                 ref isCancelled);
                    }
                    else
                    {
                        bool buffer = false;

                        //reverse the for loop so the access groups will be display in the good order
                        for (int i = selectedRowCount - 1; i >= 0; i--)
                        {
                            buffer = deleteAccessGroup(i,
                                                       "titleDeleteMultipleAccessGroup",
                                                       MessageBoxButtons.YesNoCancel,
                                                       ref isCancelled);

                            if (buffer) isAccessGroupDeleted = true;

                            if (isCancelled) break;
                        }
                    }

                    if (isAccessGroupDeleted)
                    {
                        //For traces   
                        logger.Debug(location + "Before line: refreshDgvAccessGroup();");

                        refreshDgvAccessGroup();
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

        //Manage the deletion of an access group
        private bool deleteAccessGroup(int rowID, 
                                       string msgTitle, 
                                       MessageBoxButtons buttonsType,
                                       ref bool isCancelled)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Manage the deletion of an access group");

            string message;
            bool isAccessGroupDeleted = false;

            string accessGroupID = (dgvAccessGroup.SelectedRows[rowID].Cells[0].Value ?? "").ToString();
            if (string.IsNullOrEmpty(accessGroupID)) { throw new Exception("A cell of the ID column of the dgvAccessGroup object is empty."); }

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  accessGroupID-> " + accessGroupID);

            //For traces   
            logger.Debug(location + "Before line: AppUserProfile aup = new AppUserProfile(btecDB);");
            logger.Debug(location + "        and: if (aup.getIfFindAccessGroupID(accessGroupID))");

            AppUserProfile aup = new AppUserProfile(btecDB);

            if (aup.getIfFindAccessGroupID(accessGroupID))
            {
                message = string.Format(rm.GetString("msgAccessGroupIDExists"), accessGroupID);

                MessageBox.Show(message,
                                rm.GetString("titleImpossibleToRemove"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {
                //For traces   
                logger.Debug(location + " After line: if (aup.getIfFindAccessGroupID(accessGroupID))...else");

                string msgPart1 = rm.GetString("msgDeleteAccessGroup1of2") + "\n" + "\n";
                string msgPart2 = "\n" + "\n" + rm.GetString("msgDeleteAccessGroup2of2");

                string description = (dgvAccessGroup.SelectedRows[rowID].Cells[1].Value ?? "").ToString();

                message = msgPart1 + accessGroupID + "  " + description + msgPart2;

                DialogResult result = MessageBox.Show(message,
                                     rm.GetString(msgTitle),
                                     buttonsType,
                                     MessageBoxIcon.Exclamation);

                switch (result)
                {
                    case DialogResult.Yes:
                    case DialogResult.OK:
                        //For traces   
                        logger.Debug(location + "Before line: AccessGroupDef ag = new AccessGroupDef(btecDB);");
                        logger.Debug(location + "        and: ag.deleteAccessGroup(accessGroupID);");

                        AccessGroupDef ag = new AccessGroupDef(btecDB);
                        //delete the access group
                        ag.deleteAccessGroup(accessGroupID);

                        isAccessGroupDeleted = true;
                        break;
                    case DialogResult.Cancel:
                        isCancelled = true;
                        break;
                }
            }

            //For traces
            logger.Debug(location + " Ending...");
 
            return isAccessGroupDeleted;
        }

        //Occurs when the [Copy] button is clicked
        private void btnCopy_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: OOccurs when the [Copy] button is clicked");

                //displays an hourglass cursor
                Cursor.Current = Cursors.WaitCursor;

                DataGridViewRow currentRow = (DataGridViewRow)dgvAccessGroup.CurrentRow;
                int currentIndex = currentRow.Index;

                string accessGroupID = (dgvAccessGroup[0, currentIndex].Value ?? "").ToString();

                if (string.IsNullOrEmpty(accessGroupID)) { throw new Exception("A cell of the ID column of the dgvAccessGroup object is empty."); }

                //For traces   
                logger.Debug(location + "Before line: frmAccessGroupEdit _frm = new frmAccessGroupEdit(btecDB, ACTION_COPY, accessGroupID);");
                logger.Debug(location + "        and: DialogResult result = _frm.ShowDialog(this);");

                //Opens the child form with the corresponding access group Id (next 2 lines)
                frmAccessGroupEdit _frm = new frmAccessGroupEdit(btecDB, ACTION_COPY, accessGroupID);

                DialogResult result = _frm.ShowDialog(this);
                _frm.Dispose();

                if (result == DialogResult.OK)
                {
                    //For traces   
                    logger.Debug(location + "Before line: refreshDgvAccessGroup();");

                    refreshDgvAccessGroup();
                }

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

        //Occurs when a cell of the dgvAccessGroup object is selected
        private void dgvAccessGroup_CurrentCellChanged(object sender, EventArgs e)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                enableEditRemoveCopyButtons();
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