using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waCentralRecargas.Controllers.Services
{
    public class SellClass
    {
        private Data.PDV PDV = new Data.PDV();
        PrepApi prepApi = new PrepApi();
        TaeApi taeApi = new TaeApi();
        public bool _DoSell { get; set; }
        public bool _isValid { get; set; }
        public string _idVenta { get; set; }
        public string _ticket { get; set; }
        public void SellingRecarga(string idCarrier, string idServiceDetail, string referencia, string idCliente)
        {
            _DoSell = false;
            _isValid = true;
            //validate my balance
            string amountSell = PDV._getAmountService(idServiceDetail);
            //validate client balance
            string clientBalance = PDV._getBalanceAndMoveClient(idCliente);
            string fee = clientBalance.Split('|')[2];
            string balance = clientBalance.Split('|')[0];
            string comision = clientBalance.Split('|')[1];
            double suma = Convert.ToDouble(balance) + Convert.ToDouble(comision);
            string typeMov = "";
            if ((Convert.ToDouble(amountSell) / (1 + (Convert.ToDouble(fee)))) > (Convert.ToDouble(balance)))
            {
                if ((Convert.ToDouble(amountSell) / (1 + (Convert.ToDouble(fee)))) > Convert.ToDouble(comision))
                {
                    if ((Convert.ToDouble(amountSell) / (1 + (Convert.ToDouble(fee)))) > suma)
                    {
                        _isValid = false;
                        return;
                    }
                    else
                    {
                        typeMov = "3";
                    }
                }
                else
                {
                    typeMov = "2";
                }
            }
            else
            {
                typeMov = "1";
            }
            //validate which supplier to ask
            string idSupplier = PDV._getIdSupplier((Convert.ToDouble(amountSell) / (1 + (Convert.ToDouble(fee)))).ToString(), fee);
            _idVenta= PDV._saveSell(idCliente, idServiceDetail);
            //ask for service
            switch (idSupplier)
            {
                case "1":
                    string transactionId = "";
                    Models.TaecelModel.RequestTXNModel requestTXN = taeApi.SendServiceRE("producto", amountSell, referencia, "1");
                    if (requestTXN.success)
                    {
                        if (requestTXN.error.Equals(0))
                        {
                            transactionId = requestTXN.data.transID;
                            if (!transactionId.Contains("|"))
                            {
                                //save and check
                                PDV._updateTrans(_idVenta, requestTXN.data.transID);
                                System.Threading.Thread.Sleep(2000);
                                Models.TaecelModel.StatusTXNRE StatusTXNRE = taeApi.GetStatusTXNRE(requestTXN.data.transID,"1");
                                if (StatusTXNRE.error.Equals(0) || StatusTXNRE.message.Equals("Recarga Exitosa"))
                                {
                                    //save movement
                                    PDV._updateSell(_idVenta, referencia, "", typeMov);
                                    _ticket = "Recarga " + StatusTXNRE.data.Carrier + " exitosa. Folio:" + StatusTXNRE.data.Folio + " # Tel. " + StatusTXNRE.data.Telefono + " Monto:" + StatusTXNRE.data.Monto + " Hora:" + StatusTXNRE.data.Fecha + " ";
                                    _isValid = true;_DoSell = true;
                                }
                                else
                                {
                                    PDV._updateSell(_idVenta, referencia, "", "4");
                                    _ticket= "Error, transacción # " + _idVenta + " " + StatusTXNRE.message;
                                    _isValid = false; _DoSell = true;
                                }
                            }
                            else
                            {
                                //save error and return sellId
                                PDV._updateSell(_idVenta, referencia, "", "4");
                                _ticket = "Error, transacción # " + _idVenta + " " + transactionId.Split('|')[1];
                                _isValid = false;
                                _DoSell = true;
                            }
                        }
                        else
                        {
                            PDV._updateSell(_idVenta, referencia, "", "0");
                            _ticket = "Error, transacción # " + _idVenta + " " + " Error en la respuesta del servicio, favor de reportar a soporte.";
                            _isValid = false;
                            _DoSell = true;
                        }
                    }
                    break;
            }
            //validate answer
        }
    }
}
