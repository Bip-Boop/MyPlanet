using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseOnMoon : House 
{   
    private Coroutine currentGrow;
    private Coroutine currentColorTransition;
    private Coroutine buildStatusChanging;
    private Vector3 startPos;

    public override void BuildProcess()
    {

        if (currentGrow != null)
        {
            StopCoroutine(currentGrow);
            StopCoroutine(currentColorTransition);
            StopCoroutine(buildStatusChanging);
        }
        else // if 1st time
        {
            HouseSpriteRenderer.color = AM.TRANSPARENT_WHITE_COLOR;
            HouseSpriteRenderer.enabled = true;
        }

        buildStatusChanging = StartCoroutine(setBuildStatusAfterTransformEnded(true));
        currentGrow = StartCoroutine(AM.CM.SmoothGlobalGrowBetween(target, unbuilt, built));
        currentColorTransition = StartCoroutine(AM.CM.SmoothColorTransition(HouseSpriteRenderer, AM.WHITE_COLOR));
    }

    public override void DestroyProcess()
    {
        StopCoroutine(buildStatusChanging);
        StopCoroutine(currentGrow);
        StopCoroutine(currentColorTransition);

        buildStatusChanging = StartCoroutine(setBuildStatusAfterTransformEnded(false));
        currentGrow = StartCoroutine(AM.CM.SmoothGlobalGrowBetween(target, built, unbuilt));
        currentColorTransition = StartCoroutine(AM.CM.SmoothColorTransition(HouseSpriteRenderer, AM.TRANSPARENT_WHITE_COLOR));
    }
        
}
