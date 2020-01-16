using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexibleMenu
{
    public abstract class AbsMenu : IMenu
    {
        public abstract void AddPartMenu(params string[] textOfPartMEnu);
        public abstract SubMenu AddSubMenu(string sourcePart, params string[] textOfPartMenu);
        public abstract void Start();
        public protected virtual void PressButton()
        {

        }
    }
}
