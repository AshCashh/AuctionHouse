using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionHouse.Menus.Dialogs;
using AuctionHouse.Menus.Interfaces;
using AuctionHouse.Model.UserInterface;
using AuctionHouse.Model.Utility;
namespace AuctionHouse.Menus
{
    /// <summary>Dialog done to display a range of menu options.
    /// Structure was based off of Lawrence Buckingham's OOP Case Study - Bringing it all together
    /// from CAB201's Week 10 content</summary>
    public class Menu : Dialog
    {
        const string Prompt = "> ";
        const string Heading = "\nPlease select an option between 1 and {0}";

        /// <summary>Interface copy of menu options.</summary>
        private IDisplayable[] dialogOptions;

        /// <summary>Initialise a new menu object.</summary>
        /// <param name="title">Title being display.</param>
        /// <param name="options">List of menu options for users to choose from.</param>
        public Menu(string title, ClientUtilities client, params IDisplayable[] dialogOptions) : base(title, client)
        {
            this.dialogOptions = new IDisplayable[dialogOptions.Length];
            Array.Copy(dialogOptions, this.dialogOptions, dialogOptions.Length);
        }

        /// <summary>Get a menu option from user and take action.</summary>
        public override void Display()
        {
            while (true)
            {
                IDisplayable opt;
                WriteDialogOptions();
                GetDialogOption(out opt);

                opt.Display();

                if (opt is IMenuTerminator)
                {
                    UI.GoodbyeMessage();
                    break;
                }
                if (opt is IClientTerminator)
                {
                    
                    break;
                }

            }
        }
        /// <summary>
        /// Gets dialog option from user
        /// </summary>
        /// <param name="opt"></param>
        /// <returns></returns>
        private bool GetDialogOption(out IDisplayable opt)
        {
            int optionIndex;
            bool valid = UI.Read(out optionIndex, Prompt, 1, dialogOptions.Length);

            if (valid)
            {
                opt = dialogOptions[optionIndex - 1];

            }
            else
            {
                opt = null;
            }
            return valid;
        }
        /// <summary> Displays the dialog options for user</summary>
        private void WriteDialogOptions()
        {
            Console.WriteLine(Title);
            int index = 0;

            for (int i = 0; i < dialogOptions.Length; i++)
            {
                Console.WriteLine($"({i + 1})" + dialogOptions[i].Title);
                index++;
            }
            Console.WriteLine(Heading, index);
        }

    }
}
