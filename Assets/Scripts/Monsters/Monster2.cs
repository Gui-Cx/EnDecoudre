using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : Monster
{
    [SerializeField] public GameObject BulletPrefab;
    public override IEnumerator doAttack(Player player)
    {
        if (canAttack)
        {
            canAttack = false;
            Vector2 directionToTarget = player.transform.position - transform.position;
            float angle = Vector3.Angle(Vector3.right, directionToTarget);
            if (player.transform.position.y < transform.position.y) angle *= -1;
            Quaternion bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject bullet = Instantiate(BulletPrefab, transform.position, bulletRotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            yield return new WaitForSeconds(cooldown);
            canAttack = true;
            monsterState = MonsterStates.Reaching;
            yield return null;
        }

    }
}
