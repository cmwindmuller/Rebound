using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollLoop : MonoBehaviour {
    
    ScrollRect scroll;
    List<GameObject> items;
    float screenHeight, shelfHeight, limitY;
    float shelfWidth, showX, hideX;
    public bool isVisible;
    public float btnHeight = 160;//includes gap now
    public float btnOffset = 40;
    float btnHeightActual;
    //float btnGap = 20;

	// Use this for initialization
	void Awake () {
        scroll = GetComponent<ScrollRect>();
        scroll.movementType = ScrollRect.MovementType.Unrestricted;
	}

    private void Start()
    {
        screenHeight = scroll.GetComponent<RectTransform>().rect.height;
        shelfWidth = scroll.GetComponent<RectTransform>().rect.width;
        showX = shelfWidth / 2;
        hideX = -showX;
    }

    private void LateUpdate()
    {
        //Loop the Scrolling, only if there's enough content
        if (scroll.movementType != ScrollRect.MovementType.Unrestricted)
            return;
        //keep the .content reasonably in place
        Vector3 shelfPos = scroll.content.position;
        if(shelfPos.y < -limitY)
        {
            shelfPos.y += limitY;
            scroll.content.position = shelfPos;
        }
        else if(shelfPos.y > limitY)
        {
            shelfPos.y -= limitY;
            scroll.content.position = shelfPos;
        }
        if (items == null)
            return;
        //menu items loop around
        for (int i = 0; i < items.Count; i++)
        {
            Vector3 item_p = items[i].transform.position;
            if(item_p.y + btnHeightActual/2 > limitY)
            {
                item_p.y -= limitY;
            }
            else if(item_p.y + btnHeightActual/2 < 0)
            {
                item_p.y += limitY;
            }
            items[i].transform.position = item_p;
        }
    }
    
    float buttonOffset(float y, float a = 1)
    {
        return y + btnHeightActual * a;
    }

    public void LoadItems(GameObject[] items)
    {
        int s = items.Length;
        int ts = s > 4 ? s - 1 : s;

        float btnFullHeight = screenHeight / ts;
        btnHeightActual = Mathf.Max(btnHeight+btnOffset, btnFullHeight);
        shelfHeight = s * btnHeightActual;
        //only scroll if the list is long
        if (shelfHeight < screenHeight)
        {
            scroll.movementType = ScrollRect.MovementType.Clamped;
        }

        this.items = new List<GameObject>();
        for (int i = 0; i < items.Length; i++)
        {
            items[i].transform.SetParent(scroll.content);
            RectTransform r = items[i].GetComponent<RectTransform>();
            r.sizeDelta = new Vector2(scroll.GetComponent<RectTransform>().sizeDelta.x, btnHeight);
            Vector3 p = items[i].transform.position;
            p.y = i * (btnHeightActual) + btnHeightActual/2;
            items[i].transform.position = p;
            this.items.Add(items[i]);
        }
        limitY = Mathf.Max(screenHeight, shelfHeight);
    }

    public void toggleVisible(bool isVisible)
    {
        this.isVisible = isVisible;
    }

}
