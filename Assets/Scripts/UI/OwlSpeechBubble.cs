using UnityEngine;
using System.Collections;

public class OwlSpeechBubble : MonoBehaviour
{
    public Animator m_speechBubbleAnimator;

    private CategoryGridTile.CategoryGridTileType m_tileType = CategoryGridTile.CategoryGridTileType.Invalid;

    public void ShowSpeechBubbleForCategory(CategoryGridTile.CategoryGridTileType tileType)
    {
        if(m_tileType != tileType)
        {
            m_tileType = tileType;
            switch (tileType)
            {
                case CategoryGridTile.CategoryGridTileType.UserInterface:
                    m_speechBubbleAnimator.SetTrigger("TriggerApps");
                    break;
                case CategoryGridTile.CategoryGridTileType.Decals:
                    m_speechBubbleAnimator.SetTrigger("TriggerDecals");
                    break;
                case CategoryGridTile.CategoryGridTileType.PersonalWork:
                    m_speechBubbleAnimator.SetTrigger("TriggerPersonalWork");
                    break;
            }
        }
    }

    public void HideSpeechBubble()
    {
        m_speechBubbleAnimator.SetTrigger("TriggerReset");
        m_tileType = CategoryGridTile.CategoryGridTileType.Invalid;
    }

    public void ShowSpeechBubbleChooseCategory()
    {
        m_speechBubbleAnimator.SetTrigger("TriggerChooseCategory");
        m_tileType = CategoryGridTile.CategoryGridTileType.Invalid;
    }
}
