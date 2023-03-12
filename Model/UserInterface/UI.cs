using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionHouse.Model.Database;

namespace AuctionHouse.Model.UserInterface
{
    /// <summary>Class that reads and writes input from the user</summary>
    public static class UI
    {
        const string NoBlank = "The supplied value is not valid";
        const string IntBounds = "Please supply a whole number between {0} and {1}";

        /// <summary>Gets a line  of text from user.</summary>
        /// <param name="result">String where result is saved.</param>
        /// <param name="prompt">Text used to request input.</param>
        /// <param name="errorMessage">Text used if empty line entered.</param>
        /// <param name="allowBlank">Should we accept an empty line?</param>
        public static bool Read(out string result, string prompt, string errorMessage = NoBlank, bool allowBlank = false)
        {
            bool valid = true;
            result = null;
            while (true)
            {
                Write(prompt);
                
                result = Console.ReadLine();
                valid = result != null;

                if (!valid) break;
                if (allowBlank) break;
                if (!string.IsNullOrEmpty(result)) break;

                Console.WriteLine(errorMessage);

            }
            return valid;
        }

        /// <summary>Gets a validated integer value from user.</summary>
        /// <param name="result">Variable in which the result 
        /// will be saved.</param>
        /// <param name="prompt">Text used to request input.</param>
        public static bool Read(out int result, string prompt, string errorMessage = NoBlank)
        {
            bool valid;
            result = int.MinValue;
            while (true)
            {
                string input;
                valid = Read(out input, prompt, errorMessage);

                if (!valid) break;
                if (int.TryParse(input, out result)) break;

                Console.WriteLine(errorMessage);
            }
            return valid;
        }

        /// <summary>Gets a validated integer value from user, with bounds </summary>
        /// <param name="result">Variable in which the result 
        /// will be saved.</param>
        /// <param name="prompt">Text used to request input.</param>
        /// <param name="lowerBound">The lower bound of the desired value.</param>
        /// <param name="upperBound">The upper bound of the desired value.</param>
        /// <param name="errorFormat">Text used if invalid data entered.</param>
        public static bool Read(out int result, string prompt, int lowerBound, int upperBound = int.MaxValue, string errorFormat = NoBlank)
        {
            string errorMessage = string.Format(errorFormat, lowerBound, upperBound);
            bool valid = true;
            result = int.MinValue;

            while (true)
            {
                valid = Read(out result, prompt, errorMessage);

                if (!valid) break;
                if (result >= lowerBound && result <= upperBound) break;

                Console.WriteLine(errorMessage);
            }
            return valid;
        }

        /// <summary>Displays text.</summary>
        /// <param name="format"></param>
        /// <param name="args">Text content to display</param>
        public static void Write(string format, params object[] args)
        {
            Console.Write(format, args);
        }

        /// <summary>Writes personal details of user</summary>
        /// <param name="index">Index of client in client list</param>
        /// <param name="client">Client that is being written</param>
        public static void PersonalDetails(int index, Client client)
        {
            int CharLength = DataManager.Instance.GetClientLength(index);

            Console.WriteLine($"\nPersonal Details for {client.Name}({client.Email})");
            Console.WriteLine(new string('-', CharLength + 23));
        }

        /// <summary>Writes the delivery options</summary>
        public static void DeliveryHeading()
        {
            string[] options = { "(1) Click and collect", "(2) Home Delivery" };

            Console.WriteLine("\nDelivery Instructions");
            Console.WriteLine(new string('-', 21));
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine(options[i]);
            }
        }

        /// <summary>Writes the welcome message at the begginning of
        /// the program</summary>
        public static void WelcomeMessage()
        {
            Console.WriteLine("+" + new string('-', 30) + "+");
            Console.WriteLine("| Welcome to the Auction House |");
            Console.WriteLine("+" + new string('-', 30) + "+");
        }

        /// <summary>Writes the goodbye message at the end of
        /// the program</summary>
        public static void GoodbyeMessage()
        {
            Console.WriteLine("+" + new string('-', 50) + "+");
            Console.WriteLine("| Good bye, thank you for using the Auction House! |");
            Console.WriteLine("+" + new string('-', 50) + "+");
        }

        /// <summary>Writes the password requirements</summary>
        public static void PasswordRequirments()
        {
            string[] array = { "* At least 8 characters", "* No white space characters", "* At least one upper-case letter", "* At least one lower-case letter", "* At least one digit", "* At least one special character" };
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }
        }
        /// <summary>Writes the product columns</summary>
        public static void ProductColumns()
        {
            string[] array = { "\nItem #", "Product name", "Description", "List price", "Bidder name", "Bidder email", "Bid amt" };

            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + "\t");

            }
        }

        /// <summary>Writes the purchased product columns</summary>
        public static void ProductPurchasedColumns()
        {
            string[] array2 = { "\nItem #", "Seller email", "Product name", "Description", "List price", "Amt paid", "Delivery option" };
            for (int i = 0; i < array2.Length; i++)
            {
                Console.Write(array2[i] + "\t");

            }
        }
    }
}
