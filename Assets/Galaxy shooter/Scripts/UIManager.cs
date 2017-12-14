using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public Sprite[] lives;
	public Image livesImage;
	public GameObject titleScreen;
	public Text scoreText;
	public int score = 0;

	public void UpdateLives(int currentlives) {
		livesImage.sprite = lives[currentlives];
	}

	public void UpdateScore() {
		score += 10;
		scoreText.text = "Score: " + score;
	}

	public void ShowTitleScreen() {
		titleScreen.SetActive(true);
	}

	public void HideTitleScreen() {
		titleScreen.SetActive(false);
		scoreText.text = "Score: ";
		score = 0;
	}
}
