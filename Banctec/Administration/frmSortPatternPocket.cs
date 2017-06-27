using System;
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
    public partial class frmSortPatternPocket : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmSortPatternPocket).Name;

        const string ACTION_EDIT = "Edit";
        const string ACTION_ADD = "Add";
        const string ACTION_REMOVE = "Remove";

        IBtecDB btecDB;
        ResourceManager rm;
        ILoggerBtec logger;

        frmSortPatternEdit parentForm;
        string action;
        long sortPatternID;
        long sortPatternUniqueID;
        string selectedPocketsRadioButton;
        long pocketID;
        DataTable pocketsTable = null;

        bool isSomethingChanged = false;
        bool isFormClosedByUser = false;

        public frmSortPatternPocket(frmSortPatternEdit parent, IBtecDB btecDBParent, string actionParent, long sortPatternIDParent)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmSortPatternPocket));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmSortPatternPocket));
                }

                //For traces
                logger.Debug(location + " Starting...");

                //For traces
                logger.Debug(location + "Parameters:");
                logger.Debug(location + "  actionParent-> " + actionParent);
                logger.Debug(location + "  workTypeIDParent-> " + sortPatternIDParent.ToString());

                //We must get the resource manager after setting the culture.
                rm = SingletonResourcesManager.Instance.getResourceManager();

                //For traces   
                logger.Debug(location + "Before line: InitializeComponent();");

                //Initializes all the components of the form
                InitializeComponent();

                parentForm = parent;
                btecDB = btecDBParent;
                action = actionParent;
                sortPatternID = sortPatternIDParent;
                selectedPocketsRadioButton = parentForm.getSelectedPocketsRadioButton();

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  selectedPocketsRadioButton-> " + selectedPocketsRadioButton);

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
        private void frmSortPatternPocket_Load(object sender, EventArgs e)
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

                //For traces   
                logger.Debug(location + "Before line: EndPoint ep = new EndPoint(btecDB);");

                EndPoint ep = new EndPoint(btecDB);
                DataTable endPointTable = ep.GetIdAndDescription();
            
                //To add a blank line in the cboEndPoint object list
                endPointTable.Rows.InsertAt(endPointTable.NewRow(), 0);
            
                cboEndPoint.DataSource = endPointTable;
                cboEndPoint.DisplayMember = "DESCNO";
                cboEndPoint.ValueMember = "END_POINT_ID";

                switch (selectedPocketsRadioButton)
                {
                    case frmSortPatternEdit.RADIO_PHYSICAL:
                        pocketsTable = parentForm.physicalPocketsTable; break;
                    case frmSortPatternEdit.RADIO_VIRTUAL:
                        pocketsTable = parentForm.virtualPocketsTable; break;
                }

                //For traces   
                logger.Debug(location + "Before line: fillCboType();");

                fillCboType();

                switch (action)
                {
                    case ACTION_EDIT:
                        this.Text = rm.GetString("editPocket");
                        btnAction.Text = rm.GetString("btnOK");
                        break;
                    case ACTION_ADD:
                        this.Text = rm.GetString("addPocket");
                        btnAction.Text = rm.GetString("btnAdd");
                        break;
                    case ACTION_REMOVE:
                        this.Text = rm.GetString("removePocket");
                        btnAction.Text = rm.GetString("btnRemove");
                        break;
                }

                if (action == ACTION_ADD)
                {
                    //For traces   
                    logger.Debug(location + "Before line: initializeNewPocket()");

                    initializeNewPocket();
                }    
                else
                {
                    int currentIndex = parentForm.dgvPockets.CurrentRow.Index;

                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  currentIndex-> " + currentIndex.ToString());

                    //For traces   
                    logger.Debug(location + "Before line: setPocketFields(currentIndex);");

                    setPocketFields(currentIndex);
                }

                //To detect a change in the form (next 5 lines)
                txtDescription.TextChanged += new EventHandler(this.somethingChanged);
                cboType.TextChanged += new EventHandler(this.somethingChanged);
                txtPriority.TextChanged += new EventHandler(this.somethingChanged);
                cboEndPoint.TextChanged += new EventHandler(this.somethingChanged);
                txtPhysicalPocketDef.TextChanged += new EventHandler(this.somethingChanged);
 
                EnableControl.TextBox(txtPocketId, false);
            
                if (action == ACTION_REMOVE)
                {
                    EnableControl.TextBox(txtDescription, false);
                    EnableControl.ComboBox(cboType, false);
                    EnableControl.TextBox(txtPriority, false);
                    EnableControl.ComboBox(cboEndPoint, false);
                    EnableControl.TextBox(txtPhysicalPocketDef, false);
                }
                else
                {
                    EnableControl.TextBox(txtDescription, true);
                    EnableControl.ComboBox(cboType, true);
                    EnableControl.TextBox(txtPriority, true);
                    EnableControl.ComboBox(cboEndPoint, true);

                    switch (selectedPocketsRadioButton)
                    {
                        case frmSortPatternEdit.RADIO_PHYSICAL:
                            EnableControl.TextBox(txtPhysicalPocketDef, true);
                            break;
                        case frmSortPatternEdit.RADIO_VIRTUAL:
                            EnableControl.TextBox(txtPhysicalPocketDef, false);
                            break;
                    }
                }

                //For traces   
                logger.Debug(location + "Before line: enableFieldsOnCboTypeValue();");

                enableFieldsOnCboTypeValue();

                isSomethingChanged = false;

                enablePreviousNextButtons();

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

        //Assigns the form fields based on the index row of the pockets data table
        private void setPocketFields(int currentIndex)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Assigns the form fields based on the index row of the pockets data table");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  currentIndex-> " + currentIndex.ToString());

            string pocketType;
            string rejectCode;

            string strSortPatternUniqueID = (pocketsTable.Rows[currentIndex]["SORT_PATTERN_UNIQUE_ID"] ?? "").ToString();

            if (string.IsNullOrEmpty(strSortPatternUniqueID)) { throw new Exception("A value of the SORT_PATTERN_UNIQUE_ID field in the pockets data table is empty."); }
            else { sortPatternUniqueID = long.Parse(strSortPatternUniqueID); }

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  sortPatternUniqueID-> " + sortPatternUniqueID.ToString());

            string strPocketID = (pocketsTable.Rows[currentIndex]["POCKET_NO"] ?? "").ToString();

            if (string.IsNullOrEmpty(strPocketID)) { throw new Exception("A value of the POCKET_NO field in the pockets data table is empty."); }
            else { pocketID = long.Parse(strPocketID); }

            txtPocketId.Text = (pocketsTable.Rows[currentIndex]["ABS_POCKET_NO"] ?? "").ToString();
            txtDescription.Text = (pocketsTable.Rows[currentIndex]["DESCRIPTION"] ?? "").ToString();
            txtPhysicalPocketDef.Text = (pocketsTable.Rows[currentIndex]["PHYSICAL_POCKET_DEF"] ?? "").ToString();

            pocketType = (pocketsTable.Rows[currentIndex]["POCKET_TYPE"] ?? "").ToString();
            rejectCode = (pocketsTable.Rows[currentIndex]["REJECT_CODE"] ?? "").ToString();
            cboType.SelectedIndex = cboType.FindString(parentForm.getPocketTypeDesc(pocketType, rejectCode));

            cboEndPoint.SelectedValue = pocketsTable.Rows[currentIndex]["END_POINT_ID"];
            txtPriority.Text = (pocketsTable.Rows[currentIndex]["CONDITION_PRIORITY"] ?? "").ToString();

            //For traces
            logger.Debug(location + " Ending...");
        }


        //occurs when any field is modified
        private void somethingChanged(object sender, EventArgs e)
        {
            isSomethingChanged = true;
        }

        //Enables or disables some fields depending on the cboType ComboBox value 
        private void enableFieldsOnCboTypeValue()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Enables or disables some fields depending on the cboType ComboBox value");

            if (cboType.Text.Equals(parentForm.TYPE_KILL) ||
                cboType.Text.Equals(parentForm.TYPE_REHANDLE) ||
                cboType.Text.Equals(parentForm.TYPE_ELECTRONIC) ||
                cboType.Text.Equals(parentForm.TYPE_ARC) ||
                cboType.Text.Equals(parentForm.TYPE_IE) ||
                cboType.Text.Equals(parentForm.TYPE_IRD) ||
                cboType.Text.Equals(parentForm.TYPE_MISS_FREE) ||
                cboType.Text.Equals(parentForm.TYPE_MISS_FREE_REJECT))
            {
                if (action != ACTION_REMOVE) { EnableControl.ComboBox(cboEndPoint, true); }
            }
            else
            {
                cboEndPoint.SelectedValue = DBNull.Value;
                EnableControl.ComboBox(cboEndPoint, false);
            }

            switch (selectedPocketsRadioButton)
            {
                case frmSortPatternEdit.RADIO_PHYSICAL:
                    if (cboType.Text.Equals(parentForm.TYPE_KILL) ||
                        cboType.Text.Equals(parentForm.TYPE_REHANDLE) ||
                        cboType.Text.Equals(parentForm.TYPE_ELECTRONIC) ||
                        cboType.Text.Equals(parentForm.TYPE_ARC))
                    {
                        if (action != ACTION_REMOVE) { EnableControl.TextBox(txtPhysicalPocketDef, true); }
                    }
                    else
                    {
                        txtPhysicalPocketDef.Text = null;
                        EnableControl.TextBox(txtPhysicalPocketDef, false);
                    }

                    break;
                case frmSortPatternEdit.RADIO_VIRTUAL:
                    txtPhysicalPocketDef.Text = null;
                    EnableControl.TextBox(txtPhysicalPocketDef, false);
                    break;
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Occurs when the selected index of the cboType ComboBox is changed
        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the selected index of the cboType ComboBox is changed");

                //For traces   
                logger.Debug(location + "Before line: enableFieldsOnCboTypeValue();");

                enableFieldsOnCboTypeValue();

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

        //Fills the cboType ComboBox with the appropriate values
        private void fillCboType()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            const int KILL = 0;
            const int REHANDLE = 1;
            const int REJECT = 2;
            const int ELECTRONIC = 3;
            const int ARC = 4;
            const int IE = 5;
            const int IRD = 6;
            const int MISSING = 7;
            const int MISSING_REJECT = 8;

            int[] value = null;
            string[] description = null;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Fills the cboType ComboBox with the appropriate values");

            switch (selectedPocketsRadioButton)
            {
                case frmSortPatternEdit.RADIO_PHYSICAL:
                    value = new int[] { KILL,
                                        REHANDLE, 
                                        REJECT, 
                                        ELECTRONIC, 
                                        ARC, 
                                        MISSING, 
                                        MISSING_REJECT, 
                                        REJECT };

                    description = new string[] { parentForm.TYPE_KILL, 
                                                 parentForm.TYPE_REHANDLE, 
                                                 parentForm.TYPE_REJECT_SOFT, 
                                                 parentForm.TYPE_ELECTRONIC, 
                                                 parentForm.TYPE_ARC, 
                                                 parentForm.TYPE_MISS_FREE, 
                                                 parentForm.TYPE_MISS_FREE_REJECT, 
                                                 parentForm.TYPE_REJECT_EXCEP };
                    break;
                case frmSortPatternEdit.RADIO_VIRTUAL:
                    value = new int[] { KILL,
                                        ARC, 
                                        IE, 
                                        IRD, 
                                        REJECT, 
                                        REJECT };

                    description = new string[] { parentForm.TYPE_KILL, 
                                                 parentForm.TYPE_ARC, 
                                                 parentForm.TYPE_IE, 
                                                 parentForm.TYPE_IRD, 
                                                 parentForm.TYPE_REJECT_VIRT, 
                                                 parentForm.TYPE_REJECT_VIRT_EXCEP };
                    break;
            }

            DataTable typeTable = new DataTable();

            typeTable.Columns.Add("VALUE", typeof(int));
            typeTable.Columns.Add("DESCRIPTION", typeof(string));

            for (int i = 0; i < value.Length; i++)
            {
                typeTable.Rows.Add(new Object[] { value[i], description[i] });
            }

            cboType.DataSource = typeTable;
            cboType.DisplayMember = "DESCRIPTION";
            cboType.ValueMember = "VALUE";

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Returns the maximal ABS_POCKET_NO value of the physical or virtual pockets datatable from the parent form 
        private long getMaxAbsPocketID()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Returns the maximal ABS_POCKET_NO value of the physical or virtual pockets datatable from the parent form");

            long maxPocketID = 0;

            if (pocketsTable.Rows.Count > 0)
            {
                maxPocketID = (from item in pocketsTable.AsEnumerable()
                                select Convert.ToInt64(item["ABS_POCKET_NO"] ?? 0)).Max();
            }
            else { maxPocketID = 0; }

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  maxPocketID-> " + maxPocketID.ToString());

            //For traces
            logger.Debug(location + " Ending...");

            return maxPocketID;
        }

        //Enables or disables the [Previous] and [Next] buttons
        private void enablePreviousNextButtons()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Enables or disables the [Previous] and [Next] buttons");

            DataGridViewRow currentRow = parentForm.dgvPockets.CurrentRow;

            if (currentRow != null && action == ACTION_EDIT)
            {
                if (currentRow.Index == 0) { btnPrevious.Enabled = false; }
                else { btnPrevious.Enabled = true; }

                if (currentRow.Index == parentForm.dgvPockets.Rows.Count - 1) { btnNext.Enabled = false; }
                else { btnNext.Enabled = true; }
            }
            else
            {
                btnPrevious.Enabled = false;
                btnNext.Enabled = false;
            }

            //For traces
            logger.Debug(location + " Ending...");
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

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  action-> " + action);

                switch (action)
                {
                    case ACTION_EDIT:
                    case ACTION_ADD:
                        if (validateFields())
                        {
                            //For traces   
                            logger.Debug(location + " After line: if (validateFields())");
                            //For traces   
                            logger.Debug(location + "Before line: askForSave();");

                            askForSave();
                        }
                        break;
                    case ACTION_REMOVE:
                        //displays an hourglass cursor
                        Cursor.Current = Cursors.WaitCursor;
 
                        string description = txtDescription.Text;

                        string message = "";
                        string pocket = "";
  
                        message += rm.GetString("msg1DeletePocket") + Math.Abs(pocketID).ToString()  + ":";
                        message += "\n" + "\n";

                        pocket  += String.IsNullOrEmpty(description) ? "" : description + "\n";

                        message += String.IsNullOrEmpty(pocket)      ? "" : pocket + "\n";
                        message += rm.GetString("msg2DeletePocket");

                        DialogResult result = MessageBox.Show(message,
                                                              rm.GetString("removePocket"), 
                                                              MessageBoxButtons.OKCancel,
                                                              MessageBoxIcon.Exclamation);

                        switch (result)
                        {
                            case DialogResult.OK:
                                string criteria = "SORT_PATTERN_ID = " + sortPatternID.ToString() +
                                                   " and POCKET_NO = " + pocketID.ToString();

                                //Selects the rows to be removed in the list of values data table 
                                var rowsToRemove = parentForm.listValuesTable.Select(criteria);

                                //Removes the concerned rows in the list of values data table 
                                foreach (var row in rowsToRemove)
                                {
                                    parentForm.listValuesTable.Rows.Remove(row);
                                }

                                //Selects the rows to be removed in the conditions data table 
                                rowsToRemove = parentForm.conditionsTable.Select(criteria);

                                //Removes the concerned rows in the conditions data table 
                                foreach (var row in rowsToRemove) 
                                {
                                    parentForm.conditionsTable.Rows.Remove(row);
                                }

                                //Selects the rows to be removed in the pockets data table 
                                rowsToRemove = pocketsTable.Select(criteria);
                            
                                //Removes the concerned rows in the pockets data table 
                                foreach (var row in rowsToRemove)
                                {
                                    pocketsTable.Rows.Remove(row);
                                }

                                parentForm.isSomethingChanged = true;

                                isFormClosedByUser = true;

                                //For the parent form knows that there was a refresh to do
                                this.DialogResult = DialogResult.OK;
                                this.Close();
 
                                break;
                            case DialogResult.Cancel:
                                break;
                        }

                        //displays the default cursor
                        Cursor.Current = Cursors.Default;
 
                        break;
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

        //Asks the user if he wants to save his changes and records if applicable
        private void askForSave()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Asks the user if he wants to save his changes and records if applicable");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  isSomethingChanged-> " + isSomethingChanged.ToString());
            logger.Debug(location + "  action-> " + action);

            if (isSomethingChanged)
            {
                //For traces   
                logger.Debug(location + " After line: if (isSomethingChanged)");

                //displays an hourglass cursor
                Cursor.Current = Cursors.WaitCursor;

                DialogResult result = MessageBox.Show(rm.GetString("msgSaveChanges2"),
                                                      rm.GetString("titleSaveChanges2"),
                                                      MessageBoxButtons.YesNoCancel,
                                                      MessageBoxIcon.None);

                switch (result)
                {
                    case DialogResult.Yes:
                        int currentIndex;

                        switch(action)
                        {
                            case ACTION_EDIT:
                                currentIndex = savePocketsTable();

                                //For traces
                                logger.Debug(location + "Values:");
                                logger.Debug(location + "  currentIndex-> " + currentIndex.ToString());

                                //For traces   
                                logger.Debug(location + "Before line: parentForm.refreshDgvPockets();");

                                parentForm.refreshDgvPockets();

                                //Selects the current row after the refresh
                                parentForm.dgvPockets.CurrentCell = parentForm.dgvPockets[0, currentIndex];

                                //For traces   
                                logger.Debug(location + "Before line: parentForm.refreshPocketSummary();");

                                parentForm.refreshPocketSummary();

                                parentForm.setTitleDgvConditions();

                                //For traces   
                                logger.Debug(location + "Before line: parentForm.refreshDgvConditions();");

                                parentForm.refreshDgvConditions();

                                isFormClosedByUser = true;

                                //For the parent form knows that there was no refresh to do
                                this.DialogResult = DialogResult.Cancel;
                                this.Close();
                                break;
                            case ACTION_ADD:
                                //For traces   
                                logger.Debug(location + "Before line: currentIndex = saveRoutingTable();");

                                currentIndex = savePocketsTable();

                                //For traces
                                logger.Debug(location + "Values:");
                                logger.Debug(location + "  currentIndex-> " + currentIndex.ToString());

                                //For traces   
                                logger.Debug(location + "Before line: parentForm.refreshDgvPockets();");

                                parentForm.refreshDgvPockets();

                                //Selects the current row after the refresh
                                parentForm.dgvPockets.CurrentCell = parentForm.dgvPockets[0, currentIndex];

                                //For traces   
                                logger.Debug(location + "Before line: parentForm.refreshPocketSummary();");

                                parentForm.refreshPocketSummary();

                                parentForm.setTitleDgvConditions();
                                parentForm.enablePocketsEditRemoveButtons();

                                //For traces   
                                logger.Debug(location + "Before line: parentForm.refreshDgvConditions();");

                                parentForm.refreshDgvConditions();
                                
                                parentForm.enablePocketsMoveUpDownButtons();

                                parentForm.enableConditionsEditAddRemoveButtons();

                                //For traces   
                                logger.Debug(location + "Before line: initializeNewPocket();");

                                initializeNewPocket();
                                
                                //For traces   
                                logger.Debug(location + "Before line: enableFieldsOnCboTypeValue();");

                                enableFieldsOnCboTypeValue();

                                isSomethingChanged = false;
                                break;
                        }

                        break;
                    case DialogResult.No:
                        switch (action)
                        {
                            case ACTION_EDIT:
                                isFormClosedByUser = true;

                                //For the parent form knows that there was no change in the child data
                                this.DialogResult = DialogResult.Cancel;
                                this.Close();
                                break;
                            case ACTION_ADD:
                                //For traces   
                                logger.Debug(location + "Before line: initializeNewPocket();");

                                initializeNewPocket();

                                //For traces   
                                logger.Debug(location + "Before line: enableFieldsOnCboTypeValue();");
                                
                                enableFieldsOnCboTypeValue();

                                isSomethingChanged = false;
                                break;
                        }

                        break;
                    case DialogResult.Cancel:
                        break;
                }

                //displays the default cursor
                Cursor.Current = Cursors.Default;
            }
            else
            {
                isFormClosedByUser = true;

                //For the parent form knows that there was no change in the child data
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Saves the form data in the physical or virtual pockets data table
        private int savePocketsTable()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Saves the form data in the physical or virtual pockets data table");

            int currentIndex = 0;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  action-> " + action);

            switch (action)
            {
                case ACTION_EDIT:
                    DataGridViewRow currentRow = parentForm.dgvPockets.CurrentRow;

                    currentIndex = currentRow.Index;
                    break;
                case ACTION_ADD:
                    pocketsTable.Rows.Add();
                    currentIndex = pocketsTable.Rows.Count - 1;
                    break;
            }

            string buffer;

            buffer = txtDescription.Text.Trim();
            string description = buffer.Substring(0, Math.Min(buffer.Length, 256));

            string type = cboType.SelectedValue.ToString();
            type = string.IsNullOrEmpty(type) ? null : type;

            string priority = txtPriority.Text;

            string endPointID = cboEndPoint.SelectedValue.ToString();
            endPointID = string.IsNullOrEmpty(endPointID) ? null : endPointID;

            buffer = txtPhysicalPocketDef.Text.Trim();
            string physicalPocketDef = buffer.Substring(0, Math.Min(buffer.Length, 20));

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  sortPatternID-> " + sortPatternID.ToString());
            logger.Debug(location + "  sortPatternUniqueID-> " + sortPatternUniqueID.ToString());
            logger.Debug(location + "  pocketID-> " + pocketID.ToString());
            logger.Debug(location + "  Math.Abs(pocketID)-> " + Math.Abs(pocketID).ToString());
            logger.Debug(location + "  description-> " + description);
            logger.Debug(location + "  type-> " + type);
            logger.Debug(location + "  priority-> " + priority);
            logger.Debug(location + "  endPointID-> " + endPointID);
            logger.Debug(location + "  physicalPocketDef-> " + physicalPocketDef);

            pocketsTable.Rows[currentIndex]["SORT_PATTERN_ID"] = sortPatternID;
            pocketsTable.Rows[currentIndex]["SORT_PATTERN_UNIQUE_ID"] = sortPatternUniqueID.ToString();
            pocketsTable.Rows[currentIndex]["POCKET_NO"] = pocketID;
            pocketsTable.Rows[currentIndex]["ABS_POCKET_NO"] = Math.Abs(pocketID);
            pocketsTable.Rows[currentIndex]["DESCRIPTION"] = description;

            if (type == null) 
            { 
                pocketsTable.Rows[currentIndex]["POCKET_TYPE"] = DBNull.Value;
                pocketsTable.Rows[currentIndex]["REJECT_CODE"] = DBNull.Value;
            }
            else 
            { 
                pocketsTable.Rows[currentIndex]["POCKET_TYPE"] = type; 
            
                string rejectCode = null;

                if (type == "2")
                {
                    if (cboType.Text.Equals(parentForm.TYPE_REJECT_SOFT)) { rejectCode = "1"; }
                    if (cboType.Text.Equals(parentForm.TYPE_REJECT_EXCEP)) { rejectCode = "5"; }
                    if (cboType.Text.Equals(parentForm.TYPE_REJECT_VIRT)) { rejectCode = "6"; }
                    if (cboType.Text.Equals(parentForm.TYPE_REJECT_VIRT_EXCEP)) { rejectCode = "7"; }

                }

                if (rejectCode == null) { pocketsTable.Rows[currentIndex]["REJECT_CODE"] = DBNull.Value; }
                else { pocketsTable.Rows[currentIndex]["REJECT_CODE"] = rejectCode; }
            }

            pocketsTable.Rows[currentIndex]["CONDITION_PRIORITY"] = priority;

            if (endPointID == null) 
            { 
                pocketsTable.Rows[currentIndex]["END_POINT_ID"] = DBNull.Value;
                pocketsTable.Rows[currentIndex]["END_POINT_DESC"] = DBNull.Value;
            }
            else 
            { 
                pocketsTable.Rows[currentIndex]["END_POINT_ID"] = endPointID;

                //For traces   
                logger.Debug(location + "Before line: EndPoint ep = new EndPoint(btecDB);");
                logger.Debug(location + "        and: SortPatternDefData epData = ep.GetEndPointInfo(long.Parse(endPointID));");

                EndPoint ep = new EndPoint(btecDB);
                EndPointData epData = ep.GetEndPointInfo(long.Parse(endPointID));

                pocketsTable.Rows[currentIndex]["END_POINT_DESC"] = epData.GetStringValue(epData.DESCRIPTION);
            }

            switch (selectedPocketsRadioButton)
            {
                case frmSortPatternEdit.RADIO_PHYSICAL:
                    pocketsTable.Rows[currentIndex]["PHYSICAL_POCKET_DEF"] = physicalPocketDef;
                    break;
                case frmSortPatternEdit.RADIO_VIRTUAL:
                    pocketsTable.Rows[currentIndex]["PHYSICAL_POCKET_DEF"] = rm.GetString("notApplicable");
                    break;
            }

            parentForm.isSomethingChanged = true;

            //For traces
            logger.Debug(location + " Ending...");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  currentIndex-> " + currentIndex.ToString());

            return currentIndex;
        }

        //Initializes all form fields to create a new newPocketID
        private void initializeNewPocket()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Initializes all form fields to create a new newPocketID");

            long absPocketID = getMaxAbsPocketID() + 1;

            pocketID = (selectedPocketsRadioButton == frmSortPatternEdit.RADIO_PHYSICAL ? absPocketID : -absPocketID);
            sortPatternUniqueID = getMaxSortPatternPocketUniqueID() + 1;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  absPocketID-> " + absPocketID.ToString());
            logger.Debug(location + "  pocketID-> " + pocketID.ToString());
            logger.Debug(location + "  sortPatternUniqueID-> " + sortPatternUniqueID.ToString());

            txtPocketId.Text = absPocketID.ToString();
            txtDescription.Text = null;
            txtPhysicalPocketDef.Text = null;
            cboType.SelectedIndex = 0;
            cboEndPoint.SelectedIndex = 0;
            txtPriority.Text = null;

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Makes field validation before saving
        private bool validateFields()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Makes field validation before saving");

            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                //Set the focus to the Description field
                txtDescription.Focus();

                MessageBox.Show(rm.GetString("msgDescriptionPocket"),
                                rm.GetString("TitleMsgValidationError"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return false;
            }

            bool isPriorityGreaterThan0 = false;

            switch (selectedPocketsRadioButton)
            {
                case frmSortPatternEdit.RADIO_PHYSICAL:
                    if (cboType.Text.Equals(parentForm.TYPE_KILL) ||
                        cboType.Text.Equals(parentForm.TYPE_REHANDLE) ||
                        cboType.Text.Equals(parentForm.TYPE_REJECT_SOFT))
                    {
                        isPriorityGreaterThan0 = true;
                    }

                    break;
                case frmSortPatternEdit.RADIO_VIRTUAL:
                   if (cboType.Text.Equals(parentForm.TYPE_KILL) ||
                       cboType.Text.Equals(parentForm.TYPE_IE) ||
                       cboType.Text.Equals(parentForm.TYPE_REJECT_SOFT))
                   {
                       isPriorityGreaterThan0 = true;
                   }

                    break;
            }

            int priority = 0;
            bool isInt = int.TryParse(txtPriority.Text.Trim(), out priority);

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  isPriorityGreaterThan0-> " + isPriorityGreaterThan0.ToString());
            logger.Debug(location + "  priority-> " + priority.ToString());
            logger.Debug(location + "  isInt-> " + isInt.ToString());

            if (isPriorityGreaterThan0)
            {
                if (isInt == false || priority <= 0)
                {
                    //Set the focus to the Priority field
                    txtPriority.Focus();

                    MessageBox.Show(rm.GetString("msgPriorityGreater0Pocket"),
                                    rm.GetString("TitleMsgValidationError"),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    return false;
                }
            }
            else
            {
                if (isInt == false || priority < 0)
                {
                    //Set the focus to the Priority field
                    txtPriority.Focus();

                    MessageBox.Show(rm.GetString("msgPriorityGreaterOrEqual0Pocket"),
                                    rm.GetString("TitleMsgValidationError"),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    return false;
                }
            }

            if (cboEndPoint.Enabled && string.IsNullOrEmpty(cboEndPoint.Text.Trim()))
            {
                //Set the focus to the End Point field
                cboEndPoint.Focus();

                MessageBox.Show(rm.GetString("msgEndPointPocket"),
                                rm.GetString("TitleMsgValidationError"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return false;
            }

            //For traces
            logger.Debug(location + " Ending...");

            return true;
        }

        //Occurs when the form is closed
        private void frmSortPatternPocket_FormClosing(object sender, FormClosingEventArgs e)
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

                        DialogResult selectButton = MessageBox.Show(rm.GetString("MsgQuitWithoutSaving"), rm.GetString("titleQuitWithoutSaving"), MessageBoxButtons.YesNo, MessageBoxIcon.None);

                        //For traces
                        logger.Debug(location + "Values:");
                        logger.Debug(location + "  result-> " + selectButton.ToString());

                        switch (selectButton)
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

        //Occurs when the [Previous] button is clicked
        private void btnPrevious_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Previous] button is clicked");

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  isSomethingChanged-> " + isSomethingChanged.ToString());

                if (isSomethingChanged)
                {
                    //For traces   
                    logger.Debug(location + " After line: if (isSomethingChanged)");

                    if (validateFields())
                    {
                        //For traces   
                        logger.Debug(location + " After line: if (validateFields())");

                        DialogResult result = MessageBox.Show(rm.GetString("msgSaveChanges2"),
                                                              rm.GetString("titleSaveChanges2"),
                                                              MessageBoxButtons.YesNoCancel,
                                                              MessageBoxIcon.None);

                        switch (result)
                        {
                            case DialogResult.Yes:
                                //For traces   
                                logger.Debug(location + "Before line: moveWithSave(-1);");

                                moveWithSave(-1);
                                break;
                            case DialogResult.No:
                                //For traces   
                                logger.Debug(location + "Before line: moveWithoutSave(-1);");

                                moveWithoutSave(-1);
                                break;
                            case DialogResult.Cancel:
                                break;
                        }
                    }
                }
                else 
                {
                    //For traces   
                    logger.Debug(location + " After line: if (isSomethingChanged)...else");

                    //For traces   
                    logger.Debug(location + "Before line: moveWithoutSave(-1);");

                    moveWithoutSave(-1); 
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

        //Occurs when the [Next] button is clicked
        private void btnNext_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Next] button is clicked");

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  isSomethingChanged-> " + isSomethingChanged.ToString());

                if (isSomethingChanged)
                {
                    //For traces   
                    logger.Debug(location + " After line: if (isSomethingChanged)");

                    if (validateFields())
                    {
                        //For traces   
                        logger.Debug(location + " After line: if (validateFields())");

                        DialogResult result = MessageBox.Show(rm.GetString("msgSaveChanges2"),
                                                              rm.GetString("titleSaveChanges2"),
                                                              MessageBoxButtons.YesNoCancel,
                                                              MessageBoxIcon.None);

                        switch (result)
                        {
                            case DialogResult.Yes:
                                //For traces   
                                logger.Debug(location + "Before line: moveWithSave(+1);");

                                moveWithSave(+1);
                                break;
                            case DialogResult.No:
                                //For traces   
                                logger.Debug(location + "Before line: moveWithoutSave(+1);");

                                moveWithoutSave(+1);
                                break;
                            case DialogResult.Cancel:
                                break;
                        }
                    }
                }
                else 
                {
                    //For traces   
                    logger.Debug(location + " After line: if (isSomethingChanged)...else");

                    //For traces   
                    logger.Debug(location + "Before line: moveWithoutSave(+1);");

                    moveWithoutSave(+1); 
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

        //Used when the user wants to save when he click the [Previous] or [Next] button
        private void moveWithSave(int increment)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Used when the user wants to save when he click the [Previous] or [Next] button");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  increment-> " + increment.ToString());

            int currentIndex = savePocketsTable();

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  currentIndex-> " + currentIndex.ToString());
            
            currentIndex += increment;

            //For traces   
            logger.Debug(location + "Before line: parentForm.refreshDgvPockets();");

            parentForm.refreshDgvPockets();

            //Selects the previous or next row that become the current row
            parentForm.dgvPockets.CurrentCell = parentForm.dgvPockets[0, currentIndex];

            //For traces   
            logger.Debug(location + "Before line: parentForm.refreshPocketSummary();");

            parentForm.refreshPocketSummary();

            parentForm.setTitleDgvConditions();

            //For traces   
            logger.Debug(location + "Before line: parentForm.refreshDgvConditions();");
            
            parentForm.refreshDgvConditions();
            
            parentForm.enablePocketsMoveUpDownButtons();

            parentForm.enableConditionsEditAddRemoveButtons();

            //For traces   
            logger.Debug(location + "Before line: setPocketFields(currentIndex);");

            setPocketFields(currentIndex);

            //For traces   
            logger.Debug(location + "Before line: enableFieldsOnCboTypeValue();");

            enableFieldsOnCboTypeValue();

            enablePreviousNextButtons();

            isSomethingChanged = false;

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Used when the user does not wants to save when he click the [Previous] or [Next] button
        private void moveWithoutSave(int increment)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Used when the user does not wants to save when he click the [Previous] or [Next] button");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  increment-> " + increment.ToString());

            DataGridViewRow currentRow = parentForm.dgvPockets.CurrentRow;

            int currentIndex = currentRow.Index;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  currentIndex-> " + currentIndex.ToString());

            currentIndex += increment;

            //Selects the previous or next row that become the current row
            parentForm.dgvPockets.CurrentCell = parentForm.dgvPockets[0, currentIndex];

            parentForm.setTitleDgvConditions();

            //For traces   
            logger.Debug(location + "Before line: parentForm.refreshDgvConditions();");
            
            parentForm.refreshDgvConditions();

            parentForm.enablePocketsMoveUpDownButtons();

            //For traces   
            logger.Debug(location + "Before line: setPocketFields(currentIndex);");

            setPocketFields(currentIndex);

            //For traces   
            logger.Debug(location + "Before line: enableFieldsOnCboTypeValue();");

            enableFieldsOnCboTypeValue();

            enablePreviousNextButtons();

            isSomethingChanged = false;

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Allows to obtain the maximum SORT_PATTERN_UNIQUE_ID value from the physical and virtual pockets data table
        private long getMaxSortPatternPocketUniqueID()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Allows to obtain the maximum SORT_PATTERN_UNIQUE_ID value from the physical and virtual pockets data table");

            long maxPhysicalUniqueID = 0;
            long maxVirtualUniqueID = 0;

            if (parentForm.physicalPocketsTable.Rows.Count > 0)
            {
                maxPhysicalUniqueID = (from item in parentForm.physicalPocketsTable.AsEnumerable()
                                       select Convert.ToInt64(item["SORT_PATTERN_UNIQUE_ID"] ?? 0)).Max();
            }
            else { maxPhysicalUniqueID = 0; }

            if (parentForm.virtualPocketsTable.Rows.Count > 0)
            {
                maxVirtualUniqueID = (from item in parentForm.virtualPocketsTable.AsEnumerable()
                                      select Convert.ToInt64(item["SORT_PATTERN_UNIQUE_ID"] ?? 0)).Max();
            }
            else { maxVirtualUniqueID = 0; }

            long maxTotal = Math.Max(maxPhysicalUniqueID, maxVirtualUniqueID);

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  maxPhysicalUniqueID-> " + maxPhysicalUniqueID.ToString());
            logger.Debug(location + "  maxVirtualUniqueID-> " + maxVirtualUniqueID.ToString());
            logger.Debug(location + "  maxTotal-> " + maxTotal.ToString());

            //For traces
            logger.Debug(location + " Ending...");

            return maxTotal;
        }
    }
}
