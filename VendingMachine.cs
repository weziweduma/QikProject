﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using System.Windows.Forms;
namespace Qik
{
        /// <summary>
		/// The VendingMachine class shall get user inputs from GUI for transaction type,
		/// the charging time, the money and shall perform these tasks: claming slots,
		/// charging, releasing slots, processing money, printing receipt,
		/// dispensing change, keeping track of which slots are available etc.
		/// </summary>
		public class VendingMachine {


            /// <summary>
            /// Events that can be triggered to notify the VendingMachine class.
            /// </summary>
            public class NextStageEventArgs : EventArgs
            {
                public e_CurrentUIstage UI_Stage{ get; set; }
                public e_CurrentUIstageState UI_Stage_State { get; set; }
                public e_ChargingOrRetrieving ChargingOrRetrieving { get; set; }
            }
            /// <summary>
            /// Event handler delegates for the event handlers that will process the events
            /// </summary>
            public event EventHandler<NextStageEventArgs> gotoNextStage;

            public enum e_ChargingOrRetrieving
            {
                CHARGING, RETRIEVING
            }
            /// <summary>
            /// Current STAGE that is active for the user
            /// </summary>
            public enum e_CurrentUIstage
            {
                START_STAGE, SLOT_SELECTION_STAGE, CHARGE_TIME_STAGE, PASSWORD_GENERATION_STAGE, PAYMENT_STAGE, DISPENSE_CHANGE_STAGE, PRINT_RECEIPT_STAGE, INSERT_DEVICE_STAGE, PASSWORD_ENTRY_STAGE, COLLECT_DEVICE_STAGE
            }
            /// <summary>
            /// Current process within current STAGE. eg WAITING_FOR_INPUT
            /// </summary>
            public enum e_CurrentUIstageState
            {
                WAITING_FOR_INPUT, USER_SELECTED, PROCESSING_INPUT, GENERATING_FEEDBACK, USER_ERROR, HARDWARE_ERROR, DONE
            }
            public enum e_Mode
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
            public IList<Slot> AvailableSlots { get; set; }
            /// <summary>
            /// Slots that are finished charging but have not been relieved of the charging
            /// device.
            /// </summary>
            public IList<Slot> FinishedSlots { get; set; }
		    /// <summary>
			/// Slots that are currently Busy charging
			/// </summary>
            public IList<Slot> BusySlots { get; set; }
            /// <summary>
            /// Slots that have completed the charging of their devices for the specified
            /// charging time but the devices have not been emptied for over the penalty period.
            /// 
            /// </summary>
            public IList<Slot> OverdueSlots { get; set; }
			/// <summary>
			/// Current STAGE that is active for the user
			/// </summary>
            private e_CurrentUIstage CurrentUIstage;
			/// <summary>
			/// Current process within current STAGE. eg WAITING_FOR_INPUT
			/// </summary>
            private e_CurrentUIstageState CurrentUIstageState;
			/// <summary>
			/// User selected slot in which they will deposit their phone to be charge or from
			/// which they will retrieve their charged phone.
			/// </summary>
			private int CurrentSlot;
			/// <summary>
			/// Temporary variable that stores the password enter by the user.
			/// </summary>
			private int CurrentSlotPassword;
            /// <summary>
			/// Temporary variable that stores the current charge time selected by the user.
			/// </summary>
            private int CurrentSlotChargeTime;
			/// <summary>
			/// Current time for logging purposes and perhaps also for user display
			/// </summary>
			private DateTime DateAndTime;

