using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




    public class SlotsClass
    {

        public byte     Slot_status = 0;
        public string   Slot_status_string;
        public byte     vacant = 0;
        public byte     busy_charging = 1;
        public byte     waitingRetrieve = 2;
        
        public string getSlotStatus()
        {
            switch (Slot_status)
            {
                case 0 :
                    return   "Available";
                case 1 :
                    return "Busy Charging";

                default : return "Waiting Retrieve";
            }
        }

    }
