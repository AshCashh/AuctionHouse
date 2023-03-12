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

namespace AuctionHouse.MenuDialogs
{
    /// <summary>Dialog which allows a user to view their purchased products</summary>
    internal class ViewPurchasedProductDialog : ClientDialog
    {
        private const string Title = " View My Purchased Items";
        private const string SubTitle = "Purchased Items for {0}({1})";

        /// <summary>Initilise dialog to view a clients purchased products</summary>
        /// <param name="clientutil">Reference to client utility</param>
        /// <param name="client">Client instance where actions are taken</param>
        public ViewPurchasedProductDialog(ClientUtilities clientutil, Client client) : base(Title, clientutil, client)
        {

        }
        /// <summary>Implement IDisplayable; display the clients purchased products.</summary>
        public override void Display()
        {
            int client_index = DataManager.Instance.FindClientIndex(Client);

            Console.WriteLine(SubTitle, Client.Name, Client.Email);
            Console.WriteLine(new string('-', Client.Name.Length + Client.Email.Length + 22));

            DataManager.Instance.ListPurchasedProducts(Client, client_index);

        }

    }
}
