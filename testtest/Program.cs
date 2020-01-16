using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexibleMenu;


namespace testDll
{
    class Program
    {
        static void Main(string[] args)
        {

            GeneralMenu generalMenu = new GeneralMenu("Играть", "Донат", "Выход");
            generalMenu.EventPressEnter += (sender, obj) =>
            {
                switch (obj.Target)
                {
                    case 1:
                        Console.Beep();
                        break;
                    case 2:

                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            };
            SubMenu subMenu1 = generalMenu.AddSubMenu("Донат", "TEST1", "Назад");
            subMenu1.EventPressEnter += (sender, obj) =>
            {
                switch (obj.Target)
                {
                    case 1:

                        break;
                    case 2:
                        subMenu1.isInTheCycle = true;
                        break;
                    default:
                        break;
                }
            };
            SubMenu subMenu2 = subMenu1.AddSubMenu("TEST1", "test", "Назад");
            subMenu2.EventPressEnter += (sender, obj) =>
            {
                switch (obj.Target)
                {
                    case 1:
                        Console.Beep();
                        Console.Beep();
                        break;
                    case 2:
                        subMenu2.isInTheCycle = true;
                        break;
                    default:
                        break;
                }
            };



            generalMenu.Start();
            Console.ReadKey(true);
        }



    }
}
