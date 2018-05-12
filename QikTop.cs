using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace Qik
{
    /// <summary>
    /// Main form and class for the application
    /// </summary>
    public partial class QikTop : Form
    {
        ////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Event classes for the various events
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ChargeRetrieveEventArgs class which contains information from the user 
        /// whether he wants to charge or retrieve his device
        /// <param name="ChargeRetrieve">The user selected charging or retrieving</param>
        /// </summary>
        public class ChargeRetrieveEventArgs : EventArgs
        {
            public VendingMachine.e_ChargingOrRetrieving ChargeRetrieve { get; set; }
        }      
        /// <summary>
        /// Event handler delegates for the ChargeRetrieve event 
        /// </summary>
        public event EventHandler<ChargeRetrieveEventArgs> ChargeRetrieveEvent;

        ///////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Instantiate classes used by this class
        /// </summary>
        ///////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// VendingMachine instance that is the core of the application
        /// </summary>
        public static VendingMachine vending = new VendingMachine();
        /// <summary>
        /// Timer instance that delays loading of new pictures onto the GUI
        /// </summary>
        static System.Windows.Forms.Timer image_change_timer = new System.Windows.Forms.Timer();
        /// <summary>
        /// ChargePhone instance form that captures information about the selected slot,
        /// charging time and the password
        /// </summary>
        ChargePhone charge_phone = new ChargePhone();
        /// <summary>
        /// Transact form the gives feedback to the user on how much moneys is expected,
        /// how much was inserted, and how much will be returned. It also instructs the
        /// user to start charging
        /// </summary>
        Transact trans = new Transact();//(Receive_Money, Slots_charge,(byte)selected_slot );

        ///////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Normal variables
        /// </summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        static int count = 1; ///image number
        string images_dir;    ///image directory
      //  MoneyProcessing ReceiveMoney = new MoneyProcessing();
      //  SlotsClass[] Slots = new SlotsClass[26];

        /////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Methods
        /// </summary>
        /////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor 
        /// </summary>
        public QikTop()
        {
            InitializeComponent();
            initialise();
        }
        /// <summary>
        /// Initialize the variables and classes 
        /// </summary>
        private void initialise()
        {
            image_change_timer.Tick += new EventHandler(imageChangeEvent);
            image_change_timer.Interval = 5000;
            image_change_timer.Start();
          //  ReceiveMoney.AmountPayed = 10;
          //  ReceiveMoney.setupDevices();

         /*   for (int i = 0; i < 26; i++)
            {
                Slots[i] = new SlotsClass();
            }

            for (int i = 0; i < 26; i++)
            {
                Slots[i].Slot_status = 0;   
            }
            */
            ChargeButton.Click += ChoiceButton_Click;
            RetrieveButton.Click += ChoiceButton_Click;
            this.ChargeRetrieveEvent += vending.ChargeRetrieveEvent;
            //this.SlotSelectedEvent   += vending.SlotSelected;
           // this.ChargeTimeEvent     += vending.ChargeTimeEventEntered;
            vending.gotoNextStage    += gotoSlotSelection;
            vending.gotoNextStage += charge_phone.gotoSlotSelection;                //Handler called when VendingMachine generates events
            vending.gotoNextStage += trans.gotoPayment;                //Handler called when VendingMachine generates events
            charge_phone.SlotSelectedEvent += vending.SlotSelected;      //Handlers called in VendingMachine when this form generates this event
            charge_phone.ChargeTimeEvent += vending.ChargeTimeEntered; //Handlers called in VendingMachine when this form generates this event
            charge_phone.PasswordEvent += vending.PasswordEntered;   //Handlers called in VendingMachine when this form generates this event
           // this.PaymentEvent += vending.PaymentEvent;
            //charge_phone.FormClosed += new FormClosedEventHandler(charge_phone_FormClosed);
            charge_phone.Hide();
            trans.Hide();

        }

        /// <summary>
        /// User selected that he wants to charge or retrieve his device
        /// </summary>
        /// <param name="sender">standard sender object which is the Charge or Retrieve button in this case</param>
        /// <param name="e"> standard event argument in this case it is Clicked</param>
        private void ChoiceButton_Click(object sender, EventArgs e)
        {
            Button ChoiceButton = sender as Button;
            ChargeRetrieveEventArgs ChargeRetrieveArg = new ChargeRetrieveEventArgs();
            if (ChoiceButton.Name.Equals(ChargeButton.Name))
            {
                ChargeRetrieveArg.ChargeRetrieve = VendingMachine.e_ChargingOrRetrieving.CHARGING;
            }
            else
            {
                ChargeRetrieveArg.ChargeRetrieve = VendingMachine.e_ChargingOrRetrieving.RETRIEVING;
            }
            System.Console.WriteLine("Charging Choice " + ChargeRetrieveArg.ChargeRetrieve);
            EventHandler<ChargeRetrieveEventArgs> chargeRetrieveHandler = ChargeRetrieveEvent;
            if (chargeRetrieveHandler != null)
            {
                chargeRetrieveHandler(this, ChargeRetrieveArg);
            }
        }
        /// <summary>
        /// Displays the form as instructed by the VendingMachine class and hides if the 
        /// program is at another stage which needs a different form to be displayed
        /// <param name="sender">standard sender object which is VendingMachine in this case</param>
        /// <param name="e"> NextStageEventArgs which contains information about the current 
        /// stage and atate of the program</param>
        /// </summary>
        public void gotoSlotSelection(object sender, VendingMachine.NextStageEventArgs e)
        {

            if (e.UI_Stage == VendingMachine.e_CurrentUIstage.START_STAGE)
            {
                this.Show();
            }
            else
            {
                this.Hide();
            }
        }
        /// <summary>
        /// imageChangeEvent is called whenever the delay timer times out.
        /// It changes the displayed image in the image panel to another one.
        /// <param name="sender">standard sender object which is timer in this case</param>
        /// <param name="e"> standard event argument in this case it is Elapsed</param>
        /// </summary>
        private void imageChangeEvent(object sender, System.EventArgs e)
        {

            //images_dir = @"D:\personal\Boaz\Software_Develepment\InterimSoftware\QikCharge_Ver1\ConsoleApplication1\images\panel_images\" + Convert.ToString(count) + ".jpg";
            images_dir = @"..\..\Resources\images\panel_images\" + Convert.ToString(count) + ".jpg";
            //Console.WriteLine(images_dir);

            panel1.BackgroundImage = Image.FromFile(@images_dir);
            if (count == 9) count = 1;
            else count++;
        }

        /*
        private void charge_phone_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
        */
    }
}
