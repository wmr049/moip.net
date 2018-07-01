using Moip;
using Moip.Models;
using MoipTest.Models;
using System;
using System.Net;
using System.Web.Http;
using static Moip.Configuration;

namespace MoipTest.Controllers
{
    public class PedidoController : ApiController
    {
        // GET: api/Pediido
        public IHttpActionResult Get()
        {
            Client client = new Client("R1NRVjVNQlRCSTVaWkc4UDhCMU5QSVMzRjFEWUowWU86UVM3UUVSVDdRRU1ITUpYNzlTWllEMVFRTTRDOFRaVUZaUFhMVU5JUw==", Environments.SANDBOX);

            var mockPedido = new MockPedido();
            OrderResponse createdOrder = null;

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                createdOrder = client.Orders.CreateOrder(mockPedido.orderRequest);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }            

            return Ok(createdOrder);
        }

        // GET: api/Pediido/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Pediido
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Pediido/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Pediido/5
        public void Delete(int id)
        {
        }
    }
}
