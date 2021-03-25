using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SliderMode
{

    Percent,
    Fraction,
    Download

}

public class UILoadingSliderView : MonoBehaviour
{


    // Use this for initialization

    public Text Title;
    public Text Text;
    public Slider Slider;
    public Action OnSliderShowed;
    public Action OnSliderHided;
    public Action OnComplete;
    SliderMode mode;
    Vector2 ShowSize = new Vector2(600f, 30f);
    Vector2 HideSize = new Vector2(-20f, 30f);
    long curent;
    long total;
    float curProgressValue;

    public float CurProgressValue
    {
        get
        {
            return curProgressValue;
        }
        set
        {
            Slider.value = value < 1 ? value : 1;
            curProgressValue = value;

            if (curent >= total)
            {
                OnComplete?.Invoke();
            }
        }
    }

    void Start()
    {

    }


    public void ShowSlider()
    {
        Slider.GetComponent<RectTransform>().sizeDelta = HideSize;
        Slider.GetComponent<RectTransform>().DOSizeDelta(ShowSize, 0.5f).SetDelay(0.3f).OnComplete(() => { OnSliderShowed.Invoke(); });
    }


    public void HideSlider()

    {
        Slider.GetComponent<RectTransform>().DOSizeDelta(HideSize, 0.5f).SetDelay(0.3f).OnComplete(() => { OnSliderHided?.Invoke(); });

    }
    // Update is called once per frame


    public void Hide()
    {

        var canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
        canvasGroup.DOFade(0, 1f).SetDelay(2f).SetEase(Ease.OutCubic).OnComplete(() => { this.gameObject.SetActive(false); });

    }

    public void Reset(string tips, SliderMode mode, Action onCompelete)
    {

        this.mode = mode;
        Title.text = tips;
        this.curent = 0;
        this.total = 1;
        CurProgressValue = 0;

        OnComplete = onCompelete;
    }
    public void SetData(string tips,long curent, long total)
    {
        if (tips != null)
        {
            Title.text = tips;
        }
        this.curent = curent;
        this.total = total;
        SetText();
    }

    private void SetText()
    {

        CurProgressValue = curent * 1.0f / total;
        if (this.mode == SliderMode.Percent)
        {
            Text.text = string.Format("{0:F2}%", Slider.value * 100);
        }
        else if (this.mode == SliderMode.Fraction)
        {
            Text.text = $"{curent} / {total}";
        }
        else
        {

            Text.text = $"{curent * 1.0f / (1 << 20):F2}Mb / {total * 1.0f / (1 << 20):F2} Mb";

        }
    }

    public void AddData(string tips, long add, long total)
    {

        if (tips != null)
        {
            Title.text = tips;
        }
        this.curent += add;
        this.total = total;
        SetText();
    }
}