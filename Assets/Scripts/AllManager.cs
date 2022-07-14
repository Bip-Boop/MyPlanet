using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AllManager : MonoBehaviour 
{

    [SerializeField]
    private PlanetManager planet;
    [SerializeField]
    private CoroutineManager coroutineManager;
    [SerializeField]
    private GameManager gameManager;

    public readonly Color TRANSPARENT_WHITE_COLOR = new Color(1, 1, 1, 0);
    public readonly Color WHITE_COLOR = new Color(1, 1, 1, 1);

    public PlanetManager Planet{ get{ return planet;}}
    public CoroutineManager CM{ get{ return coroutineManager;}}
    public GameManager GM{ get{ return gameManager;}}

    // For quests

    public int HousesOnPlanet
    {
        get
        {
            int num = planet.GetHouseByCollider.Values.ToArray<House>().Where((e) => e.isBuilt).Count<House>();
            print(num);
            return num;
        }
    }
}
