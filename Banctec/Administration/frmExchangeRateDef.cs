using System;
using System.Data;
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
    public partial class frmExchangeRateDef : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmExchangeRateDef).Name;        

        ResourceManager rm;
        ILoggerBtec logger;
        IBtecDB btecDB;
        DataTable RatesTable;
        DateTime effectiveDate = DateTime.MinValue;

        public frmExchangeRateDef(IBtecDB btecDBParent)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmExchangeRateDef));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmExchangeRateDef));
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
        
        //Occurs when the [New Rate] button is clicked
        private void btnNewRate_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [New Rate] button is clicked");

                string strCode1;
                string strCode2;
 
                DataGridViewRow currentRow = dgvRate.CurrentRow;

                if (currentRow != null)
                {
                    //For traces   
                    logger.Debug(location + " After line: if (currentRow != null)");
 
                    strCode1 = (RatesTable.Rows[currentRow.Index]["CURRENCY_CODE1"] ?? "").ToString();
                    strCode2 = (RatesTable.Rows[currentRow.Index]["CURRENCY_CODE2"] ?? "").ToString();
                }
                else
                {
                    strCode1 = null;
                    strCode2 = null;
                }

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  strCode1-> " + (strCode1 == null ? "NULL" : strCode1));
                logger.Debug(location + "  strCode2-> " + (strCode2 == null ? "NULL" : strCode2));

                //For traces   
                logger.Debug(location + "Before line: frmExchangeRateEdit _frm = new frmExchangeRateEdit(btecDB, DateTime.MinValue, strCode1, strCode2);");

                frmExchangeRateEdit _frm = new frmExchangeRateEdit(btecDB, DateTime.MinValue, strCode1, strCode2);

                DialogResult result = _frm.ShowDialog(this);
                _frm.Dispose();

                if (result == DialogResult.OK)
                {
                    //For traces   
                    logger.Debug(location + "Before line: refreshDgvRate();");

                    refreshDgvRate();
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

        //Initializes some components at the opening of the form
        private void frmViewExchangeRate_Load(object sender, EventArgs e)
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

                //For traces   
                logger.Debug(location + "Before line: GetExecutionDate ged = new GetExecutionDate(btecDB);");
                logger.Debug(location + "        and: GetExecutionDateData gedData = ged.Get();");

                GetExecutionDate ged = new GetExecutionDate(btecDB);
                GetExecutionDateData gedData = ged.Get();

                string strEffDate = gedData.GetStringValue(gedData.EXEC_DATE);

                if (string.IsNullOrEmpty(strEffDate)) { throw new Exception("The field EXEC_DATE on view GET_EXECUTION_DATE is empty."); }
                else
                {
                    //Convert a string, representing a date in a specific format, to a DateTime type
                    effectiveDate = DateTime.ParseExact(strEffDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                }

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  gedData.GetStringValue(gedData.EXEC_DATE)-> " + effectiveDate.ToString("yyyyMMdd"));

                string[] columnHeaderText = {rm.GetString("ColCurrencyType1"),
                                             rm.GetString("ColCurrencyType2"),
                                             rm.GetString("ColEffectiveDate"),
                                             rm.GetString("ColMinimalValue"),
                                             rm.GetString("ColMaximalValue"),
                                             rm.GetString("ColConversionRate1To2"), 
                                             rm.GetString("ColConversionRate2To1")};

                //Specifies the number of desired columns for the datagridview object
                dgvRate.ColumnCount = 7;

                //Specifies the columns width to fit with the datagridview object width
                double columnWidth = (dgvRate.Width) / dgvRate.ColumnCount;

                for (int i = 0; i < dgvRate.ColumnCount; i++)
                {
                    dgvRate.Columns[i].HeaderText = columnHeaderText[i];
                    dgvRate.Columns[i].Width = (int)Math.Floor(columnWidth);
                    dgvRate.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //specifies that we can not sort this column
                    dgvRate.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                for (int i = 0; i <= 1; i++)
                {
                    dgvRate.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvRate.Columns[i].ValueType = typeof(string);
                }

                dgvRate.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvRate.Columns[2].ValueType = typeof(DateTime);

                for (int i = 3; i < dgvRate.ColumnCount; i++)
                {
                    dgvRate.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvRate.Columns[i].ValueType = typeof(double);
                }

                radioCurrentRate.Checked = true;

                //Add the CheckedChanged event for all radio buttons in the grpBoxDisplayOptions group
                foreach (Control control in grpBoxDisplayOptions.Controls)
                {
                    RadioButton radioButton = control as RadioButton;

                    if (radioButton != null)
                    {
                        radioButton.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
                    }
                }

                //For traces   
                logger.Debug(location + "Before line: refreshDgvRate();");

                refreshDgvRate();

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

        //Refreshes the dgvRate object with the most recent data
        private void refreshDgvRate()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            string currencyCode1;
            string currencyCode2;
            string descriptionCode1;
            string descriptionCode2;
            string strCode1;
            string strCode2;
            DateTime effDate = DateTime.MinValue;
            double minimalValue = 0;
            double maximalValue = 0;
            double code1code2Rate = 0;
            double code2code1Rate = 0;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Refreshes the dgvRate object with the most recent data");

            //displays an hourglass cursor
            Cursor.Current = Cursors.WaitCursor;

            //For traces   
            logger.Debug(location + "Before line: dgvRate.Rows.Clear();");

            //Erases all data from the dgvRate object
            dgvRate.Rows.Clear();
            RatesTable = null;

            //For traces   
            logger.Debug(location + "Before line: ExchangeRates er = new ExchangeRates(btecDB);");
            logger.Debug(location + "        and: CurrencyType ct = new CurrencyType(btecDB);");

            ExchangeRates er = new ExchangeRates(btecDB);
            CurrencyType ct = new CurrencyType(btecDB);

            //For traces   
            logger.Debug(location + "Before line: switch (getSelectedRadioButton())");

            //Fill a DataTable with records according to the selected radio button
            switch (getSelectedRadioButton())
            {
                case "radioCurrentRate":
                    //For traces   
                    logger.Debug(location + "Before line: RatesTable = er.GetExchangeRates(true, effectiveDate.ToString(\"yyyyMMdd\"), null, null);");

                    RatesTable = er.GetExchangeRates(true, effectiveDate.ToString("yyyyMMdd"), null, null);
                    break;
                case "radioHistoricalRate":
                    //For traces   
                    logger.Debug(location + "Before line: RatesTable = er.GetExchangeRates(false, null, null, null);");

                    RatesTable = er.GetExchangeRates(false, null, null, null);
                    break;
            }

            int rowCount = RatesTable.Rows.Count;

            if (rowCount == 0)
            {
                if (grpBoxUser.Visible == true) grpBoxUser.Visible = false;
            }
            else
            {
                //For traces   
                logger.Debug(location + " After line: if (rowCount == 0)...else");

                if (grpBoxUser.Visible == false) grpBoxUser.Visible = true;

                DataTable dt = ct.getCurrencyTypeAndDescription();

                //For traces   
                logger.Debug(location + "Before line: for (int value = 0; value < rowCount; value++)");

                string strEffDate;
                string strMinimalValue;
                string strMaximalValue;
                string strCode1code2Rate;
                string strCode2code1Rate;

                //Fill the dgvRate object with values ​​of the RatesTable object with few treatments
                for (int i = 0; i < rowCount; i++)
                {
                    currencyCode1 = (RatesTable.Rows[i]["CURRENCY_CODE1"] ?? "").ToString();
                    currencyCode2 = (RatesTable.Rows[i]["CURRENCY_CODE2"] ?? "").ToString();
                    descriptionCode1 = getDescription(dt, currencyCode1);
                    descriptionCode2 = getDescription(dt, currencyCode2);
                    strCode1 = currencyCode1 + " - " + descriptionCode1;
                    strCode2 = currencyCode2 + " - " + descriptionCode2;

                    strEffDate = (RatesTable.Rows[i]["EFFECTIVE_DATE"] ?? "").ToString();
                    if (string.IsNullOrEmpty(strEffDate)) { throw new Exception("The field EFFECTIVE_DATE on table EXCHANGE_RATE_DTL is empty."); }
                    else
                    {
                        //Convert a string, representing a date in a specific format, to a DateTime type
                        effDate = DateTime.ParseExact(strEffDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    }

                    strMinimalValue = (RatesTable.Rows[i]["LOW_VALUE"] ?? "").ToString();
                    if (string.IsNullOrEmpty(strMinimalValue)) { throw new Exception("The field LOW_VALUE on table EXCHANGE_RATE_DTL is empty."); }
                    else { minimalValue = double.Parse(strMinimalValue); }

                    strMaximalValue = (RatesTable.Rows[i]["HIGH_VALUE"] ?? "").ToString();
                    if (string.IsNullOrEmpty(strMaximalValue)) { throw new Exception("The field HIGH_VALUE on table EXCHANGE_RATE_DTL is empty."); }
                    else { maximalValue = double.Parse(strMaximalValue); }

                    strCode1code2Rate = (RatesTable.Rows[i]["CODE1_CODE2_RATE"] ?? "").ToString();
                    if (string.IsNullOrEmpty(strCode1code2Rate)) { throw new Exception("The field CODE1_CODE2_RATE on table EXCHANGE_RATE_DTL is empty."); }
                    else { code1code2Rate = double.Parse(strCode1code2Rate); }

                    strCode2code1Rate = (RatesTable.Rows[i]["CODE2_CODE1_RATE"] ?? "").ToString();
                    if (string.IsNullOrEmpty(strCode2code1Rate)) { throw new Exception("The field CODE2_CODE1_RATE on table EXCHANGE_RATE_DTL is empty."); }
                    else { code2code1Rate = double.Parse(strCode2code1Rate); }

                    //Adds a row of records in the dgvRate object
                    dgvRate.Rows.Add(strCode1,
                                     strCode2,
                                     effDate.ToString("yyyy-MM-dd"),
                                     minimalValue.ToString("N2"),
                                     maximalValue.ToString("N2"),
                                     code1code2Rate.ToString("N5"),
                                     code2code1Rate.ToString("N5"));
                }
            }

            //displays the default cursor
            Cursor.Current = Cursors.Default;

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Returns the description corresponding to the currency code
        private string getDescription (DataTable dt, string currencyCode)
        {
            DataRow row = dt.Select(string.Format("{0} = '{1}'", "CURRENCY_CODE", currencyCode)).First();
            
            string description = (row["DESCRIPTION"] ?? "").ToString();

            return description;
        }

        //Allows to know which radio button is currently selected
        private string getSelectedRadioButton()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Allows to know which radio button is currently selected");

            var checkedButton = grpBoxDisplayOptions.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  checkedButton.Name-> " + checkedButton.Name.ToString());

            //For traces
            logger.Debug(location + " Ending...");

            return checkedButton.Name.ToString();
        }

        //Occurs when the checked radio button is changed
        void radioButton_CheckedChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the checked radio button is changed");

                RadioButton radioButton = sender as RadioButton;
            
                if (radioButton != null)
                {
                    if (radioButton.Checked) 
                    {
                        //For traces   
                        logger.Debug(location + "Before line: refreshDgvRate();");

                        refreshDgvRate(); 
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

        //Occurs when the [Refresh] button is clicked
        private void btnRefresh_Click(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the [Refresh] button is clicked");

                //For traces   
                logger.Debug(location + "Before line: refreshDgvRate();");

                refreshDgvRate();

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

        //Occurs when a line in the dgvRate object is double-clicked
        private void dgvRate_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                logger.Info(location + "Purpose: Occurs when a line in the dgvRate object is double-clicked");

                DataGridViewRow currentRow = (sender as DataGridView).CurrentRow;

                if (currentRow != null)
                {
                    //For traces   
                    logger.Debug(location + " After line: if (currentRow != null)");

                    string strCode1;
                    string strCode2;
                    DateTime effDate;

                    strCode1 = (RatesTable.Rows[currentRow.Index]["CURRENCY_CODE1"] ?? "").ToString();
                    strCode2 = (RatesTable.Rows[currentRow.Index]["CURRENCY_CODE2"] ?? "").ToString();

                    string strEffDate = (RatesTable.Rows[currentRow.Index]["EFFECTIVE_DATE"] ?? "").ToString();

                    if (string.IsNullOrEmpty(strEffDate)) { throw new Exception("The field EFFECTIVE_DATE on table EXCHANGE_RATE_DTL is empty."); }
                    else
                    {
                        //Convert a string, representing a date in a specific format, to a DateTime type
                        effDate = DateTime.ParseExact(strEffDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    }

                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  strCode1-> " + strCode1);
                    logger.Debug(location + "  strCode2-> " + strCode2);
                    logger.Debug(location + "  effDate-> " + effDate.ToString("yyyyMMdd"));

                    //For traces   
                    logger.Debug(location + "Before line: frmUpdateExchangeRate _frm = new frmUpdateExchangeRate(btecDB, effDate, strCode1, strCode2);");

                    //Opens the child form containing informations of the double-clicked line (the next 2 lines)
                    frmExchangeRateEdit _frm = new frmExchangeRateEdit(btecDB, effDate, strCode1, strCode2);

                    DialogResult result = _frm.ShowDialog(this);
                    _frm.Dispose();

                    if (result == DialogResult.OK) 
                    {
                        //For traces   
                        logger.Debug(location + "Before line: refreshDgvRate();");

                        refreshDgvRate(); 
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

        //Occurs when a cell of the dgvRate object is selected
        private void dgvRate_CurrentCellChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when a cell of the dgvRate object is selected");

                DataGridViewRow currentRow = (sender as DataGridView).CurrentRow;

                if (currentRow != null)
                    {
                    //For traces   
                    logger.Debug(location + "Before line: labelUser.Text = (RatesTable.Rows[row.Index][\"USER_ID\"] ?? \"\").ToString();");

                    labelUser.Text = (RatesTable.Rows[currentRow.Index]["USER_ID"] ?? "").ToString();
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