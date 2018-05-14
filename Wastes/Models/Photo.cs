using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wastes.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public Nullable<int> StorageId { get; set; }
        public Nullable<int> StoreId { get; set; }
        public byte[] Image { get; set; }
        public string Name { get; set; }

        public virtual Storage Storage { get; set; }
    }
}