using ShopTARgv24.Core.Dto;
using System.Threading.Tasks;

namespace ShopTARgv24.Core.ServiceInterface
{
    public interface IChuckNorrisServices
    {
        Task<ChuckNorrisJokeDto> GetRandomJoke();
    }
}