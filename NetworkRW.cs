using System;
using System.Net.NetworkInformation;
using System.Net;

namespace mdh_code
{
    class NetworkRW
    {
        public string ReturnMAC()
        {
            // Declare some holder values
            string nictype = "";
            string m_addr = "";

            // For every NIC found, loop through and print the MAC address
            // that isn't the loopbackdevice.
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                               
                // Assign the Type
                nictype = nic.NetworkInterfaceType.ToString();

                // If the type is not a loopback, then get it's MAC Address
                if (nictype != "Loopback")
                {
                    //Console.WriteLine("Found MAC Address: " + m_addr);
                    //Console.WriteLine("Type: " + nictype);
                    //Console.WriteLine("Hostname: " + System.Environment.MachineName.ToString());
                    m_addr = nic.GetPhysicalAddress().ToString();

                }
            }

            return m_addr;
        }

        public string ReturnIP()
        {
            string hostname = Dns.GetHostName();

            IPHostEntry ipe = Dns.GetHostEntry(hostname);

            IPAddress[] addr = ipe.AddressList;

            return addr[addr.Length - 1].ToString();
        }
    }
}