using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class SubtitleController : MonoBehaviour
{
    [Header("References")]
    public TextAsset srtFile;
    public AudioSource audioSource;
    public TMP_Text subtitleText;

    private bool dialogueStarted;
    private float[] startTimes;
    private float[] endTimes;
    private string[] subtitleTexts;

    void Start()
    {
        subtitleText.text = "";
        ParseSRT();
    }
    public void ParseSRT()
    {
        if (srtFile == null)
        {
            Debug.LogError("No SRT File here...");
            return;
        }


        List<float> starts = new List<float>();
        List<float> ends = new List<float>();
        List<string> texts = new List<string>();

        //need to split the srt file into groups of lines
        string content = srtFile.text
            .Replace("\r\n", "\n")
            .Replace("\r", "\n")
            .Trim();

        string[] blocks = Regex.Split(content, @"\n\s*\n");

        foreach (string block in blocks)
        {
            string[] lines = block.Split('\n');

            int timeLineIndex = -1;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("-->"))
                {
                    timeLineIndex = i;
                    break;
                }

            }

            if (timeLineIndex < 0)
            {
                continue;
            }

            string[] times = lines[timeLineIndex].Split(
                new string[] { "-->" }, System.StringSplitOptions.None);

            if (times.Length != 2)
            {
                continue;
            }

            float start = TimestampToSeconds(times[0].Trim());
            float end = TimestampToSeconds(times[1].Trim());

            //now for subtitles!

            string subtitle = string.Join(
                "\n",
                lines,
                timeLineIndex + 1,
                lines.Length - timeLineIndex - 1
                ).Trim();

            if (subtitle.Length == 0)
            {
                continue;
            }

            //add everything to those lists

            starts.Add(start);
            ends.Add(end);
            texts.Add(subtitle);
        }

        startTimes = starts.ToArray();
        endTimes = ends.ToArray();
        subtitleTexts = texts.ToArray();
    }

    float TimestampToSeconds(string timestamp)
    {
        Match match = Regex.Match(timestamp,
            @"^(\d+):(\d{2}):(\d{2})[,.](\d{1,3})$");

        if (!match.Success)
        {
            throw new FormatException("Invalid timestamp: " + timestamp);
        }

        float hours = float.Parse(
            match.Groups[1].Value,
            CultureInfo.InvariantCulture);

        float minutes = float.Parse(
            match.Groups[2].Value,
            CultureInfo.InvariantCulture);

        float seconds = float.Parse(
            match.Groups[3].Value,
            CultureInfo.InvariantCulture);

        string msText = match.Groups[4].Value.PadRight(3, '0');

        float milliseconds = float.Parse(msText, CultureInfo.InvariantCulture);

        return hours * 3600f
            + minutes * 60f
            + seconds
            + milliseconds / 1000f;
    }

    public void PlayDialogue()
    {
        if (srtFile == null || audioSource == null || subtitleText == null)
        {
            Debug.LogError("Oops! You're missing some references you chud...");
            return;
        }

        if (subtitleTexts == null ||
            endTimes == null ||
            startTimes == null)
        {
            Debug.Log("Oops! Subtitle data ain't workin' twin.");
            return;
        }

        if (subtitleTexts.Length == 0)
        {
            Debug.LogError("Yeah... there's no subtitles here.");
            return;
        }

        audioSource.Stop();
        audioSource.time = 0f;

        subtitleText.text = "";
        dialogueStarted = true;

        audioSource.Play();
    }

    void Update()
    {
        if(!dialogueStarted)
        {
            return;
        }

        if(audioSource.isPlaying)
        {
            float currentTime = audioSource.time;

            for (int i = 0; i < subtitleTexts.Length; i++)
            {
                if(currentTime >= startTimes[i] &&
                    currentTime < endTimes[i])
                {
                    subtitleText.text = subtitleTexts[i];

                    return;
                }
            }

            subtitleText.text = "";

        }
        else
        {
            subtitleText.text = "";
            dialogueStarted = false;
        }
    }

    public void StopDialogue()
    {
        audioSource.Stop();
        subtitleText.text = "";
        dialogueStarted = false;
    }
}
