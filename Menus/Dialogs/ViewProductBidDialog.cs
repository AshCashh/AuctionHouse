using AuctionHouse.Menus.Dialogs;
using AuctionHouse.Model;
using AuctionHouse.Model.Database;
using AuctionHouse.Model.Utility;
using AuctionHouse.Model.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace AuctionHouse.MenuDialogs
{
    /// <summary>Dialog which allows a user to view their products that have bids
    /// and give them the option the sell their product to the bidding client</summary>
    internal class ViewProductBidDialog : ClientDialog
    {
        private const string Title = " View Bids On My Products";
        private const string SubTitle = "List Product Bids for {0}({1})";
        private const string LikeToSell = "\nWould you like to sell something (yes or no)?";
        private const string EnterProductNum = "Please enter an integer between 1 and {0}:";
        private const string ProductSold = "You have sold {0} to {1} for {2}.";
       
        private const string ErrorMessage = "\n\tThe supplied value is not a valid input";
        private const string ErrorNoProducts = "No bids were found.";

        /// <summary>Initilise dialog to view a clients product bids</summary>
        /// <param name="clientutil">Reference to client utility</param>
        /// <param name="client">Client instance where actions are taken</param>
        public ViewProductBidDialog(ClientUtilities clientutil, Client client) : base(Title, clientutil, client)
        {

        }
        /// <summary>Implement IDisplayable; view product list and ask if they'd like
        /// to sell their product with the given bid</summary>
        public override void Display()
        {
            Validation validation = new Validation();
            int clienIndex = DataManager.Instance.FindClientIndex(Client);
            List<Product> productBids = DataManager.Instance.FindProductBids(Client, clienIndex);

            Console.WriteLine(SubTitle, Client.Name, Client.Email);
            Console.WriteLine(new string('-', Client.Name.Length + Client.Email.Length + 24));

            if (productBids.Count() != 0)
            {
                UI.ProductColumns();
                ProductUtility.WriteProduct(productBids);

                bool SellProduct = validation.GetYesOrNo(LikeToSell, ErrorMessage);

                if (SellProduct)
                {
                    string prompt = String.Format(EnterProductNum, productBids.Count);
                    int SellIndex = validation.GetInt(prompt, ErrorMessage, productBids.Count);

                    Console.WriteLine(ProductSold, productBids[SellIndex].ProductName, productBids[SellIndex].BidderName, productBids[SellIndex].BidAmt.ToString("C", CultureInfo.CurrentCulture));
                    productBids[SellIndex].SellerEmail = Client.Email;

                    /// <summary>Finds instance of product in the list and removes it</summary>
                    Product item = productBids.Single(x => x.ProductName == productBids[SellIndex].ProductName);

                    DataManager.Instance.RemoveProduct(clienIndex, item);
                }
            }
            else
            {
                Console.WriteLine(ErrorNoProducts);
            }

        }
    }
}
