using System.Diagnostics;
using System.IO.Ports;

namespace Arduino_AutoBrightness
{
    public static class Arduino
    {
        public static SerialPort serialPort = new SerialPort();

        public static void Connect(string portName)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            else
            {
                serialPort.PortName = portName;
                serialPort.BaudRate = 9600;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;
                serialPort.Parity = Parity.None;
                serialPort.Open();
            }
        }

        public static string ReadLine()
        {
            return serialPort.ReadLine();
        }

        public static void Disconnect()
        {
            if (serialPort.IsOpen)
                serialPort.Close();
        }

        public static bool IsConnected()
        {
            return serialPort.IsOpen;
        }

        public static void AttachDataReceivedEvent(SerialDataReceivedEventHandler eventHandler)
        {
            serialPort.DataReceived += eventHandler;
        }
    }
}
