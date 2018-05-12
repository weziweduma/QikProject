using System;

public class Crc16
{
    public ushort Calc_CRC(byte[] data)
    {
        int crc = 0xFFFF;
        int j,ii = 0;
        int i,len;

        len = data.Length;
        
        for (j = len; j > 0; j--)
        {
            crc ^= (data[ii] << 8);
            for (i = 0; i < 8; i++)
            {
                if ((crc & 0x8000) > 1) crc = ((crc << 1) ^ 0x8005);
                else crc <<= 1;
            }
            ii++;
        }
        return (ushort)crc;
    } 
}