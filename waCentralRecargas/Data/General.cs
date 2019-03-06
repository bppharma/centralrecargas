using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace waCentralRecargas.Data
{
    public class General
    {
        private string connStr = "Uid=luis;Database=centralrecargas;Pwd=NuevoLui5$__;Host=centralsoftware.c7qjl3xbdhqu.us-east-2.rds.amazonaws.com;";
        public void _insertClient(string idusuario)
        {
            MySqlConnection con = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand("insert into clientes(idusuario) values ('"+idusuario+"')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public string _getIdClient(string idusuario)
        {
            string returnValue = "";
            MySqlConnection con = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand("insert into clientes(idusuario) values ('" + idusuario + "')", con);
            con.Open();
            returnValue = cmd.ExecuteScalar().ToString();
            con.Close();
            return returnValue;
        }
    }
}
