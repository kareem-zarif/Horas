namespace Horas.Domain
{
    public enum OrderStatus : byte
    {
        pending = 1,
        Confirmed = 2,
        Shipped = 3,
        Deliverd = 4,
        Cancelled = 5,
        Returned = 6
    }
}
