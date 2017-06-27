using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using BancTec.PCR2P.Core.DatabaseModel.Administration;
using NetCommunTools;

namespace Administration
{
    public partial class frmEndPointDef : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmEndPointDef).Name;

        ILoggerBtec logger;
        ResourceManager rm;
        IBtecDB btecDB;

        public frmEndPointDef(IBtecDB btecDBParent)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmEndPointDef));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmEndPointDef));
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
        private void frmEndPoints_Load(object sender, EventArgs e)
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

                //Specifies the number of desired columns for the dgvEndPoints object
                dgvEndPoints.ColumnCount = 4;

                string[] columnHeaderText = {rm.GetString("colId"),
                                             rm.GetString("colDescription"),
                                             rm.GetString("colFrom"),
                                             rm.GetString("colTo")};
                //Specifies the dgvEndPoints width to calculate the columnns width
                double dgvEndPointsWidth = dgvEndPoints.Width;

                //Specifies all the columns width of the dgvEndPoints object 
                //The sum of all the constants must equal 1
                double[] columnWidth = {dgvEndPointsWidth * 0.068,
                                        dgvEndPointsWidth * 0.432,
                                        dgvEndPointsWidth * 0.250,
                                        dgvEndPointsWidth * 0.250};

                for (int i = 0; i < dgvEndPoints.ColumnCount; i++)
                {
                    dgvEndPoints.Columns[i].HeaderText = columnHeaderText[i];
                    dgvEndPoints.Columns[i].Width = (int)Math.Round(columnWidth[i]);
                    dgvEndPoints.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //specifies that we cannot sort this column
                    dgvEndPoints.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                dgvEndPoints.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEndPoints.Columns[0].ValueType = typeof(string);

                for (int i = 1; i <= 3; i++)
                {
                    dgvEndPoints.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvEndPoints.Columns[i].ValueType = typeof(string);
                }

                //For traces   
                logger.Debug(location + "Before line: refreshDgvEndPoints();");

                refreshDgvEndPoints();

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

        //Refreshes the dgvEndPoints object with the most recent data
        public void refreshDgvEndPoints()
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
                logger.Info(location + "Purpose: Refreshes the dgvEndPoints object with the most recent data");

                //displays an hourglass cursor
                Cursor.Current = Cursors.WaitCursor;

                //For traces   
                logger.Debug(location + "Before line: dgvEndPoints.Rows.Clear();");

                //Erases all data from the dgvEndPoints object
                dgvEndPoints.Rows.Clear();
  
                //For traces   
                logger.Debug(location + "Before line: EndPoint ep = new EndPoint(btecDB);");
                logger.Debug(location + "        and: EndPointData[] epData = ep.GetEndPointMatchingAddressDefinition();");

                EndPoint ep = new EndPoint(btecDB);
                EndPointData[] epData = ep.GetEndPointMatchingAddressDefinition();

                if (epData != null)
                {
                    //For traces   
                    logger.Debug(location + "Before line: for (int value = 0; value < epData.Count<EndPointData>(); value++)");

                    //Fill the dgvEndPoints object with values
                    for (int i = 0; i < epData.Count<EndPointData>(); i++)
                    {
                        //Adds a row of records in the dgvEndPoints object
                        dgvEndPoints.Rows.Add(epData[i].GetStringValue(epData[i].END_POINT_ID),
                                              epData[i].GetStringValue(epData[i].DESCRIPTION),
                                              epData[i].GetStringValue(epData[i].ADR1),
                                              epData[i].GetStringValue(epData[i].ADR2));
                    }
                }

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  dgvEndPoints.Rows.Count-> " + dgvEndPoints.Rows.Count.ToString());

                //To be sure to have a current row before calling enableEditRemoveWorkTypeButtons()
                if (dgvEndPoints.Rows.Count > 0)
                {
                    dgvEndPoints.CurrentCell = dgvEndPoints[0, 0];
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

                if (dgvEndPoints.CurrentRow != null)
                {
                    //displays an hourglass cursor
                    Cursor.Current = Cursors.WaitCursor;

                    int currentIndex = dgvEndPoints.CurrentRow.Index;
                    string strEndPointID = (dgvEndPoints[0, currentIndex].Value ?? "").ToString();

                    //For traces
                    logger.Debug(location + "Values:");
                    logger.Debug(location + "  currentIndex-> " + currentIndex.ToString());
                    logger.Debug(location + "  strEndPointID-> " + strEndPointID);

                    //For traces   
                    logger.Debug(location + "Before line: EndPoint ep = new EndPoint(btecDB);");
                    logger.Debug(location + "        and: bool isRemovable = ep.isEndPointRemovable(strEndPointID);");

                    EndPoint ep = new EndPoint(btecDB);
                    bool isRemovable = ep.isEndPointRemovable(strEndPointID);

                    if (isRemovable)
                    {
                        //For traces   
                        logger.Debug(location + " After line: if (isRemovable)");

                        string description = (dgvEndPoints[1, currentIndex].Value ?? "").ToString();
                        string from        = (dgvEndPoints[2, currentIndex].Value ?? "").ToString();
                        string to          = (dgvEndPoints[3, currentIndex].Value ?? "").ToString();
          
                        string message = "";
                        string endPoint = "";

                        message += rm.GetString("msgDeleteEndPoint1of2") + strEndPointID + ":";
                        message += "\n" + "\n";

                        endPoint += String.IsNullOrEmpty(description) ? "" : description + "\n";
                        endPoint += String.IsNullOrEmpty(from)        ? "" : from        + "\n";
                        endPoint += String.IsNullOrEmpty(to)          ? "" : to          + "\n";

                        message += String.IsNullOrEmpty(endPoint)     ? "" : endPoint + "\n";
                        message += rm.GetString("msgDeleteEndPoint2of2");
                        
                        DialogResult result = MessageBox.Show(message,
                                                              rm.GetString("titleDeleteEndPoint"), 
                                                              MessageBoxButtons.OKCancel,
                                                              MessageBoxIcon.Exclamation);

                        switch (result)
                        {
                            case DialogResult.OK:
                                //For traces   
                                logger.Debug(location + "Before line: ep.deleteEndPoint(strEndPointID);");
                            
                                //delete the end point
                                ep.deleteEndPoint(strEndPointID);

                                //For traces   
                                logger.Debug(location + "Before line: refreshDgvEndPoints();");

                                refreshDgvEndPoints();
                                break;
                            case DialogResult.Cancel:
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show(rm.GetString("msgEndPointCannotBeRemove"),
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
                logger.Debug(location + "Before line: EndPoint ep = new EndPoint(btecDB);");

                EndPoint ep = new EndPoint(btecDB);

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  ep.GetMaxEndPointID() + 1-> " + (ep.GetMaxEndPointID() + 1).ToString());
            
                long endPointID = ep.GetMaxEndPointID() + 1;

                //For traces   
                logger.Debug(location + "Before line: frmEndPointsEdit _frm = new frmEndPointsEdit(btecDB, rm.GetString(\"titleAddEndPoint\"), endPointID);");
                logger.Debug(location + "        and: DialogResult result = _frm.ShowDialog(this);");

                //Opens the child form containing a new end point corresponding to the end point Id (next 2 lines)
                frmEndPointEdit _frm = new frmEndPointEdit(btecDB, rm.GetString("titleAddEndPoint"), endPointID);

                DialogResult result = _frm.ShowDialog(this);
                _frm.Dispose();

                if (result == DialogResult.OK) 
                {
                    //For traces   
                    logger.Debug(location + "Before line: refreshDgvEndPoints();");

                    refreshDgvEndPoints(); 
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

                DataGridViewRow currentRow = dgvEndPoints.CurrentRow;

                //For traces   
                logger.Debug(location + "Before line: editingEndPoint(row);");

                editingEndPoint(currentRow);

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

        //Occurs when a line in the dgvEndPoints object is double-clicked
        private void dgvEndPoints_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                logger.Info(location + "Purpose: Occurs when a line in the dgvEndPoints object is double-clicked");

                DataGridViewRow currentRow = (sender as DataGridView).CurrentRow;

                if (currentRow != null)
                {
                    //For traces   
                    logger.Debug(location + "Before line: editingEndPoint(row);");

                    editingEndPoint(currentRow);
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

        //Gets the end point Id and opens the frmEndPointsEdit form for editing
        private void editingEndPoint(DataGridViewRow currentRow)
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
                logger.Info(location + "Purpose: Gets the end point Id and opens the frmEndPointsEdit form for editing");

                string strEndPointID = (dgvEndPoints[0, currentRow.Index].Value ?? "").ToString();
                long endPointID = 0;

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  strEndPointID-> " + strEndPointID);

                if (string.IsNullOrEmpty(strEndPointID)) { throw new Exception("A cell of the Id column of the dgvEndPoints object is empty."); }
                else { endPointID = long.Parse(strEndPointID); }

                //For traces   
                logger.Debug(location + "Before line: frmEndPointsEdit _frm = new frmEndPointsEdit(btecDB, rm.GetString(\"titleEditEndPoint\"), endPointID);");
                logger.Debug(location + "        and: DialogResult result = _frm.ShowDialog(this);");

                //Opens the child form containing the address corresponding to the address Id (next 2 lines)
                frmEndPointEdit _frm = new frmEndPointEdit(btecDB, rm.GetString("titleEditEndPoint"), endPointID);

                DialogResult result = _frm.ShowDialog(this);
                _frm.Dispose();

                if (result == DialogResult.OK)
                {
                    //For traces   
                    logger.Debug(location + "Before line: refreshDgvEndPoints();");

                    refreshDgvEndPoints();
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
            if (dgvEndPoints.CurrentRow == null)
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
        private void dgvEndPoints_CurrentCellChanged(object sender, EventArgs e)
        {
            enableEditRemoveButtons();
        }
    }
}
