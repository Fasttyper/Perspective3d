using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float timeChangeRate = 0.01f;
    public float minTimeScale = 0.001f;
    public float maxTimeScale = 1f;
    public float GlobalCurrentTime = 0f;
    public bool canSimulate = false;
    public float timeScrollSpeed = 10f;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) && Time.timeScale < maxTimeScale)
        {
            Time.timeScale = Mathf.Min(Time.timeScale + timeChangeRate, maxTimeScale);
        }
        else if (Input.GetKey(KeyCode.S) && Time.timeScale > minTimeScale)
        {
            Time.timeScale = Mathf.Max(Time.timeScale - timeChangeRate, minTimeScale);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            canSimulate = !canSimulate;
        }
    }   

    private void FixedUpdate()
    {
        if (canSimulate)
        {
            GlobalCurrentTime += Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            GlobalCurrentTime -= Time.fixedDeltaTime * timeScrollSpeed;
        } else if (Input.GetKey(KeyCode.D)) {
            GlobalCurrentTime += Time.fixedDeltaTime * timeScrollSpeed;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), "Time Scale: " + Time.timeScale.ToString());
    }
}
