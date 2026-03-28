namespace HL.Core.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync(); //SaveChangesAsync() işini görecek
}