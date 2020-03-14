using System;
using System.Net.NetworkInformation;
using System.Net;
using System.Linq;
using System.Net.Sockets;

namespace mdh_code
{
    class NetworkRW
    {
        /// <summary>
        /// Returns the MAC address of the NIC, excluding the loopback device
        /// </summary>
        /// <returns>
        /// A string representing the current machines MAC Address
        /// </returns>
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

        /// <summary>
        /// Returns the IPv4 Address of the local machine
        /// </summary>
        /// <returns>
        /// The Local IPv4 Address
        /// </returns>
        public string ReturnIP()
        {
            // declare holder string
            string ip4address = "";

            // iterrate through our addresses on the local machine
            foreach (IPAddress ipa in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork) // if it's ipv4 assign it
                {
                    ip4address = ipa.ToString();
                    break;
                }
            }

            return ip4address;
        }
    }
}