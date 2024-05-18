using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BrokenLamp : MonoBehaviour
{
    private bool isStarted;
    private Light2D lamp;
    // Start is called before the first frame update
    void Start()
    {
        lamp = transform.GetComponent<Light2D>();
        lamp.intensity = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
     if(!isStarted)
        {
            StartCoroutine(brokenLampAnimation());
        }   
    }

    IEnumerator brokenLampAnimation()
    {
        isStarted = true;
        // lamp light intensity 2 -> 1 -> 0.5 -> 0 -> 0.5 -> 2
         lamp.intensity = 1.5f;
        yield return new WaitForSeconds(1.25f);
        lamp.intensity = 0;
        yield return new WaitForSeconds(1.25f);
        lamp.intensity = 1f;
        yield return new WaitForSeconds(0.15f);
        lamp.intensity = 0f;
        yield return new WaitForSeconds(0.15f);
        lamp.intensity = 1.5f;
        yield return new WaitForSeconds(1.25f);
        isStarted = false;

    }
}
