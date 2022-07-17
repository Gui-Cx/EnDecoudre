using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public Image playerOneSkill, playerTwoSkill;
    public TextMeshProUGUI playerOneDiceface, playerTwoDiceface;
    public Image playerOneLife, playerTwoLife;
    public Transform canvas;

    Animator anim;

    void Awake() {
        anim = canvas.GetComponent<Animator>();
    }
    
    public void SwapSkill(Power power, Player player) {

        int fixedCurrentFace = player.currentFace+1;

        switch(player.getIndexOfPrefab()) {
            case 0:
            playerOneSkill.sprite = power._powerData.sprite;
            playerOneSkill.fillAmount = 1;  
            playerOneDiceface.text = fixedCurrentFace.ToString();
            break;
            case 1:
            playerTwoSkill.sprite = power._powerData.sprite;
            playerTwoSkill.fillAmount = 1;
            playerTwoDiceface.text = fixedCurrentFace.ToString();
            break;
        }
    }

    public void UpdateShotCount(Power currentPower, Player player) {

        float amount = (float) currentPower.currentCharges / (float) currentPower.totalCharges;
        switch(player.getIndexOfPrefab()) {
            case 0:
            playerOneSkill.fillAmount = amount;
            break;
            case 1:
            playerTwoSkill.fillAmount = amount;
            break;
        }
    }

    public void UpdatePlayerLife(Player player) {

        float amount = player.hp / player.maxHP;

        switch (player.getIndexOfPrefab()) {
            case 0:
            playerOneLife.fillAmount = amount;
            break;
            case 1:
            playerTwoLife.fillAmount = amount;
            break;
        }
    }

    public void StartGame() {
        anim.SetTrigger("IsStarted"); 
    }

    public void TriggerMainMenu(bool status) {
        if (status) {
            anim.SetTrigger("MainMenuOpen");
        } else {
            anim.SetTrigger("MainMenuClose");
            GameManager.Instance.UpdateGameState(GameManager.GameState.InGame);
        }
    }

    public void TriggerGameOver(bool status) {
        if (status) {
            anim.SetTrigger("GameOverOpen");
        } else {
            anim.SetTrigger("GameOverClose");
        }
    }

    public void QuitGame() {
        Application.Quit();
    }
}
