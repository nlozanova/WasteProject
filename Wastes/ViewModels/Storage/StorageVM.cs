using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wastes.ViewModels
{
    public class StorageVM
    {
        public int Id { get; set; }

        //Name of the storage
        [Required]
        [Display(Name = "Име")]
        public string Name { get; set; }

        //Storage address
        [Required]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        //Storage type - for the list of storages
        [Display(Name = "Тип")]
        public string StorageType { get; set; }

        //Map path address
        [Required]
        [Display(Name = "Google Maps адрес")]
        public string MapAddress { get; set; }


        [Display(Name = "Количество отпадъци в кг")]
        public decimal WholeQuantity { get; set; }

        //Type id for the dropdown
        public string TypeId { get; set; }

        //Storage types - for the  dropdown
        [Display(Name = "Тип")]
        public List<SelectListItem> Types { get; set; }

        public List<WasteVM> Wastes { get; set; }
    }
}