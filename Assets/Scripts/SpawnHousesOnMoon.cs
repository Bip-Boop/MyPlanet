using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHousesOnMoon : SpawnHouses 
{   
    [SerializeField]
    private Transform parentTransform;
    [SerializeField]
    private Vector3 startPos;
    [SerializeField]
    private Sprite[] differentSprites;
    [SerializeField]
    private float buildMagnitude;

    public List<House> AllHouses{ get; private set;}
    public List<SpriteRenderer> AllRenderers{ get; set;}

    public override List<House> SpawnBuildings(int amount, float scale)
    {   
        AllHouses = new List<House>();
        AllRenderers = new List<SpriteRenderer>();

        float spacing = 360f / amount;
        float passed = 0f;
        int spritesAmount = differentSprites.Length;

        Vector3 scaleVector = new Vector3(scale, scale, 1);
        House instantiatedHouse = null;
        SpriteRenderer houseSprite = null;

        for (int i = 0; i < amount; i++)
        {
            instantiatedHouse = Instantiate<House>(housePrefab, parentTransform);
            instantiatedHouse.gameObject.transform.localScale = scaleVector;
            instantiatedHouse.gameObject.transform.localPosition = startPos;
            instantiatedHouse.gameObject.transform.RotateAround(parentTransform.position, Vector3.forward, passed);
            instantiatedHouse.AM = AM;

            houseSprite = instantiatedHouse.GetComponentInChildren<SpriteRenderer>();
            houseSprite.enabled = false;
            houseSprite.sprite = differentSprites[Random.Range(0, spritesAmount)];
            instantiatedHouse.HouseSpriteRenderer = houseSprite;
            AllRenderers.Add(houseSprite);

            passed += spacing;
            AllHouses.Add(instantiatedHouse);
        }

        return AllHouses;
    }

    public override void UnspawnBuildings()
    {
        print("Bye, Moon");        
    }
}
