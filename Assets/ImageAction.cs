using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageAction : MonoBehaviour {

    public Vector3 startPoint;
    public float endY = 0;

    public string[] nameArray;
    public List<Sprite> sceneImageList;

    public int index = 0;
    public int currentIndex;
    public Image imageComponent;

    public int timeStep = 10;
    public float delayTime = 0f;

    public bool isActiveButton = false;
    public bool isLoop = false;
    private bool isBreak = false;
    public float scale;

    private bool isPlaying = false;
    // Use this for initialization
    void Start()
    {
    }

    public void OnDisable()
    {
            index = 0;
            isPlaying = false;
            isBreak = false;
            imageComponent.sprite = sceneImageList[0];
    }
    
    public void OnEnable()
    {       
        imageComponent = this.gameObject.GetComponent<Image>();
        if (imageComponent == null)
                imageComponent = this.gameObject.AddComponent<Image>();
        if(sceneImageList.Count == 0)
        {
            StartCoroutine(loadImage(0, null));
            imageComponent.sprite = sceneImageList[0];
        }else
        {
            isBreak = false;
        }
   
        if (isActiveButton == true)
        {
            Button button = this.gameObject.GetComponent<Button>();
            if (button == null)
                this.gameObject.AddComponent<Button>();
        }
        currentIndex = 0;
		isPlaying = false;
        Invoke("StartPlay", delayTime);
    }


    public delegate void callBackPlayScene();
    public callBackPlayScene delegateMethod;
    public IEnumerator loadImage(int startIndex,
     callBackPlayScene callBack = null, float frameTime = 0.01f)
    {
        if(startIndex >= nameArray.Length)
                yield return 0;
        else
        {
                Sprite sprite = SceneMgr.getInstance().getCacheImage(nameArray[startIndex]);
                if (sprite == null)
                { 
                       Object  obj = Resources.Load(nameArray[startIndex]);
                       if (obj == null)
                       {
                           if (callBack != null)
                                callBack.Invoke();
                           Debug.Log(nameArray[startIndex] + " 为空 ");
                           yield return 0;
                       }
                       else
                       {
                           Texture2D texture = (Texture2D)Instantiate(obj);
                           sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                           sprite.name = nameArray[startIndex];
                           SceneMgr.getInstance().addCacheImage(nameArray[startIndex], sprite);
                       }                    
                }
                
                sceneImageList.Add(sprite);

                startIndex++;
                yield return new WaitForSeconds(frameTime);
                if(this.gameObject != null)
                        StartCoroutine(loadImage(startIndex, callBack));
                yield return 0;
            }
    }

    public void StartPlay()
    {
        isPlaying = true;
    }

    // Update is called once per frame
    void Update () {
        if(sceneImageList.Count > 0 && isPlaying)
        {
            index++;
            if(index % timeStep == 0)
            {
                if (currentIndex >= sceneImageList.Count)
                {
                    currentIndex = 0;
                    if (isLoop == false)
                        isBreak = true;
                    else
                        isBreak = false;
                }
                    
                if(isBreak == false)
                {
                    imageComponent.sprite = sceneImageList[currentIndex];
                    currentIndex++;
                }
            }
            if (index > 100000)
                index = 0;
        }
    }
}
