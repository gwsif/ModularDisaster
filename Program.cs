using System;
using System.IO;


namespace mdh_code
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate our DataReader
            RetrieveData rd = new RetrieveData();

            // Create the holder files if they don't exist
            rd.CreateFiles();

            // Read our levels!
            rd.ReadLevels();

            // Print out the local Network Device MAC address and IPv6 Address
            NetworkRW rw = new NetworkRW();
            Console.WriteLine(rw.ReturnMAC()); // returns the MAC address of the current machine
            Console.WriteLine(rw.ReturnIP());  // returns the IPv6 address of the current machine
        }
    }
}
