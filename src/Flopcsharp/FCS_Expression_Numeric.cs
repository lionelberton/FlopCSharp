using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public class FCS_Expression_Numeric
    {
        delegate double NumCalc();
        //double _d;
        NumCalc _Eval;

        private FCS_Expression_Numeric(NumCalc eval)
        {
            _Eval = eval;
        }
        public static implicit operator FCS_Expression_Numeric(double d)
        {
            return new FCS_Expression_Numeric(delegate { return d; });
        }
        public static implicit operator FCS_Expression_Numeric(FCS_Index d)
        {
            return new FCS_Expression_Numeric(delegate { return d.Index; });
        }
        public static implicit operator FCS_Expression_Numeric(FCS_Expression_Data d)
        {
            return new FCS_Expression_Numeric(delegate { return d.eval(); });
        }
        public static FCS_Expression_Numeric operator *(double d, FCS_Expression_Numeric exp)
        {
            return new FCS_Expression_Numeric(delegate { return d * exp.Eval(); });
        }
        public static FCS_Expression_Numeric operator /( FCS_Expression_Numeric exp,double d)
        {
            return new FCS_Expression_Numeric(delegate { return exp.Eval()/d; });
        }
        public static FCS_Expression_Numeric operator +(FCS_Expression_Numeric exp1, FCS_Expression_Numeric exp2)
        {
            return new FCS_Expression_Numeric(delegate { return exp1.Eval() + exp2.Eval(); });
        }
        public static FCS_Expression_Numeric operator -(FCS_Expression_Numeric exp1, FCS_Expression_Numeric exp2)
        {
            return new FCS_Expression_Numeric(delegate { return exp1.Eval() - exp2.Eval(); });
        }
        public static FCS_Expression_Numeric operator *(FCS_Expression_Numeric exp1, FCS_Expression_Numeric exp2)
        {
            return new FCS_Expression_Numeric(delegate { return exp1.Eval() * exp2.Eval(); });
        }
        public static FCS_Expression_Numeric operator /(FCS_Expression_Numeric exp1, FCS_Expression_Numeric exp2)
        {
            return new FCS_Expression_Numeric(delegate { return exp1.Eval() / exp2.Eval(); });
        }
        public double Eval()
        {
            return _Eval();
        }

        public static FCS_Expression_Numeric MinBound()
        {
            return new FCS_Expression_Numeric(delegate { return double.NegativeInfinity; });
        }
        public static FCS_Expression_Numeric MaxBound()
        {
            return new FCS_Expression_Numeric(delegate { return double.PositiveInfinity; });
        }
        public static FCS_Expression_Numeric ZeroBound()
        {
            return new FCS_Expression_Numeric(delegate { return 0; });
        }
    }
}
