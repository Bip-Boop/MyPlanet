using System;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class DataContainer
{
   /* [Serializable]
    public class Pairs<TValue>
    {    
        [SerializeField]
        public string[] Keys;
        [SerializeField]
        public TValue[] Values;
    }

    [SerializeField]
    public Pairs<int> Ints;
    [SerializeField]
    public Pairs<float> Floats;
    [SerializeField]
    public Pairs<string> Strings;
    [SerializeField]
    public Pairs<bool> Bools; */

    public string[] IntsKeys;
    public int[] IntsValues;

    public string[] FloatsKeys;
    public float[] FloatsValues;

    public string[] StringsKeys;
    public string[] StringsValues;

    public string[] BoolsKeys;
    public bool[] BoolsValues;
}
