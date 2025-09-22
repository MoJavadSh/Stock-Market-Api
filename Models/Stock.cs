using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Stock
    {
        [Key]
        public Guid Id { get; set; }  = Guid.NewGuid();
        [Required]
        [MaxLength(50)]
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public int Purchase { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public int LastDiv { get; set; }

        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();


    }
}