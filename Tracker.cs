using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
//using System.Windows.Forms;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;
using System.IO;

public class Tracker : MonoBehaviour {

    enum StockRoom { Intro, Room1, Room2, Room3, Room4}
    StockRoom CurrentRoom;
   

    private int SelectRange;
    private int SelectRoom;
    public decimal Change = 0;
    public decimal ChangePercent = 0;

   

    GUIStyle style = new GUIStyle();
    GUIStyle PosStyle = new GUIStyle();
    GUIStyle NegStyle = new GUIStyle();
    private Texture2D ChartTextureOne;
    private Texture2D ChartTextureTwo;
    private Texture2D ChartTextureThree;
    private Texture2D ChartTextureFour;

    //string url = ""; char[] seperatedata = { ',', ',' };

    private string ChartURL;
    private string ChartURlOne;
    private string ChartURlTwo;
    private string ChartURlThree;
    private string ChartURlFour;

    private string SIInputOne = "WMT";
    private string SIInputTwo = "COKE";
    private string SIInputThree = "MSFT";
    private string SIInputFour = "DIS";

    public string SIPricesOne;
    public string SIPricesTwo;
    public string SIPricesThree;
    public string SIPricesFour;

    public decimal SIChangeOne;
    public decimal SIChangeTwo;
    public decimal SIChangeThree;
    public decimal SIChangeFour;

    public string SIChangePercentOne;
    public string SIChangePercenTwo;
    public string SIChangePercenThree;
    public string SIChangePercenFour;

    public string InputTextOne;
    public string InputTextTwo;
    public string InputTextThree;
    public string InputTextFour;

    public string Data0;
    public string Data1;
    public string Data2;
    public string Data3;
    public string Data4;
    public string Data5;
    public string Data6;
    public string Data7;
    public string Data8;
    public string Data9;
    public string Data10;
    public string Data11;
    public string Data12;
    public string Data13;
 
    public decimal StockData;

   



    public object MessageBox { get; private set; }
    public object MessageBoxButtons { get; private set; }
    public object MessageBoxIcon { get; private set; }

    // Use this for initialization
    void Start () {

       

        CurrentRoom = StockRoom.Intro;
        SelectRange = 0;
        SelectRoom = 0;
        GetData();
        StartCoroutine(ShowChart());
        getPrices();
	}

   

    void Update()
    {
        
    }




    IEnumerator ShowChart()
    {
        print("IThe Button Works.");

        if (SelectRange == 0)
        {
            ChartURL = "http://chart.finance.yahoo.com/b?s=";
        }
        if (SelectRange == 1)
        {
            ChartURL = "http://chart.finance.yahoo.com/w?s=";
        }
        if (SelectRange == 2)
        {
            ChartURL = "http://chart.finance.yahoo.com/c/3m/";
        }
        if (SelectRange == 3)
        {
            ChartURL = "http://chart.finance.yahoo.com/c/6m/";
        }
        if (SelectRange == 4)
        {
            ChartURL = "http://chart.finance.yahoo.com/c/1y/";
        }
        if (SelectRange == 5)
        {
            ChartURL = "http://chart.finance.yahoo.com/c/2y/";
        }
        if (SelectRange == 6)
        {
            ChartURL = "http://chart.finance.yahoo.com/c/5y/";
        }
        if (SelectRange == 7)
        {
            ChartURL = "http://chart.finance.yahoo.com/c/my/";
        }

        ChartURlOne = ChartURL + SIInputOne;
        ChartURlTwo = ChartURL + SIInputTwo;
        ChartURlThree = ChartURL + SIInputThree;
        ChartURlFour = ChartURL + SIInputFour;

        // Start a download of the given URL
        WWW wwwOne = new WWW(ChartURlOne);
        WWW wwwTwo = new WWW(ChartURlTwo);
        WWW wwwThree = new WWW(ChartURlThree);
        WWW wwwFour = new WWW(ChartURlFour);
        // Wait for download to complete
        yield return wwwOne;
        yield return wwwTwo;
        yield return wwwThree;
        yield return wwwFour;

        ChartTextureOne = wwwOne.texture;
        ChartTextureTwo = wwwTwo.texture;
        ChartTextureThree = wwwThree.texture;
        ChartTextureFour = wwwFour.texture;

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = wwwOne.texture;
    }

   


