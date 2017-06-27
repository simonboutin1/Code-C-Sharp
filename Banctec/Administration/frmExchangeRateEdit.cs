//2012-06 Simon Boutin MANTIS# 16396 : Convert LBTables from VB to C#

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using BancTec.PCR2P.Core.BusinessLogic.Administration;
using BancTec.PCR2P.Core.DatabaseModel;
using BancTec.PCR2P.Core.DatabaseModel.Administration;
using NetCommunTools;

namespace Administration
{
    public partial class frmExchangeRateEdit : Form
    {
        const float CENT = 0.01f;

        IBtecDB btecDB;
        ResourceManager rm;
        Control myCell;
        ILoggerBtec logger = null;

        //get the class name for traces
        string CLASS_NAME = typeof(frmExchangeRateEdit).Name;        

        bool isDgvRateModified;
        bool isFormClosedByUser;
        bool isSubscribedTextChanged;
        bool isSubscribedCellValidating;
        bool isSubscribedCellValueChanged;
        bool isDgvRateSaved;

        string titleForm;
        string msgCancel;
        string msgSave;

        DateTime effectiveDate;
        string currencyCode1;
        string currencyCode2;

        private struct recordLine
        {
            public double lowValue;
            public double highValue;
            public double code1code2Rate;
            public double code2code1Rate;
        }

        public frmExchangeRateEdit(IBtecDB btecDBParent, DateTime effDate, string strCode1, string strCode2)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." + 
                              MethodBase.GetCurrentMethod().Name + 
                              LogManagerBtec.TraceSeparator;

