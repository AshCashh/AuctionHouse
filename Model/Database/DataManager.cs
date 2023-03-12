using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;
using System.Globalization;
using AuctionHouse.Model.UserInterface;


namespace AuctionHouse.Model.Database
{
    /// <summary>
    /// Class that manages, manipulates and outputs all client data.
    /// Class utilises the Singleton pattern to allow a single instance
    /// of itself to be created.
    /// </summary>
    public class DataManager : SingletonManager<DataManager>
    {
        [JsonPropertyName("Name")]

        /// <summary>Instance of data which stores all client data upon
        /// launch, this instance is used throughout the class to
        /// access any necessary client or product information</summary>
        public Data Data { get; private set; } = new Data();

        /// <summary>Method responsible for deserializing the file into
        /// the Data class, method is called at the beginning of the program</summary>
        public void ReadFileToData()
        {
            string fileName = "AuctionHouse_Database";
            if (new FileInfo(fileName).Length == 0)
            {
                return;
            }
            else
            {
                string jsonString = File.ReadAllText(fileName);
                Data = JsonSerializer.Deserialize<Data>(jsonString, options) ?? new Data();
            }
        }
        JsonSerializerOptions options = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };

        /// <summary>Method responsible for serializing the Data class into
        /// JSON format and writing it to the file, method is called at the end
        /// of the program</summary>
        public void WriteDataToFile()
        {
            string fileName = "AuctionHouse_Database";
            var jsonString1 = JsonSerializer.Serialize(Data, options);
            File.WriteAllText(fileName, jsonString1);
        }
        /// <summary>Method that adds instance of client to the database</summary>
        public void AddClient(Client client)
        {
            Data.clients.Add(client);
        }

        /// <summary>Method that adds instance of address to the database</summary>
        public void AddAddressToClient(Address address, int index)
        {
            Data.clients[index].address = address;
        }

