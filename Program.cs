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
            
        }
    }
}
