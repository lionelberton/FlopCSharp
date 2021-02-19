using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public class FCS_Constraint
    {
        private FCS_Subset _s1;

        private int nSet = 0;
        private FCS_Expression_bound cons;

        private FCS_Domain _dom1;
        public string _name;

        private Dictionary<int, FCS_MRow> _constraints;

        #region constructors
        private FCS_Constraint()
        {
            _constraints = new Dictionary<int, FCS_MRow>();
        }
        public FCS_Constraint(FCS_Subset S1):this()
        {
            _s1 = new FCS_Subset(S1);
            nSet = 1;
        }
        public FCS_Constraint(FCS_Subset S1, FCS_Subset S2)
            : this()
        {
            _s1 = new FCS_Subset(S1, S2);
            //_s2 = S2;
            nSet = 2;
        }
        public FCS_Constraint(FCS_Subset S1, FCS_Subset S2, FCS_Subset S3)
            : this()
        {
            _s1 = new FCS_Subset(S1, S2,S3);
            //_s2 = S2;
            nSet = 3;
        }
        public FCS_Constraint(FCS_Subset S1, FCS_Subset S2, FCS_Subset S3, FCS_Subset S4)
            : this()
        {
            _s1 = new FCS_Subset(S1, S2, S3,S4);
            //_s2 = S2;
            nSet = 4;
        }
        public FCS_Constraint(FCS_Subset S1, FCS_Subset S2, FCS_Subset S3, FCS_Subset S4, FCS_Subset S5)
            : this()
        {
            _s1 = new FCS_Subset(S1, S2, S3, S4,S5);
            //_s2 = S2;
            nSet = 5;
        }
        #endregion

        #region Indexers with domains
        private FCS_Expression_bound this[FCS_Domain dom1]
        {
            set
            {
                _dom1 = dom1;
                cons = value;
            }
        }
        #endregion

        #region indexers on domains or fcs_index
        public FCS_Expression_bound this[IFCS_Domain k1]
        {
            set
            {
                this[_s1[k1]] = value;
            }
        }
        public FCS_Expression_bound this[IFCS_Domain k1, IFCS_Domain k2]
        {
            set
            {
                this[_s1[k1, k2]] = value;
            }
        }
        public FCS_Expression_bound this[IFCS_Domain k1, IFCS_Domain k2, IFCS_Domain k3]
        {
            set
            {
                this[_s1[k1, k2, k3]] = value;
            }
        }
        public FCS_Expression_bound this[IFCS_Domain k1, IFCS_Domain k2, IFCS_Domain k3, IFCS_Domain k4]
        {
            set
            {
                this[_s1[k1, k2, k3, k4]] = value;
            }
        }
        public FCS_Expression_bound this[IFCS_Domain k1, IFCS_Domain k2, IFCS_Domain k3, IFCS_Domain k4, IFCS_Domain k5]
        {
            set
            {
                this[_s1[k1, k2, k3, k4, k5]] = value;
            }
        }
        #endregion


        public void CreateConstraints(FCS_Model m)
        {
            foreach (int i in _dom1.IterateCreate())
            {
                FCS_MRow row = new FCS_MRow(m,_name +_dom1.GetCurrentString());

                _constraints.Add(i, row);
                cons.Action(row,1);

            }
        }
        public void WriteCoeff()
        {
            foreach(FCS_MRow row in  _constraints.Values){
                row.WriteCoeff();
            }
        }
        public void Dump()
        {
            _s1.Dump();
        }

    }
}
