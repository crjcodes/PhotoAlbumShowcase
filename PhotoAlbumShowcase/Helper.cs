using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("UnitTests")]
namespace PhotoAlbumShowcase
{
    // this is more of an old-school, unix style approach suitable for console apps
    public enum ErrCodes { 
        HELPED = -1,
        NO_ARG = -2,
        INVALID_ARG = -3,
        INVALID_ALBUM_ID = -4,
    };


    /// <summary>
    /// Methods to help manipulate, validate, process incoming arguments
    /// </summary>
    internal static class Helper
    {
        /// <summary>
        /// Validates the arguments given; nominal path, returns the album id
        /// </summary>
        /// <param name="args">the command-line arguments given</param>
        /// <returns>-1 if errored and should exit, otherwise the album id to display</returns>
        public static int ProcessArgs(string[] args)
        {
            if (args.Length < 1)
            {
                ManPage.WriteManPage();

                return (int) ErrCodes.NO_ARG;
            }

            // allows loose formatting of argument label:
            // double prefix, single prefix, or no prefix
            var listOfArgs = args.ToList().Select(
                a => string.IsNullOrWhiteSpace(a) ? a : a.TrimStart('-'));

            if (listOfArgs.All(a => string.IsNullOrWhiteSpace(a)))
            {
                ManPage.WriteManPage();

                // tracking whether the console app should exit, structured such that
                // we can unit test without the exit

                return (int) ErrCodes.NO_ARG;
            }

            // because an album id could be zero or greater (assumption), using negative return
            // codes to signal termination if needed
            if (listOfArgs.Contains("help", StringComparer.InvariantCultureIgnoreCase))
            {
                ManPage.WriteManPage();

                return (int)ErrCodes.HELPED;
            }

            // a more robust solution would be to look for multiple arguments of
            // album={int} on the command line

            var regex = new Regex(@"album=[0-9]+$", RegexOptions.IgnoreCase);
            var albums = listOfArgs.Where(a => regex.IsMatch(a));

            if (albums == null || !albums.Any())
            {
                ManPage.WriteManPage();

                return (int)ErrCodes.INVALID_ALBUM_ID;
            }

            var album = albums.First()?.Replace("album=", "");
            var success = int.TryParse(album, out var albumId);

            if (!success)
            {
                ManPage.WriteManPage();

                return (int)ErrCodes.INVALID_ALBUM_ID;
            }

            return albumId;
        }

        /// <summary>
        /// Maps error codes to readable strings
        /// This isn't necessary for this simple showcase, but it does make unit 
        /// testing easier.
        /// </summary>
        /// <param name="errCodes"></param>
        /// <returns></returns>
        public static string UserMessage(ErrCodes errCodes)
        {
            return errCodes switch
            {
                ErrCodes.NO_ARG => "Must provide an argument.",
                ErrCodes.INVALID_ARG => "Invalid format. Please provide a valid argument.\"",
                ErrCodes.INVALID_ALBUM_ID => "Must give a valid album identifier.",
                _ => "Unknown error."
            };
        }
    }
}
