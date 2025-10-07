using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;
using ShopTARgv24.Models.RealEstates;




namespace ShopTARgv24.Controllers
{
    public class RealEstateController : Controller
    {
        private readonly ShopTARgv24Context _context;
        private readonly IRealEstateServices _realestateServices;
        public RealEstateController
            (
                ShopTARgv24Context context,
                IRealEstateServices realEstatesServices
            )
        {
            _context = context;
            _realestateServices = realEstatesServices;
        }
        public IActionResult Index()
        {
            var result = _context.RealEstates
                .Select(x => new RealEstateIndexViewModel
                {
                    Id = x.Id,
                    Area = x.Area,
                    Location = x.Location,
                    RoomNumber = x.RoomNumber,
                    BuildingType = x.BuildingType
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel result = new();

            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Area = vm.Area,
                Location = vm.Location,
                RoomNumber = vm.RoomNumber,
                BuildingType = vm.BuildingType,
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.Id,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        RealEstateId = x.RealEstateId,
                    }).ToArray(),

                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,

            }; 

            var result = await _realestateServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var realestate = await _realestateServices.DetailAsync(id);

            if (realestate == null)
            {
                return NotFound();
            }

            var vm = new RealEstateCreateUpdateViewModel();

            vm.Id = realestate.Id;
            vm.Area = realestate.Area;
            vm.Location = realestate.Location;
            vm.RoomNumber = realestate.RoomNumber;
            vm.BuildingType = realestate.BuildingType;

            vm.CreatedAt = realestate.CreatedAt;
            vm.ModifiedAt = realestate.ModifiedAt;

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Area = vm.Area,
                Location = vm.Location,
                RoomNumber = vm.RoomNumber,
                BuildingType = vm.BuildingType,

                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            var result = await _realestateServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var realestate = await _realestateServices.DetailAsync(id);

            if (realestate == null)
            {
                return NotFound();
            }

            var vm = new RealEstateDeleteViewModel();

            vm.Id = realestate.Id;
            vm.Area = realestate.Area;
            vm.Location = realestate.Location;
            vm.RoomNumber = realestate.RoomNumber;
            vm.BuildingType = realestate.BuildingType;

            vm.CreatedAt = realestate.CreatedAt;
            vm.ModifiedAt = realestate.ModifiedAt;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var realestate = await _realestateServices.Delete(id);

            if (realestate == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var realestate = await _realestateServices.DetailAsync(id);

            if (realestate == null)
            {
                return NotFound();
            }

            var vm = new RealEstateDetailsViewModel();

            vm.Id = realestate.Id;
            vm.Area = realestate.Area;
            vm.Location = realestate.Location;
            vm.RoomNumber = realestate.RoomNumber;
            vm.BuildingType = realestate.BuildingType;

            vm.CreatedAt = realestate.CreatedAt;
            vm.ModifiedAt = realestate.ModifiedAt;

            return View(vm);
        }
    }
}
