using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Qik
{
    /// <summary>
    /// Transact form the gives feedback to the user on how much moneys is expected,
    /// how much was inserted, and how much will be returned. It also instructs the
    /// user to start charging
    /// </summary>
    public partial class Transact : Form
    {
        /*
        ////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Event classes for the various events
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// PaymentEventArgs class which contains the payment the user selected
        /// <param name="ChargeRetrieve">The user selected charging or retrieving</param>
        /// </summary>
        public class PaymentEventArgs : EventArgs
        {
           public int Payment { get; set; }
        }
        /// <summary>
        /// Event handler delegates for the PaymentEvent event 
        /// </summary>
        public event EventHandler<PaymentEventArgs> PaymentEvent;
        */
        ///////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Instantiate classes used by this class
        /// </summary>
        ///////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Timer instance that delays loading of new pictures onto the GUI
        /// </summary>
        static System.Windows.Forms.Timer transtimeout = new System.Windows.Forms.Timer();
        
        ///////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Normal variables
        /// </summary>
        ///////////////////////////////////////////////////////////////////////////////////////////
        int timeout_outvalue = 60;
       // public MoneyProcessing ReceiveMoney1;
       // SlotsClass[] Slots_trans;
       // byte selectedSlot;

        /////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Methods
        /// </summary>
        /////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor 
        /// </summary>
        public Transact()//(MoneyProcessing ReceiveMoney,SlotsClass[] Slots,byte selected_slot)
        {
            InitializeComponent();
  
      //      ReceiveMoney1 = ReceiveMoney;
      //      Slots_trans = Slots;
      //      selectedSlot = selected_slot;
            initialise();
            QikTop.vending.gotoNextStage += gotoPayment;                //Handler called when VendingMachine generates events
           // PaymentEvent += QikTop.vending.PaymentProcess;      //Handlers called in VendingMachine when this form generates this event
        }

        /// <summary>
        /// Initialize the variables and classes 
        /// </summary>
        private void initialise()
        {
               // timeout_outvalue 
               // TimerLabel.Text = Convert.ToString(timeout_outvalue);
          //      transtimeout.Tick += new EventHandler(CountDown_Event);
          //      transtimeout.Interval = 1000;
           //     transtimeout.Start();
        }

   /*     private void CountDown_Event(object sender, System.EventArgs e)
        {
            if (timeout_outvalue > 0)
            {
                timeout_outvalue = timeout_outvalue - 1;
                TimerLabel.Text  = Convert.ToString(timeout_outvalue);
*/
/*
                //Money received correctly and change has been dispensed
                if (timeout_outvalue == 57)
                {
                    receivedLBL.Visible = true;
                    Received_AmountLBL.Visible = true;
            //        ReceiveMoney1.AmountPayed += 30;
                }
                //Printing the receipt
                if (timeout_outvalue == 56)
                {
                    Printing_ReceiptLBL.Visible = true;
                }

                //Payment process complete
                if (timeout_outvalue == 54)
                {
                    TransDone_Pic.Visible = true;

                }
                //Inform user to insert device and charge in the opened slot
                if (timeout_outvalue == 53)
                {
                    ChargeDevicePn.Visible = true;
                    label8.Visible = true;
                    label9.Visible = true;
                    label10.Visible = true;
                    label11.Visible = true;
                    label12.Visible = true;
                    label2.Visible = true;
                }

                if (timeout_outvalue == 50)
                {
                    transtimeout.Stop();
                    TimerLabel.Visible = false;
                    //Form1 MainWindow = new Form1();
                   // MainWindow.FormClosed += new FormClosedEventHandler(MainWindow_FormClosed);
                    //MainWindow.Show();

             //       Slots_trans[(int)selectedSlot].Slot_status = 1;
                    this.Hide();
                    this.Close();
                }
 * */
  //          }
   //     }
        /// <summary>
        /// Displays the form as instructed by the VendingMachine class and hides if the 
        /// program is at another stage which needs a different form to be displayed
        /// <param name="sender">standard sender object which is VendingMachine in this case</param>
        /// <param name="e"> NextStageEventArgs which contains information about the current 
        /// stage and atate of the program</param>
        /// </summary>
        public void gotoPayment(object sender, VendingMachine.NextStageEventArgs e)
        {
            if (e.UI_Stage == VendingMachine.e_CurrentUIstage.PAYMENT_STAGE)
            {
                this.Show();
                switch (e.UI_Stage_State)
                {
                    case VendingMachine.e_CurrentUIstageState.WAITING_FOR_INPUT:
                    {
                            PaymentInstructLabel.Text = "*Please Insert via the Bill Collector or Coin Acceptor";
                            receivedLBL.Text = "Insert  ";
                            receivedLBL.Visible = true;
                            Received_AmountLBL.Text = "R" + QikTop.vending.getMoneyCharged();
                            Received_AmountLBL.Visible = true;
                            Console.WriteLine("Transact: Insert money");
                            DateTime start = DateTime.Now;
                            while ((DateTime.Now - start).TotalMilliseconds < 2000)
                                Application.DoEvents();
                            break;
                    }
                    case VendingMachine.e_CurrentUIstageState.USER_ERROR:
                    {
                        PaymentInstructLabel.Text = "Timed Out";
                        receivedLBL.Visible = false;
                        Received_AmountLBL.Visible = false;
                        Console.WriteLine("Transact: Insert money error");
                        break;
                    }
                    case VendingMachine.e_CurrentUIstageState.HARDWARE_ERROR:
                    {
                        PaymentInstructLabel.Text = "Hardware Failure!";
                        receivedLBL.Visible = false;
                        Received_AmountLBL.Visible = false;
                        Console.WriteLine("Transact: Insert money error");
                        break;
                    }
                    case VendingMachine.e_CurrentUIstageState.DONE:
                    {
                            receivedLBL.Text = "Received  ";
                            receivedLBL.Visible = true;
                            Received_AmountLBL.Text = "R" + QikTop.vending.getMoneyReceived();
                            Received_AmountLBL.Visible = true;
                            Console.WriteLine("Transact: Received money");
                            DateTime start = DateTime.Now;
                            while ((DateTime.Now - start).TotalMilliseconds < 2000)
                                Application.DoEvents();
                            break;
                    }
                }
            }

            else if (e.UI_Stage == VendingMachine.e_CurrentUIstage.PRINT_RECEIPT_STAGE || e.UI_Stage == VendingMachine.e_CurrentUIstage.DISPENSE_CHANGE_STAGE)
            {
                switch (e.UI_Stage_State)
                {
                    case VendingMachine.e_CurrentUIstageState.GENERATING_FEEDBACK:
                        {
                            Printing_ReceiptLBL.Visible = true;
                            Console.WriteLine("Transact: Dispensing change. Printing receipt");
                            DateTime start = DateTime.Now;
                            while ((DateTime.Now - start).TotalMilliseconds < 2000)
                                Application.DoEvents();
                            break;
                        }
                    case VendingMachine.e_CurrentUIstageState.DONE:
                        {
                            TransDone_Pic.Visible = true;
                            Console.WriteLine("Transact: payment process done");
                            DateTime start = DateTime.Now;
                            while ((DateTime.Now - start).TotalMilliseconds < 2000)
                                Application.DoEvents();
                            break;
                        }
                }
            }
            else if (e.UI_Stage == VendingMachine.e_CurrentUIstage.INSERT_DEVICE_STAGE)
            {
                ChargeDevicePn.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label2.Visible = true;
                Console.WriteLine("Transact: Insert device");
                DateTime start = DateTime.Now;
                while ((DateTime.Now - start).TotalMilliseconds < 2000)
                    Application.DoEvents();
            }
            else
            {
                this.Hide();  //hide form
                timeout_outvalue = 0;
                TimerLabel.Text = Convert.ToString(timeout_outvalue);

                //Money received correctly and change has been dispensed
                receivedLBL.Visible = false;
                Received_AmountLBL.Text = "";
                Received_AmountLBL.Visible = false;

                //Printing the receipt
                Printing_ReceiptLBL.Visible = false;

                //Payment feedback picture
                TransDone_Pic.Visible = false;

                //Proceed with charging panel
                ChargeDevicePn.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                label12.Visible = false;
                label2.Visible = false;
            }
        }
    /*    private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
*/

    }
}
