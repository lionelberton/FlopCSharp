using System;
using System.Collections.Generic;
using System.Text;
using PR.Flopcsharp;

namespace TestFlopcsharp.Exemples
{
    class mod_assign
    {
        /// <summary>
        /// Lionel BERTON (2008).
        /// Exemple adapté du problème "assign" de GLPK4.26
        /// </summary>
        public static void Run()
        {
            int m=8, n=8;
            FCS_Set I = new FCS_Set();
            I.Add(m);
            FCS_Set J = new FCS_Set();
            J.Add(n);
            FCS_Data c = new FCS_Data(I, J);
            
            c.FillMatrix(
                "0  1  2  3  4  5  6  7",
            @"0  13 21 20 12  8 26 22 11
              1  12 36 25 41 40 11  4  8
              2  35 32 13 36 26 21 13 37
              3  34 54  7  8 12 22 11 40
              4  21  6 45 18 24 34 12 48
              5  42 19 39 15 14 16 28 46
              6  16 34 38  3 34 40 22 24
              7  26 20  5 17 45 31 37 43" );


            FCS_Variable x = new FCS_Variable(I, J);

            FCS_Constraint phi = new FCS_Constraint(I);
            FCS_Constraint psi = new FCS_Constraint(J);

            FCS_Index i = new FCS_Index();
            FCS_Index j = new FCS_Index();

            FCS_Model model = new FCS_Model();

            phi[i] = model.Sum(J[j], x[i, j]) <= 1;
            psi[j] = model.Sum(I[i], x[i, j]) == 1;

            model.AddVariable(x);
            model.AddConstraint(phi);
            model.AddConstraint(psi);

            model.CreateProblem();
            model.Objective(model.Sum(I[i] * J[j], c[i, j] * x[i, j]));
            model.Minimise();

            //printf "\n";
            Console.Out.WriteLine("Agent  Task       Cost");
            foreach (int k in I[i].Iterate())
            {
                Console.Out.WriteLine("{0}      {1}       {2}", i.Index, model.Sum(J[j], j * x[i, j]).GetValue(),
                   model.Sum(J[j], c[i, j] * x[i, j]).GetValue());
            }
            Console.Out.WriteLine("----------------------");
            double objval=model.Sum(I[i] * J[j], c[i, j] * x[i, j]).GetValue();
            Console.Out.WriteLine("    Total: {0:G}", objval);
            new EnhancedConsole().WriteTest(objval == 76);
            Console.Out.WriteLine("");

        }
    }
}
