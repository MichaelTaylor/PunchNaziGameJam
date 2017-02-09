using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayMNG : MonoBehaviour {

    public PlayerScript Player;
    public Image JumpForceImage;
    public AudioClip [] BGMTracks;

    private AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
        GetProperties();
	}
	
	void GetProperties()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        audioSource = GetComponent<AudioSource>();
    }

    void UIChecker()
    {
        JumpForceImage.fillAmount = Player.JumpForce / Player.MaxJumpForce;
    }

	void Update ()
    {
        UIChecker();
	}

    public void PlayBGMAudio(AudioClip NewBGM)
    {
        audioSource.clip = NewBGM;
        audioSource.Play();
    }
}
