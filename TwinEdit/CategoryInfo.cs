using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinEdit
{
    public class CategoryInfo
    {
        public int Id;
        public string Title;
        public string Tags;
        public List<PassageInfo> Passages;

        public CategoryInfo()
        {
            Passages = new List<PassageInfo>();
        }
    }
}
