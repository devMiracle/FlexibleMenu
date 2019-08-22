using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexibleMenu
{
    public class SubMenu
    {
        private class SubMenuTitleClass
        {
            public string title;
            public bool isLast;
        }
        private List<SubMenuTitleClass> SubMenuTitle;
        private List<SubMenu> SubSubMenu;

        public string sourcePartMenu;
        public int indexSourcePartMenu;

        public event EventHandler<MyEventArgs> EventPressEnter;
        public bool isInTheCycle;
        public SubMenu()
        {
            //требуется только для того, чтобы с помощью объекта обратится к ивенту при присвоении ему логики. 
        }
        public SubMenu(int _indexSourcePartMEnu, string _sourcePartMenu, params string[] textOfPartMenu)
        {
            indexSourcePartMenu = _indexSourcePartMEnu;
            sourcePartMenu = _sourcePartMenu;
            SubSubMenu = new List<SubMenu>();//будет определятся только через метод.
            SubMenuTitle = new List<SubMenuTitleClass>();

            for (int i = 0; i < textOfPartMenu.Length; i++)
            {
                SubMenuTitleClass SubMenuTitleClass = new SubMenuTitleClass();
                SubMenuTitleClass.title = textOfPartMenu[i];
                SubMenuTitleClass.isLast = true;
                SubMenuTitle.Add(SubMenuTitleClass);
            }
            
        }

        public void AddPartMenu(params string[] textOfPartMEnu)
        {
            for (int i = 0; i < textOfPartMEnu.Length; i++)
            {
                SubMenuTitleClass SubMenuTitleClass = new SubMenuTitleClass();
                SubMenuTitleClass.title = textOfPartMEnu[i];
                SubMenuTitleClass.isLast = true;
                SubMenuTitle.Add(SubMenuTitleClass);
            }
        }

        public SubMenu AddSubMenu(string sourcePart, params string[] textOfPartMenu)
        {
            int? index = null;//? - не знаю нужно или нет =)
            index = SubMenuTitle.FindIndex(item => item.title == sourcePart);
            if (index != -1)
            {
                SubSubMenu.Add(new SubMenu((int)index, sourcePart, textOfPartMenu));
                SubMenuTitle[(int)index].isLast = false;
            }
            else if (index == null)
            {
                Console.Beep();
                Environment.Exit(0);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Неверно указан источник, к которому была произведена попытка подвязать вложенное меню.");
                Console.ReadKey(true);
                Environment.Exit(0);
            }
            return SubSubMenu.Find(item => item.indexSourcePartMenu == index);
        }
        
        public void Start()
        {
            if (SubMenuTitle.Count != 0)
            {
                PressButton();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Ошибка, пунктов меню не найдено");
                Console.ResetColor();
                Console.ReadKey(true);
                return;
            }
        }

        //TODO: создать метод, который генерирует новый подкласс 
        //public SubMenu AddMenu()
        //{
        //    SubMenu sub = new SubMenu();
        //    return sub;
        //}

        private void PressButton()
        {
            ConsoleKeyInfo button;
            int target = 1;
            do
            {
                Console.Clear();
                for (int i = 0; i < SubMenuTitle.Count; i++)
                {
                    if (target - 1 == i)
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.SetCursorPosition(GeneralMenu.XCoord, GeneralMenu.YCoord + i);
                    Console.Write(SubMenuTitle[i].title);
                    Console.ResetColor();
                }
                button = Console.ReadKey(true);//ждем нажатие пользователя
                switch (button.Key)
                {
                    case ConsoleKey.Enter:
                        MyEventArgs obj = new MyEventArgs();
                        obj.Target = target;
                        if (SubMenuTitle[target - 1].isLast)
                        {
                            EventPressEnter?.Invoke(this, obj);//событие нажатие Enter'a, передаем таргет меню
                        }
                        else
                        {
                            SubSubMenu[target - 1].Start();
                        }
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.UpArrow:
                        if (target == 1)
                            break;
                        target--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (target == SubMenuTitle.Count)
                            break;
                        target++;
                        break;
                    default:
                        break;
                }
            } while (!isInTheCycle);
        }
    }
}
