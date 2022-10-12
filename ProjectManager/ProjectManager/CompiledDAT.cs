using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Xml;

namespace ProjectManager
{
    public class CompiledDAT
    {
        public int numDats;
        public bool OverWrite;
        public long HeaderLength;
        string pjtName;
        FileStream BinaryFile;
        BinaryWriter BinaryFileOut;
        FileStream CurrentDatFile;
        Int32[] ACQPos;
        Int32[][] data;
        public List<AnimalType> AnimalList;

        public CompiledDAT(List<AnimalType> AL)
        {
            AnimalList = AL;
            if (AnimalList.Count < 1) return;
            numDats = 0;
            
            foreach (AnimalType AT in AnimalList)
            {
                //numDats += AT.SZF.Count;
            }
            data = new Int32[numDats][];
            ACQPos = new Int32[numDats];

            //loadData();

        }

        public CompiledDAT(List<AnimalType> AL, bool OW)
        {
            OverWrite = OW;


        }


        //public void loadData()
        //{

        //    int index = 0;
        //    for (int i = 0; i < AnimalList.Count; i++)
        //    {
        //        for (int j = 0; j < AnimalList[i].SZF.Count; j++)
        //        {
        //            string Path = AnimalList[i].SZF[j];
        //            FileStream tempF = new FileStream(Path, FileMode.Open, FileAccess.Read);
        //            data[index] = new int[tempF.Length * 4];
                   

        //        }
                    
                

                
       



    }
}
