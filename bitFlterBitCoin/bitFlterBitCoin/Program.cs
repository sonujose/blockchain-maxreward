using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bitFlterBitCoin
{
    class Program
    {
        static void Main()
        {
            BlockDataAccessor Block = new BlockDataAccessor();
            Transaction[] TransactionsList = Block.GetInitialData();

            int TransactionsLength = TransactionsList.Length;            
            long maxTransactions = 500000;                   
            double maxReward = 0;

            int[] array = new int[TransactionsLength];
            for (var i = 0; i < TransactionsLength; ++i)
            {
                array[i] = i;
            }
            List<int[]> output;

            for (int i = TransactionsLength-1; i > 2; i--)
            {
                output = TransactionCalculationsEngine.CombinationsUtil(array, i, 0, new int[0], new List<int[]>());
                long sum = 0;
                foreach (var item in output.ToArray())
                {                    
                    sum = TransactionCalculationsEngine.geTransactionBytes(item, TransactionsList);
                    if(sum <= maxTransactions) {
                        double value = TransactionCalculationsEngine.getTransactionValue(item, TransactionsList);
                        if (value > maxReward) {
                            maxReward = value;                           
                        };
                    }                  
                }
            }
            maxReward = maxReward + 12.5;
            Console.Write("Max possible reward for creating a block is {0}", maxReward);           
            Console.ReadLine();
        }        
    }
    
    /// <summary>
    /// POCO for transaction
    /// </summary>
    public class Transaction
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public double Fee { get; set; }
    }

    public static class TransactionCalculationsEngine
    {   
        /// <summary>
        /// Method to genearte possible combinations for particular size
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="size"></param>
        /// <param name="start"></param>
        /// <param name="initialStuff"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static List<int[]> CombinationsUtil(int[] arr, int size, int start, int[] initialStuff, List<int[]> output)
        {

            if (initialStuff.Length >= size)
            {
                output.Add(initialStuff);
            }
            else
            {

                for (int i = start; i < arr.Length; i++)
                {
                    var currentStuff = initialStuff.ToList();
                    currentStuff.Add(arr[i]);
                    output = CombinationsUtil(arr, size, i + 1, currentStuff.ToArray(), output);
                }
            }

            return output;
        }

        /// <summary>
        /// Returns Transaction Reward for a particular combination
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="Transactions"></param>
        /// <returns></returns>
        public static double getTransactionValue(int[] pattern, Transaction[] Transactions)
        {
            double val = 0;

            foreach (var item in pattern)
            {
                val += Transactions[item].Fee;
            }

            return val;
        }
        
        /// <summary>
        /// Return Size for selected pattern
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="Transactions"></param>
        /// <returns></returns>
        public static long geTransactionBytes(int[] pattern, Transaction[] Transactions)
        {
            long val = 0;

            foreach (var item in pattern)
            {
                val += Transactions[item].Size;
            }

            return val;
        }
    }
    
    /// <summary>
    /// Accessor
    /// </summary>
    public class BlockDataAccessor
    {   
        /// <summary>
        /// Returns Transaction Table
        /// </summary>
        /// <returns></returns>
        public Transaction[] GetInitialData()
        {
            Transaction[] Transactions = new Transaction[] {
                new Transaction {Id = 1, Size = 57247, Fee = 0.0887 },
                new Transaction {Id = 2, Size = 98732, Fee = 0.1856 },
                new Transaction {Id = 3, Size = 134928, Fee = 0.2307 },
                new Transaction {Id = 4, Size = 77275, Fee = 0.1522 },
                new Transaction {Id = 5, Size = 29240, Fee = 0.0532 },
                new Transaction {Id = 6, Size = 15440, Fee = 0.0250 },
                new Transaction {Id = 7, Size = 70820, Fee = 0.1409 },
                new Transaction {Id = 8, Size = 139603, Fee = 0.2541 },
                new Transaction {Id = 9, Size = 63718, Fee = 0.1147 },
                new Transaction {Id = 10, Size = 143807, Fee = 0.2660 },
                new Transaction {Id = 11, Size = 190457, Fee = 0.2933 },
                new Transaction {Id = 12, Size = 40572, Fee = 0.0686 },
            };
            return Transactions;
        }
    }    
}
