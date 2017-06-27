//2013-05 Simon Boutin MANTIS# 16396 : Convert LBTables from VB to C#

using System;
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
    public partial class frmAccessGroupEdit : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmAccessGroupEdit).Name;

        IBtecDB btecDB;
        string accessGroupID;
        bool isSomethingChanged = false;
        bool isChangeApply = false;
        bool isFormClosedByUser = false;

        string action;

        const string ACTION_EDIT = "Edit";
        const string ACTION_ADD = "Add";
        const string ACTION_COPY = "Copy";

        const int GROUP_ID_WIDTH = 6;
        const int GROUP_DESC_WIDTH = 60;

        public DataTable availableAccessCodesTable;
        public DataTable selectedAccessCodesTable;

        ResourceManager rm;
        ILoggerBtec logger;

        public frmAccessGroupEdit(IBtecDB btecDBParent, string actionParent, string accessGroupIDParent)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmAccessGroupEdit));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmAccessGroupEdit));
                }

                //For traces
                logger.Debug(location + " Starting...");

                //For traces
                logger.Debug(location + "Parameters:");
                logger.Debug(location + "  actionParent-> " + actionParent);
                logger.Debug(location + "  accessGroupIDParent-> " + accessGroupIDParent);

                //We must get the resource manager after setting the culture.
                rm = SingletonResourcesManager.Instance.getResourceManager();

                //For traces   
                logger.Debug(location + "Before line: InitializeComponent();");

                //Initializes all the components of the form
                InitializeComponent();

                btecDB = btecDBParent;
                action = actionParent;
                accessGroupID = accessGroupIDParent;

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
        private void frmAccessGroupEdit_Load(object sender, EventArgs e)
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

                string[] AvailableColumnHeaderText = {rm.GetString("colIdNo"),
                                                      rm.GetString("colDescription")};

                //Specifies the number of desired columns for the dgvAvailableAccessCodes object
                dgvAvailableAccessCodes.ColumnCount = 2;

                //Specifies the dgvAvailableAccessCodes width to calculate the columnns width
                double dgvAvailableAccessCodesWidth = dgvAvailableAccessCodes.Width;

                //Specifies all the columns width of the dgvAvailableAccessCodes object 
                //The sum of all the constants must equal 1
                double[] AvailableColumnWidth = {dgvAvailableAccessCodesWidth * 0.2,
                                                 dgvAvailableAccessCodesWidth * 0.8};

                for (int i = 0; i < dgvAvailableAccessCodes.ColumnCount; i++)
                    {
                    dgvAvailableAccessCodes.Columns[i].HeaderText = AvailableColumnHeaderText[i];
                    dgvAvailableAccessCodes.Columns[i].Width = (int)Math.Round(AvailableColumnWidth[i]);
                    //specifies that we can sort this column
                    dgvAvailableAccessCodes.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvAvailableAccessCodes.Columns[i].ValueType = typeof(string);
                }

                dgvAvailableAccessCodes.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAvailableAccessCodes.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAvailableAccessCodes.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvAvailableAccessCodes.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                //Force a column to be sorted by défault
                dgvAvailableAccessCodes.Sort(dgvAvailableAccessCodes.Columns[0], ListSortDirection.Ascending);

                string[] SelectedColumnHeaderText = {rm.GetString("colIdNo"),
                                                     rm.GetString("colDescription")};

                //Specifies the number of desired columns for the dgvSelectedAccessCodes object
                dgvSelectedAccessCodes.ColumnCount = 2;

                //Specifies the dgvSelectedAccessCodes width to calculate the columnns width
                double dgvSelectedAccessCodesWidth = dgvSelectedAccessCodes.Width;

                //Specifies all the columns width of the dgvSelectedAccessCodes object 
                //The sum of all the constants must equal 1
                double[] SelectedColumnWidth = {dgvSelectedAccessCodesWidth * 0.2,
                                                dgvSelectedAccessCodesWidth * 0.8};

                for (int i = 0; i < dgvSelectedAccessCodes.ColumnCount; i++)
                {
                    dgvSelectedAccessCodes.Columns[i].HeaderText = SelectedColumnHeaderText[i];
                    dgvSelectedAccessCodes.Columns[i].Width = (int)Math.Round(SelectedColumnWidth[i]);
                    //specifies that we can sort this column
                    dgvSelectedAccessCodes.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvSelectedAccessCodes.Columns[i].ValueType = typeof(string);
                }

                dgvSelectedAccessCodes.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvSelectedAccessCodes.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvSelectedAccessCodes.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvSelectedAccessCodes.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                //Force a column to be sorted by défault
                dgvSelectedAccessCodes.Sort(dgvSelectedAccessCodes.Columns[0], ListSortDirection.Ascending);

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
            logger.Debug(location + "Before line: AccessGroupDef spd = new AccessGroupDef(btecDB);");
            logger.Debug(location + "        and: AccessGroupDefData agData = spd.GetAccessGroupDefInfo(accessGroupID);");

            AccessGroupDef sp = new AccessGroupDef(btecDB);
            AccessGroupDefData agData = sp.GetAccessGroupDefInfo(accessGroupID);

            txtID.TextChanged -= new EventHandler(txtID_TextChanged);
            txtDescription.TextChanged -= new EventHandler(txtDescription_TextChanged);

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  action-> " + action);

            //adjusts the form appearance depending on the action specified in the parent form
            switch (action)
            {
                case ACTION_EDIT:
                    this.Text = rm.GetString("titleEditAccessGroup");

                    txtID.Text = accessGroupID;
                    EnableControl.TextBox(txtID, false);

                    txtDescription.Text = agData.GetStringValue(agData.DESCRIPTION);
                    break;
                case ACTION_ADD:
                    this.Text = rm.GetString("titleAddAccessGroup");

                    txtID.Text = null;
                    EnableControl.TextBox(txtID, true);

                    txtDescription.Text = null;
                    break;
                case ACTION_COPY:
                    this.Text = rm.GetString("titleCopyAccessGroup");

                    txtID.Text = null;
                    EnableControl.TextBox(txtID, true);

                    txtDescription.Text = null;
                    break;
            }

            txtID.TextChanged += new EventHandler(txtID_TextChanged);
            txtDescription.TextChanged += new EventHandler(txtDescription_TextChanged);

            //For traces   
            logger.Debug(location + "Before line: refreshAvailableAccessCodesData();");

            refreshAvailableAccessCodesData();

            //For traces   
            logger.Debug(location + "Before line: refreshDgvAvailableAccessCodes();");

            refreshDgvAvailableAccessCodes();

            //For traces   
            logger.Debug(location + "Before line: refreshSelectedAccessCodesData();");

            refreshSelectedAccessCodesData();

            //For traces   
            logger.Debug(location + "Before line: refreshDgvSelectedAccessCodes();");

            refreshDgvSelectedAccessCodes();

            isSomethingChanged = false;
            btnOK.Enabled = false;

            //Enables or disables the 4 transfer buttons
            enableTransferButtons();

            //For traces
            logger.Debug(location + " Ending...");

            //displays the default cursor
            Cursor.Current = Cursors.Default;
        }

        //Refreshes the dgvAvailableAccessCodes object with the most recent data
        public void refreshDgvAvailableAccessCodes()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the dgvAvailableAccessCodes object with the most recent data");

            //Erases all data from the dgvAvailableAccessCodes object
            dgvAvailableAccessCodes.Rows.Clear();

            //Get the current sorted column and its direction (next 2 lines)
            int currentSortedColumnIndex = dgvAvailableAccessCodes.SortedColumn.Index;
            ListSortDirection direction = (dgvAvailableAccessCodes.SortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);

            if (availableAccessCodesTable != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in availableAccessCodesTable.Rows)");

                //Fill the availableAccessCodesTable object with values
                foreach (DataRow row in availableAccessCodesTable.Rows)
                {
                    //Adds a row of records in the dgvAvailableAccessCodes object
                    dgvAvailableAccessCodes.Rows.Add((row["ACCESS_CODE_ID"] ?? "").ToString(),
                                                        (row["DESCRIPTION"] ?? "").ToString());
                }
            }

            //Sort the dgvAvailableAccessCodes object like before
            dgvAvailableAccessCodes.Sort(dgvAvailableAccessCodes.Columns[currentSortedColumnIndex], direction);

            //Put the focus on the fisrt row
            if (dgvAvailableAccessCodes.Rows.Count > 0)
            {
                dgvAvailableAccessCodes.CurrentCell = dgvAvailableAccessCodes[0, 0];
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Refreshes the available access codes DataTable with the most recent data
        void refreshAvailableAccessCodesData()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the available access codes DataTable with the most recent data");

            //For traces   
            logger.Debug(location + "Before line: AvailableAccessCodes sac = new AvailableAccessCodes(btecDB);");
            logger.Debug(location + "        and: availableAccessCodesTable = sac.GetAvailableAccessCodes(accessGroupID);");

            AvailableAccessCodes sac = new AvailableAccessCodes(btecDB);

            availableAccessCodesTable = sac.GetAvailableAccessCodes(accessGroupID);

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Refreshes the dgvSelectedAccessCodes object with the most recent data
        public void refreshDgvSelectedAccessCodes()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the dgvSelectedAccessCodes object with the most recent data");

            //Erases all data from the dgvSelectedAccessCodes object
            dgvSelectedAccessCodes.Rows.Clear();

            //Get the current sorted column and its direction (next 2 lines)
            int currentSortedColumnIndex = dgvSelectedAccessCodes.SortedColumn.Index;
            ListSortDirection direction = (dgvSelectedAccessCodes.SortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);

            if (selectedAccessCodesTable != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in selectedAccessCodesTable.Rows)");

                //Fill the selectedAccessCodesTable object with values
                foreach (DataRow row in selectedAccessCodesTable.Rows)
                {
                    //Adds a row of records in the dgvSelectedAccessCodes object
                    dgvSelectedAccessCodes.Rows.Add((row["ACCESS_CODE_ID"] ?? "").ToString(),
                                                    (row["DESCRIPTION"] ?? "").ToString());
                }
            }

            //Sort the dgvSelectedAccessCodes object like before
            dgvSelectedAccessCodes.Sort(dgvSelectedAccessCodes.Columns[currentSortedColumnIndex], direction);

            //Put the focus on the fisrt row
            if (dgvSelectedAccessCodes.Rows.Count > 0)
            {
                dgvSelectedAccessCodes.CurrentCell = dgvSelectedAccessCodes[0, 0];
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Refreshes the selected access codes DataTable with the most recent data
        void refreshSelectedAccessCodesData()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the selected access codes DataTable with the most recent data");

            //For traces   
            logger.Debug(location + "Before line: SelectedAccessCodes sac = new SelectedAccessCodes(btecDB);");
            logger.Debug(location + "        and: selectedAccessCodesTable = sac.GetSelectedAccessCodes(accessGroupID);");

            SelectedAccessCodes sac = new SelectedAccessCodes(btecDB);

            selectedAccessCodesTable = sac.GetSelectedAccessCodes(accessGroupID);

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Do some adjustements after a transfer of access codes
        private void adjustAfterTransfer()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Do some adjustements after a transfer of access codes");

            //For traces   
            logger.Debug(location + "Before line: refreshDgvAvailableAccessCodes();");
            logger.Debug(location + "        and: refreshDgvSelectedAccessCodes();");

            refreshDgvAvailableAccessCodes();
            refreshDgvSelectedAccessCodes();

            isSomethingChanged = true;

            //For traces   
            logger.Debug(location + "Before line: bool isFormValidated = validateForm();");

            bool isFormValidated = validateForm();

            btnOK.Enabled = isFormValidated;

            //Enables or disables the 4 transfer buttons
            enableTransferButtons();

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Transfer an available access code to the selected group
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
                logger.Info(location + "Purpose: Transfer an available access code to the selected group");

                string currentAccessCode = dgvAvailableAccessCodes.CurrentRow.Cells[0].Value.ToString();
                if (string.IsNullOrEmpty(currentAccessCode)) { throw new Exception("A cell of the ID column of the dgvAvailableAccessCodes object is empty."); }

                DataRow currentTableRowAvailable = availableAccessCodesTable.Select("ACCESS_CODE_ID = '" + currentAccessCode + "'").First();

                //For traces   
                logger.Debug(location + "Before line: selectedAccessCodesTable.ImportRow(currentTableRowAvailable);");
                logger.Debug(location + "        and: availableAccessCodesTable.Rows.Remove(currentTableRowAvailable);");

                //Add the current row
                selectedAccessCodesTable.ImportRow(currentTableRowAvailable);

                //Remove the current row
                availableAccessCodesTable.Rows.Remove(currentTableRowAvailable);

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

        //Transfer a selected access code to the available group
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
                logger.Info(location + "Purpose: Transfer a selected access code to the available group");

                string currentAccessCode = dgvSelectedAccessCodes.CurrentRow.Cells[0].Value.ToString();
                if (string.IsNullOrEmpty(currentAccessCode)) { throw new Exception("A cell of the ID column of the dgvSelectedAccessCodes object is empty."); }

                DataRow currentTableRowSelected = selectedAccessCodesTable.Select("ACCESS_CODE_ID = '" + currentAccessCode + "'").First();

                //For traces   
                logger.Debug(location + "Before line: availableAccessCodesTable.ImportRow(currentTableRowSelected);");
                logger.Debug(location + "        and: selectedAccessCodesTable.Rows.Remove(currentTableRowSelected);");

                //Add the current row
                availableAccessCodesTable.ImportRow(currentTableRowSelected);

                //Remove the current row
                selectedAccessCodesTable.Rows.Remove(currentTableRowSelected);

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

        //Transfer all available access codes to the selected group
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
                logger.Info(location + "Purpose: Transfer all available access codes to the selected group");

                if (availableAccessCodesTable != null)
                {
                    //For traces   
                    logger.Debug(location + "Before line: foreach (DataRow row in availableAccessCodesTable.Rows)");

                    foreach (DataRow row in availableAccessCodesTable.Rows)
                    {
                        //Add the current row
                        selectedAccessCodesTable.ImportRow(row);
                    }
                }

                availableAccessCodesTable.Clear();

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

        //Transfer all selected access codes to the available group
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
                logger.Info(location + "Purpose: Transfer all selected access codes to the available group");

                if (selectedAccessCodesTable != null)
                {
                    //For traces   
                    logger.Debug(location + "Before line: foreach (DataRow row in selectedAccessCodesTable.Rows)");

                    foreach (DataRow row in selectedAccessCodesTable.Rows)
                    {
                        //Add the current row
                        availableAccessCodesTable.ImportRow(row);
                    }
                }

                selectedAccessCodesTable.Clear();

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
            btnMoveOneRight.Enabled = (dgvAvailableAccessCodes.CurrentRow == null ? false : true);
        }

        //Enable or disable the MoveOneLeft button
        private void enableMoveOneLeftButton()
        {
            btnMoveOneLeft.Enabled = (dgvSelectedAccessCodes.CurrentRow == null ? false : true);
        }

        //Enable or disable the MoveAllRight button
        private void enableMoveAllRightButton()
        {
            btnMoveAllRight.Enabled = (dgvAvailableAccessCodes.RowCount == 0 ? false : true);
        }

        //Enable or disable the MoveAllLeft button
        private void enableMoveAllLeftButton()
        {
            btnMoveAllLeft.Enabled = (dgvSelectedAccessCodes.RowCount == 0 ? false : true);
        }

        //Occurs when the text is changed in the txtDescription TextBox
        private void txtDescription_TextChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the text is changed in the txtDescription TextBox");

                isSomethingChanged = true;

                //For traces   
                logger.Debug(location + "Before line: bool isFormValidated = validateForm();");

                bool isFormValidated = validateForm();

                btnOK.Enabled = isFormValidated;

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

        //Occurs when the text is changed in the txtID TextBox
        private void txtID_TextChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the text is changed in the txtID TextBox");

                isSomethingChanged = true;

                //For traces   
                logger.Debug(location + "Before line: bool isFormValidated = validateForm();");

                bool isFormValidated = validateForm();

                btnOK.Enabled = isFormValidated;

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

            string buffer;

            //To be sure that the value doesn't exceed the table field size (next 2 lines)
            buffer = txtID.Text.Trim();
            string groupID = buffer.Substring(0, Math.Min(buffer.Length, GROUP_ID_WIDTH));

            //To be sure that the value doesn't exceed the table field size (next 2 lines)
            buffer = txtDescription.Text.Trim();
            string groupDescription = buffer.Substring(0, Math.Min(buffer.Length, GROUP_DESC_WIDTH));

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  groupID-> " + groupID);
            logger.Debug(location + "  groupDescription-> " + groupDescription);

            //For traces   
            logger.Debug(location + "Before line: AccessGroupDef ag = new AccessGroupDef(btecDB);");
            logger.Debug(location + "        and: SelectedAccessCodes sac = new SelectedAccessCodes(btecDB);");

            AccessGroupDef ag = new AccessGroupDef(btecDB);
            SelectedAccessCodes sac = new SelectedAccessCodes(btecDB);

            //For traces   
            logger.Debug(location + "Before line: ag.saveAccessGroupInfo(groupID, groupDescription);");

            //save the access group definition
            ag.saveAccessGroupInfo(groupID, groupDescription);

            //For traces   
            logger.Debug(location + "Before line: sac.deleteSelectedAccessCodes(groupID);");

            //delete the previous selected access codes
            sac.deleteSelectedAccessCodes(groupID);

            string accessCodeID;

            if (selectedAccessCodesTable != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in selectedAccessCodesTable.Rows)");

                //Saves all data from the selected access codes DataTable 
                foreach (DataRow row in selectedAccessCodesTable.Rows)
                {
                    accessCodeID = (row["ACCESS_CODE_ID"] ?? "").ToString();

                    //Saves the data in the ACCESS_GROUP_CODE table
                    sac.saveSelectedAccessGroupInfo(groupID, accessCodeID);
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

            //Validation 1
            if (string.IsNullOrEmpty(txtID.Text.Trim()) ||
                string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                return false;
            }

            //Validation 2
            string buffer = txtID.Text.Trim();
            string groupID = buffer.Substring(0, Math.Min(buffer.Length, GROUP_ID_WIDTH));

            //For traces   
            logger.Debug(location + "Before line: AccessGroupDef spd = new AccessGroupDef(btecDB);");
            logger.Debug(location + "        and: AccessGroupDefData agData = spd.GetAccessGroupDefInfo(groupID);");

            AccessGroupDef sp = new AccessGroupDef(btecDB);
            AccessGroupDefData agData = sp.GetAccessGroupDefInfo(groupID);

            if (agData != null && txtID.Enabled == true)
            {
                return false;
            }

            //Validation 3
            if (dgvSelectedAccessCodes.Rows.Count == 0)
            {
                return false;
            }

            //For traces
            logger.Debug(location + " Ending...");

            return true;
        }

        //Occurs when the form is closed
        private void frmAccessGroupEdit_FormClosing(object sender, FormClosingEventArgs e)
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

                        DialogResult selectButton = MessageBox.Show(rm.GetString("MsgQuitWithoutSaving"), rm.GetString("titleQuitWithoutSaving"), MessageBoxButtons.YesNo, MessageBoxIcon.None);

                        //For traces
                        logger.Debug(location + "Values:");
                        logger.Debug(location + "  result-> " + selectButton.ToString());

                        switch (selectButton)
                        {
                            case DialogResult.Yes:
                                //For the parent form knows that there was no change in the child data
                                this.DialogResult = (isChangeApply ? DialogResult.OK : DialogResult.Cancel);
                                //Closes the form
                                break;
                            case DialogResult.No:
                                //Doesn't close the form
                                e.Cancel = true;
                                break;
                        }
                    }
                }

                //For the parent form knows if there was a change in the child data
                this.DialogResult = (isChangeApply ? DialogResult.OK : DialogResult.Cancel);
 
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

                //For the parent form knows that there was change in the child data
                this.DialogResult = (isChangeApply ? DialogResult.OK : DialogResult.Cancel);
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

        //Do the txtID validation when this object lost its focus
        private void txtID_Validating(object sender, CancelEventArgs e)
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
                logger.Info(location + "Purpose: Do the txtID validation when this object lost its focus");

                string buffer = txtID.Text.Trim();
                string groupID = buffer.Substring(0, Math.Min(buffer.Length, GROUP_ID_WIDTH));

                //For traces   
                logger.Debug(location + "Before line: AccessGroupDef spd = new AccessGroupDef(btecDB);");
                logger.Debug(location + "        and: AccessGroupDefData agData = spd.GetAccessGroupDefInfo(groupID);");

                AccessGroupDef sp = new AccessGroupDef(btecDB);
                AccessGroupDefData agData = sp.GetAccessGroupDefInfo(groupID);

                if (agData != null && txtID.Enabled == true)
                {
                    //Informs the user that this access group ID already exists
                    MessageBox.Show(rm.GetString("msgGroupIdExists"),
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

                            //For the parent form knows that there was a change in the child data
                            isChangeApply = true;

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

        //Allow to format the display of the dgvAvailableAccessCodes columns
        private void dgvAvailableAccessCodes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
                logger.Info(location + "Purpose: Allow to format the display of the dgvAvailableAccessCodes columns");

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

        //Allow to format the display of the dgvSelectedAccessCodes columns
        private void dgvSelectedAccessCodes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
                logger.Info(location + "Purpose: Allow to format the display of the dgvSelectedAccessCodes columns");

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
    }
}