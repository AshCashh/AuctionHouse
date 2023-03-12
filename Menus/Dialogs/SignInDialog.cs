using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionHouse.Menus;
using AuctionHouse.Menus.Dialogs;
using AuctionHouse.Model;
using AuctionHouse.Model.Database;
using AuctionHouse.Model.UserInterface;
using AuctionHouse.Model.Utility;

namespace AuctionHouse.MenuDialogs
{
    /// <summary>Dialog which allows a user to sign into the Auction House</summary>
    internal class SignInDialog : Dialog
    {
        private const string Title = " Sign In";
        private const string SubTitle = "\nSign In\n-------";
        private const string EmailPrompt = "\nPlease enter your email address";
        private const string PasswordPrompt = "\nPlease enter your password";

        private const string UnitNumPrompt = "Please provide your home address.\nUnit number (0 = none):";
        private const string StNumPrompt = "\nStreet number:";
        private const string StNamePrompt = "\nStreet name:";
        private const string StSuffixPrompt = "\nStreet suffix:";
        private const string CityPrompt = "\nCity:";
        private const string StatePrompt = "\nState (ACT, NSW, NT, QLD, SA, TAS, VIC, WA):";
        private const string PostcodePrompt = "\nPostcode (1000 .. 9999):";

        private const string ErrorEmail = "\tThe supplied value is not a valid email";
        private const string ErrorPassword = "\tThe supplied value is not a valid password";
        private const string ErrorClientNotExist = "\tSupplied credentials do no match those of any existing client.\n\tPlease check your credentials or register.";
        
        private const string ErrorUnitNum = "\tUnit number must be greater than 0.";
        private const string Error2UnitNum = "\tThe supplied value is not a valid unit number.";
        private const string ErrorStNum = "\tStreet number must be greater than 0.";
        private const string Error2StNum = "\tStreet number must be a positve integer.";
        private const string ErrorStName = "\tThe supplied value is not a valid street name.";
        private const string ErrorStSuffix = "\tThe supplied value is not a valid street suffix";
        private const string ErrorCity = "\tThe supplied value is not a valid city.";
        private const string ErrorState = "\tThe supplied value is not a valid state.";
        private const string ErrorPostcode = "\tPostcode must be between 1000 and 9999";
        private const string Error2Postcode = "\tThe supplied value is not a valid postcode.";

        /// <summary>Initilise sign in dialog</summary>
        /// <param name="clientutil">Reference to client utility</param>
        public SignInDialog(ClientUtilities clientutil) : base(Title, clientutil)
        {
        }

        /// <summary>Implement IDisplayable: gets client details and validates if 
        /// they exist in database then initilises the next menu.</summary>
        public override void Display()
        {
            Console.WriteLine(SubTitle);
            Validation validation = new Validation();

            string email = validation.GetEmail(EmailPrompt, ErrorEmail);
            string password = validation.GetPassword(PasswordPrompt, ErrorPassword, false);

            int clientIndex = DataManager.Instance.FindClientIndex(email, password);

            if (clientIndex != -1)
            {
                Client client = DataManager.Instance.FindClient(clientIndex);

                /// <summary>Checks if the client has registered an address</summary>
                if (DataManager.Instance.CheckIfAddressExists(client))
                {
                    UI.PersonalDetails(clientIndex, client);

                    AddAddress(clientIndex);
                    
                }
                ClientUtilities clientUtil = new ClientUtilities();
                Menu ClientMenu = new ClientMenu(clientUtil, client);
                ClientMenu.Display();
            }
            else
            {
                Console.WriteLine(ErrorClientNotExist);
            }
        }
        /// <summary>Method for address dialog and intilises a new address to that client</summary>
        /// <param name="clientIndex">Index of current client in the client list</param>
        public void AddAddress(int clientIndex)
        {
            Validation validation = new Validation();

            int? unitNum = validation.GetUnitNum(UnitNumPrompt, ErrorUnitNum, Error2UnitNum);
            int stNum = validation.GetStNum(StNumPrompt, ErrorStNum, Error2StNum);
            string stName = validation.GetNonBlank(StNamePrompt, ErrorStName);
            string stSuffix = validation.GetNonBlank(StSuffixPrompt, ErrorStSuffix);
            string city = validation.GetNonBlank(CityPrompt, ErrorCity);
            string state = validation.GetState(StatePrompt, ErrorState);
            int postcode = validation.GetPostcode(PostcodePrompt, ErrorPostcode, Error2Postcode);

            Address newaddress = new Address(unitNum, stNum, stName, stSuffix, city, postcode, state);
            newaddress.Confirmation();
            DataManager.Instance.AddAddressToClient(newaddress, clientIndex);
        }
    }
}
