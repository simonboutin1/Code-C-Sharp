using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using BancTec.PCR2P.Core.BusinessLogic.Administration;
using BancTec.PCR2P.Core.DatabaseModel.Administration;
using BancTec.PCR2P.Core.DatabaseModel.Administration;
using NetCommunTools;

namespace Administration
{
    //Needed so that the form can be called in Visual Basic (the next 4 lines)
    [ComVisible(true)]
    [Guid("8507912F-FFF6-41B0-8353-192FCF22960B")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Administration.frmEndPoints")]

    public partial class frmEndPoints : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmEndPoints).Name;

        //Needed so that the form can be called in Visual Basic (the next 2 lines)
        bool _isInit = false;
        public bool IsInitialized { get { return _isInit; } }

        ILoggerBtec logger;
        ResourceManager rm;
        IBtecDB btecDB;

        string _sLogginUserID = "";
        string _sLoggedPWD = "";
        string _sLoggedDSN = "";
        int _intUsersAuthorizationLevel;

        public frmEndPoints() { }

        //Needed so that the form can be called in Visual Basic
        public void Init(string sLoggedUserID, string sPWD, string sDSN, int intLanguage, int intUsersAuthorizationLevel)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {

                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmEndPoints));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmEndPoints));
                }

                //For traces
                logger.Debug(location + " Starting...");

                //For traces
                logger.Info(location + "Purpose: Needed so that the form can be called in Visual Basic");

                //For traces
                logger.Debug(location + "Parameters:");
                logger.Debug(location + "  sLoggedUserID-> " + sLoggedUserID);
                logger.Debug(location + "  sPWD-> " + "*****");
                logger.Debug(location + "  sDSN-> " + sDSN);
                logger.Debug(location + "  intLanguage-> " + intLanguage.ToString());
                logger.Debug(location + "  intUsersAuthorizationLevel-> " + intUsersAuthorizationLevel.ToString());

                //If we want to force the form to appear in French. Remove these 2 lines if we want English (default)
//                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fr-CA");
//                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr-CA");

                //To enable this form to be multilingual. The "Localizable" propertie must be True for the form (the next 2 lines)
                Thread.CurrentThread.CurrentCulture = new CultureInfo((intLanguage == 2 ? "fr-CA" : "en-US"));
                Thread.CurrentThread.CurrentUICulture = new CultureInfo((intLanguage == 2 ? "fr-CA" : "en-US"));

                //We must get the resource manager after setting the culture.
                rm = Utilities.GetResourceManager("Administration.Properties.Resources", this.GetType().Assembly);

                //For traces   
                logger.Debug(location + "Before line: InitializeComponent();");

                //Initializes all the components of the form
                InitializeComponent();

                _sLogginUserID = sLoggedUserID;
                _intUsersAuthorizationLevel = intUsersAuthorizationLevel;
                _sLoggedDSN = sDSN;
                _sLoggedPWD = sPWD;

                if (string.IsNullOrEmpty(_sLogginUserID) || string.IsNullOrEmpty(_sLoggedPWD) || string.IsNullOrEmpty(_sLoggedDSN))
                {
                    MessageBox.Show(rm.GetString("UserOrPasswordIsNull"), rm.GetString("MissingInfo"));
                    this.Close();
                }
                else
                {
                    //For traces   
                    logger.Debug(location + "Before line: IniFile ini = new IniFile(Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), \"lockbox.ini\"));");
                    logger.Debug(location + "        and: btecDB = IBtecDBFactory.GetDefaultDb(new ConnectionProvider(_sLoggedDSN, ini.ReadValue(\"Database\", \"TNS\", \"\"), _sLogginUserID, _sLoggedPWD));");

                    IniFile ini = new IniFile(Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "lockbox.ini"));
                    btecDB = IBtecDBFactory.GetDefaultDb(new ConnectionProvider(_sLoggedDSN, ini.ReadValue("Database", "TNS", ""), _sLogginUserID, _sLoggedPWD));
                }

                _isInit = true;

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

                //For traces   
                logger.Debug(location + "Before line: dgvEndPoints.dgvSetVerticalBar();");

                //Always shows the vertical scrollbar
                dgvEndPoints.dgvSetVerticalBar();

                //Specifies the number of desired columns for the dgvEndPoints object
                dgvEndPoints.ColumnCount = 4;

                string[] columnHeaderText = {rm.GetString("colId"),
                                             rm.GetString("colDescription"),
                                             rm.GetString("colFrom"),
                                             rm.GetString("colTo")};
                //Specifies the dgvEndPoints width to calculate the columnns width
                double dgvEndPointsWidth = dgvEndPoints.Width - dgvEndPoints.getVerticalScrollBarWidth();

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
                SortPatternDefData[] epData = ep.GetEndPointMatchingAddressDefinition();

                //For traces   
                logger.Debug(location + "Before line: for (int i = 0; i < epData.Count<EndPointData>(); i++)");

                //Fill the dgvEndPoints object with values
                for (int i = 0; i < epData.Count<SortPatternDefData>(); i++)
                {
                    //Adds a row of records in the dgvEndPoints object
                    dgvEndPoints.Rows.Add(epData[i].GetValue("END_POINT_ID"),
                                          epData[i].GetValue("DESCRIPTION"),
                                          epData[i].GetValue("ADR1"),
                                          epData[i].GetValue("ADR2"));
                }

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  dgvEndPoints.Rows.Count-> " + dgvEndPoints.Rows.Count.ToString());

                //To be sure to have a current row before calling enableEditRemoveButtons()
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
                                                              MessageBoxIcon.Question);

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
                                        MessageBoxIcon.Exclamation);
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
                frmEndPointsEdit _frm = new frmEndPointsEdit(btecDB, rm.GetString("titleAddEndPoint"), endPointID);

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
                logger.Debug(location + "Before line: editingEndPoint(currentRow);");

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

                //For traces   
                logger.Debug(location + "Before line: editingEndPoint(currentRow);");

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
                frmEndPointsEdit _frm = new frmEndPointsEdit(btecDB, rm.GetString("titleEditEndPoint"), endPointID);

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

        //Occurs when a line of the dgvRate object is selected
        private void dgvEndPoints_SelectionChanged(object sender, EventArgs e)
        {
            enableEditRemoveButtons();
        }
    }
}
