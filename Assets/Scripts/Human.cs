using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Human : Event
{
    [SerializeField] private Animation leftLeg, rightLeg;

    private IEnumerator Start()
    {
        leftLeg.Play();
        yield return new WaitForSeconds(leftLeg.clip.length / 2);
        rightLeg.Play();
    }

    public override void Activate(Player player)
    {
        var playerTransform = player.transform;
        const float r = 30;
        float theta = Random.value * 2 * Mathf.PI;
        float x = playerTransform.position.x + r * Mathf.Cos(theta);
        float y = playerTransform.position.y + r * Mathf.Sin(theta);
        transform.position = new Vector3(x, y, 0);
        Vector2 dir = transform.position - playerTransform.position;
        transform.position = new Vector3(x + Random.Range(-3, 3f), y + Random.Range(-3, 3f), 0);

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x) + 90);
        StartCoroutine(Move(dir));
    }

    private IEnumerator Move(Vector2 dir)
    {
        Vector2 targetPos = (Vector2)transform.position + -dir * 15;

        float speed = 10f;
        var step =  speed * Time.deltaTime;
        while (Vector2.Distance(transform.position, targetPos) > 4f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
            yield return null;
        }
        
        Destroy(gameObject);
        yield return null;
    }
}
