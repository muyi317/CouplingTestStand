using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using CouplingTestStand.Properties;
using System.Windows;

namespace CouplingTestStand
{
    class CouplingTestDB
    {
        /// <summary>
        /// 服务器地址；端口号；数据库；用户名；密码
        /// </summary>
        /// //SQLBulkCopy
        string sqlstr = "Data Source=localHost;User ID=root;Password=114114;DataBase=couplings;";
        MySqlConnection sqlCon;

        public List<string> id = new List<string>();
        public List<string> couplingTypes = new List<string>();
        public List<string> mesTime = new List<string>();
        public List<string> testItem = new List<string>();
        public List<string> testTorque = new List<string>();
        public List<string> testFrequency = new List<string>();
        public List<string> testStiffness = new List<string>();
        public List<string> torqueRated = new List<string>();
        public List<string> maxSpeed = new List<string>();
        public List<string> torqueCons = new List<string>();
        public List<string> axialCons = new List<string>();
        public List<string> momentIne = new List<string>();
        public List<string> mass = new List<string>();
        public List<string> dataDir = new List<string>();

        public Dictionary<string, string> couplingInf = new Dictionary<string, string>();

        public void Select()
        {
            id.Clear();
            couplingTypes.Clear();
            mesTime.Clear();
            testItem.Clear();
            testTorque.Clear();
            testFrequency.Clear();
            testStiffness.Clear();
            torqueRated.Clear();
            maxSpeed.Clear();
            torqueCons.Clear();
            axialCons.Clear();
            momentIne.Clear();
            mass.Clear();
            dataDir.Clear();

            sqlCon = new MySqlConnection(sqlstr);
            sqlCon.Open();

            string sql = "select * from couplings;";
            //获得MySsqlCommand
            MySqlCommand cmd = new MySqlCommand(sql, sqlCon);

            //执行SQL并且返回查询结果
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //处理返回结果
            while (dataReader.Read())
            {
                string obj = dataReader.GetString("couplingId");
                couplingTypes.Add(obj);
                obj = dataReader.GetString("id");
                id.Add(obj);
                obj = dataReader.GetString("mesTime");
                mesTime.Add(obj);
                obj = dataReader.GetString("testItem");
                testItem.Add(obj);
                obj = dataReader.GetString("testTorque");
                testTorque.Add(obj);
                obj = dataReader.GetString("testFrequency");
                testFrequency.Add(obj);
                obj = dataReader.GetString("testStiffness");
                testStiffness.Add(obj);
                obj = dataReader.GetString("torqueRated");
                torqueRated.Add(obj);
                obj = dataReader.GetString("maxSpeed");
                maxSpeed.Add(obj);
                obj = dataReader.GetString("torqueCons");
                torqueCons.Add(obj);
                obj = dataReader.GetString("axialCons");
                axialCons.Add(obj);
                obj = dataReader.GetString("momentIne");
                momentIne.Add(obj);
                obj = dataReader.GetString("mass");
                mass.Add(obj);
                obj = dataReader.GetString("dataDir");
                dataDir.Add(obj);
            }

            cmd = null;
            sqlCon.Close();
            sqlCon = null;
        }

        public void SelectCouplingType(string type)
        {
            couplingInf.Clear();
            couplingTypes.Clear();

            sqlCon = new MySqlConnection(sqlstr);
            sqlCon.Open();

            string sql = string.Format("select * from couplings where id like '{0}';", type);
            //获得MySsqlCommand
            MySqlCommand cmd = new MySqlCommand(sql, sqlCon);

            //执行SQL并且返回查询结果
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //处理返回结果
            while (dataReader.Read())
            {
                couplingInf.Add("id", dataReader.GetString("id"));
                couplingInf.Add("couplingId", dataReader.GetString("couplingId"));
                couplingInf.Add("testStiffness", dataReader.GetString("testStiffness"));
                couplingInf.Add("torqueRated", dataReader.GetString("torqueRated"));
                couplingInf.Add("maxSpeed", dataReader.GetString("maxSpeed"));
                couplingInf.Add("torqueCons", dataReader.GetString("torqueCons"));
                couplingInf.Add("axialCons", dataReader.GetString("axialCons"));
                couplingInf.Add("momentIne", dataReader.GetString("momentIne"));
                couplingInf.Add("mass", dataReader.GetString("mass"));
                couplingInf.Add("dataDir", dataReader.GetString("dataDir"));
            }
            cmd = null;
            sqlCon.Close();
            sqlCon = null;
        }

        public void Delete(Dictionary<string, string> dic)
        {
            sqlCon = new MySqlConnection(sqlstr);
            sqlCon.Open();

            string DeleteSqlCommand = string.Format("delete from couplings where id = '{0}'", dic["id"]);
            MySqlCommand cmd = new MySqlCommand(DeleteSqlCommand, sqlCon);

            int updateCount = cmd.ExecuteNonQuery();
            if (updateCount > 0)
            {
                Console.Write("删除成功");
            }
            else
            {
                Console.Write("删除失败");
            }

            cmd = null;
            sqlCon.Close();
            sqlCon = null;
        }

        public void Add(CouplingCurrent dic)
        {
            //打开连接
            sqlCon = new MySqlConnection(sqlstr);
            sqlCon.Open();

            //SQL语句
            string InsertSqlCommand = string.Format("insert into couplings(couplingId, testItem, testStiffness, torqueRated, " +
                "maxSpeed, torqueCons, axialCons, momentIne, mass, dataDir, testTorque, testFrequency) values('{0}', '{1}', {2}, {3}, {4}, {5}, {6}, {7}, {8}, '{9}', {10}, {11})",
                dic.CouplingId, dic.TestItem, dic.TestStiffness, dic.TorqueRated, dic.MaxSpeed, dic.TorqueCons, dic.AxialCons, dic.MomentIne, dic.Mass, dic.DataDir, dic.TestTorque, dic.TestFrequency);
            Console.WriteLine(InsertSqlCommand);
            MySqlCommand cmd = new MySqlCommand(InsertSqlCommand, sqlCon);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException)
            {
                MessageBox.Show("数据库操作错误");
            }

            cmd = null;
            sqlCon.Close();
            sqlCon = null;
        }
    }
}