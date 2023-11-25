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

    public AudioSource ambienceReal;
    public AudioSource ambienceFake;

    public float ambianceVolume = 0.3f;

    private void Awake()
    {
        ambienceFake.volume = 0.0f;

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
        
        ambienceReal.volume = isReal ? ambianceVolume : 0.0f;
        ambienceFake.volume = isReal ? 0.0f : ambianceVolume;

        cam.GetCinemachineComponent<CinemachineFramingTransposer>().ForceCameraPosition(cam.transform.position + new Vector3(0, 100 * (isReal ? 1 : -1), 0), cam.transform.rotation);
        player.transform.position = player.transform.position + new Vector3(0, 100 * (isReal ? 1 : -1), 0);

    }

}
