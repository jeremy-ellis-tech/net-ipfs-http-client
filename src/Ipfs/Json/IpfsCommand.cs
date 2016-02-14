using System.Collections.Generic;

namespace Ipfs.Json
{
    public class IpfsCommand
    {
        public string Name { get; set; }
        public List<IpfsCommand> Subcommands { get; set; }
        public List<Option> Options { get; set; }
        public bool ShowOptions { get; set; }
    }

    public class Option
    {
    }
}
