using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Target : MonoBehaviour
{
    private MultiSpectController mainController;

    private float maxTargetAngle = 60.0f;
    [ReadOnly][SerializeField]
    private float currTargetAngle = 0.0f;
    private float anglePercent;

    [ReadOnly][SerializeField]
    private float lifespan = 3.0f; //set by the Controller
    [ReadOnly][SerializeField]
    private float scoreSizeMod; //set by the Controller based on size of target and a const

    [ReadOnly][SerializeField]
    private bool clicked = false;
    private bool ezMode = false;

    public void OnDestroy()
    {
        mainController.RemoveFromTargetList(this.gameObject);
        mainController.ToggleLine(false);
        mainController.GetAudio().Stop();
    }

    public void OnMouseDown()
    {
        clicked = true;
        if (CheckShipAngle() < maxTargetAngle)
        {
            mainController.ToggleLine(true);
            mainController.GetAudio().Play();
        }
    }

    public void OnMouseUp()
    {
        if (clicked)
            clicked = false;
        mainController.ToggleLine(false);
        mainController.GetAudio().Stop();
    }

    public void OnMouseOver()
    {
        if(clicked)
        {
            currTargetAngle = CheckShipAngle();

            if (ezMode)
                FaceShipToTarget();

            if (currTargetAngle < maxTargetAngle)
            {

                anglePercent = currTargetAngle / maxTargetAngle;
                mainController.ToggleLine(true);
                mainController.GetAudio().pitch = 10.0f * (1 - anglePercent);
                mainController.SetLineWidth(anglePercent);
                mainController.SetLineColors(anglePercent);
                mainController.SetAudioPitch(anglePercent);
                mainController.SetLinePoints(this.transform.position);
                mainController.AddScore(anglePercent, scoreSizeMod, Time.deltaTime);
            }
            else
            {
                if(!ezMode)
                    clicked = false;

                mainController.ToggleLine(false);
            }
        }
    }
    public void OnMouseExit()
    {
        clicked = false;
        mainController.ToggleLine(false);
        mainController.GetAudio().Stop();
    }

    private float CheckShipAngle()
    {
        Vector3 ant2target = (mainController.GetAntennaPos() - this.transform.position).normalized;
        Vector3 ship2ant = (GameRoot.player.transform.position - mainController.GetAntennaPos()).normalized;

        return Mathf.Abs(Vector3.Angle(ant2target, ship2ant));
    }

    private void FaceShipToTarget()
    {
        Vector3 targetDirection = (GameRoot.player.transform.position - this.transform.position).normalized;
        Vector3 shipDirection = (GameRoot.player.transform.position - mainController.GetAntennaPos()).normalized;
        float angle2target = Vector3.SignedAngle(targetDirection, shipDirection, Vector3.forward);

        if (angle2target == 0)
            return;
        else if(angle2target < 0)
        {
            GameRoot.player.transform.Rotate(Vector3.forward, Constants.Ship.RotateSpeed);
        }
        else
        {
            GameRoot.player.transform.Rotate(Vector3.forward, -Constants.Ship.RotateSpeed);
        }
    }

    public void SetLifespan(float val)
    {
        lifespan = val;
        Destroy(gameObject, lifespan);
    }

    public void SetScoreSizeMod(float val)
    {
        scoreSizeMod = val;
    }

    public void SetEZmode(bool val)
    {
        ezMode = val;
    }

    public void SetController(MultiSpectController controller)
    {
        mainController = controller;
    }
}
