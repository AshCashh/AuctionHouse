using AuctionHouse.Model.Database;
using AuctionHouse.Model.UserInterface;
using System;
namespace AuctionHouse.Model
{
    /// <summary> Class for a clients address details </summary>
    public class Address
    {
        /// <summary> Address optional unit number </summary>
        public int? UnitNum { get; }

        /// <summary> Address street number </summary>
        public int StNum { get; }

        /// <summary> Address street name </summary>
        public string StName { get; }

        /// <summary> Address street suffix </summary>
        public string StSuffix { get; }

        /// <summary> Address city </summary>
        public string City { get; }

        /// <summary> Address postcode </summary>
        public int Postcode { get; }

        /// <summary> Address state </summary>
        public string State { get; }

        /// <summary> Initialise a new address for a client </summary>
        /// <param name="unitNum"> The address unit number </param>
        /// <param name="stNum"> The address street number </param>
        /// <param name="stName"> The address street name </param>
        /// <param name="stSuffix"> The address street suffix </param>
        /// <param name="city"> The address city </param>
        /// <param name="postcode"> The address postcode </param>
        /// <param name="state"> The address state </param>
        public Address(int? unitNum, int stNum, string stName, string stSuffix, string city, int postcode, string state)
        {

            UnitNum = unitNum;
            StNum = stNum;
            StName = stName;
            StSuffix = stSuffix;
            City = city;
            Postcode = postcode;
            State = state;
        }

        /// <summary> Confirmation dialog once a client has registered a valid address </summary>
        public void Confirmation()
        {
            Console.WriteLine($"\nAddress has been updated to {UnitNum}/{StNum} {StName} {StSuffix}, {City} {State.ToUpper()} {Postcode}");
        }
    }
}

