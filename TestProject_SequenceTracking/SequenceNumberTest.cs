using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SequenceAlgorithm.Controllers;
using SequenceAlgorithm.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace TestProject_SequenceTracking
{
    public class SequenceNumberTest
    {
        #region ClassData_Input_TestCases

        [Theory, ClassData(typeof(InputData))]
        public void Test_Sequenceprocessor_ForAllCombinations(int[] inputdata, string expectedOutput, bool IsCSVEnabled)
        {
            TestUtility.ProcessorExecutor(inputdata, expectedOutput, IsCSVEnabled);
        }

        [Theory, ClassData(typeof(InputForCSVData))]
        public void Test_Sequenceprocessor_1stComplex_CSV(int[] inputdata, string expectedOutput, bool IsCSVEnabled)
        {
            TestUtility.ProcessorExecutor(inputdata, expectedOutput, IsCSVEnabled);
        } 

        #endregion

        #region Json_Input_TestCases

        [Theory, JsonFileData("data.json", "BasicInput1ForCSV")]
        public void Should_Return_CSVFormattedOutputFirstPattern_From_Sequenceprocessor(string inputdatatext, string expectedOutput, bool IsCSVEnabled)
        {
            TestUtility.ProcessTestInput(inputdatatext, expectedOutput, IsCSVEnabled);
        }
        [Theory, JsonFileData("data.json", "BasicInput2ForCSV")]
        public void Should_Return_CSVFormattedOutputSecondPattern_From_Sequenceprocessor(string inputdatatext, string expectedOutput, bool IsCSVEnabled)
        {
            TestUtility.ProcessTestInput(inputdatatext, expectedOutput, IsCSVEnabled);
        }
        [Theory, JsonFileData("data.json", "BasicInput3ForCSV")]
        public void Should_Return_CSVFormattedOutputThirdPattern_From_Sequenceprocessor(string inputdatatext, string expectedOutput, bool IsCSVEnabled)
        {
            TestUtility.ProcessTestInput(inputdatatext, expectedOutput, IsCSVEnabled);
        }
        [Theory, JsonFileData("data.json", "CheckForNoElement")]
        public void Should_Return_ErrorMessage_From_Sequenceprocessor_For_EmptyArray(string inputdatatext, string expectedOutput, bool IsCSVEnabled)
        {
            TestUtility.ProcessTestInput(inputdatatext, expectedOutput, IsCSVEnabled);
        }    

        #endregion
    }

    internal class TestUtility
    {
        public static void ProcessTestInput(string inputdatatext, string expectedOutput, bool IsCSVEnabled)
        {
            int[] inputdata = ArrayConversion(inputdatatext);
            ProcessorExecutor(inputdata, expectedOutput, IsCSVEnabled);
        }

        public static int[] ArrayConversion(string inputdatatext)
        {
            string[] datas = inputdatatext.Split(",");
            int[] inputdata = NullAndEmptyChecker(inputdatatext, datas);
            int val = 0;
            for (int i = 0; i < datas.Length; i++)
            {
                if (int.TryParse(datas[i], out val))
                {
                    inputdata[i] = int.Parse(datas[i]);
                }
            }

            return inputdata;
        }

        public static int[] NullAndEmptyChecker(string inputdatatext, string[] datas)
        {
            return !string.IsNullOrEmpty(inputdatatext) ? new int[datas.Length] : new int[] { };
        }

        public static void ProcessorExecutor(int[] inputdata, string expectedOutput, bool IsCSVEnabled)
        {
            SequenceProcessor sequenceProcessor = new SequenceProcessor();
            int[] input = inputdata;
            string result = sequenceProcessor.ProcessInput(input, IsCSVEnabled);

            Assert.Matches(expectedOutput, result);
        }

    }

    internal class JsonFileDataAttribute : DataAttribute
    {
        private readonly string _filePath;
        private readonly string _propertyName;

        /// <summary>
        /// Load data from a JSON file as the data source for a theory
        /// </summary>
        /// <param name="filePath">The absolute or relative path to the JSON file to load</param>
        public JsonFileDataAttribute(string filePath)
            : this(filePath, null) { }

        /// <summary>
        /// Load data from a JSON file as the data source for a theory
        /// </summary>
        /// <param name="filePath">The absolute or relative path to the JSON file to load</param>
        /// <param name="propertyName">The name of the property on the JSON file that contains the data for the test</param>
        public JsonFileDataAttribute(string filePath, string propertyName)
        {
            _filePath = filePath;
            _propertyName = propertyName;
        }

        /// <inheritDoc />
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            NullChecker(testMethod);

            // Get the absolute path to the JSON file
            string _filePath1 = GetFileDirectory();

            FileExistsChecker(_filePath1);

            // Load the file
            var fileData = File.ReadAllText(_filePath1);

            if (string.IsNullOrEmpty(_propertyName))
            {
                //whole file is the data
                return JsonConvert.DeserializeObject<List<object[]>>(fileData);
            }

            // Only use the specified property as the data
            var allData = JObject.Parse(fileData);
            var data = allData[_propertyName];
            return data.ToObject<List<object[]>>();
        }

        private string GetFileDirectory()
        {
            var pathrel = Path.GetRelativePath(Directory.GetCurrentDirectory(), _filePath);
            string _filePath1 = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            _filePath1 = Directory.GetParent(_filePath1).FullName;
            _filePath1 = Directory.GetParent(Directory.GetParent(_filePath1).FullName).FullName;
            _filePath1 = _filePath1 + "\\" + pathrel;
            var path = Path.IsPathRooted(_filePath)
                ? _filePath
                : Path.GetRelativePath(Directory.GetCurrentDirectory(), _filePath);
            return _filePath1;
        }

        private static void NullChecker(MethodInfo testMethod)
        {
            if (testMethod == null) { throw new ArgumentNullException(nameof(testMethod)); }
        }

        private static void FileExistsChecker(string _filePath1)
        {
            if (!File.Exists(_filePath1))
            {
                throw new ArgumentException($"Could not find file at path: {_filePath1}");
            }
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
