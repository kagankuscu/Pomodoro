using PomodoroRacer.Backend.Common.Domain.Contracts;
using PomodoroRacer.Backend.Common.Domain.Exceptions;

namespace PomodoroRacer.Backend.Common.Domain.Models;

public abstract class AuditableEntity<TId> : SoftDeletableEntity<TId>, IAuditableEntity
    where TId : struct
{
    protected AuditableEntity()
    {
        this.CreatedBy = default!;
        this.CreatedOn = default!;
        this.ModifiedBy = default!;
        this.ModifiedOn = default!;
    }

    protected AuditableEntity(TId id)
        : base(id)
    {
        this.CreatedBy = default!;
        this.CreatedOn = default!;
        this.ModifiedBy = default!;
        this.ModifiedOn = default!;
    }

    public string CreatedBy { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public string? ModifiedBy { get; private set; }

    public DateTime? ModifiedOn { get; private set; }

    public void SetCreationProperties(string createdBy, DateTime createdOn)
    {
        this.CreatedBy = createdBy;
        this.CreatedOn = createdOn;
    }

    public void SetModificationProperties(string? modifiedBy, DateTime modifiedOn)
    {
        Guard.AgainstNull<string?, InvalidAuditableEntityException>(modifiedBy, nameof(this.ModifiedBy));

        this.ModifiedBy = modifiedBy;
        this.ModifiedOn = modifiedOn;
    }
}