using UnityEngine;

public class ButtonAvoidance : MonoBehaviour
{
    public float avoidanceRange = 100f;   // Fare ile buton arasýndaki mesafe eþiði
    public float offsetDistance = 50f;    // Butonun kaçacaðý mesafe

    public Vector2 triggerCenter;         // Tetikleme merkezi (örneðin: 300, 300)
    public float triggerRadius = 50f;     // Yarýçap (örneðin: 50)

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

        // Buton kaçma davranýþý
        if (distance < avoidanceRange)
        {
            Vector3 direction = (buttonRectTransform.position - mousePosition).normalized;
            Vector3 newPosition = buttonRectTransform.position + direction * offsetDistance;
            buttonRectTransform.position = newPosition;
        }

        // Tetikleme alanýna girdiðinde olay tetiklenir
        if (!hasTriggered && IsInTriggerZone(buttonRectTransform.position))
        {
            hasTriggered = true;
            TriggerSpecialEvent();
        }
    }

    // Butonun belirtilen merkez ve yarýçap içindeki alanda olup olmadýðýný kontrol eder
    bool IsInTriggerZone(Vector3 position)
    {
        float distanceToCenter = Vector2.Distance(new Vector2(position.x, position.y), triggerCenter);
        if (debugger) Debug.Log("Mesafe:" + distanceToCenter);
        return distanceToCenter <= triggerRadius;
    }

    // Olay tetiklendiðinde yapýlacak iþlemler
    void TriggerSpecialEvent()
    {
        if (debugger) Debug.Log("Buton tetikleme alanýna girdi! Olay tetiklendi.");
        stop = true;
    }
}
