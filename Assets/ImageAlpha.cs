using UnityEngine;
using System.Collections;

public class ImageAlpha : MonoBehaviour {

    private CanvasGroup canvasGroup;
    public float speed = 0.02f;
    private float speedTemp = 0.0f;
    public float startAlpha = 0.0f;

    public float showTime = 1.0f;
    public bool isToZero = true;
    public bool isActive = false;
    public float delayTime = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
    public void OnEnable()
    {
        canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        if(canvasGroup == null)
        {
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
        }
        speedTemp = 0;
        isActive = true;
        canvasGroup.alpha = 0f;

        Debug.Log("ImageAlpha  "+this.gameObject.name);
        iTween.ValueTo(this.gameObject, iTween.Hash("from", 0, "to", 1, "time", showTime, "delay", delayTime, "easeType", iTween.EaseType.easeOutExpo,
            "onupdate", "updateFadeToOneTween", "onupdatetarget", this.gameObject,
            "oncompleteparams", this.gameObject));

        isToZero = false;
    }

    public void AlphaOnFalse()
    {
        isActive = false;
        canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
        }
        speedTemp = 0f;
        isToZero = true;
		iTween.ValueTo(this.gameObject, iTween.Hash("from", 1, "to", 0, "time", 0.6f, "easeType", iTween.EaseType.easeOutExpo,
            "onupdate", "updateFadeToZeroTween", "onupdatetarget", this.gameObject, "oncomplete", "updateFadeToZeroComplete",
            "oncompleteparams", this.gameObject));
    }

    void updateFadeToOneTween(float value)
    {
        canvasGroup.alpha = value;
    }

    void updateFadeToZeroTween(float value)
    {
        canvasGroup.alpha = value;
    }

    public void updateFadeToZeroComplete(GameObject target)
    {
        if(isActive == false)
            this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {      
	}
}
