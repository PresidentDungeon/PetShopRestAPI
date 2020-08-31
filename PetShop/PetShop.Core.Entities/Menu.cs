using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.Entities
{
    public abstract class Menu
    {
        private int EXIT_OPTION = 0;
        protected bool ShouldCloseOnFinish = false;
        private string InputText = "Please select an option:";

        private string[] MenuItems;
        private string MenuTitle;

        public Menu(string menuTitle, params string[] menuItems)
        {
            this.MenuTitle = menuTitle;
            this.MenuItems = menuItems;
        }

        private int GetOption()
        {
            int option = -1;

            while (option < 0)
            {
                Console.WriteLine("\n" + InputText);
                string nrAsText = Console.ReadLine();

                try
                {
                    option = int.Parse(nrAsText);

                    if (option < 0 || option > MenuItems.Length)
                    {
                        Console.WriteLine("Invalid options. Please choose an option in range (0 - " + MenuItems.Length + " )");
                        option = -1;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Make sure that the entered value is a number");
                }
            }

            return option;
        }

        protected abstract void DoAction(int option);

        public void Run()
        {
            do
            {
                PrintMenu();
                int option = GetOption();

                if (option != 0)
                {
                    DoAction(option);
                }
                else
                {
                    Console.WriteLine("\nClosing " + MenuTitle);
                    break;
                }
            } while (!ShouldCloseOnFinish);
        }

        private void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine(MenuTitle + "\n");

            for (int i = 0; i < MenuItems.Length; i++)
            {
                Console.WriteLine(i + 1 + ": " + MenuItems[i]);
            }
            Console.WriteLine("0: Exit");
        }

        protected int GetOption<T>(IList<T> List, bool isCancelable)
        {
            int selection;

            for (int i = 0; i < List.Count; i++)
            {
                Console.WriteLine($"{i + 1}:\n{List[i]}\n");
            }
            if (!isCancelable)
            {
                while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > List.Count)
                {
                    Console.WriteLine($"Invalid input. Please choose an option in range (1-{List.Count})");
                }
            }
            else
            {
                Console.WriteLine("0: Back");
                while (!int.TryParse(Console.ReadLine(), out selection) || selection < 0 || selection > List.Count)
                {
                    Console.WriteLine($"Invalid input. Please choose an option in range (0-{List.Count})");
                }
            }
            return selection;
        }

        protected bool ConfirmChoise()
        {
            Console.WriteLine("(y/n)");

            string choise = Console.ReadLine();

            while (choise.ToLower() != "y" && choise.ToLower() != "n")
            {
                Console.WriteLine("Please type 'y' to accept or 'n' to cancel");
                choise = Console.ReadLine();
            }

            if (choise.Equals("y"))
            {
                return true;
            }

            return false;
        }

        protected void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
