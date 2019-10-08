using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Oicrute
{
    class ComparePicture
    {
        const byte VARIATION = 5;

        //Сделать бы вычисляемым
        const int lenghtStream = 2;

        private Bitmap face { get; set; }

        private byte[] faceArrByte { get; set; }

        public ComparePicture(Bitmap face)
        {
            this.face = face;
            this.faceArrByte = TranslatePictureInArr(face);
        }

        private byte[] TranslatePictureInArr(Image image)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(image, typeof(byte[]));
        }

        public bool CompareImage(Bitmap faceCompare)
        {

            byte[] faceArrByteCompare = TranslatePictureInArr(faceCompare);
            if (faceArrByte.Length == faceArrByte.Length)
            {
                Task<ulong>[] tasks = new Task<ulong>[lenghtStream];
                int start = 0;
                int end = faceArrByte.Length / lenghtStream;
                for (int i = 0; i < lenghtStream; i++)
                {
                    byte[] buffer = faceArrByte.Skip(start).Take(end).ToArray();
                    tasks[i] = Task<ulong>.Factory.StartNew(() => CompareByte(faceArrByte.Skip(start).Take(end).ToArray(), faceArrByteCompare.Skip(start).Take(end).ToArray()));
                }
                Task.WaitAll(tasks);
                foreach (var result in tasks)
                {
                    if (result.Result > VARIATION)
                        return false;
                }
                return true;
            }
            throw new Exception("Разные размеры картинок");
        }

        private ulong CompareByte(byte[] compareFrom, byte[] compareTo)
        {
            ulong missByte = 0;
            for (int index = 0; index < compareTo.Length; index++)
            {
                if (Math.Abs(compareTo[index] - compareFrom[index]) > VARIATION)
                    missByte++;
            }
            return missByte;
        }
    }
}
