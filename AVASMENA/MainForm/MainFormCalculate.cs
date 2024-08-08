using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel;

namespace AVASMENA
{
    public partial class MainForm
    {
        private (int SeyfExcel, int nalf, int bnf, int nalp, int bnp, int seyf, int minus, int zp, int haurs, int zp4, int seyfEnd, int viruchka, int itog, int zarp1, int zarp2, int zarp3, string name, string name2, string name3, int minus1, int minus2, int minus3, int minusSeyf, int minusNoSeyf)
    GetValues()
        {
            int SeyfExcel = ExcelHelper.GetCellValueAsInt("s", "seyf");
            string name = NameComboBoxOtchet.Text;
            string name2 = SecondComboBoxNameOtchet.Text;
            string name3 = ThertyComboBox.Text;

            int.TryParse(textBox2.Text, out int nalf);
            int.TryParse(textBox3.Text, out int bnf);
            int.TryParse(textBox4.Text, out int nalp);
            int.TryParse(textBox5.Text, out int bnp);
            int.TryParse(textBox7.Text, out int seyf);
            int.TryParse(Minus1.Text, out int minus1);
            int.TryParse(Minus2.Text, out int minus2);
            int.TryParse(Minus3.Text, out int minus3);
            int.TryParse(HaursBox1.Text, out int hours1);
            int.TryParse(HaursBox2.Text, out int hours2);
            int.TryParse(HaursBox3.Text, out int hours3);

            var minusSeyf = CalculatCheackSeyf(SeyfExcel, seyf);

            int minusNoSeyf = CalculateMinus(nalf, nalp, bnf, bnp);
            int minus = minusNoSeyf + minusSeyf;
            int zp = 167;
            int haurs = hours1 + hours2 + hours3;
            int zp4 = zp * haurs + (minus > 0 ? 0 : minus);

            int seyfEnd = seyf + nalf - 1000;
            int viruchka = nalf + bnf - 1000;
            int itog = viruchka - zp4;
            int zarp1 = CalculateZarp(hours1, zp, minus1);
            if (!Minus1.Visible)
            {
                int minus11 = minus * -1;
                zarp1 = CalculateZarp(hours1, zp, minus11);
            }
            int zarp2 = CalculateZarp(hours2, zp, minus2);
            int zarp3 = CalculateZarp(hours3, zp, minus3);

            return (SeyfExcel, nalf, bnf, nalp, bnp, seyf, minus, zp, haurs, zp4, seyfEnd, viruchka, itog, zarp1, zarp2, zarp3, name, name2, name3, minus1, minus2, minus3, minusSeyf, minusNoSeyf);
        }

        private int CalculatCheackSeyf(int SeyfExcel, int seyf)
        {
            var value = seyf - SeyfExcel;
            if (value < -20)
            {
                return value;
            }
            return 0;
        }

        private int CalculateMinus(int nalf, int nalp, int bnf, int bnp)
        {
            int minus = (nalf - nalp) + (bnf - bnp);

            return minus > 0 ? 0 : minus;
        }
        private int CalculateZarp(int hours, int zp, int minus)
        {
            return zp * hours - minus;
        }
        private int CalculateShtraph(int syu)
        {
            if (syu > 0)
                return syu *= -1;
            return syu;
        }

    }
}
