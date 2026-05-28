using HL.Core.Common;

public class UserClaim : BaseEntity
{
    public int AppUserId { get; set; }
    public string ClaimType { get; set; }=string.Empty;//Permission
    public string ClaimValue { get; set; }=string.Empty;//Örn: "Laboratory.Read"
}