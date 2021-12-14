using System;
using System.Collections.Generic;

#nullable disable

namespace SampleApi.Data.Mssql
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsBlocked { get; set; }
    }
}
