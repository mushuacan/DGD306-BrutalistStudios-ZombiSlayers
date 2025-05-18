using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;

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
    [SerializeField] private RawImage[] heartsArray;
    [SerializeField] private Tween heartsTween;


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
    public void WeaponUsing()
    {
        weaponImage.DOFade(fadePower, fadeDuration + 0.2f);
    }
    public void StartWeaponCooldown(float time)
    {
        weaponCooldown.gameObject.SetActive(true);
        weaponCooldown.fillAmount = 1;
        weaponCooldown.DOFillAmount(0, time).SetEase(Ease.Linear).OnComplete(() =>
        {
            weaponCooldown.gameObject.SetActive(false);
            weaponCooldown.fillAmount = 0;
            weaponImage.DOFade(1f, fadeDuration);
        });
    }
    public void SecondAbilityUsing()
    {
        secondAbilityImage.DOFade(fadePower, fadeDuration);
    }
    public void StartSecondAbilityCooldown(float time)
    {
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
    private void ArrangeHeart(int index, bool activeness)
    {
        if (activeness)
        {
            if (!heartsArray[index].gameObject.activeSelf)
            {
                heartsArray[index].gameObject.SetActive(true);
                heartsArray[index].transform.localScale = Vector3.zero; // Baþlangýçta küçültüyoruz
                heartsTween = heartsArray[index].transform.DOScale(new Vector3(0.5f, 0.5f, 0), 0.3f) // Ýlk büyüme 0.6'a
                    .SetEase(Ease.OutBack) // Daha yumuþak bir büyüme
                    .OnComplete(() =>
                    {
                        // Büyüme sonrasý fazla gittiði için biraz geri dönsün
                        heartsTween = heartsArray[index].transform.DOScale(new Vector3(0.4f, 0.4f, 0), 0.2f)
                            .SetEase(Ease.InOutQuad); // Geri dönüþ animasyonu
                    });
            }
        }
        else
        {
            if (heartsArray[index].gameObject.activeSelf)
            {
                heartsTween = heartsArray[index].transform.DOScale(Vector3.zero, 0.3f)
                    .OnKill(() => heartsArray[index].gameObject.SetActive(false)); // Küçülme efekti
            }
        }
    }

    public void ArrangeHearts(int hearts, bool withAnimation)
    {
        heartsTween.Complete();
        for (int i = 0; i < heartsArray.Length; i++)
        {
            if (hearts >= i + 1)
            {
                if (withAnimation)
                {
                    ArrangeHeart(i, true);
                }
                else
                {
                    heartsArray[i].gameObject.SetActive(true);
                    heartsArray[i].transform.DOScale(new Vector3(0.4f, 0.4f, 1), 0.2f);
                }
            }
            else
            {
                if (withAnimation)
                {
                    ArrangeHeart(i, false);
                }
                else
                {
                    heartsArray[i].gameObject.SetActive(false);
                    heartsArray[i].transform.DOScale(new Vector3(0.4f, 0.4f, 1), 0.2f);
                }
            }
        }
    }
}
