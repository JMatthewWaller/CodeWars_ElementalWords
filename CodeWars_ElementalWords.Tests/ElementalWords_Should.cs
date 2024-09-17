using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Emit;
using CodeWars_ElementalWords;
using System;

namespace CodeWars_ElementalWords.Tests
{
    public class Tests
    {
        private readonly List<ElementItem> buildArrayResultElementList = new List<ElementItem>
            {
                new ElementItem { Level = 0, Text = "Sulfur (s)"},
                new ElementItem { Level = 1, Text = "Nitrogen (n)"},
                new ElementItem { Level = 2, Text = "Actinium (ac)"},
                new ElementItem { Level = 4, Text = "Potassium (k)"},
                new ElementItem { Level = 1, Text = "Sodium (na)"},
                new ElementItem { Level = 3, Text = "Carbon (c)"},
                new ElementItem { Level = 4, Text = "Potassium (k)"},
                new ElementItem { Level = 0, Text = "Tin (sn)"},
                new ElementItem { Level = 2, Text = "Actinium (ac)"},
                new ElementItem { Level = 4, Text = "Potassium (k)"}
            };

        private readonly List<ElementItem> resultElementListWithRepeats = new List<ElementItem>
            {
                new ElementItem { Level = 0, Text = "Sulfur (s)"},
                new ElementItem { Level = 1, Text = "Nitrogen (n)"},
                new ElementItem { Level = 2, Text = "Actinium (ac)"},
                new ElementItem { Level = 4, Text = "Potassium (k)"},
                new ElementItem { Level = 0, Text = "Sulfur (s)"},
                new ElementItem { Level = 1, Text = "Sodium (na)"},
                new ElementItem { Level = 3, Text = "Carbon (c)"},
                new ElementItem { Level = 4, Text = "Potassium (k)"},
                new ElementItem { Level = 0, Text = "Tin (sn)"},
                new ElementItem { Level = 2, Text = "Actinium (ac)"},
                new ElementItem { Level = 4, Text = "Potassium (k)"}
            };

        private ElementalWords _elementalWords;

        [SetUp]
        public void Setup()
        {
            _elementalWords = new ElementalWords();

        }

        [Test]
        public void BuildArray_Returns_ExpectedResult()
        {
            List<ElementItem> inputElementList = new List<ElementItem>();

            ElementalWords.BuildArray("snack", 0, inputElementList);

            Assert.That(inputElementList.SequenceEqual(buildArrayResultElementList),"list result not expected");
        }

        [Test]
        public void BuildArray_Returns_EmptyArray()
        {
            List<ElementItem> inputElementList = new List<ElementItem>();

            ElementalWords.BuildArray("snooze", 0, inputElementList);

            Assert.That(inputElementList.Count() == 0, "list result not empty");
        }

        [Test]
        public void FindParentItemIndex_Returns_ExpectedResult()
        {
            List<ElementItem> resultElementList = new List<ElementItem>
            {
                new ElementItem { Level = 0, Text = "Sulfur (s)"},
                new ElementItem { Level = 1, Text = "Nitrogen (n)"},
                new ElementItem { Level = 2, Text = "Actinium (ac)"},
                new ElementItem { Level = 4, Text = "Potassium (k)"},
                new ElementItem { Level = 1, Text = "Sodium (na)"},
                new ElementItem { Level = 3, Text = "Carbon (c)"},
                new ElementItem { Level = 4, Text = "Potassium (k)"},
                new ElementItem { Level = 0, Text = "Tin (sn)"},
                new ElementItem { Level = 2, Text = "Actinium (ac)"},
                new ElementItem { Level = 4, Text = "Potassium (k)"}
            };

            int i = ElementalWords.FindParentItemIndex(1, 4, resultElementList);

            Assert.That(i == 0, "result not expected");
        }

        [Test]
        public void RepeatLowerLevelItems_Returns_ExpectedResult()
        {
            List<ElementItem> resultElementList = new List<ElementItem>
            {
                new ElementItem { Level = 0, Text = "Sulfur (s)"},
                new ElementItem { Level = 1, Text = "Nitrogen (n)"},
                new ElementItem { Level = 2, Text = "Actinium (ac)"},
                new ElementItem { Level = 4, Text = "Potassium (k)"},
                new ElementItem { Level = 1, Text = "Sodium (na)"},
                new ElementItem { Level = 3, Text = "Carbon (c)"},
                new ElementItem { Level = 4, Text = "Potassium (k)"},
                new ElementItem { Level = 0, Text = "Tin (sn)"},
                new ElementItem { Level = 2, Text = "Actinium (ac)"},
                new ElementItem { Level = 4, Text = "Potassium (k)"}
            };

            List<ElementItem> repeatedElementList = new List<ElementItem>();
            repeatedElementList = ElementalWords.RepeatLowerLevelItems(resultElementList);

            Assert.That(repeatedElementList.Count() == buildArrayResultElementList.Count() + 1, "Not added level 0 second time");
            Assert.That(repeatedElementList.SequenceEqual(resultElementListWithRepeats), "list result not expected");
        }

        [Test]
        public void BuildArrayOfStringArraysFromElementList_Returns_ExpectedResult()
        {
            string[][] resultArray = ElementalWords.BuildArrayOfStringArraysFromElementList(resultElementListWithRepeats);

            Assert.That(resultArray[1][0] == "Sulfur (s)", "Not added level 0 second time");
        }
    }
}