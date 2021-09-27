using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject menuController;
    [SerializeField] private List<GameObject> ObstaclePlanes;
    [SerializeField] private List<GameObject> created_Planes;
    [SerializeField] private GameObject noObstaclePlanes;
    [SerializeField] private new Camera camera;
    [SerializeField] private GameObject LevelEnd;
    [SerializeField] private GameObject LevelStart;
    [SerializeField] private GameObject Environment;
    [SerializeField] private List<GameObject> created_Environment;
    [SerializeField] private int number_Of_Sphere_Per_Blocks;

    [SerializeField] private List<int> SavePlanes;
    [SerializeField] private List<int> LoadPlanes;
    [SerializeField] private List<int> SaveEnvironmentPosition;
    [SerializeField] private List<int> LoadEnvironmentPosition;

    private int NumberLevel = 1;
    private int random_plane;
    private float planePosition;
    private int EnvironmentPosition;
    public GameObject character;
    private int number_Of_Sphere_To_Win;

    [Header("Setting")]
    private int minNumberPlanes = 4;
    private int maxNumberPlanes = 6;






    void Start()
    {

    }


    void Update()
    {

    }

    public void StartGame()
    {
        character.GetComponent<PlayerController>().Alive();       
    }

    public void СreateLevel()
    {
        if (PlayerPrefs.HasKey("Plane_0"))
        {
            LoadLevel();
            Level_Builder(LoadPlanes.Count, LoadPlanes, LoadEnvironmentPosition);
        }
        else
        {
            RandomLevel();
        }
    }
    public void Again()
    {
        LoadLevel();
        Level_Builder(LoadPlanes.Count, LoadPlanes, LoadEnvironmentPosition);
        character.GetComponent<PlayerController>().FalseDeathAnivation();
    }

    public void NextLevel()
    {
        RandomLevel();
        character.GetComponent<PlayerController>().FalseWinAnivation();
        NumberLevel += 1;
        menuController.GetComponent<MenuController>().NumberLevel(NumberLevel);
        PlayerPrefs.SetInt("NumberLevel", NumberLevel);
    }




    public void RandomLevel()
    {
        int randome = Random.Range(minNumberPlanes, maxNumberPlanes + 1);
        List<int> randomeList = new List<int>();
        for (int i = 0; i < randome; i++)
        {
            randomeList.Add(Random.Range(0, ObstaclePlanes.Count));
        }

        List<int> randomeEnvironmentPosition = new List<int>();
        randomeEnvironmentPosition.Add(Random.Range(-600, 300));
        randomeEnvironmentPosition.Add(Random.Range(1400, 500));

        Level_Builder(randome, randomeList, randomeEnvironmentPosition);
    }

    public void Level_Builder(int number_Of_Level_Blocks, List<int> planes, List<int> randomeEnvironmentPosition)
    {
        ReloadLevel();
        planePosition = -7;
        number_Of_Sphere_To_Win = number_Of_Level_Blocks * number_Of_Sphere_Per_Blocks;
        for (int i = 0; i <= number_Of_Level_Blocks; i++)
        {
            if (i == 0)
            {
                planePosition += LevelStart.transform.localScale.z * 3;
                created_Planes.Add(Instantiate(LevelStart, new Vector3(0, 0, planePosition), Quaternion.identity));
            }
            if (i != 0)
            {
                random_plane = planes[i - 1];
                SavePlanes.Add(random_plane);
                planePosition += ObstaclePlanes[random_plane].transform.localScale.z * 5 + created_Planes[created_Planes.Count - 1].transform.localScale.z * 5;
                created_Planes.Add(Instantiate(ObstaclePlanes[random_plane], new Vector3(0, 0, planePosition), Quaternion.identity));
                if (i != number_Of_Level_Blocks)
                {
                    planePosition += noObstaclePlanes.transform.localScale.z * 5 + created_Planes[created_Planes.Count - 1].transform.localScale.z * 5;
                    created_Planes.Add(Instantiate(noObstaclePlanes, new Vector3(0, 0, planePosition), Quaternion.identity));
                }
            }

        }
        planePosition += created_Planes[created_Planes.Count - 1].transform.localScale.z * 5 + LevelEnd.transform.localScale.z * 5;
        created_Planes.Add(Instantiate(LevelEnd, new Vector3(0, 0, planePosition), Quaternion.identity));

        EnvironmentPosition = randomeEnvironmentPosition[0];
        SaveEnvironmentPosition.Add(EnvironmentPosition);
        created_Environment.Add(Instantiate(Environment, new Vector3(-100, -40, EnvironmentPosition), Quaternion.identity));
        EnvironmentPosition = randomeEnvironmentPosition[1];
        SaveEnvironmentPosition.Add(EnvironmentPosition);
        created_Environment.Add(Instantiate(Environment, new Vector3(100, -40, EnvironmentPosition), Quaternion.identity));
        created_Environment[created_Environment.Count - 1].transform.eulerAngles = new Vector3(0, 180, 0);

        SaveLevel();
    }



    public void CollisionWall()
    {
        if (character.GetComponent<PlayerController>().sphere < number_Of_Sphere_To_Win)
        {
            character.GetComponent<PlayerController>().Death();
            menuController.GetComponent<MenuController>().Again();          
        }
    }

    void ReloadLevel()
    {
        DestroyLevel();
        SavePlanes.Clear();
        SaveEnvironmentPosition.Clear();
        character.GetComponent<PlayerController>().ReloadPlayer();


    }
    public void LevelComplete()
    {
        character.GetComponent<PlayerController>().Win();
        menuController.GetComponent<MenuController>().NextLevel();
    }

    public void DestroyLevel()
    {
        for (int i = 0; i < created_Planes.Count; i++)
        {
            Destroy(created_Planes[i]);
        }
        created_Planes.Clear();

        for (int i = 0; i < created_Environment.Count; i++)
        {
            Destroy(created_Environment[i]);
        }
        created_Environment.Clear();
    }

    void SaveLevel()
    {

        PlayerPrefs.SetInt("SavePlanes", SavePlanes.Count);
        for (int i = 0; i < SavePlanes.Count; i++)
        {
            PlayerPrefs.SetInt("Plane_" + i, SavePlanes[i]);
        }

        PlayerPrefs.SetInt("SaveEnvironmentPosition", SaveEnvironmentPosition.Count);
        for (int i = 0; i < SaveEnvironmentPosition.Count; i++)
        {
            PlayerPrefs.SetInt("EnvironmentPosition_" + i, SaveEnvironmentPosition[i]);
        }
        PlayerPrefs.SetInt("NumberLevel", NumberLevel);

    }

    public void LoadLevel()
    {

        if (PlayerPrefs.HasKey("Plane_0"))
        {
            LoadPlanes.Clear();
            int number = PlayerPrefs.GetInt("SavePlanes");
            for (int i = 0; i < number; i++)
            {
                LoadPlanes.Add(PlayerPrefs.GetInt("Plane_" + i));
            }

            LoadEnvironmentPosition.Clear();
            int _number = PlayerPrefs.GetInt("SaveEnvironmentPosition");
            for (int i = 0; i < _number; i++)
            {
                LoadEnvironmentPosition.Add(PlayerPrefs.GetInt("EnvironmentPosition_" + i));
            }
        }
        NumberLevel = PlayerPrefs.GetInt("NumberLevel");
        menuController.GetComponent<MenuController>().NumberLevel(NumberLevel);
    }
}
