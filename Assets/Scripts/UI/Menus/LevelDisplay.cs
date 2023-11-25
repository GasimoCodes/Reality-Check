using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    public LevelData[] levels;
    public Transform levelsUIContainer;
    public GameObject LevelDisplayObject;

    /// <summary>
    /// This shows the player all the levels they can play
    /// </summary>
    private void OnEnable()
    {
        // Remove all levels if present
        for (int i = 0; i < levelsUIContainer.childCount; i++)
        {
            Destroy(levelsUIContainer.transform.GetChild(i).gameObject);
        }

        // Load levels
        foreach (var level in levels)
        {
            //TODO:  Show level unloacked status

            GameObject lvl = Instantiate(LevelDisplayObject, levelsUIContainer);
            lvl.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = level.LevelName;
            lvl.GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene(level.SceneName);
            });
        }
    }
}
