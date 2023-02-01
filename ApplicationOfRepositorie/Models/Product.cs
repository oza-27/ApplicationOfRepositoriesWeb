using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationOfRepositorie.Models
{
    public class Product
    {
        
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string ISBN { get; set; }
        
        [Required]
        public string Author { get; set; }
        
        [Required]
        [Display(Name ="List Price")]
        public double listPrice { get; set; }
        
        [Required]
        [Display(Name ="Price for 1-50")]
        public double Price { get; set; }
        
        [Required]
        [Display(Name ="Price for 51-100")]
        public double Price50 { get; set; }
        
        [Required]
        [Display(Name ="Price for 100+")]
        public double Price100 { get; set; }
        
        [ValidateNever]
        public string ImageUrl { get; set; }
        
        [Required]
        [Display(Name ="Choose Category Type")]
        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        
        [Required]
        [Display(Name ="Choose Cover Type")]
        public int CoverTypeId { get; set; }
        
        [ForeignKey("CoverTypeId")]
        [ValidateNever]
        public CoverType CoverType { get; set; }

    }
}
