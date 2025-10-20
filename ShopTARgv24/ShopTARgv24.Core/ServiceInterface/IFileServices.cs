using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;

namespace ShopTARgv24.Core.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(SpaceshipDto dto, Spaceship spaceship);
        void UploadFilesToDatabase(KindergardenDto dto, Kindergarden domain);
        Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto);

        Task<FileToDatabase> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos);
    }
}
