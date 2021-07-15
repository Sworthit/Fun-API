using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FunApi.Model
{
    [Table("Names")]
    public class Name
    {
        [Key]
        [Required]
        public int nameId { get; set; }

        [Required]
        public string name { get; set; }
    }
}
