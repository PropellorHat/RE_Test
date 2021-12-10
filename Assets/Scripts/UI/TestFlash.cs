using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestFlash : MonoBehaviour
{
    public float interval;

    private TextMeshProUGUI text;
    
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void OnEnable()
    {
        StopCoroutine("Flash");
        StartCoroutine("Flash");
    }

    private void OnDisable()
    {
        StopCoroutine("Flash");
    }

    private IEnumerator Flash()
    {
        while (true)
        {
            text.enabled = true;
            yield return new WaitForSeconds(interval);
            text.enabled = false;
            yield return new WaitForSeconds(interval);
        }
        
    }
}
