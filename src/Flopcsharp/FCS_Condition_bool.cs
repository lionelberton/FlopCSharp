using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public delegate bool EvalCond();
    public class FCS_Condition_bool
    {
        public EvalCond Action;

        public FCS_Condition_bool(EvalCond act)
        {
            Action = act;
        }
        public FCS_Condition_bool()
        {
            Action = delegate() { return true; };
        }


        public static FCS_Condition_bool operator &(FCS_Condition_bool i, FCS_Condition_bool j)
        {
            return new FCS_Condition_bool(delegate() { 
                return i.Action() && j.Action(); });
        }
        public static FCS_Condition_bool operator |(FCS_Condition_bool i, FCS_Condition_bool j)
        {
            return new FCS_Condition_bool(delegate() { return i.Action() || j.Action(); });
        }
        public static bool operator true(FCS_Condition_bool c)
        {
            return c.Action();
        }
        public static bool operator false(FCS_Condition_bool c)
        {
            return !c.Action();
        }
    }
}
