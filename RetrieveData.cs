using System;
using System.IO;
using System.Linq;

namespace mdh_code
{
    class RetrieveData
    {
        public void ReadLevels()
        {
            // Parameters
            double water = 0.0;
            double sewage = 0.0 ;
            double power = 0.0;

            /*Read the contents of each dummy file
                NOTE: Each value has a separate file.
                This is for testing live changes by echoing
                the test value and overwriting. For example
                echo "0.9" >> water
            */
            
            // Create our arrays
            string[] wLine = File.ReadLines("water").ToArray();
            string[] sLine = File.ReadLines("sewage").ToArray();
            string[] pLine = File.ReadLines("power").ToArray();

            // Assign those values
            water = Convert.ToDouble(wLine[0]);
            sewage = Convert.ToDouble(sLine[0]);
            power = Convert.ToDouble(pLine[0]);

            // Check if valid
            CheckValid(water, "WATER");
            CheckValid(sewage, "SEWAGE");
            CheckValid(power, "POWER");

            // Echo These back for test purposes;
            Console.WriteLine("Water: " + water);
            Console.WriteLine("Sewage: " + sewage);
            Console.WriteLine("Power: " + power);
        }

        public void CheckValid(double in_Value, string attr)
        {
            // if values are not within bounds - echo an error code
            //   and issue it to error log with a timestamp

            String timestamp = DateTime.Now.ToString();

            if(in_Value < 0.0 || in_Value > 1.0) 
            {
                Console.WriteLine("E001 - " + attr + " level not within bounds!" + "\n");

                using (StreamWriter sw = File.AppendText("log.txt"))
                {
                    sw.WriteLine(timestamp + " E001 - " + attr + " level not within bounds!");
                    sw.Close();

                }
            }

        }
    }

}