    private void OnGUI()
    {
       

        style.fontSize = 24;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.black;
        

        PosStyle.fontSize = 24;
        PosStyle.fontStyle = FontStyle.Bold;
        PosStyle.normal.textColor = Color.green;

        NegStyle.fontSize = 24;
        NegStyle.fontStyle = FontStyle.Bold;
        NegStyle.normal.textColor = Color.red;

        GUI.backgroundColor = Color.white;
        GUI.Label(new Rect(0,0,100, 30),"SSG Stock Monitor",style);

        if (CurrentRoom == StockRoom.Intro)
        {
            
        


            SIInputOne = GUI.TextField(new Rect( 50,  50, 100, 30), SIInputOne);
            SIInputTwo = GUI.TextField(new Rect(850, 50, 100, 30), SIInputTwo);
            SIInputThree = GUI.TextField(new Rect(50, 450, 100, 30), SIInputThree);
            SIInputFour = GUI.TextField(new Rect(850, 450, 100, 30), SIInputFour);

            GUI.Label(new Rect(150, 50, 100, 30), SIPricesOne, style);
            GUI.Label(new Rect(950, 50, 100, 30), SIPricesTwo, style);
            GUI.Label(new Rect(150, 450, 100, 30), SIPricesThree, style);
            GUI.Label(new Rect(950, 450, 100, 30), SIPricesFour, style);


            if (SIChangeOne > 0)
            {
                GUI.Label(new Rect(250, 50, 100, 30), SIChangeOne.ToString(), PosStyle);
                GUI.Label(new Rect(350, 50, 100, 30), SIChangePercentOne, PosStyle);
            }
            else if (SIChangeOne < 0)
            {
                GUI.Label(new Rect(250, 50, 100, 30), SIChangeOne.ToString(), NegStyle);
                GUI.Label(new Rect(350, 50, 100, 30), SIChangePercentOne, NegStyle);
            }
            else
            {
                GUI.Label(new Rect(250, 50, 100, 30), SIChangeOne.ToString(), style);
                GUI.Label(new Rect(350, 50, 100, 30), SIChangePercentOne, style);
            }

            if (SIChangeTwo > 0)
            {
                GUI.Label(new Rect(1050, 50, 100, 30), SIChangeTwo.ToString(), PosStyle);
                GUI.Label(new Rect(1150, 50, 100, 30), SIChangePercenTwo, PosStyle);
            }
            else if (SIChangeTwo < 0)
            {
                GUI.Label(new Rect(1050, 50, 100, 30), SIChangeTwo.ToString(), NegStyle);
                GUI.Label(new Rect(1150, 50, 100, 30), SIChangePercenTwo, NegStyle);
            }
            else
            {
                GUI.Label(new Rect(1050, 50, 100, 30), SIChangeTwo.ToString(), style);
                GUI.Label(new Rect(1150, 50, 100, 30), SIChangePercenTwo, style);
            }

            if (SIChangeThree > 0)
            {
                GUI.Label(new Rect(250, 450, 100, 30), SIChangeThree.ToString(), PosStyle);
                GUI.Label(new Rect(350, 450, 100, 30), SIChangePercenThree, PosStyle);
            }
            else if (SIChangeThree < 0)
            {
                GUI.Label(new Rect(250, 450, 100, 30), SIChangeThree.ToString(), NegStyle);
                GUI.Label(new Rect(350, 450, 100, 30), SIChangePercenThree, NegStyle);
            }
            else
            {
                GUI.Label(new Rect(250, 450, 100, 30), SIChangeThree.ToString(), style);
                GUI.Label(new Rect(350, 450, 100, 30), SIChangePercenThree, style);
            }

            if (SIChangeFour > 0)
            {
                GUI.Label(new Rect(1050, 450, 100, 30), SIChangeFour.ToString(), PosStyle);
                GUI.Label(new Rect(1150, 450, 100, 30), SIChangePercenFour, PosStyle);
            }
            else if (SIChangeFour < 0)
            {
                GUI.Label(new Rect(1050, 450, 100, 30), SIChangeFour.ToString(), NegStyle);
                GUI.Label(new Rect(1150, 450, 100, 30), SIChangePercenFour, NegStyle);
            }
            else
            {
                GUI.Label(new Rect(1050, 450, 100, 30), SIChangeFour.ToString(), style);
                GUI.Label(new Rect(1150, 450, 100, 30), SIChangePercenFour, style);
            }

            if (GUI.Button(new Rect(600, 200, 100, 50), "Get Chart"))
            {
                print("The button works!");
                StartCoroutine(ShowChart());

            }

            if (GUI.Button(new Rect(600, 400, 100, 50), "Exit"))
            {
                Application.Quit();
            }

            if (GUI.Button(new Rect(50, 100, 600, 600), ChartTextureOne, style))
            {
                CurrentRoom = StockRoom.Room1;
                SelectRoom = 0;
                GetData();
                print("Button works!");
            }
            if (GUI.Button(new Rect(700, 100, 600,600), ChartTextureTwo, style))
            {
                CurrentRoom = StockRoom.Room2;
                SelectRoom = 1;
                GetData();
                print("Button works!");
            }
            if (GUI.Button(new Rect(50, 500, 600, 600), ChartTextureThree, style))
            {
                CurrentRoom = StockRoom.Room3;
                SelectRoom = 2;
                GetData();
                print("Button works!");
            }
            if (GUI.Button(new Rect(700, 500,600, 600), ChartTextureFour, style))
            {
                CurrentRoom = StockRoom.Room4;
                SelectRoom = 3;
                GetData();
                print("Button works!");
            }

            if (GUI.Button(new Rect(600, 300, 100, 50), "Get Prices"))
            {


                string url = ""; 
                if (SIInputOne != "") url += SIInputOne + "+";
                if (SIInputTwo != "") url += SIInputTwo + "+";
                if (SIInputThree != "") url += SIInputThree + "+";
                if (SIInputFour != "") url += SIInputFour + "+";
                if (url != "")
                {
                    // Remove the trailing plus sign.
                    url = url.Substring(0, url.Length - 1);

                    // Prepend the base URL.
                    const string base_url =
                        "http://download.finance.yahoo.com/d/quotes.csv?s=@&f=sl1d1t1c1p2";
                    url = base_url.Replace("@", url);
                    // Get the response.
                    try
                    {
                        string result = GetWebResponse(url);
                       
                        print(result.Replace("\\r\\n", "\r\n"));
                        // Pull out the current prices.
                        string[] lines = result.Split(
                            new char[] { '\r', '\n' },
                            StringSplitOptions.RemoveEmptyEntries);



                        SIPricesOne = decimal.Parse(lines[0].Split(',')[1]).ToString("C3");
                        SIPricesTwo = decimal.Parse(lines[1].Split(',')[1]).ToString("C3");
                        SIPricesThree = decimal.Parse(lines[2].Split(',')[1]).ToString("C3");
                        SIPricesFour = decimal.Parse(lines[3].Split(',')[1]).ToString("C3");

                        SIChangeOne = decimal.Parse(lines[0].Split(',')[4]);
                        SIChangeTwo = decimal.Parse(lines[1].Split(',')[4]);
                        SIChangeThree = decimal.Parse(lines[2].Split(',')[4]);
                        SIChangeFour = decimal.Parse(lines[3].Split(',')[4]);

                        SIChangePercentOne = lines[0].Split(',')[5].Trim(new char[] { '"' });
                        SIChangePercenTwo = lines[1].Split(',')[5].Trim(new char[] { '"' });
                        SIChangePercenThree = lines[2].Split(',')[5].Trim(new char[] { '"' });
                        SIChangePercenFour = lines[3].Split(',')[5].Trim(new char[] { '"' });

                       
                       





                    }
                    catch //(Exception ex)
                    {
                        //MessageBox.ToString(ex.Message, "Read Error",MessageBoxButtons.GetType, MessageBoxIcon.GetType);
                        print("Error");
                    }
                }

            }
        }

        if (CurrentRoom == StockRoom.Room1)
        {
            SIInputOne = GUI.TextField(new Rect(50, 100, 100, 30), SIInputOne);
            GUI.Box(new Rect(50, 450, 600, 600), ChartTextureOne, style);
            GUI.Label(new Rect(50, 150, 100, 30), Data3, style);
            GUI.Label(new Rect(50, 250, 100, 30), Data5, style);
            GUI.Label(new Rect(250, 250, 100, 30), Data6, style);
            GUI.Label(new Rect(450, 250, 100, 30), Data7, style);
            GUI.Label(new Rect(650, 250, 100, 30), Data8, style);
            GUI.Label(new Rect(50, 350, 100, 30), Data9, style);
            GUI.Label(new Rect(250, 350, 100, 30), Data11, style);
            GUI.Label(new Rect(450, 350, 100, 30), Data12, style);
            GUI.Label(new Rect(650, 350, 100, 30), Data13, style);

            if (StockData > 0)
            {
                GUI.Label(new Rect(250, 150, 100, 30), Data4, PosStyle);
                GUI.Label(new Rect(450, 150, 100, 30), Data10, PosStyle);
            }
            else if (StockData < 0)
            {
                GUI.Label(new Rect(250, 150, 100, 30), Data4, NegStyle);
                GUI.Label(new Rect(450, 150, 100, 30), Data10, NegStyle);
            }
            else
            {
                GUI.Label(new Rect(250, 150, 100, 30), Data4, style);
                GUI.Label(new Rect(450, 150, 100, 30), Data10, style);
            }

            if (GUI.Button(new Rect(950, 10, 100, 50), "Back"))
            {
                CurrentRoom = StockRoom.Intro;
            }


            if (GUI.Button(new Rect(600, 450, 100, 50), "1-Day Chart"))
            {
                SelectRange = 0;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(700, 450, 100, 50), "5-Day Chart"))
            {
                SelectRange = 1;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(800, 450, 100, 50), "3-Month Chart"))
            {
                SelectRange = 2;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(600, 550, 100, 50), "6-Month Chart"))
            {
                SelectRange = 3;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(700, 550, 100, 50), "1-Year Chart"))
            {
                SelectRange = 4;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(800, 550, 100, 50), "2-year Chart"))
            {
                SelectRange = 5;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(600, 650, 100, 50), "5-Year Chart"))
            {
                SelectRange = 6;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(700, 650, 100, 50), "Max Chart"))
            {
                SelectRange = 7;
                StartCoroutine(ShowChart());
            }




            if (GUI.Button(new Rect(150, 100, 160, 30), "Get Data"))
            {
                // Build the URL.
                string url = ""; char[] seperatedata = { ',', ',' };//new char[] { '\r', '\n' }
                if (SIInputOne != "") url += SIInputOne + "+";
                {
                    if (url != "")
                    {


                        // Remove the trailing plus sign.
                        url = url.Substring(0, url.Length - 1);

                        // Prepend the base URL.
                        const string base_url =
                            "http://download.finance.yahoo.com/d/quotes.csv?s=@&f=sl1d1t1c1hgvbap2oc3rd";
                        url = base_url.Replace("@", url);


                        try
                        {
                            string result = GetWebResponse(url);
                            Console.WriteLine(result.Replace("\\r\\", "\r\n"));

                            // Pull out the current prices.
                            string[] lines = result.Split(seperatedata, StringSplitOptions.RemoveEmptyEntries);

                            foreach (string d in lines)
                            {
                                Console.WriteLine(d);
                            }

                            Data0 = lines[2].ToString();
                            Data1 = "Today's Date:" + DateTime.Today.ToString("MM/dd/yy");
                            Data2 = "Time:" + DateTime.Now.ToString();
                            Data3 = "Price:" + lines[1].ToString();
                            Data4 = "Change:" + lines[4].ToString();
                            Data5 = "Day's High:" + lines[5].ToString();
                            Data6 = "Day's Low:" + lines[6].ToString();
                            Data7 = "Volume:" + lines[7].ToString();
                            Data8 = "Bid:" + lines[8].ToString();
                            Data9 = "Ask:" + lines[9].ToString();
                            Data10 = "Change%:" + lines[10].Trim(new char[] { '"' });
                            Data11 = "Open:" + lines[11].ToString();
                            Data12 = "P/E Ratio:" + lines[13].ToString();
                            Data13 = "Dividens:" + lines[14].ToString();

                            StockData = decimal.Parse(lines[4]);





                        }
                        catch
                        {
                            print("Error");
                        }


                    }
                }
            }

        }
        if (CurrentRoom == StockRoom.Room2)
        {
            SIInputTwo = GUI.TextField(new Rect(50, 100, 100, 30), SIInputTwo, style);
            
            GUI.Box(new Rect(50, 450, 600, 600), ChartTextureTwo, style);
            GUI.Label(new Rect(50, 150, 100, 30), Data3, style);
            GUI.Label(new Rect(50, 250, 100, 30), Data5, style);
            GUI.Label(new Rect(250, 250, 100, 30), Data6, style);
            GUI.Label(new Rect(450, 250, 100, 30), Data7, style);
            GUI.Label(new Rect(650, 250, 100, 30), Data8, style);
            GUI.Label(new Rect(50, 350, 100, 30), Data9, style);
            GUI.Label(new Rect(250, 350, 100, 30), Data11, style);
            GUI.Label(new Rect(450, 350, 100, 30), Data12, style);
            GUI.Label(new Rect(650, 350, 100, 30), Data13, style);

            if (StockData > 0)
            {
                GUI.Label(new Rect(250, 150, 100, 30), Data4, PosStyle);
                GUI.Label(new Rect(450, 150, 100, 30), Data10, PosStyle);
            }
            else if (StockData < 0)
            {
                GUI.Label(new Rect(250, 150, 100, 30), Data4, NegStyle);
                GUI.Label(new Rect(450, 150, 100, 30), Data10, NegStyle);
            }
            else
            {
                GUI.Label(new Rect(250, 150, 100, 30), Data4, style);
                GUI.Label(new Rect(450, 150, 100, 30), Data10, style);
            }

            if (GUI.Button(new Rect(950, 10, 100, 50), "Back"))
            {
                CurrentRoom = StockRoom.Intro;
            }


            if (GUI.Button(new Rect(600, 450, 100, 50), "1-Day Chart"))
            {
                SelectRange = 0;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(700, 450, 100, 50), "5-Day Chart"))
            {
                SelectRange = 1;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(800, 450, 100, 50), "3-Month Chart"))
            {
                SelectRange = 2;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(600, 550, 100, 50), "6-Month Chart"))
            {
                SelectRange = 3;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(700, 550, 100, 50), "1-Year Chart"))
            {
                SelectRange = 4;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(800, 550, 100, 50), "2-year Chart"))
            {
                SelectRange = 5;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(600, 650, 100, 50), "5-Year Chart"))
            {
                SelectRange = 6;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(700, 650, 100, 50), "Max Chart"))
            {
                SelectRange = 7;
                StartCoroutine(ShowChart());
            }







            if (GUI.Button(new Rect(150, 100, 160, 30), "Get Data"))
            {
                // Build the URL.
                string url = ""; char[] seperatedata = { ',', ',' };//new char[] { '\r', '\n' }
                if (SIInputTwo != "") url += SIInputTwo + "+";
                {
                    if (url != "")
                    {


                        // Remove the trailing plus sign.
                        url = url.Substring(0, url.Length - 1);

                        // Prepend the base URL.
                        const string base_url =
                            "http://download.finance.yahoo.com/d/quotes.csv?s=@&f=sl1d1t1c1hgvbap2oc3rd";
                        url = base_url.Replace("@", url);


                        try
                        {
                            string result = GetWebResponse(url);
                            Console.WriteLine(result.Replace("\\r\\", "\r\n"));

                            // Pull out the current prices.
                            string[] lines = result.Split(seperatedata, StringSplitOptions.RemoveEmptyEntries);

                            foreach (string d in lines)
                            {
                                Console.WriteLine(d);
                            }

                            Data0 = lines[2].ToString();
                            Data1 = "Today's Date:" + DateTime.Today.ToString("MM/dd/yy");
                            Data2 = "Time:" + DateTime.Now.ToString();
                            Data3 = "Price:" + lines[1].ToString();
                            Data4 = "Change:" + lines[4].ToString();
                            Data5 = "Day's High:" + lines[5].ToString();
                            Data6 = "Day's Low:" + lines[6].ToString();
                            Data7 = "Volume:" + lines[7].ToString();
                            Data8 = "Bid:" + lines[8].ToString();
                            Data9 = "Ask:" + lines[9].ToString();
                            Data10 = "Change%:" + lines[10].Trim(new char[] { '"' });
                            Data11 = "Open:" + lines[11].ToString();
                            Data12 = "P/E Ratio:" + lines[13].ToString();
                            Data13 = "Dividens:" + lines[14].ToString();

                            StockData = decimal.Parse(lines[4]);





                        }
                        catch
                        {
                            print("Error");
                        }


                    }
                }
            }
        }
        if (CurrentRoom == StockRoom.Room3)
        {
            SIInputThree = GUI.TextField(new Rect(50, 100, 100, 30), SIInputThree, style);
            GUI.Box(new Rect(50, 450, 600, 600), ChartTextureThree, style);
            GUI.Label(new Rect(50, 150, 100, 30), Data3, style);
            GUI.Label(new Rect(50, 250, 100, 30), Data5, style);
            GUI.Label(new Rect(250, 250, 100, 30), Data6, style);
            GUI.Label(new Rect(450, 250, 100, 30), Data7, style);
            GUI.Label(new Rect(650, 250, 100, 30), Data8, style);
            GUI.Label(new Rect(50, 350, 100, 30), Data9, style);
            GUI.Label(new Rect(250, 350, 100, 30), Data11, style);
            GUI.Label(new Rect(450, 350, 100, 30), Data12, style);
            GUI.Label(new Rect(650, 350, 100, 30), Data13, style);

            if (StockData > 0)
            {
                GUI.Label(new Rect(250, 150, 100, 30), Data4, PosStyle);
                GUI.Label(new Rect(450, 150, 100, 30), Data10, PosStyle);
            }
            else if (StockData < 0)
            {
                GUI.Label(new Rect(250, 150, 100, 30), Data4, NegStyle);
                GUI.Label(new Rect(450, 150, 100, 30), Data10, NegStyle);
            }
            else
            {
                GUI.Label(new Rect(250, 150, 100, 30), Data4, style);
                GUI.Label(new Rect(450, 150, 100, 30), Data10, style);
            }

            if (GUI.Button(new Rect(950, 10, 100, 50), "Back"))
            {
                CurrentRoom = StockRoom.Intro;
            }


            if (GUI.Button(new Rect(600, 450, 100, 50), "1-Day Chart"))
            {
                SelectRange = 0;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(700, 450, 100, 50), "5-Day Chart"))
            {
                SelectRange = 1;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(800, 450, 100, 50), "3-Month Chart"))
            {
                SelectRange = 2;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(600, 550, 100, 50), "6-Month Chart"))
            {
                SelectRange = 3;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(700, 550, 100, 50), "1-Year Chart"))
            {
                SelectRange = 4;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(800, 550, 100, 50), "2-year Chart"))
            {
                SelectRange = 5;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(600, 650, 100, 50), "5-Year Chart"))
            {
                SelectRange = 6;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(700, 650, 100, 50), "Max Chart"))
            {
                SelectRange = 7;
                StartCoroutine(ShowChart());
            }





            if (GUI.Button(new Rect(150, 100, 160, 30), "Get Data"))
            {
                // Build the URL.
                string url = ""; char[] seperatedata = { ',', ',' };//new char[] { '\r', '\n' }
                if (SIInputThree != "") url += SIInputThree + "+";
                {
                    if (url != "")
                    {


                        // Remove the trailing plus sign.
                        url = url.Substring(0, url.Length - 1);

                        // Prepend the base URL.
                        const string base_url =
                            "http://download.finance.yahoo.com/d/quotes.csv?s=@&f=sl1d1t1c1hgvbap2oc3rd";
                        url = base_url.Replace("@", url);


                        try
                        {
                            string result = GetWebResponse(url);
                            Console.WriteLine(result.Replace("\\r\\", "\r\n"));

                            // Pull out the current prices.
                            string[] lines = result.Split(seperatedata, StringSplitOptions.RemoveEmptyEntries);

                            foreach (string d in lines)
                            {
                                Console.WriteLine(d);
                            }

                            Data0 = lines[2].ToString();
                            Data1 = "Today's Date:" + DateTime.Today.ToString("MM/dd/yy");
                            Data2 = "Time:" + DateTime.Now.ToString();
                            Data3 = "Price:" + lines[1].ToString();
                            Data4 = "Change:" + lines[4].ToString();
                            Data5 = "Day's High:" + lines[5].ToString();
                            Data6 = "Day's Low:" + lines[6].ToString();
                            Data7 = "Volume:" + lines[7].ToString();
                            Data8 = "Bid:" + lines[8].ToString();
                            Data9 = "Ask:" + lines[9].ToString();
                            Data10 = "Change%:" + lines[10].Trim(new char[] { '"' });
                            Data11 = "Open:" + lines[11].ToString();
                            Data12 = "P/E Ratio:" + lines[13].ToString();
                            Data13 = "Dividens:" + lines[14].ToString();

                            StockData = decimal.Parse(lines[4]);





                        }
                        catch
                        {
                            print("Error");
                        }


                    }
                }
            }
        }
        if (CurrentRoom == StockRoom.Room4)
        {
            SIInputFour = GUI.TextField(new Rect(50, 100, 100, 30), SIInputFour, style);
            GUI.Box(new Rect(50, 450, 600, 600), ChartTextureFour, style);
            GUI.Label(new Rect(50, 150, 100, 30), Data3, style);
            GUI.Label(new Rect(50, 250, 100, 30), Data5, style);
            GUI.Label(new Rect(250, 250, 100, 30), Data6, style);
            GUI.Label(new Rect(450, 250, 100, 30), Data7, style);
            GUI.Label(new Rect(650, 250, 100, 30), Data8, style);
            GUI.Label(new Rect(50, 350, 100, 30), Data9, style);
            GUI.Label(new Rect(250, 350, 100, 30), Data11, style);
            GUI.Label(new Rect(450, 350, 100, 30), Data12, style);
            GUI.Label(new Rect(650, 350, 100, 30), Data13, style);

            if (StockData > 0)
            {
                GUI.Label(new Rect(250, 150, 100, 30), Data4, PosStyle);
                GUI.Label(new Rect(450, 150, 100, 30), Data10, PosStyle);
            }
            else if (StockData < 0)
            {
                GUI.Label(new Rect(250, 150, 100, 30), Data4, NegStyle);
                GUI.Label(new Rect(450, 150, 100, 30), Data10, NegStyle);
            }
            else
            {
                GUI.Label(new Rect(250, 150, 100, 30), Data4, style);
                GUI.Label(new Rect(450, 150, 100, 30), Data10, style);
            }

            if (GUI.Button(new Rect(950, 10, 100, 50), "Back"))
            {
                CurrentRoom = StockRoom.Intro;
            }


            if (GUI.Button(new Rect(600, 450, 100, 50), "1-Day Chart"))
            {
                SelectRange = 0;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(700, 450, 100, 50), "5-Day Chart"))
            {
                SelectRange = 1;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(800, 450, 100, 50), "3-Month Chart"))
            {
                SelectRange = 2;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(600, 550, 100, 50), "6-Month Chart"))
            {
                SelectRange = 3;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(700, 550, 100, 50), "1-Year Chart"))
            {
                SelectRange = 4;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(800, 550, 100, 50), "2-year Chart"))
            {
                SelectRange = 5;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(600, 650, 100, 50), "5-Year Chart"))
            {
                SelectRange = 6;
                StartCoroutine(ShowChart());
            }
            if (GUI.Button(new Rect(700, 650, 100, 50), "Max Chart"))
            {
                SelectRange = 7;
                StartCoroutine(ShowChart());
            }




            if (GUI.Button(new Rect(150, 100, 160, 30), "Get Data"))
            {
                // Build the URL.
                string url = ""; char[] seperatedata = { ',', ',' };//new char[] { '\r', '\n' }
                if (SIInputFour != "") url += SIInputFour + "+";
                {
                    if (url != "")
                    {


                        // Remove the trailing plus sign.
                        url = url.Substring(0, url.Length - 1);

                        // Prepend the base URL.
                        const string base_url =
                            "http://download.finance.yahoo.com/d/quotes.csv?s=@&f=sl1d1t1c1hgvbap2oc3rd";
                        url = base_url.Replace("@", url);


                        try
                        {
                            string result = GetWebResponse(url);
                            Console.WriteLine(result.Replace("\\r\\", "\r\n"));

                            // Pull out the current prices.
                            string[] lines = result.Split(seperatedata, StringSplitOptions.RemoveEmptyEntries);

                            foreach (string d in lines)
                            {
                                Console.WriteLine(d);
                            }

                            Data0 = lines[2].ToString();
                            Data1 = "Today's Date:" + DateTime.Today.ToString("MM/dd/yy");
                            Data2 = "Time:" + DateTime.Now.ToString();
                            Data3 = "Price:" + lines[1].ToString();
                            Data4 = "Change:" + lines[4].ToString();
                            Data5 = "Day's High:" + lines[5].ToString();
                            Data6 = "Day's Low:" + lines[6].ToString();
                            Data7 = "Volume:" + lines[7].ToString();
                            Data8 = "Bid:" + lines[8].ToString();
                            Data9 = "Ask:" + lines[9].ToString();
                            Data10 = "Change%:" + lines[10].Trim(new char[] { '"' });
                            Data11 = "Open:" + lines[11].ToString();
                            Data12 = "P/E Ratio:" + lines[13].ToString();
                            Data13 = "Dividens:" + lines[14].ToString();

                            StockData = decimal.Parse(lines[4]);





                        }
                        catch
                        {
                            print("Error");
                        }


                    }
                }
            }
        }
    }





    private string GetWebResponse(string url)
    {
        // Make a WebClient.
        WebClient web_client = new WebClient();

        // Get the indicated URL.
        Stream response = web_client.OpenRead(url);

        // Read the result.
        using (StreamReader stream_reader = new StreamReader(response))
        {
            // Get the results.
            string result = stream_reader.ReadToEnd();

            // Close the stream reader and its underlying stream.
            stream_reader.Close();

            // Return the result.
            return result;
        }
    }

    private void GetData()
    {
        if (SelectRoom == 0)
        {
            // Build the URL.
            string url = ""; char[] seperatedata = { ',', ',' };//new char[] { '\r', '\n' }
            if (SIInputOne != "") url += SIInputOne + "+";
            {
                if (url != "")
                {


                    // Remove the trailing plus sign.
                    url = url.Substring(0, url.Length - 1);

                    // Prepend the base URL.
                    const string base_url =
                        "http://download.finance.yahoo.com/d/quotes.csv?s=@&f=sl1d1t1c1hgvbap2oc3rd";
                    url = base_url.Replace("@", url);


                    try
                    {
                        string result = GetWebResponse(url);
                        Console.WriteLine(result.Replace("\\r\\", "\r\n"));

                        // Pull out the current prices.
                        string[] lines = result.Split(seperatedata, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string d in lines)
                        {
                            Console.WriteLine(d);
                        }

                        Data0 = lines[2].ToString();
                        Data1 = "Today's Date:" + DateTime.Today.ToString("MM/dd/yy");
                        Data2 = "Time:" + DateTime.Now.ToString();
                        Data3 = "Price:" + lines[1].ToString();
                        Data4 = "Change:" + lines[4].ToString();
                        Data5 = "Day's High:" + lines[5].ToString();
                        Data6 = "Day's Low:" + lines[6].ToString();
                        Data7 = "Volume:" + lines[7].ToString();
                        Data8 = "Bid:" + lines[8].ToString();
                        Data9 = "Ask:" + lines[9].ToString();
                        Data10 = "Change%:" + lines[10].Trim(new char[] { '"' });
                        Data11 = "Open:" + lines[11].ToString();
                        Data12 = "P/E Ratio:" + lines[13].ToString();
                        Data13 = "Dividens:" + lines[14].ToString();

                        StockData = decimal.Parse(lines[4]);
                    }
                    catch
                    {
                        print("Error");
                    }


                }
            }
        }
        if (SelectRoom == 1)
        {
            // Build the URL.
            string url = ""; char[] seperatedata = { ',', ',' };//new char[] { '\r', '\n' }
            if (SIInputTwo != "") url += SIInputTwo + "+";
            {
                if (url != "")
                {


                    // Remove the trailing plus sign.
                    url = url.Substring(0, url.Length - 1);

                    // Prepend the base URL.
                    const string base_url =
                        "http://download.finance.yahoo.com/d/quotes.csv?s=@&f=sl1d1t1c1hgvbap2oc3rd";
                    url = base_url.Replace("@", url);


                    try
                    {
                        string result = GetWebResponse(url);
                        Console.WriteLine(result.Replace("\\r\\", "\r\n"));

                        // Pull out the current prices.
                        string[] lines = result.Split(seperatedata, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string d in lines)
                        {
                            Console.WriteLine(d);
                        }

                        Data0 = lines[2].ToString();
                        Data1 = "Today's Date:" + DateTime.Today.ToString("MM/dd/yy");
                        Data2 = "Time:" + DateTime.Now.ToString();
                        Data3 = "Price:" + lines[1].ToString();
                        Data4 = "Change:" + lines[4].ToString();
                        Data5 = "Day's High:" + lines[5].ToString();
                        Data6 = "Day's Low:" + lines[6].ToString();
                        Data7 = "Volume:" + lines[7].ToString();
                        Data8 = "Bid:" + lines[8].ToString();
                        Data9 = "Ask:" + lines[9].ToString();
                        Data10 = "Change%:" + lines[10].Trim(new char[] { '"' });
                        Data11 = "Open:" + lines[11].ToString();
                        Data12 = "P/E Ratio:" + lines[13].ToString();
                        Data13 = "Dividens:" + lines[14].ToString();

                        StockData = decimal.Parse(lines[4]);
                    }
                    catch
                    {
                        print("Error");
                    }


                }
            }
        }
        if (SelectRoom == 2)
        {
            // Build the URL.
            string url = ""; char[] seperatedata = { ',', ',' };//new char[] { '\r', '\n' }
            if (SIInputThree != "") url += SIInputThree + "+";
            {
                if (url != "")
                {


                    // Remove the trailing plus sign.
                    url = url.Substring(0, url.Length - 1);

                    // Prepend the base URL.
                    const string base_url =
                        "http://download.finance.yahoo.com/d/quotes.csv?s=@&f=sl1d1t1c1hgvbap2oc3rd";
                    url = base_url.Replace("@", url);


                    try
                    {
                        string result = GetWebResponse(url);
                        Console.WriteLine(result.Replace("\\r\\", "\r\n"));

                        // Pull out the current prices.
                        string[] lines = result.Split(seperatedata, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string d in lines)
                        {
                            Console.WriteLine(d);
                        }

                        Data0 = lines[2].ToString();
                        Data1 = "Today's Date:" + DateTime.Today.ToString("MM/dd/yy");
                        Data2 = "Time:" + DateTime.Now.ToString();
                        Data3 = "Price:" + lines[1].ToString();
                        Data4 = "Change:" + lines[4].ToString();
                        Data5 = "Day's High:" + lines[5].ToString();
                        Data6 = "Day's Low:" + lines[6].ToString();
                        Data7 = "Volume:" + lines[7].ToString();
                        Data8 = "Bid:" + lines[8].ToString();
                        Data9 = "Ask:" + lines[9].ToString();
                        Data10 = "Change%:" + lines[10].Trim(new char[] { '"' });
                        Data11 = "Open:" + lines[11].ToString();
                        Data12 = "P/E Ratio:" + lines[13].ToString();
                        Data13 = "Dividens:" + lines[14].ToString();

                        StockData = decimal.Parse(lines[4]);
                    }
                    catch
                    {
                        print("Error");
                    }


                }
            }
        }
        if (SelectRoom == 3)
        {
            // Build the URL.
            string url = ""; char[] seperatedata = { ',', ',' };//new char[] { '\r', '\n' }
            if (SIInputFour != "") url += SIInputFour + "+";
            {
                if (url != "")
                {


                    // Remove the trailing plus sign.
                    url = url.Substring(0, url.Length - 1);

                    // Prepend the base URL.
                    const string base_url =
                        "http://download.finance.yahoo.com/d/quotes.csv?s=@&f=sl1d1t1c1hgvbap2oc3rd";
                    url = base_url.Replace("@", url);


                    try
                    {
                        string result = GetWebResponse(url);
                        Console.WriteLine(result.Replace("\\r\\", "\r\n"));

                        // Pull out the current prices.
                        string[] lines = result.Split(seperatedata, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string d in lines)
                        {
                            Console.WriteLine(d);
                        }

                        Data0 = lines[2].ToString();
                        Data1 = "Today's Date:" + DateTime.Today.ToString("MM/dd/yy");
                        Data2 = "Time:" + DateTime.Now.ToString();
                        Data3 = "Price:" + lines[1].ToString();
                        Data4 = "Change:" + lines[4].ToString();
                        Data5 = "Day's High:" + lines[5].ToString();
                        Data6 = "Day's Low:" + lines[6].ToString();
                        Data7 = "Volume:" + lines[7].ToString();
                        Data8 = "Bid:" + lines[8].ToString();
                        Data9 = "Ask:" + lines[9].ToString();
                        Data10 = "Change%:" + lines[10].Trim(new char[] { '"' });
                        Data11 = "Open:" + lines[11].ToString();
                        Data12 = "P/E Ratio:" + lines[13].ToString();
                        Data13 = "Dividens:" + lines[14].ToString();

                        StockData = decimal.Parse(lines[4]);
                    }
                    catch
                    {
                        print("Error");
                    }


                }
            }
        }


    }


    private  void getPrices()
    {

        string url = "";
        if (SIInputOne != "") url += SIInputOne + "+";
        if (SIInputTwo != "") url += SIInputTwo + "+";
        if (SIInputThree != "") url += SIInputThree + "+";
        if (SIInputFour != "") url += SIInputFour + "+";
        if (url != "")
        {
            // Remove the trailing plus sign.
            url = url.Substring(0, url.Length - 1);

            // Prepend the base URL.
            const string base_url =
                "http://download.finance.yahoo.com/d/quotes.csv?s=@&f=sl1d1t1c1p2";
            url = base_url.Replace("@", url);
            // Get the response.
            try
            {
                string result = GetWebResponse(url);

                print(result.Replace("\\r\\n", "\r\n"));
                // Pull out the current prices.
                string[] lines = result.Split(
                    new char[] { '\r', '\n' },
                    StringSplitOptions.RemoveEmptyEntries);



                SIPricesOne = decimal.Parse(lines[0].Split(',')[1]).ToString("C3");
                SIPricesTwo = decimal.Parse(lines[1].Split(',')[1]).ToString("C3");
                SIPricesThree = decimal.Parse(lines[2].Split(',')[1]).ToString("C3");
                SIPricesFour = decimal.Parse(lines[3].Split(',')[1]).ToString("C3");

                SIChangeOne = decimal.Parse(lines[0].Split(',')[4]);
                SIChangeTwo = decimal.Parse(lines[1].Split(',')[4]);
                SIChangeThree = decimal.Parse(lines[2].Split(',')[4]);
                SIChangeFour = decimal.Parse(lines[3].Split(',')[4]);

                SIChangePercentOne = lines[0].Split(',')[5].Trim(new char[] { '"' });
                SIChangePercenTwo = lines[1].Split(',')[5].Trim(new char[] { '"' });
                SIChangePercenThree = lines[2].Split(',')[5].Trim(new char[] { '"' });
                SIChangePercenFour = lines[3].Split(',')[5].Trim(new char[] { '"' });








            }
            catch //(Exception ex)
            {
                //MessageBox.ToString(ex.Message, "Read Error",MessageBoxButtons.GetType, MessageBoxIcon.GetType);
                print("Error");
            }
        }

    
}

   


}
