using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BancTec.PCR2P.Core.DatabaseModel;
using NetCommunTools;
using System.Resources;
using System.Threading;
using System.Globalization;
using BancTec.PCR2P.Core.BusinessLogic.Administration;
using System.Reflection;
using System.IO;

namespace Administration
{
    public partial class frmAddressEdit : Form
    {
        //get the class name for traces
        string CLASS_NAME = typeof(frmAddressEdit).Name;

        IBtecDB btecDB;
        long addressID = 0;
        bool isSomethingChanged = false;
        bool isFormClosedByUser = false;

        ResourceManager rm;
        ILoggerBtec logger;

        public frmAddressEdit(IBtecDB btecDBParent, string title, long addressIDParent)
        {
            //Get the method name for traces
            string location = CLASS_NAME + "." +
                              MethodBase.GetCurrentMethod().Name +
                              LogManagerBtec.TraceSeparator;

            try
            {
                //Used in the constructor to be able to trace (the next 5 lines)
                logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmAddressEdit));

                if (string.IsNullOrEmpty(logger.LogFileName))
                {
                    LogManagerBtecEx.ConfigLog4NetUsingPredefinedXmlAndParmatecINI(
                        Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Parmatec.ini"), "Administration.exe", "");

                    logger = LogManagerBtecEx.GetLoggerBtec(typeof(frmAddressEdit));
                }

                //For traces
                logger.Debug(location + " Starting...");

                //For traces
                logger.Debug(location + "Parameters:");
                logger.Debug(location + "  title-> " + title);
                logger.Debug(location + "  workTypeIDParent-> " + addressIDParent.ToString());

                //We must get the resource manager after setting the culture.
                rm = SingletonResourcesManager.Instance.getResourceManager();

                //For traces   
                logger.Debug(location + "Before line: InitializeComponent();");

                //Initializes all the components of the form
                InitializeComponent();

                this.Text = title;

                btecDB = btecDBParent;
                addressID = addressIDParent;

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
        private void frmAddressEditAddress_Load(object sender, EventArgs e)
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
                logger.Debug(location + "Before line: AddressDefinition ad = new AddressDefinition(btecDB);");
                logger.Debug(location + "        and: AddressDefinitionData adData = ad.GetAddressInfo(strWorkTypeID);");

                AddressDefinition ad = new AddressDefinition(btecDB);
                AddressDefinitionData adData = ad.GetAddressInfo(addressID);

                string name = null;
                string line1 = null;
                string line2 = null;
                string city = null;
                string state = null;
                string zipCode = null;

                if (adData != null)
                {
                    name = adData.GetStringValue(adData.NAME);
                    line1 = adData.GetStringValue(adData.ADDRESS_LINE1);
                    line2 = adData.GetStringValue(adData.ADDRESS_LINE2);
                    city = adData.GetStringValue(adData.CITY);
                    state = adData.GetStringValue(adData.PROV_STATE);
                    zipCode = adData.GetStringValue(adData.POSTAL_CODE); 
                }

                //For traces
                logger.Debug(location + "Values:");
                logger.Debug(location + "  strWorkTypeID-> " + addressID.ToString());
                logger.Debug(location + "  name-> "      + name);
                logger.Debug(location + "  line1-> "     + line1);
                logger.Debug(location + "  line2-> "     + line2);
                logger.Debug(location + "  city-> "      + city);
                logger.Debug(location + "  state-> "     + state);
                logger.Debug(location + "  zipCode-> "   + zipCode);

                labelAddressID.Text = addressID.ToString();
                txtName.Text = name;
                txtLine1.Text = line1;
                txtLine2.Text = line2;
                txtCity.Text = city;
                txtState.Text = state;
                txtZipCode.Text = zipCode;

                //To detect a change in the form (next 6 lines)
                txtName.TextChanged    += new EventHandler(this.somethingChanged);
                txtLine1.TextChanged   += new EventHandler(this.somethingChanged);
                txtLine2.TextChanged   += new EventHandler(this.somethingChanged);
                txtCity.TextChanged    += new EventHandler(this.somethingChanged);
                txtState.TextChanged   += new EventHandler(this.somethingChanged);
                txtZipCode.TextChanged += new EventHandler(this.somethingChanged);

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

        //occurs when any field is modified
        private void somethingChanged(object sender, EventArgs e) 
        {
            isSomethingChanged = true;
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

                if (!string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    //For traces   
                    logger.Debug(location + " After line: if (!string.IsNullOrEmpty(txtName.Text.Trim()))");

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

                        switch (result)
                        {
                            case DialogResult.OK:
                                string buffer;

                                buffer = txtName.Text.Trim();
                                string name = buffer.Substring(0, Math.Min(buffer.Length, 60));

                                buffer = txtLine1.Text.Trim();
                                string line1 = buffer.Substring(0, Math.Min(buffer.Length, 30));

                                buffer = txtLine2.Text.Trim();
                                string line2 = buffer.Substring(0, Math.Min(buffer.Length, 30));

                                buffer = txtCity.Text.Trim();
                                string city = buffer.Substring(0, Math.Min(buffer.Length, 20));

                                buffer = txtState.Text.Trim();
                                string state = buffer.Substring(0, Math.Min(buffer.Length, 20));

                                buffer = txtZipCode.Text.Trim();
                                string zipCode = buffer.Substring(0, Math.Min(buffer.Length, 10));

                                AddressDefinition ad = new AddressDefinition(btecDB);

                                //For traces
                                logger.Debug(location + "Values:");
                                logger.Debug(location + "  strWorkTypeID-> " + addressID.ToString());
                                logger.Debug(location + "  name-> " + name);
                                logger.Debug(location + "  line1-> " + line1);
                                logger.Debug(location + "  line2-> " + line2);
                                logger.Debug(location + "  city-> " + city);
                                logger.Debug(location + "  state-> " + state);
                                logger.Debug(location + "  zipCode-> " + zipCode);

                                //For traces   
                                logger.Debug(location + "Before line: ad.SaveSimpleAddress(strWorkTypeID, name, line1, line2, city, state, zipCode);");

                                //save the address
                                ad.SaveSimpleAddress(addressID,
                                                     name,
                                                     line1,
                                                     line2,
                                                     city,
                                                     state,
                                                     zipCode);

                                isFormClosedByUser = true;

                                //For the parent form knows that there was a change in the child data
                                this.DialogResult = DialogResult.OK;
                                this.Close();
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
                }
                else
                {
                    //Set the focus to the Name field
                    txtName.Focus();

                    MessageBox.Show(rm.GetString("msgNameAddress"),
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

        //Occurs when the form is closed
        private void frmAddressEditAddress_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}
