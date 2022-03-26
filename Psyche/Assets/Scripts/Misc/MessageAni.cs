using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageAni : MonoBehaviour
{
    bool moveUP = false;
    private float moveDist = 0.0f;
    private float timer = 0.0f;
    private float fadeTime = 1.0f;
    private float maxTime = 8.0f;
    private Color startColor;
    private Vector3 targetPos = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        startColor = this.GetComponent<TextMeshProUGUI>().color;
        this.GetComponent<TextMeshProUGUI>().color *= 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer < maxTime / 2)
            FadeIn(timer);
        else
            FadeOut(timer);


        if(moveUP)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, Time.deltaTime * 100);
            if (this.transform.position == this.transform.position + Vector3.up * moveDist)
                moveUP = false;
        }
    }

    private void FadeIn(float time)
    {
        float percent = Mathf.Clamp(time / fadeTime, 0.0f, 1.0f);

        this.GetComponent<TextMeshProUGUI>().color = startColor * percent;
    }
    private void FadeOut(float time)
    {
        if(time > maxTime)
        {
            PopMessageUI.RemovePop(this.gameObject);
        }
        float percent = Mathf.Clamp((time - (maxTime - fadeTime)) / fadeTime, 0.0f, 1.0f);

        this.GetComponent<TextMeshProUGUI>().color = startColor * (1 - percent);
    }

    public void ShiftY(float dist)
    {
        if (moveUP)
            moveDist += dist;
        else
            moveDist = dist;

        moveUP = true;
        targetPos = this.transform.position + Vector3.up * moveDist;
    }

    public void SetLifeTime(float val)
    {
        maxTime = val;
    }

}
