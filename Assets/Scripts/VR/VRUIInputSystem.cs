using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using Valve.VR.Extras;
using Valve.VR;
using Sanicball.UI;

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
            inp.OnClick.Invoke();
        }
        InputField inpf = e.target.gameObject.GetComponent<InputField>();
        if (inpf != null)
        {
            SteamVR.instance.overlay.ShowKeyboard(0, 0, 0, inpf.placeholder.GetComponent<Text>().text, (uint) inpf.characterLimit, inpf.text, 0);
        }
        ButtonHorizontalSplit btnhz = e.target.gameObject.GetComponent<ButtonHorizontalSplit>();
        if (btnhz != null)
        {
            btnhz.onClickRight.Invoke();
        }
        else
        {
            Button btn = e.target.gameObject.GetComponent<Button>();
            if (btn != null)
            {
                //ExecuteEvents.Execute(btn.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
                btn.onClick.Invoke();
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
