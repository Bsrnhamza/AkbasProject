using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestingAkbas.Models
{
    public class FabricViewModel
    {
        public IEnumerable<Fabric> Fabrics { get; set; }
        public IEnumerable<SelectListItem> FabricOptions { get; set; }
        public string SelectedFabricCode { get; set; }
    }
}
