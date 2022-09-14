using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class CategoryModel
    {


        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }


    }
}
