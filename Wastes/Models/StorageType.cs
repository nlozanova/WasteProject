using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wastes.Models
{
    public class StorageType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Storage> Storages { get; set; }
    }
}