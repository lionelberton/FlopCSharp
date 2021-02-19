using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public class FCS_Variable
    {
        protected FCS_Subset _s1;
        protected int nSet = 0;

        protected Dictionary<int, FCS_MCol> _variables;

        public FCS_Model _model;
        public string _name;

        public bool binary = false;

        private FCS_Variable()
        {
            _variables = new Dictionary<int, FCS_MCol>();
            //_dom1 = new FCS_BaseDomain();
            //_dom2 = new FCS_BaseDomain();
        }
        public FCS_Variable(FCS_Subset S1):this()
        {
            _s1 = new FCS_Subset(S1);
            nSet = 1;

        }
        public FCS_Variable(FCS_Subset S1, FCS_Subset S2)
            : this()
        {
            _s1 = new FCS_Subset(S1, S2);
            nSet = 2;
        }
        public FCS_Variable(FCS_Subset S1, FCS_Subset S2,FCS_Subset S3)
            : this()
        {
            _s1 = new FCS_Subset(S1, S2,S3);
            nSet = 3;
        }
        public FCS_Variable(FCS_Subset S1, FCS_Subset S2, FCS_Subset S3,FCS_Subset S4)
            : this()
        {
            _s1 = new FCS_Subset(S1, S2, S3,S4);
            nSet = 4;
        }
        public FCS_Variable(FCS_Subset S1, FCS_Subset S2, FCS_Subset S3, FCS_Subset S4,FCS_Subset S5)
            : this()
        {
            _s1 = new FCS_Subset(S1, S2, S3, S4,S5);
            nSet = 5;
        }


        private FCS_Expression_Variable this[FCS_Domain ind1]
        {
            get
            {
                return new FCS_Expression_Variable(this,ind1);
            }
        }

        #region indexers on domains or fcs_index
        public FCS_Expression_Variable this[IFCS_Domain k1]
        {
            get
            {
                return this[_s1[k1]];
            }
        }
        public FCS_Expression_Variable this[IFCS_Domain k1, IFCS_Domain k2]
        {
            get
            {
                return this[_s1[k1, k2]];
            }
        }
        public FCS_Expression_Variable this[IFCS_Domain k1, IFCS_Domain k2, IFCS_Domain k3]
        {
            get
            {
                return this[_s1[k1, k2, k3]];
            }
        }
        public FCS_Expression_Variable this[IFCS_Domain k1, IFCS_Domain k2, IFCS_Domain k3, IFCS_Domain k4]
        {
            get
            {
                return this[_s1[k1, k2, k3, k4]];
            }
        }
        public FCS_Expression_Variable this[IFCS_Domain k1, IFCS_Domain k2, IFCS_Domain k3, IFCS_Domain k4, IFCS_Domain k5]
        {
            get
            {
                return this[_s1[k1, k2, k3, k4, k5]];
            }
        }
        #endregion

        public int eval(int j)
        {
            if (j == -1)
            {
                return -1;
            }
            FCS_MCol newindex;
            //int j = _dom1.Append();
            if (!_variables.ContainsKey(j))
            {
                if (_model == null)
                {
                    throw new Exception("Variable Array does not exist in the model. Use Model.AddVariable()");
                }
                newindex = new FCS_MCol(_model.NewCol());
                _model.SetColName(newindex._col,_name+ _s1.GetCurrentString(j));
                _model.SetColBounds(newindex._col, GlpkSharp.BOUNDSTYPE.Lower, 0, 0);
                if (binary)
                {
                    _model.SetColKind(newindex._col, GlpkSharp.COLKIND.Binary);
                }
                _variables.Add(j,newindex);
            }
            else
            {
                newindex = _variables[j];
            }
            return newindex._col;
        }

        public void Dump()
        {
            _s1.Dump();
        }
        public FCS_MCol GetMCol(int j)
        {
            return _variables[j];
        }

        public double GetValue(int j)
        {
            return _model.GetMipColVal(GetMCol(j)._col);
        }
    }
}
