using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string ThisPlanetHighScoreKey;
    [SerializeField] private PlayerDataContainer SaveManager;

    public int Points { get; private set; }
    private Queue<int> _pointsQueue;
    private Queue<System.Action> _changesDisplay;

    [SerializeField] private Transform _scorePanel;

    [SerializeField] private CoroutineManager.AnimationSettings _minimumScale;
    [SerializeField] private CoroutineManager.AnimationSettings _mediumScale;
    [SerializeField] private CoroutineManager.AnimationSettings _maximumScale;
    
    [SerializeField] private Text _scoreNumText;
    [SerializeField] private Text _tickPointsText;

    private bool _isCycleStarted;
    
    
    public void AddPointsChangesToQueue(int points)
    {
        _pointsQueue.Enqueue(points);
        
        if (_isCycleStarted) return;
        
        _isCycleStarted = true;
        StartCoroutine(PointsCycle(0.5f));


    }

    private IEnumerator PointsCycle(float waitBefore)
    {       
        yield return new WaitForSeconds(waitBefore);
        
        var wasPoints = Points;
        while (_pointsQueue.Count != 0)
        {    
            Points += _pointsQueue.Dequeue();
        }

        var changed = Math.Abs(Points - wasPoints);
        
        if (changed <= 4)
        {
            StartCoroutine(SmoothScaleChange(_scorePanel, _minimumScale.curve, _minimumScale.speed));
        }
        else if (changed <= 10)
        {
            StartCoroutine(SmoothScaleChange(_scorePanel, _mediumScale.curve, _mediumScale.speed)); 
        }
        else
        {
            StartCoroutine(SmoothScaleChange(_scorePanel, _maximumScale.curve, _maximumScale.speed));
        }

        int grower = 0;
        
        if (wasPoints < Points)
        {
            while (wasPoints < Points)
            {
                wasPoints += 1;
                grower += 1;
                _scoreNumText.text = wasPoints.ToString();
                _tickPointsText.text = grower.ToString();
                yield return new WaitForSeconds(0.05f);
            }
        }
        else if (wasPoints > Points)
        {
            while (wasPoints > Points)
            {
                wasPoints -= 1;
                grower -= 1;
                _scoreNumText.text = wasPoints.ToString();
                _tickPointsText.text = grower.ToString();
                yield return new WaitForSeconds(0.05f);
            }
        }

    
        _isCycleStarted = false;
        _tickPointsText.text = grower.ToString();
        _scoreNumText.text = Points.ToString();

        if (Points > SaveManager.GetInt(ThisPlanetHighScoreKey, 0))
        {
            SaveManager.SetInt(ThisPlanetHighScoreKey, Points);
        }

        if (Points > SaveManager.GetInt(PlayerDataContainer.Defines.TotalHighScore, 0))
        {
            SaveManager.SetInt(PlayerDataContainer.Defines.TotalHighScore, Points);
        }

    }

    private IEnumerator SmoothScaleChange(Transform target, AnimationCurve curve, float speed)
    {
        float t = 0f;
        float s = 0f;

        Vector3 startScale = target.localScale;

        while (t <= 1f)
        {
            s = curve.Evaluate(t);
            t += Time.deltaTime * speed;
            target.localScale = startScale * s;
            yield return 0;
        }
    }


    private void Awake()
    {
        _pointsQueue = new Queue<int>();
    }
}
