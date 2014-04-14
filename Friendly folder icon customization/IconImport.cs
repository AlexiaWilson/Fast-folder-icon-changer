using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Friendly_folder_icon_customization
{
    static class ShellAPI
    {
        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern UInt32 SHGetSetFolderCustomSettings([In, Out] ref SHFOLDERCUSTOMSETTINGS pcfs, string path, UInt32 dwReadWrite);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFOLDERCUSTOMSETTINGS
        {

            public UInt32 dwSize;
            public UInt32 dwMask;
            public IntPtr pvid;
            public string pszWebViewTemplate;
            public UInt32 cchWebViewTemplate;
            public string pszWebViewTemplateVersion;
            public string pszInfoTip;
            public UInt32 cchInfoTip;
            public IntPtr pclsid;
            public UInt32 dwFlags;
            public string pszIconFile;
            public UInt32 cchIconFile;
            public int iIconIndex;
            public string pszLogo;
            public UInt32 cchLogo;
        };

        public static void SetIcon(string folder, Icon icon)
        {
            UInt32 FCS_FORCEWRITE = 0x00000002;
            UInt32 FCSM_ICONFILE = 0x00000010;
            var pcfs = new SHFOLDERCUSTOMSETTINGS();

            pcfs.dwSize = (UInt32)Marshal.SizeOf(pcfs);
            pcfs.dwMask = FCSM_ICONFILE;
            pcfs.pszIconFile = icon.FileLocation;
            pcfs.iIconIndex = 0;

            SHGetSetFolderCustomSettings(ref pcfs, folder, FCS_FORCEWRITE);
        }
    }
}
