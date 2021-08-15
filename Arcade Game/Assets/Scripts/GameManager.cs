using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class GameManager:MonoBehaviour
{
	// Reference to other scripts
	public PlayerController playerController;

	// States of the game
	public bool gameIsActive = false;
	public bool isGameWon = false;
	public bool isGameLost = false;

	// Menu
	public GameObject titleBackground;
	public GameObject titleScreen;
	public GameObject howToPlay;
	public GameObject characterSelection;
	public GameObject gameOverScreen;
	public GameObject win;
	public GameObject gameOver;
	public GameObject gameplayHUD;
	public GameObject pauseMenu;

	// Character selection
	public Button start;

	public bool luna;
	public bool kanel;

	public Button lunaButton;
	public Button kanelButton;

	public GameObject characterLuna;
	public GameObject characterKanel;

	// Score
	public int score;
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI finalScoreText;

	// Hearts
	public GameObject ThreeLife;
	public GameObject TwoLife;
	public GameObject OneLife;

	// Prints
	public int printsRequirement;
	public int printsCounter;

	// Player
	public GameObject player;
	public int playerRotation;
	public float coolDown = 0;

	// Spawn
	public GameObject spawn;
	public Vector2 spawnPosition;

	// Exit
	public GameObject exit;
	public Vector2 exitPosition;

	// Level number
	public int level;

	// Levels
	public GameObject levelOne;
	public GameObject levelTwo;
	public GameObject levelThree;
	public GameObject levelFour;
	public GameObject levelFive;
	public GameObject levelSix;
	public GameObject levelSeven;

	void Start()
	{
		level = 0;
		score = 0;
		playerController.lives = -1;

		player = GameObject.Find("Player");

		LevelChanger();
		TitleScreen();
	}

	void FixedUpdate()
	{
		if(Input.GetKeyDown(KeyCode.Escape) && gameIsActive && !isGameLost && !isGameWon)
		{
			Debug.Log("Esc was pressed");
			
			if(pauseMenu.activeInHierarchy == false)
			{
				Pause();
			}
			
			else if(pauseMenu.activeInHierarchy == true)
			{
				Resume();
			}
		}
		
		if(player.activeInHierarchy == false)
		{
			Respawn();
		}

		else if(gameIsActive == true && isGameWon == false)
		{
			GameActivity();
			UpdateScore();
		}

		if(playerController.lives == 0 || isGameWon == true)
		{
			GameOver();
		}
	}

	public void TitleScreen()
	{
		titleBackground.SetActive(true);
		titleScreen.SetActive(true);
	}

	public void OpenHowToPlay()
	{
		titleScreen.SetActive(false);
		howToPlay.SetActive(true);
	}

	public void CloseHowToPlay()
	{
		howToPlay.SetActive(false);
		titleScreen.SetActive(true);
	}

	public void CharacterSelection()
	{
		titleScreen.SetActive(false);
		characterSelection.SetActive(true);
	}

	public void StartGame()
	{
		titleBackground.SetActive(false);

		if(luna == true && kanel == false)
		{
			characterLuna.SetActive(true);
			characterKanel.SetActive(false);
		}

		if(kanel == true && luna == false)
		{
			characterKanel.SetActive(true);
			characterLuna.SetActive(false);
		}

		characterSelection.SetActive(false);
		gameplayHUD.SetActive(true);
		gameIsActive = true;
	}

	public void Respawn()
	{
		player.SetActive(false);
		playerController.playerRb.transform.position = spawnPosition;
		playerController.playerRoundedPosition = spawnPosition;
		playerController.rotation = playerRotation;
		coolDown = 2;
		player.SetActive(true);
	}

	void GameOver()
	{
		finalScoreText.text = scoreText.text;
		gameOverScreen.SetActive(true);

		if(isGameWon == true)
		{
			win.SetActive(true);
			gameplayHUD.SetActive(false);
			titleBackground.SetActive(true);
		}

		else if(playerController.lives == 0)
		{
			gameOver.SetActive(true);
			gameplayHUD.SetActive(false);
			titleBackground.SetActive(true);
		}
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetSceneByName("Game").name);
		Time.timeScale = 1;
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void GameActivity()
	{
		if(playerController.lives < 3)
		{
			ThreeLife.SetActive(false);
		}

		if(playerController.lives < 2)
		{
			TwoLife.SetActive(false);
		}

		if(playerController.lives < 1)
		{
			OneLife.SetActive(false);
		}

		if(playerController.lives == 0)
		{
			gameIsActive = false;
		}

		if(printsCounter == printsRequirement)
		{
			AllowExit();
		}

		if(coolDown > 0)
		{
			coolDown -= 1 * Time.deltaTime;
		}

		else
		{
			coolDown = 0;
		}
	}

	public void AllowExit()
	{
		exit.SetActive(false);
	}

	public void LunaButton()
	{
		lunaButton.interactable = false;
		kanelButton.interactable = true;
		start.interactable = true;
		luna = true;
		kanel = false;
		playerController.lives = 3;
	}

	public void KanelButton()
	{
		kanelButton.interactable = false;
		lunaButton.interactable = true;
		start.interactable = true;
		kanel = true;
		luna = false;
		playerController.lives = 1;
	}

	public void UpdateScore()
	{
		scoreText.text = "" + score;
	}

	public void LevelChanger()
	{
		level++;

		if(level == 1)
		{
			levelOne.SetActive(true);
			spawn.transform.position = new Vector2(-5f, -3f);
			playerRotation = -90;
			exit.transform.position = new Vector2(5f, 9f);
			exit.SetActive(true);
		}

		else if(level == 2)
		{
			levelOne.SetActive(false);
			levelTwo.SetActive(true);
			spawn.transform.position = new Vector2(3f, 5f);
			playerRotation = -90;
			exit.transform.position = new Vector2(15f, 6f);
			exit.SetActive(true);
		}

		else if(level == 3)
		{
			levelTwo.SetActive(false);
			levelThree.SetActive(true);
			spawn.transform.position = new Vector2(3f, -5f);
			playerRotation = -90;
			exit.transform.position = new Vector2(0f, 9f);
			exit.SetActive(true);
		}

		else if(level == 4)
		{
			levelThree.SetActive(false);
			levelFour.SetActive(true);
			spawn.transform.position = new Vector2(3f, -5f);
			playerRotation = -90;
			exit.transform.position = new Vector2(0f, 9f);
			exit.SetActive(true);
		}

		else if(level == 5)
		{
			levelFour.SetActive(false);
			levelFive.SetActive(true);
			spawn.transform.position = new Vector2(3f, -5f);
			playerRotation = -90;
			exit.transform.position = new Vector2(0f, 9f);
			exit.SetActive(true);
		}

		else if(level == 6)
		{
			levelFive.SetActive(false);
			levelSix.SetActive(true);
			spawn.transform.position = new Vector2(3f, -5f);
			playerRotation = -90;
			exit.transform.position = new Vector2(0f, 9f);
			exit.SetActive(true);
		}

		else if(level == 7)
		{
			levelSix.SetActive(false);
			levelSeven.SetActive(true);
			spawn.transform.position = new Vector2(3f, -5f);
			playerRotation = -90;
			exit.transform.position = new Vector2(0f, 9f);
			exit.SetActive(true);
		}

		else if(level > 7)
		{
			levelSeven.SetActive(false);
			gameIsActive = false;
			isGameWon = true;
		}

		printsCounter = 0;
		printsRequirement = GameObject.FindGameObjectsWithTag("Prints").Length;

		score += (Convert.ToInt32(playerController.energy));

		if((luna == true && kanel == false) || level == 1)
		{
			playerController.energy = 100;
		}
		
		playerController.rotation = playerRotation;
		spawnPosition = spawn.transform.position;

		Respawn();
	}

	public void Pause()
	{
		Time.timeScale = 0;

		pauseMenu.SetActive(true);
	}

	public void Resume()
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1;	
	}
}