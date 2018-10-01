; Includes
; ========
!include "MUI.nsh"
!include "nsDialogs.nsh" ; also includes LogicLib!

; Variables
; =========
Var ShortcutStartup
Var ShortcutStartmenu
Var ShortcutDesktop
Var ShortcutQuicklaunch
Var InstallForMe
Var InstallForAllUsers

Var CheckBox_ShortcutStartup
Var CheckBox_ShortcutStartmenu
Var CheckBox_ShortcutDesktop
Var CheckBox_ShortcutQuicklaunch

Var RadioButton_InstallForMe
Var RadioButton_InstallForAllUsers

; Options
; =======
Name "PipView"
BrandingText "Copyright (c) 2001-2008 Joost-Wim Boekesteijn"
OutFile "pipview-2.0.3-setup.exe"
InstallDir "$PROGRAMFILES\PipView"
SetCompressor /SOLID lzma
RequestExecutionLevel admin

!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\win-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\win-uninstall.ico"

; Installer pages
; ===============
!insertmacro MUI_PAGE_DIRECTORY
Page custom EnterOptions LeaveOptions
!insertmacro MUI_PAGE_INSTFILES

!define MUI_FINISHPAGE_LINK "De website van PipView bezoeken voor meer informatie"
!define MUI_FINISHPAGE_LINK_LOCATION "http://pipview.xxp.nu/"

!define MUI_FINISHPAGE_RUN "$INSTDIR\pipview.exe"
!define MUI_FINISHPAGE_RUN_TEXT "PipView starten"

!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
; =================
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_UNPAGE_FINISH

; Languages
; =========
!insertmacro MUI_LANGUAGE "Dutch"

; Installer Sections
; ==================
Section
	SetOutPath $INSTDIR

	File "..\bin\Dotfuscator\pipview.exe"

	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\PipView" "DisplayName" "PipView"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\PipView" "HelpLink" "http://pipview.xxp.nu/uitleg"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\PipView" "URLInfoAbout" "http://pipview.xxp.nu/"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\PipView" "URLUpdateInfo" "http://pipview.xxp.nu/"
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\PipView" "NoRepair" 1
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\PipView" "NoModify" 1
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\PipView" "UninstallString" '"$INSTDIR\uninstall.exe"'

	${If} $InstallForAllUsers = ${BST_CHECKED}
		WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\PipView" "Users" "all"
		SetShellVarContext all
	${Else}
		WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\PipView" "Users" "me"
	${EndIf}

	${If} $ShortcutStartup = ${BST_CHECKED}
		CreateShortCut "$SMSTARTUP\PipView.lnk" "$INSTDIR\pipview.exe"
	${EndIf}

	${If} $ShortcutStartmenu = ${BST_CHECKED}
		CreateDirectory "$SMPROGRAMS\PipView"
		CreateShortCut "$SMPROGRAMS\PipView\PipView.lnk" "$INSTDIR\pipview.exe"
		CreateShortCut "$SMPROGRAMS\PipView\Verwijder PipView.lnk" "$INSTDIR\uninstall.exe"
	${EndIf}

	${If} $ShortcutDesktop = ${BST_CHECKED}
		CreateShortCut "$DESKTOP\PipView.lnk" "$INSTDIR\pipview.exe"
	${EndIf}

	${If} $ShortcutQuicklaunch = ${BST_CHECKED}
		CreateShortCut "$QUICKLAUNCH\PipView.lnk" "$INSTDIR\pipview.exe"
	${EndIf}

	WriteUninstaller "uninstall.exe"
SectionEnd

; Installer Functions
; ===================

Function .onInit
	StrCpy $ShortcutStartup		${BST_CHECKED}
	StrCpy $ShortcutStartmenu	${BST_CHECKED}
	StrCpy $ShortcutDesktop		${BST_CHECKED}
	StrCpy $ShortcutQuicklaunch	${BST_CHECKED}
	StrCpy $InstallForMe		${BST_CHECKED}
	StrCpy $InstallForAllUsers	${BST_UNCHECKED}

	ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v2.0.50727" "Install"

	${If} $0 <> 1
		MessageBox MB_YESNO|MB_ICONQUESTION "U heeft het Microsoft .NET Framework v2.0 niet op uw computer staan. Dit onderdeel is nodig om PipView te kunnen starten.$\n$\nOp de website van PipView wordt uitgelegd hoe u dit kunt installeren. Hierna kunt u de installatie van PipView opnieuw starten. Wilt u deze uitleg nu lezen?" IDYES 0 IDNO +2
		ExecShell "open" "http://pipview.xxp.nu/netframework/"
		Quit
	${EndIf}
