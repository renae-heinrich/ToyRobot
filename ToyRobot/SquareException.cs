using System;

namespace ToyRobot
{
    public class SquareException : Exception
    {
        public SquareException(){}
        
        public SquareException(string message):base(message){}
    }
}