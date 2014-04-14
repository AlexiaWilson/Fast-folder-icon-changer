using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Friendly_folder_icon_customization
{
    static class ShellAPI
    {
        [DllImport("kernel32.dll")]
        private static extern void RtlZeroMemory(IntPtr dest, int length);

        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern UInt32 SHGetSetFolderCustomSettings(ref SHFOLDERCUSTOMSETTINGSIN pcfs, string path, UInt32 dwReadWrite);

        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern UInt32 SHGetSetFolderCustomSettings(ref SHFOLDERCUSTOMSETTINGSOUT pcfs, string path, UInt32 dwReadWrite);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFOLDERCUSTOMSETTINGSIN
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

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFOLDERCUSTOMSETTINGSOUT
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
            public IntPtr pszIconFile;
            public UInt32 cchIconFile;
            public int iIconIndex;
            public string pszLogo;
            public UInt32 cchLogo;
        };

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SHGetFileInfo(string pszPath, int dwFileAttributes, out SHFILEINFO psfi, int cbFileInfo, int uFlags);

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
        public struct SHFILEINFO
        {
            IntPtr hIcon;
            int iIcon;
            uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=260)] // MAX_PATH = 260
            string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=80)]
            string szTypeName;
        }

        public static void SetIcon(string folder, Icon icon)
        {
            UInt32 FCS_FORCEWRITE = 0x00000002;
            UInt32 FCSM_ICONFILE = 0x00000010;
            var pcfs = new SHFOLDERCUSTOMSETTINGSIN();

            pcfs.dwSize = (UInt32)Marshal.SizeOf(pcfs);
            pcfs.dwMask = FCSM_ICONFILE;
            pcfs.pszIconFile = icon.FileLocation;
            pcfs.iIconIndex = 0;
            pcfs.cchIconFile = 0;

            SHGetSetFolderCustomSettings(ref pcfs, folder, FCS_FORCEWRITE);
        }

        public static string GetIcon(string folder)
        {
            UInt32 FCS_READ = 0x00000001;
            UInt32 FCSM_ICONFILE = 0x00000010;
            var pcfs = new SHFOLDERCUSTOMSETTINGSOUT();

            var stringBuffer = Marshal.AllocHGlobal(260);
            RtlZeroMemory(stringBuffer, 260);

            pcfs.dwSize = (UInt32)Marshal.SizeOf(pcfs);
            pcfs.dwMask = FCSM_ICONFILE;
            pcfs.pszIconFile = stringBuffer;
            pcfs.cchIconFile = 260;

            SHGetSetFolderCustomSettings(ref pcfs, folder, FCS_READ);
            string IconLocation = Marshal.PtrToStringAuto(stringBuffer);
            Marshal.FreeHGlobal(stringBuffer);
            return IconLocation;
        }
    }
}
