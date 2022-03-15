using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    //트리거에 다른 게임 오브젝트가 들어왔을 때 실행되는 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.transform.parent = EnemyPool.Inst.gameObject.transform;
            EnemyPool.Inst.ReturnEnemy(collision.gameObject);
        }
    }
}
