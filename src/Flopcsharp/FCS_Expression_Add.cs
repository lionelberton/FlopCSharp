using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public class FCS_Expression_Add : FCS_Expression_Linear
    {
        FCS_Expression_Linear _exp1;
        FCS_Expression_Linear _exp2;
        
        double _sign;

        protected FCS_Expression_Add()
        {

        }
        public FCS_Expression_Add(FCS_Expression_Linear exp1, FCS_Expression_Linear exp2)
        {
            _exp1 = exp1;
            _exp2 = exp2;
            _coeff = 1;
            _sign=1;
        }
        //pour les soustractions, le facteur vaut -1
        public FCS_Expression_Add(FCS_Expression_Linear exp1, FCS_Expression_Linear exp2,double sign):
            this(exp1,exp2)
        {
            _sign=sign;   
        }

        public override void Action(FCS_MRow mrow,double factor)
        {
            _exp1.Action(mrow,factor*_coeff.Eval());
            _exp2.Action(mrow,factor*_coeff.Eval()*_sign);
        }

        public static FCS_Expression_Add operator *(FCS_Expression_Numeric v, FCS_Expression_Add exp1)
        {
            exp1._coeff= exp1._coeff* v;
            return exp1;
        }
        public static FCS_Expression_Add operator *( FCS_Expression_Add exp1,FCS_Expression_Numeric v)
        {
            exp1._coeff = exp1._coeff* v;
            return exp1;
        }
        public static FCS_Expression_Add operator /(FCS_Expression_Add exp1, FCS_Expression_Numeric v)
        {
            exp1._coeff = exp1._coeff / v;
            return exp1;
        }

        public override double GetValue()
        {
            return _coeff.Eval()*(_exp1.GetValue() + _sign *_exp2.GetValue());
        }
    }
}
