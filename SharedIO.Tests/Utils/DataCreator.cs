using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetManager.Model;
using SharedIO.Model;

namespace AssetManager.Tests.Utils
{
    public static class DataCreator
    {
        public static IEnumerable<Transaction> GetTransactions()
        {
            var transactions = new List<Transaction> { 
                new Transaction { Amount = 100},
                new Transaction { Amount = 200},
                new Transaction { Amount = 300}
            };
            return transactions;

        }
 

//        public static IEnumerable<Asset> GetAssets()
//        {
//            var asset = new Asset
//            {
//                Id = 2,
//                OwnerId = 1,
//                Address = "Test",
//                Charges = new List<Charge>() { 
//                    new Charge { AccountNumber = "111", Id = 1}
//                }
//            };
//
//            return new List<Asset>{asset};
//        }
    }
}
