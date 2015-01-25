using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour
{
    [SerializeField]
    int _levelIndex = 0;

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            Application.LoadLevel(_levelIndex);
        }
    }
}
