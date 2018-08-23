using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageUI : IManage {

    public static Canvas canvas;
    public Canvas _canvas;
    public bool isConfirm, isDown;
    public Button confirmBtn, denyBtn;
    List<Button> allBtn;

	// Use this for initialization
	void Awake ()
    {
        Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = false;


        allBtn = new List<Button>();

        confirmBtn.onClick.AddListener(delegate { OnButtonClick(true); });
        denyBtn.onClick.AddListener(delegate { OnButtonClick(false); });

        allBtn.Add(confirmBtn);
        allBtn.Add(denyBtn);

        hideAllButtons();
	}

    private void Start()
    {
        canvas = _canvas;
    }

    void OnButtonClick(bool isConfirmBtn)
    {
        isDown = true;
        isConfirm = isConfirmBtn;
    }

    private void LateUpdate()
    {
        isDown = false;
    }

    public void hideAllButtons()
    {
        foreach(Button btn in allBtn)
        {
            btn.GetComponent<Image>().enabled = false;
        }
    }

    public void showAllButtons()
    {
        foreach (Button btn in allBtn)
        {
            btn.GetComponent<Image>().enabled = true;
        }
    }
}
