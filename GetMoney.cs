using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class MoneyProcessing
    {
        public bool Start_Polling = false;
        public int AmountPayed;
        BillCollector Bill_Collector_machine = new BillCollector();
        CoinAcceptor CoinAcceptingMachine = new CoinAcceptor();
        public bool successful_receipt = false;
        

        public void Start_Polling_Money_Devices()
        {
            Bill_Collector_machine.BillCollector_SetupPolling();
            CoinAcceptingMachine.CoinAcceptor_SetupPolling();
            Bill_Collector_machine.BillCollectorplus_Sync();
            Bill_Collector_machine.BillCollectorplus_SetProtocol(0x04);
            Bill_Collector_machine.BillCollector_EnableAllChannels();
            Bill_Collector_machine.BillCollector_Enable();
            Bill_Collector_machine.BillCollector_Bezel_ON();
            Bill_Collector_machine.BillCollector_Start_polling();
            CoinAcceptingMachine.CoinAcceptor_Start_polling();
        }

        public void GetPayment()
        {
            if ((Bill_Collector_machine.bill_received || CoinAcceptingMachine.CoinReceived))// && payment_timeout != 0)
            {
                AmountPayed = Bill_Collector_machine.Amount_Received + CoinAcceptingMachine.Amount_Received;
                successful_receipt = true;
            }          
        }
    }

