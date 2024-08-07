using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Ini
{
    class IniFile
    {
        #region Properties
        public string path;
        #endregion

        #region DLL Imports
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);
        #endregion

        #region Initializers
        public IniFile(string INIPath)
        {
            this.path = INIPath;
        }
        #endregion

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }
        public string IniReadValue(string Section, string Key, string Def)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, (Def != null ? Def : string.Empty), temp, 255, this.path);
            return temp.ToString();
        }
        public int IniReadValue(string Section, string Key, int Def2)
        {
            StringBuilder temp = new StringBuilder(255);
            string Def = Def2.ToString();
            int i = GetPrivateProfileString(Section, Key, (Def != null ? Def : string.Empty), temp, 255, this.path);
            return int.Parse(temp.ToString());
        }
        public double IniReadValue(string Section, string Key, double Def2)
        {
            StringBuilder temp = new StringBuilder(255);
            string Def = Def2.ToString();
            int i = GetPrivateProfileString(Section, Key, (Def != null ? Def : string.Empty), temp, 255, this.path);
            return double.Parse(temp.ToString());
        }
        public DateTime IniReadValue(string Section, string Key)
        {
          DateTime New;
            StringBuilder temp = new StringBuilder(255);
          GetPrivateProfileString(Section, Key, string.Empty, temp, 255, this.path);
          if (DateTime.TryParse(temp.ToString(), out New))
              return New.Date;
          else
              return DateTime.MinValue;
        }
        public void IniReadValue(string Section, string Key, out TimeSpan New)
        {
            
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, string.Empty, temp, 255, this.path);
            if (!TimeSpan.TryParse(temp.ToString(), out New))
            {
                New = TimeSpan.MaxValue;
            }            
        }
        public void IniWriteValue(string Section, string Key, int Value)
        {
            IniWriteValue(Section, Key, Value.ToString());
        }
        public void IniWriteValue(string Section, string Key, bool Value)
        {
            if (Value)
                IniWriteValue(Section, Key, "true");
            else
              IniWriteValue(Section, Key, "false");
        }
        public void IniWriteValue(string Section, string Key, double Value)
        {
            IniWriteValue(Section, Key, Value.ToString());
        }
        public bool IniReadValue(string Section, string Key, bool Def2)
        {   
            string Def;
            if (Def2)
             Def = "true";
            else 
              Def = "false";
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, Def, temp, 255, this.path);
            if (temp.ToString() == "true")
                return true;
            else
                return false;
        }
    }
}
