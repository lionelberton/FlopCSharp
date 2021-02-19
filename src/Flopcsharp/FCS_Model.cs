using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using GlpkSharp;

namespace PR.Flopcsharp
{
    public class FCS_Model
    {
        List<FCS_Constraint> _constraints;

        LPProblem lp;
        bool _init = false;

        public FCS_Model()
        {
            _constraints = new List<FCS_Constraint>();
            lp = new LPProblem();
        }
        public void Objective(FCS_Expression_Linear obj)
        {
            FCS_MRow_Obj objrow = new FCS_MRow_Obj(this);
            obj.Action(objrow, 1);
            objrow.WriteCoeff();
        }

        public void Minimise()
        {
            if (!_init)
            {
                Console.Out.WriteLine("Problem not created yet!");
                return;
            }
            lp.ObjectiveDirection = OptimisationDirection.MINIMISE;
            var iocp = lp.IntegerOptControlParams;
            iocp.presolve = GLPOnOff.On;
            lp.IntegerOptControlParams = iocp;
            lp.SolveInteger();
        }
        public void Maximise()
        {
            if (!_init)
            {
                Console.Out.WriteLine("Problem not created yet!");
                return;
            }

            lp.ObjectiveDirection = OptimisationDirection.MAXIMISE;
            var iocp = lp.IntegerOptControlParams;
            iocp.presolve = GLPOnOff.On;
            lp.IntegerOptControlParams = iocp;
            lp.SolveInteger();
        }

        public int NewRow()
        {
            return lp.AddRows(1);
        }
        public int NewCol()
        {
            return lp.AddCols(1);
        }

        public void AddConstraint(FCS_Constraint c)
        {
            _constraints.Add(c);
        }
        public void AddVariable(FCS_Variable v)
        {
            v._model = this;
        }

        public void CreateProblem()
        {
            foreach (FCS_Constraint c in _constraints)
            {
                c.CreateConstraints(this);
                c.WriteCoeff();
            }
            _init = true;
        }

        public FCS_Expression_Sum Sum(FCS_Domain dom, FCS_Expression_Linear exp)
        {
            return new FCS_Expression_Sum(dom, exp);
        }

        public void WriteLP()
        {
            lp.WriteCPLEX("mat.lp");
        }
        public void WriteMPS()
        {
            lp.WriteMPS("mat.mps", MPSFORMAT.FreeMPS);
        }
        public void WriteSOL()
        {
            lp.WriteSol("mat.sol");
        }

        public void SetMatRow(int row, int[] ind, double[] val)
        {
            lp.SetMatRow(row, ind, val);
        }
        public void SetRowBounds(int row, BOUNDSTYPE boundstype, double lb, double ub)
        {
            lp.SetRowBounds(row, boundstype, lb, ub);
        }
        public void SetColKind(int col, COLKIND type)
        {
            lp.SetColKind(col, type);
        }
        public void SetRowName(int row, string name)
        {
            lp.SetRowName(row, name);
        }
        public void SetColName(int col, string name)
        {
            lp.SetColName(col, name);
        }
        public void SetObjCoef(int col, double v)
        {
            lp.SetObjCoef(col, v);
        }
        public void SetColBounds(int col, BOUNDSTYPE type, double lb, double ub)
        {
            lp.SetColBounds(col, type, lb, ub);
        }

        public void Dispose()
        {
            lp.Destroy();
        }

        public double GetMipColVal(int j)
        {
            return lp.GetMIPColVal(j);
        }

        public double GetLPObjVal()
        {
            return lp.GetObjectiveValue();
        }
        public double GetMIPObjVal()
        {
            return lp.GetMIPObjectiveValue();
        }

    }
}
