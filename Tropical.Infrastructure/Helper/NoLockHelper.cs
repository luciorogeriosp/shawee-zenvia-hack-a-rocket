using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;    

namespace Tropical.Infrastructure.Helper
{
    public static class NoLockHelper
    {
        public static TransactionScope CreateNoLockTransaction()
        {
            var options = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            };
            return new TransactionScope(TransactionScopeOption.Required, options);
        }


        public static List<T> ToListNoLock<T>(this IEnumerable<T> query)
        {
            if (Transaction.Current == null)
            {
                using (TransactionScope ts = CreateNoLockTransaction())
                {

                    return query.ToList();
                }
            }
            else
                return query.ToList();
        }
    }
}
