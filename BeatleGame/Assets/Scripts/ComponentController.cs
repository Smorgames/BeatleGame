using UnityEngine;

public class ComponentController : MonoBehaviour
{
    public static ComponentController instance;

    private void Awake()
    {
        instance = this;
    }

    private Moving _moving;

    private void Start()
    {
        _moving = GetComponent<Moving>();
    }

    public void SetMoving(bool enableOrDisable)
    {
        _moving.enabled = enableOrDisable;
    }
}
