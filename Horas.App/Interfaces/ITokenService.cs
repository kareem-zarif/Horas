namespace Horas.Domain.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(Person user);
    }
}