            /// <summary>
            /// The Sequence of actions are for charging or retrieving device.
            /// </summary>
            private e_ChargingOrRetrieving ChargingOrRetrieving;
			/// <summary>
			/// Current application mode: admin mode or normal mode
			/// </summary>
			private e_Mode Mode;
            /// <summary>
            /// Current application mode: admin mode or normal mode
            /// </summary>
            private int MoneyCharged;
            /// <summary>
            /// Current application mode: admin mode or normal mode
            /// </summary>
            private int MoneyReceived;
			/// <summary>
			/// Text to be printed out to receipt or screen
			/// </summary>
			private string printText;
            public const int Tariff_per15Min = 10;

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
                //Initialize classes
                /////////////////////////////////////
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
                 // Console.WriteLine("Slot ID " + i + "  " + AvailableSlots.ElementAt(i).getID());
                }
                m_ChangeDispenser = new ChangeDispenser();
                m_FileManager     = new FileManager();
                m_MoneyMachine    = new MoneyMachine();
                m_Printer         = new Printer();

                //Other members
                ChargingOrRetrieving = e_ChargingOrRetrieving.CHARGING;
                CurrentUIstage       = e_CurrentUIstage.START_STAGE;
                CurrentUIstageState  = e_CurrentUIstageState.WAITING_FOR_INPUT;

