using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public class FCS_Subset
    {
        #region//gestion des erreurs
        protected string _lastError;

        public virtual string GetLastError()
        {
            return _lastError;
        }
        #endregion

        FCS_Subset _s1;
        FCS_Subset _s2;
        FCS_Subset _s3;
        FCS_Subset _s4;
        FCS_Subset _s5;
        public string Name="<noname>";
        private Dictionary<FCS_uplet, int> _elements;
        internal List<FCS_uplet> _sortedElements;

        internal int nSet = 1;

        #region Add elements
        public virtual int Add(int i1)
        {
            return Add(i1, 0, 0, 0, 0);
        }
        public int Add(int i1, int i2)
        {
            return Add(i1, i2, 0, 0, 0);
        }
        public int Add(int i1, int i2, int i3)
        {
            return Add(i1, i2, i3, 0, 0);
        }
        public int Add(int i1, int i2, int i3, int i4)
        {
            return Add(i1, i2, i3, i4, 0);
        }
        public int Add(int i1, int i2, int i3, int i4, int i5)
        {
            FCS_uplet up = new FCS_uplet(i1, i2, i3, i4, i5);
            if (!up.Valid())
            {
                return -1;
            }
            if (!_elements.ContainsKey(up))
            {
                _elements.Add(up, _sortedElements.Count);
                _sortedElements.Add(up);
                return _sortedElements.Count - 1;
            }
            else
            {
                return _elements[up];
            }

        }
        #endregion

        public virtual object Get(int i)
        {
            return _sortedElements[i];
        }


        #region constructors
        protected FCS_Subset()
        {
            _elements = new Dictionary<FCS_uplet, int>();
            _sortedElements = new List<FCS_uplet>();
        }
        public FCS_Subset(FCS_Subset s1)
            : this()
        {
            _s1 = s1;
            nSet = 1;
        }
        public FCS_Subset(FCS_Subset s1, FCS_Subset s2)
            : this()
        {
            _s1 = s1;
            _s2 = s2;
            nSet = 2;
        }
        public FCS_Subset(FCS_Subset s1, FCS_Subset s2, FCS_Subset s3)
            : this()
        {
            _s1 = s1;
            _s2 = s2;
            _s3 = s3;
            nSet = 3;
        }
        public FCS_Subset(FCS_Subset s1, FCS_Subset s2, FCS_Subset s3, FCS_Subset s4)
            : this()
        {
            _s1 = s1;
            _s2 = s2;
            _s3 = s3;
            _s4 = s4;
            nSet = 4;
        }
        public FCS_Subset(FCS_Subset s1, FCS_Subset s2, FCS_Subset s3, FCS_Subset s4, FCS_Subset s5)
            : this()
        {
            _s1 = s1;
            _s2 = s2;
            _s3 = s3;
            _s4 = s4;
            _s5 = s5;
            nSet = 5;
        }
        #endregion


        public int this[string t1, string t2]
        {
            get
            {
                if (nSet != 2)
                {
                    throw new Exception();
                }
                if (_s1.nSet != 1 || _s2.nSet != 1)
                {
                    throw new Exception();
                }
                FCS_uplet up = new FCS_uplet(_s1[t1], _s2[t2], 0, 0, 0);
                return _elements[up];
            }
        }
        public virtual int this[string t1]
        {
            get
            {
                return 0;
            }
        }

        //private FCS_Domain this[FCS_Domain dom1, FCS_Domain dom2]
        //{
        //    get
        //    {
        //        return new FCS_Domain(this, dom1, dom2);
        //    }
        //}
        #region indexeurs par IFCS_Domain
        public FCS_Domain this[IFCS_Domain i1]
        {
            get
            {
                if (nSet != 1)
                {
                    throw new Exception();
                }


                return new FCS_Domain(this, GetDomain(_s1, i1));
            }
        }
        public FCS_Domain this[IFCS_Domain i1, IFCS_Domain i2]
        {
            get
            {
                if (nSet != 2)
                {
                    throw new Exception();
                }


                return new FCS_Domain(this, GetDomain(_s1, i1), GetDomain(_s2, i2));
            }
        }
        public FCS_Domain this[IFCS_Domain i1, IFCS_Domain i2, IFCS_Domain i3]
        {
            get
            {
                if (nSet != 3)
                {
                    throw new Exception();
                }


                return new FCS_Domain(this, GetDomain(_s1, i1), GetDomain(_s2, i2), GetDomain(_s3, i3));
            }
        }
        public FCS_Domain this[IFCS_Domain i1, IFCS_Domain i2, IFCS_Domain i3, IFCS_Domain i4]
        {
            get
            {
                if (nSet != 4)
                {
                    throw new Exception();
                }


                return new FCS_Domain(this, GetDomain(_s1, i1), GetDomain(_s2, i2), GetDomain(_s3, i3), GetDomain(_s4, i4));
            }
        }
        public FCS_Domain this[IFCS_Domain i1, IFCS_Domain i2, IFCS_Domain i3, IFCS_Domain i4, IFCS_Domain i5]
        {
            get
            {
                if (nSet != 5)
                {
                    throw new Exception();
                }


                return new FCS_Domain(this, GetDomain(_s1, i1), GetDomain(_s2, i2), GetDomain(_s3, i3), GetDomain(_s4, i4), GetDomain(_s5, i5));
            }
        }
        #endregion

        private FCS_Domain GetDomain(FCS_Subset s, IFCS_Domain i)
        {
            FCS_Domain d1;
            if (i is FCS_Index)
            {
                d1 = new FCS_BaseDomain(s, (FCS_Index)i);
            }
            else
            {
                d1 = (FCS_Domain)i;
            }
            return d1;
        }
        public virtual IEnumerable<FCS_uplet> Indexes(FCS_Index k)
        {
            foreach (KeyValuePair<FCS_uplet, int> pair in _elements)
            {
                k.Index = pair.Value;
                yield return pair.Key;
            }
        }
        public IEnumerable<FCS_uplet> Uplets()
        {
            foreach (FCS_uplet up in _sortedElements)
            {
                yield return up;
            }
        }
        #region indexeurs par int
        public int this[int i1]
        {
            get
            {
                if (nSet != 1)
                {
                    throw new Exception();
                }
                //FCS_uplet up = new FCS_uplet(i1, i2, i3, i4, i5);
                FCS_uplet up = new FCS_uplet(i1, 0, 0, 0, 0);
                //Console.Out.WriteLine("in " + _name + " : " + up.ToString());
                if (_elements.ContainsKey(up))
                {
                    return _elements[up];
                }
                else
                {
                    return -1;
                }
            }
        }
        public int this[int i1, int i2]
        {
            get
            {
                if (nSet != 2)
                {
                    throw new Exception();
                }
                //FCS_uplet up = new FCS_uplet(i1, i2, i3, i4, i5);
                FCS_uplet up = new FCS_uplet(i1, i2, 0, 0, 0);
                //Console.Out.WriteLine("in " + _name + " : " + up.ToString());
                if (_elements.ContainsKey(up))
                {
                    return _elements[up];
                }
                else
                {
                    return -1;
                }
            }
        }
        public int this[int i1, int i2, int i3]
        {
            get
            {
                if (nSet != 3)
                {
                    throw new Exception();
                }
                //FCS_uplet up = new FCS_uplet(i1, i2, i3, i4, i5);
                FCS_uplet up = new FCS_uplet(i1, i2, i3, 0, 0);
                //Console.Out.WriteLine("in " + _name + " : " + up.ToString());
                if (_elements.ContainsKey(up))
                {
                    return _elements[up];
                }
                else
                {
                    return -1;
                }
            }
        }
        public int this[int i1, int i2, int i3, int i4]
        {
            get
            {
                if (nSet != 4)
                {
                    throw new Exception();
                }
                //FCS_uplet up = new FCS_uplet(i1, i2, i3, i4, i5);
                FCS_uplet up = new FCS_uplet(i1, i2, i3, i4, 0);
                //Console.Out.WriteLine("in " + _name + " : " + up.ToString());
                if (_elements.ContainsKey(up))
                {
                    return _elements[up];
                }
                else
                {
                    return -1;
                }
            }
        }
        public int this[int i1, int i2, int i3, int i4, int i5]
        {
            get
            {
                if (nSet != 5)
                {
                    throw new Exception();
                }
                //FCS_uplet up = new FCS_uplet(i1, i2, i3, i4, i5);
                FCS_uplet up = new FCS_uplet(i1, i2, i3, i4, i5);
                //Console.Out.WriteLine("in " + _name + " : " + up.ToString());
                if (_elements.ContainsKey(up))
                {
                    return _elements[up];
                }
                else
                {
                    return -1;
                }
            }
        }
        #endregion

        public virtual void Dump()
        {
            foreach (FCS_uplet up in _sortedElements)
            {
                Console.Out.WriteLine(Name +" "+ GetCurrentString(up)); 
            }
        }

        public virtual string GetCurrentString(int upo)
        {
            return GetCurrentString(_sortedElements[upo]);
        }
        public virtual string GetCurrentString(FCS_uplet upo)
        {
            FCS_uplet up = upo;
            string curString="";
            switch (nSet)
            {

                case 1:
                    {
                        curString=
    "(" + _s1.GetCurrentString(up.i1)
    + ")"
;
                        break;
                    }
                case 2:
                    {
                        curString=
    "(" + _s1.GetCurrentString(up.i1) + "," +
    _s2.GetCurrentString(up.i2)
    + ")"
;
                        break;
                    }
                case 3:
                    {
                        curString=
    "(" + _s1.GetCurrentString(up.i1) + "," +
    _s2.GetCurrentString(up.i2) + "," +
    _s3.GetCurrentString(up.i3)
    + ")"
;
                        break;
                    }
                case 4:
                    {
                        curString=
    "(" + _s1.GetCurrentString(up.i1) + "," +
    _s2.GetCurrentString(up.i2) + "," +
    _s3.GetCurrentString(up.i3) + "," +
    _s4.GetCurrentString(up.i4)
    + ")"
;
                        break;
                    }
                case 5:
                    {
                        curString=
    "(" + _s1.GetCurrentString(up.i1) + "," +
    _s2.GetCurrentString(up.i2) + "," +
    _s3.GetCurrentString(up.i3) + "," +
    _s4.GetCurrentString(up.i4) + "," +
    _s5.GetCurrentString(up.i5)
    + ")"
;
                        break;
                    }

            }
            return curString;
        }

    }
}
