using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services.APIClientService.DTOs
{
    public class RepoPullTemplateDTO
    {
        public IList<ConvertedFileElementDTO> Elements { get; set; }
    }
}
