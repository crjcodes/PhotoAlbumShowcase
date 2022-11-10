using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]
namespace PhotoAlbumShowcase
{
    internal static class ManPage
    {
        internal static void WriteManPage()
        {
            Console.WriteLine("\nPhotoAlbumShowcase\n");
            Console.WriteLine("\t --help  displays this screen");
            Console.WriteLine("\t --album={integer}  displays the information for the given photo album identifier");
        }
    }
}
