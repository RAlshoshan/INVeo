using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.Contracts;
using System.Threading.Tasks;
using System;


namespace INVeo.Services
{
    public class BlockchainService
    {
        private readonly Web3 web3;

        // The ABI for your smart contract
        private readonly string contractAbi = @"[Your Smart Contract ABI]";
        // Bytecode of your compiled smart contract
        private readonly string bytecode = "0x[Your Smart Contract Bytecode]";
        // Ethereum network URL and account for deploying
        private readonly string rpcUrl = "https://[YourEthereumNode].infura.io/v3/[YourProjectId]";
        private readonly string privateKey = "[YourPrivateKey]";
        private readonly string ownerAddress = "[YourEthereumAddress]";

        public BlockchainService()
        {
            web3 = new Web3(rpcUrl);
        }

        public async Task<string> DeployContractAsync(string title, string description, BigInteger price)
        {
            var deploymentMessage = new ContractDeploymentMessage(bytecode)
            {
                Title = title,
                Description = description,
                Price = price,
                OwnerAddress = ownerAddress
            };

            var deploymentHandler = web3.Eth.GetContractDeploymentHandler<ContractDeploymentMessage>();
            var transactionReceipt = await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage);

            return transactionReceipt.ContractAddress;
        }
    }
}
