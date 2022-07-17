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


    public void SwapSkill(Power power, Player player) {

        switch(player.getIndexOfPrefab()) {
            case 0:
            //playerOneSkill.sprite = skill;
            playerOneSkill.fillAmount = power.totalCharges;
            playerOneDiceface.text = player.currentFace.ToString();
            break;
            case 1:
            //playerTwoSkill.sprite = skill;
            playerTwoSkill.fillAmount = power.totalCharges;
            playerTwoDiceface.text = player.currentFace.ToString();
            break;
        }
    }

    public void UpdateShotCount(Power currentPower, Player player) {

        float amount = currentPower.currentCharges / currentPower.totalCharges;
        switch(player.getIndexOfPrefab()) {
            case 0:
            playerOneSkill.fillAmount = amount;
            break;
            case 1:
            playerTwoSkill.fillAmount = amount;
            break;
        }
    }

    public void UpdatePlayerLife(int amount, int maxLife, Player player) {

        amount = amount / amount;

        switch (player.getIndexOfPrefab()) {
            case 0:
            playerOneLife.fillAmount = amount;
            break;
            case 1:
            playerTwoLife.fillAmount = amount;
            break;
        }
    }
}
