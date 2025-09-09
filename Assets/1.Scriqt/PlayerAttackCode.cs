using System.Collections;
using UnityEngine;

public class PlayerAttackCode : MonoBehaviour
{
    [Header("Hitboxes (자식 Hitbox 컴포넌트 3개)")]
    public Hitbox[] hitboxes;        // 0,1,2 연결 (Collider2D+SetActive 갖는 스크립트)

    [Header("Timings per stage")]
    public float[] windUp = { 0.06f, 0.08f, 0.10f };   // 예비동작
    public float[] active = { 0.15f, 0.18f, 0.20f };   // 실제 타격창
    public float comboWindow = 0.6f;                   // 다음 입력 유효시간

    Animator anim;
    bool attacking = false;
    int stage = 0;              // 0,1,2 (= 1,2,3타)
    bool queued = false;        // 다음 타 대기

    void Awake() { anim = GetComponent<Animator>(); }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))        // 공격 입력(나중에 버튼으로 바꿔도 됨)
        {
            if (!attacking)
            {
                StartCoroutine(DoCombo());
            }
            else if (stage < 2)
            {
                queued = true;                  // 윈도 안에서 누르면 다음 타 예약
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
            // 1) 현재 스테이지 애니메이션 재생(트리거만)
            anim.ResetTrigger("AK1"); anim.ResetTrigger("AK2"); anim.ResetTrigger("AK3");
            anim.SetTrigger(stage == 0 ? "AK1" : stage == 1 ? "AK2" : "AK3");

            // 2) 히트박스 타이밍
            yield return new WaitForSeconds(windUp[stage]);
            SetOnly(stage, true);
            yield return new WaitForSeconds(active[stage]);
            SetAll(false);

            // 3) 다음 입력 기다리기(콤보 윈도)
            queued = false;
            windowTimer = comboWindow;
            while (windowTimer > 0f)
            {
                windowTimer -= Time.deltaTime;
                if (queued) break;             // 다음 타 입력 들어옴
                yield return null;
            }

            if (queued && stage < 2)
            {
                stage++;                       // 2/3타로 진행
                continue;
            }
            break;                             // 콤보 종료
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
