using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeteoriteOnMoon : FlyingObjectBehaviour
{
    public override void OnHouseHit(House house)
    {
        hitParticles.Play();
        StopCoroutine(moveCoroutine); // object somehow moved to planet => moveCoroutine != null
        StartCoroutine(MoveBehaviour.SmoothColorTransition(flyingObjSpriteRenderer, AM.TRANSPARENT_WHITE_COLOR));

        if (house.isBuilt)
        {
            house.DestroyProcess();
            StartCoroutine(TurnOffParticlesWhen(house, false, 0f));
            AM.GM.AddPointsChangesToQueue(onHouseEnterPoints);
            return;
        }

        StartCoroutine(TurnOffParticlesWhen(house, false, 5f));
        AM.GM.AddPointsChangesToQueue(onEmptyPlacePoints);
    }
}
