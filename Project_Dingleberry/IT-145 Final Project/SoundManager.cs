using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Project_Dingleberry
{
    public static class SoundManager
    {
        private static readonly string soundFolder =
            Path.Combine(Application.StartupPath, "Assets", "Sounds");

        private static readonly Dictionary<string, CachedSound> cachedSounds =
            new Dictionary<string, CachedSound>();

        private static readonly string[] allSounds =
        {
            "sfx_MenuStart.wav",
            "sfx_MenuHighScore.wav",
            "sfx_MenuInstructions.wav",
            "sfx_PlayerHit.wav",
            "sfx_ItemBomb.wav",
            "sfx_ItemLife.wav",
            "sfx_ItemMine.wav",
            "sfx_GameOver.wav"
        };

        private static WaveOutEvent outputDevice;
        private static MixingSampleProvider mixer;
        private static bool isInitialized = false;

        public static void Initialize()
        {
            if (isInitialized)
            {
                return;
            }

            mixer = new MixingSampleProvider(
                WaveFormat.CreateIeeeFloatWaveFormat(44100, 2)
            );

            mixer.ReadFully = true;

            outputDevice = new WaveOutEvent();
            outputDevice.DesiredLatency = 50;
            outputDevice.NumberOfBuffers = 2;
            outputDevice.Init(mixer);
            outputDevice.Play();

            PreloadAllSounds();

            isInitialized = true;
        }

        public static void PreloadAllSounds()
        {
            foreach (string fileName in allSounds)
            {
                LoadSound(fileName);
            }
        }

        private static void LoadSound(string fileName)
        {
            if (cachedSounds.ContainsKey(fileName))
            {
                return;
            }

            string fullPath = Path.Combine(soundFolder, fileName);

            if (!File.Exists(fullPath))
            {
                return;
            }

            cachedSounds[fileName] = new CachedSound(fullPath);
        }

        private static void PlaySound(string fileName)
        {
            try
            {
                Initialize();
                LoadSound(fileName);

                if (!cachedSounds.TryGetValue(fileName, out CachedSound sound))
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
            if (outputDevice != null)
            {
                outputDevice.Stop();
                outputDevice.Dispose();
                outputDevice = null;
            }

            mixer = null;
            cachedSounds.Clear();
            isInitialized = false;
        }
    }

    public class CachedSound
    {
        public float[] AudioData { get; private set; }
        public WaveFormat WaveFormat { get; private set; }

        public CachedSound(string audioFileName)
        {
            using (AudioFileReader audioFileReader = new AudioFileReader(audioFileName))
            {
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

                List<float> wholeFile = new List<float>();
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
    }

    public class CachedSoundSampleProvider : ISampleProvider
    {
        private readonly CachedSound cachedSound;
        private int position;

        public CachedSoundSampleProvider(CachedSound cachedSound)
        {
            this.cachedSound = cachedSound;
            position = 0;
        }

        public WaveFormat WaveFormat
        {
            get { return cachedSound.WaveFormat; }
        }

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
}