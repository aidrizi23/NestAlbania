namespace NestAlbania.Models
{
    public class AgentForCreationDto
    {
       public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int LicenseNumber { get; set; }
        public string Motto { get; set; }
        public string YearsOfExeperience { get; set; }
    }
}
