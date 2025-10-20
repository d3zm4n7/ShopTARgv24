using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARgv24.ApplicationServices.Services;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;
using ShopTARgv24.Models.Kindergarden;
using ShopTARgv24.Models.Kindergardens;
using ShopTARgv24.Models.Spaceships;
using static System.Net.Mime.MediaTypeNames;


namespace ShopTARgv24.Controllers
{
    public class KindergardensController : Controller
    {
        private readonly ShopTARgv24Context _context;
        private readonly IKindergardensServices _kindergardensServices;
        private readonly IFileServices _fileServices;

        public KindergardensController
            (
                ShopTARgv24Context context,
                IKindergardensServices kindergardensServices,
                IFileServices fileServices
            )
        {
            _context = context;
            _kindergardensServices = kindergardensServices;
            _fileServices = fileServices;
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

        [HttpGet]
        public IActionResult Create()
        {
            KindergardenCreateUpdateViewModel result = new();

            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(KindergardenCreateUpdateViewModel vm)
        {
            var dto = new KindergardenDto()
            {
                Id = vm.Id,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergardenName = vm.KindergardenName,
                TeacherName = vm.TeacherName,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        KindergardenId = x.KindergartenId,
                    }).ToArray()
            };

            var result = await _kindergardensServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var kindergarden = await _kindergardensServices.DetailAsync(id);

            if (kindergarden == null)
            {
                return NotFound();
            }

            KindergardenImageViewModel[] images = await FilesFromDatabase(id);

            var vm = new KindergardenDeleteViewModel();

            vm.Id = kindergarden.Id;
            vm.GroupName = kindergarden.GroupName;
            vm.ChildrenCount = kindergarden.ChildrenCount;
            vm.KindergardenName = kindergarden.KindergardenName;
            vm.TeacherName = kindergarden.TeacherName;
            vm.CreatedAt = kindergarden.CreatedAt;
            vm.UpdatedAt = kindergarden.UpdatedAt;
            vm.Image.AddRange(images);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var kindergarden = await _kindergardensServices.Delete(id);

            if (kindergarden == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var kindergarden = await _kindergardensServices.DetailAsync(id);

            if (kindergarden == null)
            {
                return NotFound();
            }
            KindergardenImageViewModel[] images = await FilesFromDatabase(id);
            var vm = new KindergardenCreateUpdateViewModel();

            vm.Id = kindergarden.Id;
            vm.GroupName = kindergarden.GroupName;
            vm.ChildrenCount = kindergarden.ChildrenCount;
            vm.KindergardenName = kindergarden.KindergardenName;
            vm.TeacherName = kindergarden.TeacherName;
            vm.CreatedAt = kindergarden.CreatedAt;
            vm.UpdatedAt = kindergarden.UpdatedAt;
            vm.Image.AddRange(images);

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(KindergardenCreateUpdateViewModel vm)
        {
            var dto = new KindergardenDto()
            {
                Id = vm.Id,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergardenName = vm.KindergardenName,
                TeacherName = vm.TeacherName,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        KindergardenId = x.KindergartenId
                    }).ToArray()

            };

            var result = await _kindergardensServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var kindergarden = await _kindergardensServices.DetailAsync(id);

            if (kindergarden == null)
            {
                return NotFound();
            }

            KindergardenImageViewModel[] images = await FilesFromDatabase(id);
            var vm = new KindergardenDetailsViewModel();

            vm.Id = kindergarden.Id;
            vm.GroupName = kindergarden.GroupName;
            vm.ChildrenCount = kindergarden.ChildrenCount;
            vm.KindergardenName = kindergarden.KindergardenName;
            vm.TeacherName = kindergarden.TeacherName;

            vm.CreatedAt = kindergarden.CreatedAt;
            vm.UpdatedAt = kindergarden.UpdatedAt;
            vm.Image.AddRange(images);
            return View(vm);
        }

    // Meetod piltide toomiseks andmebaasist
        public async Task<KindergardenImageViewModel[]> FilesFromDatabase(Guid id)
        {
            var images = await _context.KindergardenFileToDatabase
                .Where(x => x.KindergardenId == id)
                .Select(y => new KindergardenImageViewModel
                {
                    KindergartenId = y.KindergardenId,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64, {0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            return images;
        }

        // Meetod ühe pilte eemaldamiseks andmebaasist
        [HttpPost]
        public async Task<IActionResult> RemoveImage(KindergardenImageViewModel vm)
        {
            var dto = new FileToDatabaseDto()
            {
                Id = vm.ImageId
            };

            var image = await _fileServices.RemoveImageFromDatabase(dto);

            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
