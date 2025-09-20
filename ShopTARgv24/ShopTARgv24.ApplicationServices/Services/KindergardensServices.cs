using Microsoft.EntityFrameworkCore;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;


namespace ShopTARgv24.ApplicationServices.Services
{
    public class KindergardensServices : IKindergardensServices
    {
        private readonly ShopTARgv24Context _context;

        public KindergardensServices
            (
                ShopTARgv24Context context
            )
        {
            _context = context;

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

            _context.Kindergardens.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }
    }
}
