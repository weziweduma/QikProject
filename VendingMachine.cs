using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
namespace QikChargerApplication
{
        /// <summary>
		/// The VendingMachine class shall get user inputs from GUI for transaction type,
		/// the charging time, the money and shall perform these tasks: claming slots,
		/// charging, releasing slots, processing money, printing receipt,
		/// dispensing change, keeping track of which slots are available etc.
		/// </summary>
		public class VendingMachine {
            private enum e_ChargingOrRetrieving
            {
                CHARGING, RETRIEVING
            }
            private enum e_CurrentGUIpage
            {
                START_PAGE, SLOT_SELECTION_PAGE, CHARGE_TIME_PAGE, PASSWORD_GENERATION_PAGE, PAYMENT_PAGE, PASSWORD_ENTRY_PAGE, INSERT_DEVICE_PAGE, COLLECT_DEVICE_PAGE
            }
            /// <summary>
            /// Current process within current page. eg WAITING_FOR_INPUT
            /// </summary>
            private enum e_CurrentGUIpageState
            {
                WAITING_FOR_INPUT, USER_SELECTED, PROCESSING_INPUT, GENERATING_FEEDBACK
            }
            private enum e_Mode
            {
                NORMAL, ADMIN
            }
			/// <summary>
			/// Password administrator uses to access the application
			/// </summary>
			private string AdminPassword;
			/// <summary>
			/// Slots available for charging
			/// </summary>
			private IList<Slot> AvailableSlots;
            /// <summary>
            /// Slots that are finished charging but have not been relieved of the charging
            /// device.
            /// </summary>
            private IList<Slot> FinishedSlots;
			/// <summary>
			/// Slots that are currently Busy charging
			/// </summary>
            private IList<Slot> BusySlots;
            /// <summary>
            /// Slots that have completed the charging of their devices for the specified
            /// charging time but the devices have not been emptied for over the penalty period.
            /// 
            /// </summary>
            private IList<Slot> OverdueSlots;
			/// <summary>
			/// Current page that is active for the user
			/// </summary>
            private e_CurrentGUIpage CurrentGUIpage;
			/// <summary>
			/// Current process within current page. eg WAITING_FOR_INPUT
			/// </summary>
            private e_CurrentGUIpageState;
			/// <summary>
			/// User selected slot in which they will deposit their phone to be charge or from
			/// which they will retrieve their charged phone.
			/// </summary>
			private int currentSlot;
			/// <summary>
			/// Temporary variable that stores the password enter by the user.
			/// </summary>
			private int CurrentSlotPassword;
			/// <summary>
			/// Current time for logging purposes and perhaps also for user display
			/// </summary>
			private DateTime DateTime;
			/// <summary>
			/// Events that can be triggered to notify the VendingMachine class.
			/// </summary>
		//	private Event Event;
            /// <summary>
            /// Events that can be triggered to notify the VendingMachine class.
            /// </summary>
            private e_ChargingOrRetrieving ChargingOrRetrieving
			/// <summary>
			/// Current application mode: admin mode or normal mode
			/// </summary>
			private e_Mode Mode;
			/// <summary>
			/// Text to be printed out to receipt or screen
			/// </summary>
			private string printText;
			Printer m_Printer;
			Slot[] m_Slot;
			ChangeDispenser m_ChangeDispenser;
			MoneyMachine m_MoneyMachine;
			FileManager m_FileManager;

            public const int NumSlots = 26;
            /// <todo>
            /// Implement code for file manager
            /// </todo>
			public VendingMachine()
            {
                //Initialize Slot lists
                AvailableSlots = new List<Slot>();
                FinishedSlots = new List<Slot>();
                BusySlots = new List<Slot>();
                OverdueSlots = new List<Slot>();

                m_Slot = new Slot[NumSlots];
                for (int i = 0; i < NumSlots; i++)
                {
                  m_Slot[i] = new Slot(i + 1);
                  //For now assume all slots are available till FileManager class is implemented.
                  AvailableSlots.Add(m_Slot[i]);
                }
			  }

			~VendingMachine(){

			}

			/// <summary>
			/// Administrator operation mode. This mode is for administrator to debug and
			/// modify the hardware.
			/// </summary>
			public void adminMode(){

			}

