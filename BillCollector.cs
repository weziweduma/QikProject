using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Timers;
using Qik;



public enum BillCollector_Response_type
{
    OK = 0xF0,
    CommandUnkown = 0xF2,
    WrongNoParameters = 0xF3,
    Parameres = 0xF4,
    CouldNotProcess = 0xF5,
    SoftwareError = 0xF6,
    Fail = 0xF8,
    KeyNotSet = 0xFA
};


class BillCollector
{
    public SerialPort BillCollector_SP;
    public SerialPorts NVSERPORT_class;
    private static System.Timers.Timer BillCollector_pollingTimer;
    private static int polling_rate = 200;

    Machine_Setup Settings = new Machine_Setup();

    public int Amount_Received = 0;
    public bool bill_received = false;

    public byte RX_data_STX;
    public byte RX_data_SEQ;
    public byte RX_data_LEN;
    public byte RX_data_REPLY;
    public byte[] RX_data_DATA;
    public byte SEQ;
    public int len;

    enum BillCollector_Events
    {
        SlaveReset = 0xF1,
        Read = 0xEF,
        NoteCredit = 0xEE,
        Rejecting = 0xED,
        Rejected = 0xEC,
        Stacking = 0xCC,
        Stacked = 0xEB,
        SafeJAM = 0xEA,
        UnsafeJAM = 0xE9,
        Disabled = 0xE8,
        FraudAttempt = 0xE6,
        StackerFull = 0xE7,
        NoteCleared_CashB = 0xE2,
    };

    enum BillCollector_Commands
    {
        Sync = 0x11,
        Reset = 0x01,
        HostProtVer = 0x06,
        Poll = 0x07,
        GetSer = 0x0C,
        Disable = 0x09,
        Enable = 0x0A,
        GetFWver = 0x20,
        GetDataset = 0x21,
        SetInhibits = 0x02,
        BezelDispON = 0x03,
        BezelDispOFF = 0x04,
        SetupRequest = 0x05,
        Reject = 0x08,
        UintDATA = 0x0D,
        ChannelValueData = 0x0E,
        ChannelSecurityData = 0x0F,
        ChannelReteachData = 0x10,
        LastRejectCode = 0x17,
        Hold = 0x18,
        PollWithAck = 0x56,
        EventAck = 0x57,
        SetGenerator = 0x4A,
        SetModulus = 0x4B,
        RequestKeyExhange = 0x4C,
        SSP_EncrypytKey = 0x60,
        SSPEncyptResettoDefault = 0x61
    };

    enum BillCollector_ZAR_Bills
    {
        ZAR_R10 = 0x01,
        ZAR_R20 = 0x02,
        ZAR_R50 = 0x03,
        ZAR_R100 = 0x04,
        ZAR_R200 = 0x05
    };


    public bool BillCollectorsetupComms()
    {

        
        NVSERPORT_class = new SerialPorts();
        BillCollector_SP = new SerialPort();
        return NVSERPORT_class.Setup_Port(BillCollector_SP, Settings.Bill_Collector_Port, 9600, 8, 2, 2000);
    }

    public bool BillCollectorCommsClose()
    {
        return NVSERPORT_class.SerialClose(BillCollector_SP);
    }

    public void BillCollector_Send(byte[] data)
    {
        int len = data.Length, i = 0, index = 0;
        ushort crc_word;
        byte[] checksum_bytes = new byte[len + 2];
        Crc16 crc = new Crc16();

        byte CRCL, CRCH;

        checksum_bytes[index++] = SEQ;
        checksum_bytes[index++] = (byte)len;


        for (i = 0; i < len; i++)
        {
            checksum_bytes[index++] = data[i];
        }

        crc_word = crc.Calc_CRC(checksum_bytes);

        CRCL = (byte)(crc_word & 0x00FF);
        CRCH = (byte)((crc_word & 0xFF00) >> 8);

        BillCollector_SendByte(0x7F);
        for (i = 0; i < checksum_bytes.Length; i++)
        {
            BillCollector_SendByte(checksum_bytes[i]);
        }
        BillCollector_SendByte(CRCL);
        BillCollector_SendByte(CRCH);

        if (SEQ == 0x00) SEQ = 0x80;
        else if (SEQ == 0x80) SEQ = 0x00;

        if (len == 1 && data[0] == 0x11) SEQ = 0x00;
    }

    public void BillCollectorplus_Reset()
    {
        BillCollector_Send(new byte[1] { (byte)BillCollector_Commands.Reset });
    }

    public void BillCollectorplus_Sync()
    {
        SEQ = 0x00;

        BillCollector_Send(new byte[1] { (byte)BillCollector_Commands.Sync });
        BillCollector_ReceiveByte();
    }

    public void BillCollectorplus_SetProtocol(byte protocol)
    {
        byte[] NVProt = new byte[2];
        NVProt[0] = NVProt[0] = (byte)BillCollector_Commands.HostProtVer;
        NVProt[1] = protocol;
        BillCollector_Send(NVProt);
        BillCollector_ReceiveByte();
    }

    public void Setup_BillCollectorplus()
    {
        BillCollectorplus_Sync();
        BillCollectorplus_Reset();
        BillCollectorplus_SetProtocol(0x04);
    }

    public void BillCollector_Enable()
    {
        BillCollector_Send(new byte[1] { (byte)BillCollector_Commands.Enable });
        BillCollector_ReceiveByte();
    }

    public void BillCollector_Disable()
    {
        BillCollector_Send(new byte[1] { (byte)BillCollector_Commands.Disable });
        BillCollector_ReceiveByte();
    }


    public void BillCollector_Bezel_ON()
    {
        BillCollector_Send(new byte[1] { (byte)BillCollector_Commands.BezelDispON });
        BillCollector_ReceiveByte();
    }

