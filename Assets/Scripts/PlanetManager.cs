using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour 
{   
    ////////// FIELDS //////////

    // Publics

    public Dictionary<Collider2D, House> GetHouseByCollider{ get; private set;}

    [SerializeField]
    private SpawnHouses spawnHousesControl;

    private AnimationCurve curve;


    // Privates
    private bool isRotating;
    private float rotationSpeed;
    private Vector3 rotateVector;



    ////////// METHODS //////////

    // Publics

    public void RotateChanges(float speed, bool is_rotating)
    {
        isRotating = is_rotating;
        rotationSpeed = speed;
    }

    public void SpawnHouses(int amount, float scale)
    {   
        if (GetHouseByCollider != null)
            GetHouseByCollider.Clear();
        else
            GetHouseByCollider = new Dictionary<Collider2D, House>();

        List<House> all_houses = spawnHousesControl.SpawnBuildings(amount, scale);

        for (int i = 0; i < all_houses.Count; i++)
        {
            GetHouseByCollider.Add(all_houses[i].GetComponentInChildren<Collider2D>(), all_houses[i]);
        }
    }

    public void UnspawnHouses()
    {
        spawnHousesControl.UnspawnBuildings();
    }

    // Privates

    void Awake()
    {
        isRotating = false;
        rotationSpeed = 0f;
        rotateVector = Vector3.zero;
        SpawnHouses(24, 1);
    }

    void Update()
    {
        if (isRotating)
        {   
            rotateVector.Set(0, 0, Time.deltaTime * rotationSpeed);
            this.gameObject.transform.Rotate(rotateVector);
        }
    }
}
