using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARgv24.Data;
using ShopTARgv24.Models.Kindergarden;

namespace ShopTARgv24.Controllers
{
    public class KindergardenController : Controller
    {
        private readonly ShopTARgv24Context _context;
        public KindergardenController
            (
            ShopTARgv24Context context
            )
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var result = _context.Kindergardens
                .Select(x => new KindergardenIndexViewModel
                {
                    Id = x.Id,
                    GroupName = x.GroupName,
                    ChildrenCount = x.ChildrenCount,
                    KindergardenName = x.KindergardenName,
                    TeacherName = x.TeacherName
                });
            return View(result);
        }
    }
}
