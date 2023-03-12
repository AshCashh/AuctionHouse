using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionHouse.MenuDialogs;
using AuctionHouse.Menus.Dialogs;
using AuctionHouse.Model;
using AuctionHouse.Model.Utility;

namespace AuctionHouse.Menus
{
    /// <summary>Client menu for Auction House.</summary>
    internal class ClientMenu : Menu
    {
        private const string Title = " \nClient Menu\n-----------";
        private const string LogOff = " Log off";

        /// <summary>Initialise new MainMenu object where product actions are done</summary>
        /// <param name="clientUtil">Reference to client utility</param>
        /// <param name="client">Client instance where actions are taken</param>
        public ClientMenu(ClientUtilities clientUtil, Client client) : base(Title, clientUtil,
            new ProductAdvertiseDialog(clientUtil, client),
            new ViewProductDialog(clientUtil, client),
            new SearchProductDialog(clientUtil, client),
            new ViewProductBidDialog(clientUtil, client),
            new ViewPurchasedProductDialog(clientUtil, client),
            new ExitClientDialog(LogOff)
            )
        {

        }
    }
}
