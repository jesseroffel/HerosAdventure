using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeSpellUI : MonoBehaviour {
    public Image MagicSlot;
    public Image SpellImage;
    public Text SpellName;

    public bool MagicMode = false;

    private Color hidecolor = new Vector4(1, 1, 1, 0.5f);
    private Color showcolor = new Vector4(1, 1, 1, 1);
    
    void Start()
    {
        if (MagicMode) {
            SetMagicMode(true);
        } else
        {
            SetMagicMode(false);
        }
    }

    public void SetFill(float amount)
    {
        SpellImage.fillAmount = amount;
    }


    public void SetSpellImage(Sprite spellsprite)
    {
        SpellImage.sprite = spellsprite;
    }

    public void SetSpellName(string name)
    {
        SpellName.text = name;
    }

    public void SetMagicMode(bool state)
    { if (state) {
            MagicMode = true;
            MagicSlot.color = showcolor;
            SpellImage.color = showcolor;
        } else {
            MagicMode = false;
            MagicSlot.color = hidecolor;
            SpellImage.color = hidecolor;
        }
    }
}
