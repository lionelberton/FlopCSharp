using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    public class FCS_uplet
    {
        public int i1;
        public int i2;
        public int i3;
        public int i4;
        public int i5;

        public FCS_uplet(int a)
        {
            i1 = a;
        }
        public FCS_uplet(int a, int b)
        {
            i1 = a;
            i2 = b;

        }
        public FCS_uplet(int a, int b, int c)
        {
            i1 = a;
            i2 = b;
            i3 = c;
  
        }
        public FCS_uplet(int a, int b, int c, int d)
        {
            i1 = a;
            i2 = b;
            i3 = c;
            i4 = d;

        }
        public FCS_uplet(int a,int b,int c, int d, int e )
        {
            i1 = a;
            i2 = b;
            i3 = c;
            i4 = d;
            i5 = e;
        }

        public override bool Equals(object obj)
        {
            FCS_uplet up = (FCS_uplet)obj;
            return up.i1 == i1 &&
                up.i2 == i2 &&
                up.i3 == i3 &&
                up.i4 == i4 &&
                up.i5 == i5;
        }
        public override int GetHashCode()
        {
            return (i1 +";" +i2+";" +i3+";"+i4+";"+i5).GetHashCode();
        }
        public bool Valid()
        {
            return i1 >= 0 && i2 >= 0 && i3 >= 0 && i4 >= 0 && i5>=0;
        }
        public override string ToString()
        {
            return "(" + i1 + ";" + i2 + ";" + i3 + ";" + i4 + ";" + i5 + ")";
        }
    }
}
