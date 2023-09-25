namespace PomodoroRacer.Backend.Common.Domain.Contracts;

public interface IAuditableEntity
{
    public string CreatedBy { get; }

    public DateTime CreatedOn { get; }

    public string? ModifiedBy { get; }

    public DateTime? ModifiedOn { get; }

    void SetCreationProperties(string createdBy, DateTime createdOn);

    void SetModificationProperties(string? modifiedBy, DateTime modifiedOn);
}