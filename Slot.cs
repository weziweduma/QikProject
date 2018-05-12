using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qik
{
    /// <summary>
    /// This class keeps track of the states for the charging slots and handles the
    /// call events from the VendingMachine class
    /// </summary>
    public class Slot
    {
        /// <summary>
        /// SlotEventArgs class which contains the ID of the slot and Status
        /// <param name="ID">The slot ID</param>
        /// <param name="ID">The slot status</param>
        /// </summary>
        public class SlotEventArgs : EventArgs
        {
            public int ID { get; set; }
            public e_Status Status { get; set; }
        }
        /// <summary>
        /// Event handler delegates for the SlotEvent event 
        /// </summary>
        public event EventHandler<SlotEventArgs> SlotEvent;
        /// <summary>
        /// Current status: CHARGING, FINISHED_OCCUPIED, AVAILABLE
        /// </summary>
        public enum e_Status
        {
            AVAILABLE, OCCUPIED_CHARGING, OCCUPIED_FINISHED, OCCUPIED_OVERDUE
        }
        /// <summary>
        /// The amount of time to charge for
        /// </summary>
        private int ChargingTime;
        /// <summary>
        /// The remaining amount of time to charge for
        /// </summary>
        private int RemainingChargingTime;
        /// <summary>
        /// Slot identifier
        /// </summary>
        private int ID;
        /// <summary>
        /// Password for retrieving device from slot
        /// </summary>
        private int Password;
        /// <summary>
        /// Current status: AVAILABLE, OCCUPIED_CHARGING, OCCUPIED_FINISHED
        /// </summary>
        private e_Status Status;
        /// <summary>
        /// The elapsed charging time timer
        /// </summary>
    //    private Timer Timer;
        static System.Timers.Timer timer;
        public Charger m_Charger;

        public Slot( int Id)
        {
            ID = Id;
            m_Charger = new Charger();
            timer = new System.Timers.Timer();
            timer.Elapsed += timeElapsed;
            
        }

        ~Slot()
        {

        }

        /// <summary>
        /// close the slot door. (Current not implemented as it is not possible through the
        /// software.
        /// </summary>
        public int close()
        {

            return 0;
        }

        /// <summary>
        /// Retrieve the remaining charging time for the slot
        /// </summary>
        /// <param name="ChargeTime"></param>
        public int getChargeTime()
        {

            return RemainingChargingTime;
        }

        /// <summary>
        /// Retrieve the Slot Number for this slot
        /// </summary>
        public int getID()
        {
            return ID;
        }

        /// <summary>
        /// Retrieve the password for the slot for the current user.
        /// </summary>
        public int getPassword()
        {

            return Password;
        }

        /// <summary>
        /// Retrive the the status of this slot.
        /// </summary>
        public e_Status getStatus()
        {

            return Status;
        }

        /// <summary>
        /// Open the Slot for the user to insert or collect the device
        /// </summary>
        public int open()
        {
            //open slot
            return 0;
        }

        /// <summary>
        /// Resets Slot attributes to default values
        /// </summary>
        public void reset()
        {
            ChargingTime = 0;
            Status = e_Status.AVAILABLE;
            Password = 0;
        }

        /// <summary>
        /// Set the charge time for the slot
        /// </summary>
        public bool setChargeTime(int ChargeTime)
        {
            this.ChargingTime = ChargeTime;
            return true;
        }

        /// <summary>
        /// Set the password for the Slot for the current user
        /// </summary>
        /// <param name="Password"></param>
        public bool setPassword(int Password)
        {
            this.Password = Password;
            return true;
        }

        /// <summary>
        /// Set the status of the slot: CHARGING, AVAILABLE etc.
        /// </summary>
        /// <param name="Status"></param>
        public int setStatus(e_Status Status)
        {
            this.Status = Status;
            return 0;
        }
        /// <summary>
        /// Start the charging
        /// </summary>
        public int start()
        {
            RemainingChargingTime = ChargingTime;
            Status = e_Status.OCCUPIED_CHARGING;
            m_Charger.startCharger(this.ID);
            System.Console.WriteLine("Started Slot " + ID);
            return 0;
        }
        /// <summary>
        /// Start the charging timer
        /// </summary>
        public int startTimer()
        {
            timer.Interval = 60 * 1000; //1 minute
            timer.Enabled = true;
            timer.Start();
            return 0;
        }
        /// <summary>
        /// Stop the charging
        /// </summary>
        public int stop()
        {
            m_Charger.stopCharger(this.ID);
            return 0;
        }
        /// <summary>
        /// Stop the charging timer.
        /// </summary>
        public int stopTimer()
        {
            RemainingChargingTime = 0;
            timer.Enabled = false;
            timer.Stop();
            return 0;
        }
        public void timeElapsed(object sender, EventArgs e)
        {
            SlotEventArgs SlotEventArg = new SlotEventArgs();
            SlotEventArg.ID = this.ID;
            EventHandler<SlotEventArgs> SlotEventHandler = SlotEvent;
            RemainingChargingTime -= 1;
            if (RemainingChargingTime == 0)
            {
                ChargingTime = 0;
                setStatus(e_Status.OCCUPIED_FINISHED);
                stop();
                SlotEventArg.Status = this.Status;
                if (SlotEventHandler != null)
                {
                    SlotEventHandler(this, SlotEventArg);
                }
                
            }
            else if (RemainingChargingTime == -VendingMachine.OverdueWaitTime)
            {              
                setStatus(e_Status.OCCUPIED_OVERDUE);
                stopTimer();
                SlotEventArg.Status = this.Status;
                if (SlotEventHandler != null)
                {
                    SlotEventHandler(this, SlotEventArg);
                }
            }
        }

    }//end Slot
}
