namespace PomodoroRacer.Backend.Common.Domain.Contracts;

public interface IFactory<out TEntity>
    where TEntity : IAggregateRoot
{
    TEntity Build();
}