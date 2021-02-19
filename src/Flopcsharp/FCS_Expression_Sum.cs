using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public class FCS_Expression_Sum : FCS_Expression_Add
    {
        FCS_Domain _dom;
        FCS_Expression_Linear _exp;

        public FCS_Expression_Sum(FCS_Domain dom, FCS_Expression_Linear exp)
        {
            _dom = dom;
            _exp = exp;
        }
        public override void Action(FCS_MRow mrow, double factor)
        {
            foreach (int i in _dom.Iterate())
            {
                _exp.Action(mrow, factor);
                //Console.Out.WriteLine(_dom.GetCurrentString());
            }
        }
        public override double GetValue()
        {
            double res=0;
            foreach (int i in _dom.Iterate())
            {
                res+= _exp.GetValue();
            }
            return res;
        }



    }
}
