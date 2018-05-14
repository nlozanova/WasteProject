using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wastes.ViewModels.History
{
    public class CreateHistoryVM
    {
        public int Id { get; set; }
        [Display(Name = "Начална точка")]
        public int StartStorageId { get; set; }

        [Display(Name = "Крайна точка")]
        public int EndStorageId { get; set; }

        public DateTime PredictedStartDate { get; set; }
        public DateTime PredictedEndDate { get; set; }

        public List<SelectListItem> Storages { get; set; }

        public List<string> SelectedWastes { get; set; }
        public List<WasteVM> Wastes { get; set; }
    }
}