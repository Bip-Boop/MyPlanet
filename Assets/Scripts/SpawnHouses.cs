using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnHouses : MonoBehaviour 
{   
    public AllManager AM;
    [SerializeField]
    protected House housePrefab;

    public abstract List<House> SpawnBuildings(int amount, float scale);

    public abstract void UnspawnBuildings();
}
