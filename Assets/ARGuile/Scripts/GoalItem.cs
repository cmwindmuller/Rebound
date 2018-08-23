using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalItem : BuildItem {

    public GameObject winFx;
    List<Ball> balls;

    private void Start()
    {
        balls = new List<Ball>();
    }

    /*private void OnTriggerEnter(Collider other)
    {
        Ball b = other.GetComponent<Ball>();
        if(b != null)
        {
            balls.Add(b);
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Ball b = other.GetComponent<Ball>();
        if (b != null && balls.Contains(b))
        {
            balls.Remove(b);
            GameObject p = Instantiate(winFx);
            p.transform.position = b.transform.position;
            Destroy(b.gameObject);
        }
    }*/

    public override bool TriggerEnter(Ball ball)
    {
        GameObject p = Instantiate(winFx);
        ball.transform.position = transform.position;
        p.transform.position = ball.transform.position;
        ball.Death();
        return base.TriggerEnter(ball);
    }

    private float goalDirection(Ball b)
    {
        float d = Vector3.Dot(transform.forward, b.rbody.velocity.normalized);
        return d;
    }

}
