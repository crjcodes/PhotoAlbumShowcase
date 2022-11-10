namespace UnitTests
{
    public class ManPageTests
    {
        private readonly StringWriter _stringWriter = new();

        public ManPageTests()
        {
            Console.SetOut(_stringWriter);
        }

        [Fact]
        public void WriteManPage()
        {
            ManPage.WriteManPage();
            _stringWriter.ToString().ShouldContain("--help  displays this screen");
        }
    }
}