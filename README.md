MOGUL SHADOWS - 1.1.0
Forces scene lights to cast shadows in MineMogul.

:: REQUIREMENTS ::
- MineMogul (up to date)
- BepInEx 5

:: FEATURES ::
- Runs multiple shadow-application passes after scene load
- Handles spawned/instantiated objects by scanning their light hierarchies
- Skips `MainMenu` scene
- Targets `Light` components only

:: INSTALL ::
1. Copy `mogulshadows.dll` to:
   `MineMogul\BepInEx\plugins\`
2. (Optional) Copy `mogulshadows.pdb` for symbol-backed logs.

:: BUILD ::
```powershell
dotnet build -c Release
```

:: KNOWN ISSUES ::
- None currently known.

:: CREDITS ::
- Made by RBN
