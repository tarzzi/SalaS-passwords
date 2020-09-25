using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaS
{
    class Program
    {
        static void Main(string[] args)
        {
            string mpass = "k";
            Console.WriteLine("Give master-password");
            string guess = Console.ReadLine();
            if (guess == mpass)
            {
                Menu();
            }
            else {
                Console.WriteLine("Access denied");
                Console.ReadKey();
            }
        }
        private static void Menu() {
            int selection = 1;
            do
            {
                Console.Clear();
                Console.WriteLine("##| SalaS password-manager |##");
                Console.WriteLine(" 1.List passwords \n 2.Add new password\n 3.Remove password");
                try
                {
                    int.TryParse(Console.ReadLine(), out selection);
                }
                catch (Exception)
                {
                    selection = 99;
                }
                switch (selection) {
                    case 1:
                        Console.Clear();
                        ListPasswords();
                        break;
                    case 2:
                        Console.Clear();
                        AddPassword();
                        break;
                    case 3:
                        Console.Clear();
                        RemovePassword();
                        break;
                    default:
                        break;

                }
            } while (selection != 0);
        }

        private static void RemovePassword()
        {
            List<Pass> passes = sqlitedata.LoadPass();
            if (passes.Count <= 0) { Console.WriteLine("No passwords stored"); Console.ReadKey(); }
            else
            {
                int i = 1;
                int pick = 0;
                Console.WriteLine("Index | Service | Password");
                foreach (Pass pv in passes)
                {
                    Console.WriteLine($"{i}.: {pv.Service} | {pv.Passwd}");
                    i++;
                }
                Console.WriteLine("Index of deletable entry: ");
                try
                {
                    int.TryParse(Console.ReadLine(), out pick);
                }
                catch (Exception)
                {

                    throw;
                }
                sqlitedata.DeletePass(pick);
            }
        }

        private static void AddPassword()
        {
            Pass p = new Pass();
            Console.WriteLine("Give service name");
            string serv = Console.ReadLine();
            Console.WriteLine("Enter password for service");
            string pass = Console.ReadLine();
            p.Service = serv;
            p.Passwd = pass;
            sqlitedata.SavePass(p);
            Console.WriteLine("Added succesfully");
            Console.ReadKey();
            

        
        }

        private static void ListPasswords()
        {
            List<Pass> passes = sqlitedata.LoadPass();
            if (passes.Count <= 0) { Console.WriteLine("No passwords stored"); Console.ReadKey(); }
            else
            {
                int i = 1;
                Console.WriteLine("Index | Service | Password");
                foreach (Pass pv in passes)
                {
                    Console.WriteLine($"{i}.: {pv.Service} | {pv.Passwd}");
                    i++;
                }
                Console.ReadKey();
            }
        }
    }
    public class Pass
    {
        public string Service { get; set; }
        public string Passwd { get; set; }
        public string Creds { get { return $"{Service} | {Passwd}"; } }
        public Pass() { }
        public Pass(string service, string password) {
            service = Service;
            password = Passwd;
        }
        public override string ToString()
        {
            return $"Service: {Service}  && Password: {Passwd}";
        }
    }
}