                MoneyCharged  = 0;
                MoneyReceived = 0;
                DateAndTime = new DateTime();
			  }

			~VendingMachine(){

			}

			/// <summary>
			/// Administrator operation mode. This mode is for administrator to debug and
			/// modify the hardware.
			/// </summary>
			public void adminMode()
            {
                this.Mode = e_Mode.ADMIN;

			}

			/// <summary>
			/// Initiates charging process.
			/// </summary>
			public bool charge()
            {

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
			public e_Mode getMode(){

				return Mode;
			}
            /// <summary>
            /// Retrieve the money the user has to pay in order to charge the device
            /// </summary>
            public int getMoneyCharged()
            {
                if (CurrentSlotChargeTime >= 15)
                {
                    return (int)Math.Round((float)CurrentSlotChargeTime / 15) * Tariff_per15Min;
                }
                else
                {
                    return (int)Math.Ceiling((float)CurrentSlotChargeTime / 5) * 5;
                }
            }
            /// <summary>
            /// Retrieve the money the user has to pay in order to charge the device
            /// </summary>
            public int getMoneyReceived()
            {
                return MoneyReceived;
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
			/// Display the ChargeTimeSTAGE
			/// </summary>
			public int showChargeTimeStage(){

				return 0;
			}

			/// <summary>
			/// Display the PasswordGenerationSTAGE
			/// </summary>
			public int showPasswordGenerationStage(){

				return 0;
			}

			/// <summary>
			/// Show the PaymentSTAGE
			/// </summary>
			public int showPaymentStage(){

				return 0;
			}

			/// <summary>
			/// Show the SlotRetrievingSTAGE
			/// </summary>
			public int showSlotRetrievingStage(){

				return 0;
			}

			/// <summary>
			/// Show the StartSTAGE
			/// </summary>
			public int showStartStage(){
                QikTop.ActiveForm.Show();

				return 0;
			}

			/// <summary>
			/// Store the user entered charging time
			/// </summary>
			/// <param name="Slot"></param>
			/// <param name="ChargeTime"></param>
		/*	public bool storeChargeTime(Slot Slot, time ChargeTime){

				return false;
			}
*/
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
            public void ChargeRetrieveEvent(object sender, QikTop.ChargeRetrieveEventArgs e)
            {
                QikTop QikTopForm = sender as QikTop;
                NextStageEventArgs nextStageEvent = new NextStageEventArgs();
                EventHandler<NextStageEventArgs> GoToNextHandler = gotoNextStage;
                if (GoToNextHandler == null)
                {
                    return; //error
                }
                CurrentUIstageState = e_CurrentUIstageState.USER_SELECTED;
                nextStageEvent.UI_Stage_State = CurrentUIstageState;
                nextStageEvent.UI_Stage = CurrentUIstage;
                // GoToNextHandler(this, nextStageEvent);
                //log Status
                //Check that current STAGE is the slot selection STAGE
                
                if (CurrentUIstage == e_CurrentUIstage.START_STAGE)
                {
                    ChargingOrRetrieving = e.ChargeRetrieve;
                    CurrentUIstageState = e_CurrentUIstageState.PROCESSING_INPUT;
                    //log Status
                    nextStageEvent.UI_Stage_State = CurrentUIstageState;
                    nextStageEvent.UI_Stage = CurrentUIstage;
                    GoToNextHandler(this, nextStageEvent);
                    //Check that the current mode is charging
                    // QikTopForm.gotoSlotSelection();
                    CurrentUIstage = e_CurrentUIstage.SLOT_SELECTION_STAGE;
                    CurrentUIstageState = e_CurrentUIstageState.WAITING_FOR_INPUT;
                    //log Status         
                    nextStageEvent.UI_Stage_State = CurrentUIstageState;
                    nextStageEvent.UI_Stage = CurrentUIstage;
                    GoToNextHandler(this, nextStageEvent);
                }
                else
                {
                    //post error
                }
            }
            public void SlotSelected(object sender, Qik.ChargePhone.SlotSelectedEventArgs e)
            {
                //ChargePhone SlotSelectionForm = sender as ChargePhone;
                NextStageEventArgs nextStageEvent = new NextStageEventArgs();
                EventHandler<NextStageEventArgs> GoToNextHandler = gotoNextStage;
                if (GoToNextHandler == null)
                {
                  return; //error
                }
                CurrentUIstageState = e_CurrentUIstageState.USER_SELECTED;
                nextStageEvent.UI_Stage_State = CurrentUIstageState;
                nextStageEvent.UI_Stage = CurrentUIstage;
                GoToNextHandler(this, nextStageEvent);   //Trigger event                     
                Console.WriteLine("Slot select part 1");
                //log Status
                //Check that current STAGE is the slot selection STAGE
                if (CurrentUIstage == e_CurrentUIstage.SLOT_SELECTION_STAGE)
                {
                    CurrentSlot = e.Slot;
                    CurrentUIstageState = e_CurrentUIstageState.PROCESSING_INPUT;
                    //log Status
                    nextStageEvent.UI_Stage_State = CurrentUIstageState;
                    nextStageEvent.UI_Stage = CurrentUIstage;
                    GoToNextHandler(this, nextStageEvent);
                    Console.WriteLine("Slot select part 1");
                    //Check that the current mode is charging
                    if (ChargingOrRetrieving == e_ChargingOrRetrieving.CHARGING)
                    {
                        //Update list of slots if necessary
                      //  if (AvailableSlots.IndexOf(m_Slot[CurrentSlot]) != -1)
                        {
                           // AvailableSlots.Remove(m_Slot[CurrentSlot]);
                           // BusySlots.Add(m_Slot[CurrentSlot]);
                           // m_Slot[CurrentSlot].setStatus(Slot.e_Status.OCCUPIED_CHARGING);
                            CurrentUIstage = e_CurrentUIstage.CHARGE_TIME_STAGE;
                            CurrentUIstageState = e_CurrentUIstageState.WAITING_FOR_INPUT;
                            nextStageEvent.UI_Stage_State = CurrentUIstageState;
                            nextStageEvent.UI_Stage = CurrentUIstage;
                            GoToNextHandler(this, nextStageEvent); 
                            //log Status
                        }
                     //   else
                        {
                            //post slot selection error
                        }
                    }
                    else
                    {
                        //Update list of slots if necessary
                        if (BusySlots.IndexOf(m_Slot[CurrentSlot]) != -1)
                        {
                           // BusySlots.Remove(m_Slot[CurrentSlot]);
                           // AvailableSlots.Add(m_Slot[CurrentSlot]);
                            // m_Slot[CurrentSlot].setStatus(Slot.e_Status.Available);
                            CurrentUIstage = e_CurrentUIstage.PASSWORD_ENTRY_STAGE;
                            CurrentUIstageState = e_CurrentUIstageState.WAITING_FOR_INPUT;
                            //log Status
                            nextStageEvent.UI_Stage_State = CurrentUIstageState;
                            nextStageEvent.UI_Stage = CurrentUIstage;
                            GoToNextHandler(this, nextStageEvent); 
                            // mainForm.showPasswordEntrySTAGE();
                        }
                        else if (FinishedSlots.IndexOf(m_Slot[CurrentSlot]) != -1)
                        {
                           // FinishedSlots.Remove(m_Slot[CurrentSlot]);
                           // AvailableSlots.Add(m_Slot[CurrentSlot]);
                            // m_Slot[CurrentSlot].setStatus(Slot.e_Status.Available);
                            CurrentUIstage = e_CurrentUIstage.PASSWORD_ENTRY_STAGE;
                            CurrentUIstageState = e_CurrentUIstageState.WAITING_FOR_INPUT;
                            //log Status
                            nextStageEvent.UI_Stage_State = CurrentUIstageState;
                            nextStageEvent.UI_Stage = CurrentUIstage;
                            GoToNextHandler(this, nextStageEvent); 
                            // mainForm.showPasswordEntrySTAGE();
                        }
                        else if (OverdueSlots.IndexOf(m_Slot[CurrentSlot]) != -1)
                        {
                          //  OverdueSlots.Remove(m_Slot[CurrentSlot]);
                          //  AvailableSlots.Add(m_Slot[CurrentSlot]);
                            // m_Slot[CurrentSlot].setStatus(Slot.e_Status.Available);
                            CurrentUIstage = e_CurrentUIstage.PAYMENT_STAGE;
                            CurrentUIstageState = e_CurrentUIstageState.WAITING_FOR_INPUT;
                            //log Status
                            nextStageEvent.UI_Stage_State = CurrentUIstageState;
                            nextStageEvent.UI_Stage = CurrentUIstage;
                            GoToNextHandler(this, nextStageEvent); 
                            // mainForm.showPaymentSTAGE();
                        }
                        else
                        {
                            //post slot selection error
                        }
                    }

                }
            }

            public void ChargeTimeEntered(object sender, ChargePhone.ChargeTimeEventArgs e)
            {
                //Transact PaymentForm = sender as Transact;
                ChargePhone ChargePhoneForm = sender as ChargePhone;
                NextStageEventArgs nextStageEvent = new NextStageEventArgs();
                EventHandler<NextStageEventArgs> GoToNextHandler = gotoNextStage;
                if (GoToNextHandler == null)
                {
                  return; //error
                }
                CurrentUIstageState = e_CurrentUIstageState.USER_SELECTED;
                nextStageEvent.UI_Stage_State = CurrentUIstageState;
                nextStageEvent.UI_Stage = CurrentUIstage;
                GoToNextHandler(this, nextStageEvent); 
                //log Status
                //Check that current STAGE is the slot selection STAGE
                if (CurrentUIstage == e_CurrentUIstage.CHARGE_TIME_STAGE)
                {
                    this.CurrentSlotChargeTime = e.ChargeTime;
                    this.MoneyCharged = this.getMoneyCharged();
                    Console.WriteLine("Passed this part");
                    CurrentUIstageState = e_CurrentUIstageState.PROCESSING_INPUT;
                    //log Status
                    //Check that the current mode is charging

                    if (ChargingOrRetrieving == e_ChargingOrRetrieving.CHARGING)
                    {
                      //  m_Slot[CurrentSlot].setChargeTime(CurrentSlotChargeTime);
                        CurrentUIstage = e_CurrentUIstage.PASSWORD_GENERATION_STAGE;
                        CurrentUIstageState = e_CurrentUIstageState.WAITING_FOR_INPUT;
                        //log Status
                        nextStageEvent.UI_Stage_State = CurrentUIstageState;
                        nextStageEvent.UI_Stage = CurrentUIstage;
                        GoToNextHandler(this, nextStageEvent); 
                        // mainForm.showChargeTimeSTAGE();
                        //PaymentForm.Hide();

                    }
                    else
                    {
                        //post error
                    }
                }
            }
            public void PasswordEntered(object sender, ChargePhone.PasswordEventArgs e)
            {
                //Transact PaymentForm = sender as Transact;
                ChargePhone ChargePhoneForm = sender as ChargePhone;
                NextStageEventArgs nextStageEvent = new NextStageEventArgs();
                EventHandler<NextStageEventArgs> GoToNextHandler = gotoNextStage;
                if (GoToNextHandler == null)
                {
                    return; //error
                }
                CurrentUIstageState = e_CurrentUIstageState.USER_SELECTED;
                nextStageEvent.UI_Stage_State = CurrentUIstageState;
                nextStageEvent.UI_Stage = CurrentUIstage;
                GoToNextHandler(this, nextStageEvent);
                //log Status
                //Check that current STAGE is the slot selection STAGE
                if (CurrentUIstage == e_CurrentUIstage.PASSWORD_GENERATION_STAGE)
                {
                    CurrentSlotPassword = e.Password;
                    CurrentUIstageState = e_CurrentUIstageState.PROCESSING_INPUT;
                    //log Status
                    //Check that the current mode is charging
                    if (ChargingOrRetrieving == e_ChargingOrRetrieving.CHARGING)
                    {
                        //m_Slot[CurrentSlot].setChargeTime(CurrentSlotChargeTime);
                        CurrentUIstage = e_CurrentUIstage.PAYMENT_STAGE;
                        CurrentUIstageState = e_CurrentUIstageState.WAITING_FOR_INPUT;
                        //log Status
                        nextStageEvent.UI_Stage_State = CurrentUIstageState;
                        nextStageEvent.UI_Stage = CurrentUIstage;
                        GoToNextHandler(this, nextStageEvent);
                        //log Status
                        // mainForm.showChargeTimeSTAGE();
                        //PaymentForm.Hide();

                       // System.Threading.Thread.Sleep(2000);
                        MoneyReceived = m_MoneyMachine.getMoney(getMoneyCharged());
                        Console.WriteLine("Received  " + MoneyReceived);
                        if(MoneyReceived == 0) //User didn't insert 
                        {
                            CurrentUIstageState = e_CurrentUIstageState.USER_ERROR;
                         
                        }
                        else if (MoneyReceived == -1)//
                        {
                            CurrentUIstageState = e_CurrentUIstageState.HARDWARE_ERROR;
                        }
                        else
                        {
                            CurrentUIstageState = e_CurrentUIstageState.DONE;
                            nextStageEvent.UI_Stage_State = CurrentUIstageState;
                            nextStageEvent.UI_Stage = CurrentUIstage;
                            //log Status
                            GoToNextHandler(this, nextStageEvent);
                            Console.WriteLine("Going to Insert Money");
                            //System.Threading.Thread.Sleep(3000);
                            //dispense change
                            bool result = m_ChangeDispenser.dispenseChange(MoneyReceived - MoneyCharged);
                            CurrentUIstage = e_CurrentUIstage.DISPENSE_CHANGE_STAGE;
                            CurrentUIstageState = e_CurrentUIstageState.GENERATING_FEEDBACK;
                            nextStageEvent.UI_Stage_State = CurrentUIstageState;
                            nextStageEvent.UI_Stage = CurrentUIstage;
                            //log Status
                            GoToNextHandler(this, nextStageEvent);
                            Console.WriteLine("Going to Dispense change");
                           // System.Threading.Thread.Sleep(3000);
                            if (!result)
                            {
                                CurrentUIstageState = e_CurrentUIstageState.HARDWARE_ERROR;
                                //log error
                            }
                            else
                            {
                                CurrentUIstage = e_CurrentUIstage.PRINT_RECEIPT_STAGE;
                                CurrentUIstageState = e_CurrentUIstageState.GENERATING_FEEDBACK;
                                nextStageEvent.UI_Stage_State = CurrentUIstageState;
                                nextStageEvent.UI_Stage = CurrentUIstage;
                                //log Status
                                GoToNextHandler(this, nextStageEvent);
                                m_Printer.print("Slot Selected is " + CurrentSlot + "\r\n" +
                                    "Time is " + DateTime.Now + "\r\n" +
                                    "Charge time is  " + CurrentSlotChargeTime + "\r\n" +
                                    "Password is " + CurrentSlotPassword + "\r\n" +
                                    "Money requested was " + MoneyCharged + "\r\n" +
                                    "Money received is " + MoneyReceived + "\r\n" +
                                    "Change dispensed is " + (MoneyReceived - MoneyCharged) + "\r\n");
                                CurrentUIstage = e_CurrentUIstage.PRINT_RECEIPT_STAGE;
                                CurrentUIstageState = e_CurrentUIstageState.DONE;
                                nextStageEvent.UI_Stage_State = CurrentUIstageState;
                                nextStageEvent.UI_Stage = CurrentUIstage;
                                GoToNextHandler(this, nextStageEvent);
                                Console.WriteLine("Going to printing");
                               // System.Threading.Thread.Sleep(2000);

                                //insert device
                                CurrentUIstage = e_CurrentUIstage.INSERT_DEVICE_STAGE;
                                CurrentUIstageState = e_CurrentUIstageState.WAITING_FOR_INPUT;
                                nextStageEvent.UI_Stage_State = CurrentUIstageState;
                                nextStageEvent.UI_Stage = CurrentUIstage;
                                GoToNextHandler(this, nextStageEvent);
                                Console.WriteLine("Going to Insert device");
                               // System.Threading.Thread.Sleep(2000);

                                //go to start
                                //insert device
                                CurrentUIstage = e_CurrentUIstage.START_STAGE;
                                CurrentUIstageState = e_CurrentUIstageState.WAITING_FOR_INPUT;
                                nextStageEvent.UI_Stage_State = CurrentUIstageState;
                                nextStageEvent.UI_Stage = CurrentUIstage;
                                GoToNextHandler(this, nextStageEvent);
                                Console.WriteLine("Going to Start");
                            }
                        }


                    }
                    else
                    {
                        //post error
                    }
                }
            }
          /*  public void PaymentProcess(object sender, Transact.PaymentEventArgs e)
            {
                //Transact PaymentForm = sender as Transact;
                ChargePhone ChargePhoneForm = sender as ChargePhone;
                NextStageEventArgs nextStageEvent = new NextStageEventArgs();
                EventHandler<NextStageEventArgs> GoToNextHandler = gotoNextStage;
                if (GoToNextHandler == null)
                {
                    return; //error
                }
                CurrentUIstageState = e_CurrentUIstageState.USER_SELECTED;
                nextStageEvent.UI_Stage_State = CurrentUIstageState;
                nextStageEvent.UI_Stage = CurrentUIstage;
                GoToNextHandler(this, nextStageEvent);
                //log Status
                //Check that current STAGE is the slot selection STAGE
                if (CurrentUIstage == e_CurrentUIstage.PASSWORD_GENERATION_STAGE)
                {
                    CurrentSlotPassword = e.Password;
                    CurrentUIstageState = e_CurrentUIstageState.PROCESSING_INPUT;
                    //log Status
                    //Check that the current mode is charging
                    if (ChargingOrRetrieving == e_ChargingOrRetrieving.CHARGING)
                    {
                        //  m_Slot[CurrentSlot].setChargeTime(CurrentSlotChargeTime);
                        CurrentUIstage = e_CurrentUIstage.PAYMENT_STAGE;
                        CurrentUIstageState = e_CurrentUIstageState.WAITING_FOR_INPUT;
                        //log Status
                        nextStageEvent.UI_Stage_State = CurrentUIstageState;
                        nextStageEvent.UI_Stage = CurrentUIstage;
                        GoToNextHandler(this, nextStageEvent);
                        // mainForm.showChargeTimeSTAGE();
                        //PaymentForm.Hide();

                    }
                    else
                    {
                        //post error
                    }
                }
            }
           * */
		}//end VendingMachine
}
