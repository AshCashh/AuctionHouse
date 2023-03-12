using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Globalization;

namespace AuctionHouse.Model.UserInterface
{
    /// <summary>Class used to validation user input and returns input
    /// if successfully validated</summary>
    public class Validation
    {
        /// <summary>Prompt symbol used at the beginning of every readline</summary>
        public static void PromptSymbol()
        {
            Console.Write("> ");
        }

        /// <summary>Checks if input is blank or empty </summary>
        /// <param name="prompt">Prompt that requests for user input</param>
        /// <param name="errorMessage">Error message if input was invalid</param>
        /// <returns>The valid input</returns>
        public string GetNonBlank(string prompt, string errorMessage)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                PromptSymbol();
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input)) return input;
                else Console.WriteLine(errorMessage);
            }
        }

        /// <summary>Checks if input is a valid email format</summary>
        /// <param name="prompt">Prompt that requests for user input</param>
        /// <param name="errorMessage">Error message if input was invalid</param>
        /// <returns>The valid input</returns>
        public string GetEmail(string prompt, string errorMessage)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                PromptSymbol();
                string email = Console.ReadLine();
                if (Regex.IsMatch(email, "^[a-zA-Z-0-9._-]+@[a-zA-Z0-9.-]+\\.+[a-zA-Z]+$"))
                {
                    string[] emailSplit = email.Split('@');
                    string prefix = emailSplit[0];
                    string suffix = emailSplit[1];
                    string[] invalidChar = { "-", "_", "." };

                    if (invalidChar.Any(x => prefix.EndsWith(x)) || suffix.StartsWith('.'))
                    {
                        Console.WriteLine(errorMessage);
                    }
                    else
                    {
                        return email;
                    }
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }

        /// <summary>Checks if input is a valid password</summary>
        /// <param name="prompt">Prompt that requests for user input</param>
        /// <param name="errorMessage">Error message if input was invalid</param>
        /// <param name="printRequirements">Bool to print the password requirements</param>
        /// <returns>The valid input</returns>
        public string GetPassword(string prompt, string errorMessage, bool printRequirements)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                if (printRequirements)
                {
                    UI.PasswordRequirments();
                }
                PromptSymbol();
                string password = Console.ReadLine();

                var hasNum = new Regex(@"[0-9]+");
                var hasUpChar = new Regex(@"[A-Z]+");
                var hasLowChar = new Regex(@"[a-z]+");
                var hasSpecialChar = new Regex(@"\W");
                var hasEightChar = new Regex(@".{8,}");

                if (hasNum.IsMatch(password) && hasUpChar.IsMatch(password) && hasLowChar.IsMatch(password) && hasSpecialChar.IsMatch(password) && hasEightChar.IsMatch(password))
                {
                    return password;
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }

        /// <summary>Checks if input is a valid unit number </summary>
        /// <param name="prompt">Prompt that requests for user input</param>
        /// <param name="errorMessage">Error message if input was invalid</param>
        /// <returns>The valid input</returns>
        public int? GetUnitNum(string prompt, string errorMessage, string errorMessage2)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                PromptSymbol();
                string s = Console.ReadLine();
                if (int.TryParse(s, out int unitNum))
                {
                    if (unitNum == 0)
                    {
                        return null;
                    }
                    else if (unitNum > 0)
                    {
                        return unitNum;
                    }
                    else
                    {
                        Console.WriteLine(errorMessage);
                    }
                }
                else
                {
                    Console.WriteLine(errorMessage2);
                }
            }
        }

        /// <summary>Checks if input is a valid street number</summary>
        /// <param name="prompt">Prompt that requests for user input</param>
        /// <param name="errorMessage">Error message if input was invalid</param>
        /// <returns>The valid input</returns>
        public int GetStNum(string prompt, string errorMessage, string errorMessage2)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                PromptSymbol();
                string s = Console.ReadLine();
                if (int.TryParse(s, out int stNum))
                {
                    if (stNum > 0)
                    {
                        return stNum;
                    }
                    else
                    {
                        Console.WriteLine(errorMessage);
                    }
                }
                else
                {
                    Console.WriteLine(errorMessage2);
                }
            }

        }

        /// <summary>Checks if input is a valid postcode</summary>
        /// <param name="prompt">Prompt that requests for user input</param>
        /// <param name="errorMessage">Error message if input was invalid</param>
        /// <returns>The valid input</returns>
        public int GetPostcode(string prompt, string errorMessage, string errorMessage2)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                PromptSymbol();
                string s = Console.ReadLine();
                if (int.TryParse(s, out int postcode))
                {
                    if (postcode > 1000 && postcode < 9999)
                    {
                        return postcode;
                    }
                    else
                    {
                        Console.WriteLine(errorMessage);
                    }
                }
                else
                {
                    Console.WriteLine(errorMessage2);
                }
            }
        }

        /// <summary>Checks if input is a valid state</summary>
        /// <param name="prompt">Prompt that requests for user input</param>
        /// <param name="errorMessage">Error message if input was invalid</param>
        /// <returns>The valid input</returns>
        public string GetState(string prompt, string errorMessage)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                PromptSymbol();
                string state = Console.ReadLine();

                string[] array = new string[8] { "qld", "nsw", "vic", "tas", "sa", "wa", "nt", "act" };

                if (array.Any(state.ToLower().Equals))
                {
                    return state;
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }

        /// <summary>Checks if input is a integer</summary>
        /// <param name="prompt">Prompt that requests for user input</param>
        /// <param name="errorMessage">Error message if input was invalid</param>
        /// <returns>The valid input</returns>
        public int GetInt(string prompt, string errorMessage, int maxInt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                PromptSymbol();
                string s = Console.ReadLine();
                if(int.TryParse(s, out int input))
                {
                    if (input <= 0 || input > maxInt)
                    {
                        Console.WriteLine(errorMessage);
                    }
                    else
                    {
                        return input - 1;
                    }
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }
            }

        }

        /// <summary>Checks if input is a valid price format</summary>
        /// <param name="prompt">Prompt that requests for user input</param>
        /// <param name="errorMessage">Error message if input was invalid</param>
        /// <returns>The valid input</returns>
        public string GetPrice(string prompt, string errorMessage)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                PromptSymbol();
                string price = Console.ReadLine();
                if (Regex.IsMatch(price, "^\\$+[0-9]+\\.+([0-9]{2})$"))
                {
                    return price;
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }

            }
        }

        /// <summary>Checks if input is a valid decimal type</summary>
        /// <param name="prompt">Prompt that requests for user input</param>
        /// <param name="errorMessage">Error message if input was invalid</param>
        /// <returns>The valid input</returns>
        public decimal GetDecimal(string prompt, string errorMessage)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                PromptSymbol();
                string price = Console.ReadLine();
                if (Regex.IsMatch(price, "^\\$+[0-9]+\\.+([0-9]{2})$"))
                {
                    decimal value = decimal.Parse(price, NumberStyles.Currency);
                    return value;
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }

        /// <summary>Checks if input is yes or no</summary>
        /// <param name="prompt">Prompt that requests for user input</param>
        /// <param name="errorMessage">Error message if input was invalid</param>
        /// <returns>True if yes, False if no</returns>
        public bool GetYesOrNo(string prompt, string errorMessage)
        {
           
            while (true)
            {
                Console.WriteLine(prompt);
                PromptSymbol();
                string input = Console.ReadLine().Trim().ToLower();
                if (input == "yes")
                {
                    return true;
                }
                else if (input == "no")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }

        /// <summary>Checks if input is a valid start DateTime format</summary>
        /// <param name="prompt">Prompt that requests for user input</param>
        /// <param name="errorMessage">Error message if input was invalid</param>
        /// <returns>The valid input</returns>
        public DateTime GetStartDate(string prompt, string errorMessage, string errorMessage2)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                PromptSymbol();
                string s = Console.ReadLine();
                if (DateTime.TryParseExact(s, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime input))
                {
                    DateTime current = DateTime.Now;
                    DateTime currentAddHour = current.AddHours(1);

                    if (input.Date > current.Date || input.Date == current.Date && input.TimeOfDay > currentAddHour.TimeOfDay)
                    {
                        return input;
                    }
                    else
                    {
                        Console.WriteLine(errorMessage);
                    }
                }
                else
                {
                    Console.WriteLine(errorMessage2);
                }
            }
        }

        /// <summary>Checks if input is a valid end DateTime format</summary>
        /// <param name="prompt">Prompt that requests for user input</param>
        /// <param name="errorMessage">Error message if input was invalid</param>
        /// <returns>The valid input</returns>
        public DateTime GetEndDate(string prompt, string errorMessage, string errorMessage2, DateTime start_time)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                PromptSymbol();
                string s = Console.ReadLine();
                if (DateTime.TryParseExact(s, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime input))
                {
                    DateTime currentTime = DateTime.Now;
                    DateTime start_timeAddHour = start_time.AddHours(1);

                    if (input.Date > currentTime.Date || input.Date == currentTime.Date && input.TimeOfDay > start_timeAddHour.TimeOfDay)
                    {
                        return input;
                    }
                    else
                    {
                        Console.WriteLine(errorMessage);
                    }
                }
                else
                {
                    Console.WriteLine(errorMessage2);
                }
            }
        }
    }
}

