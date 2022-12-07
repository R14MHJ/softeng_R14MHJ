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
                "K�rd�s",
                "1. V�lasz",
                "2. V�lasz",
                "3. V�lasz",
                "Helyes V�lasz",
                "K�p"
            };
            xlSheet.Cells[1,1] = fejlecek[0];
            Models.HajosContext hajosContext = new Models.HajosContext();
            var QALL = hajosContext.Questions.ToList();
            object[,] adatT�mb = new object[QALL.Count(), fejlecek.Count()];
            for (int i = 0; i < QALL.Count; i++)
            {
                adatT�mb[i, 0] = QALL[i].Question1;
                adatT�mb[i, 1] = QALL[i].Answer1;
                adatT�mb[i, 2] = QALL[i].Answer2;
                adatT�mb[i, 3] = QALL[i].Answer3;
                adatT�mb[i, 4] = QALL[i].CorrectAnswer;
                adatT�mb[i, 5] = QALL[i].Image;
            }
            int sorszam = adatT�mb.GetLength(0);
            int oszlopszam = adatT�mb.GetLength(1);
            Excel.Range adatRange = xlSheet.get_Range("A2",Type.Missing).get_Resize(sorszam,oszlopszam);
            adatRange.Value2 = adatT�mb;
            adatRange.Columns.AutoFit();
            Excel.Range fejl�cRange = xlSheet.get_Range("A1", Type.Missing).get_Resize(1, 6);
            fejl�cRange.Font.Bold = true;
            fejl�cRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            fejl�cRange.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
            fejl�cRange.EntireColumn.AutoFit();
            fejl�cRange.RowHeight = 40;
            fejl�cRange.Interior.Color = Color.Fuchsia;
            fejl�cRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);
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