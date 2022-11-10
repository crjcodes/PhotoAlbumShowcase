using Shouldly;

namespace UnitTests
{
    public class HelperTests
    {
        public HelperTests()
        {
        }

        #region Validity Checks

        // more rigorous tests would loop through the string array, not just
        // check one argument at a time

        // keeping it simple for the showcase

        [Fact]
        public void ProcessArgs_NoArgs_ShouldErrorExit()
        {
            string[] args = Array.Empty<string>();
            var result = Helper.ProcessArgs(args);

            result.ShouldBe((int) ErrCodes.NO_ARG);
        }

        [Theory]
        [InlineData("--")]
        [InlineData("-")]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void ProcessArgs_EmptyArgs_ShouldErrorExit(string arg)
        {
            var args = new string[1] { arg };
            var result = Helper.ProcessArgs(args);

            result.ShouldBe((int)ErrCodes.NO_ARG);
        }


        [Theory]
        [InlineData("--al")]
        [InlineData("-album=-1")]
        [InlineData("--album=")]
        public void ProcessArgs_InvalidArgs_ShouldErrorExit(string arg)
        {
            var args = new string[1] { arg };
            var result = Helper.ProcessArgs(args);

            result.ShouldBe((int)ErrCodes.INVALID_ALBUM_ID);
        }

        [Theory]
        [InlineData("--album=123OK")]
        [InlineData("--album=123444444444444444444444444444444444444")]
        public void ProcessArgs_InvalidParsedInt_ShouldErrorExit(string arg)
        {
            var args = new string[1] { arg };
            var result = Helper.ProcessArgs(args);

            result.ShouldBe((int)ErrCodes.INVALID_ALBUM_ID);
        }

        #endregion

        [Fact]
        public void ProcessArgs_HelpArg()
        {
            var args = new string[1] { "--help" };
            var result = Helper.ProcessArgs(args);

            result.ShouldBe((int)ErrCodes.HELPED);
        }

        [Theory]
        [InlineData("--help")]
        [InlineData("-help")]
        [InlineData("help")]
        [InlineData("HELP")]
        [InlineData("--Help")]
        public void ProcessArgs_ArgDashDoesNotMatter_StillWorks(string arg)
        {
            var result = Helper.ProcessArgs(new string[1] { arg });
            result.ShouldBe((int)ErrCodes.HELPED);
        }

        [Fact]
        public void ProcessArgs_WhenValidArg_ShouldContinue()
        {
            var result = Helper.ProcessArgs(new string[1] { "--album=1" });
            result.ShouldBe(1);
        }
    }
}
