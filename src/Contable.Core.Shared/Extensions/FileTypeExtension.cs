using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Contable.Extensions
{
    public class FileTypeExtension
    {
        private static readonly byte[] PDF = { 37, 80, 68, 70, 45 };
        private static readonly byte[] XLS_DOC = { 208, 207, 17, 224, 161, 177, 26, 225 };
        private static readonly byte[] XLSX_DOCX = { 80, 75, 3, 4 };
        private static readonly byte[] JPG_JPEG = { 255, 216, 255, 238 };
        private static readonly byte[] JPG_JPEG_2 = { 255, 216, 255, 224 };
        private static readonly byte[] JPG_JPEG_3 = { 255, 216, 255, 225 };
        private static readonly byte[] PNG = { 137, 80, 78, 71, 13, 10 };
        private static readonly byte[] MP3 = { 255, 251, 48 };
        private static readonly byte[] MP4_1 = { 116, 121, 112, 105, 115, 111, 109 };
        private static readonly byte[] MP4_2 = { 116, 121, 112 };
        private static readonly byte[] WAV = { 82, 73, 70 };

        public static bool IfValidFileType(byte[] file)
        {
            if (file.Take(5).SequenceEqual(PDF))
                return true;
            if (file.Take(8).SequenceEqual(XLS_DOC))
                return true;
            if (file.Take(4).SequenceEqual(XLSX_DOCX))
                return true;
            if(file.Take(4).SequenceEqual(JPG_JPEG))
                return true;
            if(file.Take(4).SequenceEqual(JPG_JPEG_2))
                return true;
            if (file.Take(4).SequenceEqual(JPG_JPEG_3))
                return true;
            if (file.Take(6).SequenceEqual(PNG))
                return true;
            return false;
        }
        //5-11
        public static bool IfValidStreamFileType(byte[] file)
        {
            if (file.Take(3).SequenceEqual(MP3))
                return true;
            if (file.Take(12).Skip(5).SequenceEqual(MP4_1))
                return true;
            if (file.Take(8).Skip(5).SequenceEqual(MP4_2))
                return true;
            if (file.Take(3).SequenceEqual(WAV))
                return true;
            return false;
        }
    }
}

