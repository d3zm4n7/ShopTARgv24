using Microsoft.EntityFrameworkCore;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;
using ShopTARgv24.Data.Migrations;


namespace ShopTARgv24.ApplicationServices.Services
{
    public class KindergardensServices : IKindergardensServices
    {
        private readonly ShopTARgv24Context _context;
        private readonly IFileServices _fileServices;
        public KindergardensServices
            (
                ShopTARgv24Context context,
                IFileServices fileServices

            )
        {
            _context = context;
            _fileServices = fileServices;
        }
        public async Task<Kindergarden> Create(KindergardenDto dto)
        {
            Kindergarden kindergarden = new Kindergarden();

            kindergarden.Id = Guid.NewGuid();
            kindergarden.GroupName = dto.GroupName;
            kindergarden.ChildrenCount = dto.ChildrenCount;
            kindergarden.KindergardenName = dto.KindergardenName;
            kindergarden.TeacherName = dto.TeacherName;

            kindergarden.CreatedAt = DateTime.Now;
            kindergarden.UpdatedAt = DateTime.Now;

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, kindergarden);
            }
            await _context.Kindergardens.AddAsync(kindergarden);
            await _context.SaveChangesAsync();

            return kindergarden;
        }

        public async Task<Kindergarden> DetailAsync(Guid id)
        {
            var result = await _context.Kindergardens
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Kindergarden> Delete(Guid id)
        {
            var kindergarden = await _context.Kindergardens
                .FirstOrDefaultAsync(x => x.Id == id);

            var images = await _context.KindergardenFileToDatabase
                .Where(x => x.KindergardenId == id)
                .Select(y => new FileToDatabaseDto
                {
                    Id = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    KindergardenId = y.KindergardenId
                }).ToArrayAsync();

            await _fileServices.RemoveImagesFromDatabase(images);
            _context.Kindergardens.Remove(kindergarden);
            await _context.SaveChangesAsync();

            return kindergarden;
        }

        public async Task<Kindergarden> Update(KindergardenDto dto)
        {
            Kindergarden domain = new();

            domain.Id = dto.Id;
            domain.GroupName = dto.GroupName;
            domain.ChildrenCount = dto.ChildrenCount;
            domain.KindergardenName = dto.KindergardenName;
            domain.TeacherName = dto.TeacherName;

            domain.CreatedAt = dto.CreatedAt;
            domain.UpdatedAt = DateTime.Now;

            _fileServices.UploadFilesToDatabase(dto, domain);
            _context.Kindergardens.Update(domain);

            await _context.SaveChangesAsync();

            return domain;
        }

    }
}
