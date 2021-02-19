using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public enum BOUNDS
    {
        EQ,
        INF,
        SUP
    }
    public class FCS_Expression_bound : FCS_Expression
    {

        FCS_Expression_Numeric _lbound;
        FCS_Expression_Numeric _ubound;
        //BOUNDS _boundtype;
        FCS_Expression _exp;

        public FCS_Expression_bound(FCS_Expression exp, FCS_Expression_Numeric bound, BOUNDS tb)
        {
            _lbound = FCS_Expression_Numeric.MinBound();
            _ubound = FCS_Expression_Numeric.MaxBound();
            _exp = exp;
            //_boundtype = tb;
            SetBounds(bound, tb);
        }
        private void SetBounds(FCS_Expression_Numeric bound, BOUNDS tb)
        {
            switch (tb)
            {
                case BOUNDS.INF:
                    {
                        _lbound = bound;
                        break;
                    }
                case BOUNDS.SUP:
                    {
                        _ubound = bound;
                        break;
                    }
                case BOUNDS.EQ:
                    {
                        _lbound = bound;
                        _ubound = bound;
                        break;
                    }
            }
        }

        public override void Action(FCS_MRow mrow,double factor)
        {
            _exp.Action(mrow,factor);


                mrow.LBound = _lbound.Eval();

                mrow.UBound = _ubound.Eval();
            



        }

        public static FCS_Expression_bound GetExpressionBound(FCS_Expression exp, FCS_Expression_Numeric bound, BOUNDS tb)
        {
            if (exp is FCS_Expression_bound)
            {
                FCS_Expression_bound xb = exp as FCS_Expression_bound;
                xb.SetBounds(bound, tb);
                return xb;
            }
            else
            {
                return new FCS_Expression_bound(exp, bound, tb);
            }
        }


        //public static FCS_Expression_bound operator <=(FCS_Expression_bound exp1, FCS_Expression_Numeric d)
        //{
        //    FCS_Expression_bound expb = FCS_Expression_bound.GetExpressionBound(exp1, d, BOUNDS.SUP);
        //    return expb;
        //}
        //public static FCS_Expression_bound operator <=(FCS_Expression_Numeric d, FCS_Expression_bound exp1)
        //{
        //    FCS_Expression_bound expb = FCS_Expression_bound.GetExpressionBound(exp1, d, BOUNDS.INF);
        //    return expb;
        //}
        //public static FCS_Expression_bound operator >=(FCS_Expression_bound exp1, FCS_Expression_Numeric d)
        //{
        //    FCS_Expression_bound expb = FCS_Expression_bound.GetExpressionBound(exp1, d, BOUNDS.INF);
        //    return expb;
        //}
        //public static FCS_Expression_bound operator >=(FCS_Expression_Numeric d, FCS_Expression_bound exp1)
        //{
        //    FCS_Expression_bound expb = FCS_Expression_bound.GetExpressionBound(exp1, d, BOUNDS.SUP);
        //    return expb;
        //}
        //public static FCS_Expression_bound operator ==(FCS_Expression_bound exp1, FCS_Expression_Numeric d)
        //{
        //    FCS_Expression_bound expb = FCS_Expression_bound.GetExpressionBound(exp1, d, BOUNDS.EQ);
        //    return expb;
        //}
        //public static FCS_Expression_bound operator !=(FCS_Expression_bound exp1, FCS_Expression_Numeric d)
        //{
        //    FCS_Expression_bound expb = FCS_Expression_bound.GetExpressionBound(exp1, d, BOUNDS.EQ);
        //    return expb;
        //}
    }
}
