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
        if (mainController != null)
        {
            mainController.ToggleLine(false);
            try
            {
                mainController.GetAudio().Stop();
            }
            catch
            { }
            mainController.RemoveFromTargetList(this.gameObject);
            MultiSpectController.toolActive = false;
        }

    }

    public void OnMouseDown()
    {
        clicked = true;
        MultiSpectController.toolActive = true;
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
        MultiSpectController.toolActive = false;

        mainController.ToggleLine(false);
        mainController.GetAudio().Stop();
    }

    public void OnMouseOver()
    {

        if(clicked)
        {
            if (!ShipControl.resources.CanUsePower())
            {
                clicked = false;
                mainController.ToggleLine(false);
                mainController.GetAudio().Stop();
                MultiSpectController.toolActive = false;

                return;
            }

            currTargetAngle = CheckShipAngle();

            if (ezMode)
                FaceShipToTarget();

            if (currTargetAngle < maxTargetAngle)
            {
                MultiSpectController.toolActive = true;
                ShipControl.resources.UsePower(Constants.Ship.Resources.PowerUse.Multispectral * Time.deltaTime);
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
                MultiSpectController.toolActive = false;

                mainController.ToggleLine(false);
            }
        }
    }
    public void OnMouseExit()
    {
        clicked = false;
        mainController.ToggleLine(false);
        mainController.GetAudio().Stop();
        MultiSpectController.toolActive = false;

    }

    private float CheckShipAngle()
    {
        Vector3 ant2target = (mainController.GetAntennaPos() - this.transform.position).normalized;
        Vector3 ship2ant = (SceneController.player.transform.position - mainController.GetAntennaPos()).normalized;

        return Mathf.Abs(Vector3.Angle(ant2target, ship2ant));
    }

    private void FaceShipToTarget()
    {
        Vector3 targetDirection = (SceneController.player.transform.position - this.transform.position).normalized;
        Vector3 shipDirection = (SceneController.player.transform.position - mainController.GetAntennaPos()).normalized;
        float angle2target = Vector3.SignedAngle(targetDirection, shipDirection, Vector3.forward);

        if (!ShipControl.resources.CanUsePower())
            return;

        if (angle2target == 0)
        {
            ShipControl.gyroActive = false;
            return;
        }
        else if (angle2target < 0)
        {
            SceneController.player.transform.Rotate(Vector3.forward, Constants.Ship.RotateSpeed);
            ShipControl.resources.UsePower(Constants.Ship.Resources.PowerUse.GyroRotate * Time.deltaTime);
            ShipControl.gyroActive = true;

        }
        else
        {
            SceneController.player.transform.Rotate(Vector3.forward, -Constants.Ship.RotateSpeed);
            ShipControl.resources.UsePower(Constants.Ship.Resources.PowerUse.GyroRotate * Time.deltaTime);
            ShipControl.gyroActive = true;
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
