using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using FastColoredTextBoxNS;

namespace TwinEdit
{
    public class CodeEdit : FastColoredTextBox
    {
        private AutocompleteMenu menu;

        public CodeEdit()
        {
            menu = new AutocompleteMenu(this);
            menu.AppearInterval = 100;
            menu.ShowItemToolTips = true;

            

        }

    }
}
