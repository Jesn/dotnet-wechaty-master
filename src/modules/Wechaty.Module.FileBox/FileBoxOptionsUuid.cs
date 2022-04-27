using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wechaty.Module.Filebox
{
    public class FileBoxOptionsUuid : FileBoxOptions
    {
        public override FileBoxType Type => FileBoxType.Uuid;
        public string UUID { get; set; }

    }
}
