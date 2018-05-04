using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace QikChargerApplication
{

    public partial class Main : Form
    { 
        const int NumSlots = 26; //Should be obtained from Vending Machine class
        int PassWord = 0;
        int ChargeTime = 0;
        IList<Button> SlotButtonList = new List<Button>();//GroupBox

        public Main()
        {
            InitializeComponent();
            ModifyDefaultComponentValues();
            InitializeSlotButtonList();
            
        
                
        }

        /// <summary>
        /// Creates the SlotButtons dynamically and arranges them in a grid. 
        /// </summary>
        private void InitializeSlotButtonList()
        {

            Button[] SlotButtons      = new Button[NumSlots];
            Label[] SlotButtonLabels  = new Label[NumSlots];

            for(int i = 0; i < NumSlots; i++)
            {
                SlotButtons[i] = new Button();
                SlotButtons[i].BackColor = System.Drawing.Color.LimeGreen;
                SlotButtons[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                SlotButtons[i].ForeColor = System.Drawing.Color.White;
                SlotButtons[i].Location = new System.Drawing.Point((i % 4) * 200 + this.PanelSlotButtons.Width / 4, (i / 4) * 100 + 60);
                SlotButtons[i].Name      = "SlotButton" + (i+1);
                SlotButtons[i].Size      =  new System.Drawing.Size(120, 60);
                SlotButtons[i].Text      = "Available \r\nClick to Select";
                SlotButtons[i].Click += this.SlotButton_Click;                

                SlotButtonLabels[i] = new Label();
                SlotButtonLabels[i].BackColor = System.Drawing.Color.PowderBlue;
                SlotButtonLabels[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                SlotButtonLabels[i].ForeColor = System.Drawing.Color.Navy;
                SlotButtonLabels[i].Location = new System.Drawing.Point((i % 4) * 200 + this.PanelSlotButtons.Width / 4 + 30, (i / 4) * 100 + 40);
                SlotButtonLabels[i].MinimumSize = new System.Drawing.Size(60, 20);
                SlotButtonLabels[i].Name        =  "SlotButtonLabel" + (i + 1);
                SlotButtonLabels[i].Size        =  new System.Drawing.Size(60, 20);
                SlotButtonLabels[i].Text        = "     " + (i + 1) + "     ";

                SlotButtonList.Add(SlotButtons[i]);
                this.PanelSlotButtons.Controls.Add(SlotButtonLabels[i]);
                this.PanelSlotButtons.Controls.Add(SlotButtons[i]);   
            }
        }

        /// <summary>
        /// Modifies the default sizes and locations for the form controls. Also addds the handlers to the control buttons 
        /// </summary>
        public void ModifyDefaultComponentValues()
        {
            this.SuspendLayout();
            //Main Form
            this.ClientSize  = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
            this.Location    = new System.Drawing.Point(20, 100);
            this.MaximumSize = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
            this.MinimumSize = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
            
            //Instruction Label
            this.InstructionLabel.Location    = new System.Drawing.Point(100, 10);
            this.InstructionLabel.MinimumSize = new System.Drawing.Size(this.Width - 200, 80);
            this.InstructionLabel.Size        = new System.Drawing.Size(this.Width - 200, 80);
            this.InstructionLabel.Text        = "Select Slot";

            //Slot Select Page
            //PanelSlotButtons
            this.PanelSlotButtons.Location    = new System.Drawing.Point(100, 120);
            this.PanelSlotButtons.Size        = new System.Drawing.Size(this.Width - 200, this.Height - 190);
            this.PanelSlotButtons.MaximumSize = new System.Drawing.Size(this.Width - 200, this.Height - 110);
            this.PanelSlotButtons.MinimumSize = new System.Drawing.Size(this.Width - 200, this.Height - 110);

            //Charge Time Select Page
            this.PanelCharge.Location    = this.PanelSlotButtons.Location;
            this.PanelCharge.Size        = this.PanelSlotButtons.Size;
            this.PanelCharge.MaximumSize =  this.PanelSlotButtons.MaximumSize;
            this.PanelCharge.MinimumSize = this.PanelSlotButtons.MinimumSize;
            this.Button10Min.Location    = new System.Drawing.Point(PanelSlotButtons.Width / 4, 100);
            this.Button10Min.Click       += this.StandardChargeButton_Click;
            this.Button20Min.Location    = new System.Drawing.Point(200 + PanelSlotButtons.Width / 4, 100);
            this.Button20Min.Click       += this.StandardChargeButton_Click;
            this.Button30Min.Location    = new System.Drawing.Point(400 + PanelSlotButtons.Width / 4, 100);
            this.Button30Min.Click       += this.StandardChargeButton_Click;
            this.Button40Min.Location    = new System.Drawing.Point(PanelSlotButtons.Width / 4, 240);
            this.Button40Min.Click       += this.StandardChargeButton_Click;
            this.Button50Min.Location    = new System.Drawing.Point(200 + PanelSlotButtons.Width / 4, 240);
            this.Button50Min.Click       += this.StandardChargeButton_Click;
            this.Button60Min.Location    = new System.Drawing.Point(400 + PanelSlotButtons.Width / 4, 240);
            this.Button60Min.Click       += this.StandardChargeButton_Click;
            this.ButtonCustom.Location   = new System.Drawing.Point(PanelSlotButtons.Width / 4, 400);
            this.ButtonCustom.Click      += this.CustomChargeButton_Click;
            this.TextCustom.Location     = new System.Drawing.Point(170 + PanelSlotButtons.Width / 4, 400);
            this.ButtonProceed.Location  = new System.Drawing.Point(PanelSlotButtons.Width / 4, 520);
            this.ButtonProceed.Click     += this.ButtonProceed_Click;
            this.LabelChargeRatesInfo.Location = new System.Drawing.Point(this.Width / 4, 20);

            //PanelPayment
            this.PanelPayment.Location    = this.PanelSlotButtons.Location;
            this.PanelPayment.Size        = this.PanelSlotButtons.Size;
            this.PanelPayment.MaximumSize = this.PanelSlotButtons.MaximumSize;
            this.PanelPayment.MinimumSize = this.PanelSlotButtons.MinimumSize;
    
            //LabelFeedback
            this.LabelFeedback.Location = new System.Drawing.Point(PanelSlotButtons.Width / 4, 30);
            this.LabelFeedback.MaximumSize = new System.Drawing.Size(600, 600);
            this.LabelFeedback.MinimumSize = new System.Drawing.Size(600, 600);
            this.LabelFeedback.Size = new System.Drawing.Size(600, 600);

            //PanelNumPad
            this.PanelNumPad.Location = new System.Drawing.Point(PanelSlotButtons.Width / 4, 240);
            this.NumPad1.Click += this.NumPadNum_Click;
            this.NumPad2.Click += this.NumPadNum_Click;
            this.NumPad3.Click += this.NumPadNum_Click;
            this.NumPad4.Click += this.NumPadNum_Click;
            this.NumPad5.Click += this.NumPadNum_Click;
            this.NumPad6.Click += this.NumPadNum_Click;
            this.NumPad7.Click += this.NumPadNum_Click;
            this.NumPad8.Click += this.NumPadNum_Click;
            this.NumPad9.Click += this.NumPadNum_Click;
            this.NumPadEnter.Click += this.NumPadEnter_Click;
            this.NumPadBack.Click += this.NumPadBack_Click;
            this.NumPadClear.Click += this.NumPadClear_Click;

            this.ResumeLayout(false);
            this.PerformLayout();
           
        }

        /// <summary>
        ///  ButtonProceed is present in the PanelCharge page which captures the charging time for the user
        ///  When pressed the handler sets the ChargeTime and hands over control to change to the next page.
        /// </summary>
        /// <param name="sender">standard sender object which is ButtonProceed in this case</param>
        /// <param name="e"> standard event argument in this case it is Clicked</param>
        /// <todo>Interface with Vending Machine class to control progression to next page and to 
        /// communicate the selected charge time</todo>
        private void ButtonProceed_Click(object sender, EventArgs e)
        {
            if (ChargeTime > 0)
            {
                this.InstructionLabel.ForeColor = System.Drawing.Color.Navy;
                this.InstructionLabel.Text = "Processing Payment";
                this.LabelFeedback.Text = "Insert R10";
                this.PanelSlotButtons.Enabled = false;
                this.PanelSlotButtons.Visible = false;
                this.PanelSlotButtons.SendToBack();
                this.PanelCharge.Enabled = false;
                this.PanelCharge.Visible = false;
                this.PanelCharge.SendToBack();
                this.PanelPayment.Enabled = true;
                this.PanelPayment.Visible = true;
                this.PanelPayment.BringToFront();
                this.PanelNumPad.Enabled = false;
                this.PanelNumPad.Visible = false;
                this.PanelNumPad.SendToBack();
            }
            else
            {
                this.InstructionLabel.ForeColor = System.Drawing.Color.Red;
                this.InstructionLabel.Text = "Charging Time NOT Set!";
            }
        }

        /// <summary>
        ///  CustomChargeButton  is present in the PanelCharge page which captures the charging time for the user.
        ///  When pressed the handler loads the NumPad panel to allow the user to select custom charge time
        /// </summary>
        /// <param name="sender">standard sender object which is ustomChargeButton in this case</param>
        /// <param name="e"> standard event argument in this case it is Clicked</param>
        private void CustomChargeButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            //Launch New Form
            this.InstructionLabel.ForeColor = System.Drawing.Color.Navy;
            this.InstructionLabel.Text = "Enter Custom Time";
            this.PanelNumPad.Enabled = true;
            this.PanelNumPad.Visible = true;
            this.PanelNumPad.BringToFront();
            this.PanelCharge.Visible = false;
            //Also assign charge time variable
            ChargeTime = 0;
        }
        /// <summary>
        ///  StandardChargeButton is present in the PanelCharge page which captures the charging time for the user.
        ///  When pressed the handler chooses the charging time based on the button pressed and thrn sets the ChargeTime 
        ///  variable.
        /// </summary>
        /// <param name="sender">standard sender object which is StandardChargeButton in this case</param>
        /// <param name="e"> standard event argument in this case it is Clicked</param>
        private void StandardChargeButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            this.InstructionLabel.ForeColor = System.Drawing.Color.Navy;
            this.InstructionLabel.Text = "Charging Time is " + btn.Text + "\r\nClick Proceed";

            //Assign charging time
            switch (btn.Text)
            {
                case "10 Min":
                    ChargeTime = 10;
                    break;
                case "20 Min":
                    ChargeTime = 10;
                    break;
                case "30 Min":
                    ChargeTime = 10;
                    break;
                case "40 Min":
                    ChargeTime = 10;
                    break;
                case "50 Min":
                    ChargeTime = 10;
                    break;
                case "60 Min":
                    ChargeTime = 10;
                    break;
                default:
                    ChargeTime = 0;
                    break;

            }
            //Also assign charge time variable
        }
        /// <summary>
        ///  SlotButton is present in the PanelSlotButtons panel page which captures which slot the user selected.
        ///  When pressed the handler captures the selected slot and determines whether the user wants to charge or
        ///  retrieve a device from it. The handler then hands over control to change to the next page.
        /// </summary>
        /// <param name="sender">standard sender object which is ButtonProceed in this case</param>
        /// <param name="e"> standard event argument in this case it is Clicked</param>
        /// <todo>Interface with Vending Machine class to control progression to next page and to 
        /// communicate the selected slot</todo>
        private void SlotButton_Click(object sender, EventArgs e)
        {
            this.InstructionLabel.ForeColor = System.Drawing.Color.Navy;
            this.InstructionLabel.Text = "Select Charging Time";
            /*for (int i = 0; i < 26; i++)
            {
                if (SlotButtonList.ElementAt(i).Focused)
                {
                    SlotButtonList.ElementAt(i).BackColor = System.Drawing.Color.Red;
                }
            }*/
            Button btn = sender as Button;
            btn.BackColor = System.Drawing.Color.Red;
            this.PanelSlotButtons.Enabled = false;
            this.PanelSlotButtons.Visible = false;
            this.PanelSlotButtons.SendToBack();
            this.PanelCharge.Enabled = true;
            this.PanelCharge.Visible = true;
            this.PanelCharge.BringToFront();
            this.PanelPayment.Enabled = false;
            this.PanelPayment.Visible = false;
            this.PanelPayment.SendToBack();
            this.PanelPayment.Enabled = false;
            this.PanelPayment.Visible = false;
            this.PanelPayment.SendToBack();
            this.PanelNumPad.Enabled = false;
            this.PanelNumPad.Visible = false;
            this.PanelNumPad.SendToBack();
            
        }
        /// <summary>
        ///  Standard form load handler.The form load hanlder sets the PanelSlotButtons panel to the active 
        ///  panel and makes the rest inactive
        /// </summary>
        /// <param name="sender">standard sender object</param>
        /// <param name="e"> standard event argument</param>
        private void Main_Load(object sender, EventArgs e)
        {
            this.PanelSlotButtons.Enabled = true;
            this.PanelSlotButtons.Visible = true;
            this.PanelSlotButtons.BringToFront();
            this.PanelCharge.Enabled = false;
            this.PanelCharge.Visible = false;
            this.PanelCharge.SendToBack();
            this.PanelPayment.Enabled = false;
            this.PanelPayment.Visible = false;
            this.PanelPayment.SendToBack();
            this.PanelPayment.Enabled = false;
            this.PanelPayment.Visible = false;
            this.PanelPayment.SendToBack();
        }

        /// <summary>
        ///  NumPadNum is a generic handler for the numeric buttons in PanelNumPad. 
        ///  When pressed the handler calculates the custom charging time.
        /// </summary>
        /// <param name="sender">standard sender object which is NumPadx in this case</param>
        /// <param name="e"> standard event argument in this case it is Clicked</param>
        /// <todo>Interface with Vending Machine class to determine whether to calculate/determine charging time or password</todo>
        private void NumPadNum_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            this.InstructionLabel.ForeColor = System.Drawing.Color.Navy;
            this.InstructionLabel.Text = "Select Charging Time";  
            ChargeTime = ChargeTime * 10 + int.Parse(btn.Text);
            this.NumPadText.Text = ChargeTime.ToString();
        }
        /// <summary>
        ///  NumPadBack is a handler in PanelNumPad. 
        ///  When pressed the handler backspaces the current charge time..
        /// </summary>
        /// <param name="sender">standard sender object which is NumPadBack in this case</param>
        /// <param name="e"> standard event argument in this case it is Clicked</param>
        /// <todo>Interface with Vending Machine class to determine whether to calculate/determine charging time or password</todo>
        private void NumPadBack_Click(object sender, EventArgs e)
        {
            ChargeTime /= 10;
            this.InstructionLabel.ForeColor = System.Drawing.Color.Navy;
            this.InstructionLabel.Text = "Select Charging Time";
            this.NumPadText.Text = ChargeTime.ToString();
        }
        /// <summary>
        ///  NumPadClear is a handler in PanelNumPad. 
        ///  When pressed the handler clears the current charge time..
        /// </summary>
        /// <param name="sender">standard sender object which is NumPadClear in this case</param>
        /// <param name="e"> standard event argument in this case it is Clicked</param>
        /// <todo>Interface with Vending Machine class to determine whether to clear charging time or password</todo>
        private void NumPadClear_Click(object sender, EventArgs e)
        {
            ChargeTime = 0;
            this.InstructionLabel.ForeColor = System.Drawing.Color.Navy;
            this.InstructionLabel.Text = "Select Charging Time";  
            this.NumPadText.Text = ChargeTime.ToString();
        }
        /// <summary>
        ///  NumPadEnter is a handler in PanelNumPad. 
        ///  When pressed the handler captures the current charge time..
        /// </summary>
        /// <param name="sender">standard sender object which is NumPadEnter in this case</param>
        /// <param name="e"> standard event argument in this case it is Clicked</param>
        /// <todo>Interface with Vending Machine class to determine whether to calculate/determine charging time or password</todo>
        private void NumPadEnter_Click(object sender, EventArgs e)
        {
            if (ChargeTime == 0)
            {
                this.InstructionLabel.Text = "Custom charging time is not set!";
                this.InstructionLabel.ForeColor = System.Drawing.Color.Red;
            }
            else if (ChargeTime > 60)
            {
                this.InstructionLabel.Text = "Custom charging time is too big!";
                this.InstructionLabel.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                this.InstructionLabel.ForeColor = System.Drawing.Color.Navy;
                this.InstructionLabel.Text = "Click Proceed";
                this.TextCustom.Text = ChargeTime.ToString();
                this.PanelNumPad.Enabled = false;
                this.PanelNumPad.Visible = false;
                this.PanelNumPad.SendToBack();
                this.PanelCharge.Visible = true;
            }
        }
    }
}
