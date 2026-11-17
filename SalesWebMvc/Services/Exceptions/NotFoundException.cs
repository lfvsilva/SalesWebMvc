namespace SalesWebMvc.Services.Exceptions;

public class NotFoundException(string message, Exception? innerException = null)
    : Exception(message, innerException);
