using NuGet.Common;

namespace NestAlbania.Data
{
    public class Agent : BaseEntity

    {
        internal DateTime SaleDate;

        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Image {  get; set; }
       
        public string PhoneNumber { get; set; }
        public int LicenseNumber { get; set; }
        public string Motto { get; set; }
        public int YearsOfExeperience { get; set; }

        

        public string? UserId { get; set; } // do te perdoret qe kur te krijhet te lidhet, te krijohet automatikisht nje user me te njejtin email dhe password
        public string Email { get; set; }
        public string? Password { get; set; }

        public string? RoleId { get; set; }

        public ICollection<Property>? Properties { get; set;}
        

    }
}