namespace Order.Application.Interfaces.Commands;

public interface IDeleteOrderCommand
{
    void Delete(int id);
}