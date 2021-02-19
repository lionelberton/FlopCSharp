using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public class FCS_Expression_Variable : FCS_Expression
    {
        FCS_Variable _var;
        FCS_Domain _dom;

        public FCS_Expression_Variable(FCS_Variable var, FCS_Domain dom)
        {
            _var = var;
            _dom = dom;
        }

        public int BuildOrGet()
        {
            return _var.eval(_dom.Append());
        }

        public static FCS_Expression_Linear operator *(double c, FCS_Expression_Variable v)
        {
            FCS_Expression_Linear explin = new FCS_Expression_Linear(c, v);
            return explin;
        }
        public static FCS_Expression_Linear operator *(FCS_Expression_Numeric c, FCS_Expression_Variable v)
        {
            FCS_Expression_Linear explin = new FCS_Expression_Linear(c, v);
            return explin;
        }
        public static FCS_Expression_Linear operator *( FCS_Expression_Variable v,FCS_Expression_Numeric c)
        {
            FCS_Expression_Linear explin = new FCS_Expression_Linear(c, v);
            return explin;
        }
        public static FCS_Expression_Add operator +(FCS_Expression_Variable v, FCS_Expression_Variable w)
        {
            return (new FCS_Expression_Linear(1, v) + new FCS_Expression_Linear(1, w));
        }

        public static FCS_Expression_Add operator -(FCS_Expression_Variable v, FCS_Expression_Variable w)
        {
            return (new FCS_Expression_Linear(1, v) + new FCS_Expression_Linear(-1, w));
        }

        public IEnumerable<FCS_MCol> Iterate()
        {
            foreach (int col in _dom.Iterate())
            {
                yield return _var.GetMCol(col);
            }
        }

        public override double GetValue()
        {
            return _var.GetValue(_dom.Eval());
        }

    }
}
