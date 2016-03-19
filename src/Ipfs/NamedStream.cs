using System;
using System.IO;

namespace Ipfs
{
    public class NamedStream : IEquatable<NamedStream>
    {
        /// <summary>
        /// A file stream & file name pair.
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        /// <param name="fileStream">Stream to the file</param>
        public NamedStream(string fileName, Stream fileStream)
        {
            FileName = fileName;
            FileStream = fileStream;
        }

        /// <summary>
        /// The name of the file
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// A stream to the file
        /// </summary>
        public Stream FileStream { get; private set; }

        public bool Equals(NamedStream other)
        {
            return String.Equals(other.FileName, FileName)
                && Equals(other.FileStream, FileStream);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this)) return true;

            NamedStream other = obj as NamedStream;

            if (other == null) return false;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return 3 * FileName.GetHashCode()
                 + 5 * FileStream.GetHashCode();
        }

        public override string ToString()
        {
            return FileName;
        }
    }
}
