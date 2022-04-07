using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    private RectTransform rectTrans;
    public Canvas myCanvas;
    public CanvasGroup canvasGroup;
    public SlotScript inSlot;
    public TextMeshProUGUI UIText;
    public int id;
    public bool interactable;
    private Vector2 initPos;
    private bool reset;


    // Start is called before the first frame update
    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        //UIText = GetComponentInChildren<TextMeshProUGUI>();
        inSlot = null;
        initPos = this.transform.position;
        interactable = true;
        reset = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");
        canvasGroup.blocksRaycasts = false;
        if (inSlot != null)
            inSlot.correct = false;
        inSlot = null;
        DSOCcontroller.dsocRoot.caught = false;        
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        if(interactable)
            rectTrans.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
        DSOCcontroller.dsocRoot.caught = false;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        canvasGroup.blocksRaycasts = true;
        if (DSOCcontroller.dsocRoot.caught)
            DSOCcontroller.dsocRoot.caught = false;
        else
            ResetPosition();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Click");
        if (interactable)
            reset = false;
    }
    
    public void ResetPosition()
    {
        reset = true;
        //transform.position = initPos;
    }

    public void LateUpdate()
    {
        if (!reset || !interactable)
            return;

        this.transform.position = Vector3.Lerp(this.transform.position, initPos, 0.05f);
        if ((this.transform.position - new Vector3(initPos.x, initPos.y, 0)).magnitude < 0.1f)
        {
            this.transform.position = new Vector3(initPos.x, initPos.y, 0);
            reset = false;
        }
    }

    public void SetCardData(int num, string text)
    {
        Debug.Log("Incoming: " + this.gameObject.name + "\nIDnum: " + num.ToString());
        this.id = num;
        Debug.Log("Saved: " + this.gameObject.name + "\nIDnum: " + this.id.ToString());

        if (UIText == null)
            UIText = GetComponentInChildren<TextMeshProUGUI>();

        Debug.Log("Incoming: " + this.gameObject.name + "\nAnswerText: " + text);
        UIText.text = text;
        Debug.Log("Incoming: " + this.gameObject.name + "\nAnswerText: " + UIText.text);

    }
}
