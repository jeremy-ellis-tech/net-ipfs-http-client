using System.Collections.Generic;

namespace Ipfs.Json
{
    public class Option
    {
    }

    public class Subcommand
    {
        public string Name { get; set; }
        public List<object> Subcommands { get; set; }
        public List<Option> Options { get; set; }
        public bool ShowOptions { get; set; }
    }

    public class IpfsCommands
    {
        public string Name { get; set; }
        public List<Subcommand> Subcommands { get; set; }
        public List<Option> Options { get; set; }
        public bool ShowOptions { get; set; }
    }
}
