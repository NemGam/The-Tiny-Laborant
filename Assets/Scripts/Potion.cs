using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Event
{
    [SerializeField] private GameObject splash;
    [SerializeField] private AudioSource shatterSound;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public override void Activate(Player player)
    {
        var playerTransform = player.transform;
        const float r = 30;
        float theta = Random.value * 2 * Mathf.PI;
        float x = playerTransform.position.x + Random.value * r * Mathf.Cos(theta);
        float y = playerTransform.position.y + Random.value * r * Mathf.Sin(theta);
        transform.position = new Vector3(x, y, 0);

        transform.localScale = new Vector3(5, 5, 5);
        StartCoroutine(Fall());
    }
    
    private IEnumerator Fall()
    {

        float time = 2f;
        float timer = 0;
        while (timer < time)
        {
            transform.localScale = Vector3.Lerp(new Vector3(5, 5, 5), Vector3.one, timer / time);
            timer += Time.deltaTime;
            yield return null;
        }

        Instantiate(splash, transform.position, Quaternion.identity);
        // shatterSound.pitch = Random.Range(0.6f, 1.6f);
        // shatterSound.Play();
        spriteRenderer.enabled = false;
        Destroy(gameObject, 1f);
        yield return null;
    }
}
