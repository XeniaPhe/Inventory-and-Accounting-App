namespace Xenia.IaA.AppDomain.Exceptions;
public class DatabaseCreationException : Exception
{
    internal DatabaseCreationException(string? message) : base($"Database Creation Exception:\n {message}")
    {
    }
}