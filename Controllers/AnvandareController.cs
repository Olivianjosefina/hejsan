using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using VinApp.Models;


	public class AnvandareController : Controller
	{

        private readonly AnvandarMetod _anvandarMetod;

        public AnvandareController()
        {
            _anvandarMetod = new AnvandarMetod();
        }



    public IActionResult Index()
		{
			return View();
		}

	public IActionResult InsertAnvandare()
    {
			AnvandarDetalj ad = new AnvandarDetalj();
			AnvandarMetod am = new AnvandarMetod();
			int i = 0;
			string error = "";

			i = am.InsertAnvandare(ad, out error);
			ViewBag.error = error;
			ViewBag.antal = i;

			return View();
		}

    public IActionResult DeleteAnvandare(string anvandarNamn)
    {
        AnvandarMetod am = new AnvandarMetod();
        String error = "";
        int i = 0;
        i = am.DeleteAnvandare(anvandarNamn, out error);
        HttpContext.Session.SetString("antal", i.ToString());
        return RedirectToAction("selectWithDataSet");
    }



    [HttpGet]
        public IActionResult InsertAnvandare2()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InsertAnvandare2(AnvandarDetalj anvandare)
		{
        AnvandarMetod am = new AnvandarMetod();
        int i = 0;
        string error = "";

        i = am.InsertAnvandare(anvandare, out error);
        ViewBag.error = error;
        ViewBag.antal = i;
        if (i == 1)
        {
            return RedirectToAction("selectWithDataSet");
        }
        else { return View("InsertAnvandare"); }
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        AnvandarMetod am = new AnvandarMetod();
        string error = "";
        List<AnvandarDetalj> anvandareList = am.GetAnvandare(out error);

        AnvandarDetalj anvandare = anvandareList.FirstOrDefault(a => a.AnvandarID == id);

        if (anvandare == null)
        {
           
            return RedirectToAction("SelectWithDataSet");
        }

        ViewBag.error = error;
        return View(anvandare);
    }



    public IActionResult UpdateAnvandare(string anvandarNamn, AnvandarDetalj updatedAnvandare)
    {
        AnvandarMetod am = new AnvandarMetod();
        String error = "";
        int i = 0;

        try
        {
            i = am.UpdateAnvandare(anvandarNamn, updatedAnvandare, out error);

            if (i > 0)
            {
                HttpContext.Session.SetString("antal", i.ToString());
            }
            else
            {
                ViewBag.error = "Uppdateringen misslyckades.";
            }
        }
        catch (Exception ex)
        {
            ViewBag.error = "Ett fel inträffade: " + ex.Message;
        }

        return RedirectToAction("selectWithDataSet");
    }


    public ActionResult SelectWithDataSet()
    {
        List<AnvandarDetalj> AnvandarList = new List<AnvandarDetalj>();
        AnvandarMetod am = new AnvandarMetod();
        string error = "";
        AnvandarList = am.GetAnvandare(out error);
        ViewBag.error = error;
        return View(AnvandarList);

    }

    
    [HttpGet]
    public IActionResult FilterAnvandare()
    {
        return View(new FilterAnvandareViewModel());
    }


    [HttpPost]
    public IActionResult FilterAnvandare(FilterAnvandareViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        AnvandarMetod am = new AnvandarMetod();

        List<AnvandarDetalj> anvandarDetaljLista = am.FilterAnvandareByAge(model.FilterAge, out string errormsg);

        ViewData["FilteredUsers"] = anvandarDetaljLista;
        ViewBag.Error = errormsg;

        return View(model);
    }



    [HttpGet]
    public IActionResult Delete(int id)
    {
        AnvandarMetod am = new AnvandarMetod();
        string error = "";
        AnvandarDetalj anvandare = am.GetAnvandareById(id);

        if (anvandare == null)
        {
            ViewBag.Error = "Användaren hittades inte.";
            return RedirectToAction("SelectWithDataSet");
        }

        ViewBag.error = error;
        return View("Delete", anvandare); // Använd en annan vy för bekräftelse
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        AnvandarMetod am = new AnvandarMetod();
        string error = "";

        AnvandarDetalj anvandare = am.GetAnvandareById(id);

        if (anvandare == null)
        {
            ViewBag.Error = "Användaren hittades inte.";
            return RedirectToAction("SelectWithDataSet");
        }

        int result = am.DeleteAnvandare(anvandare.AnvandarNamn, out error);

        if (result > 0)
        {
            return RedirectToAction("SelectWithDataSet");
        }
        else
        {
            ViewBag.Error = "Det uppstod ett fel vid borttagning av användaren.";
            return View("Delete", anvandare); 
        }
    }


    public IActionResult Sortering(string sortColumn, string sortDirection)
    {
        string error = "";
        var users = _anvandarMetod.GetAnvandare(out error);

        if (!string.IsNullOrEmpty(error))
        {
            ViewBag.error = error;
        }

        if (string.IsNullOrEmpty(sortColumn))
        {
            sortColumn = "AnvandarNamn";
        }

        if (sortColumn == ViewBag.SortColumn)
        {
            if (sortDirection == "asc")
            {
                sortDirection = "desc";
            }
            else
            {
                sortDirection = "asc";
            }
        }
        else
        {
            sortDirection = "asc";
        }

     
        ViewBag.SortColumn = sortColumn;
        ViewBag.SortDirection = sortDirection;

        users = SortUsers(users, sortColumn, sortDirection);

        return View(users);
    }

    [HttpPost]
    public IActionResult Sortering(string sortDirection)
    {
        string error = "";
        var users = _anvandarMetod.GetAnvandare(out error);

        if (!string.IsNullOrEmpty(error))
        {
            ViewBag.error = error;
        }

        
        string sortColumn = "AnvandarNamn";
        
        ViewBag.SortColumn = sortColumn;
        ViewBag.SortDirection = sortDirection;

        users = SortUsers(users, sortColumn, sortDirection);

        return View(users);
    }


    private List<AnvandarDetalj> SortUsers(List<AnvandarDetalj> users, string sortColumn, string sortDirection)
    {
        switch (sortColumn)
        {
            case "AnvandarNamn":
                if (sortDirection == "asc")
                {
                    users = users.OrderBy(u => u.AnvandarNamn).ToList();
                }
                else if (sortDirection == "desc")
                {
                    users = users.OrderByDescending(u => u.AnvandarNamn).ToList();
                }
                break;

            case "Epost":
                if (sortDirection == "asc")
                {
                    users = users.OrderBy(u => u.Epost).ToList();
                }
                else if (sortDirection == "desc")
                {
                    users = users.OrderByDescending(u => u.Epost).ToList();
                }
                break;

            default:
                users = users.OrderBy(u => u.AnvandarNamn).ToList();
                break;
        }

        return users;
    }

    [HttpGet]
    public IActionResult SokFunktion()
    {
        return View(new SokAnvandareViewModel());
    }

    [HttpPost]
    public IActionResult SokFunktion(SokAnvandareViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        AnvandarMetod am = new AnvandarMetod();

        List<AnvandarDetalj> anvandarDetaljLista = am.SokFunktion(model.sokText, out string errormsg);

        ViewData["FilteredUsers"] = anvandarDetaljLista;
        ViewBag.Error = errormsg;

        return View(model);
    }


}












