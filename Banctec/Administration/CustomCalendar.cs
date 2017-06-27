//2013-05 Simon Boutin MANTIS# 18750 : Convert LBTables from VB to C#

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;
using BancTec.PCR2P.Core.BusinessLogic.Administration;

namespace Administration
{
    public class CustomCalendar
    {
        public TableLayoutPanel calendar;

        const int NB_ROWS = 6;
        const int NB_COLUMNS = 7;
        Color COLOR_NORMAL_DAY = Color.White;
        Color COLOR_SPECIAL_DAY = SystemColors.Control;
        Color COLOR_SELECTION = Color.Black;

        ICalendar parentForm;

        int selectedYear;
        int selectedMonth;
        int selectedDay;   

        Label[] labelWeekDay = new Label[NB_COLUMNS];

        TableLayoutPanel[,] layoutPanelDay = new TableLayoutPanel[NB_COLUMNS, NB_ROWS];
        Panel[,] panelDay = new Panel[NB_COLUMNS, NB_ROWS];
        Label[,] labelDay = new Label[NB_COLUMNS, NB_ROWS];

        Label oldClickedLabel = new Label();

        ResourceManager rm;

        public CustomCalendar(ICalendar _parentForm)
        {
            //We must get the resource manager after setting the culture.
            rm = SingletonResourcesManager.Instance.getResourceManager();

            calendar = new TableLayoutPanel();
            parentForm = _parentForm;

            createMatrix();
            createControls();
            clean();
        }

        //Gets the selected day 
        public int getDay()
        {
            return selectedDay;
        }

        //Gets the selected month 
        public int getMonth()
        {
            return selectedMonth;
        }

        //Gets the selected year 
        public int getYear()
        {
            return selectedYear;
        }

        //Creates the matrix frame of the calendar
        void createMatrix()
        {
            calendar.RowCount = NB_ROWS + 2;
            calendar.ColumnCount = NB_COLUMNS + 1;

            calendar.RowStyles.Add(new RowStyle(SizeType.Percent, 10.00F));

            for (int i = 1; i < calendar.RowCount - 1; i++)
            {
                calendar.RowStyles.Add(new RowStyle(SizeType.Percent, 14.83F));
            }

            //Add en empty row because of a lost of precision in the display
            calendar.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));