FunctionEnd

Function EnterOptions
	nsDialogs::Create /NOUNLOAD 1018

	${NSD_CreateLabel} 0 0 300u 10u "Moet PipView automatisch worden opgestart?"
		${NSD_CreateCheckBox} 5u 15u 295u 10u "Ja, elke keer dat u op deze computer inlogt, wordt PipView automatisch gestart."
		Pop $CheckBox_ShortcutStartup

	${NSD_CreateLabel} 0 30u 300u 10u "Waar moeten er snelkoppelingen naar PipView worden gemaakt?"
		${NSD_CreateCheckBox} 5u 45u 295u 10u "In het menu start."
		Pop $CheckBox_ShortcutStartmenu

		${NSD_CreateCheckBox} 5u 57u 295u 10u "Op het bureaublad."
		Pop $CheckBox_ShortcutDesktop

		${NSD_CreateCheckBox} 5u 69u 295u 10u "In de balk 'Snel starten'."
		Pop $CheckBox_ShortcutQuicklaunch

	${NSD_CreateLabel} 0 84u 300u 10u "Voor welke gebruikers moet PipView beschikbaar worden gemaakt?"
		${NSD_CreateRadioButton} 5u 99u 295u 10u "Alleen voor mij."
		Pop $RadioButton_InstallForMe

		${NSD_CreateRadioButton} 5u 111u 295u 10u "Voor alle gebruikers van deze computer."
		Pop $RadioButton_InstallForAllUsers

	${NSD_SetState} $CheckBox_ShortcutStartup	$ShortcutStartup
	${NSD_SetState} $CheckBox_ShortcutStartmenu	$ShortcutStartmenu
	${NSD_SetState} $CheckBox_ShortcutDesktop	$ShortcutDesktop
	${NSD_SetState} $CheckBox_ShortcutQuicklaunch	$ShortcutQuicklaunch
	${NSD_SetState} $RadioButton_InstallForMe	$InstallForMe
	${NSD_SetState} $RadioButton_InstallForAllUsers $InstallForAllUsers

	GetFunctionAddress $0 LeaveOptions
	nsDialogs::OnBack /NOUNLOAD $0

	!insertmacro MUI_HEADER_TEXT "Opties" "Stel hier overige installatie-opties in."
	nsDialogs::Show
FunctionEnd

Function LeaveOptions
	${NSD_GetState} $CheckBox_ShortcutStartup	$ShortcutStartup
	${NSD_GetState} $CheckBox_ShortcutStartmenu	$ShortcutStartmenu
	${NSD_GetState} $CheckBox_ShortcutDesktop	$ShortcutDesktop
	${NSD_GetState} $CheckBox_ShortcutQuicklaunch	$ShortcutQuicklaunch
	${NSD_GetState} $RadioButton_InstallForMe	$InstallForMe
	${NSD_GetState} $RadioButton_InstallForAllUsers $InstallForAllUsers

	;MessageBox MB_OK|MB_ICONINFORMATION "startup: $ShortcutStartup$\nstartmenu: $ShortcutStartmenu$\ndesktop: $ShortcutDesktop$\nquickl: $ShortcutQuicklaunch$\nforme: $InstallForMe$\n"
FunctionEnd

; Uninstaller Section
; ===================
Section "Uninstall"
	ReadRegStr $0 HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\PipView" "Users"

	${If} $0 == "all"
		SetShellVarContext all
	${EndIf}

	Delete "$SMSTARTUP\PipView.lnk"
	Delete "$QUICKLAUNCH\PipView.lnk"
	Delete "$DESKTOP\PipView.lnk"

	RMDir /r "$SMPROGRAMS\PipView"
	RMDir /r "$INSTDIR"

	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\PipView"
SectionEnd
