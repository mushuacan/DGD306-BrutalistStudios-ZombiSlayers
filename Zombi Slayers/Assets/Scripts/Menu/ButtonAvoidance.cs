using UnityEngine;

public class ButtonAvoidance : MonoBehaviour
{
    public float avoidanceRange = 100f;  // Fare ile buton arasýndaki mesafe eþiði
    public float offsetDistance = 50f;   // Butonun kaçacaðý mesafe

    private RectTransform buttonRectTransform;

    void Start()
    {
        buttonRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        // Fare ile buton arasýndaki mesafeyi hesapla
        float distance = Vector3.Distance(mousePosition, buttonRectTransform.position);

        // Eðer fare butona yakýnsa buton kaçacak
        if (distance < avoidanceRange)
        {
            Vector3 direction = (buttonRectTransform.position - mousePosition).normalized; // Fareye zýt yön
            Vector3 newPosition = buttonRectTransform.position + direction * offsetDistance;

            // Yeni pozisyonu aniden butona uygula
            buttonRectTransform.position = newPosition;
        }
    }
}
