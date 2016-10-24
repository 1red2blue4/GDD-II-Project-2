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
        private bool movingBetweenRooms = false;

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
        public string ActiveColor { get { return activeColor; } set { activeColor = value; } }
        public HSBColor ActiveColorHSB { get { return new HSBColor(colors[activeColor], 1.0f, 1.0f); } }
        public bool MovingBetweenRooms { get { return movingBetweenRooms; } set { movingBetweenRooms = value; } }

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
            activeColor = "red";
            roomLoaded = true;
            ChangeRoomColor(GameObject.Find("EntireLevel"));
            player.playerSprite.color = ChangeColor(player.BaseColor);
        }

        //Changes the visual color of the objects in the room.
        //Also influences room based on color.
        public void ChangeRoomColor(GameObject room)
        {
            //Reset room
            ResetStats();
            //Selecting a random color
            string color = colorKeys[Random.Range(0, 6)];
            while(color == activeColor)
            {
                color = colorKeys[Random.Range(0, 6)];
            }
            activeColor = color;
            //get all enemies
            GameObject[] enemiesByTag = GameObject.FindGameObjectsWithTag("enemy");

            //changing stats based on color.
            switch (color)
            {
                case "red":
                    player.damage = 2;
                    for (int i = 0; i < enemiesByTag.Length; i++)
                    {
                        if (enemiesByTag[i].name != "Collider")
                        {
                            Enemy e = (Enemy)enemiesByTag[i].GetComponent<Enemy>();
                            e.damage = 2;
                        }
                    }
                    break;
                case "orange":
                    //TODO: Need Trump Sprite
                    break;
                case "yellow":
                    //TODO: Need pickup rate
                    break;
                case "green":
                    player.defaultMaxSpeed = player.MaxSpeed;
                    player.MaxSpeed = 20f;
                    for (int i = 0; i < enemiesByTag.Length; i++)
                    {
                        if (enemiesByTag[i].name != "Collider")
                        {
                            Enemy e = (Enemy)enemiesByTag[i].GetComponent<Enemy>();
                            e.defaultMoveSpeed = e.moveSpeed;
                            e.moveSpeed = 20f;
                        }
                    }
                        break;
                case "blue":
                    player.m_Rigidbody2D.gravityScale = .5f;
                    break;
                case "purple":
                    //TODO: Need health pickups
                    break;
                default:
                    //breaks out of method if color isn't in dictionary.
                    print("The string '" + color + "' is not in the dictionary.");
                    return;
            }
            player.playerSprite.color = ChangeColor(player.playerSprite.color);

            //Changing the camera background color
            HSBColor camColor = HSBColor.FromColor(mainCamera.backgroundColor);
            camColor.h = colors[color];
            camColor.s = 1.0f;
            mainCamera.backgroundColor = camColor.ToColor();

            //Getting all instances of Sprite Renders in the room and changing their hue.
            SpriteRenderer[] children = room.GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < children.Length; i++)
            {
                if(children[i].tag != "item")
                {
                    HSBColor c = HSBColor.FromColor(children[i].color);
                    c.h = colors[color];
                    c.s = 1.0f;
                    children[i].color = c.ToColor();
                }
            }

            //Changing all enemy colors
            for (int i = 0; i < enemiesByTag.Length; i++)
            {
                if(enemiesByTag[i].name != "Collider")
                {
                    Enemy e = (Enemy)enemiesByTag[i].GetComponent<Enemy>();
                    SpriteRenderer enemySprite = e.EnemySprite;
                    HSBColor c = HSBColor.FromColor(enemySprite.color);
                    c.h = colors[color];
                    c.s = 1.0f;
                    enemySprite.color = c.ToColor();
                }
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
            newColor.a = current.a;
            return newColor.ToColor();
        }

        public Color LerpColor(Color endColor, float alpha)
        {
            HSBColor endColorHSB = HSBColor.FromColor(endColor);
            endColorHSB.s = 1.0f;
            endColorHSB.h = colors[activeColor];
            HSBColor newColor = HSBColor.FromColor(endColor);
            newColor.b -= .6f;
            newColor.s = 1.0f;
            newColor.h = colors[activeColor];
            return HSBColor.Lerp(newColor, endColorHSB, alpha).ToColor();
        }

        public void ResetStats()
        {
            GameObject[] enemiesByTag = GameObject.FindGameObjectsWithTag("enemy");
            switch (activeColor)
            {
                case "red":
                    player.damage = 1;
                    for (int i = 0; i < enemiesByTag.Length; i++)
                    {
                        if (enemiesByTag[i].name != "Collider")
                        {
                            Enemy e = (Enemy)enemiesByTag[i].GetComponent<Enemy>();
                            e.damage = 1;
                        }
                    }
                    break;
                case "orange":
                    //TODO: Need Trump Sprite
                    break;
                case "yellow":
                    //TODO: Need pickup rate
                    break;
                case "green":
                    player.MaxSpeed = player.defaultMaxSpeed;
                    for (int i = 0; i < enemiesByTag.Length; i++)
                    {
                        if (enemiesByTag[i].name != "Collider")
                        {
                            Enemy e = (Enemy)enemiesByTag[i].GetComponent<Enemy>();
                            e.moveSpeed = e.defaultMoveSpeed;
                        }
                    }
                    break;
                case "blue":
                    //player.m_Rigidbody2D.gravityScale = .5f;
                    break;
                case "purple":
                    //TODO: Need health pickups
                    break;
                default:
                    //breaks out of method if color isn't in dictionary.
                    return;
            }
        }
    }
}