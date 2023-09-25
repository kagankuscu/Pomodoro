namespace PomodoroRacer.Backend.Common.Domain.Contracts;

public interface IDomainEventEntity
{
    IReadOnlyCollection<IDomainEvent> Events { get; }

    void ClearEvents();
}