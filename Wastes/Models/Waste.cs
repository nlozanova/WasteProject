using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wastes.Models
{
    public class Waste 
    {
        public int Id { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<int> PrimaryStorageId { get; set; }
        public Nullable<int> TypeId { get; set; }
        public string Number { get; set; }
        public bool IsSelected { get; set; }
        public Nullable<int> StorageId { get; set; }
        public Nullable<int> WasteStatusId { get; set; }

        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<Protocol> Protocols { get; set; }
        public virtual WasteType WasteType { get; set; }
        public virtual Storage Storage { get; set; }
        public virtual WasteStatus WasteStatus { get; set; }
    }
}