using System;
using System.Collections.Generic;

#nullable disable

namespace SampleApi.Data.SQLite
{
    public partial class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsBlocked { get; set; }
    }
}
