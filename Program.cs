using System;
using System.IO;

namespace mdh_code
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Command Arguments
            // Check for user input
            for(int i = 0; i <args.Length; i++)
            {
                if(args[i] == "--set-unit")
                {
                    ContextSwitch.SetAsUnit();
                }

                if(args[i] == "--set-town")
                {
                    ContextSwitch.SetAsTown();
                }

                if(args[i] == "--set-city")
                {
                    ContextSwitch.SetAsCity();
                }

                if(args[i] == "--get-mode")
                {
                    Console.WriteLine(ContextSwitch.GetMode());
                }

                if(args[i] == "--tcpscan")
                {
                    NetworkRW.TCPScan();
                }

                if(args[i] == "--gendb")
                {
                    SQLHelper.CreateDB();
                }

                if(args[i] == "--refresh-city")
                {
                    NetworkRW.FindCity();
                }
            }
            #endregion
        
            #region DeviceDefaults
            // If mode is not already set, then set it by default to unit mode
            if (!File.Exists("mode"))
            {
                ContextSwitch.SetAsDefault();
            }

            // Create our dummy files
            if (!File.Exists("water") || !File.Exists("sewage") || !File.Exists("power"))
            {
                RetrieveData.CreateFiles();
            }

            #endregion

            #region ModeDependants
            if (ContextSwitch.GetMode() == 0) // if unit
            {
                // We await a connection from the Town Control
                // by using the TCP Unit Server in Network Read/Write
                Console.WriteLine("Unit Mode Detected!");
                NetworkRW.TcpUnitServer();
            }

            if (ContextSwitch.GetMode() == 1) // if town
            {
                Console.WriteLine("Town Mode Detected!");
                
                // if the database doesn't exist, generate it and populate it
                if(!File.Exists("mdh.db"))
                {   
                    Console.WriteLine("Generating Database...");
                    SQLHelper.CreateDB(); // create the database
                    Console.WriteLine("Scanning Network...");
                    NetworkRW.TCPScan(); // populate the database
                    Console.WriteLine("Detecting City...");
                    NetworkRW.FindCity(); // find the city

                }

                // Initiate the TCP town client
                NetworkRW.TcpTownClient();
            }

            if (ContextSwitch.GetMode() == 2) // if city
            {
                Console.WriteLine("City Mode Detected!");

                // if the database doesn't exist yet generate it, but do not populate it.
                if(!File.Exists("mdh.db"))
                {
                    Console.WriteLine("Generating Database...");
                    SQLHelper.CreateDB(); // create the database

                    // initialize the city and await confirmation
                    if (NetworkRW.CitySetup() == true)
                    {
                        Console.WriteLine("City Setup Successful!");
                    }
                }

            }
            #endregion
        /*
            // Instantiate our DataReader
            RetrieveData rd = new RetrieveData();

            // Create the holder files if they don't exist
            rd.CreateFiles();

            // Read our levels!
            rd.ReadLevels();

            // Print out the local Network Device MAC address and IPv6 Address
            NetworkRW rw = new NetworkRW();

            Console.WriteLine("\n"); // newline for testing
            //Console.WriteLine("LOCAL MAC ADDRESS: " + rw.ReturnMAC()); // returns the MAC address of the current machine
            //Console.WriteLine("LOCAL IP: " + rw.ReturnIP());  // returns the IPv6 address of the current machine

            Console.WriteLine("Scanning Network...");
            rw.TCPScan(); // Run the TCPScan
        */
        }
    }
}
