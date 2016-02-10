using Ipfs.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ipfs
{
    public class IpfsClient : IpfsCommand
    {
        public IpfsClient()
        {
        }

        public IpfsClient(string address) : base(address)
        {
        }

        public IpfsClient(string address, HttpClient httpClient) : base(address, httpClient)
        {
        }

        private Uri _baseUri;
        protected override Uri CommandUri
        {
            get
            {
                if (_baseUri == null)
                {
                    UriBuilder uriBuilder = new UriBuilder(_address);
                    uriBuilder.Path += "api/v0/";
                    _baseUri = uriBuilder.Uri;
                }

                return _baseUri;
            }
        }

        public async Task<byte[]> Add(string path, bool recursive = false, bool quiet = false, bool progress = false, bool wrapWithDirectory = false, bool trickle = false)
        {
            var flags = new Dictionary<string, string>();
            
            if(recursive)
            {
                flags.Add("recursive", "true");
            }

            if(quiet)
            {
                flags.Add("quiet", "true");
            }

            return await ExecuteAsync("add", Singleton(path), flags);
        }

        private IpfsBlock _block;
        public IpfsBlock Block
        {
            get
            {
                if (_block == null)
                {
                    _block = new IpfsBlock(_address, _httpClient);
                }

                return _block;
            }
        }

        private IpfsBootstrap _bootstrap;
        public IpfsBootstrap Bootstrap
        {
            get
            {
                if (_bootstrap == null)
                {
                    _bootstrap = new IpfsBootstrap(_address, _httpClient);
                }

                return _bootstrap;
            }
        }

        public async Task<byte[]> Cat(string ipfsPath)
        {
            return await ExecuteAsync("cat", Singleton(ipfsPath));
        }

        public async Task<byte[]> Commands()
        {
            return await ExecuteAsync("commands");
        }

        //public async Task<byte[]> Config(string key, string value = null)
        //{
        //    var args = new Dictionary<string, string>
        //    {
        //        {"key", key }
        //    };

        //    if (value != null)
        //    {
        //        args.Add("value", value);
        //    }

        //    return await GetAsync("config", args);
        //}

        private IpfsConfig _config;
        public IpfsConfig Config
        {
            get
            {
                if (_config == null)
                {
                    _config = new IpfsConfig(_address, _httpClient);
                }

                return _config;
            }
        }

        private IpfsDht _dht;
        public IpfsDht Dht
        {
            get
            {
                if (_dht == null)
                {
                    _dht = new IpfsDht(_address, _httpClient);
                }

                return _dht;
            }
        }

        private IpfsDiag _diag;
        public IpfsDiag Diag
        {
            get
            {
                if (_diag == null)
                {
                    _diag = new IpfsDiag(_address, _httpClient);
                }

                return _diag;
            }
        }

        public async Task<byte[]> Get(string ipfsPath, string output = null, bool archive = false, bool compress = false, int? compressionLevel = null)
        {
            return await ExecuteAsync("get", Singleton(ipfsPath));
        }

        public async Task<byte[]> Id(string peerId, string format = null)
        {
            return await ExecuteAsync("id", Singleton(peerId));
        }

        private IpfsLog _log;
        public IpfsLog Log
        {
            get
            {
                if (_log == null)
                {
                    _log = new IpfsLog(_address, _httpClient);
                }

                return _log;
            }
        }

        public async Task<byte[]> Ls(string path)
        {
            return await ExecuteAsync("ls", Singleton(path));
        }

        public async Task<byte[]> Mount(string f = null, string n = null)
        {
            return await ExecuteAsync("mount");
        }

        private IpfsName _name;
        public IpfsName Name
        {
            get
            {
                if (_name == null)
                {
                    _name = new IpfsName(_address, _httpClient);
                }

                return _name;
            }
        }

        private IpfsObject _object;
        public IpfsObject Object
        {
            get
            {
                if (_object == null)
                {
                    _object = new IpfsObject(_address, _httpClient);
                }

                return _object;
            }
        }

        private IpfsPin _pin;
        public IpfsPin Pin
        {
            get
            {
                if (_pin == null)
                {
                    _pin = new IpfsPin(_address, _httpClient);
                }

                return _pin;
            }
        }

        public async Task<byte[]> Ping(string peerId, int? count = null)
        {
            return await ExecuteAsync("ping", Singleton(peerId));
        }

        //public async Task<byte[]> Refs(string ipfsPath, string format = null, bool edges = false, bool unique = false, bool recursive = false)
        //{
        //    var args = new Dictionary<string, string>
        //    {
        //        { "arg", ipfsPath }
        //    };

        //    return await ExecuteAsync("refs", args);
        //}

        private IpfsRefs _refs;
        public IpfsRefs Refs
        {
            get
            {
                if (_refs == null)
                {
                    _refs = new IpfsRefs(_address, _httpClient);
                }

                return _refs;
            }
        }

        private IpfsSwarm _swarm;
        public IpfsSwarm Swarm
        {
            get
            {
                if (_swarm == null)
                {
                    _swarm = new IpfsSwarm(_address, _httpClient);
                }

                return _swarm;
            }
        }

        private IpfsTour _tour;
        public IpfsTour Tour
        {
            get
            {
                if (_tour == null)
                {
                    _tour = new IpfsTour(_address, _httpClient);
                }

                return _tour;
            }
        }

        private IpfsUpdate _update;
        public IpfsUpdate Update
        {
            get
            {
                if (_update == null)
                {
                    _update = new IpfsUpdate(_address, _httpClient);
                }

                return _update;
            }
        }

        public async Task<byte[]> Version(bool number = false)
        {
            return await ExecuteAsync("version");
        }
    }
}
