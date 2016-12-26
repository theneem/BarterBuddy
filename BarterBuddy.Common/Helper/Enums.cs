using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarterBuddy.Common.Helper
{
    public class Enums
    {
        public enum ResponseCode
        {
            Warning,
            Error,
            Success
        }

        public enum UserType
        {
            SGEAdmin = 1,
            TenentUser = 2
        }
    }
}
