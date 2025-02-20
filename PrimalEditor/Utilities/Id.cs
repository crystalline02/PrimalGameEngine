using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimalEditor.Utilities
{
    internal static class Id
    {
        public static int INVALID_ID = -1;
        public static bool IsValid(int id) => id != INVALID_ID;

    }
}
