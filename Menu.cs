using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Dev Miracle 
//Гибкое меню
//Библиотека, позволяющая рисовать меню в консоли(псевдографика) и выбирать пункты меню на стрелки, Enter и Escape.

namespace FlexibleMenu
{
    public class GeneralMenu
    {
        private class GeneralMenuTitleClass
        {
            public string title;
            public bool isLast;
        }
        public event EventHandler<MyEventArgs> EventPressEnter;
        private List<GeneralMenuTitleClass> GeneralMenuTitle;
        private List<SubMenu> SubMenu;
        private readonly int XСoordinate;
        private readonly int YСoordinate;
        public static int XCoord;
        public static int YCoord;
        public bool isInTheCycle;
        static GeneralMenu()
        {
            XCoord = 0;
            YCoord = 0;
        }
        public GeneralMenu() : this(0, 0) { }
        public GeneralMenu(params string[] partsOfMenu) : this (0, 0, partsOfMenu) { }
        public GeneralMenu(int _XСoordinate, int _YСoordinate) : this(XСoordinate: _XСoordinate, YСoordinate: _YСoordinate) { }
        public GeneralMenu(int XСoordinate = 0, int YСoordinate = 0, params string[] partsOfMenu)
        {
            GeneralMenuTitle = new List<GeneralMenuTitleClass>();
            SubMenu = new List<SubMenu>();
            this.XСoordinate = XСoordinate;
            this.YСoordinate = YСoordinate;
            for (int i = 0; i < partsOfMenu.Length; i++)
            {
                GeneralMenuTitleClass GeneralMenuTitleClass = new GeneralMenuTitleClass();
                GeneralMenuTitleClass.title = partsOfMenu[i];
                GeneralMenuTitleClass.isLast = true;
                GeneralMenuTitle.Add(GeneralMenuTitleClass);
            }
            XCoord = this.XСoordinate;//записываем в статическую переменную
            YCoord = this.YСoordinate;//аналогично
        }
        
        public void AddPartMenu(params string[] textOfPartMEnu)
        {
            for (int i = 0; i < textOfPartMEnu.Length; i++)
            {
                GeneralMenuTitleClass GeneralMenuTitleClass = new GeneralMenuTitleClass();
                GeneralMenuTitleClass.title = textOfPartMEnu[i];
                GeneralMenuTitleClass.isLast = true;
                GeneralMenuTitle.Add(GeneralMenuTitleClass);
            }
        }
        
        public SubMenu AddSubMenu(string sourcePart, params string[] textOfPartMenu)
        {
            int? index = null;//? - не знаю нужно или нет =)
            index = GeneralMenuTitle.FindIndex(item => item.title == sourcePart);
            if (index != -1)
            {
                SubMenu.Add(new SubMenu((int)index, sourcePart, textOfPartMenu));
                GeneralMenuTitle[(int)index].isLast = false;
            }
            else if (index == null)
            {
                Console.Beep();
                Environment.Exit(0);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Неверно указан источник, к которому была произведена попытка подвязать вложенное меню.");
                Console.ReadKey(true);
                Environment.Exit(0);
            }
            return SubMenu.Find(item => item.indexSourcePartMenu == index);
        }

        public void Start()
        {
            if (GeneralMenuTitle.Count != 0)
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
        private void PressButton()
        {
            ConsoleKeyInfo button;
            int target = 1;
            do
            {
                Console.Clear();
                for (int i = 0; i < GeneralMenuTitle.Count; i++)
                {
                    if (target - 1 == i)
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.SetCursorPosition(XСoordinate, YСoordinate + i);
                    Console.Write(GeneralMenuTitle[i].title);
                    Console.ResetColor();
                }
                button = Console.ReadKey(true);//ждем нажатие пользователя
                switch (button.Key)
                {
                    case ConsoleKey.Enter:
                        MyEventArgs obj = new MyEventArgs();
                        obj.Target = target;
                        if (GeneralMenuTitle[target - 1].isLast)
                        {
                            EventPressEnter?.Invoke(this, obj);//событие нажатие Enter'a, передаем таргет меню
                        }
                        else
                        {
                            int i = SubMenu.FindIndex(item => item.indexSourcePartMenu == --target);
                            if (i != -1)
                                SubMenu[i].Start();
                            else
                                Console.Beep();
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
                        if (target == GeneralMenuTitle.Count)
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
