using UnityEngine;
[RequireComponent(typeof(Card))]
public class CardMover : MonoBehaviour
{
    private bool _isMoving;
    private Vector3 _startPosition;
    private Vector3 _startScale;
    private Card _card;
    [SerializeField] private float _selectedScaleFactor = 0.6f;
    [SerializeField] private bool _usedInTable = false;
    [SerializeField] private GameObjectVariable _houseSelected;
    private CardDeck _deck;
    private bool _hasStup;

    private void Awake()
    {
        _card = GetComponent<Card>();


    }
    private void Start()
    {
        if (!_hasStup)
        {
            _startPosition = transform.position;
            _startScale = transform.localScale;
            _hasStup = true;
        }
    }

    private void OnEnable()
    {
        if (!_hasStup)
        {
            _startPosition = transform.position;
            _startScale = transform.localScale;
            _hasStup = true;
        }
        ResetPostion();
        _usedInTable = false;
        _isMoving = false;
    }
    public void SetInitialPosition(Vector3 position)
    {
        _startPosition = position;
    }
    public void SetDeck(CardDeck deck)
    {
        _deck = deck;
    }
    private void Update()
    {
        if (_isMoving && !_usedInTable && PaxTurnManager.Instance.isMyTurn())
        {
            Vector3 newPosition = Input.mousePosition;
            newPosition.z = transform.position.z;
            transform.position = newPosition;

            // Verifica se o bot�o do mouse foi solto para parar o movimento
            if (Input.GetMouseButtonUp(0))
            {
                _isMoving = false;
                if (_houseSelected.CurrentValue == null)
                {
                    ResetPostion();
                }
                else if (_houseSelected.CurrentValue.TryGetComponent(out HouseManager house))
                {
                    if (_deck.Deck.Contains(house.CurrentCard))
                    {
                        ResetPostion();
                    }
                    else
                    {
                        _usedInTable = true;
                        _card.OnCardUsed.Invoke(_card.cardData, house.gameObject);
                        _card.gameObject.SetActive(false);
                        transform.position = _startPosition;
                    }

                }
            }
        }
    }

    private void ResetPostion()
    {
        transform.position = _startPosition;
        transform.localScale = new Vector3(1.2f,1.2f,1.2f);
    }

    public void OnMouseEnterUI()
    {
        _card.OnCardHovered.Invoke(_card);

    }

    public void OnMouseExitUI()
    {

    }
    public void OnMouseDownUI()
    {
        if (!PaxTurnManager.Instance.isMyTurn()) return;
        transform.localScale = _startScale * _selectedScaleFactor;
        _isMoving = true;
    }
}
