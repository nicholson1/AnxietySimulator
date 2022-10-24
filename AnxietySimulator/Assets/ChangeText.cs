using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void ChangeTheText(int a)
    {
        if (a < 10)
        {
            text.text = "Although, I think it’s safe to say today had more than “little” victories, because you finished it feeling better than you have in a while. You didn’t just survive today - you totally crushed it! Go ahead and celebrate that, because you’ve more than earned it! You’re so much stronger than you know, and you should carry that with you.";
        }
        else if(a  < 30)
        {
            text.text =
                "Not only that, but your day went really well! Sure, there were stressful moments here and there, but compared to some of your stressful days, this was nothing. There’s a part of your brain telling you it could have gone better, but you’re able to ignore that part of yourself. You conquered today; it didn’t have to go perfectly for that to remain true.";
        }
        else if (a < 50)
        {
            text.text = "Sure, today was far from perfect… but if you look at the glass half full, it was far from imperfect as well! Sometimes your anxiety bubbled up inside you, but you wrapped up the day without feeling like you reached your boiling point. Even if there are parts you wish went differently, all in all, it’s still something to be proud of.";
        }
        else if (a < 75)
        {
            text.text = "Today was rocky at times, but you’ve definitely had worse. Even with whatever roadblocks you faced, a past version of yourself certainly would have handled them as well as you did… even if you don’t feel like you handled it well. You got through it, and that’s still worth patting yourself on the back for!";
        }else if (a < 95)
        {
            text.text = "Today was a difficult one to pull yourself through. When all was said and done, you wrapped today up feeling close to your breaking point. Regardless of how you feel about yourself, keep one thing in mind: today didn’t break you. You finished it with your psyche cracked, but not broken. You should be proud of yourself, even if you don’t feel like you deserve to be proud… especially if you don’t feel like you deserve it.";
        }
        else
        {
            text.text = "You’re relieved it’s all over, because right now, even the tiniest little inconvenience feels like it could shatter you beyond repair. You know what, though? You survived! Today may have worn you down, but it didn’t take you out. You’re a survivor, and that’s something worth holding onto. If that’s a cold comfort, just remember: you will never have to experience this exact day again… hopefully tomorrow will be a better one, and if not tomorrow, then the next day, and if not that, then someday! In the meantime, be gentle with yourself!";
        }
    }
}
