using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.Networking;

public abstract class FlyingObjectsSpawner : MonoBehaviour 
{   
    [SerializeField]
    protected AllManager AM;

    [SerializeField] private List<ObjCharacteristics> _objCharacteristics;
    [SerializeField] protected Wave[] AllWaves;
    

    private void GetNextAndStart(string objTag)
    {
        StartMoving(SpawnFromPool(objTag, transform));
    }

    private void GetNextAndStart(string objTag, int onEmptyPoints, int onHousePoints)
    {
        StartMoving(SpawnFromPool(objTag, transform, onEmptyPoints, onHousePoints));
    }

    protected abstract void StartMoving(FlyingObjectBehaviour startingObject);
    protected abstract void RefillPool();
    public abstract FlyingObjectBehaviour SpawnFromPool(string tag, Transform parent);
    public abstract FlyingObjectBehaviour SpawnFromPool(string tag, Transform parent, int onEmptyPoints, int onHousePoints);
    
    [System.Serializable]
    public struct ObjCharacteristics
    {
        public string Tag;
        public int OnEmptyPoints;
        public int OnHousePoints;
    }
    
    [System.Serializable]
    public struct Wave
    {
        public WaveContent Tags;
        public float PreDelay;

        public Wave(WaveContent flyingObjectsTags, float prewaveDelay)
        {
            Tags = flyingObjectsTags;
            PreDelay = prewaveDelay;
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        foreach (var wave in AllWaves)
        {    
            yield return new WaitForSeconds(wave.PreDelay);
            foreach (var objTag in wave.Tags.Content)
            {
                foreach (var objCharacteristic in _objCharacteristics)
                {
                    if (objTag == objCharacteristic.Tag)
                    {
                        GetNextAndStart(objTag, objCharacteristic.OnEmptyPoints, objCharacteristic.OnHousePoints);
                        break;
                    }
                }
            }
        }
    }

    void Start()
    {
        StartCoroutine(SpawnAllWaves());
    }
}
