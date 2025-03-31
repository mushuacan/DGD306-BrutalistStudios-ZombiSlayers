using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    public bool contuniu = true;

    public enum AlarmTypes
    {
        Important,
        Bad,
        Debug
    }

    public void Alarm(string text, AlarmTypes alarmType)
    {
        if (alarmType == AlarmTypes.Important)
        {
            Time.timeScale = 0f;
            Debug.LogError(text);
            Debug.Log("Oyunun akýþý bu hatadan dolayý durduruldu.");
            contuniu = false;
        }
        if (alarmType == AlarmTypes.Bad)
        {
            Debug.LogWarning(text);
        }
        if (alarmType == AlarmTypes.Debug)
        {
            Debug.Log(text);
        }
    }
}
