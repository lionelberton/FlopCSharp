using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PR.Flopcsharp
{
    public class FCS_Data
    {
        private FCS_Subset _s1;
        private int nSet;
        //private FCS_Domain _dom1;
        private Dictionary<int, double> _data;

        #region constructeurs
        private FCS_Data()
        {
            _data = new Dictionary<int, double>();
        }
        public FCS_Data(FCS_Subset S1):this()
        {
            _s1 = new FCS_Subset(S1);
            nSet = 1;

        }
        public FCS_Data(FCS_Subset S1, FCS_Subset S2)
            : this()
        {
            _s1 = new FCS_Subset(S1, S2);
            nSet = 2;
        }
        public FCS_Data(FCS_Subset S1, FCS_Subset S2,FCS_Subset S3)
            : this()
        {
            _s1 = new FCS_Subset(S1, S2,S3);
            nSet = 3;
        }
        public FCS_Data(FCS_Subset S1, FCS_Subset S2, FCS_Subset S3,FCS_Subset S4)
            : this()
        {
            _s1 = new FCS_Subset(S1, S2, S3,S4);
            nSet = 4;
        }
        public FCS_Data(FCS_Subset S1, FCS_Subset S2, FCS_Subset S3, FCS_Subset S4,FCS_Subset S5)
            : this()
        {
            _s1 = new FCS_Subset(S1, S2, S3, S4,S5);
            nSet = 5;
        }
        #endregion

        #region indexers on domains or fcs_index
        public FCS_Expression_Data this[FCS_Domain ind1]
        {
            get
            {
                //_dom1 = ind1;
                return new FCS_Expression_Data(this, ind1);
            }
        }
        public FCS_Expression_Data this[IFCS_Domain k1]
        {
            get
            {
                return this[_s1[k1]];
            }
        }
        public FCS_Expression_Data this[IFCS_Domain k1, IFCS_Domain k2]
        {
            get
            {
                return this[_s1[k1, k2]];
            }
        }
        public FCS_Expression_Data this[IFCS_Domain k1, IFCS_Domain k2, IFCS_Domain k3]
        {
            get
            {
                return this[_s1[k1, k2, k3]];
            }
        }
        public FCS_Expression_Data this[IFCS_Domain k1, IFCS_Domain k2, IFCS_Domain k3, IFCS_Domain k4]
        {
            get
            {
                return this[_s1[k1, k2, k3, k4]];
            }
        }
        public FCS_Expression_Data this[IFCS_Domain k1, IFCS_Domain k2, IFCS_Domain k3, IFCS_Domain k4, IFCS_Domain k5]
        {
            get
            {
                return this[_s1[k1, k2, k3, k4, k5]];
            }
        }
        #endregion

        public double Eval(int j)
        {
            if (j == -1)
            {
                return 0;
            }
            if (!_data.ContainsKey(j))
            {
                return 0;
            }
            return _data[j];
        }

        public static implicit operator FCS_Data(FCS_Subset s){
            FCS_Data d = new FCS_Data();
            d._s1 = s;
            d.nSet = s.nSet;
            return d;
        }
        #region indexeurs int
        public double this[int i1]
        {
            get
            {
                //FCS_uplet up = new FCS_uplet(i1, i2, i3, i4, i5);

                return _data[_s1[i1]];
            }
            set
            {
                int j = _s1.Add(i1);
                Add(j, value);
            }
        }
        public double this[int i1, int i2]
        {
            get
            {
                //FCS_uplet up = new FCS_uplet(i1, i2, i3, i4, i5);
                
                return _data[_s1[i1, i2]];
            }
            set
            {
                int j = _s1.Add(i1, i2);
                Add(j, value); ;
            }
        }
        public double this[int i1, int i2, int i3]
        {
            get
            {
                //FCS_uplet up = new FCS_uplet(i1, i2, i3, i4, i5);

                return _data[_s1[i1, i2, i3]];
            }
            set
            {
                int j = _s1.Add(i1, i2, i3);
                Add(j, value);
            }
        }
        public double this[int i1, int i2, int i3, int i4]
        {
            get
            {
                //FCS_uplet up = new FCS_uplet(i1, i2, i3, i4, i5);

                return _data[_s1[i1, i2, i3, i4]];
            }
            set
            {
                int j = _s1.Add(i1, i2, i3, i4);
                Add(j, value);
            }
        }
        public double this[int i1, int i2, int i3, int i4, int i5]
        {
            get
            {
                //FCS_uplet up = new FCS_uplet(i1, i2, i3, i4, i5);

                return _data[_s1[i1, i2, i3, i4, i5]];
            }
            set
            {
                int j=_s1.Add(i1, i2, i3, i4, i5);
                Add(j, value);
            }
        }
        #endregion

        private void Add(int j, double v)
        {
            if (!_data.ContainsKey(j))
            {
                _data.Add(j, v);
            }
            _data[j] = v;
        }

        public void Dump()
        {
            foreach (int i in _data.Keys )
            {
                Console.Out.WriteLine(_s1.GetCurrentString(i) +"  " +_data[i]);
            }
        }
        /// <summary>
        /// Fill the data with a matrix in this format.
        ///    c1  c2  c3  ...
        /// r1 v11 v12 v13 ...
        /// r2 v21 v22 v23 ...
        /// ...
        /// 
        /// Items are separated by some spaces or tabs
        /// rows are separated by a newline.
        /// The first row is for columns headers and has one item less than the others.
        /// 
        /// </summary>
        /// <param name="rowHeader"></param>
        /// <param name="matrix"></param>
        public void FillMatrix(string rowHeader, string matrix)
        {
            string[] colH = Regex.Split(rowHeader, "\\s+");
            string[] mats = Regex.Split(matrix, "\\s*\\n\\s*");
            foreach (string mat in mats)
            {
                string[] rows = Regex.Split(mat, "\\s+");
                for(int i=0;i<colH.Length;i++)
                {
                    this[int.Parse(rows[0]), int.Parse(colH[i])] = double.Parse(rows[i + 1]);
                }
            }
        }

        public void FillTabular(FCS_Subset S,string table)
        {
            table = table.Trim();
            string[] rows = Regex.Split(table, "\\s*\\n\\s*");
            foreach (string row in rows)
            {
                string[] cols = Regex.Split(row, "\\s+");
                int c1=int.Parse(cols[0])-1;
                int c2=int.Parse(cols[1])-1;
                S.Add(c1,c2);
                this[S[c1,c2]] = double.Parse(cols[2]);
            }
        }

    }
}
