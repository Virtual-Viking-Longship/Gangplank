using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using LogicUI.FancyTextRendering;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using LogicUI.FancyTextRendering.MarkdownLogic;
using System.Text.RegularExpressions;

/*
This class handles the display of the information of inspected objects
It interprets a md file and translates into a vertical layout
This class is expected to attached to a Info panel prefab, which has a vertical layout and templates for the elements that are spawned in it
*/
public class FormattedDocumentDisplay : MonoBehaviour
{
    // public WebBrowserButtons webBrowserButtons;
    private GameObject imageBlock, textBlock, linkBlock, audioPlayerBlock, titleBlock;
    public Transform verticalLayout;
    public RectTransform Hierarchy;
    public Transform Title;
    void Start()
    {
        imageBlock = transform.GetChild(0).gameObject;
        textBlock = transform.GetChild(1).gameObject;
        linkBlock = transform.GetChild(2).gameObject;
        audioPlayerBlock = transform.GetChild(3).gameObject;
        titleBlock = transform.GetChild(4).gameObject;
    }

    // This function is called by the ObjectInspector class
    public void DisplayDocument(TextAsset document)
    {
        
        foreach (Transform child in verticalLayout) Destroy(child.gameObject);
        foreach (Transform child in Title) Destroy(child.gameObject);

        string fileContents = document.text;

        //to fix formatting that makes .md look good in the git repo
        fileContents = System.Text.RegularExpressions.Regex.Replace(fileContents, @"<\/?div(.*?)>\s*\n\s*", "");
        // Remove extra spaces to prevent line breaks in the text
        fileContents = System.Text.RegularExpressions.Regex.Replace(fileContents, @"  ", "");
        // Replace underscores with asterisks for italics, but only if preceded or followed by whitespace or asterisks 
        fileContents = System.Text.RegularExpressions.Regex.Replace(fileContents, @"(?<=[\s*])_|_(?=[\s*])", "*");
        // Replace square brackets around citations with double dashes to prevent markdown errors
        fileContents = System.Text.RegularExpressions.Regex.Replace(fileContents, @"\[citations", "-- citations");
        fileContents = System.Text.RegularExpressions.Regex.Replace(fileContents, @"\]\s+\n", "");


        //finds the line with the image file information
        var regex = new System.Text.RegularExpressions.Regex(@"!\[(.*?)\]\((.*?)\)");
        var matches = regex.Matches(fileContents);

        //if there is an image
        if (matches.Count > 0)
        {
            int handoffIndex = 0; // set index for building text blocks between images
            // loop through image matches and display text before followed by image
            // the regex matches the image syntax in markdown: ![alt text](image path)
            foreach (Match m in matches)
            {
                var imgPath = m.Groups[2].Value; //extracts the actual image path from the regex match: the part in ()
                int startIndex = m.Index;
                int endIndex = m.Length + startIndex;

                DisplayText(fileContents.Substring(handoffIndex, startIndex - handoffIndex)); // display text to this point
                DisplayImage(imgPath); // display image
                handoffIndex = endIndex; // update handoff index to the end of the image match
            }
            // display any remaining text after the last image
            DisplayText(fileContents.Substring(handoffIndex));
        }
        else
        {
            DisplayText(fileContents);
        }
        DisplayTitle(document);
    }

    private void DisplayText(string text) {
        TextMeshProUGUI block = Instantiate(textBlock, verticalLayout).GetComponent<TextMeshProUGUI>();
        block.gameObject.SetActive(true);
        var markdownRenderer = block.GetComponent<MarkdownRenderer>();
        markdownRenderer.Source = text;
    }

    private void DisplayTitle(TextAsset document)
    {
        TextMeshProUGUI block = Instantiate(titleBlock, Title).GetComponent<TextMeshProUGUI>(); //additional step for formatting the title
        block.gameObject.SetActive(true);
        var markdownRenderer = block.GetComponent<MarkdownRenderer>();
        // Capitalize the first letter of the document name and remove the .md extension
        string tempstring = document.name[0].ToString();
        tempstring = tempstring.ToUpper();
        string doc = document.name.Remove(0, 1);
        doc = doc.Insert(0, tempstring);
        markdownRenderer.Source = doc;
        StartCoroutine(Position()); //reposition layout for proper scroll

    }
    private IEnumerator Position()
    {
        yield return null;
        yield return null;
        Hierarchy.anchorMin = new Vector2(0, 1); //set anchor preset in order to start scroll at the top
        Hierarchy.anchorMax = new Vector2(1, 1);
        Hierarchy.pivot = new Vector2(0.5f, 0.5f);
        Hierarchy.anchoredPosition = new Vector2(Hierarchy.anchoredPosition.x, -(Hierarchy.sizeDelta.y) / 2);
    }

    public void DisplayImage(String imgPath)
    {
        imgPath = "Ship-Annotations/" + imgPath.Substring(3, imgPath.Length-7);

        int childCount = gameObject.transform.parent.childCount;
        GameObject block = Instantiate(imageBlock, verticalLayout);

        block.SetActive(true);

        Sprite image = Resources.Load<Sprite>(imgPath);
        if (image != null) {
            Debug.Log("Loaded image successfully from path: " + imgPath);
        } else {
            Debug.LogError("Failed to load image from path: " + imgPath);
        }
        
        StartCoroutine(PlaceImageBlock(block, image));
    }


    // In order to get the width of the image inside the vertical layout, we need to wait one frame for it to update
    private IEnumerator PlaceImageBlock(GameObject block, Sprite image)
    {
        yield return null;
        Image imgComponent = block.GetComponent<Image>();
        if (imgComponent != null) imgComponent.sprite = image;
        block.GetComponent<LayoutElement>().preferredHeight = (image.texture.height / (float)image.texture.width) * block.GetComponent<RectTransform>().rect.width;
    }

    // private void DisplayAudioPlayer(String param)
    // {
    //     GameObject block = Instantiate(audioPlayerBlock, verticalLayout);
    //     block.gameObject.SetActive(true);
    //     string[] parameters = param.Split('(', ')')[1].Split(',');
    //     for (int i = 0; i < parameters.Length; i++) parameters[i] = parameters[i].Trim();

    //     TextMeshProUGUI tmp = block.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    //     tmp.text = parameters[0];
    //     SetTMPParameters(tmp, int.Parse(parameters[1]), parameters[2], parameters[3]);
    //     GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(parameters[4]);
    // }
}
