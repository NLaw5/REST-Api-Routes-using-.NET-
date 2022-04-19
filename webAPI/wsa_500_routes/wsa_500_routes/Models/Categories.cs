using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wsa_500_routes.Models
{
    public class Categories
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }
        
        //NOTE: SQL type is longblob, will try using a type Byte for now for now
        public byte[] Picture { get; set; }
    }
}
