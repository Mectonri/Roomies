using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ITI.Roomies.DAL.Tests
{
    public class TransactionGatewayTests
    {
        readonly Random _random = new Random();

        [Test]
        public async Task can_create_find_and_delete_transaction()
        {
            TransactionGateway sut = new TransactionGateway( TestHelpers.ConnectionString );

            string transacDes = TestHelpers.RandomTestName();
            int transacPrice = _random.Next(1,200);
            
        }
    }
}
