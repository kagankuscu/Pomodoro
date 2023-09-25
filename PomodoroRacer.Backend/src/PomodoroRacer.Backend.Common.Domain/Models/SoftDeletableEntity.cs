using PomodoroRacer.Backend.Common.Domain.Contracts;

namespace PomodoroRacer.Backend.Common.Domain.Models;

public abstract class SoftDeletableEntity<TId> : Entity<TId>, ISoftDeletableEntity
    where TId : struct
{
    protected SoftDeletableEntity()
    {
    }

    protected SoftDeletableEntity(TId id)
        : base(id)
    {
    }

    public DateTime? DeletedAt { get; private set; }
    public string? DeletedBy { get; private set; }

    public void Delete(string? deletedBy)
    {
        this.DeletedBy = deletedBy;
        this.DeletedAt = DateTime.UtcNow;
    }

    public void Restore()
    {
        this.DeletedBy = null;
        this.DeletedAt = null;
    }
}