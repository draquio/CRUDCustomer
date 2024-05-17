using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDCustomer.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public required string Email { get; set; }
        public int Age { get; set; }
    }
}
