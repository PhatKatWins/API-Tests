using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Service.Models
{
    public class Phrase
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
