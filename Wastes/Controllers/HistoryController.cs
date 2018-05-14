using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wastes.ViewModels;
using Wastes.ViewModels.History;
using Wastes.Models;

namespace Wastes.Controllers
{
    [Authorize(Roles = "Performer")]
    public class HistoryController : Controller
    {
        //Database connection
        ApplicationDbContext db = new ApplicationDbContext();

        private List<SelectListItem> GetStoragesLI()
        {
            //Gets all the storages (for dropdown)
            var storagesDb = db.Storages.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToArray();

            List<SelectListItem> storagesList = new List<SelectListItem>();

            //Fills the storagesList with the data above (for dropdown)
            for (int i = 0; i < storagesDb.Count(); i++)
            {
                storagesList.Add(storagesDb[i]);
            }

            return storagesList;
        }

        private ICollection<Waste> GetCheckedWastes(CreateHistoryVM model)
        {
            var checkedWastes = model.Wastes.Where(x => x.IsChecked == true).ToArray();

            List<Waste> wastesDbList = new List<Waste>();
            Waste wasteDb;

            //var startStorage = db.Storages.Find(model.StartStorageId);

            for (int i = 0; i < checkedWastes.Count(); i++)
            {
                wasteDb = db.Wastes.Find(checkedWastes[i].Id);
                wasteDb.StorageId = null;
                wasteDb.WasteStatusId = 2;
                wasteDb.WasteType = db.WasteTypes.Find(wasteDb.TypeId);
                wastesDbList.Add(wasteDb);
            }
            return wastesDbList;
        }
        private List<WasteVM> GetHistoryWastes(History historyDb)
        {
            List<WasteVM> wastesList = new List<WasteVM>();

            var wastes = historyDb.Wastes.ToArray();
            for (int i = 0; i < wastes.Count(); i++)
            {

                var waste = new WasteVM()
                {
                    Id = wastes[i].Id,
                    Number = wastes[i].Number,
                    Quantity = wastes[i].Quantity,
                    Type = wastes[i].WasteType.Name,
                    Status = wastes[i].WasteStatus.Name                  
                };
                wastesList.Add(waste);
            }
            return wastesList;
        }
        private StorageVM GetStorageVM(int? storageId)
        {
            var storages = this.db.Storages.ToArray();
            var startStorageDB = storages.FirstOrDefault(x => x.Id == storageId);

            var startStorageVM = new StorageVM()
            {
                Id = startStorageDB.Id,
                Name = startStorageDB.Name,
                Address = startStorageDB.Address,
                StorageType = startStorageDB.StorageType.Type,
                MapAddress = startStorageDB.MapAddress

            };

            return startStorageVM;
        }

        public ActionResult HistoryList()
        {
            //Gets the logged user Id
            string userId = User.Identity.GetUserId();

            var history = this.db.History.ToArray();

            List<HistoryVM> historiesList = new List<HistoryVM>();

            for (int i = 0; i < history.Count(); i++)
            {
              
                var row = new HistoryVM
                {
                    Id = history[i].Id,
                    PredictedStartDate = history[i].PredictedStartDate,
                    PredictedEndDate = history[i].PredictedEndDate,
                    StartStorage = GetStorageVM(history[i].StartStorageId),
                    EndStorage = GetStorageVM(history[i].EndStorageId),
                    IsChecked = history[i].IsChecked,
                    PerformerName = history[i].Performer.UserName,
                    DateOfImplementation = history[i].Date,
                    Wastes = GetHistoryWastes(history[i]),
                };

                historiesList.Add(row);
            }
            return View(historiesList);

        }

        public ActionResult GoToMedium(int rowId)
        {
            var historyDb = db.History.Where(x => x.Id == rowId).FirstOrDefault();
            var historyStartId = historyDb.StartStorageId;
            var startStorageDb = db.Storages.Find(historyStartId);

            historyDb.IsChecked = true;
            historyDb.Date = DateTime.Now;

            //Wastes part
            var wastesList = historyDb.Wastes.ToArray();

            for (int i = 0; i < wastesList.Count(); i++)
            {
                wastesList[i].WasteStatusId = 3;
                wastesList[i].StorageId = historyDb.EndStorageId;
            }
            historyDb.Wastes = wastesList;

            if (ModelState.IsValid)
            {
                db.Entry(historyDb).State = EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.SuccessMessage = "Успешен запис!";
            return View("~/Views/Shared/SuccessPage.cshtml");
        }

        public ActionResult GoToIncinerator(int rowId)
        {
            var historyDb = db.History.Where(x => x.Id == rowId).FirstOrDefault();
            var historyStartId = historyDb.StartStorageId;
            var startStorageDb = db.Storages.Find(historyStartId);

            historyDb.IsChecked = true;
            historyDb.Date = DateTime.Now;

            //Wastes part
            var wastesList = historyDb.Wastes.ToArray();

            for (int i = 0; i < wastesList.Count(); i++)
            {
                // If status isn't intermediate 
                if (3 != wastesList[i].WasteStatusId)
                {
                    ViewBag.ErrorMessage = "Отпадъците не са били регистрирани в междинна прощадка!";
                    return View("~/Views/Shared/ErrorPage.cshtml");
                }

                //status of the waste is changed to - in incinerator
                wastesList[i].WasteStatusId = 4;
                wastesList[i].StorageId = historyDb.EndStorageId;
            }
            historyDb.Wastes = wastesList;

            if (ModelState.IsValid)
            {
                db.Entry(historyDb).State = EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.SuccessMessage = "Успешен запис!";
            return View("~/Views/Shared/SuccessPage.cshtml");
        }

        public ActionResult HistoryDetails(int historyId)
        {

            var historyDb = db.History.Find(historyId);
            
            var model = new HistoryVM()
            {
                Id = historyDb.Id,
                PredictedStartDate = historyDb.PredictedStartDate,
                PredictedEndDate = historyDb.PredictedEndDate,
                StartStorage = GetStorageVM(historyDb.StartStorageId),
                EndStorage = GetStorageVM(historyDb.EndStorageId),
                IsChecked = historyDb.IsChecked,
                PerformerName = historyDb.Performer.UserName,
                DateOfImplementation = historyDb.Date,
                Wastes = GetHistoryWastes(historyDb)

            };
            return View(model);
        }

        public ActionResult CreateHistory()
        {

            var historyVM = new CreateHistoryVM()
            {
                Storages = GetStoragesLI()
            };
            return View(historyVM);
        }

        [HttpPost]
        public ActionResult CreateHistory(CreateHistoryVM model)
        {
            string userId = User.Identity.GetUserId();
            History historyDb = new History();

            historyDb.StartStorageId = model.StartStorageId;
            historyDb.EndStorageId = model.EndStorageId;
            //test
            historyDb.PerformerId = userId;
            //end test

            //Adding wastes
            historyDb.Wastes = GetCheckedWastes(model);

            //if (ModelState.IsValid)
            //{
            db.History.Add(historyDb);
            db.SaveChanges();
            ViewBag.SuccessMessage = "Успешен запис!";
            return View("~/Views/Shared/SuccessPage.cshtml");
            //}

            //model.Storages = GetStoragesLI();
            //return View(model);
        }

        public ActionResult WastesPartial(int storageId)
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
                    Type = type

                };
                wastesList.Add(wasteVM);
            }
            ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Wastes");
            return PartialView("~/Views/Shared/EditorTemplates/_WasteVM.cshtml", wastesList);
        }
    }
}