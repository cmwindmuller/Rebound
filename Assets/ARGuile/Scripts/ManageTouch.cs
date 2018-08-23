using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TouchMode { None, Tap, Swipe, Pinch };

public class ManageTouch : IManage
{
/*
    public static TouchMode touchMode;
    public Image iconPrefab;
    public Vector2 tap, swipe, pinch;

    public IDictionary<int, TouchTracker> touchObjects;

    private void Awake()
    {
        touchObjects = new Dictionary<int, TouchTracker>();
    }

    private void Update()
    {
        updateTouches();
        Debug.Log(touchObjects.Count);
    }

    void updateTouches()
    {
        if(Application.isMobilePlatform)
        {
            for(int i=0;i<Input.touchCount;i++)
            {
                Touch t = Input.touches[i];
                if (t.phase == TouchPhase.Began)
                {
                    touchObjects.Add(t.fingerId, TouchTracker.make(t,1,iconPrefab));
                }
                else
                {
                    TouchTracker tt = touchObjects[t.fingerId];
                    if(t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
                    {
                        Destroy(tt.icons[0].gameObject);
                        tt.delete();
                        touchObjects.Remove(t.fingerId);
                    }
                    else
                    {
                        tt.icons[0].transform.position = Input.GetTouch(t.fingerId).position;
                    }
                }
            }
        }
        else
        {
            for(int i=0;i<2;i++)
            {
                if(Input.GetMouseButtonDown(i))
                {
                    Touch t = new Touch();
                    t.position = Input.mousePosition;
                    t.phase = TouchPhase.Began;
                    touchObjects.Add(i, TouchTracker.make(t, 1, iconPrefab));
                }
                else if(Input.GetMouseButtonUp(i))
                {
                    TouchTracker tt = touchObjects[i];
                    tt.delete();
                    touchObjects.Remove(i);
                }
                else
                {
                    if(touchObjects.ContainsKey(i))
                    {
                        TouchTracker tt = touchObjects[i];
                        tt.moveTo(Input.mousePosition);
                    }
                }
            }
        }
    }
    */
}

    /*IEnumerator trackFinger(Touch touch)
    {
        Image[] icons = new Image[2];
        icons[0] = Instantiate(iconPrefab, canvas.transform).GetComponent<Image>();
        icons[1] = Instantiate(icons[0], icons[0].transform);
        TouchTracker t = new TouchTracker(touch, 1, icons);

        //Vector2 startPlace = touch.position;
        float startTime = Time.time;
        float holdTime = 1;
        GameObject icon = Instantiate(iconPrefab, canvas.transform);
        GameObject icon2 = Instantiate(iconPrefab, icon.transform);
        while (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
        {
            float a = Mathf.Clamp01(Time.time - startTime) / holdTime;
            icon.transform.position = touch.position;
            icon2.GetComponent<RectTransform>().localScale = new Vector3(a, a, 1);
            touch = Input.GetTouch(touch.fingerId);
            yield return null;
        }
        Destroy(icon.gameObject);
    }

    IEnumerator trackCursor(int index)
    {
        //Vector2 startPlace = touch.position;
        float startTime = Time.time;
        float holdTime = 1;
        GameObject icon = Instantiate(iconPrefab, canvas.transform);
        GameObject icon2 = Instantiate(iconPrefab, icon.transform);
        while (Input.GetMouseButton(index))
        {
            float a = Mathf.Clamp01(Time.time - startTime) / holdTime;
            icon.transform.position = Input.mousePosition;
            icon2.GetComponent<RectTransform>().localScale = new Vector3(a, a, 1);
            yield return null;
        }
        Destroy(icon.gameObject);
    }
}*/