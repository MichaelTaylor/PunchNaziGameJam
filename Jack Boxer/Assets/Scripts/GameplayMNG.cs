using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayMNG : MonoBehaviour {

    public PlayerScript Player;
    public Image JumpForceImage;
    public AudioClip [] BGMTracks;
    public int NumberOfEnemies;
    public AudioSource BGMaudioSource;
    public AudioSource SFXaudioSource;

    // Use this for initialization
    void Start ()
    {
        //DontDestroyOnLoad(gameObject);
        GetProperties();
	}
	
	void GetProperties()
    {
        if (Player != null)
        Player = GameObject.FindObjectOfType<PlayerScript>();
        JumpForceImage = GameObject.FindObjectOfType<Image>();
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
        VictoryChecker();
	}

    void VictoryChecker()
    {
        if (NumberOfEnemies <= 0)
        {
            SceneManager.LoadScene("VictoryScene");
        }
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
        //PlayBGM(BGMTracks[1]);
        SceneManager.LoadScene("GameOver");
    }
}
