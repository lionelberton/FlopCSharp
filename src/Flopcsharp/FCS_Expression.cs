using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public class FCS_Expression
    {


        public virtual void Action(FCS_MRow mrow, double factor)
        {

        }

        public static FCS_Expression_bound operator <=(FCS_Expression exp1, FCS_Expression_Numeric d)
        {
            FCS_Expression_bound expb = FCS_Expression_bound.GetExpressionBound(exp1, d, BOUNDS.SUP);
            return expb;
        }
        public static FCS_Expression_bound operator <=(FCS_Expression_Numeric d, FCS_Expression exp1)
        {
            FCS_Expression_bound expb = FCS_Expression_bound.GetExpressionBound(exp1, d, BOUNDS.INF);
            return expb;
        }
        public static FCS_Expression_bound operator >=(FCS_Expression exp1, FCS_Expression_Numeric d)
        {
            FCS_Expression_bound expb = FCS_Expression_bound.GetExpressionBound(exp1, d, BOUNDS.INF);
            return expb;
        }
        public static FCS_Expression_bound operator >=(FCS_Expression_Numeric d, FCS_Expression exp1)
        {
            FCS_Expression_bound expb = FCS_Expression_bound.GetExpressionBound(exp1, d, BOUNDS.SUP);
            return expb;
        }
        public static FCS_Expression_bound operator ==(FCS_Expression exp1, FCS_Expression_Numeric d)
        {
            FCS_Expression_bound expb = FCS_Expression_bound.GetExpressionBound(exp1, d, BOUNDS.EQ);
            return expb;
        }
        public static FCS_Expression_bound operator !=(FCS_Expression exp1, FCS_Expression_Numeric d)
        {
            FCS_Expression_bound expb = FCS_Expression_bound.GetExpressionBound(exp1, d, BOUNDS.EQ);
            return expb;
        }

        public virtual double GetValue()
        {
            return 0;
        }

    }
}
