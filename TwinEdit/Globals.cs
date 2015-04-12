using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace TwinEdit
{
    class Globals
    {

        private static List<CategoryInfo> _categories;
        internal static List<CategoryInfo> Categories
        {
            get
            {
                if (_categories == null)
                    _categories = new List<CategoryInfo>();
                return _categories;
            }
        }

        internal static Project Project;
        internal static Settings Settings;
        internal static string JustSavedPath;

    }
}
