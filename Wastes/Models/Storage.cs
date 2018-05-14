using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wastes.Models
{
    public class Storage 
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public Nullable<int> StorageTypeId { get; set; }
        public string Name { get; set; }
        public string MapAddress { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
        public virtual StorageType StorageType { get; set; }
        public virtual ICollection<Waste> Wastes { get; set; }
        public virtual ICollection<Waste> Wastes1 { get; set; }
    }
}