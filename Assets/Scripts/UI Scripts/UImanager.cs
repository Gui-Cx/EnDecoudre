using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public Image playerOneSkill, playerTwoSkill;
    public TextMeshProUGUI playerOneDiceface, playerTwoDiceface;
    public Image[] playerOneHeart, playerTwoHeart;
    public Sprite emptyHeart, fullHeart;

    
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

    public void UpdatePlayerLife(int amount, int playerIndex) {

        if (amount > 3 || amount < 0) {
            Debug.Log("Wrong amount ! can't have more or less than 3 hp");
            return;
        }

        switch (playerIndex) {
            case 0:
            for (int i = 0; i < playerOneHeart.Length; i++) {
                if (i < amount) {
                    playerOneHeart[i].sprite = fullHeart;
                } else {
                    playerOneHeart[i].sprite = emptyHeart;
                }
            }
            break;
            case 1:
            for (int i = 0; i < playerTwoHeart.Length; i++) {
                if (i < amount) {
                    playerTwoHeart[i].sprite = fullHeart;
                } else {
                    playerTwoHeart[i].sprite = emptyHeart;
                }
            }
            break;
        }
    }
}
