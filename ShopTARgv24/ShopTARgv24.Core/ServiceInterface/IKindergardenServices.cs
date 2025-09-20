using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;

namespace ShopTARgv24.Core.ServiceInterface
{
    public interface IKindergardensServices
    {
        Task<Kindergarden> Create(KindergardenDto dto);
        Task<Kindergarden> DetailAsync(Guid id);
        Task<Kindergarden> Delete(Guid id);
        Task<Kindergarden> Update(KindergardenDto dto);
    }
}
