using System;

namespace YggdrAshill.Samples
{
    public sealed class LocalFilePath
    {
        private readonly string filePath;

        public LocalFilePath(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException($"{nameof(filePath)} is empty.");
            }

            this.filePath = $"file://{filePath}";
        }

        public override string ToString()
        {
            return filePath;
        }

        public static explicit operator string(LocalFilePath filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            return filePath.filePath;
        }
    }
}
