using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs
{
    public class MerkleNode : IEquatable<MerkleNode>
    {
        public MerkleNode(string multiHash) : this(new MultiHash(multiHash))
        {

        }

        public MerkleNode(MultiHash multiHash)
        {
            Hash = multiHash;
        }

        public MerkleNode(MultiHash multihash, string name, int size, int type, byte[] data)
        {
            Hash = multihash;
            Name = name;
            Size = size;
            Type = type;
            Data = data;
        }

        public MultiHash Hash { get; private set; }

        public string Name { get; private set; }

        public int? Size { get; private set; }

        public int? Type { get; private set; }

        public byte[] Data { get; set; }

        public IEnumerable<MultiHash> Links { get; private set; }

        public bool Equals(MerkleNode other)
        {
            return Equals(other.Hash, Hash);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;

            var other = obj as MerkleNode;

            if(other == null) return false;

            return Equals(other, this);
        }

        public override int GetHashCode()
        {
            return Hash.GetHashCode();
        }

        public override string ToString()
        {
            return Hash.ToString();
        }
    }
}
