using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Task2.IList;

namespace Tasks.Tests
{
    /// <summary>
    /// Тестирование работоспособности класса MyList<T>
    /// </summary>
    [TestClass]
    public class Task2Tests
    {
        [TestMethod]
        [TestCategory("Свойства MyList<T>")]
        public void EmptyList()
        {
            MyList<int> check = new MyList<int>();
            Assert.AreEqual(0,check.Count);
        }

        [TestMethod]
        [TestCategory("Свойства MyList<T>")]
        public void CorrectCount()
        {
            MyList<int> check = new MyList<int>();
            check.Add(1);
            check.Add(5);
            check.Add(7);
            Assert.AreEqual(3, check.Count);
        }

        [TestMethod]
        [TestCategory("Значения MyList<T>")]
        public void CorrectListValueFromIndex()
        {
            MyList<int> check = new MyList<int>();
            check.Add(1);
            check.Add(5);
            check.Add(7);
            Assert.AreEqual(5, check[1]);
        }

        [TestMethod]
        [TestCategory("Значения MyList<T>")]
        public void ListIndexOf()
        {
            MyList<int> check = new MyList<int>();
            check.Add(1);
            check.Add(5);
            check.Add(7);
            Assert.AreEqual(1, check.IndexOf(5));
        }

        [TestMethod]
        [TestCategory("Значения MyList<T>")]
        public void MethodContainsIsTrue()
        {
            MyList<int> check = new MyList<int>();
            check.Add(1);
            check.Add(5);
            check.Add(7);
            Assert.IsTrue(check.Contains(5));
        }

        [TestMethod]
        [TestCategory("Значения MyList<T>")]
        public void MethodContainsIsFalse()
        {
            MyList<int> check = new MyList<int>();
            check.Add(1);
            check.Add(5);
            check.Add(7);
            Assert.IsFalse(check.Contains(123));
        }

        [TestMethod]
        [TestCategory("Значения MyList<T>")]
        public void MethodClear()
        {
            MyList<int> check = new MyList<int>();
            check.Add(1);
            check.Add(5);
            check.Add(7);
            bool IsNotEmpty = check.Count > 0;
            check.Clear();
            Assert.IsTrue(IsNotEmpty && check.Count == 0);
        }

        [TestMethod]
        [TestCategory("Значения MyList<T>")]
        public void CheckEnumerator()
        {
            MyList<int> check = new MyList<int>();
            check.Add(1);
            check.Add(5);
            check.Add(7);
            int[] checkArray = new int[3];
            int i = 0;
            foreach(int value in check)
            {
                checkArray[i] = value;
                i++;
            }
            int[] expected = { 1, 5, 7 };
            CollectionAssert.AreEqual(expected, checkArray);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestCategory("Исключения MyList<T>")]
        public void AccessToWrongIndex()
        {
            MyList<int> check = new MyList<int>();
            check.Add(1);
            check.Add(5);
            check.Add(7);
            int tmp = check[8];
        }

        [TestMethod]
        [TestCategory("Исключения MyList<T>")]
        public void CreateLargeList()
        {
            MyList<int> check = new MyList<int>();
            for(int i=0;i<1000000;i++)
            {
                check.Add(i);
            }
            Assert.AreEqual(1000000, check.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestCategory("Исключения MyList<T>")]
        public void InsertToWrongIndex()
        {
            MyList<int> check = new MyList<int>();
            check.Add(1);
            check.Add(5);
            check.Add(7);
            check.Insert(65,3);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestCategory("Исключения MyList<T>")]
        public void RemoveFromWrongIndex()
        {
            MyList<int> check = new MyList<int>();
            check.Add(1);
            check.Add(5);
            check.Add(7);
            check.RemoveAt(65);
        }

        [TestMethod]
        [TestCategory("Изменения MyList<T>")]
        public void MethodRemove()
        {
            MyList<int> check = new MyList<int>();
            check.Add(1);
            check.Add(5);
            check.Add(7);
            check.Remove(5);
            Assert.IsTrue(check.Count == 2 && !check.Contains(5) && check[1] == 7);
        }

        [TestMethod]
        [TestCategory("Изменения MyList<T>")]
        public void MethodkInsert()
        {
            MyList<int> check = new MyList<int>();
            check.Add(1);
            check.Add(5);
            check.Add(7);
            check.Insert(2,48);
            Assert.IsTrue(check.Count == 4 && check[2] == 48 && check[3] == 7);
        }

        [TestMethod]
        [TestCategory("Копирование MyList<T>")]
        public void kCopyToArrayWithEqualSize()
        {
            MyList<int> check = new MyList<int>();
            check.Add(1);
            check.Add(5);
            check.Add(7);
            check.Add(13);
            check.Add(0);
            int[] checkArray = new int[check.Count];
            check.CopyTo(checkArray,0);
            int[] arrayMustLooksLike = { 1, 5, 7, 13, 0 };
            CollectionAssert.AreEqual(checkArray, arrayMustLooksLike);
        }

        [TestMethod]
        [TestCategory("Копирование MyList<T>")]
        public void CopyToArrayWithNonEqualSize()
        {
            MyList<int> check = new MyList<int>();
            check.Add(1);
            check.Add(5);
            check.Add(7);
            check.Add(13);
            check.Add(0);
            check.Add(69);
            int[] checkArray = new int[3];
            check.CopyTo(checkArray, 0);
            int[] arrayMustLooksLike = { 1, 5, 7};
            CollectionAssert.AreEqual(checkArray, arrayMustLooksLike);
        }

        [TestMethod]
        [TestCategory("Копирование MyList<T>")]
        public void CopyToArrayFromCustomIndex()
        {
            MyList<int> check = new MyList<int>();
            check.Add(1);
            check.Add(5);
            check.Add(7);
            check.Add(13);
            check.Add(0);
            check.Add(69);
            int[] checkArray = new int[10];
            check.CopyTo(checkArray, 3);
            int[] arrayMustLooksLike = { 0, 0, 0, 1, 5, 7, 13, 0 ,69, 0 };
            CollectionAssert.AreEqual(checkArray, arrayMustLooksLike);
        }
    }
}
