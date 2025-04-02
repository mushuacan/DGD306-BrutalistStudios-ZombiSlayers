using UnityEngine;

public class ButtonAvoidance : MonoBehaviour
{
    public float avoidanceRange = 100f;  // Fare ile buton aras�ndaki mesafe e�i�i
    public float offsetDistance = 50f;   // Butonun ka�aca�� mesafe

    private RectTransform buttonRectTransform;

    void Start()
    {
        buttonRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        // Fare ile buton aras�ndaki mesafeyi hesapla
        float distance = Vector3.Distance(mousePosition, buttonRectTransform.position);

        // E�er fare butona yak�nsa buton ka�acak
        if (distance < avoidanceRange)
        {
            Vector3 direction = (buttonRectTransform.position - mousePosition).normalized; // Fareye z�t y�n
            Vector3 newPosition = buttonRectTransform.position + direction * offsetDistance;

            // Yeni pozisyonu aniden butona uygula
            buttonRectTransform.position = newPosition;
        }
    }
}
