using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDataContainer : MonoBehaviour
{

    private string _filename;
    private string _path;
    private DataContainer _data;
    
    
    public struct Defines
    {
        public const string TotalHighScore = "TotalHighScore";
        public const string TotalFinishedStages = "FinishedStages";

        // Moon
        public const string TotalHighScoreOnMoon = "MoonHighScore";
        public const string TotalFinishedStagesMoon = "FinishedStagesMoon";
        public const string Finish3StagesQuest = "QuestProgress0";
        public const string Build8HousesInOneGame = "QuestProgress1";
        public const string Score100Points = "QuestProgress2";
    }

    private Dictionary<string, int> _ints;
    private Dictionary<string, float> _floats;
    private Dictionary<string, string> _strings;
    private Dictionary<string, bool> _bools;

    public int GetInt(string key, int defaultValue)
    {
        return _ints.ContainsKey(key) ? _ints[key] : defaultValue;
    }
    
    public float GetFloat(string key, float defaultValue)
    {
        return _floats.ContainsKey(key) ? _floats[key] : defaultValue;
    }

    public string GetString(string key, string defaultValue)
    {
        return _strings.ContainsKey(key) ? _strings[key] : defaultValue;
    }

    public bool GetBool(string key, bool defaultValue)
    {
        return _bools.ContainsKey(key) ? _bools[key] : defaultValue;
    }

    public void SetInt(string key, int value)
    {
        if (_ints.ContainsKey(key))
        {
            _ints[key] = value;
            return;
        }

        _ints.Add(key, value);
    }

    public void SetFloat(string key, float value)
    {
        if (_floats.ContainsKey(key))
        {
            _floats[key] = value;
            return;
        }

        _floats.Add(key, value);
    }
    
    public void SetString(string key, string value)
    {
        if (_strings.ContainsKey(key))
        {
            _strings[key] = value;
            return;
        }

        _strings.Add(key, value);
    }
    
    public void SetBool(string key, bool value)
    {
        if (_bools.ContainsKey(key))
        {
            _bools[key] = value;
            return;
        }

        _bools.Add(key, value);
    }

    public void SaveData()
    {
        _data.IntsKeys = (string[]) _ints.Keys.ToArray().Clone();
        _data.IntsValues = (int[]) _ints.Values.ToArray().Clone();

        _data.FloatsKeys = (string[]) _floats.Keys.ToArray().Clone();
        _data.FloatsValues = (float[]) _floats.Values.ToArray().Clone();
        
        _data.StringsKeys = (string[]) _strings.Keys.ToArray().Clone();
        _data.StringsValues = (string[]) _strings.Values.ToArray().Clone();
        
        _data.BoolsKeys = (string[]) _bools.Keys.ToArray().Clone();
        _data.BoolsValues = (bool[]) _bools.Values.ToArray().Clone();
        
        string contents = JsonUtility.ToJson(_data);
        System.IO.File.WriteAllText(_path, contents);
    }

    private void ReadData()
    {
        string contents = System.IO.File.ReadAllText(_path);
        _data = JsonUtility.FromJson<DataContainer>(contents);
        
        _ints.Clear();
        _floats.Clear();
        _strings.Clear();
        _bools.Clear();
        
        for (int i = 0; i < _data.IntsKeys.Length; i++)
        {
            _ints.Add(_data.IntsKeys[i], _data.IntsValues[i]);
        }
        
        for (int i = 0; i < _data.FloatsKeys.Length; i++)
        {
            _floats.Add(_data.FloatsKeys[i], _data.FloatsValues[i]);
        }
        
        for (int i = 0; i < _data.StringsKeys.Length; i++)
        {
            _strings.Add(_data.StringsKeys[i], _data.StringsValues[i]);
        }
        
        for (int i = 0; i < _data.BoolsKeys.Length; i++)
        {
            _bools.Add(_data.BoolsKeys[i], _data.BoolsValues[i]);
        }
    }

    private void Init()
    {
        _filename = "data.json";
        _path = Application.persistentDataPath + "/" + _filename;
        
        _data = new DataContainer();
        
        _ints = new Dictionary<string, int>();
        _floats = new Dictionary<string, float>();
        _strings = new Dictionary<string, string>();
        _bools = new Dictionary<string, bool>();

        if (PlayerPrefs.GetString("FirstTime", "true") == "true")
        {    
            SaveData();
            PlayerPrefs.SetString("FirstTime", "false");
        }
        else
        {
            ReadData();
        }
    }
    
    private void Awake()
    {
        Init();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
