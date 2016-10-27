using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class TitleScreenEffect : MonoBehaviour
    {
        private float timer;
        private SpriteRenderer spriteRenderer;
        private Color prevColor;

        // Use this for initialization
        void Start()
        {
            spriteRenderer = this.GetComponent<SpriteRenderer>();
            prevColor = spriteRenderer.color;
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= 10f)
            {
                timer = 0.0f;
            }
            else
            {
                float alpha = timer / 10f;
                spriteRenderer.color = (new HSBColor(alpha, 1f, .4f)).ToColor();
            }
        }
    }
}
