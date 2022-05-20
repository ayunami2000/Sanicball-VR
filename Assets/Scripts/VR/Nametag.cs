using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class Nametag : MonoBehaviour
{
    public GameObject cam = null;

    void Start()
    {
        Vector3 sc = transform.parent.localScale;
        transform.localScale = new Vector3(1f / sc.x, 1f / sc.y, 1f / sc.z);
    }

    void Update()
    {
        if (cam == null)
            cam = GameObject.FindWithTag("MainCamera");
        transform.position = transform.parent.position + Vector3.up;
        if (cam != null)
        {
            Vector3 abc = cam.transform.eulerAngles;
            transform.eulerAngles = new Vector3(abc.x, abc.y, 0);
            float fard = Vector3.Distance(cam.transform.position, transform.position);
            Vector3 sc = transform.parent.localScale;
            transform.localScale = new Vector3(fard / sc.x, fard / sc.y, fard / sc.z) / 20f;
        }
    }
}
