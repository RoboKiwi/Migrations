using System;

namespace Migrations
{
    public class HashAttribute : Attribute
    {
        public HashAttribute(string hash)
        {
            Hash = hash;
        }

        public string Hash { get; set; }
    }
}