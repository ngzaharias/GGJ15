using UnityEngine;
using System.Collections;

public class InterfaceManager : MonoBehaviour
{
	private static InterfaceManager instance = null;

	public static InterfaceManager Instance
	{
		get { return instance; }
	}

	[SerializeField] CanvasGroup _gameOverScreen;

	bool gameOver = false;
	
	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}

	//	DontDestroyOnLoad(this.gameObject);
	}

	void Update()
	{
		if (gameOver)
		{
			_gameOverScreen.alpha += Time.deltaTime;
			if (_gameOverScreen.alpha > 1.0f)
			{
				Invoke("LoadCreditScene", 5.0f);
			}
		}
	}

	public void GameOver()
	{
		gameOver = true;
	}

	void LoadCreditScene()
    {
        Application.LoadLevel("CreditScene");
    }
}
