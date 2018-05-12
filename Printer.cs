using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms; //remove after debugging
namespace Qik
{
    /// <summary>
    /// This class prints the specified text for the user.
    /// </summary>
    public class Printer
    {
        /// <summary>
        /// PrinterEventArgs class which contains that reports despensing result to user
        /// <param name="Result">Result for printing<param"/>
        /// </summary>
        public class PrinterEventArgs : EventArgs
        {
            public e_PrinterResult Result { get; set; }
        }
        /// <summary>
        /// Event handler delegates for the PrinterEvent event 
        /// </summary>
        public event EventHandler<PrinterEventArgs> PrinterEvent;

        public enum e_PrinterResult
        {
            SUCCESSFULL, FAILED
        }
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
            DateTime start = DateTime.Now;
            while ((DateTime.Now - start).TotalMilliseconds < 2000)
                Application.DoEvents();
            Console.Write(PrintText);
            PrintDone();//shouldn't be called here.
            return true;
        }
        /// <summary>
        /// Reports dispensing result
        /// </summary>
        public void PrintDone()
        {
            PrinterEventArgs PrinterEventArg = new PrinterEventArgs();
            PrinterEventArg.Result = e_PrinterResult.SUCCESSFULL;
            EventHandler<PrinterEventArgs> PrinterrHandler = PrinterEvent;
            if (PrinterrHandler != null)
            {
                PrinterrHandler(this, PrinterEventArg);
            }
        }
    }//end Printer
}
