using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class BaseItem
    {
        [Ignore]
        public int ObjectId { get; set; }
    }
}
