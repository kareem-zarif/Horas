namespace Horas.Domain
{
    public interface IBaseEnt : IAuditable
    {
        Guid Id { get; set; }
        bool IsExist { get; set; } //for soft delete txt-1 in solutionItems
    }
}
