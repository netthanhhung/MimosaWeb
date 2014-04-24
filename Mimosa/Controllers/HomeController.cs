using System.Linq;
using System.Web.Mvc;
using Entities;
using Mimosa.Models;
using Mimosa.Models.Search;
using System.Configuration;

namespace Mimosa.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            var listSelect = Business.RoomService.GetRoomTypeList().Select(x => new SelectListItem { Text = x.Name, Value = x.RoomTypeId.ToString() }).ToList();
            listSelect.Insert(0, new SelectListItem { Text = "-- All --", Value = "" });
            ViewBag.RoomTypeList = listSelect;

            var languageId = HttpContext.Session["LanguageId"] != null ? (int)HttpContext.Session["LanguageId"] : System.Convert.ToInt32( ConfigurationManager.AppSettings["LanguageId"]);
            var districtList = Business.DistrictService.GetDistricts().Select(x => new SelectListItem { Text = languageId == 1 ? x.DistrictName_EN : x.DistrictName, Value = x.DistrictCode.ToString() }).ToList();
            districtList.Insert(0, new SelectListItem { Text = "-- All --", Value = "" });
            ViewBag.Districts = districtList;

            return View();
        }
        
        
        public ActionResult ChangeLanguage(int languageId)
        {
            HttpContext.Session["LanguageId"] = languageId;
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFooter()
        {
            var statistic = Business.RoomService.GetStatistic();

            return PartialView("_Footer", statistic);
        }

    }
}
