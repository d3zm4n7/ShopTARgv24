using ShopTARgv24.Core.Dto;
using ShopTARgv24.Data;
using Microsoft.Extensions.Hosting;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.ServiceInterface;

namespace ShopTARgv24.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly ShopTARgv24Context _context;
        private readonly IHostEnvironment _webHost;

        public FileServices
            (
                ShopTARgv24Context context,
                IHostEnvironment webHost
            )
        {
            _context = context;
            _webHost = webHost;
        }

        //public async Task<FileToApi> RemoveImageFromApi(FileToApiDto dto) { }
        //public async Task<List<FileToApi>> RemoveAllImagesFromApi(FileToApiDto [] dtos) { }
        public void FilesToApi(SpaceshipDto dto, Spaceship spaceship)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                if (!Directory.Exists(_webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\"))
                {
                    Directory.CreateDirectory(_webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\");
                }

                foreach (var file in dto.Files)
                {
                    //muutuja string uploadsFolder ja sinna laetakse failid
                    string uploadsFolder = Path.Combine(_webHost.ContentRootPath,"wwwroot", "multipleFileUpload");
                    //muutuja string uniqueFileName ja siin genereeritakse uus Guid ja lisatakse see faili ette
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    //muutuja string filePath kombineeritakse ja lisatakse koos kausta unikaalse nimega
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);

                        FileToApi path = new FileToApi
                        {
                            Id = Guid.NewGuid(),
                            ExistingFilePath = uniqueFileName,
                            SpaceshipId = spaceship.Id
                        };

                        _context.FileToApis.AddAsync(path);
                    }
                }
            }
        }

        public void UploadFilesToDatabase(RealEstateDto dto, RealEstate domain)
        {
            if (dto.Files != null && dto.Files.Any())
            {
                foreach (var file in dto.Files)
                {
                    if (file.Length > 0)
                    {
                        using (var target = new MemoryStream())
                        {
                            FileToDatabase files = new FileToDatabase() 
                            {
                                Id = Guid.NewGuid(),
                                ImageTitle = file.FileName,
                                RealEstateId = domain.Id
                            };

                            file.CopyTo(target);
                            files.ImageData = target.ToArray();

                            _context.FileToDatabases.Add(files);
                        }
                    }
                }
            }
        }
    }
}
