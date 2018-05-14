using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wastes.ViewModels
{
    public class HistoryVM
    {
        public int Id { get; set; }

        [Display(Name ="Начална точка")]
        public StorageVM StartStorage { get; set; }

        [Display(Name = "Крайна точка")]
        public StorageVM EndStorage { get; set; }

        [Display(Name = "Дата на тръгване")]
        public DateTime? PredictedStartDate { get; set; }

        [Display(Name = "Дата на пристигане")]
        public DateTime? PredictedEndDate { get; set; }

        public bool IsChecked { get; set; }

        [Display(Name = "Изпълнител")]
        public string PerformerName { get; set; }

        [Display(Name = "Дата на изпълнение")]
        public DateTime? DateOfImplementation { get; set; }


        public List<WasteVM> Wastes { get; set; }
    }
}