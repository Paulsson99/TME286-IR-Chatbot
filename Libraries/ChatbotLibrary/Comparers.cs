using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotLibrary
{
    public class MovieUtteranceComparer : IComparer<(int id, string utterance)>
    {
        public int Compare((int id, string utterance) item1, (int id, string utterance) item2)
        {
            return item1.id.CompareTo(item2.id);
        }
    }
}
