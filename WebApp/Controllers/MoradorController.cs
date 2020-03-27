using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class MoradorController : Controller
    {
        // GET: Morador
        public ActionResult Index()
        {

            IEnumerable<MoradorModel> moradorList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("morador").Result;
            moradorList = response.Content.ReadAsAsync<IEnumerable<MoradorModel>>().Result;
            return View(moradorList);

        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new MoradorModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Morador/" + id).Result;
                return View(response.Content.ReadAsAsync<MoradorModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(MoradorModel morador)
        {
            HttpResponseMessage response;
            if (morador.id == 0)
            {
                response = GlobalVariables.WebApiClient.PostAsJsonAsync("Morador", morador).Result;
            }
            else
            {
                response = GlobalVariables.WebApiClient.PutAsJsonAsync("Morador/" + morador.id, morador).Result;
            }

            return Json(new { Id = morador.id }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SalvarMorador(MoradorModel morador)
        {
            HttpResponseMessage response;
            if (morador.id == 0)
            {
                response = GlobalVariables.WebApiClient.PostAsJsonAsync("Morador", morador).Result;
            }
            else
            {
                response = GlobalVariables.WebApiClient.PutAsJsonAsync("Morador/" + morador.id, morador).Result;
            }

            return Json(new { id = morador.id_apartamento }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id = 0)
        {
            GlobalVariables.WebApiClient.DeleteAsync("Morador/" + id);
            return RedirectToAction("Index");
        }

    }
}