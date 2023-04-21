using FileReader.Common.Data;
using FileReader.Interfaces.Words;
using FileReader.Services.Logic;
using Moq;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using System;

namespace FileReader.Tests
{
    public class CountsCalculatorTests
    {
        int MaxWordLength = 1000;
        int MaxWordCount = 1000;

        [SetUp]
        public void Setup()
        {
        }

        private ICharsProvider GetICharsProvider()
        {
            var mock = new Mock<ICharsProvider>();
            return mock.Object;
        }

        [Test]
        public async Task CalculateWordCounts_OnEmptyInput_ReturnsNoWords()
        {
            var testObject = new CountsCalculator();

            IProgress<ProcessingState> progress = new Progress<ProcessingState>();

            var charProviderMock = new Mock<ICharsProvider>();
            charProviderMock.Setup(x => x.GetChars(It.IsAny<IProgress<ProcessingState>>(), It.IsAny<CancellationToken>()))
                .Returns(string.Empty.ToAsyncEnumerable());

            ICharsProvider charsProvider = charProviderMock.Object;

            var result = await testObject.CalculateWordCounts(charsProvider, progress, CancellationToken.None);
            
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task CalculateWordCounts_WordNoSpaces_ReturnsOneWord()
        {
            var randomizer = Randomizer.CreateRandomizer();
            string testData = randomizer.GetString(randomizer.Next(MaxWordLength));
            testData.Replace(' ', 'a');


            var testObject = new CountsCalculator();

            IProgress<ProcessingState> progress = new Progress<ProcessingState>();

            var charProviderMock = new Mock<ICharsProvider>();
            charProviderMock.Setup(x => x.GetChars(It.IsAny<IProgress<ProcessingState>>(), It.IsAny<CancellationToken>()))
                .Returns(testData.ToAsyncEnumerable());

            ICharsProvider charsProvider = charProviderMock.Object;

            var result = await testObject.CalculateWordCounts(charsProvider, progress, CancellationToken.None);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task CalculateWordCounts_UniqWords_ReturnsCountOneWords()
        {
            var randomizer = Randomizer.CreateRandomizer();
            var testData = new List<string>();
            var countWords = randomizer.Next(MaxWordCount)+1;
            var i = 0;

            while (i < countWords)
            {
                string data = randomizer.GetString(randomizer.Next(MaxWordLength));
                data.Replace(' ', 'a');

                if (!testData.Contains(data))
                {
                    testData.Add(data);
                    i++;
                }
            }  

            var testObject = new CountsCalculator();

            IProgress<ProcessingState> progress = new Progress<ProcessingState>();

            var charProviderMock = new Mock<ICharsProvider>();
            charProviderMock.Setup(x => x.GetChars(It.IsAny<IProgress<ProcessingState>>(), It.IsAny<CancellationToken>()))
                .Returns(string.Join(" ", testData).ToAsyncEnumerable());

            ICharsProvider charsProvider = charProviderMock.Object;

            var result = await testObject.CalculateWordCounts(charsProvider, progress, CancellationToken.None);

            foreach(var wordCount in result)
            {
                Assert.That(wordCount.Count, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task CalculateWordCounts_UniqWords_ReturnsCorrectWordsCount()
        {
            var randomizer = Randomizer.CreateRandomizer();
            var testData = new List<string>();
            var countWords = randomizer.Next(MaxWordCount)+1;
            var i = 0;

            while (i < countWords)
            {
                string data = randomizer.GetString(randomizer.Next(MaxWordLength));
                data.Replace(' ', 'a');

                if (!testData.Contains(data))
                {
                    testData.Add(data);
                    i++;
                }
            }

            var testObject = new CountsCalculator();

            IProgress<ProcessingState> progress = new Progress<ProcessingState>();

            var charProviderMock = new Mock<ICharsProvider>();
            charProviderMock.Setup(x => x.GetChars(It.IsAny<IProgress<ProcessingState>>(), It.IsAny<CancellationToken>()))
                .Returns(string.Join(" ", testData).ToAsyncEnumerable());

            ICharsProvider charsProvider = charProviderMock.Object;

            var result = await testObject.CalculateWordCounts(charsProvider, progress, CancellationToken.None);

            Assert.That(result.Count, Is.EqualTo(countWords));
        }

        [Test]
        public async Task CalculateWordCounts_DublicateWords_ReturnsSingleWordCorrectCount()
        {
            var randomizer = Randomizer.CreateRandomizer();
            string testword = randomizer.GetString(randomizer.Next(MaxWordLength));
            testword.Replace(' ', 'a');

            var countWords = randomizer.Next(MaxWordCount) + 1;

            var testData = new List<string>();

            for (int i = 0; i <= countWords; i++)
                testData.Add(testword);


            var testObject = new CountsCalculator();

            IProgress<ProcessingState> progress = new Progress<ProcessingState>();

            var charProviderMock = new Mock<ICharsProvider>();
            charProviderMock.Setup(x => x.GetChars(It.IsAny<IProgress<ProcessingState>>(), It.IsAny<CancellationToken>()))
                .Returns(string.Join(" ", testData).ToAsyncEnumerable());

            ICharsProvider charsProvider = charProviderMock.Object;

            var result = await testObject.CalculateWordCounts(charsProvider, progress, CancellationToken.None);

            Assert.That(result.First().Count, Is.EqualTo(countWords));
        }
    }
}