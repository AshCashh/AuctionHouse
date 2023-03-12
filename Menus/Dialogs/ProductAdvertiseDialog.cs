using AuctionHouse.Menus.Dialogs;
using AuctionHouse.Model;
using AuctionHouse.Model.Database;
using AuctionHouse.Model.UserInterface;
using AuctionHouse.Model.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.MenuDialogs
{
    /// <summary>Dialog which allows a user to create a product</summary>
    internal class ProductAdvertiseDialog : ClientDialog
    {
        private const string Title = " Advertise Product";
        private const string SubTitle = "\nProduct Advertisement for {0}({1})";
        private const string Name = "\nProduct name";
        private const string Description = "\nProduct description";
        private const string Price = "\nProduct price ($d.cc)";

        private const string ErrorName = "\n\tThe supplied value is not a valid name";
        private const string ErrorDesc = "\n\tThe supplied value is not a valid description";
        private const string ErrorPrice = "\n\tA currency value is required, e.g. $54.95, $9.99, $2314.15.";

        /// <summary>Initilise dialog to create a product</summary>
        /// <param name="clientutil">Reference to client utility</param>
        /// <param name="client">Client instance where actions are taken</param>
        public ProductAdvertiseDialog(ClientUtilities clientutil, Client client) : base(Title, clientutil, client)
        {
        }
        /// <summary>Implement IDisplayable; ask for product details and create products</summary>
        public override void Display()
        {
            int clientIndex = DataManager.Instance.FindClientIndex(Client);

            Validation validation = new Validation();

            Console.WriteLine(SubTitle, Client.Name, Client.Email);
            Console.WriteLine(new string('-', Client.Name.Length + Client.Email.Length + 28));

            string ProductName = validation.GetNonBlank(Name, ErrorName);
            string ProductDesc = validation.GetNonBlank(Description, ErrorDesc);
            string ProductPrice = validation.GetPrice(Price, ErrorPrice);

            ProductUtility.CreateProduct(ProductName, ProductDesc, ProductPrice, Client);
        }
       
    }
}
