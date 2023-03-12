using AuctionHouse.Menus.Dialogs;
using AuctionHouse.Model;
using AuctionHouse.Model.Database;
using AuctionHouse.Model.Utility;
using AuctionHouse.Model.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace AuctionHouse.MenuDialogs
{
    /// <summary>Dialog which allows a user to search products created by other clients
    /// then asks if they'd like to create a bid and ask for their preferred delivery option
    /// of product if the bid is successful </summary>
    internal class SearchProductDialog : ClientDialog
    {
        private const string Title = " Search For Advertised Products";
        private const string SubTitle = "\nProduct Search for {0}({1})";
        private const string SearchResult = "\nSearch results\n--------------";
        private const string SearchPrompt = "\nPlease supply a search phrase (ALL to see all products)";
        private const string LikeToSearch = "\nWould you like to place a bid on any of these items (yes or no)?";
        private const string EnterProductNum = "\nPlease enter a non-negative integer between 1 and {0}:";
        private const string DeliveryType = "\nPlease select an option between 1 and 2";
        private const string BiddingTitle = "\nBidding for {0} (regular price {1}), current highest bid {2}";
        private const string BidAmt = "\nHow much do you bid?";

        private const string UnitNumPrompt = "\nPlease provide your delivery address.\nUnit number (0 = none):";
        private const string StNumPrompt = "\nStreet number:";
        private const string StNamePrompt = "\nStreet name:";
        private const string StSuffixPrompt = "\nStreet suffix:";
        private const string CityPrompt = "\nCity:";
        private const string StatePrompt = "\nState (ACT, NSW, NT, QLD, SA, TAS, VIC, WA):";
        private const string PostcodePrompt = "\nPostcode (1000 .. 9999):";
        private const string DeliveryStart = "\nDelivery window start (dd/mm/yyyy hh:mm)";
        private const string DeliveryEnd = "\nDelivery window end (dd/mm/yyyy hh:mm)";

        private const string ErrorMessage = "\tThe supplied value is not a valid input";
        private const string ErrorPrice = "\tA currency value is required, e.g. $54.95, $9.99, $2314.15.";

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
        private const string ErrorBid = "\tBid amount must be greater than {0}";
        private const string ErrorProduct = "\tThere are no advertised products at the moment";
        private const string ErrorStartDate = "\tDelivery window start must be at least one hour in the future.";
        private const string ErrorEndDate = "\tDelivery window end must be at least one hour later than the start.";
        private const string ErrorInvalidDate = "\tPlease enter a valid data and time.";

        /// <summary>Initilise dialog to display list of products, bid creation
        /// and delivery prefference</summary>
        /// <param name="clientutil">Reference to client utility</param>
        /// <param name="client">Client instance where actions are taken</param>
        public SearchProductDialog(ClientUtilities clientutil, Client client) : base(Title, clientutil, client)
        {
        }

        /// <summary>Implement IDisplayable; view products, bid creation and
        /// and delivery preference</summary>
        public override void Display()
        {
            Validation validation = new Validation();
                        
            List<Product> sortedProducts = DataManager.Instance.SortOtherClientProducts(Client);
            
            Console.WriteLine(SubTitle, Client.Name, Client.Email);
            Console.WriteLine(new string('-', Client.Name.Length + Client.Email.Length + 21));

            /// <summary>Gets users search phrase</summary>
            string productSearch = validation.GetNonBlank(SearchPrompt, ErrorMessage);

            /// <summary>Finds products containing phrase in its name or description</summary>
            List<Product> productForBidding = ProductUtility.FindSearchedProduct(sortedProducts, productSearch, SearchResult);
            CreateBid(productForBidding, Client);
            
        }
        /// <summary>Creates bid for given product</summary>
        /// <param name="productForBidding">Product thats being bidded on</param>
        /// <param name="Client">Client that is creating the bid</param>
        public static void CreateBid(List<Product> productForBidding, Client Client)
        {
            Validation validation = new Validation();

            if (productForBidding.Count() != 0)
            {
                bool PlaceBid = validation.GetYesOrNo(LikeToSearch, ErrorMessage);

                if (PlaceBid)
                {
                    /// <summary>Gets the product index of their desired product to bid on</summary>
                    string prompt = String.Format(EnterProductNum, productForBidding.Count);
                    int bidIndex = validation.GetInt(prompt, ErrorMessage, productForBidding.Count);

                    Console.WriteLine(BiddingTitle, productForBidding[bidIndex].ProductName, productForBidding[bidIndex].ProductPrice,
                        productForBidding[bidIndex].BidAmt.ToString("C", CultureInfo.CurrentCulture));
                    while (true)
                    {
                        /// <summary>Gets users bid amount</summary>
                        decimal bidAmt = validation.GetDecimal(BidAmt, ErrorPrice);

                        /// <summary>Checks if bid is greater than previous bids</summary>
                        if (DataManager.Instance.CheckBid(productForBidding[bidIndex], bidAmt, Client))
                        {
                            productForBidding[bidIndex].BidConfirmation();
                            GetDeliveryOption(productForBidding[bidIndex]);
                            break;
                        }
                        else
                        {
                            Console.WriteLine(ErrorBid, productForBidding[bidIndex].BidAmt.ToString("C", CultureInfo.CurrentCulture));
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine(ErrorProduct);
            }
        }
        /// <summary>Gets the users desired delivery option</summary>
        /// <param name="product">Product which is being bidded on</param>
        public static void GetDeliveryOption(Product product)
        {
            UI.DeliveryHeading();
            Validation validation = new Validation();
            int option = validation.GetInt(DeliveryType, ErrorMessage, 2);

            /// <summary>Click and Collect dialog</summary>
            if (option + 1 == 1) 
            {
                DateTime startDate = validation.GetStartDate(DeliveryStart, ErrorStartDate, ErrorInvalidDate);
                DateTime endDate = validation.GetEndDate(DeliveryEnd, ErrorEndDate, ErrorInvalidDate, startDate);
                ClickAndCollect clickAndCollect = new ClickAndCollect(startDate, endDate, product);
                clickAndCollect.Confirmation();

                DataManager.Instance.AddDeliveryOptionToClient(product, clickAndCollect, null);
            }
            /// <summary>Home delivery dialog</summary>
            if (option + 1 == 2)
            {
                int? unitNum = validation.GetUnitNum(UnitNumPrompt, ErrorUnitNum, Error2UnitNum);
                int stNum = validation.GetStNum(StNumPrompt, ErrorStNum, Error2StNum);
                string stName = validation.GetNonBlank(StNamePrompt, ErrorStName);
                string stSuffix = validation.GetNonBlank(StSuffixPrompt, ErrorStSuffix);
                string city = validation.GetNonBlank(CityPrompt, ErrorCity);
                string state = validation.GetState(StatePrompt, ErrorState);
                int postcode = validation.GetPostcode(PostcodePrompt, ErrorPostcode, Error2Postcode);

                Address newaddress = new Address(unitNum, stNum, stName, stSuffix, city, postcode, state);
                HomeDelivery homeDelivery = new HomeDelivery(newaddress, product);
                homeDelivery.Confirmation();

                DataManager.Instance.AddDeliveryOptionToClient(product, null, homeDelivery);
            }


        }
    }
}
