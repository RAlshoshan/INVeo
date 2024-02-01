namespace INVeo.Models
{
    public class Idea
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brief { get; set; }
        public string? IdeaFile { get; set; }
        //public IFormFile File { get; set; }
        public bool IsAccepted { get; set; }
        //public string BlockchainIdentity { get; set; }
    }
}
