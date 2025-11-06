using UnityEngine;
using UnityEngine.UI;

public class win_level : MonoBehaviour
{
    public Image WinImage;
    public bool isAppear = false;
    private bool isHouse = false;
    public float displayTime = 5f;

    // 🔹 Посилання на Player
    private Player player;

    void Start()
    {
        // 🔹 Шукаємо об’єкт Player у сцені
        player = FindObjectOfType<Player>();

        // Перевірка, щоб уникнути помилок
        if (player == null)
        {
            Debug.LogError("❌ Player не знайдено у сцені!");
            return;
        }
    }

    void Update()
    {
        // 🔹 Умови для виграшу (перевіряються кожен кадр)
        if (player.firstTask && isHouse)
        {
            if (!isAppear)
            {
                isAppear = true;
                Appeare();

                if (WinImage != null)
                    Invoke(nameof(Hide), displayTime);
            }
        }
    }

    void Appeare()
    {
        WinImage.gameObject.SetActive(true);
        Debug.Log("✅ Виграшний екран з’явився!");
    }

    void Hide()
    {
        WinImage.gameObject.SetActive(false);
        Debug.Log("💤 Екран зник після затримки.");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("finish_lvl"))
        {
            isHouse = true;
            Debug.Log("🏠 Гравець увійшов у дім!");
        }
    }
}
