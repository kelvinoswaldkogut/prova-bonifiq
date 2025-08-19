namespace ProvaPub.Strategies.Interfaces
{
    public interface IPayment
    {
        Task<string> InsertPayment(decimal value);
    }
}
