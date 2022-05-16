using UnityEngine;
using UnityEngine.Events;

public class VRUIInput : MonoBehaviour
{
    public bool useButtonClickEvent = false;
    public UnityEvent OnClick;
    public UnityEvent OnHoverStart;
    public UnityEvent OnHoverEnd;
}