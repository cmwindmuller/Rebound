using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public struct BuildInfo
{
    public string name;
    public BuildItem item;
}
[System.Serializable]
public enum BuildState { Idle, Placement, Edit };

public class ManageBuild : IManage {

    [Header("UI")]
    public ScrollLoop scrollLoop;
    public GameObject btnPrefab;

    [Header("Items")]
    public BuildState buildState;
    public BuildInfo[] buildItems;
    BuildItem currentItem;

    private void Start()
    {
        GameObject[] items = new GameObject[buildItems.Length];
        for(int i=0;i<buildItems.Length;i++)
        {
            items[i] = Instantiate(btnPrefab);
            //button delegate [onClick = btnUp, !btnDown, !mouseOut, !mouseMoved]
            int index = i;
            items[i].GetComponent<Button>().onClick.AddListener(delegate { BuildBegin(index); });
            //end button
            items[i].GetComponent<Image>().color = buildItems[i].item.IconColor();
            items[i].GetComponentInChildren<Text>().text = (i+1).ToString() + ": " + buildItems[i].name;
        }
        scrollLoop.LoadItems(items);
    }

    float touchRayDistance = 1.6f;
    private void Update()
    {
        if (buildState == BuildState.Idle)
        {
            Touch[] touch = Input.touches;
            if (touch.Length == 1 && touch[0].phase == TouchPhase.Began
                && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                //single tap
                Ray ray = ARCamera.main.ScreenPointToRay(touch[0].position);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, touchRayDistance))
                {
                    //can it be selected?
                    BuildItem item = hitInfo.collider.GetComponent<BuildItem>();
                    if (item != null && item != currentItem)
                    {
                        currentItem = item;
                        currentItem.Select();
                        currentItem.transform.SetParent(ARCamera.main.transform);
                        Game.main.manageUI.showAllButtons();
                        buildState = BuildState.Edit;
                    }
                }
                else
                {
                    //why did they tap? (non-UI)
                }
            }
        }
        if (currentItem != null)
        {
            //currentItem.SetPosition();
            //currentItem.SetRotation();

            if (Game.main.manageUI.isDown)
            {
                if (!Game.main.manageUI.isConfirm)
                {
                    currentItem.Delete();
                }
                else
                {
                    if(buildState == BuildState.Placement)
                    {
                        if (!currentItem.ConfirmCreate())
                        {
                            return;
                        }
                    }
                    currentItem.DeSelect();
                    currentItem.transform.parent = null;
                }
                buildState = BuildState.Idle;
                Game.main.manageUI.hideAllButtons();
                currentItem = null;
            }
        }
    }

    public void BuildBegin(int index)
    {
        //ignore the operation?
        if (buildState != BuildState.Idle)
            return;
        buildState = BuildState.Placement;

        currentItem = Instantiate(buildItems[index].item);
        currentItem.Select();
        currentItem.SetPosition();
        currentItem.SetRotation();
        currentItem.transform.SetParent(ARCamera.main.transform);
        currentItem.BeginCreate();
        Game.main.manageUI.showAllButtons();
    }
    

    public BuildItem Build(int i, Transform t)
    {
        BuildItem b = Instantiate( buildItems[i].item );
        b.transform.position = t.position + t.forward/3;
        b.transform.rotation = t.rotation;

        return b;
    }
}
