using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CoroutineManager : ScriptableObject
{   
    [System.Serializable]
    public struct AnimationSettings
    {
        public AnimationCurve curve;
        public float speed;
    }

    [SerializeField]
    private AnimationSettings GrowSettings;
    [SerializeField]
    private AnimationSettings ColorTransitionSettings;

    public IEnumerator SmoothGlobalGrow(Transform growObject, Vector3 targetPos)
    {
        float t = 0f;
        float s = 0f;

        Vector3 startPos = growObject.position;
        Vector3 delta = targetPos - startPos;

        while (t <= 1f)
        {   
            t += Time.deltaTime * GrowSettings.speed;
            s = GrowSettings.curve.Evaluate(t);
            growObject.position = startPos + delta * s;
            yield return 0;
        }
    }

    public IEnumerator SmoothGlobalGrowBetween(Transform target, Transform start, Transform stop)
    {
        float t = 0f;
        float s = 0f;
        Vector3 delta = stop.position - start.position;

        while (t <= 1f)
        {
            s = GrowSettings.curve.Evaluate(t);
            delta = stop.position - start.position;
            target.position = start.position + s * delta;
            t += Time.deltaTime * GrowSettings.speed;
            yield return 0;
        }

    }

    public IEnumerator SmoothColorTransition(SpriteRenderer sr, Color targetColor)
    {
        float t = 0f;
        float c = 0f;

        Color startColor = sr.color;
        Color deltaColor = targetColor - startColor;

        while (t <= 1f)
        {
            t += Time.deltaTime * ColorTransitionSettings.speed;
            c = ColorTransitionSettings.curve.Evaluate(t);
            sr.color = startColor + deltaColor * c;
            yield return 0;
        }


    }


}
