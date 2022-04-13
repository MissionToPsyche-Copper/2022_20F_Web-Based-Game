using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShipResources : MonoBehaviour
{
    public Slider fuelSlider;
    public Slider powerSlider;
    public TextMeshProUGUI altitude;
    public TextMeshProUGUI power;
    public Image PowerOutBar;
    public Image RebootBar;
    private float alt = 0.0f;
    private float powerDraw = 0.0f;
    private float rebootDelay = 0.0f;

	private bool powerOutage = false;


    // Start is called before the first frame update
    void Start()
    {
        fuelSlider.maxValue = Constants.Ship.Resources.MaxFuel;
        fuelSlider.value = fuelSlider.maxValue;
        powerSlider.maxValue = Constants.Ship.Resources.MaxPower;
        powerSlider.value = powerSlider.maxValue;

        PowerOutBar.transform.position += new Vector3(0, powerSlider.fillRect.rect.height, 0) * Constants.Ship.Resources.PowerOutagePercent * 4.3f;
        RebootBar.transform.position += new Vector3(0, powerSlider.fillRect.rect.height, 0) * Constants.Ship.Resources.RebootPercent;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //add a method  for recharging the power when in the sunlight;
        //
        alt = LevelController.player.transform.position.magnitude - LevelController.mainAsteroid.GetComponent<PolygonCollider2D>().bounds.extents.x;
        alt *= Constants.Space.AltitudeFactorAdjust;
        altitude.text = "Altitude:\n" + alt.ToString("0.0") + "km";
        powerDraw = CalcActivePowerUse();
        power.text = "Power Use\n" + powerDraw.ToString("0.0") + "w";

        RechargePower(Constants.Ship.Resources.PowerRechargePS * Time.deltaTime);

    }

    public void UseFuel(float val)
    {
        fuelSlider.value -= val;
        if (fuelSlider.value <= 0.0f)
            LevelController.levelRoot.BadEnd(false, "Out of Fuel");
    }

    public void UsePower(float val)
    {
        powerSlider.value -= val;

        if (powerSlider.value < powerSlider.maxValue * Constants.Ship.Resources.PowerOutagePercent)
        {
            PopMessageUI.PopUpMessage("! Power Outage : System Rebooting !");
            powerOutage = true;
        }
    }

    public void RechargePower(float val)
    {
        if (powerSlider.value > powerSlider.maxValue)
        {
            powerSlider.value = powerSlider.maxValue;
            return;
        }

        if(powerOutage)
        {
            rebootDelay += Time.deltaTime;
            if(rebootDelay > Constants.Ship.Resources.RebootDelayTime)
                powerSlider.value += val;

            if(powerSlider.value > powerSlider.maxValue * Constants.Ship.Resources.RebootPercent)
            {
                rebootDelay = 0.0f;
                powerOutage = false;
            }
        }
        else
            powerSlider.value += val;
    }

    public bool CanUseFuel()
    {
        return fuelSlider.value > 0;
    }

    public bool CanUsePower()
    {
        return !powerOutage;
    }

    private float CalcActivePowerUse()
    {
        float totalUse = 0.0f;
        if (powerOutage)
            return totalUse;
        if (GammaRayController.toolActive)
            totalUse += Constants.Ship.Resources.PowerUse.GammaRay;
        if (MagnetometerController.toolActive)
            totalUse += Constants.Ship.Resources.PowerUse.Magnetometer;
        if (MultiSpectController.toolActive)
            totalUse += Constants.Ship.Resources.PowerUse.Multispectral;
        if (ShipControl.gyroActive)
            totalUse += Constants.Ship.Resources.PowerUse.GyroRotate;


        return totalUse;
    }

}


