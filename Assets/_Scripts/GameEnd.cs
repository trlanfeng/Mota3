using UnityEngine;
using UnityEngine.UI;
public class GameEnd : MonoBehaviour
{
    public Text jiejuText;

    void Start()
    {
        if (PlayerPrefs.HasKey("jieju"))
        {
            string jieju = PlayerPrefs.GetString("jieju");
            switch (jieju)
            {
                case "jiehun":
                    jiejuText.text = "勇士和公主一起回家了，他们在森林中找了间小木屋，过起了隐居的生活，幸福而甜蜜。";
                    break;
                case "siyugongzhu":
                    jiejuText.text = "因为勇士的不作为，公主一怒之下将其XX……勇士，阿不，这个闯关的人，已被世人淡忘。";
                    break;
                case "siyuBBOSS":
                    if (PlayerPrefs.GetInt("gongzhuDead") == 1)
                    {
                        jiejuText.text = "勇士终于还是无法战胜大魔王，但他的英勇事迹，仍被世人传颂。";
                    }
                    else
                    {
                        jiejuText.text = "勇士终于还是无法战胜大魔王，但他的英勇事迹，仍被世人传颂。\n公主因为勇士的离去欲哭无泪，离世后，后人将她和勇士葬在了一起。";
                    }
                    break;
                case "dabaiBOSS":
                    if (PlayerPrefs.GetInt("gongzhuDead") == 1)
                    {
                        jiejuText.text = "勇士打败了魔王，但他并不知道，真正的魔王并没有出现在他的面前。\n大魔王仍在危害人间，而勇士，已没有人知道他去向何处。";
                    }
                    else
                    {
                        jiejuText.text = "勇士打败了魔王，但他并不知道，真正的魔王并没有出现在他的面前。\n勇士和公主一起回家了，他们在森林中找了间小木屋，过起了隐居的生活，幸福而甜蜜。";
                    }
                    break;
                case "dabaiBBOSS":
                    if (PlayerPrefs.GetInt("gongzhuDead") == 1)
                    {
                        jiejuText.text = "勇士打败了大魔王，拯救了人类，英勇事迹为世人所传颂。";
                    }
                    else
                    {
                        jiejuText.text = "勇士打败了大魔王，拯救了人类，英勇事迹为世人所传颂。\n勇士和公主一起回家了，他们在森林中找了间小木屋，过起了隐居的生活，幸福而甜蜜。";
                    }
                    break;
            }
        }
    }
    public void endGame()
    {
        Application.Quit();
    }
}
