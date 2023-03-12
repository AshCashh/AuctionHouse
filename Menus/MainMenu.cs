using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionHouse.MenuDialogs;
using AuctionHouse.Menus.Dialogs;
using AuctionHouse.Model.Utility;

namespace AuctionHouse.Menus
{
    /// <summary>Main menu for Auction House.</summary>
    public class MainMenu : Menu
    {
        private const string Title = $"\nMain Menu\n--------- ";
        private const string Exit = " Exit";
        
        private ClientUtilities client;

        /// <summary>Initialise new MainMenu object.</summary>
        public MainMenu(ClientUtilities client) : base(Title, client, 
            new RegisterDialog (client),
            new SignInDialog(client), 
            new ExitMenuDialog(Exit))
        {
            this.client = client;
        }
    }
}
