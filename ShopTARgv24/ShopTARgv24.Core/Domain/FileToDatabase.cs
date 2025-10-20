using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARgv24.Core.Domain
{
    public class FileToDatabase
    {
        public Guid Id { get; set; }
        public string? ImageTitle { get; set; }
        public byte[]? ImageData { get; set; }
        public Guid? KindergardenId { get; set; }
    }
}
