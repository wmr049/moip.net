using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moip.Net.V2;
using Moip.Net.V2.Model;
using Moip.Net;
using System.Net;
using Moip.Net.V2.Filter;
using Quartz;
using Quartz.Impl;

namespace MoipTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler, start the schedular before triggers or anything else
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            // create job
            IJobDetail job = JobBuilder.Create<MoipFunctions>()
                    .WithIdentity("processarPedidosMOIP", "grupoMoip")
                    .Build();

            // create trigger
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("gatilhoMOIP", "grupoMoip")
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(30).RepeatForever())
                .Build();

            // Schedule the job using the job and trigger 
            sched.ScheduleJob(job, trigger);            
        }        
    }

    /// <summary>
    /// SimpleJOb is just a class that implements IJOB interface. It implements just one method, Execute method
    /// </summary>
    public class MoipFunctions : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                var v2Client = new V2Client(
                    new Uri("https://sandbox.moip.com.br/"),                
                    "GSQV5MBTBI5ZZG8P8B1NPIS3F1DYJ0YO",
                    "QS7QERT7QEMHMJX79SZYD1QQM4C8TZUFZPXLUNIS"
                    );

            //    1) Criar Pedido
            //    CreateOrder(v2Client);

            //    2) Criar Pedido Com Repasse
            //    CreateOrderRepasse(v2Client);

            //    3) Listar Pedido com filtros
            //    FiltrarPedidos(v2Client);

            //    4) Consultar Pedido
                ConsultarPedido(v2Client);

            //    5) Cancelamento Total
            //    CancelamentoPedido(v2Client);

            //    6) Cancelamento Pagamento
            //    CancelamentoPagamento(v2Client);
                
            }
            catch (MoipException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ResponseError.Errors);
                Console.ReadKey();
            }


        }

        private static Pagamento CancelamentoPagamento(V2Client v2Client)
        {
            var pagamento = "PAY-LZIGN8F3IHL0";
            var pedidoCancelado = v2Client.CancelarPagamentoPreAutorizado(pagamento);

            return pedidoCancelado;
        }

        private static void CancelamentoPedido(V2Client v2Client)
        {
            throw new NotImplementedException();
        }

        private static Pedido CreateOrderRepasse(V2Client v2Client)
        {
            var pedido = new Pedido()
            {
                OwnId = "15151515",
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
                        Product = "Curso Direito Civil",
                        Quantity = 1,
                        Detail = "Curso V18 Direito Civil",
                        Price = 250000
                    }
                },
                Receivers = new List<Recebedores>(){
                    new Recebedores()
                    {
                        Type = ReceiverType.SECONDARY,
                        FeePayor = true,
                        Amount = new ValoresRecebedor
                        {
                            Fixed = 20000
                        },
                        moipAccount = new ContaMoip()
                        {
                            Id = "MPA-E3C8493A06AE"
                        }
                    }
                },
                Customer = new Cliente()
                {
                    OwnId = "333333",
                    Fullname = "Milton Reis",
                    Email = "wmr049@gmail.com",
                    BirthDate = DateTime.Now.Date.AddYears(-34).ToString("yyyy-MM-dd"),
                    TaxDocument = new Documento()
                    {
                        Type = DocumentType.CPF,
                        Number = "30877030871"
                    },
                    Phone = new Telefone()
                    {
                        CountryCode = 55,
                        AreaCode = 19,
                        Number = 992129963
                    },
                    ShippingAddress = new Endereco()
                    {
                        ZipCode = "13145888",
                        Street = "Rua 6",
                        StreetNumber = "138",
                        Complement = "Campos do Conde 2",
                        City = "Paulinia",
                        District = "",
                        State = "SP",
                        Country = "BRA"
                    }
                }
            };

            var clienteCriado = v2Client.CriarPedido(pedido);
            Console.WriteLine(clienteCriado);


            return clienteCriado;
        }

        private static Pedido ConsultarPedido(V2Client v2Client)
        {
            var pedido = "ORD-5WAX9D2BC53I";
            var pedidoConsultado = v2Client.ConsultarPedido(pedido);
            Console.WriteLine(pedidoConsultado.Id);

            return pedidoConsultado;
        }

        private static Pedido CreateOrder(V2Client v2Client)
        {
            var pedido = new Pedido()
            {
                OwnId = "14141414",
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
                                Product = "Curso Direito Civil",
                                Quantity = 1,
                                Detail = "Curso V18 Direito Civil",
                                Price = 250000
                            }
                        },
                Customer = new Cliente()
                {
                    OwnId = "222222",
                    Fullname = "Milton Reis",
                    Email = "wmr049@gmail.com",
                    BirthDate = DateTime.Now.Date.AddYears(-34).ToString("yyyy-MM-dd"),
                    TaxDocument = new Documento()
                    {
                        Type = DocumentType.CPF,
                        Number = "30877030871"
                    },
                    Phone = new Telefone()
                    {
                        CountryCode = 55,
                        AreaCode = 19,
                        Number = 992129963
                    },
                    ShippingAddress = new Endereco()
                    {
                        ZipCode = "13145888",
                        Street = "Rua 6",
                        StreetNumber = "138",
                        Complement = "Campos do Conde 2",
                        City = "Paulinia",
                        District = "",
                        State = "SP",
                        Country = "BRA"
                    }
                }
            };

            var clienteCriado = v2Client.CriarPedido(pedido);
            Console.WriteLine(clienteCriado);


            return clienteCriado;
        }

        private static Pedidos FiltrarPedidos(V2Client v2Client)
        {
            //Listar todos os pedidos pagos e criados com data superior a 01/01/2016
            var filters = new Filters()
                .Add(new GreatherThanFilter<DateTime>("createdAt", new DateTime(2016, 01, 01)))
                .Add(new InFilter<OrderStatusType>("status", OrderStatusType.CREATED, OrderStatusType.PAID));

            var pedidos = v2Client.ListarTodosPedidos(filters: filters);

            return pedidos;
        }
    }
}
