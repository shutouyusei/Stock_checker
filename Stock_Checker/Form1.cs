﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace Stock_Checker
{
    public partial class Form1 : Form
    {

        [System.Runtime.InteropServices.DllImport("kernel32.dll")] // この行を追加
        private static extern bool AllocConsole();                 // この行を追加
        public Form1()
        {
            InitializeComponent();
            // チャートの表示を初期化
            AllocConsole(); //デバック用
        }

        private　void ShowGraph()
        {
            //windowsフォームチャートを表示
            Show_stock show_Stock = new Show_stock();
            string[,] csv_data = show_Stock.Read_csv("7177");
            //データを追加

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string stock_code=Stock_code_text.Text;
            if (stock_code.Length == 4)
            {
                int code;
                if(int.TryParse(stock_code, out code))
                {

                    //現在のディレクトリを取得
                    string currentDirectory = System.IO.Directory.GetCurrentDirectory();
                    //Console.WriteLine(currentDirectory);

                    ProcessStartInfo startInfo = new ProcessStartInfo(currentDirectory+"/Stock_data/Fetch_stock_py.exe");
                   //現在はデバックのためWindowStyleをNormalに設定->Hiddenに変える
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                    //ユーザーからの引数を用いてexe発行
                    startInfo.Arguments=stock_code;
                    Process p=Process.Start(startInfo);
                    p.WaitForExit();

                    Show_stock show_Stock = new Show_stock();
                    //string[,] csv_data =show_Stock.Read_csv(stock_code);
                    int[] open = show_Stock.Get_High(stock_code);
                }

            }
        }
    }
}
