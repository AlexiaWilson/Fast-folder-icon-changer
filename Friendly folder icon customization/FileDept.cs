using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace IconCustomizer
{
    class FileDept
    {
        private string _activeDirectory;
        private string _libraryFolder;

        public FileDept()
        {
            /* Setting the directory desktop.ini we'll be reading and writing to */
            var shellArguments = Environment.GetCommandLineArgs();
            // _activeDirectory = (shellArguments.Length > 1) ? shellArguments[1] : Environment.CurrentDirectory;
            _activeDirectory = @"C:\Users\Alexia\Documents\Sync\Client Projects\Project Aleyland Personalisation";

            /* These are the folders that we scan for our images */
            _libraryFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        }

        // Returns the path of the active directories icon.
        public string GetFolderIcon()
        {
            return ShellAPI.GetIcon(_activeDirectory);
        }

        public void SetFolderIcon(string filePath)
        {
            ShellAPI.SetIcon(_activeDirectory, filePath);
        }

        // Clears the icon from a folder and if it exists in the reference counter, decrements it.
        public void ClearFolderIcon()
        {
            ShellAPI.SetIcon(_activeDirectory, "");
        }

        // Returns the paths of icon files in our storage & library folders.
        public List<string> SearchForIcons()
        {
            var icons = new List<string>();
            var folders = _findDirectories(_libraryFolder);

            // Now we'll scan all the folders inside our library for icons
            foreach (var folder in folders)
            {
                icons.AddRange(_findIconsInFolders(folder));
            }

            return icons;
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
        private List<string> _findIconsInFolders(string path)
        {
            var filteredFiles = new List<string>();
            var filterExtension = ".ico";


            foreach (string file in Directory.GetFiles(path))
            {
                // Filter out any files that either don't match our extension parameter, or that are in our filename filter
                if (!file.EndsWith(filterExtension))
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
    }
}
