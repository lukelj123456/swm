using UnityEngine;
using System.Collections;

public class ImageMove : MonoBehaviour {

    public Vector3 startPoint;
    public float endX = 0;
    public float endY = 0;
    public float delayTime = 0;
    public float moveSpeed = 2000f;
    public iTween.EaseType tweenType  = iTween.EaseType.linear;
    // Use this for initialization
    void Start()
    {
    }

    void onCompleteActive()
    {
    }

    public void OnEnable()
    {
        this.gameObject.transform.localPosition = startPoint;
        //iTween.MoveTo(this.gameObject, iTween.Hash("y", endY, "easeType", "easeIn", "time", 0.6f,
        //    "oncomplete", "onCompleteActive", "oncompletetarget", this.gameObject));

        iTween.MoveTo(this.gameObject, iTween.Hash("speed", moveSpeed, "easeType", tweenType, "islocal", true,
                    "position", new Vector3(endX, endY, 0), "oncomplete", "delay", delayTime,
                    "onCompleteActive", "oncompletetarget", gameObject));
    }

    // Update is called once per frame
    void Update () {

    }
}
