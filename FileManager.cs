using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qik
{
    /// <summary>
    /// This class provides a means to store and retrieve the status of the
    /// VendingMachine and to log events to a log file.
    /// </summary>
    public class FileManager
    {

        /// <summary>
        /// URL/Filename of the file to log to.
        /// </summary>
       // private File LogFile;
        /// <summary>
        /// URL/File name of the file to store the vending machine status to.
        /// </summary>
      //  private File StatusFile;



        ~FileManager()
        {

        }

        public FileManager()
        {

        }

        /// <summary>
        /// Close the file with file descriptor Fd.
        /// </summary>
        /// <param name="Fd"></param>
        public bool close(int Fd)
        {

            return false;
        }

        /// <summary>
        /// Retrieve the name of the Log file
        /// </summary>
        public bool logFileRetreive()
        {

            return false;
        }

        /// <summary>
        /// Write new log data to the log file
        /// </summary>
        public void logFileUpdate()
        {

        }

        /// <summary>
        /// Open the file.
        /// </summary>
        /// <param name="File"></param>
        public int open(string File)
        {

            return 0;
        }

        /// <summary>
        /// Read the contents of the power up file (vending machine status file)
        /// </summary>
        public bool powerUpFileRetrieve()
        {

            return false;
        }

        /// <summary>
        /// Update the contents of the power up file (vending machine status file)
        /// </summary>
        public bool powerUpFileUpdate()
        {

            return false;
        }

        /// <summary>
        /// Read from the file descriptor and store lines in message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="Fd"></param>
        public int read(string message, int Fd)
        {

            return 0;
        }

        /// <summary>
        /// Write data/text to the file.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="Fd"></param>
        public bool write(string message, int Fd)
        {

            return false;
        }

    }//end FileManager
}
