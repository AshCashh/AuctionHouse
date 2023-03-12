using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionHouse.Menus.Interfaces;

namespace AuctionHouse.Menus.Dialogs
{
    /// <summary>Dialog used to end main menu session</summary>
    internal class ExitMenuDialog : IDisplayable, IMenuTerminator
    {
        /// <summary>Title displayed in main menu</summary>
        public string Title { get; }

        /// <summary>Initilise a new instance of ExitMenuDialog</summary>
        /// <param name="title">Title displayed in main menu</param>
        public ExitMenuDialog(string title)
        {
            Title = title;
        }

        /// <summary>Implement IDisplayable</summary>
        public void Display()
        {
        }
    }
}
