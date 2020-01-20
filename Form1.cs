using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VacationCalculator
{
    public partial class Form1 : Form
    {
        private Dictionary<int, string> floatDateEaster = new Dictionary<int, string>();
        private Dictionary<int, string> floatDateTrinity = new Dictionary<int, string>();

        private List<string> blackoutDays = new List<string>() 
        { "01/01/", "07/01/", "08/03/", "01/05/", "02/05/", "09/05/", "28/06/", "24/08/", "14/10/", "25/12/" };

        public Form1()
        {
            DictionaryAddAsync();
            InitializeComponent();
        }

        #region ControlFocus KeyDown

        private void dateTimeStart_PressEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dateTimeEnd.Focus();
        }

        private void dateTimeEnd_PressEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                numericUpDown1.Focus();
        }

        private void numeric_PressEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dateTimeStart.Focus();
        }

        #endregion

        #region ValueChanged event

        private async void dateTimeStart_ValueChanged(object sender, EventArgs e)
        {
            double result = await Task.Run(() => CalcResult());
            resultTextBox.Text = result.ToString();
        }

        private async void dateTimeEnd_ValueChanged(object sender, EventArgs e)
        {
            double result = await Task.Run(() => CalcResult());
            resultTextBox.Text = result.ToString();
        }

        private async void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double result = await Task.Run(() => CalcResult());
            resultTextBox.Text = result.ToString();
        }

        #endregion

        private double CalcResult()
        {
            // Вираховує кількість святкових днів (т.з. blackoutDays) в обраному періоді
            // Та за спец. формулою розраховує кількість днів відпустки, доступних працівнику в обраний період.

            int blackoutDaysCount = 0; 
            DateTime dateStart = dateTimeStart.Value.Date;
            DateTime dateEnd = dateTimeEnd.Value.Date;
            var daysCount = (dateEnd - dateStart).Days + 1;
            double numberDays = (int)numericUpDown1.Value;

            for (int year = dateStart.Year; year < dateEnd.Year + 1; year++)
            {
                foreach (string day in blackoutDays)
                {
                    DateTime dateToCheck = DateTime.Parse(day + year);

                    if (dateToCheck.IsInRange(dateStart, dateEnd))
                    {
                        if (year < 2016 && day == "14/10/")
                            continue;
                        if (year < 2017 && day == "25/12/")
                            continue;
                        if (year >= 2018 && day == "02/05/")
                            continue;

                        blackoutDaysCount++;
                    }
                }
            }

            for (int year = dateStart.Year; year < dateEnd.Year + 1; year++)
            {
                DateTime dateToCheckEaster = DateTime.Parse(floatDateEaster[year] + year);
                DateTime dateToCheckTrinity = DateTime.Parse(floatDateTrinity[year] + year);

                if (dateToCheckEaster.IsInRange(dateStart, dateEnd))
                    blackoutDaysCount++;
                if (dateToCheckTrinity.IsInRange(dateStart, dateEnd))
                    blackoutDaysCount++;
            }

            double result = numberDays / (365 - 11) * (daysCount - blackoutDaysCount);

            return Math.Round(result, 2);
        }

        private async Task DictionaryAddAsync()
        {
            // Ініціалізація коллекцій свят з плаваючими датами (Пасха та Трійця)
            // в заданому замовником діапазоні (з 2000 по 2028 роки включно)

            await Task.Run(() =>
            {
                #region Trinity
                Task.Run(() =>
                {
                    floatDateTrinity.Add(2000, "18/06/");
                    floatDateTrinity.Add(2001, "03/06/");
                    floatDateTrinity.Add(2002, "23/06/");
                    floatDateTrinity.Add(2003, "15/06/");
                    floatDateTrinity.Add(2004, "30/05/");
                    floatDateTrinity.Add(2005, "19/06/");
                    floatDateTrinity.Add(2006, "11/06/");
                    floatDateTrinity.Add(2007, "27/05/");
                    floatDateTrinity.Add(2008, "15/06/");
                    floatDateTrinity.Add(2009, "07/06/");
                    floatDateTrinity.Add(2010, "23/05/");
                    floatDateTrinity.Add(2011, "12/06/");
                    floatDateTrinity.Add(2012, "03/06/");
                    floatDateTrinity.Add(2013, "23/06/");
                    floatDateTrinity.Add(2014, "08/06/");
                    floatDateTrinity.Add(2015, "31/05/");
                    floatDateTrinity.Add(2016, "19/06/");
                    floatDateTrinity.Add(2017, "04/06/");
                    floatDateTrinity.Add(2018, "27/05/");
                    floatDateTrinity.Add(2019, "16/06/");
                    floatDateTrinity.Add(2020, "07/06/");
                    floatDateTrinity.Add(2021, "20/06/");
                    floatDateTrinity.Add(2022, "12/06/");
                    floatDateTrinity.Add(2023, "04/06/");
                    floatDateTrinity.Add(2024, "23/06/");
                    floatDateTrinity.Add(2025, "08/06/");
                    floatDateTrinity.Add(2026, "31/05/");
                    floatDateTrinity.Add(2027, "20/06/");
                    floatDateTrinity.Add(2028, "04/06/");
                });
                #endregion
                #region Easter
                floatDateEaster.Add(2000, "30/04/");
                floatDateEaster.Add(2001, "15/04/");
                floatDateEaster.Add(2002, "05/05/");
                floatDateEaster.Add(2003, "27/04/");
                floatDateEaster.Add(2004, "11/04/");
                floatDateEaster.Add(2005, "01/05/");
                floatDateEaster.Add(2006, "23/04/");
                floatDateEaster.Add(2007, "08/04/");
                floatDateEaster.Add(2008, "27/04/");
                floatDateEaster.Add(2009, "19/04/");
                floatDateEaster.Add(2010, "04/04/");
                floatDateEaster.Add(2011, "24/04/");
                floatDateEaster.Add(2012, "15/04/");
                floatDateEaster.Add(2013, "05/05/");
                floatDateEaster.Add(2014, "20/04/");
                floatDateEaster.Add(2015, "12/04/");
                floatDateEaster.Add(2016, "01/05/");
                floatDateEaster.Add(2017, "16/04/");
                floatDateEaster.Add(2018, "08/04/");
                floatDateEaster.Add(2019, "28/04/");
                floatDateEaster.Add(2020, "19/04/");
                floatDateEaster.Add(2021, "02/05/");
                floatDateEaster.Add(2022, "24/04/");
                floatDateEaster.Add(2023, "16/04/");
                floatDateEaster.Add(2024, "05/05/");
                floatDateEaster.Add(2025, "20/04/");
                floatDateEaster.Add(2026, "12/04/");
                floatDateEaster.Add(2027, "02/05/");
                floatDateEaster.Add(2028, "16/04/");
                #endregion
            });
        }
    }

    static class DateTimeExtension
    {
        // Перевіряє чи входить дата, на якій викликано метод, в діапазон beginDate-endDate включно

        public static bool IsInRange(this DateTime currentDate, DateTime beginDate, DateTime endDate)
        {
            return (currentDate >= beginDate && currentDate <= endDate);
        }
    }
}
