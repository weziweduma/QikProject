using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

class SerialPorts
{
    public int length_received;
   
    public bool Setup_Port(SerialPort SerObj, string port_name, int baud_rate, int data_bits, int num_stop_bits, int timeout)
    {
        // Allow the user to set the appropriate properties.


        SerObj.PortName = port_name;
        SerObj.BaudRate = baud_rate;
        SerObj.DataBits = data_bits;

        if (num_stop_bits == 1) SerObj.StopBits = StopBits.One;
        else SerObj.StopBits = StopBits.Two;

        SerObj.Parity = Parity.None;
        // Set the read/write timeouts
        SerObj.ReadTimeout = timeout;
        SerObj.WriteTimeout = timeout;

        if (SerObj.IsOpen) SerObj.Close();
        SerObj.Open();

        if (SerObj.IsOpen) { MessageBox.Show("port open"); return true; }
        else return false;
    }

    public void SerialSend(SerialPort SerObj, byte data)
    {
        byte[] data_array = new byte[1];
        data_array[0] = data;
        if (SerObj.IsOpen) SerObj.Write(data_array, 0, 1);
    }


    public byte[] SerialReceiveByte(SerialPort SerObj, int min_buffer_len)
    {
        byte[] bytes_received = new byte[100];

        int timeout_flag = 0;

        try
        {
            while (SerObj.BytesToRead < min_buffer_len) { };
            length_received = SerObj.BytesToRead;
            for (int i = 0; i < length_received; i++)
            {
                bytes_received[i] = (byte)SerObj.ReadByte();
                timeout_flag = 0;
            }
        }
        catch (TimeoutException) { Console.WriteLine("TimeOut"); timeout_flag = 1; }

        if (timeout_flag == 0) return bytes_received;
        else return null;
    }

    public bool SerialClose(SerialPort SerObj)
    {
        SerObj.Close();
        if (!SerObj.IsOpen) return true;
        else return false;
    }


}
