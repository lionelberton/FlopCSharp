using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public class FCS_MRow
    {
        double _lbound;
        double _ubound;
        int _row;
        protected FCS_Model _model;
        protected Dictionary<int, double> _rowCoeff;
        string _rowname;
        public FCS_MRow(FCS_Model m,string name)
        {
            //_row = i;
            _model = m;
            _rowCoeff = new Dictionary<int, double>();
            _rowname = name;
        }
        public double this[int i]
        {
            set
            {
                if (i == -1)
                {
                    return;
                }
                //Console.Out.WriteLine(i + " = " + value);
                if (!_rowCoeff.ContainsKey(i))
                {
                    _rowCoeff.Add(i, value);
                }
                else
                {
                    _rowCoeff[i] += value;
                }
            }
            get
            {
                return _rowCoeff[i];
            }
        }

        public double LBound
        {
            set
            {
                //Console.Out.WriteLine(">= " + value);
                _lbound = value;
            }
            get
            {
                return _lbound;
            }
        }

        public double UBound
        {
            set
            {
                //Console.Out.WriteLine("<= " + value);
                _ubound = value;
            }
            get
            {
                return _ubound;
            }
        }
        public virtual bool WriteCoeff()
        {
            if (_rowCoeff.Count ==0)
            {
                return false;
            }
            _row = _model.NewRow();
            _model.SetRowName(_row, _rowname);
            int[] ind = new int[_rowCoeff.Count + 1];
            double[] val = new double[_rowCoeff.Count + 1];
            _rowCoeff.Keys.CopyTo(ind, 1);
            _rowCoeff.Values.CopyTo(val, 1);

            _model.SetMatRow(_row, ind, val);
            if (_lbound == double.NegativeInfinity && _ubound == double.PositiveInfinity)
            {
                _model.SetRowBounds(_row, GlpkSharp.BOUNDSTYPE.Free, _lbound, _ubound);
            }

            if (_lbound != double.NegativeInfinity && _ubound == double.PositiveInfinity)
            {
                _model.SetRowBounds(_row, GlpkSharp.BOUNDSTYPE.Lower, _lbound, _ubound);
            }
            if (_lbound == double.NegativeInfinity && _ubound != double.PositiveInfinity)
            {
                _model.SetRowBounds(_row, GlpkSharp.BOUNDSTYPE.Upper, _lbound, _ubound);
            }
            if (_lbound != double.NegativeInfinity && _ubound != double.PositiveInfinity && _ubound!=_lbound)
            {
                _model.SetRowBounds(_row, GlpkSharp.BOUNDSTYPE.Double, _lbound, _ubound);
            }
            if (_lbound != double.NegativeInfinity && _ubound != double.PositiveInfinity && _ubound == _lbound)
            {
                _model.SetRowBounds(_row, GlpkSharp.BOUNDSTYPE.Fixed, _lbound, _ubound);
            }
            return true;
        }
    }
}
