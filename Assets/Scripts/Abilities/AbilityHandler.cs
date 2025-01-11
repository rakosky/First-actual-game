using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    public Ability[] abilities;

    void Update()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                abilities[i].Use(gameObject);
            }
        }
    }
}
