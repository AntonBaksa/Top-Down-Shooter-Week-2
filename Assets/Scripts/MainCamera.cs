using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private GameObject Player;


    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position - transform.forward * 24.0f;
    }

    private void OnGUI()
    {
        Rect r1 = new Rect(10, 10, 600, 64);
        Rect r2 = new Rect(10, 40, 600, 64);
        Rect r3 = new Rect(10, 70, 600, 64);
        GUI.color = Color.black;

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 30;
        style.fontStyle = FontStyle.Bold;

        GUI.Label(r1, "WASD to Move", style);
        GUI.Label(r2, "Space to Jump", style);
        GUI.Label(r3, "LMB to Shoot", style);
    }
}
