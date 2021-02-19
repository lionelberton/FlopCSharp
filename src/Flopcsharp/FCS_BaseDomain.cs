using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public class FCS_BaseDomain : FCS_Domain, IFCS_Domain
    {

        FCS_Subset set;
        FCS_Index ind;

        internal FCS_BaseDomain()
        {
            ind = new FCS_Index("basedomain");
        }
        public FCS_BaseDomain(FCS_Subset s,FCS_Index k)
        {
            set = s;
            ind = k;   
        }

        public override IEnumerable<int> Iterate()
        {
            if (set != null)
            {
                foreach (FCS_uplet up in set.Indexes(ind))
                {
                    if (_cond.Action())
                    {
                        yield return ind.Index;
                    }
                }
            }
        }

        public override int Eval()
        {
            return ind.Index;
        }

        public override string GetCurrentString()
        {
            return set.GetCurrentString(ind.Index);
        }

        public override bool SetIndexTo(int i)
        {
            if (!ind.IsLocked)
            {
                ind.Index = i;
                return _cond.Action();
            }
            return false;
        }
    }
}
