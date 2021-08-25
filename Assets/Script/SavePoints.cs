using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoints : MonoBehaviour
{
    public GameObject Player;
    public Animator PlayerAnimator;
    public GameObject GameOverPannel;
    public GameObject SaveEffect;
    public Player PlayerScript;
    public Shop shopScript;
    public GameObject LoadplayerEffect;
    public GameObject SavedText;
    public GameObject Canves;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            SfxManager.instance.PLay("GameSaved");
            GameObject effect = Instantiate(SaveEffect, transform.position, Quaternion.Euler(-90,0,0));
            Destroy(effect, 1f);
            SaveSystem.SavePlayer(PlayerScript);
            SavedText.SetActive(true);
            StartCoroutine("SavedTextDisable");

        }
    }

    IEnumerator SavedTextDisable()
    {
        yield return new WaitForSeconds(1f);
        SavedText.SetActive(false);
    }

    public void LoadData()
    {
        PlayerScript.PlayerDied = false;
        StartCoroutine("WaitForLoadGame");
    }

    IEnumerator WaitForLoadGame()
    {
        yield return new WaitForSeconds(1.30f);
        PlayerData data = SaveSystem.LoadPlayer();


        PlayerScript.HealthEqualsTo(data.healtH);
        PlayerScript.coins = data.coin;
        PlayerScript.Potions = data.potion;

        Vector2 position;
        position.x = data.position[0];
        position.y = data.position[1];

        Player.transform.position = position;
        PlayerAnimator.Play("PlayerIdle");
        GameObject effect = Instantiate(LoadplayerEffect, position, Quaternion.Euler(-90,0,0));
        effect.transform.parent = Player.transform;
        Destroy(effect, 1f);
        SfxManager.instance.PLay("PlayerSpawn");
    }
}
