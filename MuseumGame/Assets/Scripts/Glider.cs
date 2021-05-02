using UnityEngine;
//http://answers.unity.com/answers/1778449/view.html
public class Glider : MonoBehaviour
{
    /// <summary>
    /// The speed when falling
    /// </summary>
    [SerializeField]
    private float m_FallSpeed = 0f;


    /// <summary>
    /// 
    /// </summary>
    private Rigidbody2D m_Rigidbody2D = null;
    public bool IsGliding = false;

    // Awake is called before Start function
    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGliding && m_Rigidbody2D.velocity.y < 0f && Mathf.Abs(m_Rigidbody2D.velocity.y) > m_FallSpeed)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, Mathf.Sign(m_Rigidbody2D.velocity.y) * m_FallSpeed);
        }
    }

    public void StartGliding()
    {
        IsGliding = true;
    }

    public void StopGliding()
    {
        IsGliding = false;
    }

    public void ToggleGliding()
    {
        IsGliding = !IsGliding;
    }

    /// <summary>
    /// Flag to check if gliding
    /// </summary>
    ///
    //public bool IsGliding { get; set; } = false;

}