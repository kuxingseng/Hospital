namespace Blaze.Framework.Diagnostics
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// ��ʽ�� ��Ϣ���ĳ���+��Ϣ����
    /// ��Ϣ��������Ϊ<see cref="int"/>��
    /// </summary>
    public class ConsoleMessage
    {
        /// <summary>
        /// ��Ϣͷ�ĳ��ȡ�
        /// </summary>
        public const int HeaderSize = 4;

        /// <summary>
        /// �������ơ�
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// �������ݡ�
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// ��ȡ��Ϣ���ĵ��ֽ�����
        /// </summary>
        public int Size
        {
            get { return Encoding.UTF8.GetByteCount(Command) + Encoding.UTF8.GetByteCount(Content); }
        }

        /// <summary>
        /// ��ָ����<see cref="BinaryReader"/>�ж�ȡ��Ϣ��
        /// </summary>
        /// <param name="reader">��ȡ��</param>
        /// <returns>��Ϣ</returns>
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
        /// ���Դ�һ���ֽ������з����л���һ��<see cref="ConsoleMessage"/>��
        /// </summary>
        /// <param name="buffer">����</param>
        /// <param name="size">���õ��ֽ�����</param>
        /// <param name="message">��Ϣ</param>
        /// <param name="offset">��ʼƫ��</param>
        /// <returns>�Ƿ����л��ɹ�</returns>
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
        /// ����Ϣ���л�Ϊ�ֽ����顣
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