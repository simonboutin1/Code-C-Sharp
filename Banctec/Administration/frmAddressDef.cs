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
    public partial class frmAddressDef : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmAddressDef).Name;

        ILoggerBtec logger;
        ResourceManager rm;
        IBtecDB btecDB;

        public frmAddressDef(IBtecDB btecDBParent)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmAddressDef));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmAddressDef));
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

                //Specifies the number of desired columns for the dgvAddress object
                dgvAddress.ColumnCount = 7;

                string[] columnHeaderText = {rm.GetString("colId"),
                                             rm.GetString("colName"),
                                             rm.GetString("colAddressLine1"),
                                             rm.GetString("colAddressLine2"),
                                             rm.GetString("colCity"),
                                             rm.GetString("colState"), 
                                             rm.GetString("colZipCode")};

                //Specifies the dgvAddress width to calculate the columnns width
                double dgvAddressWidth = dgvAddress.Width;

                //Specifies all the columns width of the dgvAddress object 
                //The sum of all the constants must equal 1
                double[] columnWidth = {dgvAddressWidth * 0.050,
                                        dgvAddressWidth * 0.181,
                                        dgvAddressWidth * 0.181,
                                        dgvAddressWidth * 0.181,
                                        dgvAddressWidth * 0.148,
                                        dgvAddressWidth * 0.148,
                                        dgvAddressWidth * 0.111};

                for (int i = 0; i < dgvAddress.ColumnCount; i++)
                {
                    dgvAddress.Columns[i].HeaderText = columnHeaderText[i];
                    dgvAddress.Columns[i].Width = (int)Math.Round(columnWidth[i]);
                    dgvAddress.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //specifies that we can not sort this column
                    dgvAddress.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                dgvAddress.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAddress.Columns[0].ValueType = typeof(string);

                for (int i = 1; i <= 3; i++)
                {
                    dgvAddress.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvAddress.Columns[i].ValueType = typeof(string);
                }

                for (int i = 4; i < dgvAddress.ColumnCount; i++)
                {
                    dgvAddress.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAddress.Columns[i].ValueType = typeof(string);
                }

                //For traces   
                logger.Debug(location + "Before line: refreshDgvAddress();");

                refreshDgvAddress();

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

        //Refreshes the dgvAddress object with the most recent data
        public void refreshDgvAddress()
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
                logger.Info(location + "Purpose: Refreshes the dgvAddress object with the most recent data");

                //displays an hourglass cursor
                Cursor.Current = Cursors.WaitCursor;

                //For traces   
                logger.Debug(location + "Before line: dgvAddress.Rows.Clear();");

                //Erases all data from the dgvAddress object
                dgvAddress.Rows.Clear();

                //For traces   
                logger.Debug(location + "Before line: AddressDefinition ad = new AddressDefinition(btecDB);");
                logger.Debug(location + "        and: AddressDefinitionData[] adData = ad.GetAddressInfo();");

                AddressDefinition ad = new AddressDefinition(btecDB);
                AddressDefinitionData[] adData = ad.GetAddressInfo();

                if (adData != null)
                {
                    //For traces   
                    logger.Debug(location + "Before line: for (int value = 0; value < adData.Count<AddressDefinitionData>(); value++)");

                    //Fill the dgvAddress object with values
                    for (int i = 0; i < adData.Count<AddressDefinitionData>(); i++)
                    {
                        //Adds a row of records in the dgvAddress object
                        dgvAddress.Rows.Add(adData[i].GetStringValue(adData[i].ADDRESS_ID),
                                            adData[i].GetStringValue(adData[i].NAME),
                                            adData[i].GetStringValue(adData[i].ADDRESS_LINE1),
                                            adData[i].GetStringValue(adData[i].ADDRESS_LINE2),
                                            adData[i].GetStringValue(adData[i].CITY),
                                            adData[i].GetStringValue(adData[i].PROV_STATE),
                                            adData[i].GetStringValue(adData[i].POSTAL_CODE));
                    }
                }

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  dgvAddress.Rows.Count-> " + dgvAddress.Rows.Count.ToString());

                //To be sure to have a current row before calling enableEditRemoveWorkTypeButtons()
                if (dgvAddress.Rows.Count > 0)
                {
                    dgvAddress.CurrentCell = dgvAddress[0, 0];
                }

                enableEditRemoveButtons();

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

                if (dgvAddress.CurrentRow != null)
                {
                    //displays an hourglass cursor
                    Cursor.Current = Cursors.WaitCursor;

                    int currentIndex = dgvAddress.CurrentRow.Index;
                    string strAddressID = (dgvAddress[0, currentIndex].Value ?? "").ToString();

                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  currentIndex-> " + currentIndex.ToString());
                    logger.Debug(location + "  strAddressID-> " + strAddressID);

                    //For traces   
                    logger.Debug(location + "Before line: EndPoint ep = new EndPoint(btecDB);");
                    logger.Debug(location + "        and: DataTable EndPointTable = ep.GetEndPointFromID(strAddressID);");

                    EndPoint ep = new EndPoint(btecDB);
                    DataTable EndPointTable = ep.GetEndPointFromAddressID(strAddressID);

                    //For traces
                    logger.Debug(location + "  EndPointTable.Rows.Count-> " + EndPointTable.Rows.Count.ToString());

                    if (EndPointTable.Rows.Count == 0)
                    {
                        //For traces   
                        logger.Debug(location + " After line: if (EndPointTable.Rows.Count == 0)");

                        string name    = (dgvAddress[1, currentIndex].Value ?? "").ToString();
                        string line1   = (dgvAddress[2, currentIndex].Value ?? "").ToString();
                        string line2   = (dgvAddress[3, currentIndex].Value ?? "").ToString();
                        string city    = (dgvAddress[4, currentIndex].Value ?? "").ToString();
                        string state   = (dgvAddress[5, currentIndex].Value ?? "").ToString();
                        string zipCode = (dgvAddress[6, currentIndex].Value ?? "").ToString();

                        string message = "";
                        string address = "";

                        message += rm.GetString("msgDeleteAddress1of2") + strAddressID + ":";
                        message += "\n" + "\n";

                        address += String.IsNullOrEmpty(name)    ? "" : name    + "\n";
                        address += String.IsNullOrEmpty(line1)   ? "" : line1   + "\n";
                        address += String.IsNullOrEmpty(line2)   ? "" : line2   + "\n";
                        address += String.IsNullOrEmpty(city)    ? "" : city    + "\n";
                        address += String.IsNullOrEmpty(state)   ? "" : state   + "\n";
                        address += String.IsNullOrEmpty(zipCode) ? "" : zipCode + "\n";

                        message += String.IsNullOrEmpty(address)     ? "" : address + "\n";
                        message += rm.GetString("msgDeleteAddress2of2");

                        DialogResult result = MessageBox.Show(message,
                                                              rm.GetString("titleDeleteAddress"), 
                                                              MessageBoxButtons.OKCancel,
                                                              MessageBoxIcon.Exclamation);

                        switch (result)
                        {
                            case DialogResult.OK:
                                //For traces   
                                logger.Debug(location + "Before line: AddressDefinition ad = new AddressDefinition(btecDB);");

                                AddressDefinition ad = new AddressDefinition(btecDB);

                                long addressID = 0;
                            
                                if (string.IsNullOrEmpty(strAddressID)) { throw new Exception("A cell of the Id column of the dgvAddress object is empty."); }
                                else { addressID = long.Parse(strAddressID); }

                                //For traces   
                                logger.Debug(location + "Before line: ad.DeleteAddress(strWorkTypeID);");
                            
                                //delete the address
                                ad.DeleteAddress(addressID);

                                //For traces   
                                logger.Debug(location + "Before line: refreshDgvAddress();");

                                refreshDgvAddress();
                                break;
                            case DialogResult.Cancel:
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show(rm.GetString("msgAddressCannotBeRemove"),
                                        rm.GetString("titleImpossibleToRemove"), 
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
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
                logger.Debug(location + "Before line: AddressDefinition ad = new AddressDefinition(btecDB);");

                AddressDefinition ad = new AddressDefinition(btecDB);

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  ad.GetMaxAddressID() + 1-> " + (ad.GetMaxAddressID() + 1).ToString());

                long addressID = ad.GetMaxAddressID() + 1;

                //For traces   
                logger.Debug(location + "Before line: frmAddressEditAddress _frm = new frmAddressEditAddress(btecDB, rm.GetString(\"addAddress\"), strWorkTypeID);");
                logger.Debug(location + "        and: DialogResult result = _frm.ShowDialog(this);");

                //Opens the child form containing a new address corresponding to the address Id (next 2 lines)
                frmAddressEdit _frm = new frmAddressEdit(btecDB, rm.GetString("addAddress"), addressID);

                DialogResult result = _frm.ShowDialog(this);
                _frm.Dispose();

                if (result == DialogResult.OK)
                {
                    //For traces   
                    logger.Debug(location + "Before line: refreshDgvAddress();");

                    refreshDgvAddress();
                }

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

                DataGridViewRow currentRow = dgvAddress.CurrentRow;

                //For traces   
                logger.Debug(location + "Before line: editingAddress(row);");

                editingAddress(currentRow);

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

        //Occurs when a line in the dgvAddress object is double-clicked
        private void dgvAddress_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                logger.Info(location + "Purpose: Occurs when a line in the dgvAddress object is double-clicked");

                DataGridViewRow currentRow = (sender as DataGridView).CurrentRow;

                if (currentRow != null)
                {
                    //For traces   
                    logger.Debug(location + "Before line: editingAddress(row);");

                    editingAddress(currentRow);
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

        //Gets the address Id and opens the frmAddressEditAddress form for editing
        private void editingAddress(DataGridViewRow currentRow)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            if (currentRow != null)
            {
                //displays an hourglass cursor
                Cursor.Current = Cursors.WaitCursor;

                //For traces
                logger.Debug(location + " Starting...");

                //For traces
                logger.Info(location + "Purpose: Gets the address Id and opens the frmAddressEditAddress form for editing");

                string strAddressID = (dgvAddress[0, currentRow.Index].Value ?? "").ToString();
                long addressID = 0;

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  strAddressID-> " + strAddressID);

                if (string.IsNullOrEmpty(strAddressID)) { throw new Exception("A cell of the Id column of the dgvAddress object is empty."); }
                else { addressID = long.Parse(strAddressID); }

                //For traces   
                logger.Debug(location + "Before line: frmAddressEditAddress _frm = new frmAddressEditAddress(btecDB, rm.GetString(\"editAddress\"), strWorkTypeID);");
                logger.Debug(location + "        and: DialogResult result = _frm.ShowDialog(this);");

                //Opens the child form containing the address corresponding to the address Id (next 2 lines)
                frmAddressEdit _frm = new frmAddressEdit(btecDB, rm.GetString("editAddress"), addressID);

                DialogResult result = _frm.ShowDialog(this);
                _frm.Dispose();

                if (result == DialogResult.OK)
                {
                    //For traces   
                    logger.Debug(location + "Before line: refreshDgvAddress();");

                    refreshDgvAddress();
                }

                //For traces
                logger.Debug(location + " Ending...");

                //displays the default cursor
                Cursor.Current = Cursors.Default;
            }
        }

        //Enables or disables the [Edit] and [Remove] buttons
        private void enableEditRemoveButtons()
        {
            if (dgvAddress.CurrentRow == null)
            {
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
            }
            else
            {
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
            }
        }

        //Occurs when a cell of the dgvRate object is selected
        private void dgvAddress_CurrentCellChanged(object sender, EventArgs e)
        {
            enableEditRemoveButtons();
        }
    }
}
