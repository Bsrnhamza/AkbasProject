using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingAkbas.Models
{
    public class ExportRequest
    {
        public List<string> Headers { get; set; }
        public List<List<string>> Data { get; set; }
    }
}
