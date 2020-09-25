using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaS
{
    class sqlitedata
    {
        public static List<Pass> LoadPass()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) 
            {
                var output = cnn.Query<Pass>("SELECT * FROM credentials");
                return output.ToList();
            }
        }
        public static void SavePass(Pass pass) 
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"insert into credentials (service, passwd) values ('{pass.Service}', '{pass.Passwd}')");
            }
        }
        public static void DeletePass(int target)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"delete from credentials where id = {target}");
            }
        }
        public static void AddUser(User user) {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"insert into user (uname, upass) values ('{user.Uname}', '{user.Upass}')");
            }
        }
        public static bool Login(string uname, string upass) {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var result = cnn.Query<User>($"select * from user where uname = '{uname}' and upass = '{upass}')");
                result = result.ToList();
                if (result.Count() <= 0) { return false; }
                else { return true; }
            }
        }
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
