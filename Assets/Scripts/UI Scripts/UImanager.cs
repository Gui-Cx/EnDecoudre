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


    public void SwapSkill(Sprite skill, int playerIndex, int faceValue) {

        switch(playerIndex) {
            case 0:
            playerOneSkill.sprite = skill;
            //playerOneSkill.fillAmount = skill.maxCharges;
            playerOneDiceface.text = faceValue.ToString();
            break;
            case 1:
            playerTwoSkill.sprite = skill;
            //playerTwoSkill.fillAmount = skill.maxCharges;
            playerTwoDiceface.text = faceValue.ToString();
            break;
        }
    }

    public void UpdateShotCount(float amount, int maxAmmo,  int playerIndex) {

        amount = amount / maxAmmo;
        switch(playerIndex) {
            case 0:
            playerOneSkill.fillAmount = amount;
            break;
            case 1:
            playerTwoSkill.fillAmount = amount;
            break;
        }
    }

    public void UpdatePlayerLife(int amount, int maxLife, int playerIndex) {

        amount = amount / maxLife;

        switch (playerIndex) {
            case 0:
            playerOneLife.fillAmount = amount;
            break;
            case 1:
            playerTwoLife.fillAmount = amount;
            break;
        }
    }
}
