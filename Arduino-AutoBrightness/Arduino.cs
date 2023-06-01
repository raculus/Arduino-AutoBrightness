using System.IO.Ports;

namespace Arduino_AutoBrightness
{
    public class Arduino
    {
        public static SerialPort serialPort = new SerialPort();

        public static void Connect(string portName, int baudRate = 9600, int dataBits = 8, StopBits stopBits = StopBits.One, Parity parity = Parity.None)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            else
            {
                serialPort.PortName = portName;
                serialPort.BaudRate = baudRate;
                serialPort.DataBits = dataBits;
                serialPort.StopBits = stopBits;
                serialPort.Parity = parity;
            }
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
