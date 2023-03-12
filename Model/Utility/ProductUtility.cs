using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;
using System.Globalization;
using AuctionHouse.Model.Database;
using AuctionHouse.Model.UserInterface;

namespace AuctionHouse.Model.Utility
{
    /// <summary>Class for managing product utility</summary>
    public class ProductUtility
    {
        /// <summary>Writes the product</summary>
        /// <param name="products">The product being written</param>
        public static void WriteProduct(List<Product> products)
        {

            int itemNum = 1;
            foreach (Product product5 in products)
            {
                if (product5.BidAmt == 0)
                {
                    Console.Write($"\n{itemNum++}\t" + product5 + product5.BidAmt.ToString("-", CultureInfo.CurrentCulture));

                }
                else
                {
                    Console.Write($"\n{itemNum++}\t" + product5 + product5.BidAmt.ToString("C", CultureInfo.CurrentCulture));
                }

            }
        }
        /// <summary>Creates an instance of product</summary>
        /// <param name="ProductName">The product name</param>
        /// <param name="ProductDesc">The product description</param>
        /// <param name="ProductPrice">The product listing price</param>
        /// <param name="owner">The client owner of product</param>
        public static void CreateProduct(string ProductName, string ProductDesc, string ProductPrice, Client owner)
        { 
            Product newProduct = new Product(ProductName, ProductDesc, ProductPrice, owner);
            
            owner.AddProduct(newProduct);
            newProduct.Confirmation();
        }
        /// <summary>Find products that contain the searched input from the user</summary>
        /// <param name="sortedProducts">List of sorted products</param>
        /// <param name="productSearch">The user input for their searching product</param>
        /// <param name="SearchResult">The dialog for the list of products</param>
        /// <returns></returns>
        public static List<Product> FindSearchedProduct(List<Product> sortedProducts, string productSearch, string SearchResult)
        { 
            List<Product> printedProducts = new();

            if (sortedProducts.Count() != 0)
            {
                Console.WriteLine(SearchResult);

                UI.ProductColumns();
                /// <summary>Loops through every instance of a product in the list</summary>
                for (int i = 0; i < sortedProducts.Count(); i++)
                {
                    if (productSearch.ToLower() == "all")
                    {
                        WriteProduct(sortedProducts);
                        return sortedProducts;
                    }
                    /// <summary>Searches to see if the product name or description contains the search input</summary>
                    else if (sortedProducts[i].ProductName.Contains(productSearch.Trim()) || sortedProducts[i].ProductDesc.Contains(productSearch.Trim()))
                    {
                        printedProducts.Add(sortedProducts[i]);
                    }
                }
                WriteProduct(printedProducts);
                return printedProducts;

            }
            else
            {
                return printedProducts;
            }
        }
    }
}

