using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityStandardAssets._2D
{
    //Singleton
    //To use methods or get properties, first type 'GameManager.Instance'
    public class GameManager : MonoBehaviour
    {

        //Variables: Data
        private static GameManager _instance;
        private Dictionary<string, float> colors;
        private string[] colorKeys;
        private string activeColor;
        private bool roomLoaded;

        //Variables: In The Scene
        private Camera mainCamera;
        private PlatformerCharacter2D player;
        private List<GameObject> enemies;

        //Properties
        public static GameManager Instance { get { return _instance; } }
        public Camera MainCamera { get { return mainCamera; } }
        public PlatformerCharacter2D Player { get { return player; } set { player = value; } }
        public List<GameObject> Enemies { get { return enemies; } }
        public bool RoomLoaded { get { return roomLoaded; } set { roomLoaded = value; } }
        public HSBColor ActiveColorHSB { get { return new HSBColor(colors[activeColor], 1.0f, 0.0f); } }

        //happens when created
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
            //Finding Game Objects in the scene.
            mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            player = GameObject.Find("Player").GetComponent<PlatformerCharacter2D>();
            enemies = new List<GameObject>();
            GameObject[] tempEnemies = GameObject.FindGameObjectsWithTag("enemy");
            for (int i = 0; i < tempEnemies.Length; i++)
            {
                enemies.Add(tempEnemies[i]);
            }

            //adding colors to dictionary
            colors = new Dictionary<string, float>();
            colors.Add("green", .333f);
            colors.Add("blue", .483f);
            colors.Add("red", .027f);
            colors.Add("yellow", .144f);
            colors.Add("purple", .811f);
            colors.Add("orange", .069f);
            //adding color names to array
            colorKeys = new string[6] { "red", "orange", "yellow", "green", "blue", "purple" };
            activeColor = "";
            roomLoaded = true;
            ChangeRoomColor(GameObject.Find("Level 01"), "red");
        }

        //Changes the visual color of the objects in the room.
        //Also influences room based on color.
        public void ChangeRoomColor(GameObject room, string color)
        {
            activeColor = color;
            switch (color)
            {
                case "red":
                    //TODO: Need Damage values
                    break;
                case "orange":
                    //TODO: Need Trump Sprite
                    break;
                case "yellow":
                    //TODO: Need pickup rate
                    break;
                case "green":
                    //TODO: Need enemy base class
                        break;
                case "blue":
                    //player.m_Rigidbody2D.gravityScale = .5f;
                    break;
                case "purple":
                    //TODO: Need health pickups
                    break;
                default:
                    //breaks out of method if color isn't in dictionary.
                    print("The string '" + color + "' is not in the dictionary.");
                    return;
            }

            //Changing the camera background color
            HSBColor camColor = HSBColor.FromColor(mainCamera.backgroundColor);
            camColor.h = colors[color];
            camColor.s = 1.0f;
            mainCamera.backgroundColor = camColor.ToColor();

            //Getting all instances of Sprite Renders in the room and changing their hue.
            SpriteRenderer[] children = room.GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < children.Length; i++)
            {
                HSBColor c = HSBColor.FromColor(children[i].color);
                c.h = colors[color];
                c.s = 1.0f;
                children[i].color = c.ToColor();
            }

            //Changing all enemy colors
            GameObject[] enemiesByTag = GameObject.FindGameObjectsWithTag("enemy");
            for (int i = 0; i < enemiesByTag.Length; i++)
            {
                Enemy e = (Enemy)enemiesByTag[i].GetComponent<Enemy>();
                SpriteRenderer enemySprite = e.EnemySprite;
                HSBColor c = HSBColor.FromColor(enemySprite.color);
                c.h = colors[color];
                c.s = 1.0f;
                enemySprite.color = c.ToColor();
            }
        }

        /// <summary>
        /// Returns the active color version of the input color.
        /// </summary>
        /// <param name="current">A Unity Color</param>
        /// <returns>The new colored version of the input color.</returns>
        public Color ChangeColor(Color current)
        {
            HSBColor newColor = HSBColor.FromColor(current);
            newColor.h = colors[activeColor];
            newColor.s = 1.0f;
            return newColor.ToColor();
        }

        //public Color LerpColor(Color currentColor, Color endColor, float alpha)
        //{
        //    HSBColor newColor = HSBColor.FromColor(currentColor);

        //}
    }
}