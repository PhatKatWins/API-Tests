using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Service.Models
{
    public class Variant
    {
        public int Id { get; set; }
        public int PhraseId { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
