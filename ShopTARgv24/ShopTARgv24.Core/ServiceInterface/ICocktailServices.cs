using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARgv24.Core.ServiceInterface
{
    public interface ICocktailServices
    {
        Task<string> SearchCocktails(string searchTerm);
    }
}
