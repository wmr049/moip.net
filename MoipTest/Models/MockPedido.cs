using Moip.Models;
using System.Collections.Generic;

namespace MoipTest.Models
{
    public class MockPedido
    {

        public OrderRequest orderRequest{ get; set; }
        public MockPedido()
        {
            TaxDocument taxDocument = new TaxDocument
            {
                Type = "CPF",
                Number = "22222222222"
            };

            Phone phone = new Phone
            {
                CountryCode = "55",
                AreaCode = "11",
                Number = "66778899"
            };

            ShippingAddress shippingAddress = new ShippingAddress
            {
                Street = "Rua test",
                StreetNumber = "123",
                Complement = "Ap test",
                District = "Bairro test",
                City = "TestCity",
                State = "SP",
                Country = "BRA",
                ZipCode = "01234000"
            };

            CustomerRequest customerRequest = new CustomerRequest
            {
                Fullname = "Fulano de Tal",
                OwnId = "OFulanoDeTal",
                BirthDate = "1990-01-01",
                Email = "fulano@detal.com.br",
                Phone = phone,
                ShippingAddress = shippingAddress,
                TaxDocument = taxDocument
            };

            SubtotalsRequest subtotalsRequest = new SubtotalsRequest
            {
                Shipping = 1500,
                Addition = 20,
                Discount = 10
            };

            AmountOrderRequest amountRequest = new AmountOrderRequest
            {
                Currency = "BRL",
                Subtotals = subtotalsRequest
            };

            Item itemsRequest = new Item
            {
                Product = "Bicicleta Specialized Tarmac 26 Shimano Alivio",
                Quantity = 1,
                Detail = "uma linda bicicleta",
                Price = 2000
            };

            List<Item> itemsRequestList = new List<Item>
            {
                itemsRequest
            };

            orderRequest = new OrderRequest
            {
                OwnId = "12121313",
                Amount = amountRequest,
                Items = itemsRequestList,
                Customer = customerRequest,
            };

        }
    }
}