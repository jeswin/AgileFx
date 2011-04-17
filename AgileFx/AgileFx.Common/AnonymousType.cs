/* AgileFx Version 2.0
 * An open-source framework for rapid development of applications and services using Microsoft.net
 * Developed by: AgileHead
 * Website: www.agilefx.org
 * This component is licensed under the terms of the Apache 2.0 License.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AgileFx
{
    public abstract class AnonymousType
    {
        List<int> initializedFieldIndexes = new List<int>();
        public List<int> InitializedFieldIndexes { get { return initializedFieldIndexes; } }

        public static Type GetGenericType(int numFields)
        {
            switch (numFields)
            {
			
                case 1:
                    return typeof(AnonymousType<>);
			
                case 2:
                    return typeof(AnonymousType<,>);
			
                case 3:
                    return typeof(AnonymousType<,,>);
			
                case 4:
                    return typeof(AnonymousType<,,,>);
			
                case 5:
                    return typeof(AnonymousType<,,,,>);
			
                case 6:
                    return typeof(AnonymousType<,,,,,>);
			
                case 7:
                    return typeof(AnonymousType<,,,,,,>);
			
                case 8:
                    return typeof(AnonymousType<,,,,,,,>);
			
                case 9:
                    return typeof(AnonymousType<,,,,,,,,>);
			
                case 10:
                    return typeof(AnonymousType<,,,,,,,,,>);
			
                case 11:
                    return typeof(AnonymousType<,,,,,,,,,,>);
			
                case 12:
                    return typeof(AnonymousType<,,,,,,,,,,,>);
			
                case 13:
                    return typeof(AnonymousType<,,,,,,,,,,,,>);
			
                case 14:
                    return typeof(AnonymousType<,,,,,,,,,,,,,>);
			
                case 15:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,>);
			
                case 16:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,>);
			
                case 17:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,>);
			
                case 18:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,>);
			
                case 19:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,>);
			
                case 20:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,>);
			
                case 21:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,>);
			
                case 22:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,>);
			
                case 23:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 24:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 25:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 26:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 27:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 28:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 29:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 30:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 31:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 32:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 33:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 34:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 35:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 36:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 37:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 38:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 39:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 40:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 41:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 42:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 43:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 44:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 45:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 46:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 47:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 48:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 49:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
			
                case 50:
                    return typeof(AnonymousType<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
				default:
					throw new NotSupportedException("The number of fields exceed that supported with AnonymousType.");
            }
        }

        public abstract List<object> GetValues();

        public static PropertyInfo GetPropertyByIndex(Type type, int index)
        {
            return type.GetProperty("Field" + index.ToString());
        }
    }
	public class AnonymousType<T0> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0 };
        }
    }
	public class AnonymousType<T0, T1> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1 };
        }
    }
	public class AnonymousType<T0, T1, T2> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
		T32 field32;
        public T32 Field32
        {
            get { return field32; }
            set { field32 = value; InitializedFieldIndexes.Add(32); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31, field32 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
		T32 field32;
        public T32 Field32
        {
            get { return field32; }
            set { field32 = value; InitializedFieldIndexes.Add(32); }
        }
		T33 field33;
        public T33 Field33
        {
            get { return field33; }
            set { field33 = value; InitializedFieldIndexes.Add(33); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31, field32, field33 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
		T32 field32;
        public T32 Field32
        {
            get { return field32; }
            set { field32 = value; InitializedFieldIndexes.Add(32); }
        }
		T33 field33;
        public T33 Field33
        {
            get { return field33; }
            set { field33 = value; InitializedFieldIndexes.Add(33); }
        }
		T34 field34;
        public T34 Field34
        {
            get { return field34; }
            set { field34 = value; InitializedFieldIndexes.Add(34); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31, field32, field33, field34 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
		T32 field32;
        public T32 Field32
        {
            get { return field32; }
            set { field32 = value; InitializedFieldIndexes.Add(32); }
        }
		T33 field33;
        public T33 Field33
        {
            get { return field33; }
            set { field33 = value; InitializedFieldIndexes.Add(33); }
        }
		T34 field34;
        public T34 Field34
        {
            get { return field34; }
            set { field34 = value; InitializedFieldIndexes.Add(34); }
        }
		T35 field35;
        public T35 Field35
        {
            get { return field35; }
            set { field35 = value; InitializedFieldIndexes.Add(35); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31, field32, field33, field34, field35 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
		T32 field32;
        public T32 Field32
        {
            get { return field32; }
            set { field32 = value; InitializedFieldIndexes.Add(32); }
        }
		T33 field33;
        public T33 Field33
        {
            get { return field33; }
            set { field33 = value; InitializedFieldIndexes.Add(33); }
        }
		T34 field34;
        public T34 Field34
        {
            get { return field34; }
            set { field34 = value; InitializedFieldIndexes.Add(34); }
        }
		T35 field35;
        public T35 Field35
        {
            get { return field35; }
            set { field35 = value; InitializedFieldIndexes.Add(35); }
        }
		T36 field36;
        public T36 Field36
        {
            get { return field36; }
            set { field36 = value; InitializedFieldIndexes.Add(36); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31, field32, field33, field34, field35, field36 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
		T32 field32;
        public T32 Field32
        {
            get { return field32; }
            set { field32 = value; InitializedFieldIndexes.Add(32); }
        }
		T33 field33;
        public T33 Field33
        {
            get { return field33; }
            set { field33 = value; InitializedFieldIndexes.Add(33); }
        }
		T34 field34;
        public T34 Field34
        {
            get { return field34; }
            set { field34 = value; InitializedFieldIndexes.Add(34); }
        }
		T35 field35;
        public T35 Field35
        {
            get { return field35; }
            set { field35 = value; InitializedFieldIndexes.Add(35); }
        }
		T36 field36;
        public T36 Field36
        {
            get { return field36; }
            set { field36 = value; InitializedFieldIndexes.Add(36); }
        }
		T37 field37;
        public T37 Field37
        {
            get { return field37; }
            set { field37 = value; InitializedFieldIndexes.Add(37); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31, field32, field33, field34, field35, field36, field37 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
		T32 field32;
        public T32 Field32
        {
            get { return field32; }
            set { field32 = value; InitializedFieldIndexes.Add(32); }
        }
		T33 field33;
        public T33 Field33
        {
            get { return field33; }
            set { field33 = value; InitializedFieldIndexes.Add(33); }
        }
		T34 field34;
        public T34 Field34
        {
            get { return field34; }
            set { field34 = value; InitializedFieldIndexes.Add(34); }
        }
		T35 field35;
        public T35 Field35
        {
            get { return field35; }
            set { field35 = value; InitializedFieldIndexes.Add(35); }
        }
		T36 field36;
        public T36 Field36
        {
            get { return field36; }
            set { field36 = value; InitializedFieldIndexes.Add(36); }
        }
		T37 field37;
        public T37 Field37
        {
            get { return field37; }
            set { field37 = value; InitializedFieldIndexes.Add(37); }
        }
		T38 field38;
        public T38 Field38
        {
            get { return field38; }
            set { field38 = value; InitializedFieldIndexes.Add(38); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31, field32, field33, field34, field35, field36, field37, field38 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
		T32 field32;
        public T32 Field32
        {
            get { return field32; }
            set { field32 = value; InitializedFieldIndexes.Add(32); }
        }
		T33 field33;
        public T33 Field33
        {
            get { return field33; }
            set { field33 = value; InitializedFieldIndexes.Add(33); }
        }
		T34 field34;
        public T34 Field34
        {
            get { return field34; }
            set { field34 = value; InitializedFieldIndexes.Add(34); }
        }
		T35 field35;
        public T35 Field35
        {
            get { return field35; }
            set { field35 = value; InitializedFieldIndexes.Add(35); }
        }
		T36 field36;
        public T36 Field36
        {
            get { return field36; }
            set { field36 = value; InitializedFieldIndexes.Add(36); }
        }
		T37 field37;
        public T37 Field37
        {
            get { return field37; }
            set { field37 = value; InitializedFieldIndexes.Add(37); }
        }
		T38 field38;
        public T38 Field38
        {
            get { return field38; }
            set { field38 = value; InitializedFieldIndexes.Add(38); }
        }
		T39 field39;
        public T39 Field39
        {
            get { return field39; }
            set { field39 = value; InitializedFieldIndexes.Add(39); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31, field32, field33, field34, field35, field36, field37, field38, field39 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
		T32 field32;
        public T32 Field32
        {
            get { return field32; }
            set { field32 = value; InitializedFieldIndexes.Add(32); }
        }
		T33 field33;
        public T33 Field33
        {
            get { return field33; }
            set { field33 = value; InitializedFieldIndexes.Add(33); }
        }
		T34 field34;
        public T34 Field34
        {
            get { return field34; }
            set { field34 = value; InitializedFieldIndexes.Add(34); }
        }
		T35 field35;
        public T35 Field35
        {
            get { return field35; }
            set { field35 = value; InitializedFieldIndexes.Add(35); }
        }
		T36 field36;
        public T36 Field36
        {
            get { return field36; }
            set { field36 = value; InitializedFieldIndexes.Add(36); }
        }
		T37 field37;
        public T37 Field37
        {
            get { return field37; }
            set { field37 = value; InitializedFieldIndexes.Add(37); }
        }
		T38 field38;
        public T38 Field38
        {
            get { return field38; }
            set { field38 = value; InitializedFieldIndexes.Add(38); }
        }
		T39 field39;
        public T39 Field39
        {
            get { return field39; }
            set { field39 = value; InitializedFieldIndexes.Add(39); }
        }
		T40 field40;
        public T40 Field40
        {
            get { return field40; }
            set { field40 = value; InitializedFieldIndexes.Add(40); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31, field32, field33, field34, field35, field36, field37, field38, field39, field40 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
		T32 field32;
        public T32 Field32
        {
            get { return field32; }
            set { field32 = value; InitializedFieldIndexes.Add(32); }
        }
		T33 field33;
        public T33 Field33
        {
            get { return field33; }
            set { field33 = value; InitializedFieldIndexes.Add(33); }
        }
		T34 field34;
        public T34 Field34
        {
            get { return field34; }
            set { field34 = value; InitializedFieldIndexes.Add(34); }
        }
		T35 field35;
        public T35 Field35
        {
            get { return field35; }
            set { field35 = value; InitializedFieldIndexes.Add(35); }
        }
		T36 field36;
        public T36 Field36
        {
            get { return field36; }
            set { field36 = value; InitializedFieldIndexes.Add(36); }
        }
		T37 field37;
        public T37 Field37
        {
            get { return field37; }
            set { field37 = value; InitializedFieldIndexes.Add(37); }
        }
		T38 field38;
        public T38 Field38
        {
            get { return field38; }
            set { field38 = value; InitializedFieldIndexes.Add(38); }
        }
		T39 field39;
        public T39 Field39
        {
            get { return field39; }
            set { field39 = value; InitializedFieldIndexes.Add(39); }
        }
		T40 field40;
        public T40 Field40
        {
            get { return field40; }
            set { field40 = value; InitializedFieldIndexes.Add(40); }
        }
		T41 field41;
        public T41 Field41
        {
            get { return field41; }
            set { field41 = value; InitializedFieldIndexes.Add(41); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31, field32, field33, field34, field35, field36, field37, field38, field39, field40, field41 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
		T32 field32;
        public T32 Field32
        {
            get { return field32; }
            set { field32 = value; InitializedFieldIndexes.Add(32); }
        }
		T33 field33;
        public T33 Field33
        {
            get { return field33; }
            set { field33 = value; InitializedFieldIndexes.Add(33); }
        }
		T34 field34;
        public T34 Field34
        {
            get { return field34; }
            set { field34 = value; InitializedFieldIndexes.Add(34); }
        }
		T35 field35;
        public T35 Field35
        {
            get { return field35; }
            set { field35 = value; InitializedFieldIndexes.Add(35); }
        }
		T36 field36;
        public T36 Field36
        {
            get { return field36; }
            set { field36 = value; InitializedFieldIndexes.Add(36); }
        }
		T37 field37;
        public T37 Field37
        {
            get { return field37; }
            set { field37 = value; InitializedFieldIndexes.Add(37); }
        }
		T38 field38;
        public T38 Field38
        {
            get { return field38; }
            set { field38 = value; InitializedFieldIndexes.Add(38); }
        }
		T39 field39;
        public T39 Field39
        {
            get { return field39; }
            set { field39 = value; InitializedFieldIndexes.Add(39); }
        }
		T40 field40;
        public T40 Field40
        {
            get { return field40; }
            set { field40 = value; InitializedFieldIndexes.Add(40); }
        }
		T41 field41;
        public T41 Field41
        {
            get { return field41; }
            set { field41 = value; InitializedFieldIndexes.Add(41); }
        }
		T42 field42;
        public T42 Field42
        {
            get { return field42; }
            set { field42 = value; InitializedFieldIndexes.Add(42); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31, field32, field33, field34, field35, field36, field37, field38, field39, field40, field41, field42 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
		T32 field32;
        public T32 Field32
        {
            get { return field32; }
            set { field32 = value; InitializedFieldIndexes.Add(32); }
        }
		T33 field33;
        public T33 Field33
        {
            get { return field33; }
            set { field33 = value; InitializedFieldIndexes.Add(33); }
        }
		T34 field34;
        public T34 Field34
        {
            get { return field34; }
            set { field34 = value; InitializedFieldIndexes.Add(34); }
        }
		T35 field35;
        public T35 Field35
        {
            get { return field35; }
            set { field35 = value; InitializedFieldIndexes.Add(35); }
        }
		T36 field36;
        public T36 Field36
        {
            get { return field36; }
            set { field36 = value; InitializedFieldIndexes.Add(36); }
        }
		T37 field37;
        public T37 Field37
        {
            get { return field37; }
            set { field37 = value; InitializedFieldIndexes.Add(37); }
        }
		T38 field38;
        public T38 Field38
        {
            get { return field38; }
            set { field38 = value; InitializedFieldIndexes.Add(38); }
        }
		T39 field39;
        public T39 Field39
        {
            get { return field39; }
            set { field39 = value; InitializedFieldIndexes.Add(39); }
        }
		T40 field40;
        public T40 Field40
        {
            get { return field40; }
            set { field40 = value; InitializedFieldIndexes.Add(40); }
        }
		T41 field41;
        public T41 Field41
        {
            get { return field41; }
            set { field41 = value; InitializedFieldIndexes.Add(41); }
        }
		T42 field42;
        public T42 Field42
        {
            get { return field42; }
            set { field42 = value; InitializedFieldIndexes.Add(42); }
        }
		T43 field43;
        public T43 Field43
        {
            get { return field43; }
            set { field43 = value; InitializedFieldIndexes.Add(43); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31, field32, field33, field34, field35, field36, field37, field38, field39, field40, field41, field42, field43 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
		T32 field32;
        public T32 Field32
        {
            get { return field32; }
            set { field32 = value; InitializedFieldIndexes.Add(32); }
        }
		T33 field33;
        public T33 Field33
        {
            get { return field33; }
            set { field33 = value; InitializedFieldIndexes.Add(33); }
        }
		T34 field34;
        public T34 Field34
        {
            get { return field34; }
            set { field34 = value; InitializedFieldIndexes.Add(34); }
        }
		T35 field35;
        public T35 Field35
        {
            get { return field35; }
            set { field35 = value; InitializedFieldIndexes.Add(35); }
        }
		T36 field36;
        public T36 Field36
        {
            get { return field36; }
            set { field36 = value; InitializedFieldIndexes.Add(36); }
        }
		T37 field37;
        public T37 Field37
        {
            get { return field37; }
            set { field37 = value; InitializedFieldIndexes.Add(37); }
        }
		T38 field38;
        public T38 Field38
        {
            get { return field38; }
            set { field38 = value; InitializedFieldIndexes.Add(38); }
        }
		T39 field39;
        public T39 Field39
        {
            get { return field39; }
            set { field39 = value; InitializedFieldIndexes.Add(39); }
        }
		T40 field40;
        public T40 Field40
        {
            get { return field40; }
            set { field40 = value; InitializedFieldIndexes.Add(40); }
        }
		T41 field41;
        public T41 Field41
        {
            get { return field41; }
            set { field41 = value; InitializedFieldIndexes.Add(41); }
        }
		T42 field42;
        public T42 Field42
        {
            get { return field42; }
            set { field42 = value; InitializedFieldIndexes.Add(42); }
        }
		T43 field43;
        public T43 Field43
        {
            get { return field43; }
            set { field43 = value; InitializedFieldIndexes.Add(43); }
        }
		T44 field44;
        public T44 Field44
        {
            get { return field44; }
            set { field44 = value; InitializedFieldIndexes.Add(44); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31, field32, field33, field34, field35, field36, field37, field38, field39, field40, field41, field42, field43, field44 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
		T32 field32;
        public T32 Field32
        {
            get { return field32; }
            set { field32 = value; InitializedFieldIndexes.Add(32); }
        }
		T33 field33;
        public T33 Field33
        {
            get { return field33; }
            set { field33 = value; InitializedFieldIndexes.Add(33); }
        }
		T34 field34;
        public T34 Field34
        {
            get { return field34; }
            set { field34 = value; InitializedFieldIndexes.Add(34); }
        }
		T35 field35;
        public T35 Field35
        {
            get { return field35; }
            set { field35 = value; InitializedFieldIndexes.Add(35); }
        }
		T36 field36;
        public T36 Field36
        {
            get { return field36; }
            set { field36 = value; InitializedFieldIndexes.Add(36); }
        }
		T37 field37;
        public T37 Field37
        {
            get { return field37; }
            set { field37 = value; InitializedFieldIndexes.Add(37); }
        }
		T38 field38;
        public T38 Field38
        {
            get { return field38; }
            set { field38 = value; InitializedFieldIndexes.Add(38); }
        }
		T39 field39;
        public T39 Field39
        {
            get { return field39; }
            set { field39 = value; InitializedFieldIndexes.Add(39); }
        }
		T40 field40;
        public T40 Field40
        {
            get { return field40; }
            set { field40 = value; InitializedFieldIndexes.Add(40); }
        }
		T41 field41;
        public T41 Field41
        {
            get { return field41; }
            set { field41 = value; InitializedFieldIndexes.Add(41); }
        }
		T42 field42;
        public T42 Field42
        {
            get { return field42; }
            set { field42 = value; InitializedFieldIndexes.Add(42); }
        }
		T43 field43;
        public T43 Field43
        {
            get { return field43; }
            set { field43 = value; InitializedFieldIndexes.Add(43); }
        }
		T44 field44;
        public T44 Field44
        {
            get { return field44; }
            set { field44 = value; InitializedFieldIndexes.Add(44); }
        }
		T45 field45;
        public T45 Field45
        {
            get { return field45; }
            set { field45 = value; InitializedFieldIndexes.Add(45); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31, field32, field33, field34, field35, field36, field37, field38, field39, field40, field41, field42, field43, field44, field45 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
		T32 field32;
        public T32 Field32
        {
            get { return field32; }
            set { field32 = value; InitializedFieldIndexes.Add(32); }
        }
		T33 field33;
        public T33 Field33
        {
            get { return field33; }
            set { field33 = value; InitializedFieldIndexes.Add(33); }
        }
		T34 field34;
        public T34 Field34
        {
            get { return field34; }
            set { field34 = value; InitializedFieldIndexes.Add(34); }
        }
		T35 field35;
        public T35 Field35
        {
            get { return field35; }
            set { field35 = value; InitializedFieldIndexes.Add(35); }
        }
		T36 field36;
        public T36 Field36
        {
            get { return field36; }
            set { field36 = value; InitializedFieldIndexes.Add(36); }
        }
		T37 field37;
        public T37 Field37
        {
            get { return field37; }
            set { field37 = value; InitializedFieldIndexes.Add(37); }
        }
		T38 field38;
        public T38 Field38
        {
            get { return field38; }
            set { field38 = value; InitializedFieldIndexes.Add(38); }
        }
		T39 field39;
        public T39 Field39
        {
            get { return field39; }
            set { field39 = value; InitializedFieldIndexes.Add(39); }
        }
		T40 field40;
        public T40 Field40
        {
            get { return field40; }
            set { field40 = value; InitializedFieldIndexes.Add(40); }
        }
		T41 field41;
        public T41 Field41
        {
            get { return field41; }
            set { field41 = value; InitializedFieldIndexes.Add(41); }
        }
		T42 field42;
        public T42 Field42
        {
            get { return field42; }
            set { field42 = value; InitializedFieldIndexes.Add(42); }
        }
		T43 field43;
        public T43 Field43
        {
            get { return field43; }
            set { field43 = value; InitializedFieldIndexes.Add(43); }
        }
		T44 field44;
        public T44 Field44
        {
            get { return field44; }
            set { field44 = value; InitializedFieldIndexes.Add(44); }
        }
		T45 field45;
        public T45 Field45
        {
            get { return field45; }
            set { field45 = value; InitializedFieldIndexes.Add(45); }
        }
		T46 field46;
        public T46 Field46
        {
            get { return field46; }
            set { field46 = value; InitializedFieldIndexes.Add(46); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31, field32, field33, field34, field35, field36, field37, field38, field39, field40, field41, field42, field43, field44, field45, field46 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
		T32 field32;
        public T32 Field32
        {
            get { return field32; }
            set { field32 = value; InitializedFieldIndexes.Add(32); }
        }
		T33 field33;
        public T33 Field33
        {
            get { return field33; }
            set { field33 = value; InitializedFieldIndexes.Add(33); }
        }
		T34 field34;
        public T34 Field34
        {
            get { return field34; }
            set { field34 = value; InitializedFieldIndexes.Add(34); }
        }
		T35 field35;
        public T35 Field35
        {
            get { return field35; }
            set { field35 = value; InitializedFieldIndexes.Add(35); }
        }
		T36 field36;
        public T36 Field36
        {
            get { return field36; }
            set { field36 = value; InitializedFieldIndexes.Add(36); }
        }
		T37 field37;
        public T37 Field37
        {
            get { return field37; }
            set { field37 = value; InitializedFieldIndexes.Add(37); }
        }
		T38 field38;
        public T38 Field38
        {
            get { return field38; }
            set { field38 = value; InitializedFieldIndexes.Add(38); }
        }
		T39 field39;
        public T39 Field39
        {
            get { return field39; }
            set { field39 = value; InitializedFieldIndexes.Add(39); }
        }
		T40 field40;
        public T40 Field40
        {
            get { return field40; }
            set { field40 = value; InitializedFieldIndexes.Add(40); }
        }
		T41 field41;
        public T41 Field41
        {
            get { return field41; }
            set { field41 = value; InitializedFieldIndexes.Add(41); }
        }
		T42 field42;
        public T42 Field42
        {
            get { return field42; }
            set { field42 = value; InitializedFieldIndexes.Add(42); }
        }
		T43 field43;
        public T43 Field43
        {
            get { return field43; }
            set { field43 = value; InitializedFieldIndexes.Add(43); }
        }
		T44 field44;
        public T44 Field44
        {
            get { return field44; }
            set { field44 = value; InitializedFieldIndexes.Add(44); }
        }
		T45 field45;
        public T45 Field45
        {
            get { return field45; }
            set { field45 = value; InitializedFieldIndexes.Add(45); }
        }
		T46 field46;
        public T46 Field46
        {
            get { return field46; }
            set { field46 = value; InitializedFieldIndexes.Add(46); }
        }
		T47 field47;
        public T47 Field47
        {
            get { return field47; }
            set { field47 = value; InitializedFieldIndexes.Add(47); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31, field32, field33, field34, field35, field36, field37, field38, field39, field40, field41, field42, field43, field44, field45, field46, field47 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
		T32 field32;
        public T32 Field32
        {
            get { return field32; }
            set { field32 = value; InitializedFieldIndexes.Add(32); }
        }
		T33 field33;
        public T33 Field33
        {
            get { return field33; }
            set { field33 = value; InitializedFieldIndexes.Add(33); }
        }
		T34 field34;
        public T34 Field34
        {
            get { return field34; }
            set { field34 = value; InitializedFieldIndexes.Add(34); }
        }
		T35 field35;
        public T35 Field35
        {
            get { return field35; }
            set { field35 = value; InitializedFieldIndexes.Add(35); }
        }
		T36 field36;
        public T36 Field36
        {
            get { return field36; }
            set { field36 = value; InitializedFieldIndexes.Add(36); }
        }
		T37 field37;
        public T37 Field37
        {
            get { return field37; }
            set { field37 = value; InitializedFieldIndexes.Add(37); }
        }
		T38 field38;
        public T38 Field38
        {
            get { return field38; }
            set { field38 = value; InitializedFieldIndexes.Add(38); }
        }
		T39 field39;
        public T39 Field39
        {
            get { return field39; }
            set { field39 = value; InitializedFieldIndexes.Add(39); }
        }
		T40 field40;
        public T40 Field40
        {
            get { return field40; }
            set { field40 = value; InitializedFieldIndexes.Add(40); }
        }
		T41 field41;
        public T41 Field41
        {
            get { return field41; }
            set { field41 = value; InitializedFieldIndexes.Add(41); }
        }
		T42 field42;
        public T42 Field42
        {
            get { return field42; }
            set { field42 = value; InitializedFieldIndexes.Add(42); }
        }
		T43 field43;
        public T43 Field43
        {
            get { return field43; }
            set { field43 = value; InitializedFieldIndexes.Add(43); }
        }
		T44 field44;
        public T44 Field44
        {
            get { return field44; }
            set { field44 = value; InitializedFieldIndexes.Add(44); }
        }
		T45 field45;
        public T45 Field45
        {
            get { return field45; }
            set { field45 = value; InitializedFieldIndexes.Add(45); }
        }
		T46 field46;
        public T46 Field46
        {
            get { return field46; }
            set { field46 = value; InitializedFieldIndexes.Add(46); }
        }
		T47 field47;
        public T47 Field47
        {
            get { return field47; }
            set { field47 = value; InitializedFieldIndexes.Add(47); }
        }
		T48 field48;
        public T48 Field48
        {
            get { return field48; }
            set { field48 = value; InitializedFieldIndexes.Add(48); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31, field32, field33, field34, field35, field36, field37, field38, field39, field40, field41, field42, field43, field44, field45, field46, field47, field48 };
        }
    }
	public class AnonymousType<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49> : AnonymousType
    {
		T0 field0;
        public T0 Field0
        {
            get { return field0; }
            set { field0 = value; InitializedFieldIndexes.Add(0); }
        }
		T1 field1;
        public T1 Field1
        {
            get { return field1; }
            set { field1 = value; InitializedFieldIndexes.Add(1); }
        }
		T2 field2;
        public T2 Field2
        {
            get { return field2; }
            set { field2 = value; InitializedFieldIndexes.Add(2); }
        }
		T3 field3;
        public T3 Field3
        {
            get { return field3; }
            set { field3 = value; InitializedFieldIndexes.Add(3); }
        }
		T4 field4;
        public T4 Field4
        {
            get { return field4; }
            set { field4 = value; InitializedFieldIndexes.Add(4); }
        }
		T5 field5;
        public T5 Field5
        {
            get { return field5; }
            set { field5 = value; InitializedFieldIndexes.Add(5); }
        }
		T6 field6;
        public T6 Field6
        {
            get { return field6; }
            set { field6 = value; InitializedFieldIndexes.Add(6); }
        }
		T7 field7;
        public T7 Field7
        {
            get { return field7; }
            set { field7 = value; InitializedFieldIndexes.Add(7); }
        }
		T8 field8;
        public T8 Field8
        {
            get { return field8; }
            set { field8 = value; InitializedFieldIndexes.Add(8); }
        }
		T9 field9;
        public T9 Field9
        {
            get { return field9; }
            set { field9 = value; InitializedFieldIndexes.Add(9); }
        }
		T10 field10;
        public T10 Field10
        {
            get { return field10; }
            set { field10 = value; InitializedFieldIndexes.Add(10); }
        }
		T11 field11;
        public T11 Field11
        {
            get { return field11; }
            set { field11 = value; InitializedFieldIndexes.Add(11); }
        }
		T12 field12;
        public T12 Field12
        {
            get { return field12; }
            set { field12 = value; InitializedFieldIndexes.Add(12); }
        }
		T13 field13;
        public T13 Field13
        {
            get { return field13; }
            set { field13 = value; InitializedFieldIndexes.Add(13); }
        }
		T14 field14;
        public T14 Field14
        {
            get { return field14; }
            set { field14 = value; InitializedFieldIndexes.Add(14); }
        }
		T15 field15;
        public T15 Field15
        {
            get { return field15; }
            set { field15 = value; InitializedFieldIndexes.Add(15); }
        }
		T16 field16;
        public T16 Field16
        {
            get { return field16; }
            set { field16 = value; InitializedFieldIndexes.Add(16); }
        }
		T17 field17;
        public T17 Field17
        {
            get { return field17; }
            set { field17 = value; InitializedFieldIndexes.Add(17); }
        }
		T18 field18;
        public T18 Field18
        {
            get { return field18; }
            set { field18 = value; InitializedFieldIndexes.Add(18); }
        }
		T19 field19;
        public T19 Field19
        {
            get { return field19; }
            set { field19 = value; InitializedFieldIndexes.Add(19); }
        }
		T20 field20;
        public T20 Field20
        {
            get { return field20; }
            set { field20 = value; InitializedFieldIndexes.Add(20); }
        }
		T21 field21;
        public T21 Field21
        {
            get { return field21; }
            set { field21 = value; InitializedFieldIndexes.Add(21); }
        }
		T22 field22;
        public T22 Field22
        {
            get { return field22; }
            set { field22 = value; InitializedFieldIndexes.Add(22); }
        }
		T23 field23;
        public T23 Field23
        {
            get { return field23; }
            set { field23 = value; InitializedFieldIndexes.Add(23); }
        }
		T24 field24;
        public T24 Field24
        {
            get { return field24; }
            set { field24 = value; InitializedFieldIndexes.Add(24); }
        }
		T25 field25;
        public T25 Field25
        {
            get { return field25; }
            set { field25 = value; InitializedFieldIndexes.Add(25); }
        }
		T26 field26;
        public T26 Field26
        {
            get { return field26; }
            set { field26 = value; InitializedFieldIndexes.Add(26); }
        }
		T27 field27;
        public T27 Field27
        {
            get { return field27; }
            set { field27 = value; InitializedFieldIndexes.Add(27); }
        }
		T28 field28;
        public T28 Field28
        {
            get { return field28; }
            set { field28 = value; InitializedFieldIndexes.Add(28); }
        }
		T29 field29;
        public T29 Field29
        {
            get { return field29; }
            set { field29 = value; InitializedFieldIndexes.Add(29); }
        }
		T30 field30;
        public T30 Field30
        {
            get { return field30; }
            set { field30 = value; InitializedFieldIndexes.Add(30); }
        }
		T31 field31;
        public T31 Field31
        {
            get { return field31; }
            set { field31 = value; InitializedFieldIndexes.Add(31); }
        }
		T32 field32;
        public T32 Field32
        {
            get { return field32; }
            set { field32 = value; InitializedFieldIndexes.Add(32); }
        }
		T33 field33;
        public T33 Field33
        {
            get { return field33; }
            set { field33 = value; InitializedFieldIndexes.Add(33); }
        }
		T34 field34;
        public T34 Field34
        {
            get { return field34; }
            set { field34 = value; InitializedFieldIndexes.Add(34); }
        }
		T35 field35;
        public T35 Field35
        {
            get { return field35; }
            set { field35 = value; InitializedFieldIndexes.Add(35); }
        }
		T36 field36;
        public T36 Field36
        {
            get { return field36; }
            set { field36 = value; InitializedFieldIndexes.Add(36); }
        }
		T37 field37;
        public T37 Field37
        {
            get { return field37; }
            set { field37 = value; InitializedFieldIndexes.Add(37); }
        }
		T38 field38;
        public T38 Field38
        {
            get { return field38; }
            set { field38 = value; InitializedFieldIndexes.Add(38); }
        }
		T39 field39;
        public T39 Field39
        {
            get { return field39; }
            set { field39 = value; InitializedFieldIndexes.Add(39); }
        }
		T40 field40;
        public T40 Field40
        {
            get { return field40; }
            set { field40 = value; InitializedFieldIndexes.Add(40); }
        }
		T41 field41;
        public T41 Field41
        {
            get { return field41; }
            set { field41 = value; InitializedFieldIndexes.Add(41); }
        }
		T42 field42;
        public T42 Field42
        {
            get { return field42; }
            set { field42 = value; InitializedFieldIndexes.Add(42); }
        }
		T43 field43;
        public T43 Field43
        {
            get { return field43; }
            set { field43 = value; InitializedFieldIndexes.Add(43); }
        }
		T44 field44;
        public T44 Field44
        {
            get { return field44; }
            set { field44 = value; InitializedFieldIndexes.Add(44); }
        }
		T45 field45;
        public T45 Field45
        {
            get { return field45; }
            set { field45 = value; InitializedFieldIndexes.Add(45); }
        }
		T46 field46;
        public T46 Field46
        {
            get { return field46; }
            set { field46 = value; InitializedFieldIndexes.Add(46); }
        }
		T47 field47;
        public T47 Field47
        {
            get { return field47; }
            set { field47 = value; InitializedFieldIndexes.Add(47); }
        }
		T48 field48;
        public T48 Field48
        {
            get { return field48; }
            set { field48 = value; InitializedFieldIndexes.Add(48); }
        }
		T49 field49;
        public T49 Field49
        {
            get { return field49; }
            set { field49 = value; InitializedFieldIndexes.Add(49); }
        }
      
		public override List<object> GetValues()
        {
            return new List<object>() { field0, field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12, field13, field14, field15, field16, field17, field18, field19, field20, field21, field22, field23, field24, field25, field26, field27, field28, field29, field30, field31, field32, field33, field34, field35, field36, field37, field38, field39, field40, field41, field42, field43, field44, field45, field46, field47, field48, field49 };
        }
    }
}