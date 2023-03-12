using AuctionHouse.Menus;
using AuctionHouse.Model;
using AuctionHouse.Model.Database;
using AuctionHouse.Model.UserInterface;
using AuctionHouse.Model.Utility;
using System;
using System.IO;

namespace AuctionHouse
{
    /// <summary>Program entry point for online auction house simulator.</summary>
    class AuctionHouse
    {
        /// <summary>Main entry point.</summary>
        static void Main(string[] args)
        {
            /// <summary>Reads the file to the Data class</summary>
            DataManager.Instance.ReadFileToData();

            Client newClient;

            ClientUtilities newClient2 = new ClientUtilities();
            UI.WelcomeMessage();
            MainMenu menu = new MainMenu(newClient2);
            menu.Display();

            /// <summary>Writes the database to the file</summary>
            DataManager.Instance.WriteDataToFile();
        }
    }
}








