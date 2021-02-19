using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public class FCS_Condition
    {

        public Func<int> Action;

        public FCS_Condition(Func<int> act)
        {
            Action = act;
        }

        public static implicit operator FCS_Condition(FCS_Index i)
        {
            return new FCS_Condition(() => i.Index);
        }
        public static FCS_Condition operator *(FCS_Condition i, FCS_Condition j)
        {
            return new FCS_Condition(() => i.Action() * j.Action());
        }
        public static FCS_Condition operator +(FCS_Condition i, FCS_Condition j)
        {
            return new FCS_Condition(() => i.Action() + j.Action());
        }
        public static FCS_Condition operator -(FCS_Condition i, FCS_Condition j)
        {
            return new FCS_Condition(() => i.Action() - j.Action());
        }

        public static FCS_Condition_bool operator ==(FCS_Condition i, FCS_Condition j)
        {
            return new FCS_Condition_bool(() => i.Action() == j.Action());
        }
        public static FCS_Condition_bool operator <=(FCS_Condition i, FCS_Condition j)
        {
            return new FCS_Condition_bool(() => i.Action() <= j.Action());
        }
        public static FCS_Condition_bool operator >=(FCS_Condition i, FCS_Condition j)
        {
            return new FCS_Condition_bool(() => i.Action() >= j.Action());
        }
        public static FCS_Condition_bool operator !=(FCS_Condition i, FCS_Condition j)
        {
            return new FCS_Condition_bool(() => i.Action() != j.Action());
        }


    }
}
