/*  Room.cs class file  
 *  Version 1.0 Date 21.02.2015
 *  Description : This is a template class file which is used for describe rooms for wumpusTheGame
 */ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusTheGame
{
    class Room
    {
        // Declarations
        private string roomNumber;
        private string threeAdjacentRooms;
        private string roomDescription;

        // Parameterless Constructor
        public Room()
        {

        }
        
        // Parameterised Constructor used to initialise the member variables 
        public Room(string roomNumber , string threeAdjacentRooms, string roomDescription)
        {
            this.roomNumber = roomNumber;
            this.threeAdjacentRooms = threeAdjacentRooms;
            this.roomDescription = roomDescription;
        }

        // GETTERS AND SETTERS

        // For roomNumber variable
        public string RoomNumber
        {
            get { return roomNumber; }
            set { roomNumber = value; }
        }

        // For threeAdjacentRooms variable
        public string ThreeAdjacentRooms
        {
            get { return threeAdjacentRooms; }
            set { threeAdjacentRooms = value; }
        }

        // For roomDescription variable
        public string RoomDescription
        {
            get { return roomDescription;  }
            set { roomDescription = value; }
        }
    }
}
