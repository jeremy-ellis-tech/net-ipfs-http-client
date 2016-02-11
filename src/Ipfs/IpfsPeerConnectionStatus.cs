namespace Ipfs
{
    public class IpfsPeerConnectionStatus
    {
        public IpfsPeerConnectionStatus(string command, MultiHash id, bool success)
        {
            Command = command;
            ID = id;
            Success = success;
        }

        public string Command { get; private set; }

        public MultiHash ID { get; private set; }

        public bool Success { get; set; }
    }
}
