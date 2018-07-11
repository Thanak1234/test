using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject
{
    public class FileUploadInfo
    {
        private string _serial;
        public string Identifier { get; set; }

        public string activityName { get; set; }
        public byte[] Stream { get; set; }
        
        public String fileName { get; set; }
        public String ext { get; set; }
        public DateTime createdDate { get; set; } = DateTime.Now;

        public string serial {
            get {
                if(string.IsNullOrEmpty(_serial))
                {
                    _serial = string.Format("{0}_{1}_{2:N}", Identifier, fileName, Guid.NewGuid());
                }

                return _serial;
            }

            set
            {
                _serial = value;
            }
        }




    }
}
