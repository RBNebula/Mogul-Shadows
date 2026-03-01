# Mogul Shadows+

A BepInEx 5 mod for MineMogul that does one thing only:

- Forces all scene `Light` components to cast shadows.

## Behavior

- Runs once on plugin load.
- Runs again on scene load.
- Runs periodically every `0.5` seconds to catch newly spawned/placed objects.
- Skips `MainMenu`.

## Scope

- Targets `Light` components only.
- This mod does not change emissive materials directly.

## Build

```powershell
dotnet build -c Release
```

Output:

`bin\Release\net472\mogulshadows.dll`

## Deploy

Copy to:

`<MineMogul>\BepInEx\plugins\`
