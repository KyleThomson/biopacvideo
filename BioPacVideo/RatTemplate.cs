﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioPacVideo
{
    public class RatTemplate
    {
        public double Weight;
        public DateTime FirstSeizure;
        public DateTime Injection;
        public DateTime Surgery;
        public bool Medication;
        public RatTemplate()
        {
            Weight = new double();
            FirstSeizure = new DateTime();
            Injection = new DateTime();
            Surgery = new DateTime();
            Medication = new bool();
        }
        static public RatTemplate[] NewInitArray(ulong num)
        {
            RatTemplate[] arrSC = new RatTemplate[num];
            for (ulong i = 0; i < num; i++)
            {
                arrSC[i] = new RatTemplate();
            }   
            return arrSC;
        }
    }
}