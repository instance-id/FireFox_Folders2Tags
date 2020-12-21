// ----------------------------------------------------------------------------
// -- Project : https://github.com/instance-id/FireFox_Folders2Tags          --
// -- instance.id 2020 | http://github.com/instance-id | http://instance.id  --
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace instance.id.FTC
{
    public static class BookmarkHandler
    {
        private static DateTime UnixEpoch = new(1970, 1, 1);
        private static Dictionary<string, List<string>> tagDictionary = new();
        private const string containerType = "text/x-moz-place-container";
        private const string placeType = "text/x-moz-place";

        private static string NameCheck(this Data.Bookmark b) =>
            !string.IsNullOrEmpty(b.Title) ? b.Title : b.Uri;

        public static Data.Bookmark GetBookmarks(FileInfo NewestBookmarkFile, bool debug = false)
        {
            if (NewestBookmarkFile == default(FileInfo))
                return new Data.Bookmark();

            Console.WriteLine($"Processing: {NewestBookmarkFile}");

            try
            {
                var Bookmarks = Data.Bookmark.Create(NewestBookmarkFile.FullName);

                if (Bookmarks.Type != containerType)
                    return new Data.Bookmark();

                var currentItem = "";
                try
                {
                    var index = 0;

                    void GetChildren(IEnumerable<Data.Bookmark> children, int i, List<string> parent)
                    {
                        var childObjects = children as Data.Bookmark[] ?? children.ToArray();
                        foreach (var x in childObjects)
                        {
                            switch (x.Type)
                            {
                                case placeType:
                                    var tagString = parent?.ToArray()
                                        .Aggregate(new StringBuilder(), (current, next) => current.Append(current.Length == 0 ? "" : ",").Append(next))
                                        .ToString();

                                    if (parent != null && parent.Count != 0) x.Tags = tagString;

                                    if (debug) Console.WriteLine($"iter:{i + 2} : [{parent[index]}] : '{x.NameCheck()}' : index {x.Index} : Tags: '{tagString}' : {x.Uri}");
                                    break;
                                case containerType:
                                    currentItem = $"iter:{i + 2} : {x.Type} : [{parent?[index]}] : '{x.NameCheck()}' : index {x.Index} : {x.Uri}";
                                    if (x.Children == null || x.Children.Length == 0) continue;

                                    var parentTitle = new List<string> {x.Title};
                                    parentTitle.AddRange(parent);

                                    if (debug)
                                    {
                                        var parentString = parentTitle
                                            .Aggregate(new StringBuilder(), (current, next) => current.Append(current.Length == 0 ? "" : ",").Append(next))
                                            .ToString();
                                        var childCount = x.Children.Length.ToString();
                                        Console.WriteLine($"iter:{i + 2} : [{parent[index]}] : '{x.NameCheck()}' : index {x.Index} :  Children: {childCount} Parents:{parentString}");
                                    }

                                    GetChildren(x.Children, i + 1, parentTitle);
                                    break;
                            }
                        }
                    }

                    foreach (var x in Bookmarks.Children)
                    {
                        if (x == null || !x.Children.Any()) continue;
                        var childCount = x.Children.Length;
                        if (childCount == 0) continue;
                        if (debug) Console.WriteLine($"iter:{0} : {x.NameCheck()} : index {x.Index} : {x.Uri}");

                        foreach (var b in x.Children)
                        {
                            if (b.Children == null)
                            {
                                if (debug) Console.WriteLine($"iter:{1} : {b.NameCheck()} : index {b.Index} : {b.Uri}");
                                continue;
                            }

                            childCount = x.Children.Length;
                            if (debug) Console.WriteLine($"iter:{1} : {b.NameCheck()} : index {b.Index} : Children: {childCount.ToString()}");

                            var parentList = new List<string> {b.Title};
                            if (b.Children != null && b.Children.Length > 0)
                                GetChildren(b.Children, index = 0, parentList.ToList());
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{currentItem} {e}");
                    throw;
                }

                return Bookmarks;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Data.Bookmark();
            }
        }


    }
}
