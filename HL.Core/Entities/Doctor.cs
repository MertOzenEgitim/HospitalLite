using HMS.Core.Common;

namespace HMS.Core.Entities;

public class Doctor : BaseEntity
{
    public int AppUserId { get; set; }
    public virtual AppUser AppUser { get; set; } = null!;
    
    public int DepartmentId { get; set; }
    public virtual Department Department { get; set; } = null!;
    
    public string Title { get; set; } = string.Empty; // Uzm. Dr, Doç. vb.
}