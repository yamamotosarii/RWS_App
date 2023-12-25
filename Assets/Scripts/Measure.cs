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
    bool first_data = true;
    public float acceleration = 0f;
    public float ac_x;
    public float ac_y;
    public float ac_z;
    public Text Ac = null;
    public AudioClip audioClip1;
    private AudioSource audioSource;
    public int startingPitch = 1;
    public float changedPitch = 0f;
    float max_acc;
    float min_acc;
    float acc_data;

        
        
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip1;
        audioSource.pitch = startingPitch;
        audioSource.loop = true;
        audioSource.Play();
        
    }

    public void onClickChangeMode()
    {
        SceneManager.LoadScene("Setting");
    }

    // Update is called once per frame
    void Update()
    {
        if(first_data == true)
        {
            ac_y = Input.acceleration.x;
            ac_y = Input.acceleration.x;
            ac_y = Input.acceleration.x;
            acceleration += Mathf.Sqrt(Mathf.Pow(ac_x, 2) + Mathf.Pow(ac_y, 2) + Mathf.Pow(ac_z, 2));
            max_acc = acceleration;
            min_acc = acceleration;
            first_data = false;
        }
        if(flag == true){
            currentTime += Time.deltaTime;
            ac_y = Input.acceleration.x;
            ac_y = Input.acceleration.x;
            ac_y = Input.acceleration.x;
            acceleration = Mathf.Sqrt(Mathf.Pow(ac_x, 2) + Mathf.Pow(ac_y, 2) + Mathf.Pow(ac_z, 2));
            if(acceleration > max_acc)
            {
                max_acc = acceleration;
            }
            if(acceleration < min_acc)
            {
                min_acc = acceleration;
            }
            if(currentTime >= span){
                Debug.LogFormat ("{0}秒経過", total_time);
                currentTime = 0f;
                total_time ++;
                if(total_time == 6f){
                    flag = false;
                    acc_data = max_acc - min_acc;
                    Ac.text =  "Acceleration:" + acc_data.ToString();
                    if(acc_data < 1.5f)
                    {
                        changedPitch = 0.5f;
                    }
                    else if(acceleration >= 1.5f || acceleration <= 3.0f)
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
            }
        }

    }

    public void onClick()
    {
        Debug.Log("Start!");
        flag = true;
        first_data = true;
    }
}
