using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonGroup : MonoBehaviour {

    public List<Button> btnGroup;
    public SpriteRenderer selectedImage;
    public SpriteRenderer normalImage;
	// Use this for initialization
	void Start () {
        btnGroup = new List<Button>();
        GameObject selected = GameObject.Find("ButtonAcceleratorOverSprite");
        selectedImage = selected.GetComponent<SpriteRenderer>();

        GameObject normal = GameObject.Find("ButtonArrowUpSprite");
        normalImage = normal.GetComponent<SpriteRenderer>();

        for(int i = 0;i<5;i++)
        {
            GameObject item = GameObject.Find("selectItem"+i);
            Button btn = item.GetComponent<Button>();
            btn.onClick.AddListener(delegate()
            {
                this.btnClick(item);
            });
            btnGroup.Add(btn);
        }
	}

    public void btnClick(GameObject obj)
    {
        for (int i = 0; i < 5; i++)
        {
            Text text = btnGroup[i].GetComponentInChildren<Text>();
            text.fontSize = 12;
            text.color = Color.black;
            btnGroup[i].image.sprite = normalImage.sprite;
        }

        int currentSelectIndex = 0;
        switch(obj.name)
        {
            case "selectItem0":
                currentSelectIndex = 0;
                break;
            case "selectItem1":
                currentSelectIndex = 1;
                break;
            case "selectItem2":
                currentSelectIndex = 2;
                break;
            case "selectItem3":
                currentSelectIndex = 3;
                break;
            case "selectItem4":
                currentSelectIndex = 4;
                break;
        }

        btnGroup[currentSelectIndex].image.sprite = selectedImage.sprite;
        Text btnText = btnGroup[currentSelectIndex].GetComponentInChildren<Text>();
        btnText.fontSize = 20;        
        btnText.color = Color.red;
    }

    public void setSelectedItem(int index)
    {
        for(int i=0;i<5;i++)
        {
            btnGroup[i].image.sprite = normalImage.sprite;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
