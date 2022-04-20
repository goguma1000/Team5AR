using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffectManager : MonoBehaviour
{
    Vector2 dir;
    SpriteRenderer sprite;

    private float moveSpeed = 0.1f;
    private float reduceSpeed = 1;
    private float transparentSpeed = 5f;
    private float minSize = 0.01f;
    private float maxSize = 0.03f;

    [SerializeField]
    public Color[] colors;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = colors[Random.Range(0, colors.Length)];

        dir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector2(size, size);
        Debug.Log("size" + size);
    }

    // Update is called once per frame
    void Update()
    {
       //transform.Translate(dir * moveSpeed);

        // Size down to 0
        transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, Time.deltaTime * reduceSpeed);

        
        // Make transparent
        Color color = sprite.color;
        color.a = Mathf.Lerp(sprite.color.a, 0, Time.deltaTime * transparentSpeed);
        Debug.Log("color" + color.a);
        sprite.color = color;

        if (sprite.color.a <= 0.01f)
           Destroy(gameObject);
    }
}
