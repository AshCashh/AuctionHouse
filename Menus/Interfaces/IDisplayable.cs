using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Menus.Interfaces
{
    /// <summary>All objects displayable in the view will implement this interface</summary>
    public interface IDisplayable
    {
        /// <summary>The displayable objects title</summary>
        public string Title { get; }

        /// <summary>Display and executes the logic of the displayable objecte</summary>
        public void Display();
    }
}
