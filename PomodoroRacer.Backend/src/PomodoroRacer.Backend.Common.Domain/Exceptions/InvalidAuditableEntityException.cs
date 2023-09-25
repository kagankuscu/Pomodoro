namespace PomodoroRacer.Backend.Common.Domain.Exceptions;

public class InvalidAuditableEntityException : BaseDomainException
{
    public InvalidAuditableEntityException() { }

    public InvalidAuditableEntityException(string error) => this.Error = error;
}