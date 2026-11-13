using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab1;
    [SerializeField] GameObject playerPrefab2;
    [SerializeField] GameObject botPrefab;
    [SerializeField] Transform[] spawnPoints;

    void Awake()
    {
        if (GameManager.Instance.mode != Mode.PvP)
        {
            InputDevice[] devices = Gamepad.current != null
                ? new InputDevice[] { Keyboard.current, Gamepad.current }
                : new InputDevice[] { Keyboard.current };

            Spawn(playerPrefab1, 0, "Solo", devices);
            Instantiate(botPrefab, spawnPoints[1].position, Quaternion.identity);
        }
        else
        {
            bool pad = Gamepad.all.Count > 0;
            Spawn(playerPrefab1, 0, pad ? "KeyboardARROWS" : "KeyboardZQSD", Keyboard.current);
            Spawn(playerPrefab2, 1, pad ? "Gamepad" : "KeyboardARROWS",pad ? Gamepad.all[0] : Keyboard.current);
        }
    }

    void Spawn(GameObject prefab,int i, string scheme, params InputDevice[] devices)
    {
        var p = PlayerInput.Instantiate(prefab, i, scheme, -1, devices);
        p.transform.position = spawnPoints[i].position;
    }
}