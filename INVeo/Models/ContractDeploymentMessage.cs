using INVeo.Models;
using System.Numerics;

public class ContractDeploymentMessage : BaseDeploymentMessage
{
    public string Title { get; set; }
    public string Description { get; set; }
    public BigInteger Price { get; set; }
    public string OwnerAddress { get; set; }

    public ContractDeploymentMessage(string byteCode) : base(byteCode)
    {
        // Constructor logic here
    }

    // Parameterless constructor if needed
    public ContractDeploymentMessage() : base(string.Empty) // Pass a parameter to the base class constructor
    {
    }
    //public ContractDeploymentMessage() { } //Error
}
