using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Light[] lights;
    public float fadeDuration = 3f;
   
    private bool isFading = false;
    private float timer = 0f;
    void Start()
    {
        SetLightsIntensity(0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q) && !isFading)
        {
            SetLightsIntensity(1f);
            isFading = true;
            timer = 0f;
        }

        if (isFading)
        {
            timer += Time.deltaTime;
            float intensity = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            SetLightsIntensity(intensity);

            if(timer >= fadeDuration)
            {
                isFading = false;
            }
        }
    }

    void SetLightsIntensity(float intensity)
    {
        foreach(var light in lights)
        {
            light.intensity = intensity;
        }
    }
}
