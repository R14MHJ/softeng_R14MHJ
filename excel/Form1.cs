using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;

namespace excel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateExcel();
        }
        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;
        public void CreateExcel() 
        {
            try
            {
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWB.ActiveSheet;
                CreateTable();
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                string error = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(error, "Error");
                xlWB.Close(false,Type.Missing,Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }   
        }
        public void CreateTable()
        {
            string[] fejlecek = new string[] 
            {
                "Kérdés",
                "1. Válasz",
                "2. Válasz",
                "3. Válasz",
                "Helyes Válasz",
                "Kép"
            };
            xlSheet.Cells[1,1] = fejlecek[0];
            Models.HajosContext hajosContext = new Models.HajosContext();
            var QALL = hajosContext.Questions.ToList();
            object[,] adatTömb = new object[QALL.Count(), fejlecek.Count()];
            for (int i = 0; i < QALL.Count; i++)
            {
                adatTömb[i, 0] = QALL[i].Question1;
                adatTömb[i, 1] = QALL[i].Answer1;
                adatTömb[i, 2] = QALL[i].Answer2;
                adatTömb[i, 3] = QALL[i].Answer3;
                adatTömb[i, 4] = QALL[i].CorrectAnswer;
                adatTömb[i, 5] = QALL[i].Image;
            }
            int sorszam = adatTömb.GetLength(0);
            int oszlopszam = adatTömb.GetLength(1);
            Excel.Range adatRange = xlSheet.get_Range("A2",Type.Missing).get_Resize(sorszam,oszlopszam);
            adatRange.Value2 = adatTömb;
            adatRange.Columns.AutoFit();
            Excel.Range fejlécRange = xlSheet.get_Range("A1", Type.Missing).get_Resize(1, 6);
            fejlécRange.Font.Bold = true;
            fejlécRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            fejlécRange.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
            fejlécRange.EntireColumn.AutoFit();
            fejlécRange.RowHeight = 40;
            fejlécRange.Interior.Color = Color.Fuchsia;
            fejlécRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);
            int lastrowID = xlSheet.UsedRange.Rows.Count;
            Excel.Range tabla = xlSheet.get_Range("A1",Type.Missing).get_Resize(lastrowID, 6);
            tabla.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);
            Excel.Range elsooszlop = xlSheet.get_Range("A1", Type.Missing).get_Resize(lastrowID, 1);
            elsooszlop.Font.Bold = true;
            elsooszlop.Interior.Color = Color.LightYellow;
            Excel.Range utolsooszlop = xlSheet.get_Range("E2", Type.Missing).get_Resize(lastrowID,1);
            utolsooszlop.Interior.Color = Color.LightGreen;
            utolsooszlop.NumberFormat = "#,##0.00";
        }
        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int div = y;
            int modulo;
            while (div > 0)
            {
                modulo = (div - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                div = (int)((div - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();
            return ExcelCoordinate;
        }
    }
}