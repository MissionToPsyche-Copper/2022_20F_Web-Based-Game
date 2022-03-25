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
                correct = true;
                DSOCcontroller.dsocRoot.CheckAnswers();
            }
            else
            {
                correct = false;
            }
        }
    }
}
