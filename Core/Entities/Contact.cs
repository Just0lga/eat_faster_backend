using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Contact : BaseEntity
    {
        public int BusinessId { get; set; }
        public string ContactName { get; set; }
        public string ContactDescription { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
    }
}