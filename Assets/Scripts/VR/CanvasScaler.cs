using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasScaler : MonoBehaviour
{
    private bool setCam = false;

    void Start()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(UnityEngine.XR.XRSettings.eyeTextureWidth, UnityEngine.XR.XRSettings.eyeTextureHeight / 2);
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
            }
        }
    }
}
