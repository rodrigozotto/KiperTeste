using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ApartamentoController : Controller
    {
        // GET: Apartamento
        public ActionResult Index()
        {
            IEnumerable<ApartamentoModel> apartamentoList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("apartamento").Result;
            apartamentoList = response.Content.ReadAsAsync<IEnumerable<ApartamentoModel>>().Result;
            return View(apartamentoList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                //return Json(new { Resultado = new ApartamentoModel(), JsonRequestBehavior.AllowGet });
            return View(new ApartamentoModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("apartamento/" + id).Result;
                return View(response.Content.ReadAsAsync<ApartamentoModel>().Result);
                //return Json(new { Resultado = response.Content.ReadAsAsync<ApartamentoModel>().Result.id.ToString(), JsonRequestBehavior.AllowGet });
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(ApartamentoModel apartamento)
        {
            HttpResponseMessage response;
            if (apartamento.id == 0)
            {
                response = GlobalVariables.WebApiClient.PostAsJsonAsync("apartamento", apartamento).Result;
            }
            else
            {
                response = GlobalVariables.WebApiClient.PutAsJsonAsync("apartamento/" + apartamento.id, apartamento).Result;
            }

            return Json(new { Id = apartamento.id }, JsonRequestBehavior.AllowGet);
        }
        

        public ActionResult Delete(int id = 0)
        {
            GlobalVariables.WebApiClient.DeleteAsync("apartamento/" + id);
            return RedirectToAction("Index");


        }

        public ActionResult ListarMoradores(int id, int idMorador = 0)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("apartamento/moradores?id=" + id).Result;
            IEnumerable<MoradorModel> lstMorador = response.Content.ReadAsAsync<IEnumerable<MoradorModel>>().Result;
            ViewBag.IdApartamento = id;

            if(idMorador > 0)
            {
                response = GlobalVariables.WebApiClient.GetAsync("apartamento/moradores?id=" + id).Result;
                MoradorModel morador = response.Content.ReadAsAsync<MoradorModel>().Result;
            }

            return PartialView("ListarMoradores", lstMorador); // Json(lstMorador, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarMorador(string id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Morador/" +id).Result;
            MoradorModel morador = response.Content.ReadAsAsync<MoradorModel>().Result;
            return Json(new { Morador = morador }, JsonRequestBehavior.AllowGet);
        }

    }
}