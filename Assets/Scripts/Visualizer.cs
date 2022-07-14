using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float[] _bands;
    private float[] _bandsBuffer;
    private float[] _bufferDecrease;
    private float[] _bufferIncrease;
    [SerializeField] private Visualizable[] _visualizers;

    [Header("General")]
    [SerializeField] private float _min;
    [SerializeField] private int _historyRange;
    [SerializeField] private float _multiplyer;

    [System.Serializable]
    private struct Visualizable
    {
        public int BandID;
        public float Multiplyer;
        public GameObject _object;
    }

    private float[] _samples;
    private Vector3 _setVector;
    private float _xyScale;

    private List<float>[] _historyList;

    private void Awake ()
    {
        _samples = new float[512];

        int len = _bands.Length;
        _bandsBuffer = new float[len];
        _bufferDecrease = new float[len];
        _bufferIncrease = new float[len];
        _historyList = new List<float>[len];

        for (int i = 0; i < len; i++)
        {
            _bufferIncrease[i] = 0.005f;
            _bufferDecrease[i] = 0.005f;
            _historyList[i] = new List<float>(); 
        }

	}
	
	// Update is called once per frame
	void Update ()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
        CalculateBands();
        CalculateBuffer2(_historyRange);

        for (int i = 0; i < _visualizers.Length; i++)
        {
            _xyScale = _bandsBuffer[_visualizers[i].BandID] * _visualizers[i].Multiplyer + _min;
            _setVector.Set(_xyScale, _xyScale, 0);
            _visualizers[i]._object.transform.localScale = _setVector;
        }
    }

    private void CalculateBands()
    {
        int count = 0;
        int bandsCount = _bands.Length;

        for (int i = 0; i < bandsCount; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i + 1);

            if (i == bandsCount - 1)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }

            average /= count;
            _historyList[i].Add(average);
            _bands[i] = average;
        }
    }

    private void CalculateBandBuffer()
    {
        for (int i = 0; i < _bands.Length; ++i)
        {
            if (_bands[i] > _bandsBuffer[i])
            {
                _bandsBuffer[i] = _bands[i];
                //_bufferIncrease[i] *= 1.2f;
                _bufferDecrease[i] = 0.005f;
            }

            if (_bands[i] < _bandsBuffer[i])
            {
                _bandsBuffer[i] -= _bufferDecrease[i];
                _bufferDecrease[i] *= 1.2f;
               //_bufferIncrease[i] = 0.005f;
            }
        }
    }

    private void CalculateBuffer2(int historyLenght)
    {
        for (int i = 0; i < _bandsBuffer.Length; i++)
        {
            if(_historyList[i].Count >= historyLenght)
            {
                float average = 0;

                for (int j = _historyList[i].Count - 1; j >= _historyList[i].Count - historyLenght; j--)
                {
                    average += _historyList[i][j];
                }

                average /= historyLenght;
                _bandsBuffer[i] = average;
            }
        }

        for (int i = 0; _historyList[_historyList.Length - 1].Count > historyLenght; i++)
        {
            for(int j = 0; _historyList[i].Count >= historyLenght; j++)
            {
                _historyList[i].RemoveAt(j);
            }
        }
    }
}
