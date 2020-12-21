# FireFox_Folders2Tags  

An application that takes in the path to a Firefox 'bookmarks-YYYY-MM-DD.json' backup file as a parameter and then based on your bookmark folder structure, applies tags to your bookmarks based on the parent folder names,

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

## Usage
First and most important, backup your current bookmarks file. I recommend you take the exported bookmarks json file, make a copy and store that somewhere safe and leave it there just in case. You never know if you might need it one day.

![](https://i.imgur.com/wsEVz7b.png)

Once you have a backup bookmarks-YYYY-MM-DD.json file, either move it into the same folder as FireFox_Folders2Tags.exe and it will find it automatically, or run from the commandline passing in the path of the file:
```powershell
.\FireFox_Folders2Tags.exe "C:\myfiles\bookmarks-2020-12-20.json"
```
Once complete, a new json file will be created with the same name as your input file, but will have .tags added after the date.  

Ex: 
```powershell
Input-File  = bookmarks-2020-12-20.json
Output-File = bookmarks-2020-12-20.tag.json
```

---  

#### Warning: The following restoration step removes all of your current bookmarks and reimports them from the file. This is why it is very important to make sure you keep a copy of your original backup file somewhere safe. 

---
You can then go back to FireFox and select Restore > Choose File as seen below. Then you select the new bookmarks-**.tags.json   

![](https://i.imgur.com/2KBW3tA.png)

![](https://i.imgur.com/Ruesoth.png)


---
## Notes:

| Note | Details |
| :--- | :--- |
| Version | I have tested this in the FireFox Developer version of the browser: version 85.0b3 (64-bit). I cannot say whether it will work on other build types or versions. |
| Blank Folders | I was using a folder on my bookmark bar which has no name, but when I went to reimport it, it was still there, but had no contents. |
| Capacity | This was the max tested, as it is how many bookmarks I had. ![](https://i.imgur.com/KHupJqS.png)  |

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.  
Please make sure to update tests as appropriate (if there are any).

## License
[MIT](https://choosealicense.com/licenses/mit/)

---
![alt text](https://i.imgur.com/cg5ow2M.png "instance.id")
