using FrikinCore.Score;
using UnityEngine;

namespace FrikinCore.Collectable
{
    public abstract class Collectable : MonoBehaviour
    {
        [SerializeField] ScoreHandler.ScoreValues scoreAmount;
        [SerializeField] TextMesh scoreNumberGameObject;
        [SerializeField] GameObject holderObject;

        SpriteRenderer spriteRenderer;
        Collider2D collectableCollider;

        public ScoreHandler.ScoreValues ScoreAmount => scoreAmount;

        void OnEnable()
        {
            SetEnableOnComponentsTo(true);
        }

        protected virtual void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            collectableCollider = GetComponent<Collider2D>();

            if (scoreNumberGameObject != null)
                scoreNumberGameObject.GetComponent<TextMesh>().text = ((int)scoreAmount).ToString();
        }

        void Start() => GameManager.CallOnSceneChange += OnSceneChange;
        void OnDisable() => GameManager.CallOnSceneChange -= OnSceneChange;
        void OnDestroy() => GameManager.CallOnSceneChange -= OnSceneChange;

        void OnSceneChange() => gameObject.SetActive(false);

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameTags.Player.ToString()))
            {
                OnPickUp();
            }
        }

        protected void AddScore()
        {
            ScoreHandler.AddScore(ScoreAmount);
            if (scoreNumberGameObject != null) ActivateChildNumber();
        }

        protected virtual void ActivateChildNumber()
        {
            scoreNumberGameObject.gameObject.SetActive(true);
            Invoke(nameof(DeactivateChildNumber), 1.5f);
        }

        protected virtual void DeactivateChildNumber()
        {
            scoreNumberGameObject.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        protected void SetEnableOnComponentsTo(bool value)
        {
            if (spriteRenderer != null) spriteRenderer.enabled = value;
            if (collectableCollider != null) collectableCollider.enabled = value;
            if (holderObject != null) holderObject.SetActive(value);
        }

        protected abstract void OnPickUp();
    }
}