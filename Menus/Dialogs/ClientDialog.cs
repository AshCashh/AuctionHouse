using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionHouse.Model;
using AuctionHouse.Model.Utility;

namespace AuctionHouse.Menus.Dialogs
{
    /// <summary>Base class for dialogues which handle Products.</summary>
    public abstract class ClientDialog : Dialog
    {

        /// <summary>Client where actions are taken</summary>
        protected Client Client { get; }
        /// <summary>Initilise dialog</summary>
        /// <param name="title">Title where options are printed</param>
        /// <param name="clientutil">Utility where actions are taken</param>
        /// <param name="client">Client where actions are taken</param>
        public ClientDialog(string title, ClientUtilities clientutil, Client client) : base(title, clientutil)
        {
            Client = client;
        }
    }
}
