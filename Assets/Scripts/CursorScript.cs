using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour
{
    public GameObject QuestUI;
    // Use this for initialization
    void Start()
    {
        LockCursor();
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        QuestUI.SetActive(false);
    }
}
