using System.Collections;
using UnityEngine;

public class PlayerAttackCode : MonoBehaviour
{
    [Header("Hitboxes (�ڽ� Hitbox ������Ʈ 3��)")]
    public Hitbox[] hitboxes;        // 0,1,2 ���� (Collider2D+SetActive ���� ��ũ��Ʈ)

    [Header("Timings per stage")]
    public float[] windUp = { 0.06f, 0.08f, 0.10f };   // ������
    public float[] active = { 0.15f, 0.18f, 0.20f };   // ���� Ÿ��â
    public float comboWindow = 0.6f;                   // ���� �Է� ��ȿ�ð�

    Animator anim;
    bool attacking = false;
    int stage = 0;              // 0,1,2 (= 1,2,3Ÿ)
    bool queued = false;        // ���� Ÿ ���

    void Awake() { anim = GetComponent<Animator>(); }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))        // ���� �Է�(���߿� ��ư���� �ٲ㵵 ��)
        {
            if (!attacking)
            {
                StartCoroutine(DoCombo());
            }
            else if (stage < 2)
            {
                queued = true;                  // ���� �ȿ��� ������ ���� Ÿ ����
            }
        }
    }

    IEnumerator DoCombo()
    {
        attacking = true;
        stage = 0;
        float windowTimer = comboWindow;

        while (true)
        {
            // 1) ���� �������� �ִϸ��̼� ���(Ʈ���Ÿ�)
            anim.ResetTrigger("AK1"); anim.ResetTrigger("AK2"); anim.ResetTrigger("AK3");
            anim.SetTrigger(stage == 0 ? "AK1" : stage == 1 ? "AK2" : "AK3");

            // 2) ��Ʈ�ڽ� Ÿ�̹�
            yield return new WaitForSeconds(windUp[stage]);
            SetOnly(stage, true);
            yield return new WaitForSeconds(active[stage]);
            SetAll(false);

            // 3) ���� �Է� ��ٸ���(�޺� ����)
            queued = false;
            windowTimer = comboWindow;
            while (windowTimer > 0f)
            {
                windowTimer -= Time.deltaTime;
                if (queued) break;             // ���� Ÿ �Է� ����
                yield return null;
            }

            if (queued && stage < 2)
            {
                stage++;                       // 2/3Ÿ�� ����
                continue;
            }
            break;                             // �޺� ����
        }

        attacking = false;
        stage = 0;
        SetAll(false);
    }

    void SetOnly(int idx, bool on)
    {
        for (int i = 0; i < hitboxes.Length; i++) hitboxes[i].SetActive(on && i == idx);
    }
    void SetAll(bool on)
    {
        for (int i = 0; i < hitboxes.Length; i++) hitboxes[i].SetActive(on);
    }
}
