using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassLibrary
{
    public class DataBase
    {
        //private string strConnection1 = string.Format("server={0};user={1};password={2};database={3};", "192.168.3.253", "root", "test", "test");
        //private string strConnection2 = string.Format("server={0};user={1};password={2};database={3};", "192.168.3.250", "root", "1234", "gudi");

        public MySqlConnection GetConnection()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection();

                string path = "/public/DBInfo.json";                
                string result = new StreamReader(File.OpenRead(path)).ReadToEnd();
                JObject jo = JsonConvert.DeserializeObject<JObject>(result);
                Hashtable map = new Hashtable();
                foreach (JProperty col in jo.Properties())
                {
                    Console.WriteLine("{0} : {1}", col.Name, col.Value);
                    map.Add(col.Name, col.Value);
                }

                string strConnection
                    = string.Format("server={0};user={1};password={2};database={3};", map["server"], map["user"], map["password"], map["database"]);
                conn.ConnectionString = strConnection;
                conn.Open();

                Console.WriteLine("DB접속 정상처리됨");

                return conn;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("에러발생 : " + e.Message);
                return null;
            }
        }
    }
}
