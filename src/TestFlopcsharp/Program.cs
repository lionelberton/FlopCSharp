using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PR.Flopcsharp;
using System.Threading;
using System.IO;
using TestFlopcsharp.Exemples;

namespace TestFlopcsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            mod_assign.Run();
            mod_bpp.Run();
            mod_tsp.Run();
            mod_maxcut.Run();

            //TestSET();
            //TestModel1();
            //Test1();
            Console.Out.WriteLine("Press a key to continue...");
            Console.In.Read();

        }
        public static void TestModel1()
        {
            FCS_Set S = new FCS_Set();
            FCS_Set P = new FCS_Set();
            FCS_Subset ST = new FCS_Subset(S, P);
            FCS_Data cost = new FCS_Data(S);
            FCS_Data tax = new FCS_Data(P);
            FCS_Variable qtrans = new FCS_Variable(S);
            FCS_Variable stock = new FCS_Variable(P);
            FCS_Constraint matbal = new FCS_Constraint(S);
            FCS_Model model = new FCS_Model();

            FCS_Index i=new FCS_Index();
            FCS_Index j=new FCS_Index("j");
            matbal[i] = model.Sum(P[j], qtrans[i] * tax[j]) <= cost[i];
            
            model.AddVariable(qtrans);
            model.AddConstraint(matbal);
            model.CreateProblem();

        }
        public static void TestSET()
        {
            TextWriter w=Console.Out;
            EnhancedConsole ec=new EnhancedConsole();
            FCS_Set SITES = new FCS_Set();
            SITES.Add();
            SITES.Add("yoy");
            SITES.Add(3);
            SITES.Dump();
            SITES.Add(1, 2); //il faut éviter ça!!!!
            ec.WriteTest(SITES.AddorGet("yoy")==1);
            foreach (string s in SITES.Elements())
            {
                w.WriteLine(s);
            }
            ec.WriteTest( SITES.Get(1)=="yoy");
            FCS_Index ind = new FCS_Index();
            ind = 1;
            ec.WriteTest(SITES.Get(ind) == "yoy");
            ec.WriteTest( SITES.GetCurrentString(1)=="yoy");
            ec.WriteTest(SITES.GetCurrentString(new FCS_uplet(1)) == "yoy");
            foreach (FCS_uplet up in SITES.Uplets())
            {
                w.WriteLine( up.ToString());
            }
        }
        

        public static void Test1()
        {

            DataSet dsModel = new DataSet();
            FCS_Set PERIODS = new FCS_Set();          // Sources 
            FCS_Set PRODUCTS = new FCS_Set();
            FCS_Set SITES = new FCS_Set();

            FCS_Set MODES = new FCS_Set();
            FCS_Set ILEVELS = new FCS_Set();
            FCS_Set TGROUPS = new FCS_Set();
            FCS_Set IGROUPS = new FCS_Set();
            FCS_Set CTYPES = new FCS_Set();

            FillSet(dsModel, PERIODS, "PERIODS", "PERIOD");         // Sources 
            FillSet(dsModel, PRODUCTS, "TRANSPORT_COST", "PRODUCT");
            FillSet(dsModel, SITES, "LOCATIONS", "LOCATION");
            FillSet(dsModel, MODES, "MODES", "MODE");
            FillSet(dsModel, ILEVELS, "INVESTMENTS", "INVESTMENT");
            FillSet(dsModel, TGROUPS, "TRANSPORT_GROUPS", "GROUP");
            FillSet(dsModel, IGROUPS, "INVENTORY_GROUPS", "GROUP");
            CTYPES.Add("MAXI"); const int MAXI = 0;
            CTYPES.Add("MINI"); const int MINI = 0;

            FCS_Subset LINKS = new FCS_Subset(PRODUCTS, SITES, SITES, MODES);
            FCS_Subset LIMITEDTRANSFERS = new FCS_Subset(LINKS, PERIODS, CTYPES);
            FCS_Subset STOCKABLES = new FCS_Subset(PRODUCTS, SITES);
            FCS_Subset LIMITEDSTOCKS = new FCS_Subset(STOCKABLES, PERIODS, CTYPES);
            FCS_Subset INVESTABLES = new FCS_Subset(ILEVELS, STOCKABLES);
            FCS_Subset LIMITEDSTOCKGROUP = new FCS_Subset(IGROUPS, PERIODS, CTYPES);
            FCS_Subset LIMITEDTRANSFERGROUP = new FCS_Subset(TGROUPS, PERIODS, CTYPES);
            FCS_Subset STOCKGROUPS = new FCS_Subset(STOCKABLES, IGROUPS);
            FCS_Subset TRANSFERGROUPS = new FCS_Subset(LINKS, TGROUPS);


            foreach (DataRow row in dsModel.Tables["TRANSPORT_COST"].Rows)
            {
                LINKS.Add(
                    PRODUCTS[row["PRODUCT"].ToString()],
                    SITES[row["SOURCE"].ToString()],
                    SITES[row["DESTINATION"].ToString()],
                    MODES[row["MODE"].ToString()]
                );
            }

            foreach (DataRow row in dsModel.Tables["TRANSPORT_LIMITS"].Rows)
            {
                if (!string.IsNullOrEmpty(row["MAX"].ToString()))
                {
                    LIMITEDTRANSFERS.Add(
                    LINKS.Add(
                        PRODUCTS[row["PRODUCT"].ToString()],
                        SITES[row["SOURCE"].ToString()],
                        SITES[row["DESTINATION"].ToString()],
                        MODES[row["MODE"].ToString()]
                    ),
                    PERIODS[row["PERIOD"].ToString()],
                    MAXI);
                }
                if (!string.IsNullOrEmpty(row["MIN"].ToString()))
                {
                    LIMITEDTRANSFERS.Add(
                    LINKS.Add(
                        PRODUCTS[row["PRODUCT"].ToString()],
                        SITES[row["SOURCE"].ToString()],
                        SITES[row["DESTINATION"].ToString()],
                        MODES[row["MODE"].ToString()]
                    ),
                    PERIODS[row["PERIOD"].ToString()],
                    MINI);
                }
            }

            foreach (DataRow row in dsModel.Tables["INVENTORY_COST"].Rows)
            {
                STOCKABLES.Add(
                    PRODUCTS[row["PRODUCT"].ToString()],
                    SITES[row["SOURCE"].ToString()]
                );
            }

            foreach (DataRow row in dsModel.Tables["INVENTORY_LIMITS"].Rows)
            {
                if (!string.IsNullOrEmpty(row["MAX"].ToString()))
                {
                    LIMITEDSTOCKS.Add(
                    STOCKABLES.Add(
                        PRODUCTS[row["PRODUCT"].ToString()],
                        SITES[row["SOURCE"].ToString()]
                    ),
                    PERIODS[row["PERIOD"].ToString()],
                    MAXI);
                }
                if (!string.IsNullOrEmpty(row["MIN"].ToString()))
                {
                    LIMITEDSTOCKS.Add(
                    STOCKABLES.Add(
                        PRODUCTS[row["PRODUCT"].ToString()],
                        SITES[row["SOURCE"].ToString()]
                    ),
                    PERIODS[row["PERIOD"].ToString()],
                    MINI);
                }
            }

            foreach (DataRow row in dsModel.Tables["INVESTMENTS"].Rows)
            {
                INVESTABLES.Add(
                    ILEVELS[row["INVESTMENT"].ToString()],
                    STOCKABLES[
                    PRODUCTS[row["PRODUCT"].ToString()],
                    SITES[row["SOURCE"].ToString()]
                    ]
                );
            }

            FCS_Data availability = new FCS_Subset(PRODUCTS, SITES, PERIODS);
            FCS_Data demand = new FCS_Subset(PRODUCTS, SITES, PERIODS);
            FCS_Data transportcost = new FCS_Subset(LINKS);
            FCS_Data storagecost = new FCS_Subset(STOCKABLES);
            FCS_Data investment = new FCS_Subset(INVESTABLES);

            FCS_Data basetransportlimit = new FCS_Subset(LIMITEDTRANSFERS);
            FCS_Data grouptransportlimit = new FCS_Subset(LIMITEDTRANSFERGROUP);
            FCS_Data basestoragelimit = new FCS_Subset(LIMITEDSTOCKS);
            FCS_Data groupstoragelimit = new FCS_Subset(LIMITEDSTOCKGROUP);

            FCS_Data transportgroupcoeff = new FCS_Subset(TRANSFERGROUPS);
            FCS_Data storagegroupcoeff = new FCS_Subset(STOCKGROUPS);

            FCS_Data xtcapacity = new FCS_Subset(INVESTABLES);
            FCS_Data initialinventory = new FCS_Subset(STOCKABLES);

            FCS_Data duration = new FCS_Subset(PERIODS);

            foreach (DataRow row in dsModel.Tables["OPENING_INVENTORY"].Rows)
            {
                initialinventory[
                STOCKABLES[
                    PRODUCTS[row["PRODUCT"].ToString()],
                    SITES[row["SOURCE"].ToString()]
                ]] = double.Parse(row["VALUE"].ToString());
            }
            foreach (DataRow row in dsModel.Tables["EXTRA_CAPACITY"].Rows)
            {
                xtcapacity[
                    INVESTABLES[
                    ILEVELS[row["INVESTMENT"].ToString()],
                STOCKABLES[
                    PRODUCTS[row["PRODUCT"].ToString()],
                    SITES[row["SOURCE"].ToString()]
                ]]] = double.Parse(row["VALUE"].ToString());
            }
            foreach (DataRow row in dsModel.Tables["INVESTMENTS"].Rows)
            {
                investment[
                    INVESTABLES[
                    ILEVELS[row["INVESTMENT"].ToString()],
                STOCKABLES[
                    PRODUCTS[row["PRODUCT"].ToString()],
                    SITES[row["SOURCE"].ToString()]
                ]]] = double.Parse(row["VALUE"].ToString());
            }
            foreach (DataRow row in dsModel.Tables["DURATION"].Rows)
            {
                duration[
                PERIODS[
                    row["PERIOD"].ToString()]
                ] = double.Parse(row["DURATION"].ToString());
            }

            foreach (DataRow row in dsModel.Tables["AVAILABILITY"].Rows)
            {
                availability[
                    PRODUCTS[row["PRODUCT"].ToString()],
                    SITES[row["SOURCE"].ToString()],
                    PERIODS[row["PERIOD"].ToString()]
                ] = double.Parse(row["VALUE"].ToString());
            }
            foreach (DataRow row in dsModel.Tables["DEMAND"].Rows)
            {
                demand[
                    PRODUCTS[row["PRODUCT"].ToString()],
                    SITES[row["DESTINATION"].ToString()],
                    PERIODS[row["PERIOD"].ToString()]
                ] = double.Parse(row["VALUE"].ToString());
            }

            foreach (DataRow row in dsModel.Tables["TRANSPORT_COST"].Rows)
            {
                transportcost[
                    LINKS[
                    PRODUCTS[row["PRODUCT"].ToString()],
                    SITES[row["SOURCE"].ToString()],
                    SITES[row["DESTINATION"].ToString()],
                    MODES[row["MODE"].ToString()]
                ]] = double.Parse(row["VALUE"].ToString());
            }

            foreach (DataRow row in dsModel.Tables["TRANSPORT_LIMITS"].Rows)
            {
                if (string.IsNullOrEmpty(row["MAX"].ToString()))
                {
                    basetransportlimit[
                        LIMITEDTRANSFERS[
                        LINKS[
                        PRODUCTS[row["PRODUCT"].ToString()],
                        SITES[row["SOURCE"].ToString()],
                        SITES[row["DESTINATION"].ToString()],
                        MODES[row["MODE"].ToString()]
                        ],
                        PERIODS[row["PERIOD"].ToString()],
                    MAXI
                    ]] = double.Parse(row["MAX"].ToString());
                }
                if (string.IsNullOrEmpty(row["MIN"].ToString()))
                {
                    basetransportlimit[
                        LIMITEDTRANSFERS[
                        LINKS[
                        PRODUCTS[row["PRODUCT"].ToString()],
                        SITES[row["SOURCE"].ToString()],
                        SITES[row["DESTINATION"].ToString()],
                        MODES[row["MODE"].ToString()]
                        ],
                        PERIODS[row["PERIOD"].ToString()],
                    MINI
                    ]] = double.Parse(row["MIN"].ToString());
                }
            }
            foreach (DataRow row in dsModel.Tables["INVENTORY_COST"].Rows)
            {
                storagecost[
                    STOCKABLES[
                    PRODUCTS[row["PRODUCT"].ToString()],
                    SITES[row["SOURCE"].ToString()]
                ]]
                 = double.Parse(row["VALUE"].ToString());
            }

            foreach (DataRow row in dsModel.Tables["INVENTORY_LIMITS"].Rows)
            {
                if (string.IsNullOrEmpty(row["MAX"].ToString()))
                {
                    basestoragelimit[
                        LIMITEDSTOCKS[
                        STOCKABLES[
                        PRODUCTS[row["PRODUCT"].ToString()],
                        SITES[row["SOURCE"].ToString()]
                        ],
                        PERIODS[row["PERIOD"].ToString()],
                    MAXI
                    ]] = double.Parse(row["MAX"].ToString());
                }
                if (string.IsNullOrEmpty(row["MIN"].ToString()))
                {
                    basetransportlimit[
                        LIMITEDSTOCKS[
                        STOCKABLES[
                        PRODUCTS[row["PRODUCT"].ToString()],
                        SITES[row["SOURCE"].ToString()]
                        ],
                        PERIODS[row["PERIOD"].ToString()],
                    MINI
                    ]] = double.Parse(row["MIN"].ToString());
                }
            }

            FCS_Variable qtrans = new FCS_Variable(LINKS, PERIODS);
            FCS_Variable stocklevel = new FCS_Variable(STOCKABLES, PERIODS);
            FCS_Variable invest = new FCS_Variable(INVESTABLES, PERIODS);
            invest.binary = true;
            FCS_Variable qpenalplus = new FCS_Variable(PRODUCTS, SITES, PERIODS);
            FCS_Variable qpenalmoins = new FCS_Variable(PRODUCTS, SITES, PERIODS);
            //FCS_Set SITE = new FCS_Set("SITE");
            //SITE.Add(6);
            //FCS_Set PRODUCTS = new FCS_Set("PRODUCTS");
            //PRODUCTS.Add(4);

            //FCS_Subset STOCKABLE = new FCS_Subset(SITE, PRODUCTS);
            //STOCKABLE.Add(SITE["SITE1"], PRODUCTS["PRODUCTS3"]);

            //FCS_Subset LINKS = new FCS_Subset(SITE, SITE);
            //LINKS._name = "links";
            //LINKS.Add(0, 1);
            //LINKS.Add(SITE["SITE2"], 1);
            //LINKS.Add(SITE["SITE2"], 3);
            //LINKS.Add(SITE["SITE4"], 5);

            //FCS_Constraint stockmax = new FCS_Constraint(STOCKABLE);
            FCS_Index s = new FCS_Index("s");
            FCS_Index s1 = new FCS_Index("s1");
            FCS_Index p = new FCS_Index("p");
            FCS_Index t = new FCS_Index("t");
            FCS_Index m = new FCS_Index("m");
            FCS_Index t2 = new FCS_Index("t2");
            FCS_Index i = new FCS_Index("i");
            FCS_Index st = new FCS_Index("st");
            FCS_Index ct = new FCS_Index("ct");
            FCS_Index lk = new FCS_Index("lk");
            //FCS_Variable stock = new FCS_Variable(STOCKABLE);
            //FCS_Variable import = new FCS_Variable(STOCKABLE);

            //FCS_Variable qtrans = new FCS_Variable(LINKS);
            //FCS_Constraint matbal = new FCS_Constraint(SITE);
            //FCS_Constraint maxcap = new FCS_Constraint(LINKS);
            //FCS_Data transportlimit = new FCS_Subset(LINKS);
            //FCS_Data demand = new FCS_Subset(SITE);
            //FCS_Data production = new FCS_Subset(SITE);

            //demand[0] = 5;
            //demand[1] = 4;
            //demand[2] = 7;
            //demand[3] = 8;
            //demand[4] = 5;
            //demand[5] = 5;

            //production[0] = 5;
            //production[1] = 5;
            //production[2] = 5;
            //production[3] = 5;
            //production[4] = 5;
            //production[5] = 5;

            //transportlimit[0] = 5;
            //transportlimit[1] = 34.4;
            //transportlimit[2] = 7;

            FCS_Model model = new FCS_Model();

            FCS_Constraint uniqueinvestment = new FCS_Constraint(STOCKABLES, PERIODS);
            FCS_Constraint capacitymax = new FCS_Constraint(STOCKABLES, PERIODS, CTYPES);
            FCS_Constraint matbal = new FCS_Constraint(PRODUCTS, SITES, PERIODS);

            uniqueinvestment[s, t] = model.Sum(INVESTABLES[i, s], invest[INVESTABLES[i, s], t]) <= 1;

            capacitymax[s, t, CTYPES[ct].SuchThat(ct == MAXI)] =
                stocklevel[s, t] - model.Sum(ILEVELS[i] * PERIODS[t2].SuchThat(t2 <= t),
                invest[INVESTABLES[i, s], t2] * xtcapacity[INVESTABLES[i, s]])
                <= basestoragelimit[s, t, ct];

            matbal[p, s, t] =
              model.Sum(LINKS[p, s1, s, m], qtrans[LINKS[p, s1, s, m]])
            + qpenalplus[p, s, t]
            - model.Sum(LINKS[p, s, s1, m], qtrans[LINKS[p, s, s1, m]])
            - qpenalmoins[p, s, t]
            - stocklevel[STOCKABLES[p, s], t]
            ==
            demand[p, s, t]
            - initialinventory[STOCKABLES[p, s]]
            - availability[p, s, t];

            //matbal[s] = model.Sum(SITE[s2], qtrans[LINKS[s2, s]]) -
            //    model.Sum(SITE[s2], 3 * qtrans[LINKS[s, s2]]) == 4 + demand[s] + production[s];
            ////matbal[s] = qtrans[LINKS[s2, s]] - (qtrans[LINKS[s, s2]])<=7;
            //maxcap[s] = qtrans[s] <= transportlimit[s];
            //stockmax[s] =  3 * stock[s] + import[s]<=4;
            //stockmax[STOCKABLE[s,p]] = 3 * stock[STOCKABLE[s,p]] + import[s] == 4;




            model.AddVariable(qtrans);
            model.AddConstraint(matbal);
            model.CreateProblem();
            //model.Minimise(import[s]);

            //Thread.Sleep(5000);
        }
        public static void FillSet(DataSet dsModel, FCS_Set set, String stable, String scol)
        {

            DataTable table = dsModel.Tables[stable];
            foreach (DataRow row in table.Rows)
            {
                String s = row[scol].ToString();
                set.Add(s);
            }

        }
    }
}
