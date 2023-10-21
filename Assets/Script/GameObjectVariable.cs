using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectVar", menuName = "GMJP/GameObjectVariable")]
public class GameObjectVariable : ScriptableObject, ISerializationCallbackReceiver
{
    private GameObject _currentValue;

    public GameObject CurrentValue { get => _currentValue; set => _currentValue = value; }


    public void OnAfterDeserialize()
    {
        CurrentValue = null;
    }

    public void OnBeforeSerialize()
    {
        CurrentValue = null;
    }
}
