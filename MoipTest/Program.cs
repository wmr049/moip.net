using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moip.Net.V2;
using Moip.Net.V2.Model;
using Moip.Net;
using System.Net;

namespace MoipTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var v2Client = new V2Client(
                    new Uri("https://sandbox.moip.com.br/"),                
                    "GSQV5MBTBI5ZZG8P8B1NPIS3F1DYJ0YO",
                    "QS7QERT7QEMHMJX79SZYD1QQM4C8TZUFZPXLUNIS"
                    );

                    var pedido = new Pedido()
                    {                    
                        OwnId = "1212",
                        Amount = new Valores()
                        {
                            Currency = CurrencyType.BRL,
                            Subtotals = new Subtotal()
                            {
                                Shipping = 1000
                            }
                        },
                        Items = new List<ItemPedido>()
                        {
                            new ItemPedido()
                            {
                                Product = "Descrição do produto",
                                Quantity = 1,
                                Detail = "Detalhes",
                                Price = 1000
                            }
                        },
                        Customer = new Cliente()
                        {
                            OwnId = "1313",
                            Fullname = "José Silva",
                            Email = "josesilva@acme.com.br",
                            BirthDate = DateTime.Now.Date.AddYears(-18).ToString("yyyy-MM-dd"),
                            TaxDocument = new Documento()
                            {
                                Type = DocumentType.CPF,
                                Number = "65374721054"
                            },
                            Phone = new Telefone()
                            {
                                CountryCode = 55,
                                AreaCode = 11,
                                Number = 999999999
                            },
                            ShippingAddress = new Endereco()
                            {
                                ZipCode = "01234000",
                                Street = "Avenida Faria Lima",
                                StreetNumber = "2927",
                                Complement = "SL 1",
                                City = "São Paulo",
                                District = "Itaim",
                                State = "SP",
                                Country = "BRA"
                            }
                        }
                    };

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                
                var clienteCriado = v2Client.CriarPedido(pedido);

                Console.WriteLine(clienteCriado);

            }
            catch (MoipException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ResponseError.Errors);
            }

        }
    }
}
