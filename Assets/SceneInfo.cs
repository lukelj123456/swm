using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SceneInfo : MonoBehaviour {
    public string[] backGroundNameArray;
    public List<Sprite> sceneImageList;

    public int index = 0;
    private int currentIndex = 0;
    public Image backgroundImage;

    /// <summary>
    /// 缩略图
    /// </summary>
    public Image miniImage;
    public GameObject miniMapLayer;
    public GameObject bigMapLayer;
    public Text miniTitle;

    public GameObject closeBtn;
    public bool isShowMiniMap = true;

    /// <summary>
    /// Plus
    /// </summary>
    public GameObject plusBtn;
    public GameObject plusLayer;
    public Text plusTitle;

    /// <summary>
    /// Minus
    /// </summary>
    public GameObject minusBtn;
    public GameObject minusLayer;
    public Text bigMapTitle;
    public Text bigMapDesc;
    /// <summary>
    /// HasMap存储已经取到的Image
    /// </summary>
    public Dictionary<string, Sprite> spriteList = new Dictionary<string, Sprite>();
    // Use this for initialization
    void Start () {
	
	}

    public void OnLoad(string name)
    {
        SceneMgr.getInstance().setPageClick("Page2", false);
        string sceneIndex = name.Split('_')[0];
        string pageIndex = name.Split('_')[1];
        setCurrentIndex(int.Parse(sceneIndex) - 1);
        
        this.gameObject.SetActive(true);
        Button close = closeBtn.GetComponent<Button>();
        close.onClick.AddListener(delegate()
        {
                ImageAlpha imageAlpha = this.gameObject.GetComponent<ImageAlpha>();
                imageAlpha.AlphaOnFalse();
        });
        GameObject obj = PointMgr.getInstance().getImageAction(name);
        if (obj != null)//说明此次点击是播放一段序列帧动画
        {
                bigMapLayer.SetActive(true);

                Transform backImageTransform = obj.transform.Find("back");
                if(backImageTransform != null)
                {
                    Image img = backImageTransform.gameObject.GetComponent<Image>();
                    Sprite sp = getSpriteByImageSrc(name); 
                    if(sp != null)
                    {
                            img.sprite = sp;
                    }
                }
                foreach (Transform node in bigMapLayer.transform)
                {
                    if (node.name.Length == 1)
                    {
                        foreach (Transform nodeChild in node.transform)//判断子节点是否是对应显示的gameObject
                        {
                                Debug.Log("node  "+nodeChild.name);
                                nodeChild.gameObject.SetActive(false);
                        }
                        node.gameObject.SetActive(true);
                    }
                }
                plusLayer.SetActive(false);
                initBigMapLayer(name);
                obj.SetActive(true);
                return;
        }        

        miniMapLayer.SetActive(true);
        bigMapLayer.SetActive(false);

        if (isShowMiniMap == true)//show min map
        {
            miniImage.sprite = getSpriteByImageSrc(name);
            miniImage.gameObject.SetActive(true);
            miniMapLayer.SetActive(true);
            miniMapLayer.name = "mini_" + name;
            Button miniBtn = miniMapLayer.GetComponent<Button>();
            miniTitle.text = PointMgr.getInstance().getTitleByIndex(int.Parse(sceneIndex) - 1, int.Parse(pageIndex) - 1);
            miniBtn.onClick.AddListener(delegate ()//show big map
            {
                ImageAlpha imageAlpha = miniMapLayer.GetComponent<ImageAlpha>();
                imageAlpha.AlphaOnFalse();
                initBigMapLayer(name);
                bigMapLayer.SetActive(true);
                closeBtn.SetActive(true);
            });
        }
    }

    public void OnEnable()
    {        
    }

    public void initBigMapLayer(string bigName)
    {
            string sceneIndex = bigName.Split('_')[0];
            string pageIndex = bigName.Split('_')[1];
            bigMapTitle.text = PointMgr.getInstance().getTitleByIndex(int.Parse(sceneIndex) - 1, int.Parse(pageIndex) - 1);
            bigMapDesc.text = PointMgr.getInstance().getDescByIndex(int.Parse(sceneIndex) - 1, int.Parse(pageIndex) - 1);
            plusTitle.text = PointMgr.getInstance().getTitleByIndex(int.Parse(sceneIndex) - 1, int.Parse(pageIndex) - 1);
            plusLayer.SetActive(true);
            minusLayer.SetActive(false);
            foreach (Transform node in bigMapLayer.transform)
            {
                if (node.name == sceneIndex)
                {
                        foreach (Transform nodeChild in node.transform)//判断子节点是否是对应显示的gameObject
                        {
                            if (nodeChild.name == bigName)
                            {
                                Image nodeImage = nodeChild.gameObject.GetComponent<Image>();
                                if(nodeImage != null)
                                {
                                    nodeImage.sprite = getSpriteByImageSrc(bigName);
                                    nodeImage.name = bigName;
                                    nodeChild.gameObject.SetActive(true);
                                }
                            }
                            else
                                nodeChild.gameObject.SetActive(false);
                        }
                        node.gameObject.SetActive(true);                
                }
                else if (name.Length == 1)
                    node.gameObject.SetActive(false);
            }
            Button pBtn = plusBtn.GetComponent<Button>();
            pBtn.onClick.AddListener(delegate ()
            {
                    minusLayer.SetActive(true);
                    plusLayer.SetActive(false);
            });

            Button mBtn = minusBtn.GetComponent<Button>();
            mBtn.onClick.AddListener(delegate ()
            {
                    minusLayer.SetActive(false);
                    plusLayer.SetActive(true);
            });
    }

    public void setCurrentIndex(int index)
    {
            this.currentIndex = index;
    }

    public int getCurrentIndeex()
    {
            return this.currentIndex;
    }

    void OnDisable()
    {
            isShowMiniMap = true;
            SceneMgr.getInstance().setPageClick("Page2", true);
    }

    // Update is called once per frame
    void Update () {
	
	}

    public Sprite getSpriteByImageSrc(string name)
    {
            string sceneIndex = name.Split('_')[0];
            string pageIndex = name.Split('_')[1];

            string imgSrc = PointMgr.getInstance().getImgByIndex(getCurrentIndeex(), int.Parse(pageIndex));
            Sprite sprite = SceneMgr.getInstance().getCacheImage(imgSrc);
            if(sprite == null)
            {
                Debug.Log("getSpriteByImageSrc  " + imgSrc);  
                Object obj = Resources.Load(imgSrc);
                if (obj == null)
                {
                    Debug.Log("getSpriteByImageSrc  " + imgSrc+" is null");  
                    return null;
                }
                
                Texture2D texture = (Texture2D)Instantiate(obj);
                sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                sprite.name = imgSrc;

                SceneMgr.getInstance().addCacheImage(imgSrc, sprite);
            }
            return sprite;
        }
}
