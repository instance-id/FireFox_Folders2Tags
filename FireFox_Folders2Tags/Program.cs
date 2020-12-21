// ----------------------------------------------------------------------------
// -- Project : https://github.com/instance-id/FireFox_Folders2Tags          --
// -- instance.id 2020 | http://github.com/instance-id | http://instance.id  --
// ----------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Int32;

namespace instance.id.FTC
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            var pathString = ".";

            if (args?.Length != 0)
                pathString = args?[0] ?? Directory.GetCurrentDirectory();

            var path = Debugger.IsAttached
                ? Path.Combine(Directory.GetCurrentDirectory(), "../../../", pathString)
                : Path.Combine(Directory.GetCurrentDirectory(), pathString);

            FileInfo sortedFile;

            if (!path.EndsWith(".json"))
            {
                var fileDirectory = new DirectoryInfo(path);

                if (!fileDirectory.Exists)
                    return; // @formatter:off

                sortedFile = fileDirectory.GetFiles("bookmarks-*-*-*.json").OrderByDescending(BookmarkFile => {
                    try {
                        var splitDate = BookmarkFile.Name.Split('-', '.');
                        TryParse(splitDate[1], out var Year);
                        TryParse(splitDate[2], out var Month);
                        TryParse(splitDate[3], out var Day);
                        return new DateTime(Year, Month, Day); }
                    catch { return DateTime.MinValue; }
                }).FirstOrDefault(); // @formatter:on
            }
            else sortedFile = new FileInfo(path);

            if (sortedFile == null || !sortedFile.Exists)
            {
                Console.WriteLine("File not found. Please either specify a FireFox bookmark.json file or containing folder and try again");
                return;
            }

            Console.WriteLine("Converting bookmark parent folders to tag names...");

            var Bookmarks = BookmarkHandler.GetBookmarks(sortedFile);
            var splitName = sortedFile.Name.Split(".");
            var outputFile = $"{splitName[0]}.tag.json";

            await Task.Run(() => Data.Bookmark.Save(Bookmarks, Path.Combine(path, outputFile)));
            Console.WriteLine($"Processing Complete: {Path.Combine(path, outputFile)}");
        }
    }
}
