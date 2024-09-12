namespace NestAlbania.Data;

public class Notification : BaseEntity
{
    
    public bool IsRead { get; set; }
    public string? Title { get; set; }
    public string Message { get; set; }
    
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    
    public DateTime CreatedOn { get; set; }
    
}