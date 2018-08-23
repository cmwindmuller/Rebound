using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class BuildItem : MonoBehaviour {

    public bool blocking;
    public string sound;
    protected bool completed;
    protected bool selected;
    public float lightTime = 0.5f;
    Outline outline;
    Color myColor;

    protected virtual void Awake()
    {
        myColor = findColor();
        outline = GetComponentInChildren<Outline>();
        outline.color = 1;
        DeSelect();
    }

    public virtual Color IconColor()
    {
        return gameObject.GetComponentInChildren<Renderer>().sharedMaterial.color;
    }

    public virtual void BeginCreate()
    {
        completed = false;
    }

    public virtual Vector3 SetPosition()
    {
        return transform.position = ARCamera.position + ARCamera.forward * 0.7f - ARCamera.up * 0.06f;
    }

    public virtual void SetRotation()
    {
        transform.rotation = Quaternion.LookRotation(ARCamera.forward, ARCamera.up);
    }

    public virtual bool ConfirmCreate()
    {
        return completed = true;
    }

    public virtual void DeSelect()
    {
        selected = false;
        outline.enabled = false;
    }

    public virtual void Select()
    {
        selected = true;
        outline.enabled = true;
    }

    public void Pinch(Vector3 p)
    {
        Vector3 s = transform.localScale;
        s = s + transform.InverseTransformVector(p);
        transform.localScale = s;
    }

    public void Delete()
    {
        Destroy(this.gameObject);
    }

    public virtual bool TriggerEnter(Ball ball)
    {
        Library.playSound(transform.position, sound);
        StartCoroutine(reactFX());
        //lightUp();
        //Invoke("lightDown", lightTime);
        return blocking;
    }

    protected virtual Color findColor()
    {
        MeshRenderer r = GetComponentInChildren<MeshRenderer>();
        return r.sharedMaterial.color;
    }

    protected virtual IEnumerator reactFX()
    {
        MeshRenderer r = GetComponentInChildren<MeshRenderer>();
        float waitedTime = 0;
        while(waitedTime < lightTime)
        {
            waitedTime += Time.deltaTime;
            //r.material.color = Color.Lerp(Color.Lerp(Color.white,myColor,0.75f), myColor, Mathf.Lerp(0.4f, 1, waitedTime/lightTime));
            //r.material.SetColor("_EmissionColor", Color.Lerp(r.material.color, Color.black, Mathf.Lerp(0.4f, 1, waitedTime / lightTime)));
            r.material.SetColor("_EmissionColor", Color.Lerp(Color.Lerp(Color.white, myColor, 0.75f), Color.black, Mathf.Lerp(0.4f, 1, waitedTime / lightTime)));
            yield return null;
        }
        r.material.color = myColor;
        r.material.SetColor("_EmissionColor", Color.black);
    }

}
