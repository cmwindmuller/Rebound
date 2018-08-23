                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    using C33;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxItem : BuildItem {

    int confirms;
    const int DIMENSIONS = 3;
    Vector3 initialScale;
    Vector3[] p;

    protected override void Awake()
    {
        base.Awake();
    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
    public override void BeginCreate()
    {
        base.BeginCreate();
        confirms = 0;
        initialScale = transform.localScale;
        p = new Vector3[DIMENSIONS];
    }

    public override Vector3 SetPosition()
    {
        if(confirms == 0 || completed)
        {
            base.SetPosition();
        }
        else if(confirms == 1)
        {
            Vector3 camP = ARCamera.offsetPosition;
            Vector3 nowP = Vector3.Lerp(p[0], camP, 0.5f);
            transform.position = nowP;
            transform.rotation = Quaternion.LookRotation(camP - p[0], ARCamera.up);
            Vector3 s = transform.localScale;
            s.z = Mathf.Max(initialScale.z,(camP - p[0]).magnitude);
            transform.localScale = s;
        }
        else if(confirms == 2)
        {
            /*Vector3 camP = ARCamera.offsetPosition;
            Vector3 up = Vector3.Cross(p[1] - p[0], camP - p[0]);
            transform.rotation = Quaternion.LookRotation(p[1] - p[0], up);
            transform.position = Vector3.Lerp(Vector3.Lerp(p[0], p[1], 0.5f), camP, 0.5f);
            Vector3 s = transform.localScale;
            s.x = Mathf.Max(initialScale.x, Maths.Div(up, (p[1] - p[0]).normalized).magnitude);
            transform.localScale = s;*/
        }
        return transform.position;
    }

    public override void SetRotation()
    {
        if (confirms == 0 || completed)
            base.SetRotation();
        else
        {

        }
    }

    public override bool ConfirmCreate()
    {
        if(confirms < DIMENSIONS)
        {
            p[confirms] = ARCamera.offsetPosition;
            confirms++;
        }
        return completed = confirms >= DIMENSIONS;
    }
}
