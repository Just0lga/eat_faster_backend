using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Business : BaseEntity
    {
        public string BusinessName { get; set; }
        public string BusinessDescription { get; set; }
        public string BusinessAddress {  get; set; }
        public string BusinessImageURL { get; set; }
        public string BusinessWorkingHours {  get; set; }
    }
}
