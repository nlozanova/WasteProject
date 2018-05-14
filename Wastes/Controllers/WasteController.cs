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
    public class WasteController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        private List<SelectListItem> GetStoragesLI()
        {
            //Gets all the storages from the database (for the dropdown)
            var storagesDb = db.Storages.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToArray();

            //Fills the Storages list (for the dropdown)
            List<SelectListItem> storagesList = new List<SelectListItem>();

            for (int i = 0; i < storagesDb.Count(); i++)
            {
                storagesList.Add(storagesDb[i]);
            }
            return storagesList;
        }
        private List<SelectListItem> GetWasteTypesLI()
        {
            //Gets all the waste types from the database (for the dropdown)
            var wasteTypesDB = db.WasteTypes.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToArray();

            //Fills the Waste Type list (for the dropdown)
            List<SelectListItem> wasteTypesList = new List<SelectListItem>();

            for (int i = 0; i < wasteTypesDB.Count(); i++)
            {
                wasteTypesList.Add(wasteTypesDB[i]);
            }
            return wasteTypesList;
        }

        public ActionResult ListOfWastes()
        {
            var wastesDb = db.Wastes.ToArray();

            List<WasteVM> wastesList = new List<WasteVM>();

            //Fills a list with all the db wastes 
            for (int i = 0; i < wastesDb.Length; i++)
            {
                var storageName = db.Storages.Find(wastesDb[i].PrimaryStorageId).Name;
                var wasteType = db.WasteTypes.Find(wastesDb[i].TypeId).Name;
                var wasteStatus = db.WasteStatuses.Find(wastesDb[i].WasteStatusId).Name;
                var waste = new WasteVM()
                {
                    Id = wastesDb[i].Id,
                    Quantity = wastesDb[i].Quantity,
                    StorageName = storageName,
                    Type = wasteType,
                    Number = wastesDb[i].Number,
                    Status = wasteStatus

                };
                wastesList.Add(waste);
            }
            return View(wastesList);
        }
        public ActionResult CreateWaste()
        {
            // StorageVM for the Edit form
            var wasteVM = new WasteVM()
            {
                Types = GetWasteTypesLI(),
                Storages = GetStoragesLI()
            };

            return View(wasteVM);
        }

        [HttpPost]
        public ActionResult CreateWaste(WasteVM wasteVM)
        {
            //Db Model
            Waste wasteDb = new Waste();

            //Fills the dbModel with the passed data
            wasteDb.Number = wasteVM.Number;
            wasteDb.Quantity = wasteVM.Quantity;
            wasteDb.TypeId = int.Parse(wasteVM.TypeId);
            wasteDb.PrimaryStorageId = int.Parse(wasteVM.PrimaryStorageId);
            wasteDb.StorageId = int.Parse(wasteVM.PrimaryStorageId);
            //WasteStatusId = 1 is the start point of the waste
            wasteDb.WasteStatusId = 1;

            if (ModelState.IsValid)
            {
                db.Wastes.Add(wasteDb);
                db.SaveChanges();
                ViewBag.SuccessMessage = "Успешен запис!";
                return View("~/Views/Shared/SuccessPage.cshtml");
            }

            wasteVM.Types = GetWasteTypesLI();
            wasteVM.Storages = GetStoragesLI();
            return View(wasteVM);
        }
        
        public ActionResult EditWaste(int wasteId)
        {
            //Gets the selected waste
            var wasteDb = db.Wastes.Find(wasteId);

            //WasteVM for the Edit form
            var wasteVM = new WasteVM()
            {
                Id = wasteDb.Id,
                Number = wasteDb.Number,
                Quantity = wasteDb.Quantity,
                Storages = GetStoragesLI(),
                Types = GetWasteTypesLI()
            };
            return View(wasteVM);
        }
        
        [HttpPost]
        public ActionResult EditWaste(WasteVM waste)
        {
            //Gets the edited waste
            var wasteDb = db.Wastes.Find(waste.Id);

            //Fills the db model
            wasteDb.Number = waste.Number;
            wasteDb.Quantity = waste.Quantity;
            wasteDb.TypeId = int.Parse(waste.TypeId);
            wasteDb.PrimaryStorageId = int.Parse(waste.PrimaryStorageId);
            wasteDb.WasteStatusId = 3;

            if (ModelState.IsValid)
            {
                db.Entry(wasteDb).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.SuccessMessage = "Успешен запис!";
                return View("~/Views/Shared/SuccessPage.cshtml");
            }

            waste.Types = GetWasteTypesLI();
            waste.Storages = GetStoragesLI();
            return View(waste);
        }
        //Deletes waste
        public ActionResult DeleteWaste(int wasteId)
        {
            //Gets the selected waste
            var wasteDb = db.Wastes.Find(wasteId);
            
            db.Wastes.Remove(wasteDb);
            db.SaveChanges();

            return RedirectToAction("ListOfWastes");
        }
    }
}