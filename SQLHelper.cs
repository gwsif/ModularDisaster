using System;
using System.Data.SQLite;
using System.Collections.Generic;

namespace mdh_code
{
    public class SQLHelper
    {
        string command_string = "";
        string a_return = "";
        List<string> returnlist = new List<string>();
        Int64 a_return_int = 0;

        public SQLHelper (string command_string)
        {
            this.command_string = command_string;
        }

        public string Get_Out()
        {
            return a_return;
        }

        public List<string> Get_List()
        {
            return returnlist;
        }

        public void Run_Cmd()
        {
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=mdh.sqlite;Version=3;");
            m_dbConnection.Open();

            // Create our command
            SQLiteCommand command = new SQLiteCommand(command_string, m_dbConnection);

            // Execute our command
            command.ExecuteNonQuery();

            // Close the connection to the DB
            m_dbConnection.Close();
        }

        public void SetIPs()
        {
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=mdh.sqlite;Version=3");
            m_dbConnection.Open();

            // Create our command
            SQLiteCommand command = new SQLiteCommand(command_string, m_dbConnection);

            // Start the reader
            SQLiteDataReader reader = command.ExecuteReader();

            // While the reader has data to read, append the text to the areturn string
            while (reader.Read())
            {
                // Declare holder string
                string holder = "";
                
                // Grab the value
                holder = reader.GetString(0);

                // Add to our return list
                returnlist.Add(holder);
            }

            // Close db connection
            m_dbConnection.Close();
        }

        public static void CreateDB()
        {
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=mdh.sqlite;Version=3");
            m_dbConnection.Open();

            // Create our Tables
            string units_table = ("CREATE TABLE IF NOT EXISTS units (mac TEXT UNIQUE, ip TEXT UNIQUE, unit_id TEXT UNIQUE)");
            string stat_table = ("CREATE TABLE IF NOT EXISTS status (timestamp INTEGER NOT NULL, unit_id TEXT UNIQUE, w_level REAL, s_level REAL, "
                                 + "p_level REAL, PRIMARY KEY(timestamp), FOREIGN KEY(unit_id) REFERENCES units(unit_id))");
            
            SQLiteCommand makeUnits = new SQLiteCommand(units_table, m_dbConnection);
            makeUnits.ExecuteNonQuery();
            
            SQLiteCommand makeStats = new SQLiteCommand(stat_table, m_dbConnection);
            makeStats.ExecuteNonQuery();

            // Close the connection
            m_dbConnection.Close();
        }

    }
}