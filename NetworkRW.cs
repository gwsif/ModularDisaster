using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.IO;
using System.Collections.Generic;

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

        /// <Summary>
        /// Scans the network for IP Addresses and MAC Addresses and prints them out
        /// </Summary>
        /// <Returns>
        /// void
        /// </Summary>
        public void TCPScan()
        {
            // run nmap
            string cmd = "nmap -sP 192.168.1.0/24 | awk '/is up/ {print up}; {gsub (/\\(|\\)/,\"\"); up = $NF}'";
            var output = cmd.ExecBash();

            // Create a List for the ips
            List<string> iplist = new List<string>();

            // Create a List for the MACs
            List<string> maclist = new List<string>();

            // Split the outputs and add them to the list
            iplist = output.Split('\n').ToList();

            // Remove the last line of the list (because it's always blank)
            if(iplist.Any())
            {
                iplist.RemoveAt(iplist.Count - 1);
            }

            // For each ip in the list, ping it to get the MAC address then add to the list
            foreach (var address in iplist)
            {
                string getresult = "arping -c 1 " + address + " | grep -o -E \'([[:xdigit:]]{1,2}:){5}[[:xdigit:]]{1,2}\'"; // use grep to pull the MAC
                var result = getresult.ExecBash();
                
                if (result == "") //if no result, tell that it's not available
                {
                    result = "Unavailable";
                }
                maclist.Add(result); // add it to the list               
            }

            //Show entries in the lists together
            for (var i = 0; i < iplist.Count; i++)
            {
                Console.WriteLine("FOUND!");
                Console.WriteLine("IP: " + iplist[i] + " MAC: " + maclist[i]); //Show on same line
            }

            /* shove into file
            FileStream fs = File.Create("ips");
            fs.Close();

            StreamWriter sw = new StreamWriter("ips");
            sw.WriteLine(output);
            sw.Close();
            */
            
            // Associate 

            // print the output
            //Console.WriteLine(iplist[0]);

        }
    }
}