using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_rock : Monster_normal
{
    ISlime_info slime_info;
    /// <summary>
    /// 施法目標
    /// </summary>
    Vector3 spell_target;
    Vector3 roll_direct;
    /// <summary>
    /// 擊飛力度
    /// </summary>
    float knock_away = 0;
    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
        if (skill_charge >= 5 && !is_spelling)
        {
            skill_charge = 0;
            is_spelling = true;
            spell_target = search();
            roll_direct = spell_target - transform.position;
            roll_direct.y = 0;
            roll_direct = roll_direct.normalized;
            StartCoroutine(monster_spell_rock(Time.time, 5));
        }
        if (is_spelling)
        {
            move(roll_direct * 5 + transform.position, roll_direct * 5 + transform.position, move_speed);
        }
    }

    /// <summary>
    /// 衝撞
    /// </summary>
    protected IEnumerator monster_spell_rock(float _start_time, float _effect_time)
    {
        yield return new WaitForSeconds(0.1f);
        if (Time.time - _start_time - _effect_time <= 0)
        {
            if (move_speed <= 20)
            {
                move_speed *= 1.1f;
                knock_away += 0.0025f;
            }

            StartCoroutine(monster_spell_rock(_start_time, _effect_time));
        }
        else
        {
            move_speed = 5;
            is_spelling = false;
            StopCoroutine(monster_spell_rock(_start_time, _effect_time));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (is_spelling)
        {
            if (collision.gameObject.tag == "PPAP")
            {
                collision.gameObject.GetComponent<Player>().get_attack(10, 0.25f, transform.position, "knock");
            }
            else if (collision.gameObject.tag == "Slime")
            {
                slime_info = collision.gameObject.GetComponent(typeof(ISlime_info)) as ISlime_info;
                slime_info.Get_attack(10, 0.25f, transform.position, "knock");
            }
        }
    }

}
