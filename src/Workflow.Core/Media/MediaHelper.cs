using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Media
{

    public class MediaHelper
    {

        /// <summary>
        /// To Combine the mp3 files to stream 
        /// </summary>
        /// <param name="files">The list of source file name</param>
        /// <param name="result">The Stream object</param>
        public static void Combine(string[] files, Stream result)
        {
            foreach (string file in files)
            {
                Mp3FileReader reader = new Mp3FileReader(file);
                if ((result.Position == 0) && (reader.Id3v2Tag != null))
                {
                    result.Write(reader.Id3v2Tag.RawData, 0, reader.Id3v2Tag.RawData.Length);
                }
                Mp3Frame frame;
                while ((frame = reader.ReadNextFrame()) != null)
                {
                    result.Write(frame.RawData, 0, frame.RawData.Length);
                }
            }
        }

        /// <summary>
        /// To Combine the wave files to stream
        /// </summary>
        /// <param name="files">The list of source file name</param>
        /// <param name="stream">The Stream object</param>
        public static void CombineToWave(string[] files, Stream stream)
        {
            byte[] buffer = new byte[1024];
            WaveFileWriter writer = null;
            try
            {
                int count = files.Count();
                for (int i = 0; i < count; i++)
                {
                    var file = files[i];
                    using (WaveFileReader reader = new WaveFileReader(file))
                    {
                        if (writer == null)
                            writer = new WaveFileWriter(stream, reader.WaveFormat);
                        else
                            if (!reader.WaveFormat.Equals(writer.WaveFormat)) continue;

                        int read;
                        while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            writer.Write(buffer, 0, read);
                        }
                    }
                }
            } finally
            {
                if (writer != null)
                {
                    //writer.Dispose();
                }
            }            
        }
    }
}
