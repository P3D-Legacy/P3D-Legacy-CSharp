using System.IO;

namespace P3D.Legacy.Core.GameJolt
{
    public class StreamWriterLock : StreamWriter
    {

        private object _lock = new object();
        public StreamWriterLock(Stream stream) : base(stream)
        {
        }

        public override void Write(char value)
        {
            lock (_lock)
            {
                base.Write(value);
            }
        }
    }
}
