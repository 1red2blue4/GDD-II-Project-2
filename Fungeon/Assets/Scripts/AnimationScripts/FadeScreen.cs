using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class FadeScreen : MonoBehaviour
    {
        private bool lerping = false;
        private Color prevColor;
        private float timer;
        private SpriteRenderer spriteRenderer;
        public DoorScripty door;

        private void Awake()
        {
            timer = 0.0f;
            spriteRenderer = this.GetComponent<SpriteRenderer>();
            prevColor = this.GetComponent<SpriteRenderer>().color;
        }

        public void Update()
        {
            if (lerping)
            {
                timer += Time.deltaTime;
                if(timer >= .49f)
                {
                    timer = 0.0f;
                    lerping = false;
                }
                else
                {
                    float alpha = timer / .49f;
                    HSBColor endColor = GameManager.Instance.ActiveColorHSB;
                    endColor.b = .5f;
                    HSBColor beginColor = HSBColor.FromColor(this.prevColor);
                    beginColor.b = .5f;
                    spriteRenderer.color = HSBColor.Lerp(beginColor, endColor, alpha).ToColor();
                }
            }
        }

        public void StartFade(){
            spriteRenderer.color = GameManager.Instance.ChangeColor(spriteRenderer.color);
        }

        public void BeginLerp()
        {
            GameManager.Instance.ChangeRoomColor(GameObject.Find("New_Entire_Level"));
            lerping = true;
            this.prevColor = spriteRenderer.color;
            int side = door.side;
            Collider2D other = door.otherCollision;
            GameObject target = door.target;
            if (side == 0)
            {
                other.gameObject.transform.position = new Vector2(target.transform.position.x + 2, target.transform.position.y + 2);
            }
            if (side == 1)
            {
                other.gameObject.transform.position = new Vector2(target.transform.position.x - 2, target.transform.position.y + 2);
            }
            if (side == 2)
            {
                other.gameObject.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 0.5f);
            }
            if (side == 3)
            {
                other.gameObject.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 2);
            }
            GameManager.Instance.MainCamera.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, GameManager.Instance.MainCamera.transform.position.z);
        }

        public void DestroySelf()
        {
            GameManager.Instance.MovingBetweenRooms = false;
            Destroy(this.gameObject);
        }
    }
}