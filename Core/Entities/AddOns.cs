using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AddOns : BaseEntity
    {
        public int BusinessId { get; set; }
        public int ProductId { get; set; }
        public string AddOnsName { get; set; }
        public string AddOnsDescription { get; set; }
    }
}
