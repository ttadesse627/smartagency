using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Pagess
{
    public class CreatePostImageRequest
    {
        public ICollection<string> SliderImages { get; set; } = [];
    }
}