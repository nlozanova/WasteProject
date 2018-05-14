using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wastes.Models
{
    public class History 
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string State { get; set; }
        public string PerformerId { get; set; }
        public Nullable<int> AssignorId { get; set; }
        public Nullable<int> StartStorageId { get; set; }
        public Nullable<int> EndStorageId { get; set; }
        public Nullable<System.DateTime> PredictedStartDate { get; set; }
        public Nullable<System.DateTime> PredictedEndDate { get; set; }
        public bool IsChecked { get; set; }

        public virtual ApplicationUser Performer { get; set; }
        public virtual ICollection<Waste> Wastes { get; set; }
        public virtual ICollection<Protocol> Protocols { get; set; }
    }
}