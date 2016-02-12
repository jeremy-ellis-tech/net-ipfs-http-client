# net-ipfs-api v0.1
.NET portable class library (PCL) for the InterPlanetary File System (IPFS) API.

_Currently a work in progress_

This library should be easy to use if you're familiar with the IPFS cli. Top level commands (eg. `ipfs cat`, `ipfs add`) are methods in `IpfsClient`. Subcommands are methods in the IPFS client's properties named after the subcommands.

ie. `ipfs swarm peers` becomes `ipfs.Swarm.Peers()` and `ipfs add <file>` becomes `ipfs.Add("file")`. Easy!

The only exception is if a valid 'top level' command has subcommands, since C# can't have methods and properties with the same name. ie. `ipfs config` and `ipfs config edit` becomes `ipfs.ConfigCommand()` and `ipfs.Config.Edit()`

## Examples
### Disconnect from all peers
    using (var ipfs = new IpfsClient())
    {
        var peers = await ipfs.Swarm.Peers();
        await ipfs.Swarm.Disconnect(peers);
    }

## TODO
* Serialize to types from JSON
* Base58 tests
* MultiHash/Address tests
