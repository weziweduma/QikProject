using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qik
{
    /// <summary>
    /// This class dispenses the change the user is supposed to receive when the money
    /// inserted is more than the money corresponding to the selected charging time.
    /// </summary>
    public class ChangeDispenser
    {

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

            return true;
        }

    }//end ChangeDispenser
}
