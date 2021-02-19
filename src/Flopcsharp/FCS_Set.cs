using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Flopcsharp
{
    /// <summary>
    /// 
    /// </summary>
    public class FCS_Set : FCS_Subset
    {
        private Dictionary<string, int> _elements;
        private List<string> _names;

        #region constructeurs
        public FCS_Set()
        {
            _elements = new Dictionary<string, int>();
            _names = new List<string>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name">le nom du nouveau set.</param>
        public FCS_Set(string name)
            : this()
        {
            Name = name;

        }
        #endregion

        /// <summary>
        /// Ajoute un nouvel �l�ment. En g�n�rant un nom automatique bas� sur le nom du set et le nombre d'�l�ment.
        /// </summary>
        /// <returns>la position du nouveau set.</returns>
        public int Add()
        {
            int j = -1;
            int i = _elements.Count;
            while (j == -1)
            {
                string label = Name + i.ToString();
                j = Add(label);
                i++;
            }
            return j;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns>renvoie la position de l'�l�ment ajout�, ou -1 s'il existe d�j�.</returns>
        public int Add(string s)
        {
            if (!_elements.ContainsKey(s))
            {
                int i = _elements.Count;
                _elements.Add(s, i);
                _names.Add(s);
                return i;
            }
            else
            {
                _lastError = "Cet �l�ment '" + s + "', est d�j� dans l'ensemble: " + Name;
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns>La position de l'�l�ment ajout� ou celle de l'�l�ment existant.</returns>
        public int AddorGet(string s)
        {
            int i = Add(s);
            if (i >= 0)
            {
                return i;
            }
            else
            {
                return _elements[s];
            }
        }
        /// <summary>
        /// Ajoute n �l�ments � l'ensemble.
        /// </summary>
        /// <param name="n"></param>
        /// <returns>La position du dernier �l�ment ajout�.</returns>
        public override int Add(int n)
        {
            int j = 0;
            for (int g = 1; g <= n; g++)
            {
                j = Add();
            }
            return j;
        }

        public override IEnumerable<FCS_uplet> Indexes(FCS_Index k)
        {
            foreach (int i in _elements.Values)
            {
                k.Index = i;
                yield return new FCS_uplet(i, 0, 0, 0, 0);
            }
        }
        public IEnumerable<string> Elements()
        {
            foreach (string s in _names)
            {
                yield return s;
            }
        }

        /// <summary>
        /// Renvoie un domaine
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public FCS_BaseDomain this[FCS_Index k]
        {
            get
            {
                return new FCS_BaseDomain(this, k);
            }
        }


        public override int this[string k]
        {
            get
            {
                return _elements[k];
            }
        }

        public string Get(FCS_Index i)
        {
            return _names[i.Index];
        }

        public override string GetCurrentString(int upo)
        {
            return _names[upo];
        }
        public override string GetCurrentString(FCS_uplet upo)
        {
            return GetCurrentString(upo.i1);
        }
        public override void Dump()
        {
            foreach (string s in _names)
            {
                Console.Out.WriteLine(Name + " (" + s + ")");
            }
        }

    }
}
