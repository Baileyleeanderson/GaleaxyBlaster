using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	
	public Sprite[] lives;
	public Image livesImageDisplay;
	public int score;
	public Text scoreText;
	public GameObject titleScreen;
	public GameObject controls;

	public void UpdateLives(int currentLives){
		livesImageDisplay.sprite = lives[currentLives];
	}

	public void UpdateScore(){
		score += 10;
		scoreText.text = "Score: " + score;
	}

	public void ShowTitleScreen(){
		titleScreen.SetActive(true);
		controls.SetActive(true);
	}

	public void HideTitleScreen(){
		scoreText.text = "Score:";
		score = 0;
		titleScreen.SetActive(false);
		controls.SetActive(false);
	}
}
