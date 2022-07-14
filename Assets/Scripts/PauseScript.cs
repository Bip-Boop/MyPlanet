using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private QuestScript _questScript;
    [SerializeField] private int _planetID;
	[SerializeField] private PlayerDataContainer SaveManager;

    [SerializeField] private AudioSource _sound;

	[SerializeField] private Alerts _alerts;
	[SerializeField] private CanvasGroup _canvasGroup;
	[SerializeField] private CoroutineManager.AnimationSettings _settings;

	[Header("Quests and stats")]
	
	[SerializeField] private Text _totalScoreTxt;
	[SerializeField] private Text _thisPlanetTotalScoreTxt;
	[SerializeField] private string _thisPlanetTotalScoreKey;
	
	
	[Header("Sprites")]
	
	[SerializeField] private Sprite _pauseImage;
	[SerializeField] private Sprite _unpauseImage;
	[SerializeField] private Sprite _offSound;
	[SerializeField] private Sprite _onSound;
	
	[Header("Buttons")] 
	
	[SerializeField] private Button PauseOrUnpauseBtn;
	[SerializeField] private Button PlayAgainBtn;
	[SerializeField] private Button TurnOnOrTurnOffSoundBtn;
	[SerializeField] private Button GoToMainMenuBtn;
	[SerializeField] private Button ShowQuestsAndStatsBtn;

	// private Button[] _allBtns;

	
	[Header("Panels")]
	
	[SerializeField] private GameObject BGPanel;
	[SerializeField] private GameObject QuestsAndStatsPanel;


	public void OpenPauseMenu()
	{
        _sound.Pause();
        _questScript.UpdateQuests(_planetID);
		_totalScoreTxt.text = "Best score:\n" +
		                      SaveManager.GetInt(PlayerDataContainer.Defines.TotalHighScore, 0).ToString();
		_thisPlanetTotalScoreTxt.text =
			"This planet best score:\n" + SaveManager.GetInt(_thisPlanetTotalScoreKey, 0).ToString();
		
		//BGPanel.SetActive(true);

		//foreach (var btn in _allBtns)
		//{
		//	btn.gameObject.SetActive(true);
		//}

		PauseOrUnpauseBtn.image.sprite = _unpauseImage;

		// Time.timeScale = 0f;

		PauseOrUnpauseBtn.onClick.RemoveAllListeners();
		PauseOrUnpauseBtn.onClick.AddListener(ClosePauseMenu);
		
		_alerts.ShowSpecialAlert(_canvasGroup, _settings);
	}

	public void ClosePauseMenu()
	{
        //BGPanel.SetActive(false);

        //foreach (var btn in _allBtns)
        //{
        //	btn.gameObject.SetActive(false);
        //}
        _sound.UnPause();
		PauseOrUnpauseBtn.image.sprite = _pauseImage;
		
		//Time.timeScale = 1f;
		
		PauseOrUnpauseBtn.onClick.RemoveAllListeners();
		PauseOrUnpauseBtn.onClick.AddListener(OpenPauseMenu);
		
		_alerts.CloseSpecialAlert(_canvasGroup, _settings);
		
	}

	public void PlayAgain()
	{
        SaveManager.SaveData();
		SceneManager.LoadScene(SceneManager.GetActiveScene()
			.buildIndex);
	}

	public void TurnSoundOn()
	{
		_sound.mute = false;

		TurnOnOrTurnOffSoundBtn.image.sprite = _offSound;
		
		TurnOnOrTurnOffSoundBtn.onClick.RemoveAllListeners();
		TurnOnOrTurnOffSoundBtn.onClick.AddListener(TurnSoundOff);
	}

	public void TurnSoundOff()
	{
		_sound.mute = true;
		
		TurnOnOrTurnOffSoundBtn.image.sprite = _onSound;
		
		TurnOnOrTurnOffSoundBtn.onClick.RemoveAllListeners();
		TurnOnOrTurnOffSoundBtn.onClick.AddListener(TurnSoundOn);
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void ShowQuestsAndStatistics()
	{
		_totalScoreTxt.text = "Total High Score:\n" +
		                      SaveManager.GetInt(PlayerDataContainer.Defines.TotalHighScore, 0).ToString();
		_thisPlanetTotalScoreTxt.text =
			"This Planet High Score:\n" + SaveManager.GetInt(_thisPlanetTotalScoreKey, 1).ToString();
		
		QuestsAndStatsPanel.gameObject.SetActive(true);
	}

	public void CloseQuestsAndStatistics()
	{
		QuestsAndStatsPanel.gameObject.SetActive(false);
	}

	private void Awake()
	{
		// _allBtns = new []
		//	{PlayAgainBtn, TurnOnOrTurnOffSoundBtn, GoToMainMenuBtn, ShowQuestsAndStatsBtn};
	}

}
