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
    private InputField currentInput = null;

    public void Awake()
    {
        SteamVR_Events.System(EVREventType.VREvent_KeyboardClosed).Listen(OnKeyboardClosed);
        if (laserPointer == null)
        {
            var go = GameObject.FindWithTag("MainCamera");
            if (go != null)
            {
                laserPointer = go.transform.parent.gameObject.GetComponentInChildren(typeof(SteamVR_LaserPointer)) as SteamVR_LaserPointer;
            }
        }
        if (laserPointer != null)
        {
            laserPointer.PointerIn += HandlePointerIn;
            laserPointer.PointerOut += HandlePointerOut;
            laserPointer.PointerClick += HandleTriggerClicked;
        }
    }

    public void Update()
    {
        if (laserPointer == null)
        {
            var go = GameObject.FindWithTag("MainCamera");
            if (go != null)
            {
                laserPointer = go.transform.parent.gameObject.GetComponentInChildren(typeof(SteamVR_LaserPointer)) as SteamVR_LaserPointer;
            }
            if (laserPointer != null)
            {
                laserPointer.PointerIn += HandlePointerIn;
                laserPointer.PointerOut += HandlePointerOut;
                laserPointer.PointerClick += HandleTriggerClicked;
            }
        }
    }

    private void OnKeyboardClosed(VREvent_t args)
    {
        if (currentInput != null)
        {
            System.Text.StringBuilder textBuilder = new System.Text.StringBuilder(1024);
			uint size = SteamVR.instance.overlay.GetKeyboardText(textBuilder, 1024);
			currentInput.text = textBuilder.ToString();
            currentInput = null;
        }
    }

    private void HandleTriggerClicked(object sender, PointerEventArgs e)
    {
        VRUIInput inp = e.target.gameObject.GetComponent<VRUIInput>();
        if (inp != null)
        {
            inp.OnClick.Invoke();
        }
        InputField inpf = e.target.gameObject.GetComponent<InputField>();
        if (inpf != null)
        {
            bool stillOpenKeyboard = true;
            if (currentInput != null)
            {
                SteamVR.instance.overlay.HideKeyboard();
                if (currentInput == inpf)
                {
                    stillOpenKeyboard = false;
                }
            }
            if (stillOpenKeyboard)
            {
                currentInput = inpf;
                SteamVR.instance.overlay.ShowKeyboard(0, 0, 0, inpf.placeholder.GetComponent<Text>().text, (uint) inpf.characterLimit, inpf.text, 0);
            }
        }
        ButtonHorizontalSplit btnhz = e.target.gameObject.GetComponent<ButtonHorizontalSplit>();
        if (btnhz != null)
        {
            btnhz.onClickRight.Invoke();
        }
        Button btn = e.target.gameObject.GetComponent<Button>();
        if (btn != null)
        {
            //ExecuteEvents.Execute(btn.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            btn.onClick.Invoke();
        }
    }

    private void HandlePointerIn(object sender, PointerEventArgs e)
    {
        VRUIInput inp = e.target.gameObject.GetComponent<VRUIInput>();
        if (inp != null)
        {
            inp.OnHoverStart.Invoke();
        }
    }

    private void HandlePointerOut(object sender, PointerEventArgs e)
    {
        VRUIInput inp = e.target.gameObject.GetComponent<VRUIInput>();
        if (inp != null)
        {
            inp.OnHoverEnd.Invoke();
        }
    }
}
