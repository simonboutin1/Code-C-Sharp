//2012-07 Simon Boutin MANTIS# 16396 : Convert LBTables from VB to C#

using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using BancTec.PCR2P.Core.DatabaseModel.Administration;
using NetCommunTools;

namespace Administration
{
    public partial class frmSortPatternDef : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmSortPatternDef).Name;

        const string ACTION_EDIT = "Edit";
        const string ACTION_ADD = "Add";
        const string ACTION_REMOVE = "Remove";

        ILoggerBtec logger;
        ResourceManager rm;
        IBtecDB btecDB;

        public frmSortPatternDef(IBtecDB btecDBParent)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmSortPatternDef));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmSortPatternDef));
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
        private void frmAddressDef_Load(object sender, EventArgs e)
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

                //Specifies the number of desired columns for the dgvSortPattern object
                dgvSortPattern.ColumnCount = 4;

                string[] columnHeaderText = {rm.GetString("colIdNo"),
                                             rm.GetString("colDescription"),
                                             string.Format(rm.GetString("colPhysicalPockets"), "\n"),
                                             string.Format(rm.GetString("colVirtualPockets"), "\n")};

                //Specifies the dgvSortPattern width to calculate the columnns width
                double dgvSortPatternWidth = dgvSortPattern.Width;

                //Specifies all the columns width of the dgvSortPattern object 
                //The sum of all the constants must equal 1
                double[] columnWidth = {dgvSortPatternWidth * 0.080,
                                        dgvSortPatternWidth * 0.638,
                                        dgvSortPatternWidth * 0.141,
                                        dgvSortPatternWidth * 0.141};

                for (int i = 0; i < dgvSortPattern.ColumnCount; i++)
                {
                    dgvSortPattern.Columns[i].HeaderText = columnHeaderText[i];
                    dgvSortPattern.Columns[i].Width = (int)Math.Round(columnWidth[i]);
                    dgvSortPattern.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //specifies that we can not sort this column
                    dgvSortPattern.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                dgvSortPattern.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvSortPattern.Columns[0].ValueType = typeof(string);

                dgvSortPattern.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvSortPattern.Columns[1].ValueType = typeof(string);

                for (int i = 2; i <= 3; i++)
                {
                    dgvSortPattern.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvSortPattern.Columns[i].ValueType = typeof(string);
                }

                //For traces   
                logger.Debug(location + "Before line: refreshDgvSortPattern();");

                refreshDgvSortPattern();

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

        //Refreshes the dgvSortPattern object with the most recent data
        public void refreshDgvSortPattern()
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
                logger.Info(location + "Purpose: Refreshes the dgvSortPattern object with the most recent data");

                //displays an hourglass cursor
                Cursor.Current = Cursors.WaitCursor;

                //For traces   
                logger.Debug(location + "Before line: dgvSortPattern.Rows.Clear();");

                //Erases all data from the dgvSortPattern object
                dgvSortPattern.Rows.Clear();

                //For traces   
                logger.Debug(location + "Before line: SortPatternDef spd = new SortPatternDef(btecDB);");
                logger.Debug(location + "        and: SortPatternDefData[] spData = spd.GetSortPatternDefWithPocketsCount();");

                SortPatternDef sp = new SortPatternDef(btecDB);
                SortPatternDefData[] spData = sp.GetSortPatternDefWithPocketsCount();

                if (spData != null)
                {
                    //For traces   
                    logger.Debug(location + "Before line: for (int value = 0; value < spData.Count<SortPatternDefData>(); value++)");

                    //Fill the dgvSortPattern object with values
                    for (int i = 0; i < spData.Count<SortPatternDefData>(); i++)
                    {
                        //Adds a row of records in the dgvSortPattern object
                        dgvSortPattern.Rows.Add(spData[i].GetStringValue(spData[i].SORT_PATTERN_ID),
                                                spData[i].GetStringValue(spData[i].DESCRIPTION),
                                                spData[i].GetStringValue(spData[i].PHYSICAL_POCKETS),
                                                spData[i].GetStringValue(spData[i].VIRTUAL_POCKETS));
                    }
                }

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  dgvSortPattern.Rows.Count-> " + dgvSortPattern.Rows.Count.ToString());

                //To be sure to have a current row before calling enableEditRemoveWorkTypeButtons()
                if (dgvSortPattern.Rows.Count > 0)
                {
                    dgvSortPattern.CurrentCell = dgvSortPattern[0, 0];
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

                DataGridViewRow currentRow = dgvSortPattern.CurrentRow;

                //For traces   
                logger.Debug(location + "Before line: editingSortPattern(row, ACTION_REMOVE);");

                editingSortPattern(currentRow, ACTION_REMOVE);

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
                logger.Debug(location + "Before line: SortPattern spd = new SortPattern(btecDB);");

                SortPatternDef sp = new SortPatternDef(btecDB);

                long sortPatternID = sp.getMaxSortPatternDefID() + 1;

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  sortPatternID-> " + sortPatternID.ToString());

                //For traces   
                logger.Debug(location + "Before line: frmSortPatternEdit _frm = new frmSortPatternEdit(btecDB, ACTION_ADD, sortPatternID);");
                logger.Debug(location + "        and: DialogResult result = _frm.ShowDialog(this);");

                //Opens the child form with the new sort pattern Id (next 2 lines)
                frmSortPatternEdit _frm = new frmSortPatternEdit(btecDB, ACTION_ADD, sortPatternID);

                DialogResult result = _frm.ShowDialog(this);
                _frm.Dispose();

                if (result == DialogResult.OK)
                {
                    //For traces   
                    logger.Debug(location + "Before line: refreshDgvSortPattern();");

                    refreshDgvSortPattern();

                    dgvSortPattern.CurrentCell = dgvSortPattern[0, dgvSortPattern.Rows.Count - 1];
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

                DataGridViewRow currentRow = dgvSortPattern.CurrentRow;

                //For traces   
                logger.Debug(location + "Before line: editingSortPattern(row, ACTION_EDIT);");

                editingSortPattern(currentRow, ACTION_EDIT);

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

        //Occurs when a line in the dgvSortPattern object is double-clicked
        private void dgvSortPattern_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                logger.Info(location + "Purpose: Occurs when a line in the dgvSortPattern object is double-clicked");

                DataGridViewRow currentRow = (sender as DataGridView).CurrentRow;

                if (currentRow != null)
                {
                    //For traces   
                    logger.Debug(location + "Before line: editingSortPattern(row, ACTION_EDIT);");

                    editingSortPattern(currentRow, ACTION_EDIT);
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

        //Gets the sort pattern Id and opens the frmSortPatternEdit form for a specific action
        private void editingSortPattern(DataGridViewRow currentRow, string action)
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
                logger.Info(location + "Purpose: Gets the sort pattern Id and opens the frmSortPatternEdit form for a specific action");

                //For traces
                logger.Debug(location + "Parameters:");
                logger.Debug(location + "  action-> " + action);

                string strSortPatternID = (dgvSortPattern[0, currentIndex].Value ?? "").ToString();
                long sortPatternID = 0;

                if (string.IsNullOrEmpty(strSortPatternID)) { throw new Exception("A cell of the Id column of the dgvSortPattern object is empty."); }
                else { sortPatternID = long.Parse(strSortPatternID); }

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  sortPatternID-> " + sortPatternID.ToString());

                //For traces   
                logger.Debug(location + "Before line: frmSortPatternEdit _frm = new frmSortPatternEdit(btecDB, action, sortPatternID);");
                logger.Debug(location + "        and: DialogResult result = _frm.ShowDialog(this);");

                //Opens the child form containing the sort pattern pockets corresponding to the sort pattern Id (next 2 lines)
                frmSortPatternEdit _frm = new frmSortPatternEdit(btecDB, action, sortPatternID);

                DialogResult result = _frm.ShowDialog(this);
                _frm.Dispose();

                if (result == DialogResult.OK)
                {
                    //For traces   
                    logger.Debug(location + "Before line: refreshDgvSortPattern();");

                    refreshDgvSortPattern();

                    if (action == ACTION_EDIT) { dgvSortPattern.CurrentCell = dgvSortPattern[0, currentIndex]; }
                }

                //For traces
                logger.Debug(location + " Ending...");

                //displays the default cursor
                Cursor.Current = Cursors.Default;
            }
        }

        //Enables or disables the [Edit], [Remove] and [Copy] buttons
        private void enableEditRemoveCopyButtons()
        {
            if (dgvSortPattern.CurrentRow == null)
            {
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
                btnCopy.Enabled = false;
            }
            else
            {
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
                btnCopy.Enabled = true;
            }
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
                logger.Info(location + "Purpose: Occurs when the [Copy] button is clicked");

                DialogResult result;

                result = MessageBox.Show(rm.GetString("msgCopySortPattern"),
                                         rm.GetString("titleCopySortPattern"),
                                         MessageBoxButtons.OKCancel,
                                         MessageBoxIcon.None);

                switch (result)
                {
                    case DialogResult.OK:
                        //displays an hourglass cursor
                        Cursor.Current = Cursors.WaitCursor;

                        DataGridViewRow currentRow = (DataGridViewRow)dgvSortPattern.CurrentRow;
                        int currentIndex = currentRow.Index;

                        string strSortPatternID = (dgvSortPattern[0, currentIndex].Value ?? "").ToString();
                        long sortPatternID = 0;

                        if (string.IsNullOrEmpty(strSortPatternID)) { throw new Exception("A cell of the Id column of the dgvSortPattern object is empty."); }
                        else { sortPatternID = long.Parse(strSortPatternID); }

                        //For traces
                        logger.Debug(location + "Values:");
                        logger.Debug(location + "  currentIndex-> " + currentIndex.ToString());
                        logger.Debug(location + "  sortPatternID-> " + sortPatternID.ToString());

                        //For traces   
                        logger.Debug(location + "Before line: long newSortPatternID = copySortPatternDef(sortPatternID);");

                        //Copies for the SORT_PATTERN_DEFINITION table
                        long newSortPatternID = copySortPatternDef(sortPatternID);

                        //For traces
                        logger.Debug(location + "Values:");
                        logger.Debug(location + "  newSortPatternID-> " + newSortPatternID.ToString());

                        //For traces   
                        logger.Debug(location + "Before line: copySortPatternPocket(sortPatternID, newSortPatternID);");

                        //Copies for the SORT_PATTERN_POCKET table
                        copySortPatternPocket(sortPatternID, newSortPatternID);

                        //For traces   
                        logger.Debug(location + "Before line: copySortPatternPocketCondAndListValues(sortPatternID, newSortPatternID);");

                        //Copies for the SORT_PATTERN_POCKET_CONDITION and LIST_VALUES tables
                        copySortPatternPocketCondAndListValues(sortPatternID, newSortPatternID);

                        //For traces   
                        logger.Debug(location + "Before line: refreshDgvSortPattern();");

                        refreshDgvSortPattern();

                        //Selects the last row which represent the copy
                        dgvSortPattern.CurrentCell = dgvSortPattern[0, dgvSortPattern.Rows.Count - 1];

                        //For traces
                        logger.Debug(location + " Ending...");

                        //displays the default cursor
                        Cursor.Current = Cursors.Default;

                        break;
                    case DialogResult.Cancel:
                        break;
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

        //Copies a new record in the SORT_PATTERN_DEFINITION table
        private long copySortPatternDef(long sortPatternID)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Copies a new record in the SORT_PATTERN_DEFINITION table");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  sortPatternID-> " + sortPatternID.ToString());

            //For traces   
            logger.Debug(location + "Before line: SortPatternDef spd = new SortPatternDef(btecDB);");

            SortPatternDef sp = new SortPatternDef(btecDB);
            long newSortPatternID = sp.getMaxSortPatternDefID() + 1;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  newSortPatternID-> " + newSortPatternID.ToString());

            //For traces   
            logger.Debug(location + "Before line: spd.copySortPatternDefInfo(sortPatternID, newSortPatternID, rm.GetString(\"strCopyOf\"))");

            //Does the copy in the SORT_PATTERN_DEFINITION table
            sp.copySortPatternDefInfo(sortPatternID, newSortPatternID, rm.GetString("strCopyOf"));

            //For traces
            logger.Debug(location + " Ending...");

            return newSortPatternID;
        }

        //Copies new records in the SORT_PATTERN_POCKET table for physical and virtual pockets
        private void copySortPatternPocket(long sortPatternID, long newSortPatternID)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Copies new records in the SORT_PATTERN_POCKET table for physical and virtual pockets");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  sortPatternID-> " + sortPatternID.ToString());
            logger.Debug(location + "  newSortPatternID-> " + newSortPatternID.ToString());

            //For traces   
            logger.Debug(location + "Before line: SortPatternPocket spp = new SortPatternPocket(btecDB);");

            SortPatternPocket spp = new SortPatternPocket(btecDB); 
            DataTable[] dtPockets = new DataTable[2];

            long MaxUniqueID = spp.getMaxSortPatternPocketUniqueID();
            long sortPatternUniqueID;
            string strSortPatternUniqueID;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  MaxUniqueID-> " + MaxUniqueID.ToString());

            dtPockets[0] = spp.GetSortPatternPocket(sortPatternID, true);  //Physical Pockets
            dtPockets[1] = spp.GetSortPatternPocket(sortPatternID, false); //Virtual Pockets

            //For traces   
            logger.Debug(location + "Before line: for (int value = 0; value < dtPockets.Length; value++)");
            logger.Debug(location + "        and: foreach (DataRow row in dtPockets[value].Rows)");

            //Handles physical pockets and then virtual pockets
            for (int i = 0; i < dtPockets.Length; i++)
            {
                foreach (DataRow row in dtPockets[i].Rows)
                {
                    MaxUniqueID++;

                    strSortPatternUniqueID = (row["SORT_PATTERN_UNIQUE_ID"] ?? "").ToString();
                    sortPatternUniqueID = 0;

                    if (string.IsNullOrEmpty(strSortPatternUniqueID)) { throw new Exception("A field of the SORT_PATTERN_UNIQUE_ID column of the SORT_PATTERN_POCKET table is empty."); }
                    else { sortPatternUniqueID = long.Parse(strSortPatternUniqueID); }

                    //Does the copy in the SORT_PATTERN_POCKET table
                    spp.copySortPatternPocketInfo(newSortPatternID, sortPatternUniqueID, MaxUniqueID);
                }
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Copies new records in the SORT_PATTERN_POCKET_CONDITION and LIST_VALUES tables
        private void copySortPatternPocketCondAndListValues(long sortPatternID, long newSortPatternID)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Copies new records in the SORT_PATTERN_POCKET_CONDITION and LIST_VALUES tables");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  sortPatternID-> " + sortPatternID.ToString());
            logger.Debug(location + "  newSortPatternID-> " + newSortPatternID.ToString());

            //For traces   
            logger.Debug(location + "Before line: SortPatternPocketCond sppc = new SortPatternPocketCond(btecDB);");
            logger.Debug(location + "        and: DataTable dtCond = sppc.GetSortPatternPocketCond(sortPatternID);");

            SortPatternPocketCond sppc = new SortPatternPocketCond(btecDB);
            DataTable dtCond = sppc.GetSortPatternPocketCond(sortPatternID);

            long pocketNo;
            long conditionSequence;
            string strPocketNo;
            string strConditionSequence;

            //For traces   
            logger.Debug(location + "Before line: ListValues lv = new ListValues(btecDB);");

            ListValues lv = new ListValues(btecDB);

            if (dtCond != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in dtCond.Rows)");

                foreach (DataRow row in dtCond.Rows)
                {
                    strPocketNo = (row["POCKET_NO"] ?? "").ToString();
                    strConditionSequence = (row["CONDITION_SEQUENCE"] ?? "").ToString();

                    pocketNo = 0;
                    conditionSequence = 0;

                    if (string.IsNullOrEmpty(strPocketNo)) { throw new Exception("A field of the POCKET_NO column of the SORT_PATTERN_POCKET_CONDITION table is empty."); }
                    else { pocketNo = long.Parse(strPocketNo); }

                    if (string.IsNullOrEmpty(strConditionSequence)) { throw new Exception("A field of the CONDITION_SEQUENCE column of the SORT_PATTERN_POCKET_CONDITION table is empty."); }
                    else { conditionSequence = long.Parse(strConditionSequence); }

                    //Does the copy in the SORT_PATTERN_POCKET_CONDITION table
                    sppc.copySortPatternPocketCondInfo(sortPatternID, newSortPatternID, pocketNo, conditionSequence);

                    //Does the copy in the LIST_VALUES table
                    lv.copyListValuesInfo(sortPatternID, newSortPatternID, pocketNo, conditionSequence);
                }
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Occurs when a cell of the dgvSortPattern object is selected
        private void dgvSortPattern_CurrentCellChanged(object sender, EventArgs e)
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
