namespace NestAlbania.Models.DtoForEdit;

public class AgentForEditDto
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public string Surname { get; set; }
        
    public IFormFile? Image {  get; set; }
    
    public string PhoneNumber { get; set; }
    
    public int LicenseNumber { get; set; }
    
    public string Motto { get; set; }
    
    public int YearsOfExeperience { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
}