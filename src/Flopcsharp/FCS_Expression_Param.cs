using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public class FCS_Expression_Param : FCS_Expression_Linear
    {
        FCS_Expression_Numeric _datacoeff;
        //FCS_Expression_Variable _v;
        public FCS_Expression_Param(FCS_Expression_Numeric coeff, FCS_Expression_Variable v)
        {
            _coeff = 1;
            _datacoeff = coeff;
            _v = v;
        }
        public override void Action(FCS_MRow mrow,double factor)
        {
            mrow[_v.BuildOrGet()] = _coeff.Eval()*factor*_datacoeff.Eval();
        }
    }
}
