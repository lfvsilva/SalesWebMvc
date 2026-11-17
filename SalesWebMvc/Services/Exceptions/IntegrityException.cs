namespace SalesWebMvc.Services.Exceptions;

public class IntegrityException(string message, Exception? innerException = null)
    : Exception(message, innerException);
