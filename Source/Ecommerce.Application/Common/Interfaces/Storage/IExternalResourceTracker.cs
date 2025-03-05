namespace Ecommerce.Application.Common.Interfaces.Storage;

public interface IExternalResourceTracker
{
    void AddUploadedImage(string objectIdentifier);
    Task RollbackAsync();
}
