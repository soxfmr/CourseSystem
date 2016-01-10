using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigurationEditor
{
    public class CommonHelper
    {
        public static bool isEmpty(params string[] input)
        {
            foreach (var str in input)
            {
                if (string.IsNullOrEmpty(str))
                    return true;
            }

            return false;
        }

        public static bool inRange(string num, int min, int max)
        {
            int val = 0;
            try
            {
                val = Int32.Parse(num);

                return inRange(val, min, max);
            } catch (Exception)
            {
                return false;
            }
        }

        public static bool inRange(int num, int min, int max)
        {
            return num >= min && num <= max;
        }

        public static int StrToInt(string str)
        {
            int val = -1;
            try
            {
                val = Int32.Parse(str);
            }
            catch (Exception) {}

            return val;
        }

        public static void ShowMessage(IWin32Window owner, string message)
        {
            MessageBox.Show(owner, message, "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
