namespace EmployeeManagement.Infrastructure.Middelwares.GlobalExceptionHandlingMiddleware
{
    public abstract class IdentityException : Exception
    {
        public IdentityException(string message) : base(message){}
    }

    public class IdentityNotFoundException : IdentityException
    {
        public IdentityNotFoundException(string message) : base(message){}
    }

    public class IdentityBadRequestException : IdentityException
    {
        public IdentityBadRequestException(string message) : base(message){}
    }

    public class IdentityUnAuthorizedException : IdentityException
    {
        public IdentityUnAuthorizedException(string message) : base(message){}
    }

    public class IdentityForbiddenException : IdentityException
    {
        public IdentityForbiddenException(string message) : base(message){}
    }

    public class IdentityServiceNotFound : IdentityException
    {
        public IdentityServiceNotFound(string message) : base(message){}
    }

     public class IdentityUserExistException : IdentityException
    {
        public IdentityUserExistException(string message) : base(message){}
    }

}