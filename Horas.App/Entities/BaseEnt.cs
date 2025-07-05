namespace Horas.Domain
{
    public abstract class BaseEnt : Auditable
    {
        public Guid Id { get; set; }
        public bool IsExist { get; set; } = true; //for soft delete txt-1 in solutionItems
    }
}
