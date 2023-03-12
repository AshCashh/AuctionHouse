using System;
namespace AuctionHouse.Model.Database
{
    /// <summary>Singleton class which only allows a single instance of itself to be created</summary>
    public class SingletonManager<T> where T : class, new()
    {
        public SingletonManager() { }

        class SingletonCreator
        {
            static SingletonCreator() { }
            internal static readonly T Instance = new T();
        }

        public static T Instance
        {
            get { return SingletonCreator.Instance; }
        }
    }
}

