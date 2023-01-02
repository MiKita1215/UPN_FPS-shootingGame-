using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    public void LoadLevel1()
    {
        LevelManager.instance.LoadLevel("Level1");
    }

    public void LoadLevel2()
    {
        LevelManager.instance.LoadLevel("Level2");
    }

    // Add more methods for additional levels as needed
}
