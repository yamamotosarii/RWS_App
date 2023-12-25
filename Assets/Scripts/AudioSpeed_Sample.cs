using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioSpeed_Sample : MonoBehaviour
{
    [SerializeField] AudioClip clip_BGM;
    [SerializeField] float loopStart_BGM;//BGMのループが始まる秒数
    [SerializeField] float loopEnd_BGM;//BGMのループが終わる秒数
    float[] pitches = {0.7937f, 0.8409f, 0.8909f, 0.94387f, 1f,
    1.05946f, 1.12246f, 1.18921f, 1.25992f, 1.33484f, 1.41421f, 1.49831f, 1.5874f, 1.68179f, 1.7818f, 1.88775f, 2f };
    //ピッチが-4から+12までの倍率を格納したテーブル（Indexが4の時に等速）
    //このテーブルは2の（N/12）乗根を計算することで求めることができます。

    int resentBar = 0;
    int nowPitch = 4;
    int aimPitch = 4;
    AudioSource audioSource;
    ////////////////////////////////////////////////////デモ用
    [SerializeField] Text demoText;
    ////////////////////////////////////////////////////
    void Start()
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip_BGM;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void onClickMinus()
    {
        if(aimPitch > 0)
        {
        aimPitch--;
        }
    }
    public void onClickChangeMode()
    {
         SceneManager.LoadScene("SampleScene");
    }
    public void onClickPlus()
    {
        if(aimPitch < 12)
        {
        aimPitch++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        SpeedChange();
        LoopCheck();
        DemoFunc();
    }

    void SpeedChange()
    {
        //BGMが転調可能ポイントに差し掛かった時に、目標ピッチとのずれがあれば転調する
        if (CheckResentBar())
        {
            if (nowPitch != aimPitch)
            {
                nowPitch = aimPitch;
            }
        }
        float _pitch = pitches[nowPitch];
        audioSource.pitch = _pitch;
    }

    void LoopCheck()
    {
        //BGMがループポイントに来た時にループ処理を行う
        float _t = audioSource.time;
        if (_t > loopEnd_BGM)
        {
            audioSource.time = audioSource.time - (loopEnd_BGM - loopStart_BGM);
        }
    }

    bool CheckResentBar(float _t =10f)
    {
        //BGMが一回ループする間に何回転調可能時間を付けるか？
        //（例）20小節の曲で、2小節に一回転調できるように設定する場合　_t = 20 / 2
        float _f = (loopEnd_BGM - loopStart_BGM) / _t;
        int _i = (int)((audioSource.time - loopStart_BGM) / _f);
        if (resentBar != _i)
        {
            resentBar = _i;
            return true;
        }
        else
        {
            return false;
        }
    }

    void DemoFunc()
    {
        if (nowPitch != aimPitch)
        {
            demoText.text = "転調待機中…\n" + (nowPitch - 4) + " → " + (aimPitch - 4);
        }
        else
        {
            demoText.text = "再生中\nPitch:" + (nowPitch - 4);
        }
    }
    public void PitchChange(int i)
    {
        aimPitch = aimPitch + i;
        aimPitch = Mathf.Clamp(aimPitch, 0, 16);
    }
}

