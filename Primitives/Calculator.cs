using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    class Calculator
    {
        public static double getCoordinate(char name, string str)
        {
            int index = 0;
            double result = 0;

            switch (name)
            {
                case 'X':
                    index = str.IndexOf(';');
                    str = str.Substring(0, index);
                    result = Convert.ToDouble(str);
                    break;
                case 'Y':
                    index = str.IndexOf(';');
                    str = str.Substring(index + 1);
                    index = str.IndexOf(';');
                    str = str.Substring(0, index);
                    result = Convert.ToDouble(str);
                    break;
                case 'Z':
                    index = str.IndexOf(';');
                    str = str.Substring(index + 1);
                    index = str.IndexOf(';');
                    str = str.Substring(index + 1);
                    result = Convert.ToDouble(str);
                    break;
            }
            return result;
        }
    }
}
