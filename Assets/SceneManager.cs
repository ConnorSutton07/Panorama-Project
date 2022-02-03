using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] Button btnExplore;

    public static Dictionary<string, int> sceneDict = new Dictionary<string, int>
    {
        { "Arctic", 0 },
        { "Map", 1 }
    };

    // Start is called before the first frame update
    void Start()
    {
        btnExplore.GetComponent<Button>().onClick.AddListener(Explore);
    }

    // Update is called once per frame
    void Explore()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneDict["Arctic"]);
    }
}
