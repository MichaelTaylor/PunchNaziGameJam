using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayMNG : MonoBehaviour {

    public PlayerScript Player;
    public Image JumpForceImage;

	// Use this for initialization
	void Start ()
    {
        GetProperties();
	}
	
	void GetProperties()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    void UIChecker()
    {
        JumpForceImage.fillAmount = Player.JumpForce / Player.MaxJumpForce;
    }

	void Update ()
    {
        UIChecker();
	}
}
