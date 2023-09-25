namespace PomodoroRacer.Backend.Common.Domain.Contracts;

public interface ISoftDeletableEntity
{
    DateTime? DeletedAt { get; }

    string? DeletedBy { get; }

    void Delete(string? deletedBy);
    void Restore();
}