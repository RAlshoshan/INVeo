using Nethereum.Web3;

namespace INVeo.Services
{
    public class EthreumService
    {
        private readonly Web3 _web3;
        private readonly string _contractAddress;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public EthreumService(IConfiguration config)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _web3 = new Web3(config["Ethereum:Url"]);
#pragma warning disable CS8601 // Possible null reference assignment.
            _contractAddress = config["Ethereum:ContractAddress"];
#pragma warning restore CS8601 // Possible null reference assignment.
        }

        // Implement methods to interact with your smart contract
    }
    internal class Web3
    {
        private string? v;

        public Web3(string? v)
        {
            this.v = v;
        }
    }
}
