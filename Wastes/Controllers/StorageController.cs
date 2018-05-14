using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wastes.ViewModels;
using Wastes.Models;

namespace Wastes.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StorageController : Controller
    {
        //Database connection
        ApplicationDbContext db = new ApplicationDbContext();

        private List<WasteVM> GetWastesByStorage(int storageId)
        {
            //Wastes             
            var wastesDb = db.Wastes.Where(x => x.PrimaryStorageId == storageId).ToArray();

            var wasteTypesDb = db.WasteTypes.ToArray();
            string type;
            List<WasteVM> wastesList = new List<WasteVM>();

            for (int i = 0; i < wastesDb.Count(); i++)
            {
                type = wasteTypesDb.FirstOrDefault(x => x.Id == wastesDb[i].TypeId).Name;
                var wasteVM = new WasteVM()
                {
                    Id = wastesDb[i].Id,
                    Number = wastesDb[i].Number,
                    Quantity = wastesDb[i].Quantity,
                    Status = wastesDb[i].WasteStatus.Name,
                    Type = type

                };
                wastesList.Add(wasteVM);
            }
            return wastesList;

        }

        private decimal GetStorageQuantity(int storageId)
        {
            var wastesDb = db.Wastes.Where(x => x.StorageId == storageId).ToArray();

            var wasteTypesDb = db.WasteTypes.ToArray();
            string type;
            List<WasteVM> wastesList = new List<WasteVM>();

            for (int i = 0; i < wastesDb.Count(); i++)
            {
                type = wasteTypesDb.FirstOrDefault(x => x.Id == wastesDb[i].TypeId).Name;
                var wasteVM = new WasteVM()
                {
                    Id = wastesDb[i].Id,
                    Number = wastesDb[i].Number,
                    Quantity = wastesDb[i].Quantity,
                    Status = wastesDb[i].WasteStatus.Name,
                    Type = type

                };
                wastesList.Add(wasteVM);
            }
            decimal quantity = 0;
            if (null != wastesList)
            {

                for (int i = 0; i < wastesList.Count(); i++)
                {
                    quantity += (decimal)wastesList[i].Quantity;
                }
            }
            return quantity;
        }

        private List<SelectListItem> GetStorageTypes()
        {
            //Gets all the storage types from the database (for the dropdown)
            var storageTypesDB = db.StorageTypes.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Type }).ToArray();

            //Fills the Storage Type list (for the dropdown)
            List<SelectListItem> storageTypesList = new List<SelectListItem>();

            for (int i = 0; i < storageTypesDB.Count(); i++)
            {
                storageTypesList.Add(storageTypesDB[i]);
            }
            return storageTypesList;
        }

        public ActionResult ListStorages()
        {
            //Gets all the storages
            var storagesDB = this.db.Storages.ToArray();

            //Creates an empty list of storages used in the view
            List<StorageVM> storagesList = new List<StorageVM>();

            //Fills the list with the data from the database
            for (int i = 0; i < storagesDB.Count(); i++)
            {

                var storageVM = new StorageVM()
                {
                    Id = storagesDB[i].Id,
                    Name = storagesDB[i].Name,
                    Address = storagesDB[i].Address,
                    MapAddress = storagesDB[i].MapAddress,
                    StorageType = storagesDB[i].StorageType.Type,
                    WholeQuantity = GetStorageQuantity(storagesDB[i].Id)

                };
                storagesList.Add(storageVM);
            }
            return View(storagesList);
        }

        public ActionResult EditStorage(int storageId)
        {
            //Gets the selected storage from the database
            var storageDB = db.Storages.Find(storageId);

            // Creates the Storage View Model for the Edit form
            var storageVM = new StorageVM()
            {
                Id = storageDB.Id,
                Name = storageDB.Name,
                Address = storageDB.Address,
                MapAddress = storageDB.MapAddress,
                Types = GetStorageTypes()
            };
            return View(storageVM);
        }

        [HttpPost]
        public ActionResult EditStorage(StorageVM storage)
        {
            //Gets the edited storage
            var storageDB = db.Storages.Find(storage.Id);

            //Passes the new data to the dbModel
            storageDB.Name = storage.Name;
            storageDB.Address = storage.Address;
            storageDB.MapAddress = storage.MapAddress;
            storageDB.StorageTypeId = int.Parse(storage.TypeId);

            if (ModelState.IsValid)
            {
                db.Entry(storageDB).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.SuccessMessage = "Успешен запис!";
                return View("~/Views/Shared/SuccessPage.cshtml");
            }

            storage.Types = GetStorageTypes();
            return View(storage);
        }

        public ActionResult CreateStorage()
        {
            // Creates the Storage View Model for the Edit form
            var storageVM = new StorageVM()
            {
                Types = GetStorageTypes()
            };

            return View(storageVM);
        }

        [HttpPost]
        public ActionResult CreateStorage(StorageVM storageVM)
        {
            Storage storageDb = new Storage();
            storageDb.Name = storageVM.Name;
            storageDb.Address = storageVM.Address;
            storageDb.MapAddress = storageVM.MapAddress;
            storageDb.StorageTypeId = int.Parse(storageVM.TypeId);

            if (ModelState.IsValid)
            {
                db.Storages.Add(storageDb);
                db.SaveChanges();
                ViewBag.SuccessMessage = "Успешен запис!";
                return View("~/Views/Shared/SuccessPage.cshtml");
            }

            storageVM.Types = GetStorageTypes();
            return View(storageVM);
        }

        public ActionResult DeleteStorage(int storageId)
        {
            var storageDB = db.Storages.Find(storageId);
            db.Storages.Remove(storageDB);
            db.SaveChanges();

            return RedirectToAction("ListStorages");
        }
        public ActionResult WastesPartial(int storageId)
        {
            ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Wastes");
            return PartialView("~/Views/Shared/EditorTemplates/_WastePopup.cshtml", GetWastesByStorage(storageId));
        }

    }
}