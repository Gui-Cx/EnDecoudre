using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : Monster
{
    private void Update()
    {
        // print("hnelo");
    }

    public override IEnumerator doAttack(Player player)
    {
        player.takeDamage(degat);
        yield return new WaitForSeconds(cooldown);
        monsterState = MonsterStates.Reaching;
        yield return null;
    }

}
