using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotLibrary
{
    public class SparseVector
    {
        List<int> indices;
        List<double> values;

        public SparseVector()
        {
            indices = new List<int>();
            values = new List<double>();
        }

        public void Normalize()
        {
            double normSquared = 0;
            foreach (double v in values)
            {
                normSquared += v*v;
            }
            double norm = Math.Sqrt(normSquared);
            for (int i = 0; i < values.Count; i++)
            {
                values[i] /= norm;
            }
        }

        public double Dot(SparseVector v)
        {
            double dotProduct = 0;
            for (int i = 0; i < indices.Count;i++) 
            {
                int index = indices[i];
                dotProduct += v[index] * values[i];
            }
            return dotProduct;
        }

        public double this[int index]
        {
            // Can be indexed as a normal array
            // Will be slow for large array but none should be larger than ~20 so it should be fine
            get {
                int i = indices.IndexOf(index);
                if (i == -1)
                {
                    return 0;
                }
                return values[i]; 
            }
            set {
                int i = indices.IndexOf(index);
                if (i == -1)
                {
                    indices.Add(index);
                    values.Add(value);
                }
                else
                {
                    values[i] = value;
                }
            }
        }
    }
}
