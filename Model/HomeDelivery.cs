using System;
namespace AuctionHouse.Model
{
    public class HomeDelivery : DeliveryOption
    {
        /// <summary>The delivery address where the product is being delivered</summary>
        public Address Address { get; }

        /// <summary>Product that is storing the delivery option</summary>
        public Product DeliveryProduct { get; }

        /// <summary>Initialise a new instance of HomeDelivery</summary>
        /// <param name="address">The delivery address</param>
        /// <param name="DeliveryProduct">The product being delivered</param>
        public HomeDelivery(Address address, Product DeliveryProduct) : base(DeliveryProduct)
        {
            Address = address;
            
        }

        /// <summary>Confirmation message to confirm that the delivery has
        /// been created for the product</summary>
        public override void Confirmation()
        {
            Console.WriteLine($"Thank you for your bid. If successful, the item will be provided via delivery to {Address.UnitNum}/{Address.StNum} {Address.StName} " +
                $"{Address.StSuffix}, {Address.City} {Address.State.ToUpper()} {Address.Postcode}");
        }

        /// <summary>Formats the delivery to be displayed in the product list</summary>
        public override string ToString()
        {
            return $"Deliver to {Address.UnitNum}/{Address.StNum} {Address.StName} " +
                $"{Address.StSuffix}, {Address.City} {Address.State.ToUpper()} {Address.Postcode}";
        }
    }
}

