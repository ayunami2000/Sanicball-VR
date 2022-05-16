using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using Valve.VR.Extras;

public class VRUIInputSystem : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;

    public void Awake()
    {
        laserPointer.PointerIn += HandlePointerIn;
        laserPointer.PointerOut += HandlePointerOut;
        laserPointer.PointerClick += HandleTriggerClicked;
    }

    public void HandleTriggerClicked(object sender, PointerEventArgs e)
    {
        VRUIInput inp = e.target.gameObject.GetComponent<VRUIInput>();
        if (inp != null)
        {
            if (inp.useButtonClickEvent)
            {
                inp.gameObject.GetComponent<Button>().onClick.Invoke();
                //ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
            else
            {
                inp.OnClick.Invoke();
            }
        }
    }

    public void HandlePointerIn(object sender, PointerEventArgs e)
    {
        VRUIInput inp = e.target.gameObject.GetComponent<VRUIInput>();
        if (inp != null)
        {
            inp.OnHoverStart.Invoke();
        }
    }

    public void HandlePointerOut(object sender, PointerEventArgs e)
    {
        VRUIInput inp = e.target.gameObject.GetComponent<VRUIInput>();
        if (inp != null)
        {
            inp.OnHoverEnd.Invoke();
        }
    }
}
