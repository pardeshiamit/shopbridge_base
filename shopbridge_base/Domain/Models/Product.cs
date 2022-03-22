using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Models
{
    [Table("ProductMaster")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [ForeignKey("CategoryMaster")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("UnitMaster")]
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
    }
}
