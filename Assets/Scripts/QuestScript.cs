using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour
{
    [SerializeField] private AllManager _allManager;
    [SerializeField] private PlayerDataContainer _saveManager;

    [System.Serializable]
    private struct QuestUI
    {
        public string Name;
        public Text NameText;

        public int ToDo;
        public Text ProgressText;

        public Button QuestButton;
        public Text QuestButtonText;

        public void UpdateQuest(int done)
        {
            NameText.text = Name;
            ProgressText.text = done.ToString() + " / " + ToDo.ToString();

            if (done == ToDo)
            {
                QuestButtonText.text = "Done";
                ProgressText.color = new Color(0.6196079f, 0.7843137f, 0.3568628f);
            }
            else
            {
                QuestButtonText.text = "In Progress";
                ProgressText.color = new Color(0.6078432f, 0.5294118f, 0.7686275f);
            }
        }
    }

    [SerializeField] private QuestUI[] _allQuests;

    public void UpdateQuests(int planetID)
    {
        CheckQuestsProgress(planetID);

        // Each planet has 3 quests

        int i = planetID * 3;
        int upper = i + 3;

        for (; i < upper; i++)
        {
            _allQuests[i].UpdateQuest(_saveManager.GetInt("QuestProgress" + i.ToString(), 0));
        }
    }

    private System.Action[] Checks;
    private void CheckQuestsProgress(int planetID)
    {
        Checks = new System.Action[] { () => CheckQuestsProgressMoon(), () => CheckQuestsProgressMars() };
        Checks[planetID].Invoke();
    }

    private void CheckQuestsProgressMoon()
    {   
        // Quest 1
        _saveManager.SetInt(PlayerDataContainer.Defines.Finish3StagesQuest,
            _saveManager.GetInt(PlayerDataContainer.Defines.TotalFinishedStagesMoon, 0));

        // Quest 2

        if (_allManager.HousesOnPlanet > _saveManager.GetInt(PlayerDataContainer.Defines.Build8HousesInOneGame, 0))
        {
            _saveManager.SetInt(PlayerDataContainer.Defines.Build8HousesInOneGame, _allManager.HousesOnPlanet > 8 ? 8 : _allManager.HousesOnPlanet);
        }

        // Quest 3
        _saveManager.SetInt(PlayerDataContainer.Defines.Score100Points,
            Mathf.Clamp(_saveManager.GetInt(PlayerDataContainer.Defines.TotalHighScoreOnMoon, 0), 0, 100));
    }

    private void CheckQuestsProgressMars()
    {

    }

}
