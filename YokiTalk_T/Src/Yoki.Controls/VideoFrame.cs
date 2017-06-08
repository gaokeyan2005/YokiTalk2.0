using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Yoki.Controls
{
    public class VideoFrame
    {
        public long Timestamp { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Size { get; set; }
        public byte[] Data { get; set; }

        public VideoFrame(IntPtr data, int width, int heidht, int size, long time)
        {
            Size = size;
            Width = width;
            Height = heidht;
            Timestamp = time;
            Data = new byte[size];
            Marshal.Copy(data, Data, 0, Data.Length);
        }

        public Stream GetBmpStream()
        {
            using (Bitmap resultBitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb))
            {
                MemoryStream curImageStream = new MemoryStream();

                resultBitmap.Save(curImageStream, System.Drawing.Imaging.ImageFormat.Bmp);

                byte[] tempData = new byte[4];

                //bmp format: https://en.wikipedia.org/wiki/BMP_file_format
                //读取数据开始位置，写入字节流
                curImageStream.Position = 10;

                curImageStream.Read(tempData, 0, 4);

                var dataOffset = BitConverter.ToInt32(tempData, 0);
                curImageStream.Position = dataOffset;
                curImageStream.Write(Data, 0, (int)Size);
                curImageStream.Flush();
                return curImageStream;
            }
        }

    }


    public class VideoEventAgrs : EventArgs
    {
        public VideoFrame Frame { get; set; }

        public VideoEventAgrs(VideoFrame frame)
        {
            Frame = frame;
        }
    }
}
