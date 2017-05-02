namespace Blaze.Framework.Diagnostics
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// 格式： 消息正文长度+消息正文
    /// 消息长度类型为<see cref="int"/>。
    /// </summary>
    public class ConsoleMessage
    {
        /// <summary>
        /// 消息头的长度。
        /// </summary>
        public const int HeaderSize = 4;

        /// <summary>
        /// 命令名称。
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// 命令内容。
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 获取消息正文的字节数。
        /// </summary>
        public int Size
        {
            get { return Encoding.UTF8.GetByteCount(Command) + Encoding.UTF8.GetByteCount(Content); }
        }

        /// <summary>
        /// 从指定的<see cref="BinaryReader"/>中读取消息。
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <returns>消息</returns>
        public static ConsoleMessage Read(BinaryReader reader)
        {
            reader.ReadInt32();
            var message = new ConsoleMessage
            {
                Command = readString(reader),
                Content = readString(reader),
            };
            return message;
        }

        /// <summary>
        /// 尝试从一个字节数组中反序列化出一个<see cref="ConsoleMessage"/>。
        /// </summary>
        /// <param name="buffer">缓存</param>
        /// <param name="size">可用的字节数量</param>
        /// <param name="message">消息</param>
        /// <param name="offset">起始偏移</param>
        /// <returns>是否反序列化成功</returns>
        public static bool TryDeserialize(byte[] buffer, int offset, int size, out ConsoleMessage message)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            message = null;
            var availableByteCount = size - offset;
            if (availableByteCount < HeaderSize)
                return false;

            using (var stream = new MemoryStream(buffer, offset, size))
            {
                var reader = new BinaryReader(stream);
                var bodySize = reader.ReadInt32();
                if (availableByteCount < HeaderSize + bodySize)
                    return false;
                message = new ConsoleMessage
                {
                    Command = readString(reader),
                    Content = readString(reader),
                };
                return true;
            }
        }

        /// <summary>
        /// 将消息序列化为字节数组。
        /// </summary>
        public byte[] Serialize()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                writer.Write(Size);
                writeString(writer, Command);
                writeString(writer, Content);
                writer.Flush();
                return stream.ToArray();
            }
        }

        private static string readString(BinaryReader reader)
        {
            var size = reader.ReadInt32();
            var bytes = reader.ReadBytes(size);
            return Encoding.UTF8.GetString(bytes);
        }

        private static void writeString(BinaryWriter writer, string text)
        {
            if (text == null)
                text = string.Empty;
            var bytes = Encoding.UTF8.GetBytes(text);
            writer.Write(bytes.Length);
            writer.Write(bytes);
        }
    }
}