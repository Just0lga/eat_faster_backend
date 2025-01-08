using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Programmer : BaseEntity
    {
        public int BusinessId { get; set; }
        public string ProgrammerName { get; set; }
        public string ProgrammerEmail { get; set; }
        public string ProgrammerPhone { get; set; }
    }
}
