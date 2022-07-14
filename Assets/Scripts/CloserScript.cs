using UnityEngine;
using UnityEngine.Events;

public class CloserScript : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnClose;


    private void OnMouseDown()
    {    
        OnClose.Invoke();
    }
}
