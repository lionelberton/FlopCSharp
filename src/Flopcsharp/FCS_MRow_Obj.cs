using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    class FCS_MRow_Obj : FCS_MRow
    {
        public FCS_MRow_Obj(FCS_Model m)
            : base(m, "")
        {

        }

        public override bool WriteCoeff()
        {
            if (_rowCoeff.Count == 0)
            {
                return false;
            }
            foreach (KeyValuePair<int, double> pair in _rowCoeff)
            {
                _model.SetObjCoef(pair.Key, pair.Value);
            }

            return true;
        }
    }
}
