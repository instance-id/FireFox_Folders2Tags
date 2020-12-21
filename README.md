#FireFox_Folders2Tags
An application that takes in the path to a Firefox 'bookmarks-xxxx-xx-xx.json' backup file as a parameter and then based on your bookmark folder structure, applies tags to your bookmarks based on the parent folder names,

Example folder structure => Tags applied to the bookmarks  

```
 Bookmark_Toolbar
     \ Work   
          \ Server 
              \ Web
                  \ Website Login : thewebserver.com   | Tags:[Work,Server,Web]
              \ Email 
                  \ Email Login : theemailserver.com   | Tags:[Work,Server,Email]
      | Dev
          \ dotnet
              \ C# Corner : c-sharpcorner.com          | Tags:[Dev,dotnet]
              | .NET Blog : devblogs.microsoft.com     | Tags:[Dev,dotnet]
          | Go   
              \ GoLang : golangnews.com                | Tags:[Dev,Go]
```
