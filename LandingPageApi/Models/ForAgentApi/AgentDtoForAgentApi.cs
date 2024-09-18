namespace LandingPageApi.Models;

public class AgentDtoForAgentApi
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string? Image {  get; set; }
    public required string PhoneNumber { get; set; }
    public required int LicenseNumber { get; set; }
    public required string Motto { get; set; }
    public required int YearsOfExeperience { get; set; }
    public required string? UserId { get; set; }
    public required string Email { get; set; }
    public required bool IsDeleted { get; set; }
    
    public ICollection<PropertyDtoForAgentApi>? PropertyDto { get; set; }
}