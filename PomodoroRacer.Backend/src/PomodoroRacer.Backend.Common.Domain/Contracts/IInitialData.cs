namespace PomodoroRacer.Backend.Common.Domain.Contracts;

public interface IInitialData
{
    Type EntityType { get; }

    IEnumerable<object> GetData();
}