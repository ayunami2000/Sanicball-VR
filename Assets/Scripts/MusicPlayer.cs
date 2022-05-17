using Sanicball;
using Sanicball.Data;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using Sanicball.UI;

namespace Sanicball
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        //public GUISkin skin;

        public MusicPlayerCanvas playerCanvasPrefab;
        public bool playerCanvasLobbyOffset = false;
        private MusicPlayerCanvas playerCanvas;

        public bool startPlaying = false;
        public bool fadeIn = false;

        public Song[] playlist;
        public AudioSource fastSource;

        [System.NonSerialized]
        public bool fastMode = false;

        private int currentSongID;
        private bool isPlaying;
        private string currentSongCredits;

        //Song credits
        private float timer = 0;

        private float lastTime = 0f;

        private float slidePosition;
        private float slidePositionMax = 20;

        private AudioSource aSource;

        public void Play()
        {
            Play(playlist[currentSongID].name);
        }

        public void Play(string credits)
        {
            if (!ActiveData.GameSettings.music) return;
            playerCanvas.Show(credits);
            isPlaying = true;
            aSource.Play();
        }

        public void Pause()
        {
            aSource.Pause();
            isPlaying = false;
        }

        private void Start()
        {
            var c = GameObject.FindWithTag("MainCamera");
            playerCanvas = Instantiate(playerCanvasPrefab, c.transform);
            playerCanvas.GetComponent<Canvas>().worldCamera = c.GetComponent<Camera>();
            playerCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(UnityEngine.XR.XRSettings.eyeTextureWidth, UnityEngine.XR.XRSettings.eyeTextureHeight / 2);
            if (playerCanvasLobbyOffset) 
            {
                playerCanvas.lobbyOffset = true;
            }

            aSource = GetComponent<AudioSource>();

            slidePosition = slidePositionMax;
            ShuffleSongs();

            if (ActiveData.ESportsFullyReady)
            {
                Sanicball.Logic.MatchManager m = FindObjectOfType<Sanicball.Logic.MatchManager>();
                if (!m.InLobby) {
                    List<Song> p = playlist.ToList();
                    Song s = new Song();
                    s.name = "Skrollex - Bungee Ride";
                    s.clip = ActiveData.ESportsMusic;
                    p.Insert(0,s);
                    playlist = p.ToArray();
                }
            }
            

            aSource.clip = playlist[0].clip;

            currentSongID = 0;
            isPlaying = aSource.isPlaying;
            if (startPlaying && ActiveData.GameSettings.music)
            {
                Play();
            }
            if (fadeIn)
            {
                aSource.volume = 0f;
            }
            if (!ActiveData.GameSettings.music)
            {
                fastSource.Stop();
            }
        }

        private void Update()
        {
            if (aSource == null) return;
            if (fadeIn && aSource.volume < 0.5f)
            {
                aSource.volume = Mathf.Min(aSource.volume + Time.deltaTime * 0.1f, 0.5f);
            }
            //If it's not playing but supposed to play, change song
            float currTime = aSource.clip == null ? 0 : aSource.clip.length;
            if ((aSource.time >= currTime || GameInput.IsChangingSong() || (aSource.time == 0 && lastTime > 0)) && isPlaying)
            {
                if (currentSongID < playlist.Length - 1)
                {
                    currentSongID++;
                }
                else
                {
                    currentSongID = 0;
                }
                lastTime = 0f;
                aSource.time = 0f;
                aSource.clip = playlist[currentSongID].clip;
                slidePosition = slidePositionMax;
                //todo: fix audio stopping randomly...
                Play();
            }
            else
            {
                lastTime = aSource.time;
            }
            //Timer
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }

            if (fastMode && fastSource.volume < 1)
            {
                fastSource.volume = Mathf.Min(1, fastSource.volume + Time.deltaTime * 0.25f);
                aSource.volume = 0.5f - fastSource.volume / 2;
            }
            if (!fastMode && fastSource.volume > 0)
            {
                fastSource.volume = Mathf.Max(0, fastSource.volume - Time.deltaTime * 0.5f);
                aSource.volume = 0.5f - fastSource.volume / 2;
            }
            if (timer > 0)
            {
                slidePosition = Mathf.Lerp(slidePosition, 0, Time.deltaTime * 4);
            }
            else
            {
                slidePosition = Mathf.Lerp(slidePosition, slidePositionMax, Time.deltaTime * 2);
            }
        }

        private void ShuffleSongs()
        {
            //Shuffle playlist using Fisher-Yates algorithm
            for (int i = playlist.Length; i > 1; i--)
            {
                int j = Random.Range(0, i);
                Song tmp = playlist[j];
                playlist[j] = playlist[i - 1];
                playlist[i - 1] = tmp;
            }
        }
    }

    [System.Serializable]
    public class Song
    {
        public string name;
        public AudioClip clip;
    }
}