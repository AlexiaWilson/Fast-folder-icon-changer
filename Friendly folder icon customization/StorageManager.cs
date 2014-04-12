using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Friendly_folder_icon_customization
{
    class StorageManager
    {
        public List<string> Library { get; set; }
        public string Storage { get; set; }

        public StorageManager()
        {
            Library = new List<string>();

            //FIXME:
            Library.Add("C:\\Users\\Alexia\\Pictures\\Icons");
        }

        public void Save(Icon icon)
        {
            string output = "";
            /*FileStream stream = File.Open("desktop.ini", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            using ( StreamReader reader = new StreamReader(stream) )
            {
                string current_line;
                while ( (current_line = reader.ReadLine()) != null )
                {
                    if (current_line.Contains("IconResource") || current_line.Contains("IconFile") || current_line.Contains("IconIndex"))
                    {
                        continue;
                    }
                    output += current_line;
                }
            }

            bool ContainsShellHeader = output.Contains("[.ShellClassInfo]");

            if ( !ContainsShellHeader )
            {*/
                output += "[.ShellClassInfo]";
            //}


            output += String.Format( Environment.NewLine + "\nIconResource={0},{1}", icon.FileLocation, icon.Index);

            FileStream stream = File.Open("desktop.ini", FileMode.Truncate, FileAccess.Write);
            using( StreamWriter writer = new StreamWriter(stream) )
            {
                writer.Write(output);
            }
        }
    }
}
