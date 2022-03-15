using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer : MonoBehaviour
{
    // ����1. �׽�Ʈ �÷��̾�� �ٴڿ� ������ �״´�
    // ����1. �׽�Ʈ �÷��̾�� ������ ������ �״´�
    // ����1. �׽�Ʈ �÷��̾�� ������ �ݴ��� �������� ������. z�� + �������� ȸ��
    // ����1. �׽�Ʈ �÷��̾�� ������ ������ �ȵ�
    // ����1. �׽�Ʈ �÷��̾�� ������ ��� ��ũ���� ���� 

    // ��Ʈ 
    // OncollisionEnter // �� ��ũ��Ʈ�� ���� �ö��̴��� �ٸ� �ö��̴��� �浹 ���� �� ����Ǵ� �Լ�
    // AddTorque        // Rigidbody�� �ɹ� �Լ�. ȸ���� �����ش�.

    private Rigidbody2D rigid = null;   
   
   
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    //�� ��ũ��Ʈ�� ���� �ö��̴��� �ٸ� �ö��̴��� �浹 ���� �� ����Ǵ� �Լ�
    private void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log(collision.gameObject.name + "�浹");
        OnDead(); 

        if (collision.gameObject.CompareTag("Ground")) //.Tag == "Ground" ���ٴ� CompareTag�� �� ����
        {
            OnFalldown(collision.gameObject);
        }
    }
    public void OnDead()
    {
        rigid.gravityScale = 1.0f; //�׽�Ʈ �÷��̾� ����. �׾��� ���� �߷� ���� ����
        rigid.AddTorque(3.0f);     //������ٵ� ���� ȸ���� �߰�
    }
    private void OnFalldown(GameObject ground) // �ٴڿ� ����������
    {
       // Debug.Log(" �� �ε�ħ ");
        // ���̶�Ű���� ������ ��
        // FindObjectOfType : Ư�� Ÿ���� �����ʴ��� ������ �ִ� ù��° ���� ������Ʈ�� ã���ִ� �Լ�
        // FindObjectsOfType : Ư�� Ÿ���� �����ʴ��� ������ �ִ� ��� ���� ������Ʈ�� ã���ִ� �Լ�
        // GameObject.Find : �Ķ���ͷ� ���� ���ڿ��� ���� �̸��� ���� ���� ������Ʈ�� ã���ִ� �Լ� -> ���ڿ� �񱳶� Bad
        // GameObject.FindGameObjectWithnTag : �Ķ���ͷ� ���� ���ڿ��� ���� �±׸� ���� ù��° ���� ������Ʈ�� ã���ִ� �Լ�
        // GameObject.FindGameObjecstWithnTag : �Ķ���ͷ� ���� ���ڿ��� ���� �±׸� ���� ��� ���� ������Ʈ�� ã���ִ� �Լ�

        Scroller scroller = ground.GetComponent<Scroller>();
        scroller.ScrollSwitch = false;
    }

    public void TestPoolUse(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            GameObject obj = EnemyPool.Inst.GetEnemy(0);
            Debug.Log(obj);
        }
    }


}
