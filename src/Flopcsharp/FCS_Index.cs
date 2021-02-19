using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public class FCS_Index : IFCS_Domain
    {
        private int _index;
        private string _name;

        private bool _locked;

        public void Lock()
        {
            _locked = true;
        }
        public void Unlock()
        {
            _locked = false;
        }
        public bool IsLocked
        {
            get
            {
                return _locked;
            }
        }
        public int Index
        {
            get
            {
                //Console.Out.WriteLine("reading index " + _name + " = " + _index);
                return _index;
            }
            set
            {
                //Console.Out.WriteLine("setting index " + _name + " = " + value);
                _index = value;
            }
        }
        public FCS_Index()
        {

        }
        public FCS_Index(int i)
        {
            _index = i;
        }
        public FCS_Index(string s)
        {
            _name = s;
        }
        public static FCS_Condition operator *(int c, FCS_Index i)
        {
            return new FCS_Condition(() => i.Index * c);
        }
        public static FCS_Condition operator +(int c, FCS_Index i)
        {
            return new FCS_Condition(() => i.Index + c);
        }
        public static FCS_Condition operator -(FCS_Index i, int c)
        {
            return new FCS_Condition(() => i.Index - c);
        }

        public static FCS_Condition operator *(FCS_Index i, FCS_Index j)
        {
            return new FCS_Condition(() => i.Index * j.Index);
        }
        public static FCS_Condition operator +(FCS_Index i, FCS_Index j)
        {
            return new FCS_Condition(() => i.Index + j.Index);
        }
        public static FCS_Condition operator -(FCS_Index i, FCS_Index j)
        {
            return new FCS_Condition(() => i.Index - j.Index);
        }

        public static FCS_Condition_bool operator <=(FCS_Index i, FCS_Index j)
        {
            return new FCS_Condition_bool(() => i.Index <= j.Index);
        }
        public static FCS_Condition_bool operator >=(FCS_Index i, FCS_Index j)
        {
            return new FCS_Condition_bool(() => i.Index >= j.Index);
        }
        public static FCS_Condition_bool operator ==(FCS_Index i, FCS_Index j)
        {
            return new FCS_Condition_bool(() => i.Index == j.Index);
        }
        public static FCS_Condition_bool operator !=(FCS_Index i, FCS_Index j)
        {
            return new FCS_Condition_bool(() => i.Index != j.Index);
        }

        public static implicit operator FCS_Index(int i)
        {
            return new FCS_Index(i);
        }

        public override bool Equals(object obj)
        {
            var o = obj as FCS_Index;
            if (o == null) return false;
            return o._index == this._index;
        }
        public override int GetHashCode()
        {
            return _index.GetHashCode();
        }

    }
}
