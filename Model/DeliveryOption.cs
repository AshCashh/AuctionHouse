using System;
using AuctionHouse.Model.Database;
namespace AuctionHouse.Model
{
    /// <summary>Base class for delivery options</summary>
    public abstract class DeliveryOption
    {
        /// <summary>Product that is storing the delivery option</summary>
        protected Product DeliveryProduct { get; }

        /// <summary>Initialise a delivery option</summary>
        /// <param name="deliveryProduct">The product storing the delivery option</param>
        public DeliveryOption(Product deliveryProduct)
        {
            DeliveryProduct = deliveryProduct;
        }

        /// <summary>Abstract confirmation message for delivery</summary>
        public abstract void Confirmation();
    }
}

