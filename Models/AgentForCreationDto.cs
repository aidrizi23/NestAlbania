namespace NestAlbania.Models
{
    public class AgentForCreationDto
    {
       public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Image { get; set; }

        public int PhoneNumber { get; set; }
        public int LicenseNumber { get; set; }
        public string Motto { get; set; }
        public string YearsOfExeperience { get; set; }

        public string? Email { get; set; }

        //public string? UserId { get; set; }
        public string Password { get; set; }


        // kjo do te perdoret per krijimin automatik te nje roli
        public string RoleId = "a14bs9c0-aa65-4af8-bd17-00bd9344e575"; // id e ketij roli eshte marre nga ApplicationDbContext.
        // per momentin eshte pa set sepse nuk do ndryshohet. te gjithe agjetnet do te kene te njejtin rol

        public int? PropertyId { get; set; }
    }
}
