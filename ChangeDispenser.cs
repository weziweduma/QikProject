using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms; //remove after debugging
namespace Qik
{

    /// <summary>
    /// This class dispenses the change the user is supposed to receive when the money
    /// inserted is more than the money corresponding to the selected charging time.
    /// </summary>
    public class ChangeDispenser
    {
        /// <summary>
        /// ChangeDispenserEventArgs class which contains that reports despensing result to user
        /// <param name="Result">Result for dispensing<param"/>
        /// </summary>
        public class ChangeDispenserEventArgs : EventArgs
        {
            public e_ChangeDispenserResult Result { get; set; }
        }
        /// <summary>
        /// Event handler delegates for the ChangeDispenserEvent event 
        /// </summary>
        public event EventHandler<ChangeDispenserEventArgs> ChangeDispenserEvent;

        public enum e_ChangeDispenserResult
        {
           SUCCESSFULL, FAILED
        }
        public ChangeDispenser()
        {

        }

        ~ChangeDispenser()
        {

        }

        /// <summary>
        /// Dispenses the change for the user.
        /// </summary>
        /// <param name="change"></param>
        public bool dispenseChange(int change)
        {
            DateTime start = DateTime.Now;
            while ((DateTime.Now - start).TotalMilliseconds < 2000)
            Application.DoEvents();
            DispenseDone();//shouldn't be called here.
            return true;
        }
        /// <summary>
        /// Reports dispensing result
        /// </summary>
        public void DispenseDone()
        {
            ChangeDispenserEventArgs ChangeDispenserEventArg = new ChangeDispenserEventArgs();
            ChangeDispenserEventArg.Result = e_ChangeDispenserResult.SUCCESSFULL;
            EventHandler<ChangeDispenserEventArgs> ChangeDispenserHandler = ChangeDispenserEvent;
            if (ChangeDispenserHandler != null)
            {
                ChangeDispenserHandler(this, ChangeDispenserEventArg);
            }
        }

    }//end ChangeDispenser
}
