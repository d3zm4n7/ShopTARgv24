using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;

namespace ShopTARgv24.Core.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(SpaceshipDto dto, Spaceship spaceship);

        //Task<FileToApi> RemoveImageFromApi(FileToApiDto dto);
        //Task<List<FileToApi>> RemoveAllImagesFromApi(FileToApiDto[] dtos);
        void UploadFilesToDatabase(RealEstateDto dto, RealEstate domain);
        void UploadFilesToDatabaseKindergarden(KindergardenDto dto, Kindergarden domain);
    }
}
