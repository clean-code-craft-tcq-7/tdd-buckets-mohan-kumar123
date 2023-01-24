using Microsoft.Extensions.Logging;
using Moq;
using SequenceAlgorithm.Controllers;
using SequenceAlgorithm.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace TestProject_SequenceTracking
{
    public class UnitTest1
    {
        [Theory, ClassData(typeof(InputData))]
        public void Test_Sequenceprocessor_ForAllCombinations(int[] inputdata, string expectedOutput, bool IsCSVEnabled)
        {
            SequenceProcessor sequenceProcessor = new SequenceProcessor();
            int[] input = inputdata;
            string result = sequenceProcessor.ProcessInput(input, IsCSVEnabled);

            Assert.Matches(expectedOutput, result);
        }
        [Theory, ClassData(typeof(InputForCSVData))]
        public void Test_Sequenceprocessor_1stComplex_CSV(int[] inputdata, string expectedOutput, bool IsCSVEnabled)
        {
            SequenceProcessor sequenceProcessor = new SequenceProcessor();
            int[] input = inputdata;
            string result = sequenceProcessor.ProcessInput(input, IsCSVEnabled);

            Assert.Matches(expectedOutput, result);
        }
    }
    public class InputForCSVData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
    {
        new object[] { new int[] { 4, 3, 2, 3, 1 }, "1-4 5/n", true },
        new object[] { new int[] { 4, 5, 6, 7, 8 }, "4-8 5/n", true },
        new object[] { new int[] { 4, 5 }, "4-5 2/n", true },
        
    };

        public IEnumerator<object[]> GetEnumerator()
        { return _data.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }

    public class InputData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
    {
        new object[] { new int[] { 4, 3, 2, 3, 1 }, "1-4 5", false },
        new object[] { new int[] { 4, 5, 6, 7, 8 }, "4-8 5", false },
        new object[] { new int[] { 4, 5 }, "4-5 2", false },
        new object[] { new int[] { 4, 3, 2, 1, 8, 9, 10, 19, 20, 21 }, "1-4 4,8-10 3,19-21 3", false },
        new object[] { new int[] { 4, 3, 2, 1, 11, 23, 4, 31, 32, 45, 46, 47, 52, 53, 54, 8, 9, 10, 19, 20, 21 }, "1-4 5,8-11 4,19-21 3,23-23 1,31-32 2,45-47 3,52-54 3", false },
        new object[] { new int[] { 1, 2, 8, 5, 6, 9, 12, 13, 14, 15, 18, 19, 21, 22, 25, 26, 27, 45, 45, 46, 47 }, "1-2 2,5-6 2,8-9 2,12-15 4,18-19 2,21-22 2,25-27 3,45-47 4", false },

    };

        public IEnumerator<object[]> GetEnumerator()
        { return _data.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }
}
