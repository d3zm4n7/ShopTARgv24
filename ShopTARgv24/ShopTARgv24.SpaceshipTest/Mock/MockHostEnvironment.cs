using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;


namespace ShopTARgv24.SpaceshipTest.Mock
{
    public class MockHostEnvironment : IHostEnvironment
    {
        public string EnvironmentName { get; set; } = "Development";
        public string ApplicationName { get; set; } = "SpaceshipTest";
        public string ContentRootPath { get; set; } = Directory.GetCurrentDirectory();
        public IFileProvider ContentRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}