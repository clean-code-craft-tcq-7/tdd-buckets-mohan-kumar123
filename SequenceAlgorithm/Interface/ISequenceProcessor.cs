using System.Collections.Generic;

namespace SequenceAlgorithm.Model
{
    public interface ISequenceProcessor
    {
        string CheckConcequtiveNumbercheck(List<int> input, bool IsCSVPattern);
        bool CheckLengthOfInput(int[] input);
        string ProcessInput(int[] input, bool IsCSVPattern=false);
        int[] SortTheArray(int[] input);
    }
}