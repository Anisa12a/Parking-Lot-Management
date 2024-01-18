using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotManagement.Entities
{
    public class Subscribers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubscriberId { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string IdCardNumber { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }

        [Required]
        public string PlateNumber { get; set; } = string.Empty;

        [Required]
        public bool IsDeleted { get; set; }

        //We add constructor for non-nullable properties:
       /* public Subscribers(string firstName, string lastName, string idCardNumber, string email, string phoneNumber, string plateNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            IdCardNumber = idCardNumber;
            Email = email;
            PhoneNumber = phoneNumber;
            PlateNumber = plateNumber;
        } */
    }
}
