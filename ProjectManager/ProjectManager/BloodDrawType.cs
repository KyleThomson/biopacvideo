﻿using System;

namespace ProjectManager
{
    public class BloodDrawType
    {
        public DateTime dt;
        public TimeSpan EnteredTime;
        public string ID;
        public BloodDrawType(string a, string b, string c)
        {
            DateTime.TryParse(a, out dt);
            TimeSpan.TryParse(b, out EnteredTime);
            ID = c;
        }

    }
}
