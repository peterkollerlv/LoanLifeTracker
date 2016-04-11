using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace InterestTracker
{
    static class FormatDigitInput
    {
        public static void FilterKeypressToDigits(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) && e.Key != Key.Back || (char)KeyInterop.VirtualKeyFromKey(e.Key) != '.' && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                e.Handled = true;
            }
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

        public static decimal FormatToDecimal(object d)
        {
            try
            {
                d = decimal.Round(Convert.ToDecimal(d), 2, MidpointRounding.AwayFromZero);
                return (decimal)d;
            }
            catch
            {
                return 0;
            }
        }
    }
}
