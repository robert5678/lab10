using Microsoft.AspNetCore.Mvc;
using lab10.Models;

namespace lab10.Controllers
{
    public class ConsultationController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Products"] = new[] { "JavaScript", "C#", "Java", "Python", "Основи" };
            return View(new ConsultationFormModel());
        }

        [HttpPost]
        public JsonResult Index([FromBody] ConsultationFormModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Product == "Основи" && model.ConsultationDate.HasValue &&
                    model.ConsultationDate.Value.DayOfWeek == DayOfWeek.Monday)
                {
                    return Json(new { success = false, message = "Консультації щодо продукту 'Основи' не проводяться по понеділках." });
                }

                return Json(new { success = true, message = "Реєстрація на консультацію успішна!" });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                           .Select(e => e.ErrorMessage).ToList();

            return Json(new { success = false, errors });
        }
    }
}
