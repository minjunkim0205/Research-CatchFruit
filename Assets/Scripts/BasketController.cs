using UnityEngine;
using UnityEngine.InputSystem;

public class BasketController : MonoBehaviour
{
    public AudioClip appleSound;
    public AudioClip bananaSound;
    public AudioClip bombSound;

    AudioSource audioSrc;
    GameObject manager;

    void Start()
    {
        Application.targetFrameRate = 60;
        audioSrc = GetComponent<AudioSource>();
        manager = GameObject.Find("GameManager");
    }

    void Update()
    {

    }

    public void OnTouch(InputAction.CallbackContext context)
    {
        if (!manager.GetComponent<GameManager>().GetIsGameOver())
        {
            if (context.performed)
            {
                Vector2 touchPos = Pointer.current.position.ReadValue();
                Ray ray = Camera.main.ScreenPointToRay(touchPos);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    float x = Mathf.RoundToInt(hit.point.x);
                    float z = Mathf.RoundToInt(hit.point.z);
                    transform.position = new Vector3(x, 0, z);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Apple")
        {
            Debug.Log("Tag = Apple");
            audioSrc.PlayOneShot(appleSound);
            manager.GetComponent<GameManager>().GetApple();
        }
        else if (other.gameObject.tag == "Banana")
        {
            Debug.Log("Tag = Banana");
            audioSrc.PlayOneShot(bananaSound);
            manager.GetComponent<GameManager>().GetBanana();
        }
        else if (other.gameObject.tag == "Bomb")
        {
            Debug.Log("Tag = Bomb");
            audioSrc.PlayOneShot(bombSound);
            manager.GetComponent<GameManager>().GetBomb();
        }
        Destroy(other.gameObject);
    }
}