            for (int j = 0; j < calendar.ColumnCount - 1; j++)
            {
                calendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28F));
            }

            //Add en empty column because 100/7 have 4 decimals precision but the column precision is 2 decimals
            calendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.04F));

            calendar.Dock = DockStyle.Fill;
        }

        //Creates panels, labels and click events on each day
        public void createControls()
        {
            string allWeekDays = rm.GetString("WeekDaysShort");
            string[] weekDay = allWeekDays.Split(':'); //must have NB_COLUMNS elements

            //creates controls on each days of the calendar
            for (int i = 0; i < NB_COLUMNS; i++)
            {
                labelWeekDay[i] = new Label();
                labelWeekDay[i].BackColor = Color.Transparent;
                labelWeekDay[i].TextAlign = ContentAlignment.MiddleCenter;
                labelWeekDay[i].Dock = DockStyle.Fill;
                labelWeekDay[i].Text = weekDay[i];

                calendar.Controls.Add(labelWeekDay[i], i, 0);

                for (int j = 0; j < NB_ROWS; j++)
                {
                    labelDay[i, j] = new Label();
                    labelDay[i, j].Margin = new Padding(3, 3, 3, 3);
                    labelDay[i, j].BackColor = COLOR_SPECIAL_DAY;
                    labelDay[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    labelDay[i, j].Click += new EventHandler(this.dayClicked);
                    labelDay[i, j].DoubleClick += new EventHandler(this.dayClicked);
                    labelDay[i, j].Dock = DockStyle.Fill;

                    layoutPanelDay[i, j] = new TableLayoutPanel();
                    layoutPanelDay[i, j].RowCount = 1;
                    layoutPanelDay[i, j].ColumnCount = 1;
                    layoutPanelDay[i, j].RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                    layoutPanelDay[i, j].ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                    layoutPanelDay[i, j].Dock = DockStyle.Fill;

                    panelDay[i, j] = new Panel();
                    panelDay[i, j].BackColor = COLOR_SPECIAL_DAY;
                    panelDay[i, j].BorderStyle = BorderStyle.Fixed3D;
                    panelDay[i, j].Dock = DockStyle.Fill;

                    layoutPanelDay[i, j].Controls.Add(labelDay[i, j]);
                    panelDay[i, j].Controls.Add(layoutPanelDay[i, j]);
                    calendar.Controls.Add(panelDay[i, j], i, j + 1);
                }
            }

            oldClickedLabel = labelDay[NB_COLUMNS - 1, NB_ROWS - 1];
        }

        //Cleans the calendar
        public void clean()
        {
            for (int j = 0; j < NB_ROWS; j++)
            {
                for (int i = 0; i < NB_COLUMNS; i++)
                {
                    labelDay[i, j].Text = string.Empty;
                    labelDay[i, j].BackColor = COLOR_NORMAL_DAY;

                    panelDay[i, j].BackColor = COLOR_NORMAL_DAY;
                    panelDay[i, j].Visible = false;
                }
            }

            selectedDay = 0;
        }

        //Initialize the calendar for a specific year and month
        public void initializeMonth(int year, int month)
        {
            selectedYear = year;
            selectedMonth = month;

            int startColumn = getWeekDayNb(year, month, 1);
            int nbDays = getNbDaysInMonth(year, month);

            Point coord = new Point(startColumn, 0);
            int day = 1;
            
            do
            {
                if (coord.X > NB_COLUMNS - 1)
                {
                    coord.Y++;
                    coord.X = 0;
                }

                labelDay[coord.X, coord.Y].Text = day.ToString();
                panelDay[coord.X, coord.Y].Visible = true;

                coord.X++;
                day++;
            }
            while (day <= nbDays);
        }

        //Returns the week day number (0 to 6) for a year, month and day
        public int getWeekDayNb(int year, int month, int day)
        {
            DateTime dateValue = new DateTime(year, month, day);
            int weekDay = (int)dateValue.DayOfWeek;

            return weekDay;
        }

        //Returns the number of days for a specific year and month
        public int getNbDaysInMonth(int year, int month)
        {
            int nbDays = DateTime.DaysInMonth(year, month);

            return nbDays;
        }

        //Returns the calendar coordinates for a specific clicked label (day)
        public Point getCoordCalendar(Label myLabel)
        {
            Point coord = new Point(-1, -1);

            for (int i = 0; i < NB_COLUMNS; i++)
            {
                for (int j = 0; j < NB_ROWS; j++)
                {
                    if (labelDay[i, j] == myLabel)
                    {
                        coord.X = i;
                        coord.Y = j;

                        return coord;
                    }
                }
            }

            return coord;
        }

        //Returns the calendar coordinates for a specific year, month and day
        public Point getCoordCalendar(int year, int month, int nbDays)
        {
            DateTime date;

            if (DateTime.TryParse(string.Format("{0}-{1}-{2}", year, month, nbDays), out date))
            {
                int startColumn = getWeekDayNb(year, month, 1);
                Point coord = new Point(startColumn, 0);
                int day = 1;

                while (day < nbDays)
                {
                    coord.X++;
                    day++;

                    if (coord.X > NB_COLUMNS - 1)
                    {
                        coord.Y++;
                        coord.X = 0;
                    }
                }

                return coord;
            }
            else
            {
                Point coord = new Point(-1, -1); 
                
                return coord;
            }
        }

        //Occurs when a day is clicked
        private void dayClicked(object sender, EventArgs e)
        {
            Label newClickedLabel = (Label)sender;

            int day = int.Parse(newClickedLabel.Text);

            selectDay(day);

            parentForm.updateDay(day);
        }

        //Select a day in the calendar for a specific day
        public bool? selectDay(int newDay)
        {
            Point oldCoord = getCoordCalendar(oldClickedLabel);
            Point newCoord = getCoordCalendar(selectedYear, selectedMonth, newDay);

            if (newCoord.X == -1) return null;

            Label newClickedLabel = labelDay[newCoord.X, newCoord.Y];

            panelDay[oldCoord.X, oldCoord.Y].BackColor = oldClickedLabel.BackColor;
            panelDay[newCoord.X, newCoord.Y].BackColor = COLOR_SELECTION;

            oldClickedLabel = newClickedLabel;
            selectedDay = newDay;

            return true;
        }

        //Set if a specific day is a normal day or not
        public bool? setNormalDay(int newDay, bool isNormalDay)
        {
            Point newCoord = getCoordCalendar(selectedYear, selectedMonth, newDay);

            if (newCoord.X == -1) return null;

            Color panelBackColor = panelDay[newCoord.X, newCoord.Y].BackColor;

            if (isNormalDay)
            {
                labelDay[newCoord.X, newCoord.Y].BackColor = COLOR_NORMAL_DAY;
                panelDay[newCoord.X, newCoord.Y].BackColor = (panelBackColor == COLOR_SELECTION ? COLOR_SELECTION : COLOR_NORMAL_DAY);
            }
            else
            {
                labelDay[newCoord.X, newCoord.Y].BackColor = COLOR_SPECIAL_DAY;
                panelDay[newCoord.X, newCoord.Y].BackColor = (panelBackColor == COLOR_SELECTION ? COLOR_SELECTION : COLOR_SPECIAL_DAY);
            }

            return true;
        }

        //Returns if a specific day is a normal day or not
        public bool? isNormalDay(int newDay)
        {
            Point newCoord = getCoordCalendar(selectedYear, selectedMonth, newDay);

            if (newCoord.X == -1) return null;

            if (labelDay[newCoord.X, newCoord.Y].BackColor == COLOR_NORMAL_DAY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
