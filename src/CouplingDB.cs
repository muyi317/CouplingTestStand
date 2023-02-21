#define MySQLCommand

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
    class CouplingDB
    {
        /// <summary>
        /// 服务器地址；端口号；数据库；用户名；密码
        /// </summary>
        /// //SQLBulkCopy
        string sqlstr = "Data Source=localHost;User ID=root;Password=114114;DataBase=couplings;";
        MySqlConnection sqlCon;

        public List<string> couplingTypes = new List<string>();
        public Dictionary<string, string> couplingInf = new Dictionary<string, string>();

        public void Select()
        {
            couplingTypes.Clear();

            sqlCon = new MySqlConnection(sqlstr);
            sqlCon.Open();

            string sql = "select * from couplingType order by 'couplingId';";
            //获得MySsqlCommand
            MySqlCommand cmd = new MySqlCommand(sql, sqlCon);

            //执行SQL并且返回查询结果
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //处理返回结果
            while (dataReader.Read())
            {
                //string obj = dataReader.GetString(0);
                string obj = dataReader.GetString("couplingId");
                //Console.WriteLine(dataReader);
                couplingTypes.Add(obj);
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

            string sql = string.Format("select * from couplingType where couplingId like '{0}';", type);
            //获得MySsqlCommand
            MySqlCommand cmd = new MySqlCommand(sql, sqlCon);

            //执行SQL并且返回查询结果
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //处理返回结果
            while (dataReader.Read())
            {
                couplingInf.Add("couplingId", dataReader.GetString("couplingId"));
                couplingInf.Add("torqueRated", dataReader.GetString("torqueRated"));
                couplingInf.Add("maxSpeed", dataReader.GetString("maxSpeed"));
                couplingInf.Add("torqueCons", dataReader.GetString("torqueCons"));
                couplingInf.Add("axialCons", dataReader.GetString("axialCons"));
                couplingInf.Add("momentIne", dataReader.GetString("momentIne"));
                couplingInf.Add("mass", dataReader.GetString("mass"));
            }
            cmd = null;
            sqlCon.Close();
            sqlCon = null;
        }

        public void Delete(Dictionary<string, string> dic)
        {
            sqlCon = new MySqlConnection(sqlstr);
            sqlCon.Open();

            string DeleteSqlCommand = string.Format("delete from couplingType where couplingId = '{0}'", dic["couplingId"]);
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

        public void Update(CouplingCurrent dic)
        {
            //打开连接
            sqlCon = new MySqlConnection(sqlstr);
            sqlCon.Open();

            string UpdateSqlCommand = string.Format("update couplingType set couplingId = '{0}', torqueRated = {1}, " +
                "maxSpeed = {2}, torqueCons = {3}, axialCons = {4}, momentIne = {5}, mass = {6} where couplingId = '{0}'",
                dic.CouplingId, dic.TorqueRated, dic.MaxSpeed, dic.TorqueCons, dic.AxialCons, dic.MomentIne, dic.Mass);
            MySqlCommand cmd = new MySqlCommand(UpdateSqlCommand, sqlCon);

            // 执行SQL
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(MySqlException e)
            {
                MessageBox.Show("数据库操作错误\n"+e);
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
            string InsertSqlCommand = string.Format("insert into couplingType(couplingId, torqueRated, " +
                "maxSpeed, torqueCons, axialCons, momentIne, mass) values('{0}', {1}, {2}, {3}, {4}, {5}, {6})", 
                dic.CouplingId, dic.TorqueRated, dic.MaxSpeed, dic.TorqueCons, dic.AxialCons, dic.MomentIne, dic.Mass);
            Console.WriteLine(InsertSqlCommand);
            MySqlCommand cmd = new MySqlCommand(InsertSqlCommand, sqlCon);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("数据库操作错误\n"+e);
            }

            cmd = null;
            sqlCon.Close();
            sqlCon = null;
        }
    }
}