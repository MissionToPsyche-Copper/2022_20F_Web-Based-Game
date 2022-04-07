using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IDropHandler
{
    public int id;
    public bool correct = false;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item Dropped");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = this.GetComponent<RectTransform>().position;
            eventData.pointerDrag.GetComponent<DragAndDrop>().inSlot = this;
            DSOCcontroller.dsocRoot.caught = true;


            if (eventData.pointerDrag.GetComponent<DragAndDrop>().id == id)
            {
                Debug.Log("Answer in Correct Slot");
                correct = true;
                DSOCcontroller.dsocRoot.CheckAnswers();
            }
            else
            {
                Debug.Log("Answer in Wrong Slot");
                correct = false;
            }
        }
    }

    public void SetID(int num)
    {
        Debug.Log("Incoming: " + this.gameObject.name  + "\nIDnum: " + num.ToString());
        this.id = num;
        Debug.Log("Saved: " + this.gameObject.name + "\nIDnum: " + this.id.ToString());

    }
}
