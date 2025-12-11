using Scripts.Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Items;
using Unity.Jobs;

public class SliderMinigame : MonoBehaviour
{

    [SerializeField] private Slider _mainSlider;
    [SerializeField] private GameObject _sliderFiller;
    [SerializeField] private GameObject _fillArea;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private RectTransform minLocation, maxLocation;
    [SerializeField] private TextMeshProUGUI _qualityText;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip[] sfx;
    [SerializeField] private AudioSource cookingLooped;
    [SerializeField] private AudioSource sfxPlayer;

    public static bool hasCooked;

    [SerializeField] private float timeToComplete;
    private float maxTime;

    [SerializeField] private int score;
    [SerializeField] private int attempts;
    private int maxAttempts;

    [SerializeField] private float speed;
    [SerializeField] private float arrowSpeed;
    private float originalSpeed;
    private float originalArrowSpeed;

    [SerializeField] private bool flip1;
    [SerializeField] private bool flip2;

    private RectTransform fillArea, arrow;
    private Rigidbody2D _player;

    private BaseItem[] possibleRecipies;
    //have a section for the possible recipies

    private double timeShort;

    private void Awake()
    {
        hasCooked = false;

        if (player == null)
        {
            GameObject.FindWithTag("Player");
            _player = player.GetComponent<Rigidbody2D>();
        }
        _player = player.GetComponent<Rigidbody2D>();
        _fillArea.SetActive(true);
        _arrow.SetActive(true);
        arrow = _arrow.GetComponent<RectTransform>();
        fillArea = _fillArea.GetComponent<RectTransform>();
        _qualityText.text = "Press Space at the right time";
        maxAttempts = attempts;
        score = 0;
        maxTime = timeToComplete;
        originalSpeed = speed;
        originalArrowSpeed = arrowSpeed;
        cookingLooped.Play();
    }

    void Update()
    {

        if (hasCooked)
        {
            attempts = maxAttempts;
            timeToComplete = maxTime;
            hasCooked = false;
            score = 0;
            _qualityText.text = "Press Space at the right time";
            cookingLooped.Play();
            _fillArea.SetActive(true);
            _arrow.SetActive(true);
            speed = originalSpeed;
            arrowSpeed = originalArrowSpeed;
            Vector2 currentScale = fillArea.localScale;
            currentScale.x = 1;
            fillArea.localScale = currentScale;
            _sliderFiller.SetActive(false);
            _mainSlider.value = 0;
        }

        //InventoryManager.Instance.RemoveItem(HighlightInventory.InventorySlotObject.GetComponentInChildren<InventoryItem>(), 1);
        //this will take the plant/food item away from the player

        if (arrow.localPosition.x < minLocation.localPosition.x)
        {
            flip1 = true;
            
        }
        else if (arrow.localPosition.x > maxLocation.localPosition.x)
        {
            flip1 = false;
            
        }

        if (flip1)
        {
            arrow.localPosition = new Vector2(arrow.localPosition.x + arrowSpeed * Time.deltaTime, arrow.localPosition.y);
        }
        else
        {
            arrow.localPosition = new Vector2(arrow.localPosition.x - arrowSpeed * Time.deltaTime, arrow.localPosition.y);
        }

        if (fillArea.localPosition.x < -36)
        {
            flip2 = true;
        }
        else if (fillArea.localPosition.x > 36)
        {
            flip2 = false;
        }

        if (flip2)
        {
            fillArea.localPosition = new Vector2(fillArea.localPosition.x + speed * Time.deltaTime, fillArea.localPosition.y);
        }
        else
        {
            fillArea.localPosition = new Vector2(fillArea.localPosition.x - speed * Time.deltaTime, fillArea.localPosition.y);
        }

        if (attempts > 0 && attempts < maxAttempts)
        {
            timeToComplete -= Time.deltaTime;
            timeShort = (int)timeToComplete;
            _qualityText.text = timeShort.ToString();
            //_qualityText.text = timeToComplete.ToString();
        }

        if (Input.GetKeyDown(KeyCode.Space) && attempts > 0)
        {
            _player.linearVelocity = Vector2.zero;
            if (ArrowDetect.isThere)
            {
                score++;
                attempts--;
                Vector2 currentScale = fillArea.localScale;
                currentScale.x *= 0.85f;
                fillArea.localScale = currentScale;
                speed *= 1.30f;
                arrowSpeed *= 1.30f;
                PlayRightSound();
            }
            else
            {
                attempts--;
                PlayWrongSound();
            }
        }

        if (attempts <= 0)
        {
            timeToComplete -= Time.deltaTime;
            timeShort = (int)timeToComplete;
            _qualityText.text = "Press space FAST" + "\n" + timeShort.ToString();

            //start the other sequence

            if (!_sliderFiller.activeSelf)
            {
                _sliderFiller.SetActive(true);
                _fillArea.SetActive(false);
                _arrow.SetActive(false);

            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _mainSlider.value += 0.04f;
                PlayClickSound();
            }

            if (_mainSlider.value > 0.99f)
            {
                //wohoo you completed the cooking minigame
                _sliderFiller.SetActive(false);
                gameObject.SetActive(false);
                DialogueManager.FreezePlayer = false;
                cookingLooped.Stop();
                hasCooked = true;
            }

        }

    }

    private void PlayRightSound()
    {
        sfxPlayer.PlayOneShot(sfx[0]);
    }

    private void PlayWrongSound()
    {
        sfxPlayer.PlayOneShot(sfx[1]);
    }
    private void PlayClickSound()
    {
        sfxPlayer.PlayOneShot(sfx[2]);
    }
    private void PlayCookedSound()
    {
        sfxPlayer.PlayOneShot(sfx[3]);
    }



}
