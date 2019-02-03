using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashBack.Models
{
    public class RetornoAlbunsSpotify
    {
        public class RootObject
        {
            public playlists playlists { get; set; }            
        }

        public class playlists
        {
            public List<items> items { get; set; }
        }
        public class items
        {
            public string name { get; set; }
        }
    }
}
