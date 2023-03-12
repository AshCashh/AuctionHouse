using System;
using System.IO;
using System.Xml.Resolvers;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using AuctionHouse.Model.Database;
using AuctionHouse.Model.UserInterface;

namespace AuctionHouse.Model.Utility
{
    /// <summary>Class that handles client utility</summary>
    public class ClientUtilities
    {
        /// <summary>Creates instance of a client and adds it to database</summary>
        /// <param name="name">The client name</param>
        /// <param name="email">The client email</param>
        /// <param name="password">The client password</param>
        public static void Regrister(string name, string email, string password)
        {
            Client newClient = new Client(name, email, password);
            DataManager.Instance.AddClient(newClient);

            newClient.Confirmation();
        }
    }
}


