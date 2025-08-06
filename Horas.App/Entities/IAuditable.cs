namespace Horas.Domain
{
    public interface IAuditable
    {
        Guid? CreatedBy { get; set; }
        DateTime? CreatedOn { get; set; }
        Guid? ModifiedBy { get; set; }
        DateTime? ModifiedOn { get; set; }
    }
}
