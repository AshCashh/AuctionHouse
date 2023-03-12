using AuctionHouse.Menus.Dialogs;
using AuctionHouse.Model;
using AuctionHouse.Model.Utility;
using AuctionHouse.Model.UserInterface;
using AuctionHouse.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.MenuDialogs
{
    /// <summary>Dialog which allows a user to register an account</summary>
    public class RegisterDialog : Dialog
    {
        private const string Title = " Register";
        private const string SubTitle = "\nRegistration\n------------";
        private const string NamePrompt = "\nPlease enter your name";
        private const string EmailPrompt = "\nPlease enter your email address";
        private const string PasswordPrompt = "\nPlease choose a password";

        private const string ErrorName = "\tThe supplied value is not a valid name";
        private const string ErrorEmail = "\tThe supplied value is not a valid email";
        private const string ErrorPassword = "\tThe supplied value is not a valid password";
        private const string ErrorClientExists = "\tThe supplied address is already in use";

        /// <summary>Initialise the dialog necessary to register</summary>
        /// <param name="clientutil">Reference to logged in client</param>
        public RegisterDialog(ClientUtilities clientutil) : base(Title, clientutil)
        {
        }

        /// <summary>Implement IDisplayable; register a new account.</summary>
        public override void Display()
        {
            Console.WriteLine(SubTitle);

            Validation validation = new Validation();

            string name = validation.GetNonBlank(NamePrompt, ErrorName);
            string email = validation.GetEmail(EmailPrompt, ErrorEmail);
            string password = validation.GetPassword(PasswordPrompt, ErrorPassword, true);

            /// <summary>Check if client email already exists </summary>
            if (!DataManager.Instance.CheckIfClientExists(email)) ClientUtilities.Regrister(name, email, password);
            else Console.WriteLine(ErrorClientExists);
        }
        
    }
}
