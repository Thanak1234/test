using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Media
{
    public class SoundManger: ISoundManager {
        public string BasePath = "";
        public List<string> Sounds = null;

        public SoundManger(string path) {
            BasePath = path;
        }

        #region Implemenations

        public byte[] GetKhmerSound(string empId)
        {
            Sounds = new List<string>();
            if (string.IsNullOrEmpty(empId)) return null;
            MemoryStream mixer = new MemoryStream();
            string[] files = GetFilesByEmpId(empId);
            Sounds.Add(BasePath + "begining.wav");
            Sounds.AddRange(files);
            MediaHelper.CombineToWave(Sounds.ToArray(), mixer);
            return mixer != null && mixer.Length > 0 ? mixer.ToArray() : null;
        }

        public byte[] GetEnglishSound(string empId)
        {
            Sounds = new List<string>();
            if (string.IsNullOrEmpty(empId)) return null;
            MemoryStream mixer = new MemoryStream();
            string[] files = GetCharacters(empId);
            Sounds.Add(BasePath + "ID.wav");
            Sounds.AddRange(files);
            MediaHelper.CombineToWave(Sounds.ToArray(), mixer);
            return mixer != null && mixer.Length > 0 ? mixer.ToArray() : null;
        }

        #endregion

        #region Helper Methods

        protected string[] GetFilesByEmpId(string empId)
        {
            if (empId.Length > 6) return null;
            string[] starts = GetSubs(empId.Substring(0, 3));
            string[] ends = GetSubs(empId.Substring(3));
            var result = new List<string>();
            if (starts.Count() > 0)
                result.AddRange(starts);
            if (ends.Count() > 0)
                result.AddRange(ends);
            if (result.Count > 0)
            {
                return result.ToArray();
            }
            return null;
        }

        protected string[] GetSubs(string sub)
        {
            List<string> files = new List<string>();
            int num = int.Parse(sub);
            char[] ch = sub.ToCharArray();
            if (num > 0 && num < 10)
            {
                files.Add(string.Format("{0}{1}.wav", BasePath, num));
            }
            else if (num >= 10 && num < 100)
            {
                files.Add(string.Format("{0}{1}.wav", BasePath, num));
            }
            else if (num >= 100 && num < 1000)
            {
                files.Add(string.Format("{0}{1}00.wav", BasePath, Math.Abs(num / 100)));
                int integer = (num % 100);
                if (integer != 0)
                {
                    files.Add(string.Format("{0}{1}.wav", BasePath, integer));
                }
            }
            return files.ToArray();
        }

        protected string[] GetCharacters(string employeeNo)
        {
            List<string> files = new List<string>();
            char[] chars = employeeNo.ToUpper().ToCharArray();
            foreach (char ch in chars)
                files.Add(string.Format("{0}{1}.wav", BasePath, ch));
            return files.ToArray();
        }

        #endregion
    }
}
