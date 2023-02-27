using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEPortal.Data.Models
{
    public class AuthCode : BaseEntity
    {
        public string CodeNumber { get; set; } = string.Empty;
        public DateTime ExpiredDate { get; set; }
    }
}