            try
            {
                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmExchangeRateEdit));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmExchangeRateEdit));
                }

                //For traces
                logger.Debug(location + " Starting...");

                //We must get the resource manager after setting the culture.
                rm = SingletonResourcesManager.Instance.getResourceManager();

                //For traces   
                logger.Debug(location + "Before line: InitializeComponent();");

                //Initializes all the components of the form
                InitializeComponent();

                effectiveDate = effDate;
                currencyCode1 = strCode1;
                currencyCode2 = strCode2;

                //For traces
                logger.Debug(location + "Before line: btecDB = btecDBParent;");

                //Gets information about the parent connection to the Database, including the UserID
                btecDB = btecDBParent;

                titleForm = rm.GetString("TitleFormNewExchangeRates");
                msgCancel = rm.GetString("MsgQuitWithoutSaving");
                msgSave = string.Format(rm.GetString("MsgSaveChanges"), "\n\n");

                //The events was created in the InitializeComponent() method (the next 2 lines)
                isSubscribedCellValidating = true;
                isSubscribedCellValueChanged = true;

                isDgvRateModified = false;
                isFormClosedByUser = false;
                isSubscribedTextChanged = false;
                isDgvRateSaved = false;

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

        //Tries to edit each cell of the dgvRate object to cause a validation error if any
        private bool isEachCellValid()
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

             //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Tries to edit each cell of the dgvRate object to cause a validation error if any");

            try
            {
                //displays an hourglass cursor
                Cursor.Current = Cursors.WaitCursor;

                int nbRow = dgvRate.Rows.Count;
                int NbCol = dgvRate.Columns.Count;

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  nbRow-> " + nbRow.ToString());
                logger.Debug(location + "  NbCol-> " + NbCol.ToString());

                //For traces
                logger.Debug(location + "Before line: for (int row = 0; row < nbRow; row++)");
                logger.Debug(location + "        and: for (int col = 1; col < NbCol; col++)");

                for (int row = 0; row < nbRow; row++)
                {
                    //col = 1 because we don't want to edit the Minimal Value column
                    for (int col = 1; col < NbCol; col++)
                    {
                        dgvRate.CurrentCell = dgvRate[col, row];
                        dgvRate.BeginEdit(true);
                    }
                }

                //Back to the first cell to ensure that the last cell will be validate (the next 3 lines)
                dgvRate.CurrentCell = dgvRate[1, 0];
                dgvRate.BeginEdit(true);
                dgvRate.EndEdit();

                //For traces
                logger.Debug(location + " Ending...");

                //displays the default cursor
                Cursor.Current = Cursors.Default;

                return true;
            }
            catch (Exception ex) 
            {
                //For traces
                logger.Error(location + ex.Message);

                //Informs the user that an error occurs
                MessageBox.Show(rm.GetString("GenericErrorOccured") + " : " + ex.Message, rm.GetString("GenericErrorOccuredCaption"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false; 
            }
        }

        //Occurs if the user clicks the [OK] button
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
                logger.Info(location + "Purpose: Occurs if the user clicks the [OK] button");

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  isDgvRateModified-> " + isDgvRateModified.ToString());

                if (isDgvRateModified)
                {
                    DialogResult selectButton = MessageBox.Show(msgSave, titleForm, MessageBoxButtons.OKCancel, MessageBoxIcon.None);

                    //For traces
                    logger.Debug(location + "  result-> " + selectButton.ToString());

                    switch (selectButton)
                    {
                        case DialogResult.OK:
                            if (isEachCellValid())
                            {
                                //For traces
                                logger.Debug(location + "Before line: saveExchangeRates();");

                                saveExchangeRates();

                                isFormClosedByUser = true;
                                //Closes the form
                                this.Close();
                            }

                            break;
                        case DialogResult.Cancel:
                            break;
                    }
                }
                else
                {
                    isFormClosedByUser = true;
                    //Closes the form
                    this.Close();
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

        //Saves the data of the dgvRate object in the database
        private void saveExchangeRates()
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
                logger.Info(location + "Purpose: Saves the data of the dgvRate object in the database");

                recordLine ratesLine;
                List <recordLine> ratesList = new List<recordLine>();

                int lowValueCol = 0;
                int highValueCol = 1;
                int code12Col;
                int code21Col;

                string currencyCode1TableField;
                string currencyCode2TableField;

                //Determines if we must switch the columns of exchange rates
                if (string.Compare(currencyCode1, currencyCode2) < 0)
                {
                    code12Col = 2;
                    code21Col = 3;

                    currencyCode1TableField = currencyCode1;
                    currencyCode2TableField = currencyCode2;
                }
                else
                {
                    code12Col = 3;
                    code21Col = 2;

                    currencyCode1TableField = currencyCode2;
                    currencyCode2TableField = currencyCode1;
                }

                //Avoids having an error when trying to add an uninitialized value in the list
                ratesLine.lowValue = 0;

                //For traces
                logger.Debug(location + "Before line: foreach (DataGridViewRow row in dgvRate.Rows)");

                string strlowValue;
                string strhighValue;
                string strCode1code2Rate;
                string strCode2code1Rate;

                //Accumulates all rows in a list with some treatments
                foreach (DataGridViewRow row in dgvRate.Rows)
                {
                    if (row.Cells[lowValueCol].Value != null)
                    {
                        strlowValue = (row.Cells[lowValueCol].Value ?? "").ToString();
                        if (string.IsNullOrEmpty(strlowValue)) { throw new Exception("A cell in the Minimal Value column of the dgvRate object is empty."); }
                        else { ratesLine.lowValue = double.Parse(strlowValue); }
                    }

                    if (row.Cells[highValueCol].Value != null)
                    {
                        strhighValue = (row.Cells[highValueCol].Value ?? "").ToString();
                        if (string.IsNullOrEmpty(strhighValue)) { throw new Exception("A cell in the Maximal Value column of the dgvRate object is empty."); }
                        else { ratesLine.highValue = double.Parse(strhighValue); }

                        strCode1code2Rate = (row.Cells[code12Col].Value ?? "").ToString();
                        if (string.IsNullOrEmpty(strCode1code2Rate)) { throw new Exception("A cell in the Rate #1 to #2 column of the dgvRate object is empty."); }
                        else { ratesLine.code1code2Rate = double.Parse(strCode1code2Rate); }

                        strCode2code1Rate = (row.Cells[code21Col].Value ?? "").ToString();
                        if (string.IsNullOrEmpty(strCode2code1Rate)) { throw new Exception("A cell in the Rate #2 to #1 column of the dgvRate object is empty."); }
                        else { ratesLine.code2code1Rate = double.Parse(strCode2code1Rate); }
                        
                        //For traces
                        logger.Debug(location + "Before line: ratesList.Add(ratesLine);");
                    
                        //Adds the line in the list
                        ratesList.Add(ratesLine);
                    }
                    else { continue; }
                }

                ExchangeRates er = new ExchangeRates(btecDB);

                //For traces
                logger.Debug(location + "Before line: er.DeleteExchangeRatesDtl(...);");

                //Deletes the children first
                er.DeleteExchangeRatesDtl(effectiveDate.ToString("yyyyMMdd"), 
                                          currencyCode1TableField, 
                                          currencyCode2TableField);

                //For traces
                logger.Debug(location + "Before line: er.DeleteExchangeRatesHdr(...);");

                //Deletes the parent next
                er.DeleteExchangeRatesHdr(effectiveDate.ToString("yyyyMMdd"),
                                          currencyCode1TableField,
                                          currencyCode2TableField);

                //For traces
                logger.Debug(location + "Before line: er.InsertExchangeRatesHdr(...);");
            
                //Inserts a record for the parent
                er.InsertExchangeRatesHdr(effectiveDate.ToString("yyyyMMdd"), 
                                          currencyCode1TableField, 
                                          currencyCode2TableField, 
                                          btecDB.ConnProvider.UserId.ToString());

                //For traces
                logger.Debug(location + "Before line: foreach (recordLine line in ratesList)");

                //Inserts all records for the child
                foreach (recordLine line in ratesList)
                {
                    //For traces
                    logger.Debug(location + "Before line: er.InsertExchangeRatesDtl(...);");

                    er.InsertExchangeRatesDtl(effectiveDate.ToString("yyyyMMdd"),
                                              currencyCode1TableField,
                                              currencyCode2TableField,
                                              line.lowValue.ToString("0.00"),
                                              line.highValue.ToString("0.00"),
                                              line.code1code2Rate.ToString("0.00000"),
                                              line.code2code1Rate.ToString("0.00000"));
                }

                isDgvRateSaved = true;

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

        //Occurs if the user clicks the [Cancel] button
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
                logger.Info(location + "Purpose: Occurs if the user clicks the [Cancel] button");

                //Closes the form
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

        //Occurs when the form is closed
        private void frmUpdateExchangeRate_FormClosing(object sender, FormClosingEventArgs e)
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
                logger.Debug(location + "  isDgvRateModified-> " + isDgvRateModified.ToString());

                if (isDgvRateModified)
                {
                    //For traces
                    logger.Debug(location + "  isFormClosedByUser-> " + isFormClosedByUser.ToString());

                    //If the form does not ever be closed by the user
                    if (!isFormClosedByUser)
                    {
                        //For traces   
                        logger.Debug(location + " After line: if (!isFormClosedByUser)");

                        DialogResult selectButton = MessageBox.Show(msgCancel, titleForm, MessageBoxButtons.YesNo, MessageBoxIcon.None);

                        //For traces
                        logger.Debug(location + "Values:");
                        logger.Debug(location + "  result-> " + selectButton.ToString());

                        switch (selectButton)
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

                if (isDgvRateSaved == true)
                {
                    //For the parent form knows that there was a change in the child data
                    this.DialogResult = DialogResult.OK;
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
        private void frmUpdateExchangeRate_Load(object sender, EventArgs e)
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

                string[] columnHeaderText = {rm.GetString("ColMinimalValue"), rm.GetString("ColMaximalValue"),
                                             rm.GetString("ColRate1To2"),     rm.GetString("ColRate2To1")};

                GetExecutionDate ged = new GetExecutionDate(btecDB);
                GetExecutionDateData gedData = ged.Get();
                CurrencyType ct = new CurrencyType(btecDB);

                dtpEffectiveDate.ValueChanged -= this.dtpEffectiveDate_ValueChanged;

                //Used to force the display of a specific date format in a DateTimePicker (next 2 lines)
                dtpEffectiveDate.Format = DateTimePickerFormat.Custom;
                dtpEffectiveDate.CustomFormat = "dd MMMM yyyy";

                if (effectiveDate == DateTime.MinValue)
                {
                    string strEffDate = gedData.GetStringValue(gedData.EXEC_DATE);

                    if (string.IsNullOrEmpty(strEffDate)) { throw new Exception("The field EXEC_DATE on view GET_EXECUTION_DATE is empty."); }
                    else
                    {
                        //Convert a string, representing a date in a specific format, to a DateTime type
                        dtpEffectiveDate.Value = DateTime.ParseExact(strEffDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                else { dtpEffectiveDate.Value = effectiveDate; }

                dtpEffectiveDate.ValueChanged += this.dtpEffectiveDate_ValueChanged;

                cboCurrencyType1.SelectedValueChanged -= this.cboCurrencyType1_SelectedValueChanged; 
                cboCurrencyType1.DataSource = ct.GetCurrencyType();
                cboCurrencyType1.DisplayMember = "DESCRIPTION";
                cboCurrencyType1.ValueMember = "CURRENCY_CODE";

                if (currencyCode1 == null)
                {
                    //Clears the displayed text if possible
                    cboCurrencyType1.ResetText(); 
                }
                else 
                {
                    //Ensures that the currency code exists in the list to avoid an error
                    try
                    {
                        cboCurrencyType1.SelectedValue = currencyCode1;
                    }
                    catch (Exception ex)
                    {
                        //Clears the displayed text if possible
                        cboCurrencyType1.ResetText();
                    }
                }

                cboCurrencyType1.SelectedValueChanged += this.cboCurrencyType1_SelectedValueChanged;

                cboCurrencyType2.SelectedValueChanged -= this.cboCurrencyType2_SelectedValueChanged;
                cboCurrencyType2.DataSource = ct.GetCurrencyType();
                cboCurrencyType2.DisplayMember = "DESCRIPTION";
                cboCurrencyType2.ValueMember = "CURRENCY_CODE";

                if (currencyCode2 == null)
                {
                    //Clears the displayed text if possible
                    cboCurrencyType2.ResetText();
                }
                else
                {
                    //Ensures that the currency code exists in the list to avoid an error
                    try
                    {
                        cboCurrencyType2.SelectedValue = currencyCode2;
                    }
                    catch (Exception ex)
                    {
                        //Clears the displayed text if possible
                        cboCurrencyType2.ResetText();
                    }
                }

                cboCurrencyType2.SelectedValueChanged += this.cboCurrencyType2_SelectedValueChanged;

                //Allows to edit a cell as soon as it is selected
                dgvRate.EditMode = DataGridViewEditMode.EditOnEnter;

                //Specifies the number of desired columns for the datagridview object
                dgvRate.ColumnCount = 4;

                //Specifies the columns width to fit with the datagridview object width
                double columnWidth = (dgvRate.Width) / dgvRate.ColumnCount;

                //Removes the CellValueChanged event since the HeaderText assignation appears to trigger this event
                Subscribe_CellValueChanged(false);

                //For traces
                logger.Debug(location + "Before line: for (int value = 0; value < dgvRate.ColumnCount; value++)");

                for (int i = 0; i < dgvRate.ColumnCount; i++)
                {
                    dgvRate.Columns[i].HeaderText = columnHeaderText[i];
                    dgvRate.Columns[i].Width = (int)Math.Floor(columnWidth);
                    dgvRate.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvRate.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //specifies that we can not sort this column
                    dgvRate.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    //specifies the value type assigned to the column
                    dgvRate.Columns[i].ValueType = typeof(double);
                }

                //Reset the CellValueChanged event
                Subscribe_CellValueChanged(true);

                //specifies that the desired number format is with 2 decimals, and thousands separated by a space
                dgvRate.Columns[0].DefaultCellStyle.Format = "N2";
                dgvRate.Columns[1].DefaultCellStyle.Format = "N2";
                //specifies that the desired number format is with 5 decimals, and thousands separated by a space
                dgvRate.Columns[2].DefaultCellStyle.Format = "N5";
                dgvRate.Columns[3].DefaultCellStyle.Format = "N5";

                //For traces
                logger.Debug(location + "Before line: dgvRateSetColumnEnable(false, 0);");

                dgvRateSetColumnEnable(false, 0);

                //For traces
                logger.Debug(location + "Before line: isDgvRateReadyForRefresh();");

                isDgvRateReadyForRefresh();

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

        //Enable or disable an entire column
        private void dgvRateSetColumnEnable(bool isEnable, int column)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Enable or disable an entire column");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  isEnable-> " + isEnable.ToString());
            logger.Debug(location + "  column-> " + column.ToString());

            if (isEnable)
            {
                dgvRate.Columns[column].ReadOnly = false;
                dgvRate.Columns[column].DefaultCellStyle.ForeColor = Color.Black;
                dgvRate.Columns[column].DefaultCellStyle.SelectionBackColor = Color.RoyalBlue;
                dgvRate.Columns[column].DefaultCellStyle.SelectionForeColor = Color.White;
            }
            else
            {
                dgvRate.Columns[column].ReadOnly = true;
                dgvRate.Columns[column].DefaultCellStyle.ForeColor = Color.RoyalBlue;
                dgvRate.Columns[column].DefaultCellStyle.SelectionBackColor = Color.White;
                dgvRate.Columns[column].DefaultCellStyle.SelectionForeColor = Color.RoyalBlue;
            }

            //For traces
            logger.Debug(location + " Ending...");
        }
    
        //Refreshes the dgvRate object
        private bool isDgvRateReadyForRefresh()
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
            logger.Info(location + "Purpose: Refreshes the dgvRate object");

            double minValue = 0;
            double maxValue = 0;
            double rate1To2 = 0;
            double rate2To1 = 0;

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  isDgvRateModified-> " + isDgvRateModified.ToString());

            if (isDgvRateModified)
            {
                //For traces   
                logger.Debug(location + " After line: if (isDgvRateModified)");

                DialogResult selectButton = MessageBox.Show(msgSave, titleForm, MessageBoxButtons.YesNo, MessageBoxIcon.None);

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  result-> " + selectButton.ToString());

                switch (selectButton)
                {
                    case DialogResult.Yes:
                        //For traces
                        logger.Debug(location + "Before line: if (isEachCellValid())");

                        if (isEachCellValid())
                        {
                            //For traces   
                            logger.Debug(location + " After line: if (isEachCellValid())");
                         
                            //For traces
                            logger.Debug(location + "Before line: saveExchangeRates();");

                            saveExchangeRates(); 
                        }
                        else { return false; }

                        break;
                    case DialogResult.No:
                        break;
                }

                isDgvRateModified = false;
            }

            effectiveDate = dtpEffectiveDate.Value;
            currencyCode1 = nz((cboCurrencyType1.SelectedItem as DataRowView).Row["CURRENCY_CODE"]);
            currencyCode2 = nz((cboCurrencyType2.SelectedItem as DataRowView).Row["CURRENCY_CODE"]);

            //For traces
            logger.Debug(location + "Values:");
            logger.Debug(location + "  effectiveDate-> " + effectiveDate.ToString("yyyyMMdd"));
            logger.Debug(location + "  currencyCode1-> " + currencyCode1);
            logger.Debug(location + "  currencyCode2-> " + currencyCode2);

            //Removes the CellValueChanged event since we will change the value of each cell
            Subscribe_CellValueChanged(false);

            //For traces
            logger.Debug(location + "Before line: dgvRate.Rows.Clear();");

            //Erases all data from the dgvRate object
            dgvRate.Rows.Clear();

            if (!(currencyCode1 == "" || currencyCode2 == "" || currencyCode1 == currencyCode2))
            {
                //For traces
                logger.Debug(location + " After line: if (!(currencyCode1 == \"\" || currencyCode2 == \"\" || currencyCode1 == currencyCode2))");

                minValue = CENT;

                ExchangeRates er = new ExchangeRates(btecDB);

                //For traces
                logger.Debug(location + "Before line: DataTable RatesTable = er.GetExchangeRates(true, effectiveDate.ToString(\"yyyyMMdd\"), currencyCode1, currencyCode2);");
                
                DataTable RatesTable = er.GetExchangeRates(true, effectiveDate.ToString("yyyyMMdd"), currencyCode1, currencyCode2);

                dgvRate.AllowUserToAddRows = true;

                //For traces
                logger.Debug(location + "Before line: for (int value = 0; value < RatesTable.Rows.Count; value++)");

                string strMaximalValue;
                string strBuffer1;
                string strBuffer2;
                double buffer1 = 0;
                double buffer2 = 0;

                //Fill the dgvRate object with the values ​​of the RatesTable object with a few treatments
                for (int i = 0; i < RatesTable.Rows.Count; i++)
                {
                    strMaximalValue = (RatesTable.Rows[i]["HIGH_VALUE"] ?? "").ToString();
                    if (string.IsNullOrEmpty(strMaximalValue)) { throw new Exception("The field HIGH_VALUE on table EXCHANGE_RATE_DTL is empty."); }
                    else { maxValue = double.Parse(strMaximalValue); }

                    strBuffer1 = (RatesTable.Rows[i]["CODE1_CODE2_RATE"] ?? "").ToString();
                    if (string.IsNullOrEmpty(strBuffer1)) { throw new Exception("The field CODE1_CODE2_RATE on table EXCHANGE_RATE_DTL is empty."); }
                    else { buffer1 = double.Parse(strBuffer1); }

                    strBuffer2 = (RatesTable.Rows[i]["CODE2_CODE1_RATE"] ?? "").ToString();
                    if (string.IsNullOrEmpty(strBuffer2)) { throw new Exception("The field CODE2_CODE1_RATE on table EXCHANGE_RATE_DTL is empty."); }
                    else { buffer2 = double.Parse(strBuffer2); }

                    //Determines whether to switch the columns of exchange rates
                    if (string.Compare(currencyCode1, currencyCode2) < 0)
                    {
                        rate1To2 = buffer1;
                        rate2To1 = buffer2;
                    }
                    else
                    {
                        rate1To2 = buffer2;
                        rate2To1 = buffer1;
                    }

                    //For traces
                    logger.Debug(location + "Before line: dgvRate.Rows.Add(minValue.ToString(\"N2\"), maxValue.ToString(\"N2\"), rate1To2.ToString(\"N5\"), rate2To1.ToString(\"N5\"));");

                    //Adds a row of records in the dgvRate object
                    dgvRate.Rows.Add(minValue.ToString("N2"), maxValue.ToString("N2"),
                                        rate1To2.ToString("N5"), rate2To1.ToString("N5"));

                    minValue = maxValue + CENT;
                }

                //Adds a row of records in the dgvRate object just for Minimal Value
                dgvRate[0, RatesTable.Rows.Count].Value = minValue.ToString("N2");
            }
            else
            {
                //Removes the unnecessary line in the empty dgvRate object
                dgvRate.AllowUserToAddRows = false;
            }

            //Reset the CellValueChanged event
            Subscribe_CellValueChanged(true);

            //For traces
            logger.Debug(location + " Ending...");

            //displays the default cursor
            Cursor.Current = Cursors.Default;

            return true;
        }

        //Occurs when the selected value changes in the cboCurrencyType1 ComboBox
        private void cboCurrencyType1_SelectedValueChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the selected value changes in the cboCurrencyType1 ComboBox");

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  currencyCode1-> " + currencyCode1);
                logger.Debug(location + "  cboCurrencyType1.SelectedValue-> " + nz((cboCurrencyType1.SelectedItem as DataRowView).Row["CURRENCY_CODE"]));

                //Refreshes the dgvRate object only if the control's value is different
                if (currencyCode1 != nz((cboCurrencyType1.SelectedItem as DataRowView).Row["CURRENCY_CODE"]))
                {
                    //For traces
                    logger.Debug(location + " After line: if (currencyCode1 != nz((cboCurrencyType1.SelectedItem as DataRowView).Row[\"CURRENCY_CODE\"]))");

                    //If there are validation errors the refresh will not occur
                    if (!isDgvRateReadyForRefresh())
                    {
                        //For traces
                        logger.Debug(location + " After line: if (!isDgvRateReadyForRefresh())");

                        cboCurrencyType1.SelectedValueChanged -= this.cboCurrencyType1_SelectedValueChanged; 
                        cboCurrencyType1.SelectedValue = currencyCode1;
                        cboCurrencyType1.SelectedValueChanged += this.cboCurrencyType1_SelectedValueChanged;
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

        //Occurs when the selected value changes in the cboCurrencyType2 ComboBox
        private void cboCurrencyType2_SelectedValueChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the selected value changes in the cboCurrencyType2 ComboBox");

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  currencyCode2-> " + currencyCode2);
                logger.Debug(location + "  cboCurrencyType2.SelectedValue-> " + nz((cboCurrencyType2.SelectedItem as DataRowView).Row["CURRENCY_CODE"]));

                //Refreshes the dgvRate object only if the control's value is different
                if (currencyCode2 != nz((cboCurrencyType2.SelectedItem as DataRowView).Row["CURRENCY_CODE"]))
                {
                    //For traces
                    logger.Debug(location + " After line: if (currencyCode2 != nz((cboCurrencyType2.SelectedItem as DataRowView).Row[\"CURRENCY_CODE\"]))");

                    //If there are validation errors the refresh will not occur
                    if (!isDgvRateReadyForRefresh())
                    {
                        //For traces
                        logger.Debug(location + " After line: if (!isDgvRateReadyForRefresh())");

                        cboCurrencyType2.SelectedValueChanged -= this.cboCurrencyType2_SelectedValueChanged;
                        cboCurrencyType2.SelectedValue = currencyCode2;
                        cboCurrencyType2.SelectedValueChanged += this.cboCurrencyType2_SelectedValueChanged;
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

        private void dtpEffectiveDate_ValueChanged(object sender, EventArgs e)
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
                logger.Info(location + "Purpose: Occurs when the effective date changes");

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  effectiveDate-> " + effectiveDate.ToString("yyyyMMdd"));
                logger.Debug(location + "  dtpEffectiveDate.Value-> " + dtpEffectiveDate.Value.ToString("yyyyMMdd"));

                //Refreshes the dgvRate object only if the control's value is different
                if (effectiveDate != dtpEffectiveDate.Value)
                    {
                    //For traces
                    logger.Debug(location + " After line: if (effectiveDate != dtpEffectiveDate.Value)");

                    //If there are validation errors the refresh will not occur
                    if (!isDgvRateReadyForRefresh())
                    {
                        //For traces
                        logger.Debug(location + " After line: if (!isDgvRateReadyForRefresh())");

                        dtpEffectiveDate.ValueChanged -= this.dtpEffectiveDate_ValueChanged;
                        dtpEffectiveDate.Value = effectiveDate;
                        dtpEffectiveDate.ValueChanged += this.dtpEffectiveDate_ValueChanged;
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

        private void dgvRate_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
             isDgvRateModified = true;
        }

        //Allows the validation of all cells of the dgvRate object
        private void dgvRate_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;
            
            try
            {
                //Determines whether the concerned cell is currently in edit mode
                if (dgvRate[e.ColumnIndex, e.RowIndex].IsInEditMode)
                {
                    //For traces
                    logger.Debug(location + " Starting...");

                    //For traces
                    logger.Info(location + "Purpose: Allows the validation of all cells of the dgvRate object");

                    string myValue = (e.FormattedValue ?? "").ToString();
                    string newValue;
                    string errorMessage;

                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  myValue-> " + myValue);
                    logger.Debug(location + "  ColumnIndex-> " + e.ColumnIndex);

                    switch (e.ColumnIndex)
                    {
                        //If this is the Maximal Value field
                        case 1:
                            newValue = treatCellValueForDouble(myValue);

                            //If the value has changed, display the new value
                            if (!(myValue.Equals(newValue)))
                            {
                                Subscribe_TextChanged(false);

                                dgvRate.EditingControl.Text = newValue;
                         
                                Subscribe_TextChanged(true);
                            }

                            if (!(String.IsNullOrEmpty(newValue)))
                            {
                                //For traces   
                                logger.Debug(location + " After line: if (!(String.IsNullOrEmpty(newValue)))");

                                errorMessage = rm.GetString("MsgGreaterEqualMinimalValue");

                                if (isDouble(newValue))
                                {
                                    //For traces   
                                    logger.Debug(location + " After line: if (isDouble(newValue))");

                                    double newValueDouble = Double.Parse(newValue);
                                    int row = e.RowIndex;

                                    //For traces   
                                    logger.Debug(location + "Before line: while (dgvRate[0, row].Value == null) { row--; }");

                                    //Finds the first line backwards where Minimal Value is not null
                                    while (dgvRate[0, row].Value == null) { row--; }

                                    string strMinimalValue = (dgvRate[0, row].Value ?? "").ToString();
                                    double minimalValue = 0;

                                    if (string.IsNullOrEmpty(strMinimalValue)) { throw new Exception("A cell in the Minimal Value column of the dgvRate object is empty."); }
                                    else { minimalValue = double.Parse(strMinimalValue); }

                                    //If the new value is less than Minimal Value, displays an error,
                                    //otherwise adjust the Minimal Value for the next line
                                    if (newValueDouble < minimalValue)
                                    {
                                        //For traces   
                                        logger.Debug(location + " After line: if (newValueDouble < minimalValue)");

                                        treatCellError(e, errorMessage);
                                    }
                                    else
                                    {
                                        dgvRate[0, e.RowIndex + 1].Value = (newValueDouble + CENT).ToString("N2");
                                   
                                        //Remove the events because validation is accepted then the value
                                        //will be updated (the next 2 lines)
                                        Subscribe_TextChanged(false);
                                    }
                                }
                                else { treatCellError(e, errorMessage); }
                            }
                            //If Maximal Value is empty, deletes the entire line
                            else
                            {
                                //For traces   
                                logger.Debug(location + "next lines: Deletes the entire line since Maximal Value is empty");

                                //Finishes editing the current cell to allow to modify its value (the next 3 lines)
                                dgvRate.EndEdit();

                                //Remove the events because validation is accepted then the value
                                //will be updated (the next 2 lines)
                                Subscribe_TextChanged(false);
                                dgvRate[1, e.RowIndex].Value = null;

                                dgvRate[2, e.RowIndex].Value = null;
                                dgvRate[3, e.RowIndex].Value = null;

                                if (e.RowIndex < dgvRate.RowCount - 1) { dgvRate[0, e.RowIndex + 1].Value = null; }
                            }

                            break;
                        //If this is one of the Exchange Rate fields
                        case 2:
                        case 3:
                            newValue = treatCellValueForDouble(myValue);

                            //If the value has changed, display the new value
                            if (!(myValue.Equals(newValue)))
                            {
                                Subscribe_TextChanged(false);

                                dgvRate.EditingControl.Text = newValue;
                            
                                Subscribe_TextChanged(true);
                            }

                            if (!(String.IsNullOrEmpty(newValue)))
                            {
                                //For traces   
                                logger.Debug(location + " After line: if (!(String.IsNullOrEmpty(newValue)))");

                                errorMessage = rm.GetString("MsgGreater0LessEqual999");

                                if (isDouble(newValue))
                                {
                                    //For traces   
                                    logger.Debug(location + " After line: if (isDouble(newValue))");

                                    double newValueDouble = Double.Parse(newValue);

                                    //The exchange rate must be located in ]0, 999]
                                    if (!(newValueDouble > 0 && newValueDouble <= 999))
                                    {
                                        //For traces   
                                        logger.Debug(location + " After line: if (!(newValueDouble > 0 && newValueDouble <= 999))");

                                        treatCellError(e, errorMessage); 
                                    }
                                    else
                                    {
                                        //Remove the events because validation is accepted then the value
                                        //will be updated (the next 2 lines)
                                        Subscribe_TextChanged(false);
                                    }
                                }
                                else { treatCellError(e, errorMessage); }
                            }
                            else
                            {
                                //An error occurs if Maximal Value is not empty
                                if (dgvRate[1, e.RowIndex].Value != null)
                                {
                                    //For traces   
                                    logger.Debug(location + " After line: if (dgvRate[1, e.RowIndex].Value != null)");

                                    errorMessage = string.Format(rm.GetString("MsgCannotBeEmpty"), "\n\n");

                                    treatCellError(e, errorMessage);

                                    Subscribe_TextChanged(false);

                                    //Resets the original value for the cell
                                    dgvRate.CancelEdit();
                                
                                    Subscribe_TextChanged(true);
                                }
                                else
                                {
                                    //Remove the event because validation is accepted then the value
                                    //will be updated (the next 2 lines)
                                    Subscribe_TextChanged(false);
                                }
                            }

                            break;
                    }

                    //For traces
                    logger.Debug(location + " Ending...");
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

        //Does the appropriate action if the value is invalid in a cell
        private void treatCellError(DataGridViewCellValidatingEventArgs e, string errorMessage)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Does the appropriate action if the value is invalid in a cell");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  errorMessage-> " + errorMessage);

            //Selects all the text as it is possible with the mouse (the next 3 lines)
            DataGridViewTextBoxEditingControl editControl = (DataGridViewTextBoxEditingControl)dgvRate.EditingControl;

            editControl.SelectionStart = 0;
            editControl.SelectionLength = editControl.Text.Length;

            MessageBox.Show(errorMessage, rm.GetString("TitleMsgValidationError"), MessageBoxButtons.OK, MessageBoxIcon.Error);

            //Prevents from leaving the cell while the error is not corrected 
            e.Cancel = true;

            //For traces
            logger.Debug(location + " Ending...");
        }

        //Is called when a cell enters in edit mode. Used to create the TextChanged event on a cell
        private void dgvRate_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
                logger.Info(location + "Purpose: Is called when a cell enters in edit mode. Used to create the TextChanged event on a cell");

                Subscribe_TextChanged(false);

                myCell = (TextBox)e.Control;
                
                myCell.TextChanged += new EventHandler(textBox_TextChanged);
                isSubscribedTextChanged = true;

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

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;
            
            try
            {
                int column = dgvRate.CurrentCell.ColumnIndex;

                //if exchange rates are concerned
                if (column == 2 || column == 3)
                {
                    int row = dgvRate.CurrentCell.RowIndex;

                    //if the Maximal Value field is null 
                    if (dgvRate[1, row].Value == null)
                    {
                        //Removes the TextChanged event because we switch the
                        //current cell, so the value of the old cell will be updated,
                        //and we don't want to call this event
                        Subscribe_TextChanged(false);

                        //Removes the CellValidating event because we switch the
                        //current cell and we don't want to call it
                        Subscribe_CellValidating(false);

                        //puts the focus on the Maximal Value field and ready for editing (the next 2 lines)
                        dgvRate.CurrentCell = dgvRate[1, row];
                        dgvRate.BeginEdit(true);

                        //Add the CellValidating event to respond at the next validation
                        Subscribe_CellValidating(true);

                        MessageBox.Show(rm.GetString("MsgCompleteMaximalValue"), rm.GetString("TitleMsgValidationError"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                        dgvRate[column, row].Value = null;
                    }
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

        //Ensures to have only one active TextChanged event at time if any
        private void Subscribe_TextChanged(bool enabled)
        {
            try
            {
                if (!enabled)
                    myCell.TextChanged -= textBox_TextChanged;
                else if (!isSubscribedTextChanged)
                    myCell.TextChanged += textBox_TextChanged;
            }
            catch { }
 
            //An exception has no consequence for this method
            isSubscribedTextChanged = enabled;
        }

        //Ensures to have only one active CellValidating event at time if any
        private void Subscribe_CellValidating(bool enabled)
        {
            try
            {
                if (!enabled)
                    dgvRate.CellValidating -= dgvRate_CellValidating;
                else if (!isSubscribedCellValidating)
                    dgvRate.CellValidating += dgvRate_CellValidating;
            }
            catch { }

            //An exception has no consequence for this method
            isSubscribedCellValidating = enabled;
        }

        //Ensures to have only one active CellValueChanged event at time if any
        private void Subscribe_CellValueChanged(bool enabled)
        {
            try
            {
                if (!enabled)
                    dgvRate.CellValueChanged -= dgvRate_CellValueChanged;
                else if (!isSubscribedCellValueChanged)
                    dgvRate.CellValueChanged += dgvRate_CellValueChanged;
            }
            catch { }

            //An exception has no consequence for this method
            isSubscribedCellValueChanged = enabled;
        }

        //Tries to convert the value of the cell in a valid real number as a string (or null)
        private string treatCellValueForDouble(string myValue)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            //For traces
            logger.Debug(location + " Starting...");

            //For traces
            logger.Info(location + "Purpose: Tries to convert the value of the cell in a valid real number as a string (or null)");

            //For traces
            logger.Debug(location + "Parameters:");
            logger.Debug(location + "  myValue-> " + myValue);

            string newValue = myValue.Trim();

            if (!(String.IsNullOrEmpty(newValue)))
            {
                if (!(isDouble(newValue)))
                {
                    string buffer;

                    buffer = newValue.Replace(".", ",");

                    if (isDouble(buffer)) { newValue = buffer; }
                    else
                    {
                        buffer = newValue.Replace(",", ".");

                        if (isDouble(buffer)) { newValue = buffer; }
                    }
                }
            }
            else { newValue = null; }

            //For traces
                logger.Debug(location + "  newValue-> " + newValue);

            //For traces
            logger.Debug(location + " Ending...");

            return newValue;
        }

        //Returns an empty string if the object is null, otherwise returns the corresponding string
        private string nz(object obj)
        {
            return obj != null ? obj.ToString() : String.Empty;
        }

        //Determines if the string represents a double
        private bool isDouble(string myString)
        {
            try
            {
                //Tries to convert a string in double
                double value = Double.Parse(myString);

                return true;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }
    }
}