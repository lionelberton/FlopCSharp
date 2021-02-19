using System;
using System.Collections.Generic;
using System.Text;
using PR.Flopcsharp;

namespace TestFlopcsharp.Exemples
{
    /// <summary>
    /// Lionel BERTON (2008).
    /// Exemple adapté du problème bpp de GLPK4.26
    /// </summary>
    class mod_bpp
    {
        /// <summary>
        /// Bin Packing problem.
        /// </summary>
        public static void Run()
        {
            int m = 6; //number of items
            double c = 100; //bin capacity

            FCS_Set I = new FCS_Set();//items
            I.Add(m);

            FCS_Data w = new FCS_Data(I); //item's weight
            w[0] = 50;
            w[1] = 60;
            w[2] = 30;
            w[3] = 70;
            w[4] = 50;
            w[5] = 40;

            FCS_Index i = new FCS_Index();
            FCS_Index j = new FCS_Index();

            //on calcule une estimation du nombre de bin nécessaire par une heuristique.
            double usedcapa = c;
            int maxbin = 1;
            foreach (int k in (I[i]).Iterate())
            {
                if (w.Eval(i.Index) <= usedcapa)
                {
                    usedcapa -= w.Eval(i.Index);
                }
                else
                {
                    usedcapa = c;
                    maxbin++;
                }
            }

            FCS_Set J = new FCS_Set();
            J.Add(maxbin);

            FCS_Variable x = new FCS_Variable(I, J);
            FCS_Variable used = new FCS_Variable(J);
            used.binary = true;

            FCS_Constraint one = new FCS_Constraint(I);
            FCS_Constraint lim = new FCS_Constraint(J);
            
            FCS_Model model=new FCS_Model();
            one[i] = model.Sum(J[j], x[i, j]) == 1;//an item in a single bin.
            lim[j] = model.Sum(I[i], w[i] * x[i, j])-c * used[j] <=0 ;

            model.AddConstraint(one);
            model.AddConstraint(lim);
            model.AddVariable(x);
            model.AddVariable(used);
            model.Objective(model.Sum(J[j], used[j]));
            model.CreateProblem();
            model.Minimise();
            new EnhancedConsole().WriteTest(model.GetMIPObjVal() == 3);//objective must be 3


        }


    }
}
