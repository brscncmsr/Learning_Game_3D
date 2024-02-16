using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public delegate void afterSound();
    public afterSound waitSound;
    [HideInInspector] public GameObject sounds;
    [HideInInspector] public List<AudioSource> list = new List<AudioSource>();
    public GameSounds celebrate;
    public GameSounds fail;
    [Header("Compare1")]
    public GameSounds compare1Sound;
    [Header("Compare2")]
    public GameSounds compare2Story;
    public GameSounds compare2Story1;
    public GameSounds compare2Game;
    public GameSounds compare2Succes;
    public GameSounds compare2Story2;
    public GameSounds compare2Game2;
    public GameSounds compare2Succes2;
    public GameSounds compare2H1;
    public GameSounds compare2H2;
    public GameSounds compare2H3;
    public GameSounds compare2H11;
    public GameSounds compare2H22;
    public GameSounds compare2H33;
    [Header("Compare3")]
    public GameSounds compare3Story;
    public GameSounds compare3Game;
    public GameSounds compare3Story2;
    public GameSounds compare3Game2;
    public GameSounds compare3Story3;
    public GameSounds compare3Game3;
    public GameSounds compare3Succes;
    public GameSounds compare3H1;
    public GameSounds compare3H2;
    public GameSounds compare3H3;
    public GameSounds compare3H11;
    public GameSounds compare3H12;
    public GameSounds compare3H13;
    public GameSounds compare3H21;
    public GameSounds compare3H22;
    public GameSounds compare3H23;


    [System.Serializable]
    public class GameSounds
    {
        [HideInInspector] public AudioSource player;
        public AudioClip clip;

        private void SetPlayer()
        {
            player = Instance.sounds.AddComponent<AudioSource>();
            Instance.list.Add(player);
            player.playOnAwake = false;
            player.clip = clip;
        }
        public void PlaySound()
        {
            StopAllSound();
            if (player == null)
            {
                SetPlayer();
            }
            player.Play();
        }
        private void StopAllSound()
        {
            for (int i = 0; i < Instance.list.Count; i++)
            {
                Instance.list[i].Stop();
            }
        }
    }
    private void Awake()
    {
        Instance = this;
        sounds = gameObject;
    }
    public IEnumerator WaitForEndSound (GameSounds sound)
    {
        yield return new WaitForSeconds(sound.clip.length);
        waitSound.Invoke();

    }
    public void PlaySound(GameSounds sound)
    {
        sound.PlaySound();
        StartCoroutine(WaitForEndSound(sound));
     
    }
}
