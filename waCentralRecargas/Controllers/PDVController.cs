using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace waCentralRecargas.Controllers
{
    public class PDVController : Controller
    {
        private Data.PDV _operationPDV = new Data.PDV();
        private Controllers.Services.SellClass _SellClass = new Services.SellClass();
        [AutoValidateAntiforgeryToken]
        public IActionResult Index() { if (User.Identity.IsAuthenticated) { return View(); } else { return RedirectToAction("Index", "Home"); } }
        [AutoValidateAntiforgeryToken]
        public IActionResult Vende() { if (User.Identity.IsAuthenticated) { return View(); } else { return RedirectToAction("Index", "Home"); } }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Vende(string Carrier) { if (User.Identity.IsAuthenticated) { TempData["Carrier"] = Carrier; return RedirectToAction("Recargas"); } else { return View(); } }
        [AutoValidateAntiforgeryToken]
        public IActionResult Recargas()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (TempData["Carrier"] != null)
                {
                    var Carrier = TempData["Carrier"];
                    switch (Carrier)
                    {
                        case "1":
                            ViewBag.imgCarrier = "../images/Logos/logo_telcel.png";
                            ViewBag.listMontos = _operationPDV.GetMontos(Carrier.ToString());
                            ViewBag.bondades = "Aquí va el texto de lo que dura y demás la recarga";
                            return View();                            
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Recargas(Models.RecargasViewModel.Recargas recargas)
        {
            if (ModelState.IsValid)
            {
                _SellClass.SellingRecarga(recargas.Carrier, recargas.Monto, recargas.Confirmacion,Request.Cookies["IDC"].ToString());
                while (!_SellClass._DoSell) { if (!_SellClass._isValid) { return View(recargas); } }
                TempData["idVenta"] = _SellClass._idVenta;
                TempData["Ticket"] = _SellClass._ticket;
                return RedirectToAction("Ticket");
            }
            else
            {
                return View(recargas);
            }
        }
        [AutoValidateAntiforgeryToken]
        public IActionResult Ticket()
        {
            string _idVenta = TempData["idVenta"].ToString();
            string _ticket= TempData["Ticket"].ToString();
            return View();
        }
    }
}