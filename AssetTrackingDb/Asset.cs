using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingDb
{
    internal class Asset
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime PDate { get; set; }
        public int Price { get; set; }
        public string Location { get; set; }
        public string Currency { get; set; }
        public int LoPrice { get; set; }
    }
}
