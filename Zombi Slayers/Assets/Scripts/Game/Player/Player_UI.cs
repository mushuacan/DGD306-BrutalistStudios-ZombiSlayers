using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Player_UI : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadePower;

    [Header("Referances")]
    [SerializeField] private Slider castTimer;
    [SerializeField] private Slider coolDown;
    [SerializeField] private RawImage weaponImage;
    [SerializeField] private RawImage heart1;
    [SerializeField] private RawImage heart2;
    [SerializeField] private RawImage heart3;
    [SerializeField] private RawImage heart4;
    [SerializeField] private TextMeshProUGUI ammoText;


    [Header("Referances")]
    [SerializeField] private Player_Character player;

    // Start is called before the first frame update
    public void StarterPack()
    {
        castTimer.gameObject.SetActive(false);
        coolDown.gameObject.SetActive(false);
        if (player.character.weapon.icon != null)
            weaponImage.texture = player.character.weapon.icon;
    }

    public void StartCastTimer(float time)
    {
        castTimer.gameObject.SetActive(true);
        castTimer.value = 0;
        castTimer.DOValue(1, time).SetEase(Ease.Linear).OnComplete(() =>
        {
            castTimer.gameObject.SetActive(false);
            castTimer.value = 0;
            weaponImage.DOFade(fadePower, fadeDuration);
        });
    }

    public void StartCooldown(float time)
    {
        coolDown.gameObject.SetActive(true);
        coolDown.value = 0;
        coolDown.DOValue(1, time).SetEase(Ease.Linear).OnComplete(() =>
        {
            coolDown.gameObject.SetActive(false);
            coolDown.value = 0;
            weaponImage.DOFade(1f, fadeDuration);
        });
    }

    public void ArrangeAmmoCounter(int currentAmmo, int totalAmmo, bool showIt, bool hasTotalAmmo)
    {
        if (showIt)
        {
            if (hasTotalAmmo)
            {
                ammoText.text = (currentAmmo.ToString() + "/" + totalAmmo.ToString());
            }
            else
            {
                ammoText.text = (currentAmmo.ToString());
            }
        }
    }

    public void ArrangeHearts(int hearts)
    {
        if (hearts > 0)
        {
            heart1.gameObject.SetActive(true);
        } else
        {
            heart1.gameObject.SetActive(false);
        }
        if (hearts > 1)
        {
            heart2.gameObject.SetActive(true);
        }
        else
        {
            heart2.gameObject.SetActive(false);
        }
        if (hearts > 2)
        {
            heart3.gameObject.SetActive(true);
        }
        else
        {
            heart3.gameObject.SetActive(false);
        }
        if (hearts > 3)
        {
            heart4.gameObject.SetActive(true);
        }
        else
        {
            heart4.gameObject.SetActive(false);
        }

    }
}
