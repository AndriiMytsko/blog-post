using System;

namespace BlogPost.Bll.Exceptions
{
    public class UnauthorizedException: ApplicationException
    {
        public UnauthorizedException(string message)
            : base(message)
        { }
    }
}