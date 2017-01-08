using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class BulletScroll : MonoBehaviour
{
    public GameObject m_leftButton;
    public GameObject m_rightButton;
    public GameObject m_bulletPrefab;
    public GameObject m_bulletsContainer;
    public float m_bulletSpacingH = 10.0f;

    public OnBulletSelectedEvent onBulletSelected { get; set; }
    [Serializable]
    public class OnBulletSelectedEvent : UnityEvent<int>
    {
    }

    private List<GameObject> m_bullets = new List<GameObject>();

    private int m_selectedBulletIndex = -1;

    public BulletScroll()
    {
        onBulletSelected = new OnBulletSelectedEvent();
    }

    // Use this for initialization
    void Start()
    {
    }
	
	// Update is called once per frame
	void Update()
    {
	}

    public void SetBulletsCount(int count)
    {
        if(m_bullets.Count != count)
        {
            if (count > m_bullets.Count)
            {
                CreateBullets(count - m_bullets.Count);
            }
            else
            {
                RemoveBullets(m_bullets.Count - count);
            }
            ArrangeElements();
        }
        SelectBullet(0);

        bool showBullets = m_bullets.Count > 1;
        SetBulletsVisible(showBullets);
    }

    private void SetBulletsVisible(bool showBullets)
    {
        m_leftButton.SetActive(showBullets);
        m_rightButton.SetActive(showBullets);
        m_bulletsContainer.SetActive(showBullets);
    }

    private void CreateBullets(int count)
    {
        float bulletX = GetNewBulletPosX();
        for (int i = 0; i < count; ++i)
        {
            GameObject bulletObject = GameObject.Instantiate(m_bulletPrefab, m_bulletsContainer.transform, false) as GameObject;
            bulletObject.transform.localPosition = new Vector3(bulletX, 0.0f, 0.0f);
            RectTransform rectTransform = bulletObject.transform as RectTransform;
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector2(bulletX, 0.0f);
                bulletX += rectTransform.rect.width + m_bulletSpacingH;
            }
            Button button = bulletObject.GetComponent<Button>();
            if(button)
            {
                button.onClick.AddListener(() =>
                {
                    OnBulletSelected(bulletObject);
                });
            }
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            if(bullet)
            {
                bullet.SetSelected(false);
            }

            m_bullets.Add(bulletObject);
        }
    }

    private float GetNewBulletPosX()
    {
        float bulletX = 0.0f;
        if (m_bullets.Count > 0)
        {
            GameObject bullet = m_bullets[m_bullets.Count - 1];
            bulletX = bullet.transform.localPosition.x;
            RectTransform rectTransform = bullet.transform as RectTransform;
            bulletX += rectTransform.rect.width + m_bulletSpacingH;
        }
        return bulletX;
    }

    private void RemoveBullets(int count)
    {
        count = Mathf.Min(m_bullets.Count, count);
        int startIndex = m_bullets.Count - count;
        for(int i = startIndex; i < m_bullets.Count; ++i)
        {
            GameObject bullet = m_bullets[i];
            GameObject.Destroy(bullet);
        }
        m_bullets.RemoveRange(startIndex, count);
    }

    private void ArrangeElements()
    {
        ArrangeBulletsContainer();
        ResizeRootContainer();
    }
    private void ArrangeBulletsContainer()
    {
        float totalBulletWidth = ComputeTotalBulletWidth();
        Vector3 pos = m_bulletsContainer.transform.localPosition;
        pos.x = -totalBulletWidth / 2.0f;
        m_bulletsContainer.transform.localPosition = pos;
    }

    private float ComputeTotalBulletWidth()
    {
        float totalBulletWidth = 0.0f;
        if (m_bullets.Count > 0)
        {
            float bulletWidth = (m_bullets[0].transform as RectTransform).rect.width;
            totalBulletWidth = bulletWidth * m_bullets.Count + m_bulletSpacingH * (m_bullets.Count - 1);
        }
        return totalBulletWidth;
    }

    private void ResizeRootContainer()
    {
        RectTransform containerTransform = (transform as RectTransform);
        Vector2 oldSizeDelta = containerTransform.sizeDelta;
        float buttonWidth = (m_leftButton.transform as RectTransform).rect.width;
        float totalBulletWidth = ComputeTotalBulletWidth();
        float newWidth = totalBulletWidth + 2 * (buttonWidth + m_bulletSpacingH);
        Vector2 newSizeDelta = new Vector2(newWidth, oldSizeDelta.y);
        containerTransform.sizeDelta = newSizeDelta;
    }

    private void SelectBullet(int index)
    {
        if(m_bullets.Count > 0 && (m_selectedBulletIndex != index))
        {
            ToggleBullet(m_selectedBulletIndex, false);
            ToggleBullet(index, true);
            m_selectedBulletIndex = index;
            onBulletSelected.Invoke(m_selectedBulletIndex);
        }
    }

    private void ToggleBullet(int index, bool isOn)
    {
        index = Mathf.Clamp(index, 0, (m_bullets.Count - 1));
        Bullet bullet = m_bullets[index].GetComponent<Bullet>();
        if (bullet)
        {
            bullet.SetSelected(isOn);
        }
    }

    private void OnBulletSelected(GameObject bullet)
    {
        if(m_bullets[m_selectedBulletIndex] != bullet)
        {
            int index = GetBulletIndex(bullet);
            if(index != -1)
            {
                SelectBullet(index);
            }
        }
    }

    private int GetBulletIndex(GameObject bullet)
    {
        int index = -1;
        for (int i = 0; i < m_bullets.Count; ++i)
        {
            if(m_bullets[i] == bullet)
            {
                index = i;
                break;
            }
        }
        return index;
    }

    public void GoToPrevious()
    {
        if(m_bullets.Count > 0)
        {
            int prevIndex = m_selectedBulletIndex - 1;
            if(prevIndex < 0)
            {
                prevIndex = m_bullets.Count - 1;
            }
            SelectBullet(prevIndex);
        }
    }

    public void GoToNext()
    {
        if (m_bullets.Count > 0)
        {
            int nextIndex = m_selectedBulletIndex + 1;
            if (nextIndex >= m_bullets.Count)
            {
                nextIndex = 0;
            }
            SelectBullet(nextIndex);
        }
    }
}
