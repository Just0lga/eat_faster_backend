using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AddOnsOption : BaseEntity
    {
        public int BusinessId { get; set; }
        public int ProductId { get; set; }
        public int AddOnsId { get; set; }
        public string OptionName { get; set; }
        public double Price { get; set; }
    }
}
