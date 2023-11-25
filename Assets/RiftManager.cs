using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Cinemachine;

public class RiftManager : MonoBehaviour
{
    private static RiftManager instance;

    public GameObject player;
    public CinemachineVirtualCamera cam;
    public GameObject camParent;
    private bool isReal = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static RiftManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("RiftManager instance is null.");
            }
            return instance;
        }
    }

    // Faders, first we will any ongoing fades, then we set the value (assuming it wa sunset) and then we fade!

    public void ToggleRift()
    {
        isReal = !isReal;
        cam.GetCinemachineComponent<CinemachineFramingTransposer>().ForceCameraPosition(cam.transform.position + new Vector3(0, 100 * (isReal ? 1 : -1), 0), cam.transform.rotation);
        player.transform.position = player.transform.position + new Vector3(0, 100 * (isReal ? 1 : -1), 0);

    }

}
