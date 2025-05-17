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
    [SerializeField] private Image weaponCooldown, secondAbilityCooldown;
    [SerializeField] private Image weaponImage, secondAbilityImage;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI secondAbilityCountText;
    [SerializeField] private RawImage heart1, heart2, heart3, heart4;


    [Header("Referances")]
    [SerializeField] private Player_Character player;

    // Start is called before the first frame update
    public void StarterPack()
    {
        if (player.character.weapon.icon != null)
        {
            weaponImage.gameObject.SetActive(true);
            weaponImage.sprite = player.character.weapon.icon;
        }
        else
        {
            weaponImage.gameObject.SetActive(false);
        }
        if (player.character.secondAbility != null)
        {
            if (player.character.secondAbility.icon != null)
            {
                secondAbilityImage.gameObject.SetActive(true);
                secondAbilityImage.sprite = player.character.secondAbility.icon;
            }
            else
            {
                secondAbilityImage.gameObject.SetActive(false);
            }
        }
        else
        {
            if (player.character.secondAbilityIcon != null)
            {
                secondAbilityImage.gameObject.SetActive(true);
                secondAbilityImage.sprite = player.character.secondAbilityIcon;
            }
            else
            {
                secondAbilityImage.gameObject.SetActive(false);
            }
        }

        castTimer.gameObject.SetActive(false);
        weaponCooldown.fillAmount = 0;
        secondAbilityCooldown.fillAmount = 0;
    }

    public void StartCastTimer(float time)
    {
        castTimer.gameObject.SetActive(true);
        castTimer.value = 0;
        castTimer.DOValue(1, time).SetEase(Ease.Linear).OnComplete(() =>
        {
            castTimer.gameObject.SetActive(false);
            castTimer.value = 0;
        });
    }

    public void StartWeaponCooldown(float time)
    {
        weaponImage.DOFade(fadePower, fadeDuration);
        weaponCooldown.gameObject.SetActive(true);
        weaponCooldown.fillAmount = 1;
        weaponCooldown.DOFillAmount(0, time).SetEase(Ease.Linear).OnComplete(() =>
        {
            weaponCooldown.gameObject.SetActive(false);
            weaponCooldown.fillAmount = 0;
            weaponImage.DOFade(1f, fadeDuration);
        });
    }

    public void StartSecondAbilityCooldown(float time)
    {
        secondAbilityImage.DOFade(fadePower, fadeDuration);
        secondAbilityCooldown.gameObject.SetActive(true);
        secondAbilityCooldown.fillAmount = 1;
        secondAbilityCooldown.DOFillAmount(0, time).SetEase(Ease.Linear).OnComplete(() =>
        {
            secondAbilityCooldown.gameObject.SetActive(false);
            secondAbilityCooldown.fillAmount = 0;
            secondAbilityImage.DOFade(1f, fadeDuration);
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

    public void ArrangeSecondAbilityCounter( int totalAmmo, bool showIt)
    {
        if (showIt)
        {
            secondAbilityCountText.text = (totalAmmo.ToString());
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
