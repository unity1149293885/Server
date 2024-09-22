using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class  MySqlTools
    {
        public static MySqlConnection connection;
        public static void ConnectMySql()
        {
            string connectionString =
              "server = " + MySqlInfo.server +
              ";user = " + MySqlInfo.user +
              ";database = " + MySqlInfo.database +
              ";password = " + MySqlInfo.password;

            connection = new MySqlConnection(connectionString);
            connection.Open();

            Output.OutputInfo("数据库已连接！");
        }
        public static void UpdateAllUserInfo()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM UserInfo", connection);
            MySqlDataReader reader = command.ExecuteReader();
            Console.WriteLine("当前用户列表：");
            while (reader.Read())
            {
                int uid = reader.GetInt32(0);
                string name = reader.GetString(1);
                string phone = reader.GetString(2);
                int type = reader.GetInt16(3);
                UserManager.AddUser(uid, name, phone, type);
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt16(3));
            }

            reader.Close();
        }

        public static void RegisterUser(int uid,string name,string phone,int type)
        {
            string sql = "INSERT INTO UserInfo (uid,name, phone, usertype) VALUES (@uid,@name, @phone, @usertype)";

            // 创建 MySQL 命令对象
            MySqlCommand command = new MySqlCommand(sql, connection);

            // 设置参数值
            command.Parameters.AddWithValue("@uid", uid);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@usertype", type);

            // 执行命令并返回受影响行数
            int rowsAffected = command.ExecuteNonQuery();
        }

    }
}
