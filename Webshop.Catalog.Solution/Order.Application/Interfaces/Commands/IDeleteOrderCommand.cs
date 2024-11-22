namespace Order.Application.Interfaces.Commands;

public interface IDeleteOrderCommand
{
    void Delete(Guid id);
}