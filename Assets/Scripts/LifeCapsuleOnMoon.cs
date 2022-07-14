using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCapsuleOnMoon : FlyingObjectBehaviour 
{   
    public override void OnHouseHit(House house)
    {
        hitParticles.Play();
        StopCoroutine(moveCoroutine); // object somehow moved to planet => moveCoroutine != null
        StartCoroutine(MoveBehaviour.SmoothColorTransition(flyingObjSpriteRenderer, AM.TRANSPARENT_WHITE_COLOR));

        if (!house.isBuilt)
        {
            house.BuildProcess();
            StartCoroutine(TurnOffParticlesWhen(house, true, 0f));
            AM.GM.AddPointsChangesToQueue(onEmptyPlacePoints);
            return;
        }

        StartCoroutine(TurnOffParticlesWhen(house, true, 5f));
        AM.GM.AddPointsChangesToQueue(onHouseEnterPoints);
    }
        
}
