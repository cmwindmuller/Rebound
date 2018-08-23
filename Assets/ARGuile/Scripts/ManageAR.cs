using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class ManageAR : IManage {

    public bool tracking;

    public MeshRenderer pointRenderer;

    private void Update()
    {
        tracking = Frame.TrackingState == FrameTrackingState.Tracking;
        pointRenderer.enabled = !tracking;
    }

}
