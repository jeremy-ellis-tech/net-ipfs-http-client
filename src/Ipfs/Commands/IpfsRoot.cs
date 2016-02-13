using Ipfs.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.IO;

namespace Ipfs.Commands
{
    public class IpfsRoot : IpfsCommand
    {
        public IpfsRoot(Uri commandUri, HttpClient httpClient) : base(commandUri, httpClient) { }

        /// <summary>
        /// Add an object to ipfs.
        /// Adds contents of <path> to ipfs. Use -r to add directories.
        /// Note that directories are added recursively, to form the ipfs
        /// MerkleDAG.A smarter partial add with a staging area(like git)
        /// remains to be implemented
        /// </summary>
        /// <param name="path">The path to a file to be added to IPFS</param>
        /// <param name="recursive">Add directory paths recursively</param>
        /// <param name="quiet">Write minimal output</param>
        /// <param name="progress">Stream progress data</param>
        /// <param name="wrapWithDirectory">Wrap files with a directory object</param>
        /// <param name="trickle">Use trickle-dag format for dag generation</param>
        /// <returns></returns>
        public async Task<HttpContent> Add(string path, bool recursive = false, bool quiet = false, bool progress = false, bool wrapWithDirectory = false, bool trickle = false)
        {
            var flags = new Dictionary<string, string>();

            if (recursive)
            {
                flags.Add("recursive", "true");
            }

            if (quiet)
            {
                flags.Add("quiet", "true");
            }

            return await ExecuteAsync("add", ToEnumerable(path), flags);
        }

        /// <summary>
        /// Show IPFS object data
        /// Retrieves the object named by <ipfs-path> and outputs the data
        /// it contains.
        /// </summary>
        /// <param name="ipfsPath">The path to the IPFS object(s) to be outputted</param>
        /// <returns></returns>
        public async Task<Stream> Cat(string ipfsPath)
        {
            HttpContent content = await ExecuteAsync("cat", ToEnumerable(ipfsPath), null);
            return await content.ReadAsStreamAsync();
        }

        /// <summary>
        /// List all available commands.
        /// Lists all available commands (and subcommands) and exits.
        /// </summary>
        /// <returns></returns>
        public async Task<HttpContent> Commands()
        {
            return await ExecuteAsync("commands", null, null);
        }

        /// <summary>
        /// get and set IPFS config values
        ///   ipfs config controls configuration variables. It works
        ///   much like 'git config'. The configuration values are stored in a config
        ///   file inside your IPFS repository.
        /// </summary>
        /// <param name="key">The key of the config entry (e.g. "Addresses.API")</param>
        /// <param name="value">The value to set the config entry to</param>
        /// <param name="bool">Set a boolean value</param>
        /// <returns></returns>
        public async Task<HttpContent> ConfigCommand(string key, string value = null, bool @bool = false)
        {
            var args = new Dictionary<string, string>();

            if (@bool)
            {
                args.Add("bool", "true");
            }

            return await ExecuteAsync("config", ToEnumerable(key, value), null);
        }

        /// <summary>
        /// Download IPFS objects
        /// 
        /// Retrieves the object named by <ipfs-path> and stores the data to disk.
        /// 
        /// By default, the output will be stored at./<ipfs-path>, but an alternate path
        /// 
        /// can be specified with '--output=<path>' or '-o=<path>'.
        /// 
        /// To output a TAR archive instead of unpacked files, use '--archive' or '-a'.
        /// 
        /// To compress the output with GZIP compression, use '--compress' or '-C'. You
        /// may also specify the level of compression by specifying '-l=<1-9>'.
        /// </summary>
        /// <param name="ipfsPath">The path to the IPFS object(s) to be outputted</param>
        /// <param name="output">The path where output should be stored</param>
        /// <param name="archive">Output a TAR archive</param>
        /// <param name="compress">Compress the output with GZIP compression</param>
        /// <param name="compressionLevel">The level of compression (1-9)</param>
        /// <returns></returns>
        public async Task<HttpContent> Get(string ipfsPath, string output = null, bool archive = false, bool compress = false, int? compressionLevel = null)
        {
            var flags = new Dictionary<string, string>();

            if (output != null)
            {
                flags.Add("output", output);
            }

            if (archive)
            {
                flags.Add("archive", "true");
            }

            if (compress)
            {
                flags.Add("compress", "true");
            }

            if (compressionLevel != null)
            {
                flags.Add("compressionLevel", compressionLevel.Value.ToString());
            }

            return await ExecuteAsync("get", ToEnumerable(ipfsPath), flags);
        }

