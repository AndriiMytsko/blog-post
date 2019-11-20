using System;

namespace BlogPost.Bll.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message)
          : base(message)
        { }
    }
}