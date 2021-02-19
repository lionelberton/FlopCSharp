using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public class FCS_CrossDomain : FCS_Domain
    {
        public FCS_CrossDomain(FCS_Domain d1, FCS_Domain d2)
        {
            dom1 = d1;
            dom2 = d2;
            nDim = 2;
        }
        public override int Eval()
        {
            return 1;
        }
        public override string GetCurrentString()
        {
            return "(" + dom1.GetCurrentString() + " * " + dom2.GetCurrentString() + ")";
        }
        public override IEnumerable<int> Iterate()
        {
            foreach (int i in dom1.Iterate())
            {
                foreach (int j in dom2.Iterate())
                {
                    yield return i;
                }
            }
        }
    }
}
