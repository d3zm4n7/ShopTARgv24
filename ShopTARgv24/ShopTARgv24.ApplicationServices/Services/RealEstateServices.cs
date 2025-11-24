using Microsoft.EntityFrameworkCore;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;


namespace ShopTARgv24.ApplicationServices.Services
{
    public class RealEstateServices : IRealEstateServices
    {
        private readonly ShopTARgv24Context _context;
        private readonly IFileServices _fileServices;

        public RealEstateServices
            (
                ShopTARgv24Context context,
                IFileServices fileServices
            )
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<RealEstate> Create(RealEstateDto dto)
        {
            RealEstate domain = new RealEstate();

            domain.Id = Guid.NewGuid();
            domain.Area = dto.Area;
            domain.Location = dto.Location;
            domain.RoomNumber = dto.RoomNumber;
            domain.BuildingType = dto.BuildingType;
            domain.CreatedAt = DateTime.Now;
            domain.ModifiedAt = DateTime.Now;

            //peaks kontrollima, kas on faile voi ei ole
            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, domain);
            }
            
            await _context.RealEstates.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<RealEstate> DetailAsync(Guid id)
        {
            var result = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<RealEstate> Delete(Guid id)
        {
            var realestate = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);

            var images = await _context.FileToDatabases
                .Where(x => x.RealEstateId == id)
                .Select(x=>new FileToDatabaseDto
                {
                    Id = x.Id,
                    ImageTitle = x.ImageTitle,
                    RealEstateId = x.RealEstateId
                })
                .ToArrayAsync();
            await _fileServices.RemoveImageFromDatabase(images);
            _context.RealEstates.Remove(realestate);
            await _context.SaveChangesAsync();

            return realestate;
        }

        public async Task<RealEstate> Update(RealEstateDto dto)
        {
            var domain = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (domain == null)
                return null;

            domain.Area = dto.Area;
            domain.Location = dto.Location;
            domain.RoomNumber = dto.RoomNumber;
            domain.BuildingType = dto.BuildingType;
            domain.ModifiedAt = DateTime.Now;

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, domain);
            }

            await _context.SaveChangesAsync();
            return domain;
        }

    }
}


