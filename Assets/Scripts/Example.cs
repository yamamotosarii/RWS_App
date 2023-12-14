using UnityEngine;

public class Example : MonoBehaviour
{
    public AudioClip clip;

    private void Start()
    {
        int bpm = UniBpmAnalyzer.AnalyzeBpm( clip );
        Debug.Log(bpm);
    }

}