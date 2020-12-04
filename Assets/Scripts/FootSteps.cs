using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;

    [SerializeField]
    private AudioClip[] flyClips;

    private AudioSource audioSource;

    public bool isRunning; //vamos a hacer esta chapuza ya que no funcionan los animation events
    float stepFrecuency = 0.5f;
    float stepTimer;

    float wingFrecuency = 0.7f;
    float wingTimer;

    public bool isFlying;

    void Awake()
    {
        wingTimer = wingFrecuency;
        stepTimer = stepFrecuency;
        isRunning = false;
        isFlying = false;
        audioSource = GetComponent<AudioSource>();
    }

    public void StepEvent()
    {
        //Debug.Log("Aqui suena un paso !");
        AudioClip clip = GetRandomClip(clips);
        audioSource.PlayOneShot(clip);
    }

    public void WingEvent()
    {
        AudioClip clip = GetRandomClip(flyClips);
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }



    void Update()
    {
        if (isRunning)
        {
            if (stepTimer <= 0f)
            {
                stepTimer = stepFrecuency;
                StepEvent();
            }
            else
            {
                stepTimer -= Time.deltaTime;
            }
        }
        else if (isFlying)
        {
            if (wingTimer <= 0f)
            {
                wingTimer = wingFrecuency;
                WingEvent();
            }
            else
            {
                wingTimer -= Time.deltaTime;
            }
        }
    }
}
