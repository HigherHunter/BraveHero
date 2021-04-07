using UnityEngine;
using UnityEngine.UI;

//manages health bar display
[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{

    public GameObject uiPrefab;
    public Transform target;
    float visibleTime = 5f;
    float lastVisibleTime;

    Transform ui;
    Image healthSlider;
    Transform cam;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main.transform;

        //find canvas
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                //display health slider
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break;
            }
        }
        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    //if health has changed update ui
    void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            lastVisibleTime = Time.time;

            float healthPercent = currentHealth / (float)maxHealth;
            healthSlider.fillAmount = healthPercent;
            if (currentHealth <= 0)
            {
                Destroy(ui.gameObject);
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ui != null)
        {
            ui.position = target.position;
            ui.forward = -cam.forward;

            if (Time.time - lastVisibleTime > visibleTime)
            {
                ui.gameObject.SetActive(false);
            }
        }
    }
}
