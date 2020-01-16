using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexibleMenu
{
    interface IMenu
    {
        void AddPartMenu(params string[] textOfPartMEnu);
        SubMenu AddSubMenu(string sourcePart, params string[] textOfPartMenu);
        void Start();
        void PressButton();
    }
}
