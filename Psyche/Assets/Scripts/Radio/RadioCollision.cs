using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class RadioCollision : MonoBehaviour
{
    public static RadioCollision instance;

    [ReadOnly]
    public int currentRing = 0;

    [Header("------Ring Controls--------")]
    [Range(1,10)]
    [Tooltip("Total number of rings")]
    [SerializeField] private int numOfRings;
    [Tooltip("Max Value is the radius of the first ring, Min Value is the Radius of the Final Ring")]
    [MinMaxRange(0, 400)]
    public MinMaxInt minMaxRingSize = new MinMaxInt(40, 100);
    [Tooltip("Smoothness of the rings.\nHigher numbers means more line verticies and better smoothness")]
    [SerializeField] private float ringSmoothScalar = 7.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField] private float ringOpacity = 50.0f;
    private float translucentOffset = 0.25f;

    [SerializeField] private Color outerColor;
    [SerializeField] private Color middleColor;
    [SerializeField] private Color innerColor;

    [Header("------Effect Controls--------")]
    [Range(1.0f, 10.0f)]
    [SerializeField] private float maxEffectSpeed;

    [Range(0.0f,1.0f)]
    [SerializeField] private float soundVolume = 0.5f;
    [Range(0.0f, 3.0f)]
    [SerializeField] private float pitchVariance = 0.0f;
    private float initPitch;
    private float currPitch;
    [Range(0.0f, 3.0f)]
    [SerializeField] private float rateOfPitchChange = 1.0f;
    private float currTime;



    [Header("------Score Controls--------")]
    [Tooltip("The total number of points gained per second at the final ring\nFurther out rings will gain a percentage of this based on the ring level over the total number of rings")]
    [SerializeField] private float scoreRatePS;


    [Header("------Components--------")]
    public GameObject effect;
    public GameObject ringPrefab;
    private AudioSource audioEmitter;

    private List<GameObject> ringList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        audioEmitter = this.GetComponent<AudioSource>();
        ringOpacity /= 100.0f;
        effect.SetActive(false);
        audioEmitter.volume = Constants.Radio.Sounds.ringVolume * GameRoot.masterVolume;
        currPitch = audioEmitter.pitch;
        initPitch = audioEmitter.pitch;
        currTime = 0.0f;
        CreateRings();
        AdjustAudioSettings();
        ringPrefab.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentRing > 0)
        {
            effect.transform.position = GameRoot.player.transform.position;
            GameRoot._Root.ScoreRadio(scoreRatePS * ((float)currentRing/ (float)ringList.Count) * Time.deltaTime);
            
            if(audioEmitter.isPlaying)
            {
                currPitch = (((float)currentRing / (float)ringList.Count) * pitchVariance) + initPitch;

                audioEmitter.pitch = currPitch;
            }
        }   
    }

    private void AdjustAudioSettings()
    {
        //Set min/max range
        //this will affect the volume gradient as the player gets closer
        audioEmitter.minDistance = ringList[ringList.Count - 1].GetComponent<CircleCollider2D>().radius / 4.0f;
        audioEmitter.maxDistance = ringList[0].GetComponent<CircleCollider2D>().radius;
        audioEmitter.volume = soundVolume;
    }

    /// <summary>
    /// A trigger accessor to set the current ring and to turn on/off effects
    /// </summary>
    /// <param name="ringID"></param>
    public void CurrentRing(int ringID)
    {
        currentRing = ringID;
        if(currentRing > 0)
        {
            effect.transform.position = GameRoot.player.transform.position;
            var ps = effect.GetComponent<ParticleSystem>();
            ScaleEffect(ps, currentRing);
            effect.SetActive(true);
            if(!audioEmitter.isPlaying)
                audioEmitter.Play();
        }
        else
        {
            effect.SetActive(false);
            audioEmitter.Stop();
        }
    }

    /// <summary>
    /// Scales the rate of particle generation and changes of color of particles
    /// based on the level of ring the player is currently in
    /// </summary>
    /// <param name="ps">the main particle system that is part of this tool</param>
    /// <param name="ringID">the current ring the player is in</param>
    private void ScaleEffect(ParticleSystem ps, int ringID)
    {
        var main = ps.main;
        main.simulationSpeed = Mathf.Pow(maxEffectSpeed + 1, (float)currentRing / (float)ringList.Count) - 1;
        var emission = ps.emission;
        emission.rate = ((float)currentRing / (float)ringList.Count) * maxEffectSpeed;
        
        Color psColor = ringList[ringID - 1].GetComponent<LineRenderer>().endColor;
        main.startColor = new Color(psColor.r, psColor.g, psColor.b, 1.0f);
        var trail = ps.transform.Find("Trail").GetComponent<ParticleSystem>();
        var trailMain = trail.main;
        trailMain.startColor = new Color(psColor.r, psColor.g, psColor.b, 1.0f) * 4.0f;
        Gradient trailGrad = trail.colorOverLifetime.color.gradient;
        trailGrad.colorKeys[1].color = new Color(psColor.r, psColor.g, psColor.b, 1.0f);
        
        //if we're not at the final ring, the final color over lifetime is the next ring color
        if(ringID != ringList.Count)        {
            psColor = ringList[ringID].GetComponent<LineRenderer>().endColor;
            trailGrad.colorKeys[2].color = new Color(psColor.r, psColor.g, psColor.b, 1.0f);
        }
        //otherwise (since we can't step outside the bounds of ringList[], take the current color
        // and darken it.
        else
            trailGrad.colorKeys[2].color = new Color(psColor.r, psColor.g, psColor.b, 1.0f) / 4.0f;



        var glow = ps.transform.Find("GlowTrail").GetComponent<ParticleSystem>();
        var glowMain = glow.main;
        glowMain.startColor = new Color(psColor.r, psColor.g, psColor.b, 0.01f) * 10.0f;

    }

    /// <summary>
    /// Creates the ring objects based on the ring element attached to this tool
    /// and the settings modified in the inspector.
    /// This is the primary method responsible for setting the collider box radius
    /// </summary>
    private void CreateRings()
    {
        GameObject temp;
        float ringWidth;
        if (numOfRings > 1)
            ringWidth = (minMaxRingSize.Max - minMaxRingSize.Min) / (float)(numOfRings - 1);
        else
            ringWidth = (minMaxRingSize.Max - minMaxRingSize.Min);
        float currSize = minMaxRingSize.Max + ringWidth;

        for (int i = 1; i <= numOfRings; i++)
        {
            currSize -= ringWidth;
            temp = Instantiate(ringPrefab);
            temp.GetComponent<RingTrigger>().ringID = i;
            temp.transform.parent = this.gameObject.transform;
            temp.GetComponent<CircleCollider2D>().radius = currSize;

            //For all but the last ring, the width of each line needs to be from the ring above
            //to the ring below. For the final ring it needs to go from the ring above to the center
            //of the asteroid (so that theres no gap between the two).
            if(i < numOfRings)
                GenerateRingLine(temp.GetComponent<LineRenderer>(), currSize - (ringWidth / 2), ringWidth);
            else
                GenerateRingLine(temp.GetComponent<LineRenderer>(), (currSize / 2), currSize);

            ringList.Add(temp);
        }
        //Set the Opacity information for all the LineRenderer Materials
        ringList[0].GetComponent<Renderer>().sharedMaterial.SetColor("_TintColor", new Color(ringOpacity * translucentOffset, ringOpacity * translucentOffset, ringOpacity * translucentOffset, ringOpacity));

        ColorRings();
    }

    /// <summary>
    /// Generates the LineRenderer elements for the ring
    /// </summary>
    /// <param name="line">the LineRenderer attached to this ring</param>
    /// <param name="radius">Radius of the LineRenderer ring</param>
    /// <param name="width">Width of the LineRenderer ring</param>
    private void GenerateRingLine(LineRenderer line, float radius, float width)
    {
        //reset line parameters
        line.positionCount = 0;
        //function vars
        float angleStep = Mathf.PI / (radius * ringSmoothScalar);
        Vector3 vector = new Vector3(radius, 0, 0);

        for(float theta = 0.0f; theta < 360; theta += angleStep)
        {
            line.positionCount++;
            float x = vector.x * Mathf.Cos(theta) - vector.y * Mathf.Sin(theta);
            float y = vector.x * Mathf.Sin(theta) - vector.y * Mathf.Cos(theta);
            line.SetPosition(line.positionCount - 1, new Vector3(x, y, 50.0f));

        }
        line.SetWidth(width, width);
    }

    /// <summary>
    /// Colors the rings based on the Gradient information entered in the inspector
    /// if there is only ONE ring, only the OuterColor is used
    /// if there are only TWO rings, only the OuterColor and InnerColor are used.
    /// For THREE or more rings, all three colors are used.  Colors inbetween are colored
    /// based on the ring colors above and below.
    /// </summary>
    private void ColorRings()
    {
        if (numOfRings == 1)
        {
            ringList[0].GetComponent<LineRenderer>().startColor = new Color(outerColor.r, outerColor.g, outerColor.b, 0);
            ringList[0].GetComponent<LineRenderer>().endColor = new Color(outerColor.r, outerColor.g, outerColor.b, ringOpacity * translucentOffset);
 //           ringList[0].GetComponent<Renderer>().sharedMaterial.SetColor("_TintColor", new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f, ringTranslucency * translucentOffset));
        }
        else if (numOfRings == 2)
        {
            ringList[0].GetComponent<LineRenderer>().startColor = new Color(outerColor.r, outerColor.g, outerColor.b, 0);
            ringList[0].GetComponent<LineRenderer>().endColor = new Color(outerColor.r, outerColor.g, outerColor.b, ringOpacity * translucentOffset);
//            ringList[0].GetComponent<Renderer>().sharedMaterial.SetColor("_TintColor", new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f, ringTranslucency * translucentOffset));

            ringList[1].GetComponent<LineRenderer>().startColor = new Color(innerColor.r, innerColor.g, innerColor.b, 0);
            ringList[1].GetComponent<LineRenderer>().endColor = new Color(innerColor.r, innerColor.g, innerColor.b, ringOpacity * translucentOffset);
//            ringList[1].GetComponent<Renderer>().sharedMaterial.SetColor("_TintColor", new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f, ringTranslucency * translucentOffset));

        }
        else
        {
            ringList[0].GetComponent<LineRenderer>().startColor = new Color(outerColor.r, outerColor.g, outerColor.b, 0);
            ringList[0].GetComponent<LineRenderer>().endColor = new Color(outerColor.r, outerColor.g, outerColor.b, ringOpacity * translucentOffset);
 //           ringList[0].GetComponent<Renderer>().sharedMaterial.SetColor("_TintColor", new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f, ringTranslucency * translucentOffset));

            ringList[numOfRings - 1].GetComponent<LineRenderer>().startColor = new Color(innerColor.r, innerColor.g, innerColor.b, 0);
            ringList[numOfRings - 1].GetComponent<LineRenderer>().endColor = new Color(innerColor.r, innerColor.g, innerColor.b, ringOpacity * translucentOffset);
//            ringList[numOfRings - 1].GetComponent<Renderer>().sharedMaterial.SetColor("_TintColor", new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f, ringTranslucency * translucentOffset));

            int middle = Mathf.CeilToInt((numOfRings - 1) / 2.0f);
            ringList[middle].GetComponent<LineRenderer>().startColor = new Color(middleColor.r, middleColor.g, middleColor.b, 0);
            ringList[middle].GetComponent<LineRenderer>().endColor = new Color(middleColor.r, middleColor.g, middleColor.b, ringOpacity * translucentOffset);
//            ringList[middle].GetComponent<Renderer>().sharedMaterial.SetColor("_TintColor", new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f, ringTranslucency * translucentOffset));

            RecurveColorRings(0, middle);
            RecurveColorRings(middle, numOfRings - 1);
        }
    }

    /// <summary>
    /// A recursive method to divide up the ring list and color each ring in the middle of two
    /// of each half by mixing the color of the first ring and last ring of this recursive step
    /// </summary>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    private void RecurveColorRings(int begin, int end)
    {
        int mid = Mathf.FloorToInt(begin + (end - begin) / 2.0f);
        if (begin == mid)
            return;

        Color mixedColor = (ringList[begin].GetComponent<LineRenderer>().startColor + ringList[end].GetComponent<LineRenderer>().startColor ) / 2.0f;
        ringList[mid].GetComponent<LineRenderer>().startColor = new Color(mixedColor.r, mixedColor.g, mixedColor.b, 0);
        ringList[mid].GetComponent<LineRenderer>().endColor = new Color(mixedColor.r, mixedColor.g, mixedColor.b, ringOpacity * translucentOffset);
//        ringList[mid].GetComponent<Renderer>().sharedMaterial.SetColor("_TintColor", new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f, ringTranslucency * translucentOffset));
        RecurveColorRings(begin, mid);
        RecurveColorRings(mid, end);

    }
}
