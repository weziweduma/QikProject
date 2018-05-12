using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Timers;
using Qik;


class CoinAcceptor
{
    public SerialPort CoinAcceptor_SP;
    public SerialPorts CoinAcceptor_class;
    private static System.Timers.Timer CoinAcceptor_pollingTimer;
    private static int polling_rate = 250;
    private byte[] CoinAcceptor_received_amount;
    public int Amount_Received = 0;
    public bool CoinReceived = false;

    Machine_Setup Settings = new Machine_Setup();

    enum AcceptedCoins
    {
        ZAR_1Rand = 0x01,
        ZAR_2Rand = 0x02,
        ZAR_5Rand = 0x05,
    };

    public bool CoinAcceptorSetupComms()
    {
        CoinAcceptor_class = new SerialPorts();
        CoinAcceptor_SP = new SerialPort();
        return CoinAcceptor_class.Setup_Port(CoinAcceptor_SP, Settings.CoinAcceptor_Port, 4800, 8, 1, 500);
    }


    public bool CoinAcceptorCommsClose()
    {
        return CoinAcceptor_class.SerialClose(CoinAcceptor_SP);
    }

    public void CoinAcceptor_SetupPolling()
    {
        CoinAcceptor_pollingTimer = new System.Timers.Timer(polling_rate);
        CoinAcceptor_pollingTimer.Elapsed += CoinAcceptor_polling_event;
        CoinAcceptor_pollingTimer.AutoReset = true;
    }

    public void CoinAcceptor_Start_polling()
    {
        CoinAcceptor_pollingTimer.Enabled = true;
    }


    public void CoinAcceptor_polling_event(object source, ElapsedEventArgs e)
    {
        CoinAcceptor_received_amount = new byte[1];
        CoinAcceptor_received_amount = CoinAcceptor_class.SerialReceiveByte(CoinAcceptor_SP, 1);

        if (CoinAcceptor_received_amount == null) { }
        else
        {

            switch (CoinAcceptor_received_amount[0])
            {
                case (byte)AcceptedCoins.ZAR_1Rand:
                    Console.WriteLine("1 Rand Coin Received!!");
                    Amount_Received += 1;
                    CoinReceived = true;
                    break;
                case (byte)AcceptedCoins.ZAR_2Rand:
                    Console.WriteLine("2 Rand Coin Received!!");
                    Amount_Received += 2;
                    CoinReceived = true;
                    break;
                case (byte)AcceptedCoins.ZAR_5Rand:
                    Console.WriteLine("5 Rand Coin Received!!");
                    Amount_Received += 5;
                    CoinReceived = true;
                    break;
            }
        }
    }
}
