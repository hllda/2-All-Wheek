using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections.Generic;
using System.Threading;
using TMPro;

public class GameManager:MonoBehaviour
{
	public GameObject titleBackground;
	
	public GameObject gameOverScreen;
	public GameObject characterSelection;
	public GameObject gameplayHUD;

	public GameObject player;

	public bool gameIsActive = false;
	public int lives;
	public int score = 0;

	public GameObject spawn;
	public Vector2 spawnPosition;

	public GameObject prints;
	public GameObject exit;

	public Button buttonOne;
	public Button buttonTwo;

	public GameObject titleScreen;

	public Button start;

	public bool one;
	public bool two;

	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI finalScore;

	public GameObject characterOne;
	public GameObject characterTwo;
	
	public GameObject ThreeLife;
	public GameObject TwoLife;
	public GameObject OneLife;



	public void Start()
	{		
		player = GameObject.Find("Player");
		spawn = GameObject.Find("Spawn");
		prints = GameObject.FindWithTag("Prints");
		
		score = 0;

		lives = 4;
		spawnPosition = new Vector2(spawn.transform.position.x, spawn.transform.position.y);
		TitleScreen();
	}

	void FixedUpdate()
	{		
		GameActivity();

		if(gameIsActive == true)
		{
			UpdateScore();
			// Energy consumption
		}

		else if(gameIsActive == false && lives <= 3)
		{
			GameOver();
		}
	}

	public void TitleScreen()
	{
		titleBackground.SetActive(true);
		titleScreen.SetActive(true);
	}

	public void Character()
	{
		titleScreen.SetActive(false);
		characterSelection.SetActive(true);
	}

	public void StartGame()
	{
		lives = 3;
		
		titleBackground.SetActive(false);
		
		if(one == true && two == false)
		{
			characterOne.SetActive(true);
			characterTwo.SetActive(false);
		}

		if(two == true && one == false)
		{
			characterTwo.SetActive(true);
			characterOne.SetActive(false);
		}

		characterSelection.SetActive(false);
		gameplayHUD.SetActive(true);
		gameIsActive = true;
	}

	void GameOver()
	{
		finalScore.text = scoreText.text;
		gameOverScreen.SetActive(true);
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetSceneByName("Game").name);
		gameOverScreen.SetActive(false);
	}

	public void ExitGame()
	{
		//EditorApplication.isPlaying = false;
		Application.Quit();
	}

	public void GameActivity()
	{
		if(lives == 2)
		{
			ThreeLife.SetActive(false);
		}

		else if(lives == 1)
		{
			TwoLife.SetActive(false);
		}

		else if(lives == 0)
		{
			OneLife.SetActive(false);
		}
		
		if(lives < 1)
		{
			gameIsActive = false;
		}

		else
		{
			if(player.activeSelf == false)
			{
				Respawn();
			}
		}

		if(prints.activeSelf == false)
		{
			SpawnExit();
		}
	}

	public void Respawn()
	{
		player.transform.position = spawnPosition;
		transform.rotation = spawn.transform.rotation;
		player.SetActive(true);
		
		// Reduce speed to 0 for a second
	}

	public void SpawnExit()
	{
		exit.SetActive(true);	
	}

	public void ButtonOneClick()
	{
		buttonOne.interactable = false;
		buttonTwo.interactable = true;
		start.interactable = true;
		one = true;
		two = false;

	}

	public void ButtonTwoClick()
	{
		buttonTwo.interactable = false;
		buttonOne.interactable = true;
		start.interactable = true;
		two = true;
		one = false;
	}

	public void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}
}