			/// <summary>
			/// Initiates charging process.
			/// </summary>
			public bool charge(){

				return false;
			}

			/// <summary>
			/// Allocates the selected slot to the current user if it is available
			/// </summary>
			public bool claimSlot(){

				return false;
			}

			/// <summary>
			/// Called after the processPayment() method when the money inserted was higher
			/// than the money required
			/// </summary>
			public bool dispenseChange(){

				return false;
			}

			/// <summary>
			/// Generates a random password
			/// </summary>
			public bool generatePassword(){

				return false;
			}

			/// <summary>
			/// Generates the text to be printed to screen or receipt
			/// </summary>
			public bool generatePrintText(){

				return false;
			}

			/// <summary>
			/// Retrieve current mode
			/// </summary>
			public Enum getMode(){

				return null;
			}

			/// <summary>
			/// log events to a file. This is for debugging.
			/// </summary>
			public bool logEvent(){

				return false;
			}

			/// <summary>
			/// Normal operation mode. This mode is for user operation.
			/// </summary>
			public void normalMode(){

			}

			/// <summary>
			/// Powers up the hardware and places it in a default state.
			/// </summary>
			public void powerUp(){

			}

			/// <summary>
			/// Called after the password has been entered and the money has been inserted and
			/// the change has been dispensed. The receipt shall contain information about the
			/// slot allocated for charging, the charging time, the time at which the payment
			/// was made, the time at which the penalty will begin  and the password for
			/// retrieving the device.
			/// </summary>
			/// <param name="printText"></param>
			public bool printReceipt(string printText){

				return false;
			}

			/// <summary>
			/// Process the user entered Password. Check the validity of the password against
			/// the Slot's password
			/// </summary>
			/// <param name="PassWord"></param>
			/// <param name="Slot"></param>
			public bool processPassword(int PassWord, Slot Slot){

				return false;
			}

			/// <summary>
			/// Process the inserted money and continue with dispensing change and opening the
			/// charging slot if the money is correct, otherwise halt until the user has
			/// inserted the correct money.
			/// </summary>
			public bool processPayment(){

				return false;
			}

			/// <summary>
			/// process the Timers for the different slots. This is to determine which slots
			/// have an elapsed charging period.
			/// </summary>
			public bool processTimers(){

				return false;
			}

			/// <summary>
			/// Reset the states and modes of the application.
			/// </summary>
			public void reset(){

			}

			/// <summary>
			/// Called when user has enter correct password to retrieve phone from slot
			/// </summary>
			/// <param name="Slot"></param>
			public void retreive(Slot Slot){

			}

			/// <summary>
			/// Display the ChargeTimePage
			/// </summary>
			public int showChargeTimePage(){

				return 0;
			}

			/// <summary>
			/// Display the PasswordGenerationPage
			/// </summary>
			public int showPasswordGenerationPage(){

				return 0;
			}

			/// <summary>
			/// Show the PaymentPage
			/// </summary>
			public int showPaymentPage(){

				return 0;
			}

			/// <summary>
			/// Show the SlotRetrievingPage
			/// </summary>
			public int showSlotRetrievingPage(){

				return 0;
			}

			/// <summary>
			/// Show the StartPage
			/// </summary>
			public int showStartPage(){

				return 0;
			}

			/// <summary>
			/// Store the user entered charging time
			/// </summary>
			/// <param name="Slot"></param>
			/// <param name="ChargeTime"></param>
			public bool storeChargeTime(Slot Slot, time ChargeTime){

				return false;
			}

			/// <summary>
			/// Store the user entered password
			/// </summary>
			/// <param name="Slot"></param>
			/// <param name="Password"></param>
			public bool storeSlotPassword(Slot Slot, int Password){

				return false;
			}

			/// <summary>
			/// Stores the status of the various slots. This is so that if there is a power
			/// down or some malfunction, the system can restore itself to this state.
			/// </summary>
			public bool storeStatus(){

				return false;
			}

			/// <summary>
			/// Updates the sates of the SLots and the AvailableSlots and BusySlots variables.
			/// </summary>
			public bool update(){

				return false;
			}

			/// <summary>
			/// Show user the status of the payment process.
			/// </summary>
			public int updatePaymentStatus(){

				return 0;
			}

		}//end VendingMachine
}
