using PomodoroRacer.Backend.Common.Domain.Contracts;

namespace PomodoroRacer.Backend.Common.Domain.Models;

public abstract class Entity<TId> : IEntity
    where TId : struct
{
    private readonly ICollection<IDomainEvent> _events;
    protected Entity()
    {
        this._events = new List<IDomainEvent>();
    }

    protected Entity(TId id)
    {
        this.Id = id;
        this._events = new List<IDomainEvent>();
    }

    public TId Id { get; private set; } = default;

    public IReadOnlyCollection<IDomainEvent> Events => this._events.ToList();

    public void ClearEvents() => this._events.Clear();

    public void AddEvent(IDomainEvent domainEvent)
        => this._events.Add(domainEvent);

    public override bool Equals(object? obj)
    {
        if (!(obj is Entity<TId> other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (this.GetType() != other.GetType())
        {
            return false;
        }

        if (this.Id.Equals(default(TId)) || other.Id.Equals(default(TId)))
        {
            return false;
        }

        return this.Id.Equals(other.Id);
    }

    public static bool operator ==(Entity<TId>? first, Entity<TId>? second)
    {
        if (first is null && second is null)
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        return first.Equals(second);
    }

    public static bool operator !=(Entity<TId>? first, Entity<TId>? second) => !(first == second);

    public override int GetHashCode() => (this.GetType().ToString() + this.Id).GetHashCode();
}