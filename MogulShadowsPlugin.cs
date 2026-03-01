using System;
using System.Collections;
using BepInEx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MogulShadows;

[BepInPlugin(ModInfo.PLUGIN_GUID, ModInfo.PLUGIN_NAME, ModInfo.PLUGIN_VERSION)]
public sealed class MogulShadowsPlugin : BaseUnityPlugin
{
    private const string IgnoredSceneName = "MainMenu";
    private const float ScanIntervalSeconds = 0.5f;

    private Coroutine? _scanRoutine;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        _scanRoutine = StartCoroutine(ScanRoutine());
        ApplyShadowsToAllLights("awake");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (_scanRoutine != null)
        {
            StopCoroutine(_scanRoutine);
            _scanRoutine = null;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyShadowsToAllLights($"scene load ({mode})");
    }

    private IEnumerator ScanRoutine()
    {
        WaitForSecondsRealtime wait = new(ScanIntervalSeconds);
        while (true)
        {
            ApplyShadowsToAllLights("periodic");
            yield return wait;
        }
    }

    private void ApplyShadowsToAllLights(string reason)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.IsValid() &&
            activeScene.name.Equals(IgnoredSceneName, StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        Light[] lights = FindObjectsByType<Light>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        int changed = 0;

        for (int i = 0; i < lights.Length; i++)
        {
            Light light = lights[i];
            if (light == null)
            {
                continue;
            }

            Scene lightScene = light.gameObject.scene;
            if (!lightScene.IsValid() || !lightScene.isLoaded)
            {
                continue;
            }

            bool lightChanged = false;
            if (light.shadows != LightShadows.Soft)
            {
                light.shadows = LightShadows.Soft;
                lightChanged = true;
            }

            if (light.shadowStrength < 1f)
            {
                light.shadowStrength = 1f;
                lightChanged = true;
            }

            if (lightChanged)
            {
                changed++;
            }
        }

        if (changed > 0)
        {
            Logger.LogInfo($"Applied shadows to {changed} lights ({reason}).");
        }
    }
}
