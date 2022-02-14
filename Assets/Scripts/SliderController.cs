using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;
    public float minRange;
    public float maxRange;


    public int clickCount;
    public bool sliderLerp = true;
    public float minValue;
    public float maxValue;
    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(SliderIncreaseAndDecrease());
    }
    IEnumerator SliderIncreaseAndDecrease()
    {
            yield return new WaitForSeconds(0.5f);
            sliderLerp = true;
            StartCoroutine(IncreaseSliderOverTime(0.3f));
            yield return new WaitForSeconds(0.7f);
            StartCoroutine(DecreaseSliderOverTime(0.3f));
            yield return new WaitForSeconds(0.7f);
            StartCoroutine(SliderIncreaseAndDecrease());
    }



    public void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            
            //sliderLerp = false;
            if (slider.value >= minRange && slider.value <= maxRange)
            {
                Debug.Log("Good");
            }
            else
            {
                Debug.Log("Bad Timing");
                sliderLerp = false;
                StopAllCoroutines();
                slider.value = 0;
               
                StartCoroutine(SliderIncreaseAndDecrease());
            }
            
        }
    }

    IEnumerator IncreaseSliderOverTime(float seconds)
    {
        float animationTime = 0f;
        while (animationTime < seconds)
        {
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            if(sliderLerp)
            {
                slider.value = Mathf.Lerp(0f, 1f, lerpValue);
            }
           
            yield return null;
        }
    }
    IEnumerator DecreaseSliderOverTime(float seconds)
    {
        float animationTime = 0f;
        while (animationTime < seconds)
        {
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            if(sliderLerp)
            {
                slider.value = Mathf.Lerp(1f, 0f, lerpValue);
            }
            
            yield return null;
        }
    }
}
