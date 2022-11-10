// See https://aka.ms/new-console-template for more information
using PhotoAlbumShowcase;
using System.Diagnostics;
using System.Text.RegularExpressions;

// very bare-bones, just-get-the-job-done approach

// VALIDATE AND PROCESS ARGS

var processArgsResult = Helper.ProcessArgs(args);

// a little hacky; not spending more time on this
if (processArgsResult == (int) ErrCodes.HELPED)
{
    ManPage.WriteManPage();
    Environment.Exit(0);
}
else if (processArgsResult < 0)
{
    Console.Error.WriteLine(Helper.UserMessage((ErrCodes) processArgsResult));
    Environment.Exit(processArgsResult);
}

// CALL THE "API"

var result = await AlbumClient.Get(processArgsResult);

// PRESENT THE RESULTS

foreach (var album in result)
{
    var line = string.Format("[{0}] {1}", album.Id, album.Title);
    Console.WriteLine(line);
}






