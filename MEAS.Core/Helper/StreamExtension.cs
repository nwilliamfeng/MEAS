using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MEAS
{
    public static class StreamExtension
    {
        /// <summary>
        /// 将流转换为bytes[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Stream ToStream(this byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        public static Stream ToStream(this FileInfo file)
        {
            // 打开文件
            FileStream fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[]
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        public static byte[] ToBytes(this FileInfo file,FileMode mode= FileMode.Open,FileAccess access=FileAccess.Read ,FileShare share=FileShare.Read)
        {
            // 打开文件
            using (var fileStream = new FileStream(file.FullName, mode, access , share))
            {
                // 读取文件的 byte[]
                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);
                return bytes;
            }           
        }

        public static string GetMime(this FileInfo fileInfo)
        {
            return System.Web.MimeMapping.GetMimeMapping(fileInfo.FullName);
        }
    }
}
