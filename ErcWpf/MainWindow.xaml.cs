using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RedAlliance.Erc
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string GetFullBinary(string str, int count)
        {
            while (str.Length < count)
            {
                str = "0" + str;
            }
            return str;
        }

        private void AddBitsRow(int num)
        {
            _pntBits.Children.Add(new TextBlock() { Text = GetFullBinary(Convert.ToString(num, 2), 32) + " " + GetFullBinary(num.ToString("X"), 8) });
        }

        private uint GetReverse(uint x)
        {
            uint reverseSecondPart = 0;

            for (int i = 0; i < 32; i++)
            {
                uint temp = x >> (31 - i);
                temp &= 1;
                temp = temp << i;
                reverseSecondPart |= temp;
            }

            return reverseSecondPart;
        }

        private void _btnCheck_Click(object sender, RoutedEventArgs e)
        {
            // var parts = _tbErc.Text.Split('-');
            // string erc = parts.First().Trim();
            // string code = parts.Last().Trim();
            // if (erc.Length != 16 || code.Length != 8) return;

            // uint firstPart = Convert.ToUInt32(erc.Substring(0, 8), 16);
            // uint secondPart = Convert.ToUInt32(erc.Substring(8), 16);
            // uint codeBin = Convert.ToUInt32(code, 16);

            // uint reverseSecondPart = GetReverse(secondPart);

            // AddBitsRow(firstPart);
            // AddBitsRow(reverseSecondPart);
            // int xor = firstPart ^ reverseSecondPart;
            // int result = xor - 0xE010A11;
            // AddBitsRow(xor);
            // AddBitsRow(result);
            // AddBitsRow(codeBin);
            // _pntBits.Children.Add(new TextBlock());

            return;
        }

        private void _btnEncode_Click(object sender, RoutedEventArgs e)
        {
            _tbOutputEnc.Text = "";
            string erc = _tbErcEnc.Text.Replace("-", "").Replace(" ", "").Trim();
            if (erc.Length != 16) return;

            uint firstPart = Convert.ToUInt32(erc.Substring(0, 8), 16);
            uint secondPart = Convert.ToUInt32(erc.Substring(8), 16);
            uint reverseSecondPart = GetReverse(secondPart);

            uint xor = firstPart ^ reverseSecondPart;
            uint ret = xor - 0xE010A11;
            _tbOutputEnc.Text = GetFullBinary(ret.ToString("X"), 8);

            return;
        }
    }
}
