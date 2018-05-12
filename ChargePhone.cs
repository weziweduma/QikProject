using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Random;
using System.Windows.Forms;

namespace Qik
{
    /// <summary>
    /// ChargePhone form that captures information about the selected slot,
    /// charging time and the password
    /// </summary>
    public partial class ChargePhone : Form
    {
        ////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Event classes for the various events
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// SlotSelectedEventArgs class which contains the slot the user selected
        /// <param name="SlotSelectedEventArgs">The user selected slot</param>
        /// </summary>
        public class SlotSelectedEventArgs : EventArgs
        {
            public int Slot { get; set; }
        }
        /// <summary>
        /// ChargeTimeEventArgs class which contains the charging time the user selected
        /// <param name="SlotSelectedEventArgs">The user selected charging time</param>
        /// </summary>
        public class ChargeTimeEventArgs : EventArgs
        {
            public int ChargeTime { get; set; }
        }
        /// <summary>
        /// PasswordEventArgs class which contains the password the user entered
        /// <param name="SlotSelectedEventArgs">The user password</param>
        /// </summary>
        public class PasswordEventArgs : EventArgs
        {
            public int Password { get; set; }
        }
        /// <summary>
        /// Event handler delegates for the SlotSelectedEvent event 
        /// </summary>
        public event EventHandler<SlotSelectedEventArgs> SlotSelectedEvent;
        /// <summary>
        /// Event handler delegates for the ChargeTimeEvent event 
        /// </summary>
        public event EventHandler<ChargeTimeEventArgs> ChargeTimeEvent;
        /// <summary>
        /// Event handler delegates for the PasswordEvent event 
        /// </summary>
        public event EventHandler<PasswordEventArgs> PasswordEvent;

        ///////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Instantiate classes used by this class
        /// </summary>
        ///////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Timer instance that delays loading of new pictures onto the GUI
        /// </summary>
        static System.Windows.Forms.Timer image_change_timer = new System.Windows.Forms.Timer();

        ///////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Normal variables
        /// </summary>
        ///////////////////////////////////////////////////////////////////////////////////////////
        static int count              = 1;   ///image number
        string images_dir;                   ///image directory
        int passwordLength            = 6;
        public string LockerPassword;

      //  MoneyProcessing Receive_Money ;
      //  SlotsClass[] Slots_charge;
        int selected_slot;
        List<Button> Locker_buttons = new List<Button>(); ///list of slot buttons
        List<Label> Locker_status = new List<Label>();   ///list of slot labels
       // Button btn = new Button();
       // Label lbl = new Label();

