using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wastes.Models
{
    public class Protocol 
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public byte[] File { get; set; }
        public string Name { get; set; }
        public Nullable<int> HistoryId { get; set; }

        public virtual ICollection<Waste> Wastes { get; set; }
        public virtual History History { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}