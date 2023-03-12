using AuctionHouse.Menus.Interfaces;
using AuctionHouse.Model.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Menus.Dialogs
{
    /// <summary>Base class for displayable objects</summary>
    public abstract class Dialog : IDisplayable
    {
        /// <summary>Implement Title property from IDisplayable</summary>
        public string Title { get; }

        /// <summary>Reference to client utility where actions will be conducted</summary>
        protected ClientUtilities ClientUtil { get; }

        /// <summary>Initialise instance of dialog </summary>
        /// <param name="title">String to contain the title text</param>
        /// <param name="clientUtil">Reference to client utility</param>
        public Dialog(string title, ClientUtilities clientUtil)
        {
            Title = title;

            ClientUtil = clientUtil;
        }

        /// <summary>Abstract display method from IDisplayable.</summary>
        public abstract void Display();
    }
}
