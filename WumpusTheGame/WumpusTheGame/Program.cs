/*  Program.cs Main() program
 *  Version 1.0 Date 21.02.2015
 *  Description : This contains the main game program 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusTheGame
{
    class Program
    {
        // function to check the status of player and the tunnels available to other rooms
        static void printCurrentRoomStatus(Room currentRoom, int arrowLeft)
        {
            Console.WriteLine("You are in room {0}", currentRoom.RoomNumber);
            Console.WriteLine("You have {0} arrows left", arrowLeft);
            Console.WriteLine(currentRoom.RoomDescription);
            Console.WriteLine("There are tunnels to rooms {0}", currentRoom.ThreeAdjacentRooms);
        }

        // function to print the hints available in the current player room
        static void printCurrentRoomHint(Room currentRoom, string wumpusRoom, string bottomlesspitRoom, string spiderRoomNumber1, string spiderRooomNumber2)
        {
            // alert for wumpus in adjacent rooms
            if (currentRoom.ThreeAdjacentRooms.Contains(wumpusRoom))
            {
                Console.WriteLine("You smell some nasty Wumpus!");
            }
            
            // alert for bottomless pit in adjacent room
            if (currentRoom.ThreeAdjacentRooms.Contains(bottomlesspitRoom))
            {
                Console.WriteLine("You smell a dank odor");
            }

            // alert for spider rooms in adjacent room
            if (currentRoom.ThreeAdjacentRooms.Contains(spiderRoomNumber1) || currentRoom.ThreeAdjacentRooms.Contains(spiderRooomNumber2))
            {
                Console.WriteLine("You hear a faint clicking noise");
            }
        }

        // function to check if player wants to continue playing or exit game
        static bool playAgain()
        {
            // start an infinite loop 
            do
            {
                Console.WriteLine("Do you want to play again (Y)es/(N)o?");
                string s = Console.ReadLine();

                // if player wants to exit game
                if (s.Equals("n"))
                {
                    // Exits the application
                    System.Environment.Exit(0);
                }

                // if player wants to play again 
                else if (s.Equals("y"))
                {
                    // break this loop and return true
                    break;
                }

                // if player enters any other key then y or n
                else
                {
                    Console.WriteLine("Invalid Entry!!!");
                }

            } while (true);
            
            // always returns true to function
            return true;
        }

       // static void print
        static void Main(string[] args)
        {
            // Connect to wumpus data file using  Handler class
            Handler dataHandler = new Handler("wumpusData.txt");

            // Get the data in a list from Handler Class
            List<string> dataList = dataHandler.getDataList();

            // Create a list for Room Class to carry objects for all rooms
            List<Room> roomList = new List<Room>();

            foreach(string s in dataList)
            {
                // Split the list item for every * sign in the dataList
                string[] splitArray= s.Split('*');
                
                // Trim whitespaces 
                splitArray[0]= splitArray[0].Trim();
                splitArray[1] = splitArray[1].Trim();
                splitArray[2] = splitArray[2].Trim();

                // Insert data into roomList using constructor
                roomList.Add(new Room(splitArray[0], splitArray[1], splitArray[2]));                
            }

            // ---------------starts Main loop --------------------
            do
            {
                // Generates a different random room for the required    
                var random = new Random();
                
                // Creates an array and store randomly generated values in an order in values variable 
                var values = Enumerable.Range(2, 10).OrderBy(x => random.Next()).ToArray();

                // Assign array values to wumpus,pit and spiders rooms
                string wumpusRoom = values[0].ToString();
                string bottomlessPitRoom = values[1].ToString();
                string spiderRoomNumber1 = values[2].ToString();
                string spiderRoomNumber2 = values[3].ToString();

                // Declare number of Arrows
                int arrowLeft = 3;

                // Create a currentRoom object which contains the current room of the player
                Room currentRoom = new Room();

                // As the game starts from room 1 , we need to search for room number 1 in roomList  and assign it to currentRoom Object
                currentRoom = roomList.Find(x => x.RoomNumber.Equals("1"));

                // ----Starts the game for player ----
                Console.WriteLine("Welcome to  ***HUNT THE WUMPUS!!*** \n");
                
                // call a function to print the status of room
                printCurrentRoomStatus(currentRoom, arrowLeft);

                // call function to print the hints available for room
                printCurrentRoomHint(currentRoom, wumpusRoom, bottomlessPitRoom, spiderRoomNumber1, spiderRoomNumber2);

                // start loop for Move or shoot
                do
                {
                    Console.WriteLine("(M)ove or (S)hoot");

                    // Get input from player to move or shoot
                    string getPlayerMoveOrShoot = Console.ReadLine();
                    
                    // Trim and lowercase the input from player
                    getPlayerMoveOrShoot = getPlayerMoveOrShoot.Trim();
                    getPlayerMoveOrShoot = getPlayerMoveOrShoot.ToLower();

                    /* Starts ELSE IF LADDER to check player input and take action accordingly */

                    /* If player decides to move */
                    if(getPlayerMoveOrShoot.Equals("m"))
                    {
                        
                        // Get the move room
                        Console.WriteLine("Which room?");
                        string playerMoveRoom = Console.ReadLine().Trim();

                        // Check if player is allowed to move to the other room
                        if(currentRoom.ThreeAdjacentRooms.Contains(playerMoveRoom))
                        {
                            /* if player moves to room then check conditions to kill player */

                            // player moves to wumpus room 
                            if (playerMoveRoom.Equals(wumpusRoom))
                            {
                                // prints Game over
                                Console.WriteLine("GAME OVER!!! You have been eaten by a wumpus");
                                
                                // Call playAgain() defined at top of the class which returns a 
                                // boolean value to see if player wants to continue playing or not
                                if(playAgain())
                                {
                                    break;
                                }
                            }

                            // player moves to a bottomless pit room
                            else if (playerMoveRoom.Equals(bottomlessPitRoom))
                            {
                                // prints Game over
                                Console.WriteLine("GAME OVER!!! You fell in a bottomless pit. ");
                                
                                // Call playAgain() to see if player wants to continue playing or not
                                if (playAgain())
                                {
                                    break;
                                }
                            }

                            // player moves to one of the spider rooms
                            else if (playerMoveRoom.Equals(spiderRoomNumber1) || playerMoveRoom.Equals(spiderRoomNumber2))
                            {
                                // prints Game over
                                Console.WriteLine("GAME OVER!!! You have been bitten by spiders");

                                // Call playAgain() to see if player wants to continue playing or not                                if (playAgain())
                                {
                                    break;
                                }
                            }
                           
                            // If the player enters new room and does not die, change the currentRoom Object
                            // to the newRoom and print hints & status accordingly
                            else
                            {
                                // Lambda expression to search the playerMoveRoom in the Room List and get the matched 
                                // room using roomNumbers
                                Room newRoom = roomList.Find(x => x.RoomNumber.Equals(playerMoveRoom));

                                // Assign newRoom object to currentRoom object
                                currentRoom = newRoom;

                                // call a function to print the status of room
                                printCurrentRoomStatus(currentRoom, arrowLeft);

                                // call function to print the hints available for room
                                printCurrentRoomHint(currentRoom, wumpusRoom, bottomlessPitRoom, spiderRoomNumber1, spiderRoomNumber2);
                            }

                        }
                        
                        // If the user enters an invalid room to enter , except the given tunnels, Show error
                        else
                        {
                            Console.WriteLine("Dimwit! You cant get there from here");
                            Console.WriteLine("There are tunnels to rooms " + currentRoom.ThreeAdjacentRooms);
                        }
                    }

                    /* If player decides to SHOOT an arrow */
                    else if(getPlayerMoveOrShoot.Equals("s"))
                    {
                        // Get the shoot room
                        Console.WriteLine("Which room?");
                        string playerShootRoom = Console.ReadLine().Trim();

                        // If the shoot is made to the 3 allowed tunnels  then check conditions
                        if (currentRoom.ThreeAdjacentRooms.Contains(playerShootRoom))
                        {
                            // If the arrow kills the wumpus then you win
                            if ((playerShootRoom.Equals(wumpusRoom)))
                            {
                                Console.WriteLine("YOU WIN...You Killed * the Wumpus * !! ");
                                
                                // Call playAgain() to see if player wants to continue playing or not
                                if (playAgain())
                                {
                                    break;
                                }
                            }

                            // If the arrow does not hit the wumpus then decrease arrow and check condition
                            else
                            {
                                // Decrement number of arrows 
                                arrowLeft--;

                                // if the number of available arrows is more than 0 then only continue to play
                                if(arrowLeft>0)
                                {
                                    Console.WriteLine("Your arrow goes down the tunnel and is lost. You Missed");

                                    // call a function to print the status of room
                                    printCurrentRoomStatus(currentRoom, arrowLeft);

                                    // call function to print the hints available for room
                                    printCurrentRoomHint(currentRoom, wumpusRoom, bottomlessPitRoom, spiderRoomNumber1, spiderRoomNumber2);

                                }

                                // if no arrows are available 
                                else
                                {
                                    Console.WriteLine("Your arrow goes down the tunnel and is lost. You missed");
                                    Console.WriteLine("No arrows left dimwit!!  GAME OVER !!!");

                                    // Call playAgain() to see if player wants to continue playing or not
                                    if (playAgain())
                                    {
                                        break;
                                    }

                                }

                            }

                        }

                        // If player shoots in a non allowed tunnel then print message
                        else
                        {
                            Console.WriteLine("You cannot Shoot to this room. You are not robinhood!");

                            // call a function to print the status of room
                            printCurrentRoomStatus(currentRoom, arrowLeft);

                            // call function to print the hints available for room
                            printCurrentRoomHint(currentRoom, wumpusRoom, bottomlessPitRoom, spiderRoomNumber1, spiderRoomNumber2);
                        }

                    }

                    // If player pressed anything else then M or S, then print message
                    else
                    {
                        Console.WriteLine("Invalid Entry Dimwit!! Try Again.!");
                    }

                } while (true);

                // Clears the Console Screen
                Console.Clear();
                
            } while (true);
        }
    }
}
