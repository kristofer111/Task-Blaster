namespace TaskBlaster.TaskManagement.API.Services.Interfaces;

public interface IClaimsService
{
    public string RetrieveUserEmailClaim();
    public string RetrieveUserNameClaim();
}