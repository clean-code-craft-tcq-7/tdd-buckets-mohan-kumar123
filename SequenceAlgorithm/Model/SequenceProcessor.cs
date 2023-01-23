using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SequenceAlgorithm.Model
{
    public class SequenceProcessor : ISequenceProcessor
    {
        public String ProcessInput(int[] input)
        {
            if (this.CheckLengthOfInput(input))
            {
                int[] sortedArray = this.SortTheArray(input);

                return this.CheckConcequtiveNumber(sortedArray);
            }
            return EnumRequestConstants.Response.array_Null_Exception;
        }

        public int[] SortTheArray(int[] input)
        {
            int[] result = input.OrderBy(mk => mk).ToArray();
            return result;
        }

        public bool CheckLengthOfInput(int[] input)
        {
            bool IsEverythingOkay = false;

            if (input.Count() > 0)
                IsEverythingOkay = true;

            return IsEverythingOkay;
        }

        public string CheckConcequtiveNumber(int[] input)
        {
            string resultPattern = string.Empty;

            return resultPattern;
        }
    }
}
