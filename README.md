# net-ipfs-api v0.3.11 beta
.NET portable class library (PCL) for the InterPlanetary File System (IPFS) API. _Currently a work in progress_

This library should be easy to use if you're familiar with the IPFS CLI. Top level commands (eg. `ipfs cat`, `ipfs add`) are methods in `IpfsClient`. Subcommands are methods in the IPFS client's properties named after the subcommands.

ie. `ipfs swarm peers` becomes `ipfs.Swarm.Peers()` and `ipfs add <file>` becomes `ipfs.Add("file")`. Easy!

The only exception is if a valid 'top level' command has subcommands, since C# can't have methods and properties with the same name. ie. `ipfs config` and `ipfs config edit` becomes `ipfs.ConfigCommand()` and `ipfs.Config.Edit()`.

CLI arguments are method parameters, and options are optional parameters.

## Examples

### Adding & reading a file:

Since net-ipfs-api is a PCL, we have to do our file IO with streams.

    using (var ipfs = new IpfsClient())
    {
      //Name of the file to add
      string fileName = "test.txt";

      using(Stream inputStream = File.OpenRead(fileName))
      {
        //Wrap our stream in an IpfsStream, so it has a file name.
        MerkleNode node = await ipfs.Add(new IpfsStream(fileName, inputStream));

        Stream outputStream = await ipfs.Cat(MerkleNode.Hash);

        using(StreamReader sr = new StreamReader(outputStream));
        {
          string contents = sr.ReadToEnd();

          Console.WriteLine(contents); //Contents of test.txt are printed here!
        }
      }
    }


### Disconnect from all peers:
    using (var ipfs = new IpfsClient())
    {
        var peers = await ipfs.Swarm.Peers();
        await ipfs.Swarm.Disconnect(peers);
    }