        /////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Methods
        /// </summary>
        /////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor 
        /// </summary>
        public ChargePhone()//(MoneyProcessing ReceiveMoney,SlotsClass[] slots)
        {
            InitializeComponent();

           // Receive_Money = ReceiveMoney;
            // Slots_charge = slots;  
            initialise();

        }
        /// <summary>
        /// Initialize the variables and classes 
        /// </summary>
        private void initialise()
        {
               // btn.Click += new EventHandler(btn_Click);
               // lbl.TextChanged += new EventHandler(lbl_textchanged);

                ManualPasswordTB.Font = new Font("Arial", 28);
                Label15Min.Text = "R" + Convert.ToString(VendingMachine.Tariff_per15Min);
                Label30Min.Text = "R" + Convert.ToString(VendingMachine.Tariff_per15Min * 2);
                Label45Min.Text = "R" + Convert.ToString(VendingMachine.Tariff_per15Min * 3);
                Label60Min.Text = "R" + Convert.ToString(VendingMachine.Tariff_per15Min * 4);
                Label90Min.Text = "R" + Convert.ToString(VendingMachine.Tariff_per15Min * (90/15));
                Label120Min.Text = "R" + Convert.ToString(VendingMachine.Tariff_per15Min * (120/15));

                map_GUI_buttons_Labels();
            /*
                slot1_state.Text = Slots_charge[0].getSlotStatus();
                slot2_state.Text = Slots_charge[1].getSlotStatus();
                slot3_state.Text = Slots_charge[2].getSlotStatus();
                slot4_state.Text = Slots_charge[3].getSlotStatus();
                slot5_state.Text = Slots_charge[4].getSlotStatus();
                slot6_state.Text = Slots_charge[5].getSlotStatus();
                slot7_state.Text = Slots_charge[6].getSlotStatus();
                slot8_state.Text = Slots_charge[7].getSlotStatus();
                slot9_state.Text = Slots_charge[8].getSlotStatus();
                slot10_state.Text = Slots_charge[9].getSlotStatus();
                slot11_state.Text = Slots_charge[10].getSlotStatus();
                slot12_state.Text = Slots_charge[11].getSlotStatus();
                slot13_state.Text = Slots_charge[12].getSlotStatus();
                slot14_state.Text = Slots_charge[13].getSlotStatus();
                slot15_state.Text = Slots_charge[14].getSlotStatus();
                slot16_state.Text = Slots_charge[15].getSlotStatus();
                slot17_state.Text = Slots_charge[16].getSlotStatus();
                slot18_state.Text = Slots_charge[17].getSlotStatus();
                slot19_state.Text = Slots_charge[18].getSlotStatus();
                slot20_state.Text = Slots_charge[19].getSlotStatus();
                slot21_state.Text = Slots_charge[20].getSlotStatus();
                slot22_state.Text = Slots_charge[21].getSlotStatus();
                slot23_state.Text = Slots_charge[22].getSlotStatus();
                slot24_state.Text = Slots_charge[23].getSlotStatus();
                slot25_state.Text = Slots_charge[24].getSlotStatus();
                slot26_state.Text = Slots_charge[25].getSlotStatus();
            */
                image_change_timer.Tick += new EventHandler(imageChangeEvent);
                image_change_timer.Interval = 5000;
                image_change_timer.Start();
          }
        /// <summary>
        /// Put the locker buttons and labels in a list for easier referencing 
        /// </summary>
        private void map_GUI_buttons_Labels()
        {
            ///Locker buttons
            Locker_buttons.Add(locker1_btn);
            Locker_buttons.Add(locker2_btn);
            Locker_buttons.Add(locker3_btn);
            Locker_buttons.Add(locker4_btn);
            Locker_buttons.Add(locker5_btn);
            Locker_buttons.Add(locker6_btn);
            Locker_buttons.Add(locker7_btn);
            Locker_buttons.Add(locker8_btn);
            Locker_buttons.Add(locker9_btn);
            Locker_buttons.Add(locker10_btn);
            Locker_buttons.Add(locker11_btn);
            Locker_buttons.Add(locker12_btn);
            Locker_buttons.Add(locker13_btn);
            Locker_buttons.Add(locker14_btn);
            Locker_buttons.Add(locker15_btn);
            Locker_buttons.Add(locker16_btn);
            Locker_buttons.Add(locker17_btn);
            Locker_buttons.Add(locker18_btn);
            Locker_buttons.Add(locker19_btn);
            Locker_buttons.Add(locker20_btn);
            Locker_buttons.Add(locker21_btn);
            Locker_buttons.Add(locker22_btn);
            Locker_buttons.Add(locker23_btn);
            Locker_buttons.Add(locker24_btn);
            Locker_buttons.Add(locker25_btn);
            Locker_buttons.Add(locker26_btn);

            ///Locker labels
            Locker_status.Add(slot1_state_label);
            Locker_status.Add(slot2_state_label);
            Locker_status.Add(slot3_state_label);
            Locker_status.Add(slot4_state_label);
            Locker_status.Add(slot5_state_label);
            Locker_status.Add(slot6_state_label);
            Locker_status.Add(slot7_state_label);
            Locker_status.Add(slot8_state_label);
            Locker_status.Add(slot9_state_label);
            Locker_status.Add(slot10_state_label);
            Locker_status.Add(slot11_state_label);
            Locker_status.Add(slot12_state_label);
            Locker_status.Add(slot13_state_label);
            Locker_status.Add(slot14_state_label);
            Locker_status.Add(slot15_state_label);
            Locker_status.Add(slot16_state_label);
            Locker_status.Add(slot17_state_label);
            Locker_status.Add(slot18_state_label);
            Locker_status.Add(slot19_state_label);
            Locker_status.Add(slot20_state_label);
            Locker_status.Add(slot21_state_label);
            Locker_status.Add(slot22_state_label);
            Locker_status.Add(slot23_state_label);
            Locker_status.Add(slot24_state_label);
            Locker_status.Add(slot25_state_label);
            Locker_status.Add(slot26_state_label);
        }
        /// <summary>
        /// User selected slot handler
        /// </summary>
        /// <param name="sender">standard sender object which is the slot button button in this case</param>
        /// <param name="e"> standard event argument in this case it is Clicked</param>
        private void SlotButton_Click(object sender, EventArgs e)
        {
            Button SlotButton = sender as Button;
            SlotSelectedEventArgs SlotSelectedArg = new SlotSelectedEventArgs();
            SlotSelectedArg.Slot = Locker_buttons.IndexOf(SlotButton);
            EventHandler<SlotSelectedEventArgs> SlotSelectedHandler = SlotSelectedEvent;
            if (SlotSelectedHandler != null)
            {
                SlotSelectedHandler(this, SlotSelectedArg);
            }
            Console.WriteLine("Button is " + SlotButton.Name);
            Console.WriteLine("Button ID is " + (Locker_buttons.IndexOf(SlotButton) + 1));
           // SlotButtonPanel.Enabled = false;
        }
    /*    void lbl_textchanged(object sender, EventArgs e)
        {
            lbl = (Label)sender;
            selected_slot = Locker_buttons.IndexOf(sender as Button);


            if (Locker_status[selected_slot].Text == "Busy Charging" || Locker_status[selected_slot].Text == "Waiting Retrieve")
            {
                Locker_status[selected_slot].BackColor = Color.Red;
                Locker_buttons[selected_slot].Enabled = false;
            }
            else
            {
                Locker_status[selected_slot].BackColor = Color.White;
                Locker_buttons[selected_slot].Enabled = true;
            }
           
        }*/
        
