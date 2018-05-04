using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QikChargerApplication
{
    /// <summary>
    /// This class keeps track of the states for the charging slots and handles the
    /// call events from the VendingMachine class
    /// </summary>
    public class Slot
    {
        /// <summary>
        /// The amount of time to charge for
        /// </summary>
        private int ChargingTime;
        /// <summary>
        /// Slot identifier
        /// </summary>
        private int ID;
        /// <summary>
        /// Password for retrieving device from slot
        /// </summary>
        private int Password;
        /// <summary>
        /// Current status: CHARGING, FINISHED_OCCUPIED, AVAILABLE
        /// </summary>
        private Enum Status;
        /// <summary>
        /// The elapsed charging time timer
        /// </summary>
        private Timer Timer;
        public System.QikApplication.VendingMachine.Slot.Charger m_Charger;

        public Slot( int Id)
        {
            ID = Id;
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
        public int getChargeTime(int ChargeTime)
        {

            return 0;
        }

        /// <summary>
        /// Retrieve the Slot Number for this slot
        /// </summary>
        public int getID()
        {

            return 0;
        }

        /// <summary>
        /// Retrieve the password for the slot for the current user.
        /// </summary>
        public int getPassword()
        {

            return 0;
        }

        /// <summary>
        /// Retrive the the status of this slot.
        /// </summary>
        public Enum getStatus()
        {

            return null;
        }

        /// <summary>
        /// Open the Slot for the user to insert or collect the device
        /// </summary>
        public int open()
        {

            return 0;
        }

        /// <summary>
        /// Resets Slot attributes to default values
        /// </summary>
        public void reset()
        {

        }

        /// <summary>
        /// Set the charge time for the slot
        /// </summary>
        public int setChargeTime()
        {

            return 0;
        }

        /// <summary>
        /// Set the password for the Slot for the current user
        /// </summary>
        /// <param name="Password"></param>
        public bool setPassword(int Password)
        {

            return false;
        }

        /// <summary>
        /// Set the status of the slot: CHARGING, AVAILABLE etc.
        /// </summary>
        /// <param name="Status"></param>
        public int setStatus(Enum Status)
        {

            return 0;
        }

        /// <summary>
        /// Start the charging timer
        /// </summary>
        public int startTimer()
        {

            return 0;
        }

        /// <summary>
        /// Stop the charging timer.
        /// </summary>
        public int stopTimer()
        {

            return 0;
        }

    }//end Slot
}