        /// <summary>
        /// Show IPFS Node ID info
        /// 
        /// Prints out information about the specified peer,
        /// if no peer is specified, prints out local peers info.
        /// 
        /// ipfs id supports the format option for output with the following keys:
        /// <id> : the peers id
        /// <aver>: agent version
        /// <pver>: protocol version
        /// <pubkey>: public key
        /// </summary>
        /// <param name="peerId">peer.ID of node to look up</param>
        /// <param name="format">optional output format</param>
        /// <returns></returns>
        public async Task<HttpContent> Id(string peerId, string format = null)
        {
            var flags = new Dictionary<string, string>();

            if (format != null)
            {
                flags.Add("format", format);
            }

            return await ExecuteAsync("id", ToEnumerable(peerId), flags);
        }

        /// <summary>
        /// List links from an object.
        /// 
        ///  Retrieves the object named by <ipfs-path> and displays the links
        ///  it contains, with the following format:
        ///  
        /// <link base58 hash> <link size in bytes> <link name>
        /// </summary>
        /// <param name="path">The path to the IPFS object(s) to list links from</param>
        /// <returns></returns>
        public async Task<HttpContent> Ls(string path)
        {
            return await ExecuteAsync("ls", ToEnumerable(path), null);
        }

        /// <summary>
        /// Mounts IPFS to the filesystem (read-only)
        /// 
        /// Mount ipfs at a read-only mountpoint on the OS (default: /ipfs and /ipns).
        /// All ipfs objects will be accessible under that directory.Note that the
        /// root will not be listable, as it is virtual. Access known paths directly.
        /// 
        /// You may have to create /ipfs and /ipfs before using 'ipfs mount'
        /// </summary>
        /// <param name="f">The path where IPFS should be mounted</param>
        /// <param name="n">The path where IPNS should be mounted</param>
        /// <returns></returns>
        public async Task<HttpContent> Mount(string f = null, string n = null)
        {
            var flags = new Dictionary<string, string>();

            if (f != null)
            {
                flags.Add("f", f);
            }

            if (n != null)
            {
                flags.Add("n", n);
            }

            return await ExecuteAsync("mount", null, null);
        }

        /// <summary>
        /// send echo request packets to IPFS hosts
        /// 
        /// ipfs ping is a tool to test sending data to other nodes. It finds nodes
        /// via the routing system, send pings, wait for pongs, and print out round-
        /// trip latency information.
        /// </summary>
        /// <param name="peerId">ID of peer to be pinged</param>
        /// <param name="count">number of ping messages to send</param>
        /// <returns></returns>
        public async Task<HttpContent> Ping(string peerId, int? count = null)
        {
            return await ExecuteAsync("ping", ToEnumerable(peerId), null);
        }

        /// <summary>
        /// Lists links (references) from an object
        /// Retrieves the object named by <ipfs-path> and displays the link
        /// hashes it contains, with the following format:
        ///  <link base58 hash>
        /// </summary>
        /// <param name="ipfsPath">Path to the object(s) to list refs from</param>
        /// <param name="format">Emit edges with given format. tokens: <src> <dst> <linkname></param>
        /// <param name="edges">Emit edge format: `<from> -> <to>`</param>
        /// <param name="unique">Omit duplicate refs from output</param>
        /// <param name="recursive">Recursively list links of child nodes</param>
        /// <returns></returns>
        public async Task<HttpContent> RefsCommand(string ipfsPath, string format = null, bool edges = false, bool unique = false, bool recursive = false)
        {
            var flags = new Dictionary<string, string>();

            if (format != null)
            {
                flags.Add("format", format);
            }

            if (edges)
            {
                flags.Add("edges", "true");
            }

            if (unique)
            {
                flags.Add("unique", "true");
            }

            if (recursive)
            {
                flags.Add("recursive", "true");
            }

            return await ExecuteAsync("refs", ToEnumerable(ipfsPath), null);
        }

        /// <summary>
        /// An introduction to IPFS
        /// 
        /// This is a tour that takes you through various IPFS concepts,
        /// features, and tools to make sure you get up to speed with
        /// IPFS very quickly
        /// </summary>
        /// <param name="id">The id of the topic you would like to tour</param>
        /// <returns></returns>
        public async Task<HttpContent> TourCommand(string id)
        {
            return await ExecuteAsync("tour", ToEnumerable(id), null);
        }

        /// <summary>
        /// Downloads and installs updates for IPFS
        /// 
        /// ipfs update is a utility command used to check for updates and apply them.
        /// </summary>
        /// <returns></returns>
        public async Task<HttpContent> UpdateCommand()
        {
            return await ExecuteAsync("update", null, null);
        }

        public async Task<HttpContent> Version(bool number = false)
        {
            return await ExecuteAsync("version", null, null);
        }
    }
}
