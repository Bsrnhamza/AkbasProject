using System.ComponentModel.DataAnnotations;

namespace TestingAkbas.Models
{
    public class Fabric
    {
        [Key]
        public int Id { get; set; }
        public string QualityClass { get; set; }
        public string FabricCode { get; set; }
        public string QualityName { get; set; }
        public string QualityGroup { get; set; }
        public string QualityComposition { get; set; }
        public string PatternType { get; set; }
        public int Width { get; set; }
        public int Weight { get; set; }
        public decimal RawFabricPrice { get; set; }
        public decimal DomesticPrice { get; set; }
        public decimal ExportPrice { get; set; }
        public string Qualities { get; set; }
    }
}
