; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Quick directory icon customizer"
#define MyAppVersion "1"
#define MyAppPublisher "Alexia Wilson"
#define MyAppURL "http://mediant.in/"
#define MyAppExeName "FolderIcon.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{9C080BCC-69CC-4355-AC76-BF1C34A8D389}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
OutputBaseFilename=setup
Compression=lzma
SolidCompression=yes

[Registry]
Root: HKCR; Subkey: "Folder\Shell\IconCustomize"; Flags: uninsdeletekey; ValueType: none
Root: HKCR; Subkey: "Folder\Shell\IconCustomize\command"; Flags: uninsdeletekey; ValueType: string; ValueData: """{app}\{#MyAppExeName}"" ""%1"""

Root: HKCR; Subkey: "Directory\Background\shell\IconCustomize"; Flags: uninsdeletekey; ValueType: none  
Root: HKCR; Subkey: "Directory\Background\shell\IconCustomize\command"; Flags: uninsdeletekey; ValueType: string; ValueData: """{app}\{#MyAppExeName}"""

[Dirs]
Name: "{localappdata}\{#MyAppName}"

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "C:\Users\Alexia\Desktop\Project Shelve\Project Simple Icon Customization\Friendly folder icon customization\bin\Release\Friendly folder icon customization.exe"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files