    public void BillCollector_Bezel_OFF()
    {
        BillCollector_Send(new byte[1] { (byte)BillCollector_Commands.BezelDispOFF });
        BillCollector_ReceiveByte();
    }

    public void BillCollector_getFW()
    {
        BillCollector_Send(new byte[1] { (byte)BillCollector_Commands.GetFWver });
        BillCollector_ReceiveByte();
    }

    public void BillCollector_getSerial()
    {
        BillCollector_Send(new byte[1] { (byte)BillCollector_Commands.GetSer });
        BillCollector_ReceiveByte();
    }

    public void BillCollector_getDatasetVer()
    {
        BillCollector_Send(new byte[1] { (byte)BillCollector_Commands.GetDataset });
        BillCollector_ReceiveByte();
    }

    public void BillCollector_SetupRequest()
    {
        BillCollector_Send(new byte[1] { (byte)BillCollector_Commands.SetupRequest });
        BillCollector_ReceiveByte();
    }

    public void BillCollector_GetChannelData()
    {
        BillCollector_Send(new byte[1] { (byte)BillCollector_Commands.ChannelValueData });
        BillCollector_ReceiveByte();
    }

    public void BillCollector_ChannelSecurityData()
    {
        BillCollector_Send(new byte[1] { (byte)BillCollector_Commands.ChannelSecurityData });
        BillCollector_ReceiveByte();
    }

    public void BillCollector_EnableAllChannels()
    {
        byte[] enableAll = new byte[3];

        enableAll[0] = (byte)BillCollector_Commands.SetInhibits;
        enableAll[1] = 0xFF;
        enableAll[2] = 0xFF;
        BillCollector_Send(enableAll);
        BillCollector_ReceiveByte();
    }

    public void BillCollector_POLL()
    {
        BillCollector_Send(new byte[1] { (byte)BillCollector_Commands.Poll });
        BillCollector_ReceiveByte();
    }

    public void BillCollectorflashBezel(byte SEQ, int rate_ms, int num_times)
    {

        for (int i = 0; i < num_times; i++)
        {
            BillCollector_Bezel_ON();
            Thread.Sleep(rate_ms);
            BillCollector_Bezel_OFF();
            Thread.Sleep(rate_ms);
        }
        BillCollector_Bezel_ON();
    }


    public void BillCollector_ReceiveByte()
    {
        byte[] rx_data = new byte[100];
        RX_data_DATA = new byte[100];

        rx_data = NVSERPORT_class.SerialReceiveByte(BillCollector_SP, 6);

        if (rx_data.Length > 0 && rx_data != null)
        {
            len = rx_data.Length;
            RX_data_STX = (byte)rx_data[0];
            RX_data_SEQ = (byte)rx_data[1];
            RX_data_LEN = (byte)rx_data[2];
            RX_data_REPLY = (byte)rx_data[3];
            for (int i = 4; i < len; i++) { RX_data_DATA[i - 4] = (byte)rx_data[i]; }
        }
    }

    public void BillCollector_SendByte(byte data)
    {
        NVSERPORT_class.SerialSend(BillCollector_SP, data);
    }

    public void BillCollector_SetupPolling()
    {
        BillCollector_pollingTimer = new System.Timers.Timer(polling_rate);
        BillCollector_pollingTimer.Elapsed += BillCollector_polling_event;
        BillCollector_pollingTimer.AutoReset = true;
    }

    public void BillCollector_Start_polling()
    {
        BillCollector_pollingTimer.Enabled = true;
    }

    public void BillCollector_Stop_polling()
    {
        BillCollector_pollingTimer.Enabled = false;
    }

    public void BillCollector_polling_event(object source, ElapsedEventArgs e)
    {

        BillCollector_POLL();
        if (RX_data_LEN > 1)
        {
            switch (RX_data_DATA[0])
            {
                case (int)BillCollector_Events.SlaveReset:
                    break;
                case (int)BillCollector_Events.Read:
                    break;
                case (int)BillCollector_Events.NoteCredit:
                    Console.Write("Cash Received => ");
                    if (RX_data_DATA[1] == (byte)BillCollector_ZAR_Bills.ZAR_R10) { Console.WriteLine("R10 Received"); Amount_Received += 10; bill_received = true; }
                    else if (RX_data_DATA[1] == (byte)BillCollector_ZAR_Bills.ZAR_R20) { Console.WriteLine("R20 Received"); Amount_Received += 20; bill_received = true; }
                    else if (RX_data_DATA[1] == (byte)BillCollector_ZAR_Bills.ZAR_R50) { Console.WriteLine("R10 Received"); Amount_Received += 50; bill_received = true; }
                    else if (RX_data_DATA[1] == (byte)BillCollector_ZAR_Bills.ZAR_R100) { Console.WriteLine("R10 Received"); Amount_Received += 100; bill_received = true; }
                    else if (RX_data_DATA[1] == (byte)BillCollector_ZAR_Bills.ZAR_R200) { Console.WriteLine("R10 Received"); Amount_Received += 200; bill_received = true; }
                    break;
                case (int)BillCollector_Events.Rejecting:
                    break;
                case (int)BillCollector_Events.Rejected:
                    break;
                case (int)BillCollector_Events.Stacking:
                    break;
                case (int)BillCollector_Events.Stacked:
                    break;
                case (int)BillCollector_Events.SafeJAM:
                    break;
                case (int)BillCollector_Events.UnsafeJAM:
                    break;
                case (int)BillCollector_Events.Disabled:
                    break;

                case (int)BillCollector_Events.FraudAttempt:
                    break;
            }

        }
    }
}




