using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Player Player;
    public SfxManager sfxManagerScript;
    public GameObject PlayerObject;
    public Animator ConfirmPannelAnimator;
    public Animator UpgradeConfirmPannelAnimator;
    public AudioSource HelloStrangerSound;
    public GameObject pauseMenu;
    public GameObject PurchessCompleateText;
    public GameObject SorryText;
    public GameObject AlreadyPurchesedText;
    public GameObject EnterShopmenu;
    public GameObject ShopPannel;
    public Animator EnterShopPannelAnimator;
    public GameObject SkillsPannel;
    public GameObject LightningConfirmPannel;
    public GameObject SattackButton;
    public GameObject PetsPannel;
    public GameObject PotionPannel;
    public GameObject po_ConfirmPannel;
    public GameObject HealPotionButtonIngame;
    //Fox.......
    public GameObject FoxImage;
    public GameObject Fox;
    public GameObject FoxConfirmPannel;
    public GameObject UpgradeFoxButton;
    public GameObject UpgradefoxPannel;
    public GameObject FoxUpgradeLevelConfirmPannel;
    public GameObject FoxupgradeToLevel1Text,FoxupgradeToLevel2Text,FoxupgradeToLevel3Text,FoxupgradeToLevel4Text,FoxupgradeToLevelMaxText;
    public GameObject FoxlevelOneUnlockedText,FoxlevelTwoUnlockedText,FoxlevelThreeUnlockedText,FoxlevelFourUnlockedText;


    public Vector2 TextInstantiatePos;
    bool HelloStrangerSoundPlayed;
    bool FoxLevelOneUnlocked,FoxLevelTwoUnlocked,FoxLevelThreeUnlocked,FoxLevelFourUnlocked;
    public bool levelOneRunning,levelTwoRunning,levelThreeRunning,levelFourRunning;
    bool FoxIsActive ;


    public bool LightningButtonActive ;
    //public int HealPotions;
    public GameObject HealEffect_13;
    public GameObject HealEffect13;
    public Text HealPotionCount;
    public Text HealPotionCountIngame;


    void Start()
    {
        FoxIsActive = false;
        LightningButtonActive = false;
        HelloStrangerSoundPlayed = false;
        FoxLevelOneUnlocked = false;
        FoxLevelTwoUnlocked = false;
        FoxLevelThreeUnlocked = false;
        FoxLevelFourUnlocked = false;

        levelOneRunning = false;
        levelTwoRunning = false;
        levelThreeRunning = false;
        levelFourRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        HealPotionCount.text = Player.Potions.ToString();
        HealPotionCountIngame.text = Player.Potions.ToString();
        if(Player.Potions>0)
        {
            HealPotionButtonIngame.SetActive(true);
        }
        else
        {
            HealPotionButtonIngame.SetActive(false);
        }



        if(FoxIsActive == true)
        {
            UpgradeFoxButton.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            EnterShopmenu.SetActive(true);
            if(HelloStrangerSoundPlayed == false)
            {
                HelloStrangerSound.Play();
                HelloStrangerSoundPlayed = true;
                StartCoroutine("PlayHelloStrangerSoundAginAfter_seconds");
            }
        }
    }

    IEnumerator PlayHelloStrangerSoundAginAfter_seconds()
    {
        yield return new WaitForSeconds(60f);
        HelloStrangerSoundPlayed = false;
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            EnterShopmenu.SetActive(false);
        }
    }

    public void PauseMenuButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void EnterShopButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        ShopPannel.SetActive(true);
        EnterShopmenu.SetActive(false);
    }
    public void EXitShopButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        ShopPannel.SetActive(false);
    }
    public void CancelButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        EnterShopmenu.SetActive(false);
    }

    public void BackButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        ShopPannel.SetActive(false);
    }

    public void SkillskButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        SkillsPannel.SetActive(true);
    }

    public void SkillsPannelBackButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        SkillsPannel.SetActive(false);
        PetsPannel.SetActive(false);
    }
    public void lightningButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        if (LightningButtonActive == false)
        {
            if (Player.coins >= 30)
            {
                LightningConfirmPannel.SetActive(true);
            }
            if (Player.coins < 30)
            {
                GameObject TextInstanse = Instantiate(SorryText, TextInstantiatePos, Quaternion.identity);
                Destroy(TextInstanse, 2f);
            }
        }
        if(LightningButtonActive == true)
            {
                GameObject TextInstanse = Instantiate(AlreadyPurchesedText,TextInstantiatePos,Quaternion.identity);
                Destroy(TextInstanse,2f);
            }
    }

    public void lightningConfirmButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        SattackButton.SetActive(true);
        GameObject TextInstanse = Instantiate(PurchessCompleateText,TextInstantiatePos,Quaternion.identity);
        Destroy(TextInstanse,2f);
        Player.coins -= 30;
        LightningButtonActive = true;
        LightningConfirmPannel.SetActive(false);
    }
    public void lightningConfirmPannelCancelButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        LightningConfirmPannel.SetActive(false);
    }
    

    public void PetsButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        PetsPannel.SetActive(true);
    }
    public void FoxButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        FoxImage.SetActive(true);

        if(Player.coins >= 50 && FoxIsActive == false)
        {
            FoxConfirmPannel.SetActive(true);
        
        }
        else if(Player.coins < 50 && FoxIsActive == false)
        {
            GameObject TextInstanse = Instantiate(SorryText,TextInstantiatePos,Quaternion.identity);
            Destroy(TextInstanse,2f);
        }
        else if(FoxIsActive == true)
        {
            GameObject TextInstanse = Instantiate(AlreadyPurchesedText,TextInstantiatePos,Quaternion.identity);
            Destroy(TextInstanse,2f);
        }
    }
    public void ConfirmButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        Fox.SetActive(true);
        GameObject TextInstanse = Instantiate(PurchessCompleateText,TextInstantiatePos,Quaternion.identity);
        Destroy(TextInstanse,2f);
        Player.coins -= 50;
        FoxIsActive = true;
        ConfirmPannelAnimator.SetBool("Exit",true);
        StartCoroutine("FoxConfirmPannelExitAnimationPlayingTime");
    }

    public void PurchesCancelButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        StartCoroutine("FoxConfirmPannelExitAnimationPlayingTime");
        ConfirmPannelAnimator.SetBool("Exit",true);
    }

    public void upgradeFoxButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        UpgradefoxPannel.SetActive(true);
    }

    public void PotionButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        PotionPannel.SetActive(true);
    }
    public void helthPotionButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        if(Player.coins >= 5)
        {
            po_ConfirmPannel.SetActive(true);
        }
        else if(Player.coins < 5)
        {
            GameObject TextInstanse = Instantiate(SorryText,TextInstantiatePos,Quaternion.identity);
            Destroy(TextInstanse,2f);
        }
    }

    public void ConfirmButtonHealthPotion()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        Player.Potions++;
        GameObject TextInstanse = Instantiate(PurchessCompleateText,TextInstantiatePos,Quaternion.identity);
        Destroy(TextInstanse,2f);
        Player.coins -= 5;
        po_ConfirmPannel.SetActive(false);
        ConfirmPannelAnimator.SetBool("Exit",true);
    }
    public void CancelButtonHealthPotion()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        po_ConfirmPannel.SetActive(false);
        ConfirmPannelAnimator.SetBool("Exit",true);
    }

    public void HealPotionButtonInGame()
    {
        if(Player.Potions> 0)
        {
            if(Player.currentHealth < 100)
            {
                sfxManagerScript.PLay("HealSound");
                Player.IncreaseHealth(40);
                if (PlayerObject.transform.localScale.x == 0.13f)
                {
                    HealEffect13.SetActive(true);
                    StartCoroutine("HealEffectDeactive");
                }
                if (PlayerObject.transform.localScale.x == -0.13f)
                {
                    HealEffect_13.SetActive(true);
                    StartCoroutine("HealEffectDeactive");
                }
                Player.Potions--;
            }
        }
    }

    IEnumerator HealEffectDeactive()
    {
        yield return new WaitForSeconds(2f);
        HealEffect13.SetActive(false);
        HealEffect_13.SetActive(false);
    }

    public void Po_BackButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        PotionPannel.SetActive(false);
    }

    //Fox....................................................................................................
    public void FoxUpgradeToLevelButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        if(FoxLevelOneUnlocked == false)
        {
            if(Player.coins >= 100)
            {
                FoxUpgradeLevelConfirmPannel.SetActive(true);
        
            }
            else if(Player.coins < 100)
            {
                GameObject TextInstanse = Instantiate(SorryText,TextInstantiatePos,Quaternion.identity);
                Destroy(TextInstanse,2f);
            }
        }

        if(FoxLevelTwoUnlocked == true)
        {
            if(Player.coins >= 150)
            {
                FoxUpgradeLevelConfirmPannel.SetActive(true);
        
            }
            else if(Player.coins < 150)
            {
                GameObject TextInstanse = Instantiate(SorryText,TextInstantiatePos,Quaternion.identity);
                Destroy(TextInstanse,2f);
            }
        }
        if(FoxLevelThreeUnlocked == true)
        {
            if(Player.coins >= 250)
            {
                FoxUpgradeLevelConfirmPannel.SetActive(true);
        
            }
            else if(Player.coins < 250)
            {
                GameObject TextInstanse = Instantiate(SorryText,TextInstantiatePos,Quaternion.identity);
                Destroy(TextInstanse,2f);
            }
        }

        if(FoxLevelFourUnlocked == true)
        {
            if(Player.coins >= 300)
            {
                FoxUpgradeLevelConfirmPannel.SetActive(true);
        
            }
            else if(Player.coins < 300)
            {
                GameObject TextInstanse = Instantiate(SorryText,TextInstantiatePos,Quaternion.identity);
                Destroy(TextInstanse,2f);
            }
        }
    }
    public void FoxLevelUpgradeConfirmButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        if(FoxLevelOneUnlocked == false)
        {
        GameObject TextInstanse = Instantiate(PurchessCompleateText,TextInstantiatePos,Quaternion.identity);
        Destroy(TextInstanse,2f);
        Player.coins -= 100;
        FoxupgradeToLevel1Text.SetActive(false);
        FoxupgradeToLevel2Text.SetActive(true);
        FoxlevelOneUnlockedText.SetActive(true);
        FoxLevelOneUnlocked = true;
        levelOneRunning = true;
        
        StartCoroutine("Wait30sToPurchaseAgain");
        }

        if(FoxLevelTwoUnlocked == true)
        {
        GameObject TextInstanse = Instantiate(PurchessCompleateText,TextInstantiatePos,Quaternion.identity);
        Destroy(TextInstanse,2f);
        Player.coins -= 150;
        FoxupgradeToLevel2Text.SetActive(false);
        FoxupgradeToLevel3Text.SetActive(true);
        FoxlevelTwoUnlockedText.SetActive(true);
        FoxLevelTwoUnlocked = false;
        levelOneRunning = false;
        levelTwoRunning = true;
        
        StartCoroutine("Wait30sToPurchasel2");
        }

        if(FoxLevelThreeUnlocked == true)
        {
        GameObject TextInstanse = Instantiate(PurchessCompleateText,TextInstantiatePos,Quaternion.identity);
        Destroy(TextInstanse,2f);
        Player.coins -= 250;
        FoxupgradeToLevel3Text.SetActive(false);
        FoxupgradeToLevel4Text.SetActive(true);
        FoxlevelThreeUnlockedText.SetActive(true);
        FoxLevelThreeUnlocked = false;
        levelTwoRunning = false;
        levelThreeRunning = true;
        
        StartCoroutine("Wait30sToPurchasel3");
        }

        if(FoxLevelFourUnlocked == true)
        {
        GameObject TextInstanse = Instantiate(PurchessCompleateText,TextInstantiatePos,Quaternion.identity);
        Destroy(TextInstanse,2f);
        Player.coins -= 300;
        FoxupgradeToLevel4Text.SetActive(false);
        FoxupgradeToLevelMaxText.SetActive(true);
        FoxlevelFourUnlockedText.SetActive(true);
        FoxLevelFourUnlocked = false;
        levelFourRunning = true;
        }

        UpgradeConfirmPannelAnimator.SetBool("Exit",true);
        StartCoroutine("FoxUpgradeConfirmPannelExitAnimationPlayingTime");
    }

    IEnumerator Wait30sToPurchaseAgain()
    {
        yield return new WaitForSeconds(1f);
        FoxLevelTwoUnlocked = true;

    }
    IEnumerator Wait30sToPurchasel2()
    {
        yield return new WaitForSeconds(1f);
        FoxLevelThreeUnlocked = true;

    }
    IEnumerator Wait30sToPurchasel3()
    {
        yield return new WaitForSeconds(1f);
        FoxLevelFourUnlocked = true;

    }

    public void FoxUpgradePurchesCancelButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        UpgradeConfirmPannelAnimator.SetBool("Exit",true);
        StartCoroutine("FoxUpgradeConfirmPannelExitAnimationPlayingTime");
    }

    IEnumerator FoxConfirmPannelExitAnimationPlayingTime()
    {
        yield return new WaitForSeconds(.50f);
        FoxConfirmPannel.SetActive(false);
    }
    IEnumerator FoxUpgradeConfirmPannelExitAnimationPlayingTime()
    {
        yield return new WaitForSeconds(.50f);
        FoxUpgradeLevelConfirmPannel.SetActive(false);
    }

    public void FoxUpgradePannelBackButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        UpgradefoxPannel.SetActive(false);
    }


}
