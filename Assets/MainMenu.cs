using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Toggle _isTriggerA;
    [SerializeField] private Toggle _isTriggerB;
    [SerializeField] private Toggle _hasRigidbodyA;
    [SerializeField] private Toggle _hasRigidbodyB;
    [SerializeField] private Toggle _isKinematicA;
    [SerializeField] private Toggle _isKinematicB;

    [SerializeField] private Text _labelA;
    [SerializeField] private Text _labelB;

    [SerializeField] private Text _collisionLabel;
    [SerializeField] private Text _triggerLabel;

    [SerializeField] private Text _movingA;
    [SerializeField] private Text _movingB;

    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;

    [SerializeField] private Image _colIcon;
    [SerializeField] private Image _trigIcon;

    [SerializeField] private ObjectsThrower _objectsThrower;

    private readonly List<string> _colliderTypes = new List<string> {
    "STATIC COLLIDER",
    "RIGIDBODY COLLIDER",
    "KINEMATIC RIGIDBODY COLLIDER",
    "STATIC TRIGGER COLLIDER",
    "RIGIDBODY TRIGGER COLLIDER",
    "KINEMATIC RIGIDBODY TRIGGER COLLIDER",
    };

    private readonly List<string> _movingTypes = new List<string> {
    "TRANSFORM.TRANSLATE()",
    "RIGIDBODY.ADDFORCE()",
    "RIGIDBODY.MOVEPOSITION()",
    "TRANSFORM.TRANSLATE()",
    "RIGIDBODY.ADDFORCE()",
    "RIGIDBODY.MOVEPOSITION()",
    };

    private readonly bool[,] _collisionMatrix = new bool[,] {
        { false, true, false, false, false, false } ,
        { true, true, true, false, false, false } ,
        { false, true, false, false, false, false } ,
        { false, false, false, false, false, false } ,
        { false, false, false, false, false, false } ,
        { false, false, false, false, false, false }
    };
    private readonly bool[,] _triggerMatrix = new bool[,] {
        { false, false, false, false, true, true } ,
        { false, false, false, true, true, true } ,
        { false, false, false, true, true, true } ,
        { false, true, true, false, true, true } ,
        { true, true, true, true, true, true } ,
        { true, true, true, true, true, true }
    };

    public void Throw()
    {
        _objectsThrower.ThrowObjects(_isTriggerA.isOn, _hasRigidbodyA.isOn, _isKinematicA.isOn, _isTriggerB.isOn, _hasRigidbodyB.isOn, _isKinematicB.isOn);
    }

    private void Update()
    {
        string labelA = "";
        if (!_hasRigidbodyA.isOn)
        {
            labelA += "STATIC";
        }
        else if (_isKinematicA.isOn)
        {
            labelA += "KINEMATIC RIGIDBODY";
        }
        else
        {
            labelA += "RIGIDBODY";
        }
        if (_isTriggerA.isOn)
        {
            labelA += " TRIGGER";
        }
        labelA += " COLLIDER";

        string labelB = "";
        if (!_hasRigidbodyB.isOn)
        {
            labelB += "STATIC";
        }
        else if (_isKinematicB.isOn)
        {
            labelB += "KINEMATIC RIGIDBODY";
        }
        else
        {
            labelB += "RIGIDBODY";
        }
        if (_isTriggerB.isOn)
        {
            labelB += " TRIGGER";
        }
        labelB += " COLLIDER";

        _labelA.text = labelA;
        _labelB.text = labelB;
        int aType = _colliderTypes.IndexOf(labelA);
        int bType = _colliderTypes.IndexOf(labelB);
        bool colEnabled = _collisionMatrix[aType, bType];
        bool trigEnabled = _triggerMatrix[aType, bType];
        _movingA.text = _movingTypes[aType];
        _movingB.text = _movingTypes[bType];
        _colIcon.sprite = colEnabled ? _onSprite : _offSprite;
        _trigIcon.sprite = trigEnabled ? _onSprite : _offSprite;
    }

    public void ResetLabelsColor()
    {
        _collisionLabel.color = Color.black;
        _triggerLabel.color = Color.black;
    }
}
