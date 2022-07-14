using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyingObjectBehaviour : MonoBehaviour 
{

    protected AllManager AM{ get; set;}
    public CoroutineManager MoveBehaviour{ get; private set;}

    [SerializeField]
    private SpriteRenderer flyingObjSprite;
    public SpriteRenderer flyingObjSpriteRenderer{ get{ return flyingObjSprite;}}

    [SerializeField]
    protected ParticleSystem hitParticles;

    [SerializeField]
    protected int onEmptyPlacePoints;
    [SerializeField]
    protected int onHouseEnterPoints;

    public bool notHited = true;

    protected Coroutine moveCoroutine;

    public abstract void OnHouseHit(House house);

    public void SetCharacteristics(bool notHited, int onEmptyPlacePoints, int onHouseEnterPoints, CoroutineManager moveBehaviour, AllManager allManager)
    {
        this.notHited = notHited;

        this.onEmptyPlacePoints = onEmptyPlacePoints;
        this.onHouseEnterPoints = onHouseEnterPoints;
        this.MoveBehaviour = moveBehaviour;
        this.AM = allManager;
    }

    public void SetPointsBehaviour(int onEmptyPlacePoints, int onHouseEnterPoints)
    {
        this.onEmptyPlacePoints = onEmptyPlacePoints;
        this.onHouseEnterPoints = onHouseEnterPoints;
    }

    public void RotateAndStartMoovingToTarget(float z_angle, Vector3 target)
    {
        transform.RotateAround(target, Vector3.forward, z_angle);
        moveCoroutine = StartCoroutine(MoveBehaviour.SmoothGlobalGrow(transform, target));
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag.Contains("House") && notHited)
        {   
            notHited = false;
            transform.SetParent(AM.Planet.gameObject.transform);
            OnHouseHit(AM.Planet.GetHouseByCollider[coll]);
        }
    }

    protected IEnumerator TurnOffParticlesWhen(House house, bool builded, float delay)
    {
        yield return new WaitUntil(() => house.isBuilt == builded);
        yield return new WaitForSeconds(delay);
        hitParticles.Stop();
    }
       
}
