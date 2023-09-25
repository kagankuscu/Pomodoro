namespace PomodoroRacer.Backend.Common.Domain.Exceptions;

public class BaseDomainException : Exception
{
    private string? _error;

    public string Error
    {
        get => this._error ?? base.Message;
        set => this._error = value;
    }
}