using UnityEngine;
using System.Collections;

public class ImageScale : MonoBehaviour {

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
        this.gameObject.transform.localScale = new Vector3(0, 0, 0);
        iTween.ScaleTo(this.gameObject, iTween.Hash("scale", new Vector3(1f, 1f, 1f), "time", 1.6f, "easeType", iTween.EaseType.easeInOutElastic)); 
        //Debug.Log("ImageAlpha  "+this.gameObject.name);
        //iTween.ValueTo(this.gameObject, iTween.Hash("from", 0, "to", 1, "time", showTime, "delay", delayTime, "easeType", iTween.EaseType.easeOutExpo,
        //    "onupdate", "updateFadeToOneTween", "onupdatetarget", this.gameObject,
        //    "oncompleteparams", this.gameObject));

        isToZero = false;
    }

    public void AlphaOnFalse()
    {
    }

    // Update is called once per frame
    void Update () {      
	}
}
