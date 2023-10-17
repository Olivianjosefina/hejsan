using Microsoft.AspNetCore.Mvc;
using Vinlista.Models;

namespace Vinlista.Controllers
{
    public class VinController : Controller
    {
        private readonly VinMetod _vinMetod;

        public VinController()
        {
            _vinMetod = new VinMetod();
        }

        public IActionResult Index()
        {
            var vinList = _vinMetod.GetVin(out string errorMessage);
            ViewBag.ErrorMessage = errorMessage;
            return View(vinList);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var vin = _vinMetod.GetVinDetails(id, out string errorMessage);
            if (vin != null)
            {
                return View(vin);
            }
            else
            {
                ViewBag.ErrorMessage = errorMessage;
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public IActionResult Create(Vin vin)
        {
            if (ModelState.IsValid)
            {
                int result = _vinMetod.InsertVin(vin, out string errorMessage);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = errorMessage;
                    return View(vin);
                }
            }
            return View(vin);
        }

        public IActionResult Edit(int id)
        {
            var vinList = _vinMetod.GetVin(out string errorMessage);
            var vin = vinList.FirstOrDefault(v => v.VinID == id);

            if (vin != null)
            {
                return View(vin);
            }
            else
            {
                ViewBag.ErrorMessage = errorMessage;
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public IActionResult Edit(int id, Vin vin)
        {
            if (ModelState.IsValid)
            {
                int result = _vinMetod.UpdateVin(id, vin, out string errorMessage);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = errorMessage;
                    return View(vin);
                }
            }
            return View(vin);
        }

        public IActionResult Delete(int id)
        {
            var vinList = _vinMetod.GetVin(out string errorMessage);
            var vin = vinList.FirstOrDefault(v => v.VinID == id);

            if (vin != null)
            {
                return View(vin);
            }
            else
            {
                ViewBag.ErrorMessage = errorMessage;
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            int result = _vinMetod.DeleteVin(id, out string errorMessage);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = errorMessage;
                return RedirectToAction("Index");
            }
        }
    }
}
