using System.Runtime.CompilerServices;

namespace INVeo.Models
{
    public class Idea
    {
        public int Id { get; set; }
        //public IFormFile File { get; set; }
        //public string BlockchainIdentity { get; set; }
        //public Object IdBlockchain { get; set; } Comment because Asp.net not supported (object)
                
        public string Title { get; set; }
        public string Description { get; set; }
        public int IdeaId { get; set; }
        public string Seller { get; set; }
        public bool IsAccepted { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        // Address for blockchain
        public string SmartContractAddress { get; set; }
        public string? IdeaFile { get; set; }
    }
}
