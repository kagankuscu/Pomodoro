using PomodoroRacer.Backend.Common.Domain.Exceptions;

namespace PomodoroRacer.Backend.Common.Domain;

public static class Guard
{
    public static void AgainstEmptyString<TException>(string? value, string name = "Value")
        where TException : BaseDomainException, new()
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        ThrowException<TException>($"{name} cannot be null ot empty.");
    }

    public static void AgainstNull<TInput, TException>(TInput input, string name = "Value")
        where TException : BaseDomainException, new()
    {
        if (input != null)
        {
            return;
        }

        ThrowException<TException>($"{name} must not be null.");
    }

    private static void ThrowException<TException>(string message)
        where TException : BaseDomainException, new()
    {
        var exception = new TException
        {
            Error = message
        };

        throw exception;
    }
}