using UnityEngine;

public class ButtonAvoidance : MonoBehaviour
{
    public float avoidanceRange = 100f;   // Fare ile buton aras�ndaki mesafe e�i�i
    public float offsetDistance = 50f;    // Butonun ka�aca�� mesafe

    public Vector2 triggerCenter;         // Tetikleme merkezi (�rne�in: 300, 300)
    public float triggerRadius = 50f;     // Yar��ap (�rne�in: 50)

    private RectTransform buttonRectTransform;
    private bool hasTriggered = false;
    private bool stop = false;
    public bool debugger = false;

    void Start()
    {
        buttonRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (stop) return;
        Vector3 mousePosition = Input.mousePosition;
        float distance = Vector3.Distance(mousePosition, buttonRectTransform.position);

        // Buton ka�ma davran���
        if (distance < avoidanceRange)
        {
            Vector3 direction = (buttonRectTransform.position - mousePosition).normalized;
            Vector3 newPosition = buttonRectTransform.position + direction * offsetDistance;
            buttonRectTransform.position = newPosition;
        }

        // Tetikleme alan�na girdi�inde olay tetiklenir
        if (!hasTriggered && IsInTriggerZone(buttonRectTransform.position))
        {
            hasTriggered = true;
            TriggerSpecialEvent();
        }
    }

    // Butonun belirtilen merkez ve yar��ap i�indeki alanda olup olmad���n� kontrol eder
    bool IsInTriggerZone(Vector3 position)
    {
        float distanceToCenter = Vector2.Distance(new Vector2(position.x, position.y), triggerCenter);
        if (debugger) Debug.Log("Mesafe:" + distanceToCenter);
        return distanceToCenter <= triggerRadius;
    }

    // Olay tetiklendi�inde yap�lacak i�lemler
    void TriggerSpecialEvent()
    {
        if (debugger) Debug.Log("Buton tetikleme alan�na girdi! Olay tetiklendi.");
        stop = true;
    }
}
