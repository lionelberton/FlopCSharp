using System;
using System.Collections.Generic;
using System.Text;
using PR.Flopcsharp;

namespace TestFlopcsharp.Exemples
{
    /// <summary>
    /// Lionel BERTON (2008).
    /// Exemple adapté du problème "maxcut" de GLPK4.26
    /// </summary>
    class mod_maxcut
    {
        public static void Run()
        {
            int n = 15;

            FCS_Set V = new FCS_Set();
            V.Add(n);
            FCS_Subset E = new FCS_Subset(V, V);

            FCS_Data w = new FCS_Data(E);
            #region data
            string tabdata = @"1 2 1
1 5 1
2 3 1
2 6 1
3 4 1
3 8 1
4 9 1
5 6 1
5 7 1
6 8 1
7 8 1
7 12 1
8 9 1
8 12 1
9 10 1
9 14 1
10 11 1
10 14 1
11 15 1
12 13 1
13 14 1
14 15 1";
            #endregion
            w.FillTabular(E,tabdata);

            FCS_Variable x = new FCS_Variable(V);
            x.binary = true;

            FCS_Variable t = new FCS_Variable(E);
            t.binary = true;

            FCS_Index i = new FCS_Index();
            FCS_Index j = new FCS_Index();

            FCS_Model model = new FCS_Model();

            FCS_Constraint xor = new FCS_Constraint(E);
            xor[E[i, j]] = 0<= x[i] + x[j] - 2 * t[E[i, j]] <= 1;

            
            model.AddConstraint(xor);
            model.AddVariable(x);
            model.AddVariable(t);
            model.Objective(
                model.Sum(
                    E[i, j], w[E[i, j]] * (x[i] + x[j] - 2 * t[E[i, j]])
                )
            );
            model.CreateProblem();
            model.Maximise();

            double objval = model.GetMIPObjVal();
            Console.Out.WriteLine("Optimal cut has weight {0}",objval);
            new EnhancedConsole().WriteTest(objval == 20);
        }
    }
}
