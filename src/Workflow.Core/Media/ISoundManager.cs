using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Media {
    public interface ISoundManager {
        byte[] GetKhmerSound(string folio);
        byte[] GetEnglishSound(string empId);
    }
}
