using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.MSExchange.Core {
    public class EmailFileAttachment {
        public EmailFileAttachment(string name, string file) {
            Name = name;
            File = file;
            isBinaryFile = false;
        }

        public EmailFileAttachment(string name, byte[] file)
        {
            Name = name;
            ByteFile = file;
            isBinaryFile = true;
        }

        public string Name { get; set; }
        public string File { get; set; }
        public byte[] ByteFile { get; set; }
        public bool isBinaryFile { get; set; }
    }
}
