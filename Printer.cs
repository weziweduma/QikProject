using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qik
{
    /// <summary>
    /// This class prints the specified text for the user.
    /// </summary>
    public class Printer
    {

        public Printer()
        {

        }

        ~Printer()
        {

        }

        /// <summary>
        /// Prints the text string for the user.
        /// </summary>
        /// <param name="PrintText"></param>
        public bool print(string PrintText)
        {
            Console.Write(PrintText);
            return true;
        }

    }//end Printer
}
