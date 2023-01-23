using Microsoft.Extensions.Logging;
using Moq;
using SequenceAlgorithm.Controllers;
using SequenceAlgorithm.Model;
using System;
using Xunit;

namespace TestProject_SequenceTracking
{
    public class UnitTest1
    {
        [Fact]
        public void Test_Sequenceprocessor_BasicTest()
        {
            // var moq = new Mock<ISequenceProcessor>();
            //SequenceAlgorthim sequenceAlgorthim = new SequenceAlgorthim(moq.Object);
            SequenceProcessor sequenceProcessor = new SequenceProcessor();
            int[] input = { 4, 5 };
            string result = sequenceProcessor.ProcessInput(input);

            Assert.Matches("4-5, 2", result);
        }


    }
}
