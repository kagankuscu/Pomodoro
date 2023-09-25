namespace PomodoroRacer.Backend.Common.Domain.Exceptions;

public class InvalidSoftDeletableEntityException : BaseDomainException
{
    public InvalidSoftDeletableEntityException() { }

    public InvalidSoftDeletableEntityException(string error) => this.Error = error;
}