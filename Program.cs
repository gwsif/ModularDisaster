using System;
using System.IO;


namespace mdh_code
{
    class Program
    {
        static void Main(string[] args)
        {
            //Run the datareader!
            RetrieveData rd = new RetrieveData();
            rd.ReadLevels();
            
        }
    }
}
