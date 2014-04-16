using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Friendly_folder_icon_customization
{
    class FileDept
    {
        private string _activeDirectory;
        private string _libraryFolder;
        private string _appData;
        private ReferenceCounter _referenceCounter;

        public FileDept()
        {
            /* Setting the directory desktop.ini we'll be reading and writing to */
            var shellArguments = Environment.GetCommandLineArgs();
            _activeDirectory = (shellArguments.Length > 1) ? shellArguments[1] : Environment.CurrentDirectory;

            /* These are the folders that we scan for our images */
            _libraryFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            _appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\QuickFolderIconizer\"; // We'll store our used images here

            _referenceCounter = new ReferenceCounter(_appData);
            _referenceCounter.ReferenceCountIsZero += ReferenceCountIsZero_handler;
        }

        // Returns the path of the active directories icon.
        public string GetFolderIcon()
        {
            return ShellAPI.GetIcon(_activeDirectory);
        }

        public void SetFolderIcon(string filePath, string fileName)
        {
            // If the folder had an icon, it might have been managed by the reference counter
            // so we'll decrement the pervious icons filename to be safe
            var currentFolderIcon = GetFolderIcon();
            if (currentFolderIcon != "")
            {
                _referenceCounter.Decrement(_filenameSubstring(currentFolderIcon));

            }

            var storagePath = _appData + fileName;
            CopyFile(filePath, storagePath);
            ShellAPI.SetIcon(_activeDirectory, storagePath);
            _referenceCounter.Increment(_filenameSubstring(storagePath));
        }

        // Clears the icon from a folder and if it exists in the reference counter, decrements it.
        public void ClearFolderIcon()
        {
            var currentFolderIcon = GetFolderIcon();
            if (currentFolderIcon != "")
            {
                _referenceCounter.Decrement(_filenameSubstring(currentFolderIcon));

            }
            ShellAPI.SetIcon(_activeDirectory, "");
        }

        // Returns the paths of icon files in our storage & library folders.
        public List<string> SearchForIcons()
        {
            var icons = new List<string>();
            var folders = _findDirectories(_libraryFolder);

            // Scan our appdata storage folder for files matching .ico 
            icons = _filterIcons(_appData);

            // Create a copy of the list above but filter its contents to just the filename substring of each icon. 
            // We prefer to show the files in our storage folder over the files in our library, so we're going to pass it to our file scanner function 
            // which will use it to filter out any matching files names from the library search.
            var filterNameTable = icons.Select(_filenameSubstring).ToList();

            // Now we'll scan all the folders inside our library for icons
            foreach (var folder in folders)
            {
                icons.AddRange(_filterIcons(folder, filterNameTable));
            }

            return icons;
        }

        // If an icon doesn't exist in the storage directory, copy it there
        public bool CopyFile(string filePath, string storagePath)
        {
            if (File.Exists(storagePath))
            {
                return true;
            }
            File.Copy(filePath, storagePath);
            return File.Exists(storagePath);
        }

        public void DeleteFile(string fileName)
        {
            File.Delete(_appData + fileName);
        }

        // Recursively searches for directories in a path
        private List<string> _findDirectories(string path)
        {
            var Folders = new List<string>();
            Folders.Add(path);

            foreach (var folder in Directory.GetDirectories(path))
            {
                Folders.AddRange(_findDirectories(folder));
            }

            return Folders;
        }
    
        // Searches a folder for files matching a set of filters and returns them.
        private List<string> _filterIcons(string path, List<string> filterNames = null)
        {
            var filteredFiles = new List<string>();
            var filterExtension = ".ico";

            // The file name filter is optional, so we need to initialize an empty list if it's null.
            filterNames = filterNames ?? new List<string>();

            foreach (string file in Directory.GetFiles(path))
            {
                // Filter out any files that either don't match our extension parameter, or that are in our filename filter
                if (!file.EndsWith(filterExtension) || filterNames.Contains(_filenameSubstring(file)))
                {
                    continue;
                }

                filteredFiles.Add(file);
            }
            return filteredFiles;
        }

        // Returns the file name substring in a file path
        private string _filenameSubstring(string input)
        {
            return input.Substring(input.LastIndexOf(@"\"));
        }

        // The reference counter will raise an event when a file's counter reaches 0
        // Dealing with the file is delegated to the FileDept
        protected void ReferenceCountIsZero_handler(object sender, ReferenceEventArgs e)
        {
            DeleteFile(e.fileName);
        }
    }
}
