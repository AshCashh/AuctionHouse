using System;
using System.Collections.Generic;

namespace AuctionHouse.Model
{
    /// <summary> </summary>
    public class Client
    {
        /// <summary> Client name </summary>
        public string Name { get; }

        /// <summary> Client email </summary>
        public string Email { get; }

        /// <summary> Client password </summary>
        public string Password { get; }

        /// <summary> Client address</summary>
        public Address address { get; set; }

        /// <summary> List of clients products </summary>
        public List<Product> clientProducts { get; set; } = new List<Product>();

        /// <summary> Initialise a new client </summary>
        /// <param name="name"> The clients name </param>
        /// <param name="email"> The clients email </param>
        /// <param name="password"> The clients password </param>
        public Client(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
       
        /// <summary> Adds a product to the logged in clients list of products </summary>
        /// <param name="product"> The clients product </param>
        public void AddProduct(Product product)
        {
            clientProducts.Add(product);
        }

        /// <summary> Confirmation dialog once a client has registered </summary>
        public void Confirmation()
        {
            Console.WriteLine($"\nClient {Name}({Email}) has successfully registered at the Auction House.");
        }
    }
}

