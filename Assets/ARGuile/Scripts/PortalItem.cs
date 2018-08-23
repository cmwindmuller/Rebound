using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalItem : BuildItem {
    
    PortalItem otherPortal;
    Transform mesh;
    
	void Start () {
        blocking = false;
        mesh = transform.GetChild(0);
        if(otherPortal == null)
        {
            PortalItem[] portals = GameObject.FindObjectsOfType<PortalItem>();
            foreach(PortalItem p in portals)
            {
                if (p == this)
                    continue;
                if(p.otherPortal == null)
                {
                    p.otherPortal = this;
                    this.otherPortal = p;
                }
            }
        }
    }

    private void LateUpdate()
    {
        mesh.Rotate(Vector3.forward, 10 * Time.deltaTime, Space.Self);
    }

    public override Color IconColor()
    {
        return new Color(0, 0.66f, 0.82f);
    }

    public override bool TriggerEnter(Ball ball)
    {
        if (otherPortal == null)
            return false;

        Ball ball2 = ball.Copy();
        ball.transform.position = transform.position;
        ball.GetComponent<TrailRenderer>().time *= 0.5f;
        ball.Death();
        Vector3 portalLocalVelocity = transform.InverseTransformDirection(ball.rbody.velocity);
        ball2.transform.position = otherPortal.transform.position;
        ball2.rbody.velocity = otherPortal.transform.TransformDirection(portalLocalVelocity);

        return base.TriggerEnter(ball);
    }

    protected override Color findColor()
    {
        return Color.cyan;
    }

    protected override IEnumerator reactFX()
    {
        Behaviour halo = (Behaviour)GetComponent("Halo");
        float waitedTime = 0;
        while (waitedTime < lightTime)
        {
            waitedTime += Time.deltaTime;
            //halo
            yield return null;
        }
    }
}
