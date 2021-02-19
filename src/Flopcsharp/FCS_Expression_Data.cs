using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public class FCS_Expression_Data
    {
        FCS_Data _data;
        FCS_Domain _dom;

        public FCS_Expression_Data(FCS_Data data, FCS_Domain dom)
        {
            _data = data;
            _dom = dom;
        }
        public double eval()
        {
            return _data.Eval(_dom.Eval());
        }

        public static FCS_Expression_Numeric operator +(double d, FCS_Expression_Data data)
        {
            return (FCS_Expression_Numeric)d + (FCS_Expression_Numeric)data;
        }
        public static FCS_Expression_Numeric operator -(double d, FCS_Expression_Data data)
        {
            return (FCS_Expression_Numeric)d - (FCS_Expression_Numeric)data;
        }
        public static FCS_Expression_Numeric operator *(double d, FCS_Expression_Data data)
        {
            return (FCS_Expression_Numeric)d * (FCS_Expression_Numeric)data;
        }
        public static FCS_Expression_Numeric operator +( FCS_Expression_Data data,double d)
        {
            return (FCS_Expression_Numeric)d + (FCS_Expression_Numeric)data;
        }
        public static FCS_Expression_Numeric operator -(FCS_Expression_Data data, double d)
        {
            return  (FCS_Expression_Numeric)data-(FCS_Expression_Numeric)d;
        }
        public static FCS_Expression_Numeric operator +( FCS_Expression_Data data1,FCS_Expression_Data data2)
        {
            return (FCS_Expression_Numeric)data1 + (FCS_Expression_Numeric)data2;
        }
        public static FCS_Expression_Numeric operator -(FCS_Expression_Data data1, FCS_Expression_Data data2)
        {
            return (FCS_Expression_Numeric)data1 - (FCS_Expression_Numeric)data2;
        }
        public static FCS_Expression_Numeric operator *(FCS_Expression_Data data1, FCS_Expression_Data data2)
        {
            return (FCS_Expression_Numeric)data1 * (FCS_Expression_Numeric)data2;
        }
    }
}
