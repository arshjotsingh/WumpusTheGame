/*  Handler.cs 
 *  Version 1.0 Date 22.02.2015
 *  Description : This class connects to the database file 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; /* include System.IO to use TextReader class */

namespace WumpusTheGame
{
    class Handler
    {
        // Variable declaration
        private TextReader tr;
        private string line;
        private List<string> list;

        // Parameterless Constructor
        public Handler(){ }

        // Parameterised Constructor takes input of database file name
        // The file is stored in bin/debug
        public Handler(string dbFileName)
        {
            // StreamReader reads data from the text file
            tr = new StreamReader(dbFileName);

            // Create a string type List
            list = new List<string>();

            // Read from file till it returns null
            while((line = tr.ReadLine()) != null)
            {
                // add data to list from stream
                list.Add(line);
            }           
           
        }

        // returns the dataList containing data from streamReader
        public List<string> getDataList()
        {
            return list;
        }
    }
}
