using System;
using System.Collections.Generic;

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

        public MultiHash Hash { get; private set; }

        public string Name { get; set; }

        public int? Size { get; set; }

        public int? Type { get; set; }

        public byte[] Data { get; set; }

        public IEnumerable<MerkleNode> Links { get; set; }

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
