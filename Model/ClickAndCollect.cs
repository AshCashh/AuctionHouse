using System;
namespace AuctionHouse.Model
{
    public class ClickAndCollect : DeliveryOption
    {
        /// <summary>The starting window for product collection</summary>
        public DateTime WindowStart { get; }

        /// <summary>The ending window for product collection</summary>
        public DateTime WindowEnd { get; }

        /// <summary>The product storing the delivery option</summary>
        public Product DeliveryProduct { get; }

        /// <summary>Initialise a new instance of click and collect
        /// delivery option</summary>
        /// <param name="windowstart">Starting window</param>
        /// <param name="windowend">Ending window</param>
        /// <param name="DeliveryProduct">Product with delivery option</param>
        public ClickAndCollect(DateTime windowstart, DateTime windowend, Product DeliveryProduct) : base(DeliveryProduct)
        {
            WindowStart = windowstart;
            WindowEnd = windowend;
        }

        /// <summary>Confirmation message to confirm that the delivery has
        /// been created for the product</summary>
        public override void Confirmation()
        {
            Console.WriteLine($"\nThank you for your bid. If successful, the item will be provided via collection between " +
                $"{WindowStart.ToString("HH:mm")} on {WindowStart.ToString("dd/MM/yyyy")} and {WindowEnd.ToString("HH:mm")} on {WindowEnd.ToString("dd/MM/yyyy")}");

        }

        /// <summary>Formats the delivery to be displayed in the product list</summary>
        public override string ToString()
        {
            return $"Collect between {WindowStart} and {WindowEnd}";
        }
        
    }
}

