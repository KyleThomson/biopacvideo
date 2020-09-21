using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace SeizureHeatmap
{
    class Excel
    {
        // global refs
        string path = "";
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;

        public Excel()
        {

        }
        public Excel(string path, int Sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[Sheet];
        }

        public  void CreateNewFile()
        {
            this.wb = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            this.ws = wb.Worksheets[1]; // first worksheet
        }

        public void CreateNewSheet()
        {
            Worksheet temptsheet = wb.Worksheets.Add(After: ws);
        }

        public void WriteToCell(int i, int j, string s)
        {
            //Excel starts at 1 not zero so step up i and j by 1
            i++;
            j++;
            ws.Cells[i, j].Value2 = s;
        }
        public void WriteRange(int starti, int starty, int endi, int endy, int[,] writeint)
        {
            Range range = (Range)ws.Range[ws.Cells[starti, starty], ws.Cells[endi, endy]];
            range.Value2 = writeint;
        }

        public void Save()
        {
            wb.Save();
        }

        public void SaveAs(string mainPath)
        {
           wb.SaveAs(mainPath);
        }

        public void Close()
        {
            wb.Close();
        }
    }
}
