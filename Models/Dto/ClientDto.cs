using System.ComponentModel.DataAnnotations;

namespace API_CRUD.Models.Dto
{
    public class ClientDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)] 
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Order { get; set; }

    }
}
