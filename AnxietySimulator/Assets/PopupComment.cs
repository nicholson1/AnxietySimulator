using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PopupComment : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI _myText;
    [SerializeField] private Image image;
    public float fadeTime = 10f;
    public bool HonkedAt = false;

    public static event Action PopUpClicked;
    
    private String[] Comments = new[]
    {
       "You left your phone behind.",
        "I’m pretty sure you locked yourself out this morning.",
        "Don’t trip!",
        "They’re staring at you.",
        "Your music's too loud, everyone can hear it.",
        "You’re late.",
        "Is somebody following you?",
        "You forgot your wallet. You can’t pay for anything.",
        "Get out of the way, you’re in the way.",
        "You should just go home.",
        "Are you lost? I think you’re lost.",
        "You’re going the wrong way.",
        "You’re never going to get all of this stuff done.",
        "You’re forgetting something. I don’t know what it is, but you forgot it.",
        "Moving here was a mistake.",
        "You aren’t cut out for grad school.",
        "I bet everyone at the orientation last week thought you were a freak.",
        "Why are you like this?",
        "You walk weirdly and everyone notices it.",
        "You don’t deserve to make any new friends here.",
        "I bet they accepted you to this grad program by mistake.",
        "You look so awkward right now… I just know it.",
        "Stop. Just, in general… stop.",
        "Why can’t you be more like your brother?",
        "That facial expression you’re making is creeping everyone out…",
        "You’re existing wrong. I don’t know how, you just are.",
        "Your body looks gross, and your clothes don’t fit right.",
        "Your heart is beating so fast, I bet you’ll get a heart attack!",
        "How does someone like you have friends, let alone a girlfriend?",
        "You’re gonna bump into someone. They’re gonna hate you for it.",
        "Listen to your own heartbeat. Isn’t that unnerving…?",
        "You didn’t sleep enough to get through today…",
        "You’re totally gonna blow your classes next week.",
        "You’re nothing. Unimportant. Irrelevant.",
        "You could disappear right now and nobody would care enough to notice.",
        "Your brain is broken beyond repair.",
        "You’re a failure.",
        "Those stomach pains suck, huh? Don’t throw up!",
        "You break everything you touch.",
        "As a person, you’re fundamentally broken.",
        "You’re a burden to everyone in your life.",
        "Everyone is only tolerating you. Nothing more.",
        "You’re a drain on everyone you know.",
        "Why does anyone put up with you?",
        "You’re the worst.",
        "Why do you struggle with things everyone else finds easy?",
        "Why do you even exist?",
        "Your brain isn’t wired right.",
        "I bet people make fun of you behind your back.",
        "You’re an embarrassment.",
        "Why do you move like that? It doesn’t look right.",
        "You’re wasting your time.",
        "I bet they’re all judging you.",
        "A quarter of a century on this earth, and you’ve done nothing with it.",
        "Isn’t it sad how this is all you’ve amounted to?",
        "Get your shit together!",
        "You’re messing everything up.",
        "When are you going to make something of yourself?",
        "Your child self would be so sad about what you became…",
        "Ugh, why are you so awkward?",
        "Why can’t you be different?",
        "Ugh, what is your deal?",
        "What’s your problem?",
        "What the hell is wrong with you?!",
        "You could be making a more productive use of your time.",
        "Why attend grad school if you don’t even know what you’re doing with your life?",
        "You’re worthless.",
        "The world would be better off without you.",
        "You’re completely useless!",
        "You deserve to have a bad day.",
        "Why can’t you do anything right?",
        "Stop overthinking every stupid little thing!",
        "You don’t deserve to be happy.",
        "You’ll never be good enough.",
        "Isn’t it sad how your best effort still isn’t enough?",
        "You’re a disappointment.",
        "Why can’t you be better?",
        "You deserve your misery.",
        "Pretty sure that person just gave you a judgmental glance.",
        "I’m shocked anyone likes you.",
        "Stop screwing up everything!",
        "You don’t matter.",
        "You make everyone’s life worse.",
        "You’re good for nothing.",
        "Everyone will leave you eventually. I just know it.",
        "I bet everyone in your life secretly resents you.",
        "You’re such a mess.",
        "You’re a walking dumpster fire.",
        "You’ve been nothing but a burden for 25 years straight.",
        "What’re you gonna mess up next time?",
        "You’ll never amount to anything.",
        "You’re running out of time!",
        "Something bad is about to happen… I just know it.",
        "Stop overreacting to everything!",
        "Today is gonna suck and it’ll all be your fault.",
        "No wonder you have trouble making friends…",
        "You’re a total trainwreck.",
        "Everyone else in your class is better than you.",
        "Everyone here will hate you.",
        "Ugh, you’re so annoying…",
        "You’re such hateable person.",
        "The fact that you even exist is embarrassing.",
        "You take up too much space.",
        "You’re gonna fail.",
        "Did you almost trip and fall?",
        "You’re mentally weak.",
        "Wait, is someone watching you?",
        "Are you even going to make it through the day?",
        "You probably shouldn’t exist.",
        "Why can’t you be more than this?",
        "You’re so bad at life.",
        "You’re wasting your potential… assuming you ever had any.",
        "What will you ruin next?",
        "You fail at life.",
        "How are you THIS bad at existing?",
        "You’re a bad person.",
        "The people who dislike you are right.",
        "You’re unlovable.",
        "You should just leave everyone alone.",
        "You’re insane!",
        "Why are you so crazy?!",
        "You’re so mentally unstable…",
        "You’re too stupid for grad school.",
        "You deserve nothing but pain.", 
        "Why were you even accepted to grad school?",
        "Your classmates seem so talented… unlike you.",
        "Stop acting weird!",
        "Stop being a freak!",
        "No one will ever be proud of you.",
        "I’m pretty sure you already peaked, and you weren’t even that good at your peak…",
        "All your classmates will end up hating you.",
        "What little you’ve accomplished is a fluke.",
        "You don’t belong here… or anywhere.",
        "You shouldn’t be here.",
        "You will never accomplish anything worthwhile.",
        "Why can’t you be someone else?",
        "Is this all you’re ever gonna be?",
        "Isn’t your prefrontal cortex supposed to be developed by now, or is your brain broken?",
        "Studying psychology was a mistake.",
        "Ugh… just, ugh. That’s all I have to say to you right now.",
        "You should be ashamed of yourself.",
        "You don’t deserve to have self-esteem.",
        "Applying to grad school was a mistake.",
        "Can you not?!",
        "You have no reason to like yourself.",
        "There’s nothing to like about you.",
        "You know you suck, right?",
        "You’re never gonna make it in life.",
        "You never should have gotten this far in life.",
        "You aren’t worth it.",
        "You’re doomed.",
        "Hating yourself is only logical…",
        "You have no future ahead of you.",
        "You’re simply terrible - always have been, always will be.",
        "Who are you gonna disappoint next?",
        "Not only will you be replaced, finding a better replacement won’t be difficult.",
        "You have nothing to be proud of.",
        "The world doesn’t need you.",
        "Your existence is unnecessary.",
        "Nobody has any reason to be proud of you.",
        "You’re a pile of garbage that somehow became sentient.",
        "Your existence has no value.",
        "Your life is meaningless.",
        "You cause nothing but disappointment and pain.",
        "No situation will ever be improved with your presence.",
        "Everything you’re involved with becomes worse as a result.",
        "Nothing good comes from you.",
        "You should feel bad.",


    };

    private String[] HonkComments = new[]
    {
        "You’re going to get hit by a car!",
        "You're in the way of that car!",
        "Move, they're honking at you!",
        "I wish they would just hit you...",
        "Maybe you deserve to be hit…",
        "Isn’t it embarrassing how you can’t drive?",
        "Today would be easier if you had a license.",
        "Are you trying to get hit?",
        "Ugh, why are you always in the way?!",
        "You shouldn’t be allowed to be a pedestrian if you’re gonna be that dumb!",
        "Wait, are you jaywalking? Idiot.",
        "Why don’t YOU drive? You’re such a coward.",
        "What’s it like having a brain too broken to get your license?",
        "Wild how there are teenagers who can drive, and you can’t.",
        "You have no real reason not to have a license, you’re just being difficult.",
        "You’re gonna make yourself roadkill!",
        "You’re walking in the wrong place!",
        "Aww, did the honk scare you? Pathetic.",
        "Your reaction to that honk made you look like a jumpy freak.",
        "Stop flinching at every loud noise!",
        "Way to almost get hit, dumbass.",
        "You don’t live on a farm anymore, idiot, you gotta watch out for cars!",
        "Stop getting in everyone’s way!",
        "Ugh, move, dammit!",


    };
    private void Start()
    {
        if (!HonkedAt)
        {
            _myText.text = Comments[Random.Range(0, Comments.Length)];
        }
        else
        {
            _myText.text = HonkComments[Random.Range(0, HonkComments.Length)];
        }
        

        StartCoroutine(Fade());
    }
    
    private IEnumerator Fade()
    {
        
        Color initialColor = image.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, .25f);

        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            image.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeTime);
            yield return null;
        }
        
        Destroy(this.gameObject);
    }

    public void ClickOnComment()
    {
        //++ anxiety
        PopUpClicked();
        Destroy(this.gameObject);
    }

    //fade then disable
    // onclick disable + anxiety
    // random text
}
