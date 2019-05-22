using System.ComponentModel.DataAnnotations;

namespace ITI.Roomies.WebApp.Models.Category
{
    public class CategoryViewModel
    {
        //public int CategoryId { get; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        public int CollocId { get; set; }
    }
}
