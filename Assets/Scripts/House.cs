using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class House : MonoBehaviour 
{   
    [SerializeField]
    protected Transform target;
    [SerializeField]
    protected Transform unbuilt;
    [SerializeField]
    protected Transform built;

    public bool isBuilt{ get; protected set;}
    public SpriteRenderer HouseSpriteRenderer{ get; set;}
    public AllManager AM{ protected get; set;}
    public abstract void BuildProcess();
    public abstract void DestroyProcess();

    protected IEnumerator setBuildStatusAfterTransformEnded(bool isbuilt)
    {
        if (isbuilt)
        {
            yield return new WaitUntil(() => target.position == built.position);
        }
        else
        {
            yield return new WaitUntil(() => target.position == unbuilt.position);
        }

        isBuilt = isbuilt;
    }
}
