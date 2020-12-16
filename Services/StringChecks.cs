using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Melembre.Source.Services
{
    public static class StringChecks
    {
        public static bool stringIsSpaces(string text)
        {
            bool is_spaces = false;

            if (text.Length < 1)
                return true;
            else
            {
                foreach (char ch in text)
                {
                    if (ch == ' ')
                    {
                        is_spaces = true;
                    }
                    else
                    {
                        is_spaces = false;
                        return is_spaces;
                    }
                }
            }
            
            return is_spaces;
        }
    }
}
