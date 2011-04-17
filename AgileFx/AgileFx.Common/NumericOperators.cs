using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileFx
{
    //This is a little messy, but that's all I can think of right now.
    //TODO: Fixme.
    public class NumericOperators<T>
        where T : struct
    {
        public static T Add(T left, T right)
        {
            if (typeof(T) == typeof(byte))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((byte)oLeft + (byte)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(sbyte))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((sbyte)oLeft + (sbyte)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(short))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((short)oLeft + (short)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(ushort))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((ushort)oLeft + (ushort)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(int))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((int)oLeft + (int)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(uint))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((uint)oLeft + (uint)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(long))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((long)oLeft + (long)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(ulong))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((ulong)oLeft + (ulong)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(float))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((float)oLeft + (float)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(double))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((double)oLeft + (double)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(decimal))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((decimal)oLeft + (decimal)oRight);
                return (T)result;
            }
            throw new Exception("Operator cannot handle the operation Add on type " + typeof(T).Name + ".");
        }

        public static T Subtract(T left, T right)
        {
            if (typeof(T) == typeof(byte))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((byte)oLeft - (byte)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(sbyte))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((sbyte)oLeft - (sbyte)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(short))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((short)oLeft - (short)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(ushort))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((ushort)oLeft - (ushort)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(int))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((int)oLeft - (int)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(uint))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((uint)oLeft - (uint)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(long))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((long)oLeft - (long)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(ulong))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((ulong)oLeft - (ulong)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(float))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((float)oLeft - (float)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(double))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((double)oLeft - (double)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(decimal))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((decimal)oLeft - (decimal)oRight);
                return (T)result;
            }
            throw new Exception("Cannot handle the operation Subtract on type " + typeof(T).Name + ".");
        }

        public static T Multiply(T left, T right)
        {
            if (typeof(T) == typeof(byte))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((byte)oLeft * (byte)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(sbyte))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((sbyte)oLeft * (sbyte)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(short))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((short)oLeft * (short)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(ushort))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((ushort)oLeft * (ushort)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(int))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((int)oLeft * (int)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(uint))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((uint)oLeft * (uint)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(long))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((long)oLeft * (long)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(ulong))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((ulong)oLeft * (ulong)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(float))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((float)oLeft * (float)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(double))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((double)oLeft * (double)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(decimal))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((decimal)oLeft * (decimal)oRight);
                return (T)result;
            }
            throw new Exception("Cannot handle the operation Multiply on type " + typeof(T).Name + ".");
        }

        public static T Division(T left, T right)
        {
            if (typeof(T) == typeof(byte))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((byte)oLeft / (byte)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(sbyte))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((sbyte)oLeft / (sbyte)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(short))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((short)oLeft / (short)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(ushort))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((ushort)oLeft / (ushort)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(int))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((int)oLeft / (int)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(uint))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((uint)oLeft / (uint)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(long))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((long)oLeft / (long)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(ulong))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((ulong)oLeft / (ulong)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(float))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((float)oLeft / (float)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(double))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((double)oLeft / (double)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(decimal))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((decimal)oLeft / (decimal)oRight);
                return (T)result;
            }
            throw new Exception("Cannot handle the operation Division on type " + typeof(T).Name + ".");
        }

        public static bool LessThan(T left, T right)
        {
            if (typeof(T) == typeof(byte))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (byte)oLeft < (byte)oRight;
            }
            if (typeof(T) == typeof(sbyte))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (sbyte)oLeft < (sbyte)oRight;
            }
            if (typeof(T) == typeof(short))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (short)oLeft < (short)oRight;
            }
            if (typeof(T) == typeof(ushort))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (ushort)oLeft < (ushort)oRight;
            }
            if (typeof(T) == typeof(int))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (int)oLeft < (int)oRight;
            }
            if (typeof(T) == typeof(uint))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (uint)oLeft < (uint)oRight;
            }
            if (typeof(T) == typeof(long))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (long)oLeft < (long)oRight;
            }
            if (typeof(T) == typeof(ulong))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (ulong)oLeft < (ulong)oRight;
            }
            if (typeof(T) == typeof(float))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (float)oLeft < (float)oRight;
            }
            if (typeof(T) == typeof(double))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (double)oLeft < (double)oRight;
            }
            if (typeof(T) == typeof(decimal))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (decimal)oLeft < (decimal)oRight;
            }
            throw new Exception("Cannot handle the operation LessThan on type " + typeof(T).Name + ".");
        }

        public static bool GreaterThan(T left, T right)
        {
            if (typeof(T) == typeof(byte))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (byte)oLeft > (byte)oRight;
            }
            if (typeof(T) == typeof(sbyte))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (sbyte)oLeft > (sbyte)oRight;
            }
            if (typeof(T) == typeof(short))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (short)oLeft > (short)oRight;
            }
            if (typeof(T) == typeof(ushort))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (ushort)oLeft > (ushort)oRight;
            }
            if (typeof(T) == typeof(int))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (int)oLeft > (int)oRight;
            }
            if (typeof(T) == typeof(uint))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (uint)oLeft > (uint)oRight;
            }
            if (typeof(T) == typeof(long))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (long)oLeft > (long)oRight;
            }
            if (typeof(T) == typeof(ulong))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (ulong)oLeft > (ulong)oRight;
            }
            if (typeof(T) == typeof(float))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (float)oLeft > (float)oRight;
            }
            if (typeof(T) == typeof(double))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (double)oLeft > (double)oRight;
            }
            if (typeof(T) == typeof(decimal))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (decimal)oLeft > (decimal)oRight;
            }
            throw new Exception("Cannot handle the operation GreaterThan on type " + typeof(T).Name + ".");
        }

        public static bool LessThanOrEquals(T left, T right)
        {
            if (typeof(T) == typeof(byte))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (byte)oLeft <= (byte)oRight;
            }
            if (typeof(T) == typeof(sbyte))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (sbyte)oLeft <= (sbyte)oRight;
            }
            if (typeof(T) == typeof(short))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (short)oLeft <= (short)oRight;
            }
            if (typeof(T) == typeof(ushort))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (ushort)oLeft <= (ushort)oRight;
            }
            if (typeof(T) == typeof(int))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (int)oLeft <= (int)oRight;
            }
            if (typeof(T) == typeof(uint))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (uint)oLeft <= (uint)oRight;
            }
            if (typeof(T) == typeof(long))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (long)oLeft <= (long)oRight;
            }
            if (typeof(T) == typeof(ulong))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (ulong)oLeft <= (ulong)oRight;
            }
            if (typeof(T) == typeof(float))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (float)oLeft <= (float)oRight;
            }
            if (typeof(T) == typeof(double))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (double)oLeft <= (double)oRight;
            }
            if (typeof(T) == typeof(decimal))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (decimal)oLeft <= (decimal)oRight;
            }
            throw new Exception("Cannot handle the operation LessThanOrEquals on type " + typeof(T).Name + ".");
        }

        public static bool GreaterThanOrEquals(T left, T right)
        {
            if (typeof(T) == typeof(byte))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (byte)oLeft >= (byte)oRight;
            }
            if (typeof(T) == typeof(sbyte))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (sbyte)oLeft >= (sbyte)oRight;
            }
            if (typeof(T) == typeof(short))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (short)oLeft >= (short)oRight;
            }
            if (typeof(T) == typeof(ushort))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (ushort)oLeft >= (ushort)oRight;
            }
            if (typeof(T) == typeof(int))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (int)oLeft >= (int)oRight;
            }
            if (typeof(T) == typeof(uint))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (uint)oLeft >= (uint)oRight;
            }
            if (typeof(T) == typeof(long))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (long)oLeft >= (long)oRight;
            }
            if (typeof(T) == typeof(ulong))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (ulong)oLeft >= (ulong)oRight;
            }
            if (typeof(T) == typeof(float))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (float)oLeft >= (float)oRight;
            }
            if (typeof(T) == typeof(double))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (double)oLeft >= (double)oRight;
            }
            if (typeof(T) == typeof(decimal))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                return (decimal)oLeft >= (decimal)oRight;
            }
            throw new Exception("Cannot handle the operation GreaterThanOrEquals on type " + typeof(T).Name + ".");
        }

        public static T Modulus(T left, T right)
        {
            if (typeof(T) == typeof(byte))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((byte)oLeft % (byte)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(sbyte))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((sbyte)oLeft % (sbyte)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(short))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((short)oLeft % (short)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(ushort))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((ushort)oLeft % (ushort)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(int))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((int)oLeft % (int)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(uint))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((uint)oLeft % (uint)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(long))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((long)oLeft % (long)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(ulong))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((ulong)oLeft % (ulong)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(float))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((float)oLeft % (float)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(double))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((double)oLeft % (double)oRight);
                return (T)result;
            }
            if (typeof(T) == typeof(decimal))
            {
                var oLeft = (object)left;
                var oRight = (object)right;
                var result = (object)((decimal)oLeft % (decimal)oRight);
                return (T)result;
            }
            throw new Exception("Cannot handle the operation Modulus on type " + typeof(T).Name + ".");
        }

        public static T BitwiseComplement(T value)
        {
            if (typeof(T) == typeof(byte))
            {
                var oLeft = (object)value; ;
                var result = (object)~((byte)oLeft);
                return (T)result;
            }
            if (typeof(T) == typeof(sbyte))
            {
                var oLeft = (object)value;
                var result = (object)~((sbyte)oLeft);
                return (T)result;
            }
            if (typeof(T) == typeof(short))
            {
                var oLeft = (object)value;
                var result = (object)~((short)oLeft);
                return (T)result;
            }
            if (typeof(T) == typeof(ushort))
            {
                var oLeft = (object)value;
                var result = (object)~((ushort)oLeft);
                return (T)result;
            }
            if (typeof(T) == typeof(int))
            {
                var oLeft = (object)value;
                var result = (object)~((int)oLeft);
                return (T)result;
            }
            if (typeof(T) == typeof(uint))
            {
                var oLeft = (object)value;
                var result = (object)~((uint)oLeft);
                return (T)result;
            }
            if (typeof(T) == typeof(long))
            {
                var oLeft = (object)value;
                var result = (object)~((long)oLeft);
                return (T)result;
            }
            if (typeof(T) == typeof(ulong))
            {
                var oLeft = (object)value;
                var result = (object)~((ulong)oLeft);
                return (T)result;
            }
            throw new Exception("Cannot handle the operation Bitwise Complement on type " + typeof(T).Name + ".");
        }
    }
}
