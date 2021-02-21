using NUnit.Framework;
using PPB;

namespace NUnitTestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestFightDraw() //should return 0
        {
            string set1 = "R,R,R,R,R";

            string set2 = "R,R,R,R,R";

            Assert.AreEqual(0, TournamentManager.Fight(set1, set2));

        }

        [Test]
        public void TestFightDraw2() //should return 0
        {
            string set1 = "L,S,R,P,R";

            string set2 = "L,R,S,R,P";

            Assert.AreEqual(0, TournamentManager.Fight(set1, set2));

        }

        [Test]
        public void TestFightFirstPlayerWin() //should return 1
        {
            string set1 = "V,V,R,P,P";

            string set2 = "S,R,S,V,P";

            Assert.AreEqual(1, TournamentManager.Fight(set1, set2));

        }

        [Test]
        public void TestFightFirstPlayerWin2() //should return 1
        {
            string set1 = "L,L,R,P,P";

            string set2 = "V,P,S,R,S";

            Assert.AreEqual(1, TournamentManager.Fight(set1, set2));

        }

        [Test]
        public void TestFightSecondPlayerWin() //should return 2
        {
            string set1 = "S,R,S,V,P";

            string set2 = "V,V,R,P,P";

            Assert.AreEqual(2, TournamentManager.Fight(set1, set2));

        }

        [Test]
        public void TestFightSecondPlayerWin2() //should return 2
        {
            string set1 = "L,R,P,P,L"; 

            string set2 = "S,R,S,V,S";

            Assert.AreEqual(2, TournamentManager.Fight(set1, set2));

        }

        [Test]
        public void TestCalculateRoundWinnerScissorsWin() //should return 1 (Scissors win)
        {
            string a = "S";

            string b = "P";

            Assert.AreEqual(1, TournamentManager.CalculateRoundWinner(a, b));
        }

        [Test]
        public void TestCalculateRoundWinnerRockWin() //should return 1 (Rock win)
        {
            string a = "R";

            string b = "S";

            Assert.AreEqual(1, TournamentManager.CalculateRoundWinner(a, b));
        }

        [Test]
        public void TestCalculateRoundWinnerPaperWin() //should return 1 (Paper win)
        {
            string a = "P";

            string b = "R";

            Assert.AreEqual(1, TournamentManager.CalculateRoundWinner(a, b));
        }

        [Test]
        public void TestCalculateRoundWinnerLizardWin() //should return 1 (Lizard win)
        {
            string a = "L";

            string b = "V";

            Assert.AreEqual(1, TournamentManager.CalculateRoundWinner(a, b));
        }

        [Test]
        public void TestCalculateRoundWinnerSpockWin() //should return 1 (Spock win)
        {
            string a = "V";

            string b = "R";

            Assert.AreEqual(1, TournamentManager.CalculateRoundWinner(a, b));
        }
    }
}