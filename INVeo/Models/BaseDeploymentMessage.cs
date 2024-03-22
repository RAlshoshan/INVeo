using System.Numerics;
namespace INVeo.Models
{
    public abstract class BaseDeploymentMessage
    {
        public string ByteCode { get; set; }

        protected BaseDeploymentMessage(string byteCode)
        {
            ByteCode = byteCode;
        }
    }
}

