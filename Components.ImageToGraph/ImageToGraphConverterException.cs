using System;
using System.Runtime.Serialization;

namespace Components.ImageToGraph
{
    [Serializable]
    internal class ImageToGraphConverterException : Exception
    {
        public ImageToGraphConverterException()
        {
        }

        public ImageToGraphConverterException(string message) : base(message)
        {
        }

        public ImageToGraphConverterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ImageToGraphConverterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}