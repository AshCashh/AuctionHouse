using AuctionHouse.Model.Database;
using AuctionHouse.Model.UserInterface;
using System;
using System.Globalization;
using AuctionHouse;
namespace AuctionHouse.Model
{
    /// <summary> Class for a clients product details </summary>
    public class Product
    {
        /// <summary> Product name </summary>
        public string ProductName { get; }

        /// <summary> Product description </summary>
        public string ProductDesc { get; }

        /// <summary> Product list price </summary>
        public string ProductPrice { get; }

        /// <summary> Product initial bid </summary>
        public decimal BidAmt { get; set; }

        /// <summary> Name of client who bidded </summary>
        public string BidderName { get; set; } = "-";

        /// <summary> Email of client who bidded </summary>
        public string BidderEmail { get; set; } = "-";

        /// <summary> Email of client who sold product </summary>
        public string SellerEmail { get; set; }

        /// <summary> Delivery option </summary>
        public DeliveryOption deliveryOption { get; set; }

        /// <summary> Home delivery option </summary>
        public HomeDelivery homeDelivery { get; set; }

        /// <summary> Collect option </summary>
        public ClickAndCollect clickAndCollect { get; set; }

        /// <summary> Owner of product </summary>
        public Client Client { get; set; }

        /// <summary> Initialise a new product for a client </summary>
        /// <param name="productname"> The product name </param>
        /// <param name="productdesc"> The product description </param>
        /// <param name="productprice"> The product list price </param>
        /// <param name="client"> The owner of the product </param>
        public Product(string productname, string productdesc, string productprice, Client client)
        {
            ProductName = productname;
            ProductDesc = productdesc;
            ProductPrice = productprice;

            Client = client;
        }
        /// <summary> Overriding method to display product details separated by tabs </summary>
        public override string ToString()
        {
            return $"{ProductName}\t{ProductDesc}\t{ProductPrice}\t{BidderName}\t{BidderEmail}\t";
        }

        /// <summary> Confirmation dialog once a client has created a new product </summary>
        public void Confirmation()
        {
            Console.WriteLine($"\nSuccessfully added product {ProductName}, {ProductDesc}, {ProductPrice}.");
        }

        /// <summary> Confirmation dialog once a client has created a new product bid </summary>
        public void BidConfirmation()
        {
            Console.WriteLine($"Your bid of {BidAmt.ToString("C", CultureInfo.CurrentCulture)} for {ProductName} is placed.");
        }
    }
}

