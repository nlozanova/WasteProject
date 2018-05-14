using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wastes.Models
{
    public class WasteStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Waste> Wastes { get; set; }
    }
}