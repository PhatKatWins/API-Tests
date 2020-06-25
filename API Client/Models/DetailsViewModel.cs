using API_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Client.Models
{
    public class DetailsViewModel
    {
        public Phrase Phrase { get; set; }
        public IEnumerable<Variant> Variants { get; set; }
    }
}
