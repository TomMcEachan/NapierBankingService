namespace NapierUnitTests
{
    [TestClass]
    public class LoadTests
    {
        [TestMethod]
        public void VerifyGetDataFromFileWorks()
        {
            //Arrange
            string  FilePath = @"Assets/Test.csv";
            bool expected;

            NapierBankingService.ApplicationLayer.App app = new NapierBankingService.ApplicationLayer.App();

            //Act
            Dictionary<string, string> data = new Dictionary<string, string>();

            using (var stream = new StreamReader(FilePath))
            {
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                    if (line == null) continue;
                    var words = line.Split(',');
                    data.Add(words[0], words[1]);
                }
            }

            foreach (KeyValuePair<string, string> kvp in data)
            {
                string header = kvp.Key;
                string body = kvp.Value;
            }

            if (data != null)
            {
                expected = true;
            }
            else
            {
                expected = false;
            }

            bool actual = NapierBankingService.DataLayer.LoadData.GetDataFromFile(FilePath, app);

            //Assert
            Assert.AreEqual(expected, actual); 

        }
}   }