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
    /// <summary>Dialog which allows a user to view their created products</summary>
    internal class ViewProductDialog : ClientDialog
    {
        private const string Title = " View My Product List";
        private const string SubTitle = "\nProduct List for {0}({1})";
        private const string ErrorNoProducts = "\nYou have no advertised products at the moment";

        /// <summary>Initilise dialog to view a clients products</summary>
        /// <param name="clientutil">Reference to client utility</param>
        /// <param name="client">Client instance where actions are taken</param>
        public ViewProductDialog(ClientUtilities clientutil, Client client) : base(Title, clientutil, client)
        {
        }

        /// <summary>Implement IDisplayable; display the clients products.</summary>
        public override void Display()
        {
            int clientIndex = DataManager.Instance.FindClientIndex(Client);

            /// <summary>Sorts the clients products ranked in ascending order by name
            /// then description, then price.</summary>
            List<Product> clientSortedProducts = DataManager.Instance.SortClientProducts(clientIndex);
            
            if (clientSortedProducts.Count() != 0) { 
                Console.WriteLine(SubTitle, Client.Name, Client.Email);
                Console.WriteLine(new string('-', Client.Name.Length + Client.Email.Length + 19));
                UI.ProductColumns();
                ProductUtility.WriteProduct(clientSortedProducts);
            }
            else
            {
                Console.WriteLine(ErrorNoProducts);
            }

        }
    }
}
