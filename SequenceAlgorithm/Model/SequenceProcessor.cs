using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SequenceAlgorithm.Model
{
    public class SequenceProcessor : ISequenceProcessor
    {
        private string sequenceKeeper = string.Empty;
        private string countseqence = string.Empty;
        int countCheckForFirstpartition = 0;
        private bool track = false;
        string resultPattern = string.Empty;
        public String ProcessInput(int[] input,bool IsCSVPattern = false)
        {
            if (this.CheckLengthOfInput(input))
            {
                int[] sortedArray = this.SortTheArray(input);
                List<int> convertedArray = sortedArray.ToList<int>();
                return this.CheckConcequtiveNumbercheck(convertedArray, IsCSVPattern);
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

        //public string CheckConcequtiveNumber(List<int> input)
        //{
        //    string resultPattern = string.Empty;
        //    int countCheckForFirstpartition = 0;

        //    for (int i = 1; i <= input.Count; i++) // 4,5   3,3,4,5,10,11,12
        //    {
                
        //            if ((i == input.Count) || (input[i] - input[i - 1] == 1))  
        //            {
        //                this.sequenceKeeper += input[i - 1].ToString() + ","; 
        //                countCheckForFirstpartition++;
                        
        //                this.countseqence += countCheckForFirstpartition.ToString();
        //            }
        //            else if (input[i] - input[i - 1] == 0)
        //            {
        //                countCheckForFirstpartition++;
                        
        //                this.countseqence += countCheckForFirstpartition.ToString();
        //            }
        //            else
        //            {
        //                this.sequenceKeeper += input[i - 1].ToString() + " ";

        //                input = input.Skip(i).ToList();
        //                countCheckForFirstpartition++;
                      
        //                this.countseqence += countCheckForFirstpartition.ToString() + ",";
                   
        //                CheckConcequtiveNumber(input); 
        //            }
                       
        //    }
        //    input.Clear();
        //    resultPattern = GeneratePattern();
        //    return resultPattern;
        //}

        public string GeneratePattern(bool IsCSVPattern = false)
        {
            

            List<string> getIndividualSeq = new List<string>();
            List<string> getIndividualCount = new List<string>();
            
            this.sequenceKeeper.Split(" ").ToList().ForEach(mk => getIndividualSeq.Add(mk.TrimEnd(',')));
            this.countseqence.Split(",").ToList().ForEach(mk => getIndividualCount.Add(mk.Count().ToString()));

            for (int i = 0; i < getIndividualSeq.Count; i++)
            {
                PatternGeneratorByFormat(IsCSVPattern, i, getIndividualSeq, getIndividualCount);
            }
            return resultPattern.TrimEnd(',');
        }
        public void PatternGeneratorByFormat(bool IsCSVPattern, int i, List<string> getIndividualSeq, List<string> getIndividualCount)
        {
            string first = getIndividualSeq[i].Split(",")[0].ToString();
            string last = getIndividualSeq[i].Split(",")[getIndividualSeq[i].Split(",").Count() - 1].ToString();
            resultPattern += IsCSVPattern ? string.Format("{0}-{1} {2}/n", first, last, getIndividualCount[i]) + "," : string.Format("{0}-{1} {2}", first, last, getIndividualCount[i]) + ",";
        }
        public void CheckFirstPattern(int i, List<int> input)
        {
             this.countCheckForFirstpartition = 0;
            if ((i == input.Count) || (input[i] - input[i - 1] == 1))  
            {
                this.sequenceKeeper += input[i - 1].ToString() + ",";

                //countCheckForFirstpartition++;
                //this.countseqence += countCheckForFirstpartition.ToString();
                //this.track = true;
                this.CountSequence(1);
            }
        }
        public void CheckSecondPattern(int i, List<int> input)
        {
            this.countCheckForFirstpartition = 0;
            if (input[i] - input[i - 1] == 0)
            {
                //countCheckForFirstpartition++;
                //this.countseqence += countCheckForFirstpartition.ToString();
                //this.track = true;
                this.CountSequence(2);
            }
        }
        public List<int> CheckThirdPattern(int i, List<int> input)
        {
            this.countCheckForFirstpartition = 0;
            this.sequenceKeeper += input[i - 1].ToString() + " ";
            input = input.Skip(i).ToList();
            //countCheckForFirstpartition++;
            //this.countseqence += countCheckForFirstpartition.ToString() + ",";
            this.CountSequence(3);
            return input;
        }
        public void CountSequence(int pattern)
        {
            if (pattern > 2)
            {
                countCheckForFirstpartition++;
                this.countseqence += countCheckForFirstpartition.ToString() + ",";
            }
            else
            {
                countCheckForFirstpartition++;
                this.countseqence += countCheckForFirstpartition.ToString();
                this.track = true;
            }
        }
        public void ExecuteFirstpattern(int i, List<int> input)
        {
            if (!this.track)
            {
                this.CheckFirstPattern(i, input);
            }
        }
        public void ExecuteSecondpattern(int i, List<int> input)
        {
            if (!this.track)
            {
                this.CheckSecondPattern(i, input);
            }
        }
        public List<int> ExecuteThirdpattern(ref int i, List<int> input)
        {
            if (!this.track)
            {
                input = this.CheckThirdPattern(i, input);
                i = 0;
            }
           
            return input;
        }

        public string CheckConcequtiveNumbercheck(List<int> input, bool IsCSVPattern)
        {
            string resultPattern = string.Empty;
            var samo = this.sequenceKeeper;
            for (int i = 1; i <= input.Count; i++) 
            {
                this.track = false;
                this.ExecuteFirstpattern(i, input);
                this.ExecuteSecondpattern(i, input);
                input = this.ExecuteThirdpattern(ref i, input);

            }
            input.Clear();
            resultPattern = GeneratePattern(IsCSVPattern);
            return resultPattern;
        }

        //public int GetLeastpositiveIntegre(int[] A)
        //{
        //   A = this.SortTheArray(A);
        //    bool incrementNext = false;
        //    int captureword=0;
        //    int resultdata = 0;
        //    bool conditioncheck = false;
        //    //112346
        //    //1245
        //    List<int> result = new List<int>();
        //    A.ToList().ForEach(mk => conditioncheck = mk > 0);
        //    if (conditioncheck)
        //    {
        //        for (int i = 0; i < A.Length; i++)
        //        {
        //            if ((i == A.Length - 1) || (A[i] + 1 == A[i + 1]) || (A[i] == A[i + 1]))
        //            {

        //            }
        //            else
        //            {
        //                result.Add(A[i] + 1);
        //                incrementNext = true;
        //                break;
        //            }
        //            captureword = !incrementNext ? A[i] + 1 : 0;
        //        }
        //        if (captureword > 0)
        //        {
        //            result.Add(captureword);
        //        }
        //        result.Sort();


        //        result.ForEach(mk => resultdata = mk); 
        //    }
        //    else
        //    {
        //        resultdata = 1;
        //    }

        //    return resultdata;
            
        //}
    }
}
