//2012-07 Simon Boutin MANTIS# 16396 : Convert LBTables from VB to C#

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
using System.ComponentModel;

namespace Administration
{
    public partial class frmSortPatternEdit : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmSortPatternEdit).Name;

        IBtecDB btecDB;
        long sortPatternID;
        public bool isSomethingChanged = false;
        bool isFormClosedByUser = false;
        bool isSubscribedConditionsCheckedChanged = false;
        string action;

        public const string RADIO_PHYSICAL = "radioPhysical";
        public const string RADIO_VIRTUAL  = "radioVirtual";

        const string RADIO_AND = "radioAnd";
        const string RADIO_OR = "radioOr";

        const string ACTION_EDIT = "Edit";
        const string ACTION_ADD = "Add";
        const string ACTION_REMOVE = "Remove";

        string TITLE_VALIDATION;

        public string TYPE_KILL;
        public string TYPE_REHANDLE;
        public string TYPE_REJECT_SOFT;
        public string TYPE_REJECT_EXCEP;
        public string TYPE_REJECT_VIRT;
        public string TYPE_REJECT_VIRT_EXCEP;
        public string TYPE_ELECTRONIC;
        public string TYPE_ARC;
        public string TYPE_IE;
        public string TYPE_IRD;
        public string TYPE_MISS_FREE;
        public string TYPE_MISS_FREE_REJECT;

        public DataTable physicalPocketsTable;
        public DataTable virtualPocketsTable;
        public DataTable conditionsTable;
        public DataTable listValuesTable;

        public string pocketIDAndDescription;

        ResourceManager rm;
        ILoggerBtec logger;

        public frmSortPatternEdit(IBtecDB btecDBParent, string actionParent, long sortPatternIDParent)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmSortPatternEdit));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmSortPatternEdit));
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

                btecDB = btecDBParent;
                action = actionParent; 
                sortPatternID = sortPatternIDParent;

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
        private void frmSortPatternEdit_Load(object sender, EventArgs e)
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

                TITLE_VALIDATION = rm.GetString("titleValidationError");

                TYPE_KILL = rm.GetString("typeKill");
                TYPE_REHANDLE = rm.GetString("typeRehandle");
                TYPE_REJECT_SOFT = rm.GetString("typeSoftwareReject");
                TYPE_REJECT_EXCEP = rm.GetString("typeExceptionReject");
                TYPE_REJECT_VIRT = rm.GetString("typeVirtualSoftwareRejects");
                TYPE_REJECT_VIRT_EXCEP = rm.GetString("typeVirtualExceptionRejects");
                TYPE_ELECTRONIC = rm.GetString("typeElectronic");
                TYPE_ARC = rm.GetString("typeARC");
                TYPE_IE = rm.GetString("typeIE");
                TYPE_IRD = rm.GetString("typeIRD");
                TYPE_MISS_FREE = rm.GetString("typeMissingFree");
                TYPE_MISS_FREE_REJECT = rm.GetString("typeMissingFreeRejects");

                EnableControl.TextBox(txtID, false);
                EnableControl.TextBox(txtPhysical, false);
                EnableControl.TextBox(txtVirtual, false);
                EnableControl.TextBox(txtRejects, false);

                //For traces   
                logger.Debug(location + "Before line: SortPatternDef spd = new SortPatternDef(btecDB);");
                logger.Debug(location + "        and: SortPatternDefData spData = spd.GetSortPatternDefInfo(sortPatternID);");

                SortPatternDef sp = new SortPatternDef(btecDB);
                SortPatternDefData spData = sp.GetSortPatternDefInfo(sortPatternID);

                txtID.Text = sortPatternID.ToString();

                //adjusts the form appearance depending on the action specified in the parent form
                switch (action)
                {
                    case ACTION_EDIT:
                        this.Text = rm.GetString("titleEditSortPatternPocket");
                        btnAction.Text = rm.GetString("btnOK");

                        txtDescription.Text = spData.GetStringValue(spData.DESCRIPTION);
                        EnableControl.TextBox(txtDescription, true);
                        break;
                    case ACTION_ADD:
                        this.Text = rm.GetString("titleAddSortPatternPocket");
                        btnAction.Text = rm.GetString("btnAdd");

                        txtDescription.Text = string.Empty;
                        EnableControl.TextBox(txtDescription, true);
                        break;
                    case ACTION_REMOVE:
                        this.Text = rm.GetString("titleRemoveSortPatternPocket");
                        btnAction.Text = rm.GetString("btnRemove"); 

                        txtDescription.Text = spData.GetStringValue(spData.DESCRIPTION);
                        EnableControl.TextBox(txtDescription, false);
                        break;
                }

                radioPhysical.Checked = true;
                radioAnd.Checked = true;

                setTitleDgvPockets();

                txtDescription.TextChanged += new EventHandler(somethingChanged);

                //Add the CheckedChanged event for all radio buttons in the panelPockets panel
                foreach (Control control in panelPockets.Controls)
                {
                    RadioButton radioButton = control as RadioButton;

                    if (radioButton != null)
                    {
                        radioButton.CheckedChanged += new EventHandler(PocketsRadioButton_CheckedChanged);
                    }
                }

                string[] PocketsColumnHeaderText = {rm.GetString("colIdNo"),
                                                    rm.GetString("colDescription"),
                                                    rm.GetString("colType"),
                                                    rm.GetString("colPriority"),
                                                    rm.GetString("colEndPoint"),
                                                    rm.GetString("colPhysicalPocketDef")};

                //Specifies the number of desired columns for the dgvPockets object
                dgvPockets.ColumnCount = 6;

                //Specifies the dgvPockets width to calculate the columnns width
                double dgvPocketsWidth = dgvPockets.Width;

                //Specifies all the columns width of the dgvPockets object 
                //The sum of all the constants must equal 1
                double[] PocketsColumnWidth = {dgvPocketsWidth * 0.055,
                                               dgvPocketsWidth * 0.363,
                                               dgvPocketsWidth * 0.182,
                                               dgvPocketsWidth * 0.073,
                                               dgvPocketsWidth * 0.145,
                                               dgvPocketsWidth * 0.182};

                for (int i = 0; i < dgvPockets.ColumnCount; i++)
                {
                    dgvPockets.Columns[i].HeaderText = PocketsColumnHeaderText[i];
                    dgvPockets.Columns[i].Width = (int)Math.Round(PocketsColumnWidth[i]);
                    dgvPockets.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //specifies that we cannot sort this column
                    dgvPockets.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                dgvPockets.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvPockets.Columns[0].ValueType = typeof(string);

                for (int i = 1; i <= 2; i++)
                {
                    dgvPockets.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvPockets.Columns[i].ValueType = typeof(string);
                }

                for (int i = 3; i <= 5; i++)
                {
                    dgvPockets.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvPockets.Columns[i].ValueType = typeof(string);
                }

                //Add the CheckedChanged event for all radio buttons in the panelConditions panel
                foreach (Control control in panelConditions.Controls)
                {
                    RadioButton radioButton = control as RadioButton;

                    if (radioButton != null)
                    {
                        radioButton.CheckedChanged += new EventHandler(ConditionsRadioButton_CheckedChanged);
                    }
                }

                isSubscribedConditionsCheckedChanged = true;

                string[] ConditionsColumnHeaderText = {rm.GetString("colSequence"),
                                                       rm.GetString("colConditionField"),
                                                       rm.GetString("colLogicOperator"),
                                                       rm.GetString("colValue1"),
                                                       rm.GetString("colValue2")};

                //Specifies the number of desired columns for the dgvConditions object
                dgvConditions.ColumnCount = 5;

                //Specifies the dgvConditions width to calculate the columnns width
                double dgvConditionsWidth = dgvConditions.Width;

                //Specifies all the columns width of the dgvConditions object 
                //The sum of all the constants must equal 1
                double[] ConditionsColumnWidth = {dgvConditionsWidth * 0.096,
                                                  dgvConditionsWidth * 0.251,
                                                  dgvConditionsWidth * 0.151,
                                                  dgvConditionsWidth * 0.251,
                                                  dgvConditionsWidth * 0.251};

                for (int i = 0; i < dgvConditions.ColumnCount; i++)
                {
                    dgvConditions.Columns[i].HeaderText = ConditionsColumnHeaderText[i];
                    dgvConditions.Columns[i].Width = (int)Math.Round(ConditionsColumnWidth[i]);
                    dgvConditions.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //specifies that we cannot sort this column
                    dgvConditions.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                    dgvConditions.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvConditions.Columns[i].ValueType = typeof(string);
                }

                //For traces   
                logger.Debug(location + "Before line: refreshPocketsData();");
 
                refreshPocketsData();

                //For traces   
                logger.Debug(location + "Before line: refreshDgvPockets();");

                refreshDgvPockets();

                setTitleDgvConditions();

                //For traces   
                logger.Debug(location + "Before line: refreshConditionsData();");

                refreshConditionsData();

                //For traces   
                logger.Debug(location + "Before line: refreshDgvConditions();");
                
                refreshDgvConditions();

                //For traces   
                logger.Debug(location + "Before line: refreshListValuesData();");

                refreshListValuesData();

                enablePocketsEditRemoveButtons();
                enablePocketsMoveUpDownButtons();

                enableConditionsEditAddRemoveButtons();

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

        //Allows to enables or desables the CheckedChanged events of the panelConditions panel
        private void SubscribeConditions_CheckedChanged(bool enabled)
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
                logger.Info(location + "Purpose: Allows to enables or desables the CheckedChanged events of the panelConditions panel");

                //For traces
                logger.Debug(location + "Parameters:");
                logger.Debug(location + "  enabled-> " + enabled.ToString());

                if (!enabled)
                {
                    foreach (Control control in panelConditions.Controls)
                    {
                        RadioButton radioButton = control as RadioButton;

                        if (radioButton != null)
                        {
                            radioButton.CheckedChanged -= ConditionsRadioButton_CheckedChanged;
                        }
                    }
                }
                else if (!isSubscribedConditionsCheckedChanged)
                {
                    foreach (Control control in panelConditions.Controls)
                    {
                        RadioButton radioButton = control as RadioButton;

                        if (radioButton != null)
                        {
                            radioButton.CheckedChanged += ConditionsRadioButton_CheckedChanged;
                        }
                    }
                }

                //For traces
                logger.Debug(location + " Ending...");
            }
            catch { }
 
            //An exception has no consequence for this method
            isSubscribedConditionsCheckedChanged = enabled;
        }

        //Refreshes the pockets DataTables with the most recent data
        void refreshPocketsData()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the pockets DataTables with the most recent data");

            //For traces   
            logger.Debug(location + "Before line: SortPatternPocket spp = new SortPatternPocket(btecDB);");

            SortPatternPocket spp = new SortPatternPocket(btecDB);
 
            physicalPocketsTable = spp.GetSortPatternPocket(sortPatternID, true);
            virtualPocketsTable = spp.GetSortPatternPocket(sortPatternID, false);

            if (virtualPocketsTable != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in virtualPocketsTable.Rows)");

                foreach (DataRow row in virtualPocketsTable.Rows)
                {
                    row["PHYSICAL_POCKET_DEF"] = rm.GetString("notApplicable");
                }
            }

            //For traces   
            logger.Debug(location + "Before line: refreshPocketSummary();");

            refreshPocketSummary();

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Refreshes the conditions DataTable with the most recent data
        void refreshConditionsData()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the conditions DataTable with the most recent data");

            //For traces   
            logger.Debug(location + "Before line: SortPatternPocketCond sppc = new SortPatternPocketCond(btecDB);");

            SortPatternPocketCond sppc = new SortPatternPocketCond(btecDB);

            conditionsTable = sppc.GetSortPatternPocketCond(sortPatternID);

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Refreshes the list of values DataTable with the most recent data
        void refreshListValuesData()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the list of values DataTable with the most recent data");

            //For traces   
            logger.Debug(location + "Before line: ListValues lv = new ListValues(btecDB);");

            ListValues lv = new ListValues(btecDB);

            listValuesTable = lv.GetListValues(sortPatternID);

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Refreshes the dgvConditions object with the most recent data
        public void refreshDgvConditions()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the dgvConditions object with the most recent data");

            //Erases all data from the dgvConditions object
            dgvConditions.Rows.Clear();

            if (dgvPockets.CurrentRow != null)
            {
                string selectedPocketsRadio = getSelectedPocketsRadioButton();
                int currentIndex = dgvPockets.CurrentRow.Index;
                bool isForAdding;

                if (conditionsTable != null)
                {
                    //For traces   
                    logger.Debug(location + "Before line: foreach (DataRow row in conditionsTable.Rows)");

                    //Fill the dgvConditions object with values
                    foreach (DataRow row in conditionsTable.Rows)
                    {
                        isForAdding = false;

                        switch (selectedPocketsRadio)
                        {
                            case RADIO_PHYSICAL:
                                if ((row["POCKET_NO"] ?? "").ToString() == (physicalPocketsTable.Rows[currentIndex]["POCKET_NO"] ?? "").ToString())
                                {
                                    isForAdding = true;
                                }
                                break;
                            case RADIO_VIRTUAL:
                                if ((row["POCKET_NO"] ?? "").ToString() == (virtualPocketsTable.Rows[currentIndex]["POCKET_NO"] ?? "").ToString())
                                {
                                    isForAdding = true;
                                }
                                break;
                        }

                        if (isForAdding)
                        {
                            //Adds a row of records in the dgvConditions object
                            dgvConditions.Rows.Add((row["CONDITION_SEQUENCE"] ?? "").ToString(),
                                                   (row["FIELD_NAME"] ?? "").ToString(),
                                                   (row["LOGIC_OPERATOR"] ?? "").ToString(),
                                                   (row["VALUE1"] ?? "").ToString(),
                                                   (row["VALUE2"] ?? "").ToString());
                        }
                    }
                }
            }

            //To be sure to have a current row before calling enableConditionsEditAddRemoveButtons()
            if (dgvConditions.Rows.Count > 0)
            {
                dgvConditions.CurrentCell = dgvConditions[0, 0];
            }

            enablePanelConditions();

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Refreshes the grpBoxSummary group box with the most recent data
        public void refreshPocketSummary()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the grpBoxSummary group box with the most recent data");

            txtPhysical.Text = physicalPocketsTable.Rows.Count.ToString();
            txtVirtual.Text = virtualPocketsTable.Rows.Count.ToString();

            int count = 0;

            count += physicalPocketsTable.Select("REJECT_CODE <> 0").Count();
            count += virtualPocketsTable.Select("REJECT_CODE <> 0").Count();

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  count-> " + count.ToString());

            txtRejects.Text = count.ToString();

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Refreshes the dgvPockets object with the most recent data
        public void refreshDgvPockets()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the dgvPockets object with the most recent data");

            //Erases all data from the dgvPockets object
            dgvPockets.Rows.Clear();

            DataTable pocketsTable = null;

            //Gets the right DataTable according to the selected radio button
            switch (getSelectedPocketsRadioButton())
            {
                case RADIO_PHYSICAL:
                    pocketsTable = physicalPocketsTable;
                    break;
                case RADIO_VIRTUAL:
                    pocketsTable = virtualPocketsTable;
                    break;
            }

            string pocketType = "";
            string rejectCode = "";
            string pocketTypeDesc = "";

            if (pocketsTable != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in pocketsTable.Rows)");

                //Fill the dgvPockets object with values
                foreach (DataRow row in pocketsTable.Rows)
                {
                    pocketType = (row["POCKET_TYPE"] ?? "").ToString();
                    rejectCode = (row["REJECT_CODE"] ?? "").ToString();

                    pocketTypeDesc = getPocketTypeDesc(pocketType, rejectCode);

                    //Adds a row of records in the dgvPockets object
                    dgvPockets.Rows.Add((row["ABS_POCKET_NO"] ?? "").ToString(),
                                        (row["DESCRIPTION"] ?? "").ToString(),
                                         pocketTypeDesc,
                                        (row["CONDITION_PRIORITY"] ?? "").ToString(),
                                        (row["END_POINT_DESC"] ?? "").ToString(),
                                        (row["PHYSICAL_POCKET_DEF"] ?? "").ToString());
                }
            }

            //To be sure to have a current row before calling enablePocketsEditRemoveButtons()
            if (dgvPockets.Rows.Count > 0)
            {
                dgvPockets.CurrentCell = dgvPockets[0, 0];
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Returns a newPocketID type description given a specific newPocketID type and reject code
        public string getPocketTypeDesc(string pocketType, string rejectCode)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Returns a newPocketID type description given a specific newPocketID type and reject code");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  pocketType-> " + pocketType);
            logger.Debug(location + "  rejectCode-> " + rejectCode);

            string pocketTypeDesc = "";

            switch (pocketType)
            {
                case "0":
                    pocketTypeDesc = TYPE_KILL; break;
                case "1":
                    pocketTypeDesc = TYPE_REHANDLE; break;
                case "2":
                    switch (rejectCode)
                    {
                        case "1":
                            pocketTypeDesc = TYPE_REJECT_SOFT; break;
                        case "5":
                            pocketTypeDesc = TYPE_REJECT_EXCEP; break;
                        case "6":
                            pocketTypeDesc = TYPE_REJECT_VIRT; break;
                        case "7":
                            pocketTypeDesc = TYPE_REJECT_VIRT_EXCEP; break;
                    }
                    break;
                case "3":
                    pocketTypeDesc = TYPE_ELECTRONIC; break;
                case "4":
                    pocketTypeDesc = TYPE_ARC; break;
                case "5":
                    pocketTypeDesc = TYPE_IE; break;
                case "6":
                    pocketTypeDesc = TYPE_IRD; break;
                case "7":
                    pocketTypeDesc = TYPE_MISS_FREE; break;
                case "8":
                    pocketTypeDesc = TYPE_MISS_FREE_REJECT; break;
            }

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  pocketTypeDesc-> " + pocketTypeDesc);

            //For traces
            logger.Debug(location + " Ending...");

            return pocketTypeDesc;
        }

        //Occurs when the checked radio button is changed in the panelConditions panel
        private void ConditionsRadioButton_CheckedChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the checked radio button is changed in the panelConditions panel");

                RadioButton radioButton = sender as RadioButton;

                if (radioButton != null)
                {
                    if (radioButton.Checked)
                    {
                        //displays an hourglass cursor
                        Cursor.Current = Cursors.WaitCursor;

                        //For traces   
                        logger.Debug(location + "Before line: setConditionExpression();");

                        setConditionExpression();

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

        //builds a logical expression that will be recorded in the CONDITION_EXPRESSION field in the right pockets DataTable
        public void setConditionExpression()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: builds a logical expression that will be recorded in the CONDITION_EXPRESSION field in the right pockets DataTable");

            string expression = null;
            string operatorExp = null;

            switch (getSelectedConditionsRadioButton())
            {
                case RADIO_AND:
                    operatorExp = "&"; break;
                case RADIO_OR:
                    operatorExp = "|"; break;
            }

            //For traces   
            logger.Debug(location + "Before line: foreach (DataGridViewRow row in dgvConditions.Rows)");

            //Builds the expression that will be recorded in the CONDITION_EXPRESSION field 
            foreach (DataGridViewRow row in dgvConditions.Rows)
            {
                if (!string.IsNullOrEmpty(expression))
                {
                    expression += operatorExp;
                }

                expression += (row.Cells[0].Value ?? "").ToString();
            }

            int currentIndex = dgvPockets.CurrentRow.Index;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  operatorExp-> " + operatorExp);
            logger.Debug(location + "  expression-> " + expression);
            logger.Debug(location + "  currentIndex-> " + currentIndex.ToString());

            //Gets the right DataTable according to the selected radio button
            switch (getSelectedPocketsRadioButton())
            {
                case RADIO_PHYSICAL:
                    physicalPocketsTable.Rows[currentIndex]["CONDITION_EXPRESSION"] = expression;  
                    break;
                case RADIO_VIRTUAL:
                    virtualPocketsTable.Rows[currentIndex]["CONDITION_EXPRESSION"] = expression;
                    break;
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Occurs when the checked radio button is changed in the panelPockets panel
        private void PocketsRadioButton_CheckedChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the checked radio button is changed in the panelPockets panel");

                RadioButton radioButton = sender as RadioButton;

                if (radioButton != null)
                {
                    if (radioButton.Checked)
                    {
                        //displays an hourglass cursor
                        Cursor.Current = Cursors.WaitCursor;
                        
                        //For traces   
                        logger.Debug(location + "Before line: refreshDgvPockets();");

                        refreshDgvPockets();

                        setTitleDgvPockets();
                        setTitleDgvConditions();

                        //For traces   
                        logger.Debug(location + "Before line: refreshDgvConditions();");
                        
                        refreshDgvConditions();

                        enablePocketsEditRemoveButtons();
                        enablePocketsMoveUpDownButtons();

                        enableConditionsEditAddRemoveButtons();

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

        //Allows to know which radio button is currently selected in the panelPockets panel
        public string getSelectedPocketsRadioButton()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Allows to know which radio button is currently selected in the panelPockets panel");

            var checkedButton = panelPockets.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  checkedButton.Name-> " + checkedButton.Name.ToString());

            //For traces
            logger.Debug(location + " Ending...");

            return checkedButton.Name.ToString();
        }

        //Allows to know which radio button is currently selected in the panelConditions panel
        private string getSelectedConditionsRadioButton()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Allows to know which radio button is currently selected in the panelConditions panel");

            var checkedButton = panelConditions.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  checkedButton.Name-> " + checkedButton.Name.ToString());

            //For traces
            logger.Debug(location + " Ending...");

            return checkedButton.Name.ToString();
        }

        //Enables or disables the [Move Up] and [Move Down] buttons for the dgvPockets object
        public void enablePocketsMoveUpDownButtons()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Enables or disables the [Move Up] and [Move Down] buttons for the dgvPockets object");

            DataGridViewRow currentRow = dgvPockets.CurrentRow;

            if (currentRow != null && action != ACTION_REMOVE)
            {
                //For traces   
                logger.Debug(location + " After line: if (row != null && action != ACTION_REMOVE)");

                if (currentRow.Index == 0) { btnMoveUpPocket.Enabled = false; }
                else { btnMoveUpPocket.Enabled = true; }

                if (currentRow.Index == dgvPockets.Rows.Count - 1) { btnMoveDownPocket.Enabled = false; }
                else { btnMoveDownPocket.Enabled = true; }
            }
            else
            {
                //For traces   
                logger.Debug(location + " After line: if (row != null && action != ACTION_REMOVE)...else");

                btnMoveUpPocket.Enabled = false;
                btnMoveDownPocket.Enabled = false;
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Enables or disables the [Edit], [Add] and [Remove] buttons of the dgvPockets object
        public void enablePocketsEditRemoveButtons()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Enables or disables the [Edit], [Add] and [Remove] buttons of the dgvPockets object");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  action-> " + action);

            if (action == ACTION_REMOVE)
            {
                //For traces   
                logger.Debug(location + " After line: if (action == ACTION_REMOVE)");

                btnEditPocket.Enabled = false;
                btnAddPocket.Enabled = false; 
                btnRemovePocket.Enabled = false;
            }
            else
            {
                //For traces   
                logger.Debug(location + " After line: if (action == ACTION_REMOVE)...else");

                btnAddPocket.Enabled = true;

                if (dgvPockets.CurrentRow == null)
                {
                    btnEditPocket.Enabled = false;
                    btnRemovePocket.Enabled = false;
                }
                else
                {
                    btnEditPocket.Enabled = true;
                    btnRemovePocket.Enabled = true;
                }
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Enables or disables the panelConditions object
        private void enablePanelConditions()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Enables or disables the panelConditions object");

            if (dgvConditions.Rows.Count > 1 && action != ACTION_REMOVE)
            {
                panelConditions.Enabled = true;
            }
            else
            {
                panelConditions.Enabled = false;
            }

            string conditionExpression = string.Empty;

            if (dgvPockets.CurrentRow != null)
            {
                int currentIndexPockets = dgvPockets.CurrentRow.Index;

                //Gets the right DataTable according to the selected radio button
                switch (getSelectedPocketsRadioButton())
                {
                    case RADIO_PHYSICAL:
                        conditionExpression = (physicalPocketsTable.Rows[currentIndexPockets]["CONDITION_EXPRESSION"] ?? "").ToString();
                        break;
                    case RADIO_VIRTUAL:
                        conditionExpression = (virtualPocketsTable.Rows[currentIndexPockets]["CONDITION_EXPRESSION"] ?? "").ToString();
                        break;
                }
            }

            SubscribeConditions_CheckedChanged(false);

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  conditionExpression-> " + conditionExpression);

            if (conditionExpression.IndexOf("|") != -1)
            {
                radioOr.Checked = true;
            }
            else
            {
                radioAnd.Checked = true;
            }

            SubscribeConditions_CheckedChanged(true);

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Enables or disables the [Edit], [Add] and [Remove] buttons of the dgvConditions object
        public void enableConditionsEditAddRemoveButtons()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Enables or disables the [Edit], [Add] and [Remove] buttons of the dgvConditions object");

            if (action == ACTION_REMOVE || dgvPockets.CurrentRow == null)
            {
                //For traces   
                logger.Debug(location + " After line: if (action == ACTION_REMOVE || dgvPockets.CurrentRow == null)");

                btnEditCondition.Enabled = false;
                btnAddCondition.Enabled = false;
                btnRemoveCondition.Enabled = false;
            }
            else
            {
                //For traces   
                logger.Debug(location + " After line: if (action == ACTION_REMOVE || dgvPockets.CurrentRow == null)...else");

                btnAddCondition.Enabled = true;

                if (dgvConditions.CurrentRow == null)
                {
                    btnEditCondition.Enabled = false;
                    btnRemoveCondition.Enabled = false;
                }
                else
                {
                    btnEditCondition.Enabled = true;
                    btnRemoveCondition.Enabled = true;
                }
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Occurs when the [Move Up] button for the dgvPockets object is clicked
        private void btnMoveUpPocket_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Move Up] button for the dgvPockets object is clicked");

                string action = "MoveUp";

                switch (getSelectedPocketsRadioButton())
                {
                    case RADIO_PHYSICAL:
                        moveRowsDgvPockets(action, physicalPocketsTable);
                        break;
                    case RADIO_VIRTUAL:
                        moveRowsDgvPockets(action, virtualPocketsTable);
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

        //Occurs when the [Move Down] button for the dgvPockets object is clicked
        private void btnMoveDownPocket_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Move Down] button for the dgvPockets object is clicked");

                string action = "MoveDown";

                switch (getSelectedPocketsRadioButton())
                {
                    case RADIO_PHYSICAL:
                        moveRowsDgvPockets(action, physicalPocketsTable);
                        break;
                    case RADIO_VIRTUAL:
                        moveRowsDgvPockets(action, virtualPocketsTable);
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

        //Moves up or down the current row in the pockets DataTable
        private void moveRowsDgvPockets(string action, DataTable pocketsTable)
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
            logger.Info(location + "Purpose: Moves up or down the current row in the pockets DataTable");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  action-> " + action);

            int currentIndexPockets = dgvPockets.CurrentRow.Index;
            int increment = 0;

            switch (action)
            {
                case "MoveUp":
                    increment = -1; break;
                case "MoveDown":
                    increment = 1; break;
            }

            long currentPocketNo = 0;
            long otherPocketNo = 0;

            string strCurrentPocketNo = (pocketsTable.Rows[currentIndexPockets]["POCKET_NO"] ?? "").ToString();
            if (string.IsNullOrEmpty(strCurrentPocketNo)) { throw new Exception("The field POCKET_NO on table SORT_PATTERN_POCKET is empty."); }
            else { currentPocketNo = long.Parse(strCurrentPocketNo); }

            string strOtherPocketNo = (pocketsTable.Rows[currentIndexPockets + increment]["POCKET_NO"] ?? "").ToString();
            if (string.IsNullOrEmpty(strOtherPocketNo)) { throw new Exception("The field POCKET_NO on table SORT_PATTERN_POCKET is empty."); }
            else { otherPocketNo = long.Parse(strOtherPocketNo); }

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  currentIndexPockets-> " + currentIndexPockets.ToString());
            logger.Debug(location + "  increment-> " + increment.ToString());
            logger.Debug(location + "  currentPocketNo-> " + currentPocketNo.ToString());
            logger.Debug(location + "  otherPocketNo-> " + otherPocketNo.ToString());

            pocketsTable.Rows[currentIndexPockets]["POCKET_NO"] = otherPocketNo;
            pocketsTable.Rows[currentIndexPockets]["ABS_POCKET_NO"] = Math.Abs(otherPocketNo);

            pocketsTable.Rows[currentIndexPockets + increment]["POCKET_NO"] = currentPocketNo;
            pocketsTable.Rows[currentIndexPockets + increment]["ABS_POCKET_NO"] = Math.Abs(currentPocketNo);

            DataRow currentRow = pocketsTable.Rows[currentIndexPockets].Clone();

            //Switch the 2 rows in the pockets table (next 2 lines)
            pocketsTable.Rows.RemoveAt(currentIndexPockets);
            pocketsTable.Rows.InsertAt(currentRow, currentIndexPockets + increment);

            if (conditionsTable != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in conditionsTable.Rows)");

                //Switch the rows in the conditions DataTable
                foreach (DataRow row in conditionsTable.Rows)
                {
                    if ((row["POCKET_NO"] ?? "").ToString() == currentPocketNo.ToString())
                    {
                        row["POCKET_NO"] = otherPocketNo;
                    }
                    else if ((row["POCKET_NO"] ?? "").ToString() == otherPocketNo.ToString())
                    {
                        row["POCKET_NO"] = currentPocketNo;
                    }
                }
            }

            if (listValuesTable != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in listValuesTable.Rows)");

                //Switch the rows in the list of values DataTable
                foreach (DataRow row in listValuesTable.Rows)
                {
                    if ((row["POCKET_NO"] ?? "").ToString() == currentPocketNo.ToString())
                    {
                        row["POCKET_NO"] = otherPocketNo;
                    }
                    else if ((row["POCKET_NO"] ?? "").ToString() == otherPocketNo.ToString())
                    {
                        row["POCKET_NO"] = currentPocketNo;
                    }
                }
            }

            //For traces   
            logger.Debug(location + "Before line: refreshDgvPockets();");

            refreshDgvPockets();

            isSomethingChanged = true;

            //Selects the current row after the switch
            dgvPockets.CurrentCell = dgvPockets[0, currentIndexPockets + increment];

            setTitleDgvConditions();

            enablePocketsMoveUpDownButtons();

            //For traces
            logger.Debug(location + " Ending...");

            //displays the default cursor
            Cursor.Current = Cursors.Default;
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

        //set the displayed title for the dgvPockets object
        private void setTitleDgvPockets()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: set the displayed title for the dgvPockets object");

            switch (getSelectedPocketsRadioButton())
            {
                case RADIO_PHYSICAL:
                    labelPockets.Text = rm.GetString("physicalPockets"); break;
                case RADIO_VIRTUAL:
                    labelPockets.Text = rm.GetString("virtualPockets"); break;
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //set the displayed title for the dgvConditions object
        public void setTitleDgvConditions()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: set the displayed title for the dgvConditions object");

            DataGridViewRow currentRow = dgvPockets.CurrentRow;
            DataTable pocketsTable = null;

            string titleConditions = rm.GetString("conditionsForPocket");
            pocketIDAndDescription = string.Empty;
 
            if (currentRow != null)
            {
                switch (getSelectedPocketsRadioButton())
                {
                    case RADIO_PHYSICAL:
                        pocketsTable = physicalPocketsTable;
                        break;
                    case RADIO_VIRTUAL:
                        pocketsTable = virtualPocketsTable;
                        break;
                }

                pocketIDAndDescription += (pocketsTable.Rows[currentRow.Index]["ABS_POCKET_NO"] ?? "").ToString();
                pocketIDAndDescription += " - ";
                pocketIDAndDescription += (pocketsTable.Rows[currentRow.Index]["DESCRIPTION"] ?? "").ToString();

                titleConditions += " " + pocketIDAndDescription;
            }

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  titleGrpBoxFromWorkType-> " + titleConditions);

            labelConditions.Text = titleConditions;

            //For traces
            logger.Debug(location + " Ending...");
        }

        //occurs when any field is modified
        private void somethingChanged(object sender, EventArgs e)
        {
            isSomethingChanged = true;
        }

        //Occurs when the form is closed
        private void frmSortPatternEdit_FormClosing(object sender, FormClosingEventArgs e)
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
                logger.Debug(location + "  isFormClosedByUser-> " + isFormClosedByUser.ToString());

                if (isSomethingChanged)
                {
                    //For traces   
                    logger.Debug(location + " After line: if (isSomethingChanged)");

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

                DialogResult result;

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  action-> " + action);
                logger.Debug(location + "  isSomethingChanged-> " + isSomethingChanged.ToString());

                switch (action)
                {
                    case ACTION_EDIT:
                    case ACTION_ADD:
                        if (validateForm())
                        {
                            //For traces   
                            logger.Debug(location + " After line: if (validateForm())");

                            if (isSomethingChanged)
                            {
                                //For traces   
                                logger.Debug(location + " After line: if (isSomethingChanged)");

                                //displays an hourglass cursor
                                Cursor.Current = Cursors.WaitCursor;

                                result = MessageBox.Show(rm.GetString("msgSaveChanges2"),
                                                         rm.GetString("titleSaveChanges2"),
                                                         MessageBoxButtons.OKCancel,
                                                         MessageBoxIcon.None);

                                //For traces
                                logger.Debug(location + "Values:");
                                logger.Debug(location + "  result-> " + result.ToString());

                                switch (result)
                                {
                                    case DialogResult.OK:
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

                        break;
                    case ACTION_REMOVE:
                        string id = txtID.Text;
                        string description = txtDescription.Text;

                        string message = "";
                        string sortPattern = "";

                        message += rm.GetString("msg1DeleteSortPattern") + sortPatternID.ToString() + ":";
                        message += "\n" + "\n";

                        sortPattern += String.IsNullOrEmpty(txtDescription.Text) ? "" : txtDescription.Text + "\n";
                  
                        message += String.IsNullOrEmpty(sortPattern) ? "" : sortPattern + "\n";
                        message += rm.GetString("msg2DeleteSortPattern");

                        result = MessageBox.Show(message,
                                                 rm.GetString("titleRemoveSortPattern"),
                                                 MessageBoxButtons.OKCancel,
                                                 MessageBoxIcon.Exclamation);

                        //For traces
                        logger.Debug(location + "Values:");
                        logger.Debug(location + "  result-> " + result.ToString());

                        switch (result)
                        {
                            case DialogResult.OK:
                                WorkTypeDefinition wtd = new WorkTypeDefinition(btecDB);
                                DataTable wtdTable = wtd.GetWorkType(sortPatternID);

                                if (wtdTable.Rows.Count == 0)
                                {
                                    //displays an hourglass cursor
                                    Cursor.Current = Cursors.WaitCursor;

                                    //For traces   
                                    logger.Debug(location + "Before line: SortPatternDef spd = new SortPatternDef(btecDB);");
                                    logger.Debug(location + "        and: SortPatternPocket spp = new SortPatternPocket(btecDB);");
                                    logger.Debug(location + "        and: SortPatternPocketCond sppc = new SortPatternPocketCond(btecDB);");
                                    logger.Debug(location + "        and: ListValues lv = new ListValues(btecDB);");

                                    SortPatternDef spd = new SortPatternDef(btecDB);
                                    SortPatternPocket spp = new SortPatternPocket(btecDB);
                                    SortPatternPocketCond sppc = new SortPatternPocketCond(btecDB);
                                    ListValues lv = new ListValues(btecDB);

                                    //For traces   
                                    logger.Debug(location + "Before line: lv.deleteListValues(sortPatternID);");

                                    lv.deleteListValues(sortPatternID);

                                    //For traces   
                                    logger.Debug(location + "Before line: sppc.deleteSortPatternPocketCond(sortPatternID);");
                                    
                                    sppc.deleteSortPatternPocketCond(sortPatternID);

                                    //For traces   
                                    logger.Debug(location + "Before line: spp.deleteSortPatternPocketID(sortPatternID);");
                                    
                                    spp.deleteSortPatternPocketID(sortPatternID);

                                    //For traces   
                                    logger.Debug(location + "Before line: spd.deleteSortPattern(sortPatternID);");
                                    
                                    spd.deleteSortPattern(sortPatternID);

                                    //displays the default cursor
                                    Cursor.Current = Cursors.Default;

                                    //For the parent form knows that there a change in the child data
                                    this.DialogResult = DialogResult.OK;
                                }
                                else
                                {
                                    string workTypeMsg = "";
                                    string workTypes = "";

                                    workTypeMsg += rm.GetString("msgSortPatternUsedByWorkTypes");
                                    workTypeMsg += "\n" + "\n";

                                    if (wtdTable != null)
                                    {
                                        //For traces   
                                        logger.Debug(location + "Before line: foreach (DataRow row in wtdTable.Rows)");

                                        foreach (DataRow row in wtdTable.Rows)
                                        {
                                            workTypes += String.IsNullOrEmpty(row["WORK_TYPE"].ToString()) ? "" : row["WORK_TYPE"].ToString() + "\n";
                                        }
                                    }

                                    workTypeMsg += String.IsNullOrEmpty(workTypes) ? "" : workTypes;

                                    MessageBox.Show(workTypeMsg, rm.GetString("titleImpossibleToRemove"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    //For the parent form knows that there was no change in the child data
                                    this.DialogResult = DialogResult.Cancel;
                                }
                            
                                isFormClosedByUser = true;

                                this.Close();
                                break;
                            case DialogResult.Cancel:
                                break;
                        }

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

        //Opens the frmSortPatternPocket form for a specific action
        private void editingPocket(string action)
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
            logger.Info(location + "Purpose: Opens the frmSortPatternPocket form for a specific action");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  action-> " + action);

            //For traces   
            logger.Debug(location + "Before line: frmSortPatternPocket _frm = new frmSortPatternPocket(this, btecDB, action, sortPatternID);");
            logger.Debug(location + "        and: DialogResult result = _frm.ShowDialog(this);");

            //Opens the child form for a specific sort pattern newPocketID corresponding to the sort pattern Id (next 2 lines)
            frmSortPatternPocket _frm = new frmSortPatternPocket(this, btecDB, action, sortPatternID);

            DialogResult result = _frm.ShowDialog(this);
            _frm.Dispose();

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  result-> " + result.ToString());

            if (result == DialogResult.OK)
            {
                //displays an hourglass cursor
                Cursor.Current = Cursors.WaitCursor;

                //For traces   
                logger.Debug(location + "Before line: refreshDgvPockets();");

                refreshDgvPockets();

                //For traces   
                logger.Debug(location + "Before line: refreshPocketSummary();");
                
                refreshPocketSummary();

                setTitleDgvConditions();

                //For traces   
                logger.Debug(location + "Before line: refreshDgvConditions();");
                
                refreshDgvConditions();

                enablePocketsEditRemoveButtons();
                enablePocketsMoveUpDownButtons();

                enableConditionsEditAddRemoveButtons();
            }

            //For traces
            logger.Debug(location + " Ending...");

            //displays the default cursor
            Cursor.Current = Cursors.Default;
        }

        //Occurs when a line in the dgvPockets object is double-clicked
        private void dgvPockets_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                logger.Info(location + "Purpose: Occurs when a line in the dgvPockets object is double-clicked");

                DataGridViewRow currentRow = dgvPockets.CurrentRow;

                if (currentRow != null)
                {
                    //For traces   
                    logger.Debug(location + "Before line: editingPocket(ACTION_EDIT);");

                    editingPocket(ACTION_EDIT);
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

        //Occurs when the [Edit] button is clicked for the dgvPockets object
        private void btnEditPocket_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Edit] button is clicked for the dgvPockets object");

                //For traces   
                logger.Debug(location + "Before line: editingPocket(ACTION_EDIT);");

                editingPocket(ACTION_EDIT);

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

        //Occurs when the [Remove] button is clicked for the dgvPockets object
        private void btnRemovePocket_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Remove] button is clicked for the dgvPockets object");

                //For traces   
                logger.Debug(location + "Before line: editingPocket(ACTION_REMOVE);");

                editingPocket(ACTION_REMOVE);

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

        //Occurs when the [Add] button is clicked for the dgvPockets object
        private void btnAddPocket_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Add] button is clicked for the dgvPockets object");

                //For traces   
                logger.Debug(location + "Before line: editingPocket(ACTION_ADD);");

                editingPocket(ACTION_ADD);

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

        //Opens the frmSortPatternCond form for a specific action
        private void editingCondition(string action)
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
            logger.Info(location + "Purpose: Opens the frmSortPatternCond form for a specific action");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  action-> " + action);

            //For traces   
            logger.Debug(location + "Before line: frmSortPatternCond _frm = new frmSortPatternCond(this, btecDB, action, sortPatternID);");
            logger.Debug(location + "        and: DialogResult result = _frm.ShowDialog(this);");

            //Opens the child form for a specific sort pattern newPocketID condition corresponding to the sort pattern Id (next 2 lines)
            frmSortPatternCond _frm = new frmSortPatternCond(this, btecDB, action, sortPatternID);

            DialogResult result = _frm.ShowDialog(this);
            _frm.Dispose();

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  result-> " + result.ToString());

            if (result == DialogResult.OK)
            {
                //displays an hourglass cursor
                Cursor.Current = Cursors.WaitCursor;

                //For traces   
                logger.Debug(location + "Before line: refreshDgvConditions();");

                refreshDgvConditions();

                //For traces   
                logger.Debug(location + "Before line: setConditionExpression();");
                
                setConditionExpression();
                
                enablePanelConditions();

                enableConditionsEditAddRemoveButtons();
            }

            //For traces
            logger.Debug(location + " Ending...");

            //displays the default cursor
            Cursor.Current = Cursors.Default;
        }

        //Occurs when a line in the dgvConditions object is double-clicked
        private void dgvConditions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                logger.Info(location + "Purpose: Occurs when a line in the dgvConditions object is double-clicked");

                DataGridViewRow currentRow = dgvConditions.CurrentRow;

                if (currentRow != null)
                {
                    //For traces   
                    logger.Debug(location + "Before line: editingCondition(ACTION_EDIT);");

                    editingCondition(ACTION_EDIT);
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

        //Occurs when the [Remove] button is clicked for the dgvConditions object
        private void btnRemoveCondition_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Remove] button is clicked for the dgvConditions object");

                //For traces   
                logger.Debug(location + "Before line: editingCondition(ACTION_REMOVE);");

                editingCondition(ACTION_REMOVE);

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

        //Occurs when the [Edit] button is clicked for the dgvConditions object
        private void btnEditCondition_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Edit] button is clicked for the dgvConditions object");

                //For traces   
                logger.Debug(location + "Before line: editingCondition(ACTION_EDIT);");

                editingCondition(ACTION_EDIT);

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

        //Occurs when the [Add] button is clicked for the dgvConditions object
        private void btnAddCondition_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Add] button is clicked for the dgvConditions object");

                //For traces   
                logger.Debug(location + "Before line: editingCondition(ACTION_ADD);");

                editingCondition(ACTION_ADD);

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

        //Performs all the form's validation before saving
        private bool validateForm()
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
            logger.Info(location + "Purpose: Performs all the form's validation before saving");

            //Validation 1
            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                //Set the focus to the Description field
                txtDescription.Focus();

                MessageBox.Show(rm.GetString("msgDescriptionForSortPattern"),
                                rm.GetString("TitleMsgValidationError"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return false;
            }

            //For traces   
            logger.Debug(location + "Before line: if (!validatePhysicalPockets())");

            if (!validatePhysicalPockets()) 
            {
                //displays the default cursor
                Cursor.Current = Cursors.Default;

                return false; 
            }

            //For traces   
            logger.Debug(location + "Before line: if (!validateVirtualPockets())");

            if (!validateVirtualPockets())
            {
                //displays the default cursor
                Cursor.Current = Cursors.Default;

                return false;
            }

            //For traces
            logger.Debug(location + " Ending...");

            //displays the default cursor
            Cursor.Current = Cursors.Default;

            return true;
        }

        //Performs a serie of validation on the Physical pockets rows
        private bool validatePhysicalPockets()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Performs a serie of validation on the Physical pockets rows");

            string PHYSICAL_OR_VIRTUAL = rm.GetString("txtPhysical");
            const string RADIO_POCKETS = RADIO_PHYSICAL; 

            DataRow[] pocketsTableRows;
            DataTable partialTable = null;

            string pocketType;
            int softwareRejectCount = 0;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  PHYSICAL_OR_VIRTUAL-> " + PHYSICAL_OR_VIRTUAL);
            logger.Debug(location + "  RADIO_POCKETS-> " + RADIO_POCKETS);

            pocketType = TYPE_KILL + " " + rm.GetString("or") + " " + TYPE_REHANDLE;
            pocketsTableRows = physicalPocketsTable.Select("POCKET_TYPE = 0 or POCKET_TYPE = 1", "ABS_POCKET_NO");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  pocketType-> " + pocketType);

            if (pocketsTableRows.Count() > 0)
            {
                partialTable = pocketsTableRows.CopyToDataTable();

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionPriority(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");

                //Validation 2
                if (!validation_ConditionPriority(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionExpressionNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");

                //Validation 3
                if (!validation_ConditionExpressionNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }
            }

            pocketType = TYPE_REJECT_SOFT;
            pocketsTableRows = physicalPocketsTable.Select("POCKET_TYPE = 2 and REJECT_CODE = 1", "ABS_POCKET_NO");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  pocketType-> " + pocketType);

            if (pocketsTableRows.Count() > 0)
            {
                partialTable = pocketsTableRows.CopyToDataTable();

                softwareRejectCount = partialTable.Rows.Count;

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionPriority(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 4
                if (!validation_ConditionPriority(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionExpressionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 5
                if (!validation_ConditionExpressionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionExpressionNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 6
                if (!validation_ConditionExpressionNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }
            }

            pocketType = TYPE_REJECT_EXCEP;
            pocketsTableRows = physicalPocketsTable.Select("POCKET_TYPE = 2 and REJECT_CODE = 5", "ABS_POCKET_NO");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  pocketType-> " + pocketType);

            if (pocketsTableRows.Count() > 0)
            {
                partialTable = pocketsTableRows.CopyToDataTable();

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionPriority(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 7
                if (!validation_ConditionPriority(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionExpressionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 8
                if (!validation_ConditionExpressionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionExpressionNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 9
                if (!validation_ConditionExpressionNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }
            }

            pocketType = TYPE_MISS_FREE + " " + rm.GetString("or") +" " + TYPE_MISS_FREE_REJECT;
            pocketsTableRows = physicalPocketsTable.Select("POCKET_TYPE = 7 or POCKET_TYPE = 8", "ABS_POCKET_NO");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  pocketType-> " + pocketType);

            if (pocketsTableRows.Count() > 0)
            {
                partialTable = pocketsTableRows.CopyToDataTable();

                //For traces   
                logger.Debug(location + "Before line: if (!validation_MultiplePockets(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 10
                if (!validation_MultiplePockets(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_OnePocketConditionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 11
                if (!validation_OnePocketConditionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_OnePocketWithSoftwareReject(partialTable, RADIO_POCKETS, softwareRejectCount))");
                
                //Validation 12
                if (!validation_OnePocketWithSoftwareReject(partialTable, RADIO_POCKETS, softwareRejectCount)) { return false; }
            }

            pocketType = TYPE_ELECTRONIC;
            pocketsTableRows = physicalPocketsTable.Select("POCKET_TYPE = 3", "ABS_POCKET_NO");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  pocketType-> " + pocketType);

            if (pocketsTableRows.Count() > 0)
            {
                partialTable = pocketsTableRows.CopyToDataTable();

                //For traces   
                logger.Debug(location + "Before line: if (!validation_MultiplePockets(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 13
                if (!validation_MultiplePockets(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_OnePocketConditionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 14
                if (!validation_OnePocketConditionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }
            }

            pocketType = TYPE_ARC;
            pocketsTableRows = physicalPocketsTable.Select("POCKET_TYPE = 4", "ABS_POCKET_NO");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  pocketType-> " + pocketType);

            if (pocketsTableRows.Count() > 0)
            {
                partialTable = pocketsTableRows.CopyToDataTable();

                //For traces   
                logger.Debug(location + "Before line: if (!validation_MultiplePockets(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 15
                if (!validation_MultiplePockets(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_OnePocketConditionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 16
                if (!validation_OnePocketConditionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }
            }

            //For traces
            logger.Debug(location + " Ending...");

            return true;
        }

        //Performs a serie of validation on the Virtual pockets rows
        private bool validateVirtualPockets()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Performs a serie of validation on the Virtual pockets rows");

            string PHYSICAL_OR_VIRTUAL = rm.GetString("txtVirtual");
            const string RADIO_POCKETS = RADIO_VIRTUAL;

            DataRow[] pocketsTableRows;
            DataTable partialTable = null;

            string pocketType;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  PHYSICAL_OR_VIRTUAL-> " + PHYSICAL_OR_VIRTUAL);
            logger.Debug(location + "  RADIO_POCKETS-> " + RADIO_POCKETS);

            pocketType = TYPE_KILL;
            pocketsTableRows = virtualPocketsTable.Select("POCKET_TYPE = 0", "ABS_POCKET_NO");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  pocketType-> " + pocketType);

            if (pocketsTableRows.Count() > 0)
            {
                partialTable = pocketsTableRows.CopyToDataTable();

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionPriority(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 17
                if (!validation_ConditionPriority(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionExpressionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 18
                if (!validation_ConditionExpressionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionExpressionNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 19
                if (!validation_ConditionExpressionNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }
            }

            pocketType = TYPE_ARC;
            pocketsTableRows = virtualPocketsTable.Select("POCKET_TYPE = 4", "ABS_POCKET_NO");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  pocketType-> " + pocketType);
            
            if (pocketsTableRows.Count() > 0)
            {
                partialTable = pocketsTableRows.CopyToDataTable();

                //For traces   
                logger.Debug(location + "Before line: if (!validation_MultiplePockets(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 20
                if (!validation_MultiplePockets(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_OnePocketConditionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 21
                if (!validation_OnePocketConditionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }
            }

            pocketType = TYPE_IE;
            pocketsTableRows = virtualPocketsTable.Select("POCKET_TYPE = 5", "ABS_POCKET_NO");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  pocketType-> " + pocketType);
            
            if (pocketsTableRows.Count() > 0)
            {
                partialTable = pocketsTableRows.CopyToDataTable();

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionPriority(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 22
                if (!validation_ConditionPriority(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionExpressionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 23
                if (!validation_ConditionExpressionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionExpressionNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 24
                if (!validation_ConditionExpressionNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }
            }

            pocketType = TYPE_IRD;
            pocketsTableRows = virtualPocketsTable.Select("POCKET_TYPE = 6", "ABS_POCKET_NO");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  pocketType-> " + pocketType);
            
            if (pocketsTableRows.Count() > 0)
            {
                partialTable = pocketsTableRows.CopyToDataTable();

                //For traces   
                logger.Debug(location + "Before line: if (!validation_MultiplePockets(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 25
                if (!validation_MultiplePockets(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_OnePocketConditionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 26
                if (!validation_OnePocketConditionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }
            }

            pocketType = TYPE_REJECT_VIRT;
            pocketsTableRows = virtualPocketsTable.Select("POCKET_TYPE = 2 and REJECT_CODE = 6", "ABS_POCKET_NO");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  pocketType-> " + pocketType);
            
            if (pocketsTableRows.Count() > 0)
            {
                partialTable = pocketsTableRows.CopyToDataTable();

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionPriority(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 27
                if (!validation_ConditionPriority(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionExpressionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 28
                if (!validation_ConditionExpressionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionExpressionNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 29
                if (!validation_ConditionExpressionNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }
            }

            pocketType = TYPE_REJECT_VIRT_EXCEP;
            pocketsTableRows = virtualPocketsTable.Select("POCKET_TYPE = 2 and REJECT_CODE = 7", "ABS_POCKET_NO");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  pocketType-> " + pocketType);
            
            if (pocketsTableRows.Count() > 0)
            {
                partialTable = pocketsTableRows.CopyToDataTable();

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionPriority(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 30
                if (!validation_ConditionPriority(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionExpressionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 31
                if (!validation_ConditionExpressionNotNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }

                //For traces   
                logger.Debug(location + "Before line: if (!validation_ConditionExpressionNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType))");
                
                //Validation 32
                if (!validation_ConditionExpressionNull(partialTable, RADIO_POCKETS, PHYSICAL_OR_VIRTUAL, pocketType)) { return false; }
            }

            //For traces
            logger.Debug(location + " Ending...");

            return true;
        }

        //Provides validation to ensure that it was no Software Reject newPocketID if it exists a Missing & Free & Reject newPocketID 
        private bool validation_OnePocketWithSoftwareReject(DataTable partialTable,
                                                            string radioPockets,
                                                            int softwareRejectCount)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Provides validation to ensure that it was no Software Reject newPocketID if it exists a Missing & Free & Reject newPocketID");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  radioPockets-> " + radioPockets);
            logger.Debug(location + "  softwareRejectCount-> " + softwareRejectCount.ToString());

            string MSG_ONE_MISSING_WITH_SOFTWARE_REJECT = rm.GetString("msgOneMissingWithSoftwareReject");

            string pocket1;

            if (softwareRejectCount > 0)
            {
                //For traces   
                logger.Debug(location + " After line: if (softwareRejectCount > 0)");

                if (partialTable.Rows.Count == 1)
                {
                    if ((partialTable.Rows[0]["POCKET_TYPE"] ?? "").ToString().Equals("8"))
                    {
                        pocket1 = (partialTable.Rows[0]["ABS_POCKET_NO"] ?? "").ToString();
                        if (string.IsNullOrEmpty(pocket1)) { throw new Exception("The field ABS_POCKET_NO on Physical or Virtual pockets DataTable is empty."); }

                        //For traces
                        logger.Debug(location + "Values:");
                        logger.Debug(location + "  pocket1-> " + pocket1);

                        //For traces   
                        logger.Debug(location + "Before line: selectPocket(radioPockets, pocket1);");

                        selectPocket(radioPockets, pocket1);

                        MessageBox.Show(string.Format(MSG_ONE_MISSING_WITH_SOFTWARE_REJECT, TYPE_MISS_FREE_REJECT, TYPE_REJECT_SOFT),
                                        TITLE_VALIDATION,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

                        return false;
                    }
                }
            }

            //For traces
            logger.Debug(location + " Ending...");

            return true;
        }

        //Provides validation to ensure that CONDITION_EXPRESSION is null for a only one newPocketID of a specific POCKET_TYPE 
        private bool validation_OnePocketConditionNotNull(DataTable partialTable,
                                                          string radioPockets,
                                                          string physicalOrVirtual,
                                                          string pocketType)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Provides validation to ensure that CONDITION_EXPRESSION is null for a only one newPocketID of a specific POCKET_TYPE");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  radioPockets-> " + radioPockets);
            logger.Debug(location + "  physicalOrVirtual-> " + physicalOrVirtual);
            logger.Debug(location + "  pocketType-> " + pocketType);

            string MSG_ONE_MISSING_CONDITION_NULL = rm.GetString("msgOneMissingConditionNull");

            string pocket1;

            if (partialTable.Rows.Count == 1)
            {
                if (!string.IsNullOrEmpty((partialTable.Rows[0]["CONDITION_EXPRESSION"] ?? "").ToString()))
                {
                    pocket1 = (partialTable.Rows[0]["ABS_POCKET_NO"] ?? "").ToString();
                    if (string.IsNullOrEmpty(pocket1)) { throw new Exception("The field ABS_POCKET_NO on Physical or Virtual pockets DataTable is empty."); }

                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  pocket1-> " + pocket1);

                    //For traces   
                    logger.Debug(location + "Before line: selectPocket(radioPockets, pocket1);");

                    selectPocket(radioPockets, pocket1);

                    MessageBox.Show(string.Format(MSG_ONE_MISSING_CONDITION_NULL, physicalOrVirtual, pocketType, pocket1),
                                    TITLE_VALIDATION,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    return false;
                }
            }

            //For traces
            logger.Debug(location + " Ending...");

            return true;
        }

        //Provides validation to ensure that it have only one newPocketID for a specific POCKET_TYPE 
        private bool validation_MultiplePockets(DataTable partialTable,
                                                string radioPockets,
                                                string physicalOrVirtual,
                                                string pocketType)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Provides validation to ensure that it have only one newPocketID for a specific POCKET_TYPE");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  radioPockets-> " + radioPockets);
            logger.Debug(location + "  physicalOrVirtual-> " + physicalOrVirtual);
            logger.Debug(location + "  pocketType-> " + pocketType);

            string MSG_ONLY_ONE = rm.GetString("msgOnlyOne");

            string pocket1;

            if (partialTable.Rows.Count > 1)
            {
                pocket1 = (partialTable.Rows[0]["ABS_POCKET_NO"] ?? "").ToString();
                if (string.IsNullOrEmpty(pocket1)) { throw new Exception("The field ABS_POCKET_NO on Physical or Virtual pockets DataTable is empty."); }

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  pocket1-> " + pocket1);

                //For traces   
                logger.Debug(location + "Before line: selectPocket(radioPockets, pocket1);");

                selectPocket(radioPockets, pocket1);

                MessageBox.Show(string.Format(MSG_ONLY_ONE, physicalOrVirtual, pocketType),
                                TITLE_VALIDATION,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return false;
            }

            //For traces
            logger.Debug(location + " Ending...");
            
            return true;
        }

        //Provides validation to ensure that the last newPocketID must have CONDITION_EXPRESSION to null for a specific POCKET_TYPE 
        private bool validation_ConditionExpressionNotNull(DataTable partialTable,
                                                           string radioPockets,
                                                           string physicalOrVirtual,
                                                           string pocketType)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Provides validation to ensure that the last newPocketID must have CONDITION_EXPRESSION to null for a specific POCKET_TYPE");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  radioPockets-> " + radioPockets);
            logger.Debug(location + "  physicalOrVirtual-> " + physicalOrVirtual);
            logger.Debug(location + "  pocketType-> " + pocketType);

            string MSG_ONLY_POCKET = rm.GetString("msgOnlyPocket");
            string MSG_LAST_POCKET = rm.GetString("msgLastPocket");

            string pocket1;
            int currentIndex = partialTable.Rows.Count - 1;

            if (currentIndex > -1)
            {
                if (!string.IsNullOrEmpty((partialTable.Rows[currentIndex]["CONDITION_EXPRESSION"] ?? "").ToString()))
                {
                    pocket1 = (partialTable.Rows[currentIndex]["ABS_POCKET_NO"] ?? "").ToString();
                    if (string.IsNullOrEmpty(pocket1)) { throw new Exception("The field ABS_POCKET_NO on Physical or Virtual pockets DataTable is empty."); }

                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  currentIndex-> " + currentIndex.ToString());
                    logger.Debug(location + "  pocket1-> " + pocket1);

                    //For traces   
                    logger.Debug(location + "Before line: selectPocket(radioPockets, pocket1);");

                    selectPocket(radioPockets, pocket1);

                    if (currentIndex == 0)
                    {
                        MessageBox.Show(string.Format(MSG_ONLY_POCKET, physicalOrVirtual, pocket1, pocketType),
                                        TITLE_VALIDATION,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(string.Format(MSG_LAST_POCKET, physicalOrVirtual, pocket1, pocketType),
                                        TITLE_VALIDATION,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }

                    return false;
                }
            }

            //For traces
            logger.Debug(location + " Ending...");

            return true;
        }

        //Provides validation to ensure that the first newPocketID with CONDITION_EXPRESSION to null must be the last newPocketID for a specific POCKET_TYPE 
        private bool validation_ConditionExpressionNull(DataTable partialTable,
                                                        string radioPockets,
                                                        string physicalOrVirtual,
                                                        string pocketType)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Provides validation to ensure that the first newPocketID with CONDITION_EXPRESSION to null must be the last newPocketID for a specific POCKET_TYPE");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  radioPockets-> " + radioPockets);
            logger.Debug(location + "  physicalOrVirtual-> " + physicalOrVirtual);
            logger.Debug(location + "  pocketType-> " + pocketType);

            string MSG_MUST_HAVE = rm.GetString("msgMustHave");

            string pocket1;
            int currentIndex;

            DataRow[] conditionNullRows = partialTable.Select("CONDITION_EXPRESSION is null", "ABS_POCKET_NO");

            if (conditionNullRows.Count() > 0)
            {
                DataRow conditionNullFirstRow = conditionNullRows.First();

                currentIndex = partialTable.Rows.IndexOf(conditionNullFirstRow);

                if (currentIndex < partialTable.Rows.Count - 1)
                {
                    pocket1 = (partialTable.Rows[currentIndex]["ABS_POCKET_NO"] ?? "").ToString();
                    if (string.IsNullOrEmpty(pocket1)) { throw new Exception("The field ABS_POCKET_NO on Physical or Virtual pockets DataTable is empty."); }

                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  pocket1-> " + pocket1);

                    //For traces   
                    logger.Debug(location + "Before line: selectPocket(radioPockets, pocket1);");

                    selectPocket(radioPockets, pocket1);

                    MessageBox.Show(string.Format(MSG_MUST_HAVE, physicalOrVirtual, pocket1, pocketType),
                                    TITLE_VALIDATION,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    return false;
                }
            }

            //For traces
            logger.Debug(location + " Ending...");

            return true;
        }

        //Provides validation to ensure that 2 pockets have not the same CONDITION_PRIORITY for a specific POCKET_TYPE 
        private bool validation_ConditionPriority(DataTable partialTable, 
                                                  string radioPockets,
                                                  string physicalOrVirtual,
                                                  string pocketType)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Provides validation to ensure that 2 pockets have not the same CONDITION_PRIORITY for a specific POCKET_TYPE");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  radioPockets-> " + radioPockets);
            logger.Debug(location + "  physicalOrVirtual-> " + physicalOrVirtual);
            logger.Debug(location + "  pocketType-> " + pocketType);

            string MSG_SAME_PRIORITY = rm.GetString("msgSamePriority");

            string conditionPriority;
            string pocket1;
            string pocket2;

            if (partialTable != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in partialTable.Rows)");

                foreach (DataRow row in partialTable.Rows)
                {
                    conditionPriority = (row["CONDITION_PRIORITY"] ?? "").ToString();
                    if (string.IsNullOrEmpty(conditionPriority)) { throw new Exception("The field CONDITION_PRIORITY on physicalPocketsTable DataTable is empty."); }

                    var samePriorityRows = partialTable.Select("CONDITION_PRIORITY = " + conditionPriority, "ABS_POCKET_NO");

                    if (samePriorityRows.Count() > 1)
                    {
                        pocket1 = (samePriorityRows.ElementAt(0)["ABS_POCKET_NO"] ?? "").ToString();
                        if (string.IsNullOrEmpty(pocket1)) { throw new Exception("The field ABS_POCKET_NO on Physical or Virtual pockets DataTable is empty."); }

                        pocket2 = (samePriorityRows.ElementAt(1)["ABS_POCKET_NO"] ?? "").ToString();
                        if (string.IsNullOrEmpty(pocket2)) { throw new Exception("The field ABS_POCKET_NO on Physical or Virtual pockets DataTable is empty."); }

                        //For traces
                        logger.Debug(location + "Values:");
                        logger.Debug(location + "  pocket1-> " + pocket1);
                        logger.Debug(location + "  pocket2-> " + pocket2);

                        //For traces   
                        logger.Debug(location + "Before line: selectPocket(radioPockets, pocket1);");

                        selectPocket(radioPockets, pocket1);

                        MessageBox.Show(string.Format(MSG_SAME_PRIORITY, physicalOrVirtual, pocket1, physicalOrVirtual, pocket2, pocketType),
                                        TITLE_VALIDATION,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

                        return false;
                    }
                }
            }

            //For traces
            logger.Debug(location + " Ending...");

            return true;
        }

        //Selects a row in the dgvPockets object for a specific POCKET_NO
        private void selectPocket(string radioPockets, string pocketID)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Selects a row in the dgvPockets object for a specific POCKET_NO");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  radioPockets-> " + radioPockets);
            logger.Debug(location + "  pocketID-> " + pocketID);

            DataTable pocketsTable = null;

            switch (radioPockets)
            {
                case RADIO_PHYSICAL:
                    radioPhysical.Checked = true;
                    pocketsTable = physicalPocketsTable; 
                    break;
                case RADIO_VIRTUAL:
                    radioVirtual.Checked = true;
                    pocketsTable = virtualPocketsTable; 
                    break;
            }

            DataRow[] pocketNoRows = pocketsTable.Select("ABS_POCKET_NO = " + pocketID);

            if (pocketNoRows.Count() > 0)
            {
                DataRow row = pocketNoRows.First();

                int currentIndex = pocketsTable.Rows.IndexOf(row);

                dgvPockets.CurrentCell = dgvPockets[0, currentIndex];

                //displays an hourglass cursor
                Cursor.Current = Cursors.WaitCursor;

                setTitleDgvConditions();
                
                //For traces   
                logger.Debug(location + "Before line: refreshDgvConditions();");

                refreshDgvConditions();

                enablePocketsMoveUpDownButtons();
                enableConditionsEditAddRemoveButtons();

                //displays the default cursor
                Cursor.Current = Cursors.Default;
            }

            //For traces
            logger.Debug(location + " Ending...");
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

            string buffer = txtDescription.Text.Trim();
            string description = buffer.Substring(0, Math.Min(buffer.Length, 256));

            string hardwareReject = "1";
            string missingAndFree;

            DataRow[] pocketsTableRows = physicalPocketsTable.Select("POCKET_TYPE = 7 or POCKET_TYPE = 8", "ABS_POCKET_NO");

            if (pocketsTableRows.Count() > 0)
            {
                DataRow row = pocketsTableRows.First();

                missingAndFree = row["POCKET_NO"].ToString();
            }
            else
            {
                missingAndFree = "0";
            }

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  sortPatternID-> " + sortPatternID.ToString());
            logger.Debug(location + "  description-> " + description);
            logger.Debug(location + "  hardwareReject-> " + hardwareReject);
            logger.Debug(location + "  missingAndFree-> " + missingAndFree);

            //For traces   
            logger.Debug(location + "Before line: SortPatternDef spd = new SortPatternDef(btecDB);");
            logger.Debug(location + "        and: SortPatternPocket spp = new SortPatternPocket(btecDB);");
            logger.Debug(location + "        and: SortPatternPocketCond sppc = new SortPatternPocketCond(btecDB);");
            logger.Debug(location + "        and: ListValues lv = new ListValues(btecDB);");

            SortPatternDef spd = new SortPatternDef(btecDB);
            SortPatternPocket spp = new SortPatternPocket(btecDB);
            SortPatternPocketCond sppc = new SortPatternPocketCond(btecDB);
            ListValues lv = new ListValues(btecDB);

            //For traces   
            logger.Debug(location + "Before line: spd.saveSortPatternDefInfo(sortPatternID, description, hardwareReject, missingAndFree);");

            //Saves the data in the SORT_PATTERN_DEFINITION table
            spd.saveSortPatternDefInfo(sortPatternID, description, hardwareReject, missingAndFree);

            //For traces   
            logger.Debug(location + "Before line: lv.deleteListValues(sortPatternID);");

            lv.deleteListValues(sortPatternID);

            //For traces   
            logger.Debug(location + "Before line: sppc.deleteSortPatternPocketCond(sortPatternID);");
            
            sppc.deleteSortPatternPocketCond(sortPatternID);

            //For traces   
            logger.Debug(location + "Before line: spp.deleteSortPatternPocketID(sortPatternID);");
            
            spp.deleteSortPatternPocketID(sortPatternID);

            //For traces   
            logger.Debug(location + "Before line: saveSortPatternPocket(spp, physicalPocketsTable, true);");

            saveSortPatternPocket(spp, physicalPocketsTable, true);

            //For traces   
            logger.Debug(location + "Before line: saveSortPatternPocket(spp, virtualPocketsTable, false);");
            
            saveSortPatternPocket(spp, virtualPocketsTable, false);

            if (conditionsTable != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in conditionsTable.Rows)");

                //Saves all data from the conditions DataTable 
                foreach (DataRow row in conditionsTable.Rows)
                {
                    string pocketNo = (row["POCKET_NO"] ?? "").ToString();
                    pocketNo = (string.IsNullOrEmpty(pocketNo) ? "null" : pocketNo);

                    string conditionSequence = (row["CONDITION_SEQUENCE"] ?? "").ToString();
                    conditionSequence = (string.IsNullOrEmpty(conditionSequence) ? "null" : conditionSequence);

                    //Saves the data in the SORT_PATTERN_POCKET_CONDITION table
                    sppc.saveSortPatternPocketCondInfo(sortPatternID,
                                                       pocketNo,
                                                       conditionSequence,
                                                       row["TABLE_NAME"].ToString(),
                                                       row["FIELD_NAME"].ToString(),
                                                       row["LOGIC_OPERATOR"].ToString(),
                                                       row["FIELD_TYPE"].ToString(),
                                                       row["VALUE1"].ToString(),
                                                       row["VALUE2"].ToString());
                }
            }

            if (listValuesTable != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in listValuesTable.Rows)");

                //Saves all data from the list of values DataTable 
                foreach (DataRow row in listValuesTable.Rows)
                {
                    string pocketNo = (row["POCKET_NO"] ?? "").ToString();
                    pocketNo = (string.IsNullOrEmpty(pocketNo) ? "null" : pocketNo);

                    string conditionSequence = (row["CONDITION_SEQUENCE"] ?? "").ToString();
                    conditionSequence = (string.IsNullOrEmpty(conditionSequence) ? "null" : conditionSequence);

                    //Saves the data in the LIST_VALUES table
                    lv.saveListValuesInfo(sortPatternID,
                                          pocketNo,
                                          conditionSequence,
                                          row["VALUE"].ToString());
                }
            }

            //For traces
            logger.Debug(location + " Ending...");

            //displays the default cursor
            Cursor.Current = Cursors.Default;
        }

        //Save the data in the SORT_PATTERN_POCKET table from the Physical or Virtual pockets DataTable
        private void saveSortPatternPocket(SortPatternPocket spp, DataTable pocketsTable, bool isPhysicalPocketDef)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Save the data in the SORT_PATTERN_POCKET table from the Physical or Virtual pockets DataTable");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  isPhysicalPocketDef-> " + isPhysicalPocketDef.ToString());

            if (pocketsTable != null)
            {
                //For traces   
                logger.Debug(location + "Before line: foreach (DataRow row in pocketsTable.Rows)");

                foreach (DataRow row in pocketsTable.Rows)
                {
                    string pocketNo = (row["POCKET_NO"] ?? "").ToString();
                    pocketNo = (string.IsNullOrEmpty(pocketNo) ? "null" : pocketNo);

                    string pocketType = (row["POCKET_TYPE"] ?? "").ToString();
                    pocketType = (string.IsNullOrEmpty(pocketType) ? "null" : pocketType);

                    string conditionPriority = (row["CONDITION_PRIORITY"] ?? "").ToString();
                    conditionPriority = (string.IsNullOrEmpty(conditionPriority) ? "null" : conditionPriority);

                    string endPointID = (row["END_POINT_ID"] ?? "").ToString();
                    endPointID = (string.IsNullOrEmpty(endPointID) ? "null" : endPointID);

                    string sortPatternUniqueID = (row["SORT_PATTERN_UNIQUE_ID"] ?? "").ToString();
                    sortPatternUniqueID = (string.IsNullOrEmpty(sortPatternUniqueID) ? "null" : sortPatternUniqueID);

                    string rejectCode = (row["REJECT_CODE"] ?? "").ToString();
                    rejectCode = (string.IsNullOrEmpty(rejectCode) ? "null" : rejectCode);

                    //Saves the data in the SORT_PATTERN_POCKET table
                    spp.saveSortPatternPocketInfo(sortPatternID,
                                                  pocketNo,
                                                  row["DESCRIPTION"].ToString(),
                                                  pocketType,
                                                  conditionPriority,
                                                  row["CONDITION_EXPRESSION"].ToString(),
                                                  endPointID,
                                                  sortPatternUniqueID,
                                                  (isPhysicalPocketDef ? row["PHYSICAL_POCKET_DEF"].ToString() : null),
                                                  rejectCode);
                }
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Occurs when a cell of the dgvPockets object is selected
        private void dgvPockets_CurrentCellChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when a cell of the dgvPockets object is selected");

                //displays an hourglass cursor
                Cursor.Current = Cursors.WaitCursor;

                setTitleDgvConditions();

                //For traces   
                logger.Debug(location + "Before line: refreshDgvConditions();");
                    
                refreshDgvConditions();

                enablePocketsMoveUpDownButtons();
                enableConditionsEditAddRemoveButtons();

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