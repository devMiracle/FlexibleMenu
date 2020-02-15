using FlexibleMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneralMenu generalMenu = new GeneralMenu(10, 10, "Game", "SubMenu", "Exit");
            generalMenu.EventPressEnter += (sender, e) => 
            {
                switch (e.Target)
                {
                    case 1:
                        Console.Beep();
                        break;
                    case 2:
                        Console.Beep();
                        break;
                    case 3:
                        generalMenu.isInTheCycle = true;
                        break;
                    default:
                        break;
                }
            };
            generalMenu.Start();

        }
    }
}
