using System.ComponentModel.DataAnnotations;

namespace CustomerTestTask.Data.Entities
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public int Age { get; set; }
    }
}