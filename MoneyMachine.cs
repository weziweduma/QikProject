using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms; //remove after debugging
namespace Qik
{
    /// <summary>
    /// This class processes the money inserted by the user.
    /// </summary>
    public class MoneyMachine
    {
        /// <summary>
        /// MoneyMachineEventArgs class which contains the Money collected from the user
        /// <param name="Money">The user money</param>
        /// </summary>
        public class MoneyMachineEventArgs : EventArgs
        {
            public int Money { get; set; }
        }
        /// <summary>
        /// Event handler delegates for the SlotEvent event 
        /// </summary>
        public event EventHandler<MoneyMachineEventArgs> MoneyMachineEvent;
        private int Money { get; set; }
        public MoneyMachine()
        {

        }

        ~MoneyMachine()
        {

        }

        /// <summary>
        /// Checks if money is inserted and calculates how much has been inserted. Returns
        /// success or fail.
        /// </summary>
        /// <param name="Money"></param>
        public int getMoney(int Money)
        {
            DateTime start = DateTime.Now;
            while ((DateTime.Now - start).TotalMilliseconds < 2000)
            Application.DoEvents();
            this.Money = Money;//forced vvalue
            ReportMoney();//shouldn't be called here.
            return 0;
        }
        /// <summary>
        /// Report money obtained from user
        /// </summary>
        public void ReportMoney()
        {
            MoneyMachineEventArgs MoneyMachineEventArg = new MoneyMachineEventArgs();
            MoneyMachineEventArg.Money = this.Money;
            EventHandler<MoneyMachineEventArgs> MoneyMachineHandler = MoneyMachineEvent;
            if (MoneyMachineHandler != null)
            {
                MoneyMachineHandler(this, MoneyMachineEventArg);
            }
        }

    }//end MoneyMachine
}