        /// <summary>
        /// User selected charge time handler
        /// </summary>
        /// <param name="sender">standard sender object which is the charge time button in this case</param>
        /// <param name="e"> standard event argument in this case it is Clicked</param>
        private void ChargeTimeButton_Click(object sender, EventArgs e)
        {
            Button ChargeTimeButton = sender as Button;
            ChargeTimeEventArgs ChargeTimeArg = new ChargeTimeEventArgs();
            string temp = ChargeTimeButton.Text.Remove(3);// Split(" ", 1, null).ToArray;
            ChargeTimeArg.ChargeTime = int.Parse(temp);
            EventHandler<ChargeTimeEventArgs> ChargeTimeHandler = ChargeTimeEvent;
            if (ChargeTimeHandler != null)
            {
                ChargeTimeHandler(this, ChargeTimeArg);
            }
            Console.WriteLine("Button is " + ChargeTimeButton.Name);
            Console.WriteLine("Charge time is " + ChargeTimeArg.ChargeTime);
            SlotButtonPanel.Enabled = false;
        }

        /// <summary>
        /// User selected to generate a password automatically
        /// </summary>
        /// <param name="sender">standard sender object which is the AutoPassword button in this case</param>
        /// <param name="e"> standard event argument in this case it is Clicked</param>
        private void AutoPassword_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            if (LockerPassword == null)
            {
                for (int i = 0; i < passwordLength; i++)
                {
                    LockerPassword += Convert.ToString(rand.Next(0, 9));
                }
            }
            PasswordEventArgs PasswordArg = new PasswordEventArgs();
            PasswordArg.Password = int.Parse(LockerPassword);
            EventHandler<PasswordEventArgs> PasswordHandler = PasswordEvent;
            if (PasswordHandler != null)
            {
                PasswordHandler(this, PasswordArg);
            }
            Console.WriteLine("Password is " + PasswordArg.Password);
        }
        /// <summary>
        /// User selected to insert a user password
        /// </summary>
        /// <param name="sender">standard sender object which is the ManualPassword button in this case</param>
        /// <param name="e"> standard event argument in this case it is Clicked</param>
        private void ManualPassword_Click(object sender, EventArgs e)
        {
            ManualPasswordEnter.Enabled = true;
            ManualPasswordEnter.Visible = true;
        }
        /// <summary>
        /// Append user clicked digit to the password
        /// </summary>
        /// <param name="sender">standard sender object which is the NumPad number button in this case</param>
        /// <param name="e"> standard event argument in this case it is Clicked</param>
        private void NumPadNumber_Click(object sender, EventArgs e)
        {
            Button NumButton = sender as Button;
            if (ManualPasswordTB.Text.Length < passwordLength) ManualPasswordTB.Text += NumButton.Text.Trim();
        }
        /// <summary>
        /// Delete last digit of the password
        /// </summary>
        /// <param name="sender">standard sender object which is the NumPad number button in this case</param>
        /// <param name="e"> standard event argument in this case it is Clicked</param>
        private void NumPadBackspace_Click(object sender, EventArgs e)
        {
            int startpoint = ManualPasswordTB.Text.Length;
            if (ManualPasswordTB.Text.Length > 0 && ManualPasswordTB.Text.Length < 7)
            {
                ManualPasswordTB.Text = ManualPasswordTB.Text.Substring(0, startpoint - 1);
            }
        }
        /// <summary>
        /// Finalize user entered password
        /// </summary>
        /// <param name="sender">standard sender object which is the NumPad number button in this case</param>
        /// <param name="e"> standard event argument in this case it is Clicked</param>
        private void NumPadEnter_Click(object sender, EventArgs e)
        {
            PasswordEventArgs PasswordArg = new PasswordEventArgs();
            PasswordArg.Password = int.Parse(ManualPasswordTB.Text);
            EventHandler<PasswordEventArgs> PasswordHandler = PasswordEvent;
            if (PasswordHandler != null)
            {
                PasswordHandler(this, PasswordArg);
            }
            Console.WriteLine("Password is " + PasswordArg.Password);
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
            if (e.UI_Stage == VendingMachine.e_CurrentUIstage.SLOT_SELECTION_STAGE)
            {
                this.Show();
                SlotButtonPanel.Enabled = true;
                SelectLockerPn.Visible = true;
                ChargePeriodPn.Visible = false;
                SetPasswordPn.Enabled = false;
                SetPasswordPn.Visible = false;
                System.Console.WriteLine("Charging? " + e.ChargingOrRetrieving);
                foreach(Slot slot in QikTop.vending.AvailableSlots)
                {
                      Locker_status.ElementAt(slot.getID()).BackColor = Color.White;
                      Locker_status.ElementAt(slot.getID()).Text = "Available";
                      if (e.ChargingOrRetrieving == VendingMachine.e_ChargingOrRetrieving.CHARGING)
                      {
                          Locker_buttons.ElementAt(slot.getID()).Enabled = true;
                      }
                      else
                      {
                          Locker_buttons.ElementAt(slot.getID()).Enabled = false;
                      }
                }
                foreach (Slot slot in QikTop.vending.BusySlots)
                {
                    System.Console.WriteLine("slot.getID()  " + slot.getID());
                      Locker_status.ElementAt(slot.getID()).BackColor = Color.Red;
                      Locker_status.ElementAt(slot.getID()).Text = "Busy Charging";
                      if (e.ChargingOrRetrieving == VendingMachine.e_ChargingOrRetrieving.CHARGING)
                      {
                          Locker_buttons.ElementAt(slot.getID()).Enabled = false;
                      }
                      else
                      {
                          Locker_buttons.ElementAt(slot.getID()).Enabled = true;
                      }
                    
                }
                foreach (Slot slot in QikTop.vending.FinishedSlots)
                {
                      Locker_status.ElementAt(slot.getID()).BackColor = Color.Red;
                      Locker_status.ElementAt(slot.getID()).Text = "Waiting Retrieve";
                      if (e.ChargingOrRetrieving == VendingMachine.e_ChargingOrRetrieving.CHARGING)
                      {
                          Locker_buttons.ElementAt(slot.getID()).Enabled = false;
                      }
                      else
                      {
                          Locker_buttons.ElementAt(slot.getID()).Enabled = true;
                      }
                }
                foreach (Slot slot in QikTop.vending.OverdueSlots)
                {
                      Locker_status.ElementAt(slot.getID()).BackColor = Color.Red;
                      Locker_status.ElementAt(slot.getID()).Text = "Waiting Retrieve";
                      if (e.ChargingOrRetrieving == VendingMachine.e_ChargingOrRetrieving.CHARGING)
                      {
                          Locker_buttons.ElementAt(slot.getID()).Enabled = false;
                      }
                      else
                      {
                          Locker_buttons.ElementAt(slot.getID()).Enabled = true;
                      } 
                }                          
            
            }
            else if (e.UI_Stage == VendingMachine.e_CurrentUIstage.CHARGE_TIME_STAGE)
            {
                this.Show();
                SelectLockerPn.Visible = false;
                SlotButtonPanel.Enabled = false;
                ChargePeriodPn.Visible = true;
                SetPasswordPn.Enabled = false;
                SetPasswordPn.Visible = false;
            }
            else if (e.UI_Stage == VendingMachine.e_CurrentUIstage.PASSWORD_GENERATION_STAGE)
            {
                this.Show();
                SelectLockerPn.Visible = false;
                SlotButtonPanel.Enabled = false;
                ChargePeriodPn.Visible = false;
                SetPasswordPn.Enabled = true;
                SetPasswordPn.Location = new Point(0, 402);
                SetPasswordPn.Visible = true;
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

            images_dir = @"..\..\Resources\images\jpg\" + Convert.ToString(count) + ".jpg";

            cp_panel1.BackgroundImage = Image.FromFile(@images_dir);
            if (count == 3) count = 1;
            else count++;
        }
        /*     private void trans_FormClosed(object sender, FormClosedEventArgs e)
             {
                 this.Close();
             }
            */  

    }
}
