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
using BancTec.PCR2P.Core.DatabaseModel;
using System.Threading;

namespace Administration
{
    public partial class frmSortPatternCond : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmSortPatternCond).Name;

        const string ACTION_EDIT = "Edit";
        const string ACTION_ADD = "Add";
        const string ACTION_REMOVE = "Remove";

        const string DOMAIN_ID = "TMSPCT";
        const string END_POINT_ID = "24";

        IBtecDB btecDB;
        ResourceManager rm;
        ILoggerBtec logger;

        frmSortPatternEdit parentForm;
        string action;
        long sortPatternID;
        long pocketID;
        long sequence;

        DataTable LogicOperatorTable;
        string[] NamesAcceptedForList;

        bool isSomethingChanged = false;
        bool isFormClosedByUser = false;

        public frmSortPatternCond(frmSortPatternEdit parent, IBtecDB btecDBParent, string actionParent, long sortPatternIDParent)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmSortPatternCond));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmSortPatternCond));
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
        private void frmSortPatternCond_Load(object sender, EventArgs e)
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

                switch (action)
                {
                    case ACTION_EDIT:
                        this.Text = rm.GetString("editCondition");
                        btnAction.Text = rm.GetString("btnOK");
                        break;
                    case ACTION_ADD:
                        this.Text = rm.GetString("addCondition");
                        btnAction.Text = rm.GetString("btnAdd");
                        break;
                    case ACTION_REMOVE:
                        this.Text = rm.GetString("removeCondition");
                        btnAction.Text = rm.GetString("btnRemove");
                        break;
                }

                int currentIndexPockets = parentForm.dgvPockets.CurrentRow.Index;

                DataTable pocketsTable = null;

                switch (parentForm.getSelectedPocketsRadioButton())
                {
                    case frmSortPatternEdit.RADIO_PHYSICAL:
                        pocketsTable = parentForm.physicalPocketsTable;
                        break;
                    case frmSortPatternEdit.RADIO_VIRTUAL:
                        pocketsTable = parentForm.virtualPocketsTable;
                        break;
                }

                string strPocketID = (pocketsTable.Rows[currentIndexPockets]["POCKET_NO"] ?? "").ToString();

                if (string.IsNullOrEmpty(strPocketID)) { throw new Exception("A value of the POCKET_NO field in the SORT_PATTERN_POCKET table is empty."); }
                else { pocketID = long.Parse(strPocketID); }

                LogicOperatorTable = new DataTable();
                LogicOperatorTable.Columns.Add("OPERATOR", typeof(string));
                LogicOperatorTable.Columns.Add("DESCRIPTION", typeof(string));

                string[,] LogicOperatorList = new string[6, 2] {{"BT", rm.GetString("between")},
                                                                {"EQ", rm.GetString("equal")},
                                                                {"GE", rm.GetString("greaterOrEqual")},
                                                                {"GT", rm.GetString("greaterThan")},
                                                                {"LE", rm.GetString("lesserOrEqual")},
                                                                {"LT", rm.GetString("lessThan")}};

                NamesAcceptedForList = new string[28] {"ITEM_PAYMENT_INSTITUTION_TRANSIT",
                                                       "ITEM_PAYMENT_BANK_ACCOUNT",
                                                       "ITEM_PAYMENT_SUB_DOC_ID",
                                                       "ITEM_PAYMENT_TEXT_1",
                                                       "ITEM_PAYMENT_TEXT_2",
                                                       "ITEM_PAYMENT_TEXT_3",
                                                       "ITEM_STATEMENT_TEXT_1",
                                                       "ITEM_STATEMENT_TEXT_2",
                                                       "ITEM_STATEMENT_TEXT_3",
                                                       "ITEM_STATEMENT_TEXT_4",
                                                       "ITEM_STATEMENT_TEXT_5",
                                                       "ITEM_STATEMENT_TEXT_6",
                                                       "ITEM_STATEMENT_TEXT_7",
                                                       "ITEM_STATEMENT_TEXT_8",
                                                       "ITEM_STATEMENT_TEXT_9",
                                                       "ITEM_STATEMENT_TEXT_10",
                                                       "ITEM_STATEMENT_TEXT_11",
                                                       "ITEM_STATEMENT_TEXT_12",
                                                       "ITEM_STATEMENT_TEXT_13",
                                                       "ITEM_STATEMENT_TEXT_14",
                                                       "ITEM_STATEMENT_TEXT_15",
                                                       "MATCHED_PAYMENT_DETAIL_TEXT_1",
                                                       "MATCHED_PAYMENT_DETAIL_INTERFACE_TRACE1",
                                                       "MATCHED_PAYMENT_DETAIL_INTERFACE_TRACE2",
                                                       "MATCHED_PAYMENT_DETAIL_INTERFACE_TRACE3",
                                                       "MATCHED_PAYMENT_DETAIL_INTERFACE_TRACE4",
                                                       "MATCHED_PAYMENT_DETAIL_INTERFACE_TRACE5",
                                                       "ITEM_STATEMENT_SUB_STATEMENT_ID"};

                for (int i = 0; i < LogicOperatorList.GetLength(0); i++)
                {
                    LogicOperatorTable.Rows.Add(LogicOperatorList[i, 0], LogicOperatorList[i, 1]);
                }

                //For traces   
                logger.Debug(location + "Before line: ApplicationConstant ac = new ApplicationConstant(btecDB);");

                ApplicationConstant ac = new ApplicationConstant(btecDB);

                cboConditionField.SelectedValueChanged -= cboConditionField_SelectedValueChanged;
                cboConditionField.DataSource = ac.GetApplicationConstant(DOMAIN_ID);

                //Allows to know which language is being used
                string language = Thread.CurrentThread.CurrentCulture.DisplayName;

                switch (language)
                {
                    case "French (Canada)":
                        cboConditionField.DisplayMember = "FRENCH_DESCRIPTION"; break;
                    default:
                        cboConditionField.DisplayMember = "ENGLISH_DESCRIPTION"; break;
                }

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  action-> " + action);
                logger.Debug(location + "  currentIndexPockets-> " + currentIndexPockets.ToString());
                logger.Debug(location + "  pocketID-> " + pocketID.ToString());
                logger.Debug(location + "  language-> " + language);

                cboConditionField.ValueMember = "DOMAIN_TABLE_CODE";
                cboConditionField.SelectedValueChanged += cboConditionField_SelectedValueChanged;

                //For traces   
                logger.Debug(location + "Before line: EndPoint ep = new EndPoint(btecDB);");

                EndPoint ep = new EndPoint(btecDB);
                DataTable endPointTable = ep.GetIdAndDescription();

                cboEndPoint.DataSource = endPointTable;
                cboEndPoint.DisplayMember = "DESCNO";
                cboEndPoint.ValueMember = "END_POINT_ID";

                //To detect a change in the form (next 5 lines)
                cboConditionField.TextChanged += new EventHandler(this.somethingChanged);
                cboLogicOperator.TextChanged += new EventHandler(this.somethingChanged);
                cboEndPoint.TextChanged += new EventHandler(this.somethingChanged);
                txtValue1.TextChanged += new EventHandler(this.somethingChanged);
                txtValue2.TextChanged += new EventHandler(this.somethingChanged);

                EnableControl.TextBox(txtDescription, false);
                EnableControl.TextBox(txtSequence, false);

                txtDescription.Text = parentForm.pocketIDAndDescription;

                switch (action)
                {
                    case ACTION_EDIT:
                        setConditionFields();
                        break;
                    case ACTION_ADD:
                        initializeNewCondition();
                        break;
                    case ACTION_REMOVE:
                        setConditionFields();
                        disableAllControls();
                        break;
                }

                isSomethingChanged = false;

                enablePreviousNextButtons();

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

        //Disables all the form controls
        private void disableAllControls()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Disables all the form controls");
            
            EnableControl.ComboBox(cboConditionField, false);
            EnableControl.ComboBox(cboLogicOperator, false);
            EnableControl.ComboBox(cboEndPoint, false);
            EnableControl.TextBox(txtValue1, false);
            EnableControl.TextBox(txtValue2, false);
            EnableControl.ListBox(listListValues, false);
            EnableControl.TextBox(txtValue, false);

            //For traces
            logger.Debug(location + " Ending...");
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

            DataGridViewRow currentRow = parentForm.dgvConditions.CurrentRow;

            if (currentRow != null && action == ACTION_EDIT)
            {
                //For traces   
                logger.Debug(location + " After line: if (row != null && action == ACTION_EDIT)");

                if (currentRow.Index == 0) { btnPrevious.Enabled = false; }
                else { btnPrevious.Enabled = true; }

                if (currentRow.Index == parentForm.dgvConditions.Rows.Count - 1) { btnNext.Enabled = false; }
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

        //occurs when any field is modified
        private void somethingChanged(object sender, EventArgs e)
        {
            isSomethingChanged = true;
        }

        //Initializes all form fields to create a new condition
        private void initializeNewCondition()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Initializes all form fields to create a new condition"); 

            sequence = getMaxSequence() + 1;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  sequence-> " + sequence.ToString());

            txtSequence.Text = sequence.ToString();

            if (cboConditionField.Items.Count > 0) { cboConditionField.SelectedIndex = 0; }

            //For traces   
            logger.Debug(location + "Before line: setCboLogicOperatorList();");

            setCboLogicOperatorList();

            if (cboEndPoint.Items.Count > 0) { cboEndPoint.SelectedIndex = 0; }

            txtValue1.Text = null;
            txtValue2.Text = null;

            listListValues.Items.Clear();
            txtValue.Text = null;

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Adjusts the list of the cboLogicOperator ComboBox and preserves the displayed value if applicable
        private void setCboLogicOperatorList()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Adjusts the list of the cboLogicOperator ComboBox and preserves the displayed value if applicable");
            
            DataTable cboLogicOperatorTable = LogicOperatorTable.Copy();

            if (isAcceptedForList()) { cboLogicOperatorTable.Rows.Add("LIST", rm.GetString("list")); }

            string textValue = (cboLogicOperator.Text ?? "").ToString();

            cboLogicOperator.DataSource = cboLogicOperatorTable;
            cboLogicOperator.DisplayMember = "DESCRIPTION";
            cboLogicOperator.ValueMember = "OPERATOR";

            //If the end point field is selected
            if (cboConditionField.SelectedValue.ToString() == END_POINT_ID)
            {
                //For traces   
                logger.Debug(location + " After line: if (cboConditionField.SelectedValue.ToString() == END_POINT_ID)");

                cboLogicOperator.SelectedValue = "EQ";
                EnableControl.ComboBox(cboLogicOperator, false);

                cboEndPoint.Visible = true;
                txtValue1.Visible = false;
            }
            else
            {
                //For traces   
                logger.Debug(location + " After line: if (cboConditionField.SelectedValue.ToString() == END_POINT_ID)...else");

                if (action != ACTION_REMOVE) { EnableControl.ComboBox(cboLogicOperator, true); }

                cboEndPoint.Visible = false;
                txtValue1.Visible = true;

                if (cboLogicOperator.FindStringExact(textValue) != -1) { cboLogicOperator.Text = textValue; }
                else 
                {
                    if (cboLogicOperator.Items.Count > 0) { cboLogicOperator.SelectedIndex = 0; }
                }
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Enables or disables the [Remove] button
        private void enableBtnRemove()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Enables or disables the [Remove] button"); 

            if (listListValues.SelectedIndex == -1) { btnRemove.Enabled = false; }
            else { btnRemove.Enabled = true; }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Enables or disables the [Add] button
        private void enableBtnAdd()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Enables or disables the [Add] button");
            
            if (string.IsNullOrEmpty(txtValue.Text.Trim())) { btnAdd.Enabled = false; }
            else { btnAdd.Enabled = true; }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Adjusts the visibility of some controls depending on the cboLogicOperator ComboBox value
        private void setControlsVisible()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Adjusts the visibility of some controls depending on the cboLogicOperator ComboBox value");

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  cboLogicOperator.SelectedValue-> " + cboLogicOperator.SelectedValue.ToString());

            switch (cboLogicOperator.SelectedValue.ToString())
            {
                case "BT":
                    labelValue1.Visible = true; 
                    txtValue1.Visible = true;
                    labelValue2.Visible = true;
                    txtValue2.Visible = true;

                    labelListValues.Visible = false;
                    listListValues.Visible = false;
                    labelValue.Visible = false;
                    txtValue.Visible = false;
                    btnAdd.Visible = false;
                    btnRemove.Visible = false;
                    break;
                case "LIST":
                    labelValue1.Visible = false; 
                    txtValue1.Visible = false;
                    labelValue2.Visible = false;
                    txtValue2.Visible = false;

                    labelListValues.Visible = true;
                    listListValues.Visible = true;
                    labelValue.Visible = true;
                    txtValue.Visible = true;
                    btnAdd.Visible = true;
                    btnRemove.Visible = true;

                    enableBtnAdd();
                    enableBtnRemove();
                    break;
                default:
                    labelValue1.Visible = true;
                    txtValue1.Visible = !cboEndPoint.Visible;
                    labelValue2.Visible = false;
                    txtValue2.Visible = false;

                    labelListValues.Visible = false;
                    listListValues.Visible = false;
                    labelValue.Visible = false;
                    txtValue.Visible = false;
                    btnAdd.Visible = false;
                    btnRemove.Visible = false;
                    break;
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Determines whether the cboConditionField ComboBox value requires adding the <LIST> choice in the cboLogicOperator ComboBox list
        private bool isAcceptedForList()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Determines whether the cboConditionField ComboBox value requires adding the <LIST> choice in the cboLogicOperator ComboBox list");
            
            bool isAccepted = false;

            //For traces   
            logger.Debug(location + "Before line: ApplicationConstant ac = new ApplicationConstant(btecDB);");
            logger.Debug(location + "        and: ApplicationConstantData acData = ac.GetForDomainTableCode(DOMAIN_ID, cboConditionField.SelectedValue.ToString());");

            ApplicationConstant ac = new ApplicationConstant(btecDB);
            ApplicationConstantData acData = ac.GetForDomainTableCode(DOMAIN_ID, cboConditionField.SelectedValue.ToString());

            string vbVariableName = acData.GetStringValue(acData.VB_VARIABLE_NAME);

            isAccepted = (Array.IndexOf(NamesAcceptedForList, vbVariableName) != -1 ? true : false);

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  vbVariableName-> " + vbVariableName);
            logger.Debug(location + "  isAccepted-> " + isAccepted.ToString());

            //For traces
            logger.Debug(location + " Ending...");

            return isAccepted;
        }

        //Assigns the form fields based on the index row of the pockets and conditions data tables
        private void setConditionFields()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Assigns the form fields based on the index row of the pockets and conditions data tables"); 

            int currentIndexCond = parentForm.dgvConditions.CurrentRow.Index;
            string strSequence = (parentForm.dgvConditions[0, currentIndexCond].Value ?? "").ToString();

            if (string.IsNullOrEmpty(strSequence)) { throw new Exception("A value of the SEQUENCE_CONDITION field in the dgvConditions object is empty."); }
            else { sequence = long.Parse(strSequence); }

            txtSequence.Text = strSequence;

            DataRow conditionRow = parentForm.conditionsTable.Select("POCKET_NO = " + pocketID.ToString() +
                                                       " and CONDITION_SEQUENCE = " + strSequence).First();

            string tableName     = conditionRow["TABLE_NAME"].ToString();
            string fieldName     = conditionRow["FIELD_NAME"].ToString();
            string logicOperator = conditionRow["LOGIC_OPERATOR"].ToString();
            string value1        = conditionRow["VALUE1"].ToString();
            string value2        = conditionRow["VALUE2"].ToString();

            //For traces   
            logger.Debug(location + "Before line: ApplicationConstant ac = new ApplicationConstant(btecDB);");

            ApplicationConstant ac = new ApplicationConstant(btecDB);
            string domainTableCode = ac.GetFirstDomainTableCode(tableName, fieldName);

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  sequence-> " + sequence.ToString());
            logger.Debug(location + "  tableName-> " + tableName);
            logger.Debug(location + "  fieldName-> " + fieldName);
            logger.Debug(location + "  logicOperator-> " + logicOperator);
            logger.Debug(location + "  value1-> " + value1);
            logger.Debug(location + "  value2-> " + value2);
            logger.Debug(location + "  domainTableCode-> " + domainTableCode);

            if (!string.IsNullOrEmpty(domainTableCode)) 
            { 
                cboConditionField.SelectedValue = domainTableCode;

                if (domainTableCode.Equals(END_POINT_ID))
                {
                    if (!string.IsNullOrEmpty(value1)) { cboEndPoint.SelectedValue = value1; }
                    else
                    {
                        if (cboEndPoint.Items.Count > 0) { cboEndPoint.SelectedIndex = 0; }
                    }
                }
                else
                {
                    if (cboEndPoint.Items.Count > 0) { cboEndPoint.SelectedIndex = 0; }
                }
            }
            else
            {
                if (cboConditionField.Items.Count > 0) { cboConditionField.SelectedIndex = 0; }
                if (cboEndPoint.Items.Count > 0) { cboEndPoint.SelectedIndex = 0; }
            }

            setCboLogicOperatorList();

            if (!string.IsNullOrEmpty(logicOperator))
            {
                if (!domainTableCode.Equals(END_POINT_ID)) { cboLogicOperator.SelectedValue = logicOperator; }
            }
            else
            {
                if (cboLogicOperator.Items.Count > 0) { cboLogicOperator.SelectedIndex = 0; }
            }

            if (!domainTableCode.Equals(END_POINT_ID)) { txtValue1.Text = value1; }
            else { txtValue1.Text = null; }

            txtValue2.Text = value2;

            listListValues.Items.Clear();

            if (logicOperator.Equals("LIST"))
            {
                foreach (DataRow listValuesRow in parentForm.listValuesTable.Select("POCKET_NO = " + pocketID.ToString() +
                                                                      " and CONDITION_SEQUENCE = " + strSequence))
                {
                    listListValues.Items.Add(listValuesRow["VALUE"]);
                }
            }

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Returns the maximal CONDITION_SEQUENCE value of the conditions data table from the parent form for a specific newPocketID
        private long getMaxSequence()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Returns the maximal CONDITION_SEQUENCE value of the conditions data table from the parent form for a specific newPocketID");
            
            string criteria = "SORT_PATTERN_ID = " + sortPatternID.ToString() +
                               " and POCKET_NO = " + pocketID.ToString();

            long maxSequence = 0;

            if (parentForm.conditionsTable.Select(criteria).Count() > 0)
            {
                maxSequence = (from item in parentForm.conditionsTable.Select(criteria).AsEnumerable()
                               select Convert.ToInt64(item["CONDITION_SEQUENCE"] ?? 0)).Max();
            }
            else { maxSequence = 0; }

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  criteria-> " + criteria);
            logger.Debug(location + "  maxSequence-> " + maxSequence.ToString());

            //For traces
            logger.Debug(location + " Ending...");

            return maxSequence;
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

        //Occurs when the selected value changes in the cboConditionField ComboBox
        private void cboConditionField_SelectedValueChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the selected value changes in the cboConditionField ComboBox");

                //For traces   
                logger.Debug(location + "Before line: setCboLogicOperatorList();");

                setCboLogicOperatorList();

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

        //Occurs when the selected value changes in the cboLogicOperator ComboBox
        private void cboLogicOperator_SelectedValueChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the selected value changes in the cboLogicOperator ComboBox");
                
                setControlsVisible();

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

        //Occurs when the selected index changes in the listListValues ListBox
        private void listListValues_SelectedIndexChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the selected index changes in the listListValues ListBox");
                
                enableBtnRemove();

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
                
                listListValues.Items.RemoveAt(listListValues.SelectedIndex);

                isSomethingChanged = true;

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

        //Occurs when the text changes in the txtValue TextBox
        private void txtValue_TextChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the text changes in the txtValue TextBox");
                
                enableBtnAdd();

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
                
                string buffer = txtValue.Text.Trim();
                string value = buffer.Substring(0, Math.Min(buffer.Length, 100));

                if (listListValues.FindStringExact(value) != -1)
                {
                    //For traces   
                    logger.Debug(location + " After line: if (listListValues.FindStringExact(value) != -1)");

                    //Set the focus to the txtValue TextBox
                    txtValue.Focus();

                    MessageBox.Show(rm.GetString("msgValueInList"),
                                    rm.GetString("TitleMsgValidationError"),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                }
                else
                {
                    //For traces   
                    logger.Debug(location + " After line: if (listListValues.FindStringExact(value) != -1)...else");

                    listListValues.Items.Add(value);
                    listListValues.SelectedIndex = listListValues.FindStringExact(value);
                    txtValue.Text = null;

                    isSomethingChanged = true;
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
        private void frmSortPatternCond_FormClosing(object sender, FormClosingEventArgs e)
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

        //Makes fields validation before saving
        private bool validateFields()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Makes fields validation before saving"); 

            if (listListValues.Visible)
            {
                if (listListValues.Items.Count == 0)
                {
                    //Set the focus to the Value field
                    txtValue.Focus();

                    MessageBox.Show(rm.GetString("msgMustOneValueInList"),
                                    rm.GetString("TitleMsgValidationError"),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    return false;
                }
            }
            else
            {
                if (txtValue1.Visible)
                {
                    if (string.IsNullOrEmpty(txtValue1.Text.Trim()))
                    {
                        //Set the focus to the Value 1 field
                        txtValue1.Focus();

                        MessageBox.Show(rm.GetString("msgMustValue1"),
                                        rm.GetString("TitleMsgValidationError"),
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

                        return false;
                    }

                    if (!cboLogicOperator.SelectedValue.ToString().Equals("EQ") &&
                        (txtValue1.Text.IndexOf("*") != -1 || txtValue1.Text.IndexOf("?") != -1))
                    {
                        //Set the focus to the Value 1 field
                        txtValue1.Focus();

                        MessageBox.Show(rm.GetString("msgWildcardsOnlyWithEqual"),
                                        rm.GetString("TitleMsgValidationError"),
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

                        return false;
                    }
                }

                if (txtValue2.Visible)
                {
                    if (string.IsNullOrEmpty(txtValue2.Text.Trim()))
                    {
                        //Set the focus to the Value 2 field
                        txtValue2.Focus();

                        MessageBox.Show(rm.GetString("msgValue2ForBetween"),
                                        rm.GetString("TitleMsgValidationError"),
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

        //Saves the form data in the conditions data table
        private void saveConditionsTable()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Saves the form data in the conditions data table");
            
            int currentConditionsIndexTable = 0;

            switch (action)
            {
                case ACTION_EDIT:
                    DataRow conditionsRow = parentForm.conditionsTable.Select("POCKET_NO = " + pocketID.ToString() +
                                                                " and CONDITION_SEQUENCE = " + sequence.ToString()).First();

                    currentConditionsIndexTable = parentForm.conditionsTable.Rows.IndexOf(conditionsRow);
                    break;
                case ACTION_ADD:
                    parentForm.conditionsTable.Rows.Add();
                    currentConditionsIndexTable = parentForm.conditionsTable.Rows.Count - 1;
                    break;
            }

            string buffer;

            string conditionField = cboConditionField.SelectedValue.ToString();
            conditionField = string.IsNullOrEmpty(conditionField) ? null : conditionField;

            string logicOperator = cboLogicOperator.SelectedValue.ToString();
            logicOperator = string.IsNullOrEmpty(logicOperator) ? null : logicOperator;

            buffer = txtValue1.Text.Trim();
            string value1txt = buffer.Substring(0, Math.Min(buffer.Length, 100));

            string value1cbo = cboEndPoint.SelectedValue.ToString();
            value1cbo = string.IsNullOrEmpty(value1cbo) ? null : value1cbo;

            buffer = txtValue2.Text.Trim();
            string value2txt = buffer.Substring(0, Math.Min(buffer.Length, 100));

            parentForm.conditionsTable.Rows[currentConditionsIndexTable]["SORT_PATTERN_ID"] = sortPatternID.ToString();
            parentForm.conditionsTable.Rows[currentConditionsIndexTable]["POCKET_NO"] = pocketID.ToString();
            parentForm.conditionsTable.Rows[currentConditionsIndexTable]["CONDITION_SEQUENCE"] = sequence.ToString();

            //For traces   
            logger.Debug(location + "Before line: ApplicationConstant ac = new ApplicationConstant(btecDB);");
            logger.Debug(location + "        and: ApplicationConstantData acData = ac.GetForDomainTableCode(DOMAIN_ID, cboConditionField.SelectedValue.ToString());");

            ApplicationConstant ac = new ApplicationConstant(btecDB);
            ApplicationConstantData acData = ac.GetForDomainTableCode(DOMAIN_ID, cboConditionField.SelectedValue.ToString());

            parentForm.conditionsTable.Rows[currentConditionsIndexTable]["TABLE_NAME"] = acData.GetStringValue(acData.TEXT1);
            parentForm.conditionsTable.Rows[currentConditionsIndexTable]["FIELD_NAME"] = acData.GetStringValue(acData.TEXT2);

            parentForm.conditionsTable.Rows[currentConditionsIndexTable]["LOGIC_OPERATOR"] = logicOperator;

            string value1 = null;

            if (txtValue1.Visible) { value1 = value1txt; }
            else if (cboEndPoint.Visible) { value1 = value1cbo; }

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  action-> " + action);
            logger.Debug(location + "  conditionField-> " + conditionField);
            logger.Debug(location + "  logicOperator-> " + logicOperator);
            logger.Debug(location + "  value1-> " + value1);
            logger.Debug(location + "  value2txt-> " + value2txt);

            parentForm.conditionsTable.Rows[currentConditionsIndexTable]["VALUE1"] = value1;
            parentForm.conditionsTable.Rows[currentConditionsIndexTable]["VALUE2"] = txtValue2.Visible ? value2txt : null;

            parentForm.isSomethingChanged = true;

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Saves the listListValues ListBox data in the list of values data table
        private void saveListValuesTable()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Saves the listListValues ListBox data in the list of values data table");
            
            var rows = parentForm.listValuesTable.Select("POCKET_NO = " + pocketID.ToString() +
                                           " and CONDITION_SEQUENCE = " + sequence.ToString());
            foreach (var row in rows)
            {
                parentForm.listValuesTable.Rows.Remove(row);
            }

            DataRow newRow;

            //For traces   
            logger.Debug(location + "Before line: foreach (string value in listListValues.Items)");

            foreach (string value in listListValues.Items)
            {
                newRow = parentForm.listValuesTable.NewRow();

                newRow["SORT_PATTERN_ID"] = sortPatternID.ToString();
                newRow["POCKET_NO"] = pocketID.ToString();
                newRow["CONDITION_SEQUENCE"] = sequence.ToString();
                newRow["VALUE"] = value;

                parentForm.listValuesTable.Rows.Add(newRow);
            }

            //For traces
            logger.Debug(location + " Ending...");
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

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  result-> " + result.ToString());
                logger.Debug(location + "  action-> " + action);

                switch (result)
                {
                    case DialogResult.Yes:
                        int currentConditionsIndex;

                        switch (action)
                        {
                            case ACTION_EDIT:
                                saveConditionsTable();
                                saveListValuesTable();

                                currentConditionsIndex = parentForm.dgvConditions.CurrentRow.Index;

                                parentForm.refreshDgvConditions();

                                //Selects the current row after the refresh
                                parentForm.dgvConditions.CurrentCell = parentForm.dgvConditions[0, currentConditionsIndex];

                                isFormClosedByUser = true;

                                //For the parent form knows that there was no refresh to do
                                this.DialogResult = DialogResult.Cancel;
                                this.Close();
                                break;
                            case ACTION_ADD:
                                saveConditionsTable();
                                saveListValuesTable();

                                parentForm.refreshDgvConditions();
                                parentForm.setConditionExpression();

                                currentConditionsIndex = parentForm.dgvConditions.Rows.Count - 1;

                                //Selects the new row after the refresh
                                parentForm.dgvConditions.CurrentCell = parentForm.dgvConditions[0, currentConditionsIndex];

                                parentForm.enableConditionsEditAddRemoveButtons();

                                initializeNewCondition();

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
                                initializeNewCondition();

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
                        if (validateFields())
                        {
                            askForSave();
                        }
                        break;
                    case ACTION_ADD:
                        if (validateFields())
                        {
                            //To force the message box to accept to save our changes 
                            isSomethingChanged = true;

                            askForSave();
                        }
                        break;
                    case ACTION_REMOVE:
                        //displays an hourglass cursor
                        Cursor.Current = Cursors.WaitCursor;

                        string conditionField = cboConditionField.Text;
                        string logicOperator = cboLogicOperator.Text;
                        string value1 = cboEndPoint.Visible ? cboEndPoint.Text : txtValue1.Text;
                        string value2 = txtValue2.Text; 

                        string message = "";
                        string condition = "";

                        message += rm.GetString("msg1DeleteCondition") + Math.Abs(sequence).ToString() + ":";
                        message += "\n" + "\n";

                        condition += String.IsNullOrEmpty(conditionField) ? "" : conditionField + "\n";
                        condition += String.IsNullOrEmpty(logicOperator)  ? "" : logicOperator + "\n";
                        condition += String.IsNullOrEmpty(value1)         ? "" : value1 + "\n";
                        condition += String.IsNullOrEmpty(value2)         ? "" : value2 + "\n";

                        message += String.IsNullOrEmpty(condition) ? "" : condition + "\n";
                        message += rm.GetString("msg2DeleteCondition");

                        DialogResult result = MessageBox.Show(message,
                                                              rm.GetString("removeCondition"),
                                                              MessageBoxButtons.OKCancel,
                                                              MessageBoxIcon.Exclamation);

                        //For traces
                        logger.Debug(location + "Values:");
                        logger.Debug(location + "  result-> " + result.ToString());

                        switch (result)
                        {
                            case DialogResult.OK:
                                string criteria = "POCKET_NO = " + pocketID.ToString() +
                                    " and CONDITION_SEQUENCE = " + sequence.ToString();

                                logger.Debug(location + "  criteria-> " + criteria);

                                //For traces   
                                logger.Debug(location + "Before line: var rowsToRemove = parentForm.listValuesTable.Select(criteria)");

                                //Selects the rows to be removed in the list of values data table 
                                var rowsToRemove = parentForm.listValuesTable.Select(criteria);

                                //Removes the concerned rows in the list of values data table 
                                foreach (var row in rowsToRemove)
                                {
                                    parentForm.listValuesTable.Rows.Remove(row);
                                }

                                //For traces   
                                logger.Debug(location + "Before line: rowsToRemove = parentForm.conditionsTable.Select(criteria);");

                                //Selects the rows to be removed in the conditions data table 
                                rowsToRemove = parentForm.conditionsTable.Select(criteria);

                                //Removes the concerned rows in the conditions data table 
                                foreach (var row in rowsToRemove) 
                                {
                                    parentForm.conditionsTable.Rows.Remove(row);
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
                        DialogResult result = MessageBox.Show(rm.GetString("msgSaveChanges2"),
                                                              rm.GetString("titleSaveChanges2"),
                                                              MessageBoxButtons.YesNoCancel,
                                                              MessageBoxIcon.None);

                        //For traces
                        logger.Debug(location + "Values:");
                        logger.Debug(location + "  result-> " + result.ToString());

                        switch (result)
                        {
                            case DialogResult.Yes:
                                moveWithSave(-1);
                                break;
                            case DialogResult.No:
                                moveWithoutSave(-1);
                                break;
                            case DialogResult.Cancel:
                                break;
                        }
                    }
                }
                else { moveWithoutSave(-1); }

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

                        //For traces
                        logger.Debug(location + "Values:");
                        logger.Debug(location + "  result-> " + result.ToString());

                        switch (result)
                        {
                            case DialogResult.Yes:
                                moveWithSave(+1);
                                break;
                            case DialogResult.No:
                                moveWithoutSave(+1);
                                break;
                            case DialogResult.Cancel:
                                break;
                        }
                    }
                }
                else { moveWithoutSave(+1); }

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

            //For traces   
            logger.Debug(location + "Before line: saveConditionsTable();");

            saveConditionsTable();

            //For traces   
            logger.Debug(location + "Before line: saveListValuesTable();");
            
            saveListValuesTable();

            int currentIndex = parentForm.dgvConditions.CurrentRow.Index;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  currentIndex-> " + currentIndex.ToString());

            currentIndex += increment;

            parentForm.refreshDgvConditions();
            parentForm.dgvConditions.CurrentCell = parentForm.dgvConditions[0, currentIndex];

            //For traces   
            logger.Debug(location + "Before line: setConditionFields();");

            setConditionFields();

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

            int currentIndex = parentForm.dgvConditions.CurrentRow.Index;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  currentIndex-> " + currentIndex.ToString());

            currentIndex += increment;

            parentForm.dgvConditions.CurrentCell = parentForm.dgvConditions[0, currentIndex];

            //For traces   
            logger.Debug(location + "Before line: setConditionFields();");

            setConditionFields();

            enablePreviousNextButtons();

            isSomethingChanged = false;

            //For traces
            logger.Debug(location + " Ending...");
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
