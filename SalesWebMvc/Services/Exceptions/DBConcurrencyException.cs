namespace SalesWebMvc.Services.Exceptions;

public class DBConcurrencyException(string message, Exception? innerException = null)
    : Exception(message, innerException);
