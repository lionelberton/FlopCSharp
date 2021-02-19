using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public class FCS_Domain : IFCS_Domain
    {
        protected FCS_Domain dom1;
        protected FCS_Domain dom2;
        protected FCS_Domain dom3;
        protected FCS_Domain dom4;
        protected FCS_Domain dom5;
        protected FCS_Condition_bool _cond = new FCS_Condition_bool();
        protected int nDim = 0;

        FCS_Subset _set;

        protected FCS_Domain()
        {

        }
        #region constructors
        public FCS_Domain(FCS_Subset set, FCS_Domain d1)
        {
            dom1 = d1;
            nDim = 1;
            _set = set;
        }
        public FCS_Domain(FCS_Subset set, FCS_Domain d1, FCS_Domain d2)
        {
            dom1 = d1;
            dom2 = d2;
            nDim = 2;
            _set = set;
        }
        public FCS_Domain(FCS_Subset set, FCS_Domain d1, FCS_Domain d2, FCS_Domain d3)
        {
            dom1 = d1;
            dom2 = d2;
            dom3 = d3;
            nDim = 3;
            _set = set;
        }
        public FCS_Domain(FCS_Subset set, FCS_Domain d1, FCS_Domain d2, FCS_Domain d3, FCS_Domain d4)
        {
            dom1 = d1;
            dom2 = d2;
            dom3 = d3;
            dom4 = d4;
            nDim = 4;
            _set = set;
        }
        public FCS_Domain(FCS_Subset set, FCS_Domain d1, FCS_Domain d2, FCS_Domain d3, FCS_Domain d4, FCS_Domain d5)
        {
            dom1 = d1;
            dom2 = d2;
            dom3 = d3;
            dom4 = d4;
            dom5 = d5;
            nDim = 5;
            _set = set;
        }
        #endregion

        public static FCS_CrossDomain operator *(FCS_Domain d1, FCS_Domain d2)
        {
            return new FCS_CrossDomain(d1, d2);
        }

        public virtual IEnumerable<int> IterateCreate()
        {
            foreach (int k in EnumDomains(GetStack()))
            {
                yield return Append();
            }
        }
        private Stack<FCS_Domain> GetStack()
        {
            Stack<FCS_Domain> domains = new Stack<FCS_Domain>();
            switch (nDim)
            {
                case 1:
                    {
                        domains.Push(dom1);
                        break;
                    }
                case 2:
                    {
                        domains.Push(dom2);
                        domains.Push(dom1);

                        break;
                    }
                case 3:
                    {
                        domains.Push(dom3);
                        domains.Push(dom2);
                        domains.Push(dom1);
                        ;
                        break;
                    }
                case 4:
                    {
                        domains.Push(dom4);
                        domains.Push(dom3);
                        domains.Push(dom2);
                        domains.Push(dom1);


                        break;
                    }
                case 5:
                    {
                        domains.Push(dom5);
                        domains.Push(dom4);
                        domains.Push(dom3);
                        domains.Push(dom2);
                        domains.Push(dom1);


                        break;
                    }
            }
            return domains;
        }
        public virtual IEnumerable<int> Iterate()
        {
            for (int i = 0; i < _set._sortedElements.Count; i++)
            {
                if (SetIndexTo(i))
                {
                    yield return i;
                }
            }
        }
        private IEnumerable<int> EnumDomains(Stack<FCS_Domain> domains)
        {
            FCS_Domain d = domains.Pop();
            if (domains.Count > 0)
            {
                foreach (int k in d.Iterate())
                {
                    foreach (int j in EnumDomains(domains))
                    {
                        yield return j;
                    }
                }
            }
            else
            {
                foreach (int k in d.Iterate())
                {
                    if (_cond.Action())
                    {

                        yield return 0;
                    }
                }

            }
            domains.Push(d);
        }

        public virtual FCS_Domain SuchThat(FCS_Condition_bool cond)
        {
            _cond = cond;
            return this;
        }

        public virtual int Eval()
        {
            switch (nDim)
            {
                case 1:
                    {
                        return _set[dom1.Eval()];
                    }
                case 2:
                    {
                        return _set[dom1.Eval(), dom2.Eval()];
                    }
                case 3:
                    {
                        return _set[dom1.Eval(), dom2.Eval(), dom3.Eval()];
                    }
                case 4:
                    {
                        return _set[dom1.Eval(), dom2.Eval(), dom3.Eval(), dom4.Eval()];
                    }
                case 5:
                    {
                        return _set[dom1.Eval(), dom2.Eval(), dom3.Eval(), dom4.Eval(), dom5.Eval()];
                    }
                default:
                    {
                        throw new Exception("wrong dimension on domain");
                    }
            }
        }
        public int Append()
        {
            switch (nDim)
            {
                case 1:
                    {
                        return _set.Add(dom1.Eval());
                    }
                case 2:
                    {
                        return _set.Add(dom1.Eval(), dom2.Eval());
                    }
                case 3:
                    {
                        return _set.Add(dom1.Eval(), dom2.Eval(), dom3.Eval());
                    }
                case 4:
                    {
                        return _set.Add(dom1.Eval(), dom2.Eval(), dom3.Eval(), dom4.Eval());
                    }
                case 5:
                    {
                        return _set.Add(dom1.Eval(), dom2.Eval(), dom3.Eval(), dom4.Eval(), dom5.Eval());
                    }
                default:
                    {
                        throw new Exception("wrong dimension on domain");
                    }
            }
        }

        public virtual string GetCurrentString()
        {
            switch (nDim)
            {
                case 1:
                    {
                        return _set.GetCurrentString(new FCS_uplet(dom1.Eval()));
                    }
                case 2:
                    {
                        return _set.GetCurrentString(new FCS_uplet(dom1.Eval(), dom2.Eval()));
                    }
                case 3:
                    {
                        return _set.GetCurrentString(new FCS_uplet(dom1.Eval(), dom2.Eval(), dom3.Eval()));
                    }
                case 4:
                    {
                        return _set.GetCurrentString(new FCS_uplet(dom1.Eval(), dom2.Eval(), dom3.Eval(), dom4.Eval()));
                    }
                case 5:
                    {
                        return _set.GetCurrentString(new FCS_uplet(dom1.Eval(), dom2.Eval(), dom3.Eval(), dom4.Eval(), dom5.Eval()));
                    }
                default:
                    {
                        throw new Exception("wrong dimension on domain");
                    }
            }

        }

        public virtual bool SetIndexTo(int i)
        {
            FCS_uplet up = _set._sortedElements[i];
            switch (nDim)
            {
                case 1:
                    {
                        return dom1.SetIndexTo(up.i1) && _cond.Action();
                        
                    }
                case 2:
                    {
                        return dom1.SetIndexTo(up.i1) &&
                        dom2.SetIndexTo(up.i2) && _cond.Action();
                        
                    }
                case 3:
                    {
                        return dom1.SetIndexTo(up.i1) &&
                        dom2.SetIndexTo(up.i2) &&
                        dom3.SetIndexTo(up.i3) && _cond.Action();
                        
                    }
                case 4:
                    {
                        return dom1.SetIndexTo(up.i1) &&
                        dom2.SetIndexTo(up.i2) &&
                        dom3.SetIndexTo(up.i3) &&
                        dom4.SetIndexTo(up.i4) && _cond.Action();
                        
                    }
                case 5:
                    {
                        return dom1.SetIndexTo(up.i1) &&
                        dom2.SetIndexTo(up.i2) &&
                        dom3.SetIndexTo(up.i3) &&
                        dom4.SetIndexTo(up.i4) &&
                        dom5.SetIndexTo(up.i5) && _cond.Action();
                        
                    }
                default:
                    {
                        throw new Exception();
                    }
            }
        }


    }
}
