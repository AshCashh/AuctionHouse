using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionHouse.Menus.Interfaces;

namespace AuctionHouse.Menus.Dialogs
{
    /// <summary>Dialog used to end client menu session</summary>
    internal class ExitClientDialog : IDisplayable, IClientTerminator
    {
        /// <summary>Title displayed in client menu</summary>
        public string Title { get; }

        /// <summary>Initilise a new instance of ExitClientDialog</summary>
        /// <param name="title">Title displayed in client menu</param>
        public ExitClientDialog(string title)
        {
            Title = title;
        }
        /// <summary>Implement IDisplayable</summary>
        public void Display()
        {
        }
    }
}
