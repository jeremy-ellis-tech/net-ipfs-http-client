using Ipfs.Commands;
using Ipfs.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ipfs
{
    public class IpfsClient : IDisposable
    {
        private static string DefaultAddress = "http://127.0.0.1:5001";
        private static HttpClient DefaultHttpClient = new HttpClient();

        private readonly string _address;
        private readonly HttpClient _httpClient;

        public IpfsClient() : this(DefaultAddress, DefaultHttpClient)
        {
        }

        public IpfsClient(string address) : this(address, DefaultHttpClient)
        {
        }

        public IpfsClient(string address, HttpClient httpClient)
        {
            _address = address;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Root commands.
        /// Gives access to top level commands like 'ipfs add' and 'ipfs cat'
        /// Also availible at the client level with aliases defined below
        /// </summary>
        private IpfsRoot _root;
        public IpfsRoot Root
        {
            get
            {
                EnsureNotDisposed();

                if (_root == null)
                {
                    _root = new IpfsRoot(_address, _httpClient);
                }

                return _root;
            }
        }

        /// <summary>
        /// Block subcommands
        /// </summary>
        private IpfsBlock _block;
        public IpfsBlock Block
        {
            get
            {
                EnsureNotDisposed();

                if (_block == null)
                {
                    _block = new IpfsBlock(_address, _httpClient);
                }

                return _block;
            }
        }

        /// <summary>
        /// Bootstrap subcommands
        /// </summary>
        private IpfsBootstrap _bootstrap;
        public IpfsBootstrap Bootstrap
        {
            get
            {
                EnsureNotDisposed();

                if (_bootstrap == null)
                {
                    _bootstrap = new IpfsBootstrap(_address, _httpClient);
                }

                return _bootstrap;
            }
        }

        /// <summary>
        /// Config subcommands
        /// </summary>
        private IpfsConfig _config;
        public IpfsConfig Config
        {
            get
            {
                EnsureNotDisposed();

                if (_config == null)
                {
                    _config = new IpfsConfig(_address, _httpClient);
                }

                return _config;
            }
        }

        /// <summary>
        /// Dht subcommands
        /// </summary>
        private IpfsDht _dht;
        public IpfsDht Dht
        {
            get
            {
                EnsureNotDisposed();

                if (_dht == null)
                {
                    _dht = new IpfsDht(_address, _httpClient);
                }

                return _dht;
            }
        }

        /// <summary>
        /// Diag subcommands
        /// </summary>
        private IpfsDiag _diag;
        public IpfsDiag Diag
        {
            get
            {
                EnsureNotDisposed();

                if (_diag == null)
                {
                    _diag = new IpfsDiag(_address, _httpClient);
                }

                return _diag;
            }
        }

        /// <summary>
        /// Log subcommands
        /// </summary>
        private IpfsLog _log;
        public IpfsLog Log
        {
            get
            {
                EnsureNotDisposed();

                if (_log == null)
                {
                    _log = new IpfsLog(_address, _httpClient);
                }

                return _log;
            }
        }

        /// <summary>
        /// Name subcommands
        /// </summary>
        private IpfsName _name;
        public IpfsName Name
        {
            get
            {
                EnsureNotDisposed();

                if (_name == null)
                {
                    _name = new IpfsName(_address, _httpClient);
                }

                return _name;
            }
        }

        /// <summary>
        /// Object subcommands
        /// </summary>
        private IpfsObject _object;
        public IpfsObject Object
        {
            get
            {
                EnsureNotDisposed();

                if (_object == null)
                {
                    _object = new IpfsObject(_address, _httpClient);
                }

                return _object;
            }
        }

        /// <summary>
        /// Pin subcommands
        /// </summary>
        private IpfsPin _pin;
        public IpfsPin Pin
        {
            get
            {
                EnsureNotDisposed();

                if (_pin == null)
                {
                    _pin = new IpfsPin(_address, _httpClient);
                }

                return _pin;
            }
        }

        /// <summary>
        /// Refs subcommands
        /// </summary>
        private IpfsRefs _refs;
        public IpfsRefs Refs
        {
            get
            {
                EnsureNotDisposed();

                if (_refs == null)
                {
                    _refs = new IpfsRefs(_address, _httpClient);
                }

                return _refs;
            }
        }

        /// <summary>
        /// Repo subcommands
        /// </summary>
        private IpfsRepo _repo;
        public IpfsRepo Repo
        {
            get
            {
                EnsureNotDisposed();

                if (_repo == null)
                {
                    _repo = new IpfsRepo(_address, _httpClient);
                }

                return _repo;
            }
        }

        /// <summary>
        /// Swarm subcommands
        /// </summary>
        private IpfsSwarm _swarm;
        public IpfsSwarm Swarm
        {
            get
            {
                EnsureNotDisposed();

                if (_swarm == null)
                {
                    _swarm = new IpfsSwarm(_address, _httpClient);
                }

                return _swarm;
            }
        }

        /// <summary>
        /// Tour subcommands
        /// </summary>
        private IpfsTour _tour;
        public IpfsTour Tour
        {
            get
            {
                EnsureNotDisposed();

                if (_tour == null)
                {
                    _tour = new IpfsTour(_address, _httpClient);
                }

                return _tour;
            }
        }

        /// <summary>
        /// Update subcommands
        /// </summary>
        private IpfsUpdate _update;
        public IpfsUpdate Update
        {
            get
            {
                EnsureNotDisposed();

                if (_update == null)
                {
                    _update = new IpfsUpdate(_address, _httpClient);
                }

                return _update;
            }
        }

        #region Root command aliases
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
        public async Task<string> Add(string path, bool recursive = false, bool quiet = false, bool progress = false, bool wrapWithDirectory = false, bool trickle = false)
        {
            return await Root.Add(path, recursive, quiet, progress, wrapWithDirectory, trickle);
        }

        /// <summary>
        /// Show IPFS object data
        /// Retrieves the object named by <ipfs-path> and outputs the data
        /// it contains.
        /// </summary>
        /// <param name="ipfsPath">The path to the IPFS object(s) to be outputted</param>
        /// <returns></returns>
        public async Task<string> Cat(string ipfsPath)
        {
            return await Root.Cat(ipfsPath);
        }

        /// <summary>
        /// List all available commands.
        /// Lists all available commands (and subcommands) and exits.
        /// </summary>
        /// <returns></returns>
        public async Task<string> Commands()
        {
            return await Root.Commands();
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
        public async Task<string> ConfigCommand(string key, string value = null, bool @bool = false)
        {
            return await Root.ConfigCommand(key, value, @bool);
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
        public async Task<string> Get(string ipfsPath, string output = null, bool archive = false, bool compress = false, int? compressionLevel = null)
        {
            return await Root.Get(ipfsPath, output, archive, compress, compressionLevel);
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
        public async Task<string> Id(string peerId, string format = null)
        {
            return await Root.Id(peerId, format);
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
        public async Task<string> Ls(string path)
        {
            return await Root.Ls(path);
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
        public async Task<string> Mount(string f = null, string n = null)
        {
            return await Root.Mount(f, n);
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
        public async Task<string> Ping(string peerId, int? count = null)
        {
            return await Root.Ping(peerId, count);
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
        public async Task<string> RefsCommand(string ipfsPath, string format = null, bool edges = false, bool unique = false, bool recursive = false)
        {
            return await Root.RefsCommand(ipfsPath, format, edges, unique, recursive);
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
        public async Task<string> TourCommand(string id)
        {
            return await Root.TourCommand(id);
        }

        /// <summary>
        /// Downloads and installs updates for IPFS
        /// 
        /// ipfs update is a utility command used to check for updates and apply them.
        /// </summary>
        /// <returns></returns>
        public async Task<string> UpdateCommand()
        {
            return await Root.UpdateCommand();
        }

        public async Task<string> Version(bool number = false)
        {
            return await Root.Version(number);
        }
        #endregion Root command aliases

        private void EnsureNotDisposed()
        {
            if(_disposed)
            {
                throw new ObjectDisposedException("IpfsClient");
            }
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_httpClient != null) _httpClient.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
