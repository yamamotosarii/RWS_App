using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Measure : MonoBehaviour
{
    public float span = 1f;
    private float currentTime = 0f;
    private float total_time = 0f;
    float time;
    //Vector3 dire = Vector3;
    bool flag;
    public float acceleration = 0f;
    public float ac_x;
    public float ac_y;
    public float ac_z;
    public Text Ac = null;
    public AudioClip audioClip1;
    private AudioSource audioSource;
    public int startingPitch = 1;
    public float changedPitch = 0f;

        
        
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip1;
        audioSource.pitch = startingPitch;
        
    }

    public void onClickChangeMode()
    {
        SceneManager.LoadScene("Setting");
    }

    // Update is called once per frame
    void Update()
    {
        if(flag == true){
            currentTime += Time.deltaTime;
            if(currentTime >= span){
                Debug.LogFormat ("{0}秒経過", total_time);
                currentTime = 0f;
                total_time ++;
                if(total_time == 6f){
                    flag = false;
                    acceleration = acceleration / 5;
                    Debug.Log("ave is " + acceleration);
                    //Text Ac = Ac.GetComponent<Text> ();
                    Ac.text =  "Acceleration:" + acceleration.ToString();
                    if(acceleration <= 1.0f)
                    {
                        changedPitch = 0.5f;
                    }
                    else if(acceleration >= 3.0f || acceleration <= 5.0f)
                    {
                        changedPitch = 1.0f;
                    }
                    else
                    {
                        changedPitch = 1.5f;
                    }
                    audioSource.pitch = changedPitch;
                    total_time = 0f;
                    acceleration = 0f;
                }
                ac_y = Input.acceleration.x;
                ac_y = Input.acceleration.x;
                ac_y = Input.acceleration.x;
                acceleration += Mathf.Sqrt(Mathf.Pow(ac_x, 2) + Mathf.Pow(ac_y, 2) + Mathf.Pow(ac_z, 2));
                Debug.Log(acceleration);
            }
        }

    }

    public void onClick()
    {
        Debug.Log("Start!");
        flag = true;
    }
}
