using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayMNG : MonoBehaviour {

    public PlayerScript Player;
    public Image JumpForceImage;
    public AudioClip [] BGMTracks;

    public AudioSource BGMaudioSource;
    public AudioSource SFXaudioSource;

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(gameObject);
        GetProperties();
	}
	
	void GetProperties()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        BGMaudioSource = GetComponent<AudioSource>();
    }

    void UIChecker()
    {
        if (JumpForceImage != null)
        JumpForceImage.fillAmount = Player.JumpForce / Player.MaxJumpForce;
    }

	void Update ()
    {
        UIChecker();
	}

    public void PlaySFX(AudioClip SFXtoPlay)
    {
        SFXaudioSource.clip = SFXtoPlay;
        SFXaudioSource.Play();
    }

    public void PlayBGM(AudioClip NewBGM)
    {
        BGMaudioSource.clip = NewBGM;
        BGMaudioSource.Play();
    }

    public void GameOver()
    {
        PlayBGM(BGMTracks[1]);
        SceneManager.LoadScene("GameOver");
    }
}
