using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wastes.ViewModels
{
    public class WasteVM
    {
        public int Id { get; set; }

        [Display(Name = "Тип")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Количество")]
        public decimal? Quantity { get; set; }
        public int? StorageId { get; set; }

        [Display(Name = "Склад")]
        public string StorageName { get; set; }

        [Display(Name = "Статус")]
        public string Status { get; set; }

        [Required]
        [Display(Name = "Идентиф. номер")]
        public string Number { get; set; }
        //Type id for the dropdown
        public string TypeId { get; set; }

        //Waste types - for the  dropdown
        [Display(Name = "Тип")]
        public List<SelectListItem> Types { get; set; }

        //Storage id for the dropdown
        public string PrimaryStorageId { get; set; }

        //Storage types - for the  dropdown
        [Display(Name = "Склад")]
        public List<SelectListItem> Storages { get; set; }
        public bool IsChecked { get; set; }
    }
}