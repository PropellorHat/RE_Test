using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class UICameraText : MonoBehaviour
{
    private TextMeshProUGUI text;
    public CinemachineBrain cb;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = cb.ActiveVirtualCamera.VirtualCameraGameObject.name;
    }
}
