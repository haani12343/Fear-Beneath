using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI subtitleText;
    [Header("Audio Sources")]
    public AudioSource carAudioSource;
    public AudioSource ambienceAudioSource;
    [Header("Car Sounds")]
    public AudioClip carStart;
    public AudioClip carDrivingLoop;
    public AudioClip carDoorSlam;
    [Header("Ambience")]
    public AudioClip forestAmbience;
    IEnumerator Start()
    {
        subtitleText.text = "";
        carAudioSource.loop = false;
        carAudioSource.clip = carStart;
        carAudioSource.Play();
        yield return new WaitForSeconds(carStart.length);
        carAudioSource.clip = carDrivingLoop;
        carAudioSource.loop = true;
        carAudioSource.Play();
        yield return new WaitForSeconds(2f);
        subtitleText.text = "<color=#FFD54F>Ryan</color>\nAlright... about five more minutes.";
        yield return new WaitForSeconds(4f);
        subtitleText.text = "<color=#66CCFF>Jake</color>\nYou said that fifteen minutes ago.";
        yield return new WaitForSeconds(4f);
        subtitleText.text = "<color=#FFD54F>Ryan</color>\nYeah, because someone insisted we stop for snacks.";
        yield return new WaitForSeconds(4f);
        subtitleText.text = "<color=#66CCFF>Jake</color>\nAnd those snacks were absolutely worth it.";
        yield return new WaitForSeconds(4f);
        subtitleText.text = "<color=#7CFC00>Alex</color>\n(Laughs) He's got a point.";
        yield return new WaitForSeconds(4f);
        subtitleText.text = "<color=#FFD54F>Ryan</color>\nYou literally bought enough chips to survive a week.";
        yield return new WaitForSeconds(4f);
        subtitleText.text = "<color=#66CCFF>Jake</color>\nYou never know.";
        yield return new WaitForSeconds(4f);
        subtitleText.text = "<color=#7CFC00>Alex</color>\nI'm just excited to get there. It'll be nice to get away from everything for a while.";
        yield return new WaitForSeconds(5f);
        subtitleText.text = "<color=#FFD54F>Ryan</color>\nExactly. No traffic. No phones. No people.";
        yield return new WaitForSeconds(4f);
        subtitleText.text = "<color=#66CCFF>Jake</color>\nSpeaking of phones... does anyone actually have any signal?";
        yield return new WaitForSeconds(5f);
        subtitleText.text = "<color=#7CFC00>Alex</color>\nNope. Mine's completely dead.";
        yield return new WaitForSeconds(4f);
        subtitleText.text = "<color=#FFD54F>Ryan</color>\nGood. That means nobody can bother us.";
        yield return new WaitForSeconds(4f);
        subtitleText.text = "<color=#66CCFF>Jake</color>\nOr nobody can help us.";
        yield return new WaitForSeconds(4f);
        subtitleText.text = "<color=#7CFC00>Alex</color>\nYou're impossible.";
        yield return new WaitForSeconds(3f);
        subtitleText.text = "";
        yield return new WaitForSeconds(3f);
        subtitleText.text = "<color=#66CCFF>Jake</color>\nHey... did you guys see that?";
        yield return new WaitForSeconds(4f);
        subtitleText.text = "<color=#FFD54F>Ryan</color>\nSee what?";
        yield return new WaitForSeconds(3f);
        subtitleText.text = "<color=#66CCFF>Jake</color>\nThought I saw someone standing off the side of the road.";
        yield return new WaitForSeconds(5f);
        subtitleText.text = "<color=#7CFC00>Alex</color>\nProbably just a tree.";
        yield return new WaitForSeconds(4f);
        subtitleText.text = "<color=#66CCFF>Jake</color>\nYeah... maybe.";
        yield return new WaitForSeconds(4f);
        subtitleText.text = "";
        yield return new WaitForSeconds(2f);
        subtitleText.text = "<color=#FFD54F>Ryan</color>\nWe're here.";
        yield return new WaitForSeconds(3f);
        subtitleText.text = "<color=#7CFC00>Alex</color>\nThis place actually looks pretty nice.";
        yield return new WaitForSeconds(4f);
        subtitleText.text = "<color=#66CCFF>Jake</color>\nIt's creepier than it looked online.";
        yield return new WaitForSeconds(4f);
        subtitleText.text = "<color=#FFD54F>Ryan</color>\nIt's just a forest.";
        yield return new WaitForSeconds(3f);
        subtitleText.text = "<color=#66CCFF>Jake</color>\nThat's exactly what every horror movie starts with.";
        yield return new WaitForSeconds(5f);
        subtitleText.text = "<color=#7CFC00>Alex</color>\nRelax. We'll laugh about this tomorrow.";
        yield return new WaitForSeconds(4f);
        carAudioSource.Stop();
        carAudioSource.PlayOneShot(carDoorSlam);
        ambienceAudioSource.clip = forestAmbience;
        ambienceAudioSource.loop = true;
        ambienceAudioSource.Play();
        subtitleText.text = "<color=#FFD54F>Ryan</color>\nAlright, everyone grab your stuff.";
        yield return new WaitForSeconds(4f);
        subtitleText.text = "<color=#7CFC00>Alex</color>\nLet's get the tents up before it gets dark.";
        yield return new WaitForSeconds(4f);
        subtitleText.text = "";
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("SampleScene");
    }
}