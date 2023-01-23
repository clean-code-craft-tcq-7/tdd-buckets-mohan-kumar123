namespace SequenceAlgorithm.Model
{
    public interface ISequenceProcessor
    {
        string CheckConcequtiveNumber(int[] input);
        bool CheckLengthOfInput(int[] input);
        string ProcessInput(int[] input);
        int[] SortTheArray(int[] input);
    }
}