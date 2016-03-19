namespace Ipfs.Json
{
    public class IpfsObjectStat
    {
        public string Hash { get; set; }
        public int NumLinks { get; set; }
        public int BlockSize { get; set; }
        public int LinksSize { get; set; }
        public int DataSize { get; set; }
        public int CumulativeSize { get; set; }
    }
}
