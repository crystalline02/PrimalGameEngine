using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PrimalEditor.Utilities
{
    internal static class MathUtils
    {
        public static readonly double Epsilon = 1e-5;

        public static bool IsTheSame(this float x, float y) => Math.Abs(x - y) <= Epsilon;
        public static bool IsTheSame(this float? x, float? y)
        {
            if(x == null && y == null) return true;
            else if((x == null && y != null) || (x != null && y == null)) return false;
            else return IsTheSame(x!.Value, y!.Value);
        }

        public static bool IsTheSame(this float x, double y) => Math.Abs(x - y) <= Epsilon;
        public static bool IsTheSame(this float? x, double? y)
        {
            if (x == null && y == null) return true;
            else if (x == null && y != null) return false;
            else if (x != null && y == null) return false;
            else return IsTheSame(x!.Value, y!.Value);
        }

        public static bool IsTheSame(this double x, double y) => Math.Abs(x - y) <= Epsilon;
        public static bool IsTheSame(this double? x, double? y)
        {
            if (x == null && y == null) return true;
            else if (x == null && y != null) return false;
            else if (x != null && y == null) return false;
            else return IsTheSame(x!.Value, y!.Value);
        }
        public static bool IsTheSame(this double x, float y) => Math.Abs(x - y) <= Epsilon;
        public static bool IsTheSame(this double? x, float? y)
        {
            if (x == null && y == null) return true;
            else if (x == null && y != null) return false;
            else if (x != null && y == null) return false;
            else return IsTheSame(x!.Value, y!.Value);
        }
        public static bool IsTheSame(this Vector3 x, Vector3 y) => IsTheSame(x.X, y.X) && IsTheSame(x.Y, y.Y) && IsTheSame(x.Z, y.Z);
        public static bool IsTheSame(this Vector3? x, Vector3? y)
        {
            if (x == null && y == null) return true;
            else if (x == null && y != null) return false;
            else if (x != null && y == null) return false;
            else return IsTheSame(x!.Value, y!.Value);
        }
    }
}
