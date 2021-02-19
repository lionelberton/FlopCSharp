using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public class FCS_Expression_Linear : FCS_Expression
    {
        protected FCS_Expression_Numeric _coeff;
        protected FCS_Expression_Variable _v;

        protected FCS_Expression_Linear()
        {
        }
        public FCS_Expression_Linear(double coeff, FCS_Expression_Variable v)
        {
            _coeff = coeff;
            _v = v;
        }
        public FCS_Expression_Linear(FCS_Expression_Numeric coeff, FCS_Expression_Variable v)
        {
            _coeff = coeff;
            _v = v;
        }
        public FCS_Expression_Linear(FCS_Expression_Variable v)
        {
            _coeff = 1;
            _v = v;
        }
        public override void Action(FCS_MRow mrow, double factor)
        {
            mrow[_v.BuildOrGet()] = factor * _coeff.Eval();
        }

        public static implicit operator FCS_Expression_Linear(FCS_Expression_Variable v)
        {
            return new FCS_Expression_Linear(v);
        }

        public static FCS_Expression_Add operator -(FCS_Expression_Linear exp1, FCS_Expression_Linear exp2)
        {
            return new FCS_Expression_Add(exp1, exp2, -1);
        }

        public static FCS_Expression_Add operator +(FCS_Expression_Linear exp1, FCS_Expression_Linear exp2)
        {
            FCS_Expression_Add expadd = new FCS_Expression_Add(exp1, exp2);
            return expadd;
        }

        public static FCS_Expression_Linear operator *(FCS_Expression_Numeric v, FCS_Expression_Linear exp2)
        {
            exp2._coeff = v * exp2._coeff;
            return exp2;
        }
        public static FCS_Expression_Linear operator /( FCS_Expression_Linear exp2,FCS_Expression_Numeric v)
        {
            exp2._coeff = exp2._coeff/v;
            return exp2;
        }

        public virtual void DistributeFactor(FCS_Expression_Numeric f)
        {
            _coeff = _coeff * f;
        }

        public override double GetValue()
        {
            return _v.GetValue() * _coeff.Eval();
        }
    }
}