        /// <summary>Method that checks if the clients address already exists
        /// in the database</summary>
        public bool CheckIfAddressExists(Client client)
        {
            if (Data.clients.Any(client2 => client.address == null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>Method that checks if an instance of the clients email
        /// exists in the database, returns true if it does</summary>
        public bool CheckIfClientExists(string email)
        {
            for (int i = 0; i < Data.clients.Count; i++)
            {
                if (email == Data.clients[i].Email)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Method that finds an instance of the client in the database
        /// given email and password, returns the index which exists</summary>
        public int FindClientIndex(string email, string password)
        {
            for (int i = 0; i < Data.clients.Count; i++)
            {
                if (email == Data.clients[i].Email && password == Data.clients[i].Password)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>Method that gets the length of a clients name and email</summary>
        public int GetClientLength(int index)
        {
            string a = Data.clients[index].Name;
            string b = Data.clients[index].Email;

            return a.Length + b.Length;
        }

        /// <summary>Method that finds the instance of a client in the database
        /// given the clients index</summary>
        public Client FindClient(int client_index)
        {
            Client client = Data.clients.ElementAt(client_index);
            return client;
        }

        /// <summary>Method that finds an instance of the client in the database
        /// returns the index</summary>
        public int FindClientIndex(Client client)
        {
            int client_index = -1;
            for (int i = 0; i < Data.clients.Count(); i++)
            {
                if (Data.clients[i] == client)
                {
                    client_index = i;
                }

            }
            return client_index;
        }

        /// <summary>Method that removes an instance of a product from the
        /// clients list of products</summary>
        public void RemoveProduct(int client_index, Product item)
        {
            
            Data.clients[client_index].clientProducts.Remove(item);
            Data.purchasedProducts.Add(item);
        }

        /// <summary>Method that sorts the clients products, ranked in ascending
        /// order by name, then description, then price.</summary>
        public List<Product> SortClientProducts(int client_index)
        {
            return Data.clients[client_index].clientProducts.OrderBy(x => x.ProductName).ThenBy(x => x.ProductDesc).ThenByDescending(x => x.ProductPrice).ToList();

        }

        /// <summary>Method that sorts products that are not owned by
        /// the logged in client, ranked in ascending
        /// order by name, then description, then price.</summary>
        public List<Product> SortOtherClientProducts(Client client)
        {
            List<Product> otherProducts = new List<Product>();

            for (int i = 0; i < Data.clients.Count; i++)
            {
                if (Data.clients[i] != client)
                {
                    for (int j = 0; j < Data.clients[i].clientProducts.Count(); j++)
                    {
                        otherProducts.Add(Data.clients[i].clientProducts[j]);

                        otherProducts = otherProducts.OrderBy(x => x.ProductName).ThenBy(x => x.ProductDesc).ThenByDescending(x => x.ProductPrice).ToList();
                    }
                }
            }
            return otherProducts;
        }

        /// <summary>Method that checks if a product already has a bid
        /// if it does, checks if the bid is higher than the current product bid.
        /// Then updates that product bid info with the new bid info</summary>
        /// <param name="product">Product being checked</param>
        /// <param name="bidampt">The new bid amount</param>
        /// <param name="client">The current logged in client</param>
        /// <returns>True if bid was valid, false if invalid</returns>
        public bool CheckBid(Product product, decimal bidampt, Client client)
        {
            int index = -1;

            for (int i = 0; i < Data.clients.Count; i++)
            {
                for (int j = 0; j < Data.clients[i].clientProducts.Count(); j++)
                {
                    if (Data.clients[i].clientProducts[j] == product)
                    {
                        index = i;
                    }
                }
            }
            if (index != -1)
            {
                if (bidampt > product.BidAmt)
                {
                    product.BidAmt = bidampt;
                    product.BidderName = client.Name;
                    product.BidderEmail = client.Email;
                    return true;
                }
            }
            return false;
        }
        /// <summary>Method that find products owned by the client which
        /// have bids, returns those products as a list</summary>
        public List<Product> FindProductBids(Client client, int client_index)
        {
            List<Product> otherProducts = new List<Product>();

            for (int i = 0; i < Data.clients.Count; i++)
            {
                if (Data.clients[i].Email == client.Email)
                {
                    for (int j = 0; j < Data.clients[i].clientProducts.Count(); j++)
                    {
                        if (Data.clients[i].clientProducts[j].BidAmt != 0)
                        {
                            otherProducts.Add(Data.clients[i].clientProducts[j]);
                            otherProducts = otherProducts.OrderBy(x => x.ProductName).ThenBy(x => x.ProductDesc).ThenByDescending(x => x.ProductPrice).ToList();
                        }
                    }
                }
            }
            return otherProducts;
        }
        /// <summary>Method that writes the purchased products to
        /// the terminal</summary>
        public void WritePurchasedProduct(List<Product> purchasedProducts)
        {
            int itemNum = 1;
            foreach (Product product5 in purchasedProducts)
            {
                //Console.Write($"\n{itemNum++}\t{product5.SellerEmail}\t{product5.ProductName}\t{product5.ProductDesc}\t{product5.ProductPrice}\t{product5.BidAmt.ToString("C", CultureInfo.CurrentCulture)}\t{product5.deliveryOption}");
                
                if (product5.homeDelivery != null)
                {
                    Console.Write($"\n{itemNum++}\t{product5.SellerEmail}\t{product5.ProductName}\t{product5.ProductDesc}\t{product5.ProductPrice}" +
                        $"\t{product5.BidAmt.ToString("C", CultureInfo.CurrentCulture)}\t{product5.homeDelivery}");
                }
                else
                {
                    Console.Write($"\n{itemNum++}\t{product5.SellerEmail}\t{product5.ProductName}\t{product5.ProductDesc}" +
                        $"\t{product5.ProductPrice}\t{product5.BidAmt.ToString("C", CultureInfo.CurrentCulture)}\t{product5.clickAndCollect}");
                }
                
            }
        }
        /// <summary>Method that finds the purchased products of a
        /// client and writes it using the previous method</summary>
        public void ListPurchasedProducts(Client client, int index)
        {
            List<Product> printedProducts = new();

            if (Data.purchasedProducts.Count != 0)
            {
                for (int i = 0; i < Data.purchasedProducts.Count; i++)
                {
                    if (Data.purchasedProducts[i].BidderEmail == client.Email)
                    {
                        printedProducts.Add(Data.purchasedProducts[i]);   
                    }
                }
                if (printedProducts.Count != 0)
                {
                    UI.ProductPurchasedColumns();
                    printedProducts = printedProducts.OrderBy(x => x.ProductName).ThenBy(x => x.ProductDesc).ThenByDescending(x => x.ProductPrice).ToList();
                    WritePurchasedProduct(printedProducts);
                }
                else
                {
                    Console.WriteLine("\nYou have no purchased products at the moment.");
                }
            }
            else
            {
                Console.WriteLine("\nYou have no purchased products at the moment.");
            }
        }

        /// <summary>Method that adds the delivery option provided by
        /// the client to the database</summary>
        public void AddDeliveryOptionToClient(Product product, ClickAndCollect clickAndCollect, HomeDelivery homeDelivery)
        {
            int index = -1;
            int index2 = -1;

            for (int i = 0; i < Data.clients.Count; i++)
            {
                for (int j = 0; j < Data.clients[i].clientProducts.Count(); j++)
                {
                    if (Data.clients[i].clientProducts[j] == product)
                    {
                        index = i;
                        index2 = j;
                    }
                }
            }
            if (clickAndCollect != null)
            {
                Data.clients[index].clientProducts[index2].clickAndCollect = clickAndCollect;
            }
            else
            {
                Data.clients[index].clientProducts[index2].homeDelivery = homeDelivery;
            }
        }


    }
}

