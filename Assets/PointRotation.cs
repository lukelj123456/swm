using UnityEngine;
using System.Collections;

public class PointRotation : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        onCompleteActive();

        //iTween.ColorFrom(this.gameObject, iTween.Hash("a", 0, "time", 15.0f));
    }
    void onCompleteActive()
    {
        RectTransform rect = this.gameObject.GetComponent<RectTransform>();
        if (rect.rotation.z == 1.0f)
            iTween.RotateTo(this.gameObject, iTween.Hash("z", 0, "easeType", "linear", "time", 0.5, "oncomplete", "onCompleteActive", "oncompletetarget", gameObject));
        else
            iTween.RotateTo(this.gameObject, iTween.Hash("z", -180, "easeType", "linear", "time", 0.5, "oncomplete", "onCompleteActive", "oncompletetarget", gameObject));
    }

    // Update is called once per frame
    void Update () {
	
	}
}
