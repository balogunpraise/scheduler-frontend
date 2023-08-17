using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Authentication
{
    public class RegisterResult
    {
        public bool Successful { get; set; }
        public List<string>? Errors { get; set; }
    }
}
