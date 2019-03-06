using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace waCentralRecargas.Data
{
    public class PDV
    {
        private string connStr = "Uid=luis;Database=centralrecargas;Pwd=NuevoLui5$__;Host=centralsoftware.c7qjl3xbdhqu.us-east-2.rds.amazonaws.com;";
        public List<Models.RecargasViewModel.Montos> GetMontos(string idCarrier)
        {
            MySqlConnection con = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand("select * from servicesdetails where idcarrier=" + idCarrier, con);
            con.Open();
            MySqlDataReader msdr = cmd.ExecuteReader();
            List<Models.RecargasViewModel.Montos> listMontos = new List<Models.RecargasViewModel.Montos>();
            while (msdr.Read())
            {
                Models.RecargasViewModel.Montos montos = new Models.RecargasViewModel.Montos();
                montos.idservdet = msdr.GetValue(0).ToString();
                montos.bondades = msdr.GetValue(2).ToString();
                montos.monto = msdr.GetValue(3).ToString();
                listMontos.Add(montos);
            }
            con.Close();
            return listMontos;
        }
        public string _getIdSupplier(string monto, string fee)
        {
            string queryE = "SELECT max(idsupplier) FROM centralrecargas.supplier where saldo>=" + monto + " and fee>" + fee + " group by saldo";
            string retValue = "";
            MySqlConnection con = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(queryE, con);
            con.Open();
            retValue = cmd.ExecuteScalar().ToString();
            con.Close();
            return retValue;
        }
        public string _getBalanceAndMoveClient(string idCliente)
        {
            string queryE = "SELECT concat(saldo,'|',comision,'|',fee) FROM centralrecargas.cuentas where idcliente=" + idCliente;
            string retValue = "";
            MySqlConnection con = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(queryE, con);
            con.Open();
            retValue = cmd.ExecuteScalar().ToString();
            con.Close();
            return retValue;
        }
        public string _getAmountService(string idServiceDetail)
        {
            string queryE = "SELECT monto FROM centralrecargas.servicesdetails where idservdet=" + idServiceDetail;
            string retValue = "";
            MySqlConnection con = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(queryE, con);
            con.Open();
            retValue = cmd.ExecuteScalar().ToString();
            con.Close();
            return retValue;
        }
        public string _saveSell(string idClient, string idServ)
        {
            string retValue = "";
            MySqlConnection con = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand("insert into movimientos(idcliente,idservdet,fecha)values(" + idClient + "," + idServ + ",now())", con);
            con.Open();
            cmd.ExecuteNonQuery();
            retValue = cmd.LastInsertedId.ToString();
            con.Close();
            return retValue;
        }
        public void _updateTrans(string idVenta,string transId)
        {
            MySqlConnection con = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand("update movimientos set transactionid='"+transId+"' where idmovto="+idVenta, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void _updateSell(string idVenta,string referencia,string referencia2,string tipodescuento)
        {
            MySqlConnection con = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand("update movimientos set referencia='"+referencia+"', referencia2='"+referencia2+"', tipodescuento="+tipodescuento+" where idmovto=" + idVenta, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
