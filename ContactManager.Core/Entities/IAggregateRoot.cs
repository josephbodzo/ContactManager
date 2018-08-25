namespace ContactManager.Core.Entities
{
    public interface IAggregateRoot <T>
    {
         T Id { get; set; }
    }
}
