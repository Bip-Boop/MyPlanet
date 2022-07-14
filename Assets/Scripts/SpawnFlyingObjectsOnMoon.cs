using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlyingObjectsOnMoon : FlyingObjectsSpawner
{
    [SerializeField]
    private LifeCapsuleOnMoon lifeCapsulePrefab;
    [SerializeField]
    private CoroutineManager lifeCapsuleCoroutines;

    [System.Serializable]
    public struct Pool
    {
        public string tag;
        public FlyingObjectBehaviour prefab;
        public int size;
    }
    [SerializeField]
    private List<Pool> pools;
    private Dictionary<string, Queue<FlyingObjectBehaviour>> poolDictionary;


    public override FlyingObjectBehaviour SpawnFromPool(string tag, Transform parent)
    {
        FlyingObjectBehaviour tospawn = poolDictionary[tag].Dequeue();

        tospawn.gameObject.SetActive(true);
        tospawn.notHited = true;
        tospawn.transform.SetParent(parent);
        tospawn.transform.localPosition = Vector3.zero;
        tospawn.transform.rotation = Quaternion.identity;

        poolDictionary[tag].Enqueue(tospawn);

        return tospawn;
    }

    public override FlyingObjectBehaviour SpawnFromPool(string tag, Transform parent, int onEmptyPoints, int onHousePoints)
    {
        FlyingObjectBehaviour tospawn = poolDictionary[tag].Dequeue();

        tospawn.gameObject.SetActive(true);
        tospawn.notHited = true;
        tospawn.transform.SetParent(parent);
        tospawn.SetPointsBehaviour(onEmptyPoints, onHousePoints);
        tospawn.transform.localPosition = Vector3.zero;
        tospawn.transform.rotation = Quaternion.identity;

        poolDictionary[tag].Enqueue(tospawn);

        return tospawn;
    }

    protected override void StartMoving(FlyingObjectBehaviour startingObject)
    {
        startingObject.RotateAndStartMoovingToTarget(Random.Range(0f, 360f), AM.Planet.transform.position);
    }

    protected override void RefillPool()
    {
        poolDictionary = new Dictionary<string, Queue<FlyingObjectBehaviour>>();
        FlyingObjectBehaviour spawned_obj = null;

        foreach (Pool pool in pools)
        {
            Queue<FlyingObjectBehaviour> objectPool = new Queue<FlyingObjectBehaviour>();

            for (int i = 0; i < pool.size; i++)
            {
                spawned_obj = Instantiate<FlyingObjectBehaviour>(pool.prefab, transform);
                spawned_obj.gameObject.SetActive(false);
                spawned_obj.SetCharacteristics(true, 1, 5, lifeCapsuleCoroutines, AM);
                objectPool.Enqueue(spawned_obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    private void Awake()
    {
        RefillPool();
    }
}
