using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasScaler : MonoBehaviour
{
    public bool fullScreen = false;
    private bool setCam = false;
    private RectTransform rt;

    void Start()
    {
        rt = (RectTransform) gameObject.transform;
        rt.sizeDelta = new Vector2(UnityEngine.XR.XRSettings.eyeTextureWidth * (fullScreen ? 2f : 1f), UnityEngine.XR.XRSettings.eyeTextureHeight * (fullScreen ? 1f : 0.5f));
    }

    void Update()
    {
        if (!setCam)
        {
            var c = GameObject.FindWithTag("MainCamera");
            if (c != null)
            {
                setCam = true;
                Vector3 pos = gameObject.transform.localPosition;
                Quaternion rot = gameObject.transform.localRotation;
                Vector3 scale = gameObject.transform.localScale;
                gameObject.transform.SetParent(c.transform);
                gameObject.transform.localPosition = pos;
                gameObject.transform.localRotation = rot;
                gameObject.transform.localScale = scale;
                //gameObject.transform.localPosition = Vector3.zero;
                //gameObject.transform.localRotation = Quaternion.identity;
                gameObject.GetComponent<Canvas>().worldCamera = c.GetComponent<Camera>();
                Vector3 abc = rt.localPosition;
                abc.z = c.GetComponent<Camera>().nearClipPlane + 0.5f;
                rt.localPosition = abc;
                //float avg = (rt.sizeDelta.x + rt.sizeDelta.y) / 2f;
                float avg = Mathf.Max(rt.sizeDelta.x, rt.sizeDelta.y);
                rt.localScale = new Vector3(abc.z / avg, abc.z / avg, abc.z / avg);
            }
        }
        
        var cam = gameObject.GetComponent<Canvas>().worldCamera;
        if (cam != null)
        {
            var rot = cam.gameObject.transform.localRotation;
            var pos = cam.gameObject.transform.TransformPoint(0, 0, 0);
            var poss = cam.gameObject.transform.TransformPoint(0, 0, cam.nearClipPlane + 0.5f);
            
            gameObject.transform.position = new Vector3(poss.x, pos.y, poss.z);
            gameObject.transform.localRotation = new Quaternion(-rot.x, 0, -rot.z, rot.w);
        }
    }
}
