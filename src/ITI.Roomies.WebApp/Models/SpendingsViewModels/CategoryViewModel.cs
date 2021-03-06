using System.ComponentModel.DataAnnotations;

namespace ITI.Roomies.WebApp.Models.Category
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set;  }
   
        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string IconName { get; set; }

        [Required]
        public int CollocId { get; set; }
    }
}
