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
    private float alt = 0.0f;
    private float powerDraw = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        fuelSlider.maxValue = Constants.Ship.Resources.MaxFuel;
        fuelSlider.value = fuelSlider.maxValue;
        powerSlider.maxValue = Constants.Ship.Resources.MaxPower;
        powerSlider.value = powerSlider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        //add a method  for recharging the power when in the sunlight;
        //
        alt = GameRoot.player.transform.position.magnitude - (GameRoot.mainAsteroid.GetComponent<CircleCollider2D>().radius * GameRoot.mainAsteroid.transform.localScale.x);
        altitude.text = alt.ToString("0.00") + " :Altitude";

        power.text = "Power Use: " + powerDraw.ToString("0.00");

        if (powerSlider.value >= powerSlider.maxValue)
        {
            powerSlider.value = powerSlider.maxValue;
            return;
        }
        else
        {
            RechargePower(Constants.Ship.Resources.PowerRechargePS * Time.deltaTime);
        }
    }

    public void UseFuel(float val)
    {
        fuelSlider.value -= val;
    }

    public void UsePower(float val)
    {
        powerSlider.value -= val;
    }

    public void RechargePower(float val)
    {
        powerSlider.value += val;
    }

    public bool CanUseFuel()
    {
        if (fuelSlider.value > 0)
            return true;
        else
            return false;
    }

    public bool CanUsePower()
    {
        if (powerSlider.value > Constants.Ship.Resources.PowerRechargePS * 1.25)
            return true;
        else
            return false;
    }

}


