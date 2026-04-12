#pragma warning disable IDE0130

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Project_Dingleberry;

public static class SoundManager
{
    private static readonly string SoundFolder =
        Path.Combine(Application.StartupPath, "Assets", "Sounds");

    private static readonly Dictionary<string, CachedSound> CachedSounds = [];

    private static readonly string[] AllSounds =
    [
        "sfx_MenuStart.wav",
        "sfx_MenuHighScore.wav",
        "sfx_MenuInstructions.wav",
        "sfx_PlayerHit.wav",
        "sfx_ItemBomb.wav",
        "sfx_ItemLife.wav",
        "sfx_ItemMine.wav",
        "sfx_GameOver.wav"
    ];

    private static WaveOutEvent? outputDevice;
    private static MixingSampleProvider? mixer;
    private static bool isInitialized;

    public static void Initialize()
    {
        if (isInitialized)
        {
            return;
        }

        mixer = new(WaveFormat.CreateIeeeFloatWaveFormat(44100, 2))
        {
            ReadFully = true
        };

        outputDevice = new()
        {
            DesiredLatency = 50,
            NumberOfBuffers = 2
        };

        outputDevice.Init(mixer);
        outputDevice.Play();

        PreloadAllSounds();
        isInitialized = true;
    }

    public static void PreloadAllSounds()
    {
        foreach (string fileName in AllSounds)
        {
            LoadSound(fileName);
        }
    }

    private static void LoadSound(string fileName)
    {
        if (CachedSounds.ContainsKey(fileName))
        {
            return;
        }

        string fullPath = Path.Combine(SoundFolder, fileName);

        if (!File.Exists(fullPath))
        {
            return;
        }

        CachedSounds[fileName] = new(fullPath);
    }

    private static void PlaySound(string fileName)
    {
        try
        {
            Initialize();
            LoadSound(fileName);

            if (mixer == null)
            {
                return;
            }

            if (!CachedSounds.TryGetValue(fileName, out CachedSound? sound) || sound == null)
            {
                return;
            }

            mixer.AddMixerInput(new CachedSoundSampleProvider(sound));
        }
        catch
        {
        }
    }

    public static void PlayMenuStart()
    {
        PlaySound("sfx_MenuStart.wav");
    }

    public static void PlayMenuHighScore()
    {
        PlaySound("sfx_MenuHighScore.wav");
    }

    public static void PlayMenuInstructions()
    {
        PlaySound("sfx_MenuInstructions.wav");
    }

    public static void PlayPlayerHit()
    {
        PlaySound("sfx_PlayerHit.wav");
    }

    public static void PlayItemBomb()
    {
        PlaySound("sfx_ItemBomb.wav");
    }

    public static void PlayItemLife()
    {
        PlaySound("sfx_ItemLife.wav");
    }

    public static void PlayItemMine()
    {
        PlaySound("sfx_ItemMine.wav");
    }

    public static void PlayGameOver()
    {
        PlaySound("sfx_GameOver.wav");
    }

    public static void DisposeAll()
    {
        outputDevice?.Stop();
        outputDevice?.Dispose();
        outputDevice = null;

        mixer = null;
        CachedSounds.Clear();
        isInitialized = false;
    }
}

public class CachedSound
{
    public float[] AudioData { get; }
    public WaveFormat WaveFormat { get; }

    public CachedSound(string audioFileName)
    {
        using AudioFileReader audioFileReader = new(audioFileName);

        ISampleProvider sampleProvider = audioFileReader;

        if (sampleProvider.WaveFormat.Channels == 1)
        {
            sampleProvider = new MonoToStereoSampleProvider(sampleProvider);
        }

        if (sampleProvider.WaveFormat.SampleRate != 44100)
        {
            sampleProvider = new WdlResamplingSampleProvider(sampleProvider, 44100);
        }

        WaveFormat = sampleProvider.WaveFormat;

        List<float> wholeFile = [];
        float[] readBuffer = new float[WaveFormat.SampleRate * WaveFormat.Channels];
        int samplesRead;

        while ((samplesRead = sampleProvider.Read(readBuffer, 0, readBuffer.Length)) > 0)
        {
            for (int i = 0; i < samplesRead; i++)
            {
                wholeFile.Add(readBuffer[i]);
            }
        }

        AudioData = wholeFile.ToArray();
    }
}

public class CachedSoundSampleProvider(CachedSound cachedSound) : ISampleProvider
{
    private int position;

    public WaveFormat WaveFormat => cachedSound.WaveFormat;

    public int Read(float[] buffer, int offset, int count)
    {
        int availableSamples = cachedSound.AudioData.Length - position;
        int samplesToCopy = Math.Min(availableSamples, count);

        for (int i = 0; i < samplesToCopy; i++)
        {
            buffer[offset + i] = cachedSound.AudioData[position + i];
        }

        position += samplesToCopy;
        return samplesToCopy;
    }
}

#pragma warning restore IDE0130