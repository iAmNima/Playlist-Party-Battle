using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PPB;

namespace NUnitTestProject1
{
    class BattleSupportTestClass
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestMakeListFromString() 
        {
            string testString = "R,R,R,R,R";

            string[] setArray = { "R", "R", "R", "R", "R" };
            List<string> setList = new List<string>(setArray);

            Assert.AreEqual(setList,BattleSupport.MakeListFromString(testString));

        }
    }
}
