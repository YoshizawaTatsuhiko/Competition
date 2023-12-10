using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSystem : MonoBehaviour
{
    [SerializeField] private KeyCode _key = KeyCode.Return;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // R-Key�����͂����x�ɁA�����V�[�����ǂݍ��܂��B
        if (Input.GetKeyDown(_key))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
