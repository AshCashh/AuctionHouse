using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using AuctionHouse.Model;

namespace AuctionHouse.Model.Database
{
    /// <summary>Class that stores all the client info, instance of class
    /// is serilized and written to a file so data isnt lost</summary>
    public class Data
    {
        /// <summary>List of clients with their products containing
        /// any bids or delivery info</summary>
        public List<Client> clients { get; set; } = new ();

        /// <summary>List of purchased products with its owner and information</summary>
        public List<Product> purchasedProducts { get; set; } = new();

    }
}

