using System;

namespace DealerApp.Core.Exceptions
{
    public class BussinessException : Exception
    {
        public int statusCode { get; set; }
        public string message { get; set; }

        public BussinessException() { }

        public BussinessException(string Message, int StatusCode) : base(Message)
        {
            this.message = Message;
            this.statusCode = StatusCode;
        }
    }
}