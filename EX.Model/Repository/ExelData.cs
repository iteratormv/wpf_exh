using EX.Model.DbLayer;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX.Model.Repository
{
    public class ExelData
    {

        public int excelWorksheetRow { get; }
        public int excelWorksheetCol { get; }
        public string[,] data { get; }

        public ExelData()
        {
            Microsoft.Office.Interop.Excel.Application excel_app =
                new Microsoft.Office.Interop.Excel.Application();

            string file_name = "E:\\iterator\\exhibition\\архив\\pharma\\Копия Pharma Ukraine 7-8 November.xls";

            Workbook work_book = excel_app.Workbooks.Open(file_name, Type.Missing);

            Worksheet work_shet = (Worksheet)work_book.Worksheets[1];

            Range excelRange = work_shet.UsedRange;

            object[,] vallueArray = (object[,])excelRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);

            excelWorksheetRow = work_shet.UsedRange.Rows.Count;
            excelWorksheetCol = 15;
            //      excelWorksheetCol = work_shet.UsedRange.Columns.Count;
            data = new string[excelWorksheetRow, excelWorksheetCol];

            for (int row = 1; row <= excelWorksheetRow; ++row)
            {
                for (int col = 1; col <= excelWorksheetCol; ++col)
                {
                    try
                    {
                        if (vallueArray[row, col] == null) data[row - 1, col - 1] = "empty";
                        else data[row - 1, col - 1] = vallueArray[row, col].ToString();
                    }
                    catch { data[row - 1, col - 1] = "none"; }
                }
            }
        }

        public ExelData(string fName)
        {
 //           Progress_Bar progress = new Progress_Bar { Visible = true, Progress = 0, Status = "Extracr data forom file" };


            Microsoft.Office.Interop.Excel.Application excel_app =
                new Microsoft.Office.Interop.Excel.Application();

            string[] partsPath = fName.Split('\\');
            foreach (string s in partsPath)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("\n" + partsPath.Length.ToString());
            string res = "";
            for (int i = 0; i < partsPath.Length - 1; i++)
            {
                res += partsPath[i] + "\\";
            }
            res += partsPath[partsPath.Length - 1];

            string file_name = res;

            Workbook work_book = excel_app.Workbooks.Open(file_name, Type.Missing);

            Worksheet work_shet = (Worksheet)work_book.Worksheets[1];

            Range excelRange = work_shet.UsedRange;

            object[,] vallueArray = (object[,])excelRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);

            excelWorksheetRow = work_shet.UsedRange.Rows.Count;
            excelWorksheetCol = 15;
            //           excelWorksheetCol = work_shet.UsedRange.Columns.Count;

            data = new string[excelWorksheetRow, excelWorksheetCol];

            for (int row = 1; row <= excelWorksheetRow; ++row)
            {
 //               progress.Progress = (row * 100 / excelWorksheetRow);
                for (int col = 1; col <= excelWorksheetCol; ++col)
                {
                    try
                    {
                        if (vallueArray[row, col] == null) data[row - 1, col - 1] = "empty";
                        else data[row - 1, col - 1] = vallueArray[row, col].ToString();
                    }
                    catch { data[row - 1, col - 1] = "none"; }
                }
            }
        }

        //public ExelData(string fName, Action<Progress_Bar> progressChanged_)
        //{
        //    Progress_Bar progress = new Progress_Bar { Visible = true, Progress = 0, Status = "Extracr data forom file" };


        //    Microsoft.Office.Interop.Excel.Application excel_app =
        //        new Microsoft.Office.Interop.Excel.Application();

        //    string[] partsPath = fName.Split('\\');
        //    foreach (string s in partsPath)
        //    {
        //        Console.WriteLine(s);
        //    }
        //    Console.WriteLine("\n" + partsPath.Length.ToString());
        //    string res = "";
        //    for (int i = 0; i < partsPath.Length - 1; i++)
        //    {
        //        res += partsPath[i] + "\\";
        //    }
        //    res += partsPath[partsPath.Length - 1];

        //    string file_name = res;

        //    Workbook work_book = excel_app.Workbooks.Open(file_name, Type.Missing);

        //    Worksheet work_shet = (Worksheet)work_book.Worksheets[1];

        //    Range excelRange = work_shet.UsedRange;

        //    object[,] vallueArray = (object[,])excelRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);

        //    excelWorksheetRow = work_shet.UsedRange.Rows.Count;
        //    excelWorksheetCol = 15;
        //    //           excelWorksheetCol = work_shet.UsedRange.Columns.Count;

        //    data = new string[excelWorksheetRow, excelWorksheetCol];

        //    for (int row = 1; row <= excelWorksheetRow; ++row)
        //    {
        //        progress.Progress = (row * 100 / excelWorksheetRow);
        //        progressChanged_(progress);
        //        for (int col = 1; col <= excelWorksheetCol; ++col)
        //        {
        //            try
        //            {
        //                if (vallueArray[row, col] == null) data[row - 1, col - 1] = "empty";
        //                else data[row - 1, col - 1] = vallueArray[row, col].ToString();
        //            }
        //            catch { data[row - 1, col - 1] = "none"; }
        //        }
        //    }
        //}

        public void setDataToCollection(DbSet<Visitor> visitorCollection)
        {
            //         progress_Bar.Status = "Add Exel data to database";
            for (int row = 0; row < excelWorksheetRow; row++)
            {
                Visitor visitor = new Visitor();
                //            progress_Bar.Progress = row * 100 / excelWorksheetRow ;

                if (data[row, 0] != null) visitor.Collumn1 = data[row, 0]; else visitor.Collumn1 = "none";
                if (data[row, 1] != null) visitor.Collumn2 = data[row, 1]; else visitor.Collumn2 = "none";
                if (data[row, 2] != null) visitor.Collumn3 = data[row, 2]; else visitor.Collumn3 = "none";
                if (data[row, 3] != null) visitor.Collumn4 = data[row, 3]; else visitor.Collumn4 = "none";
                if (data[row, 4] != null) visitor.Collumn5 = data[row, 4]; else visitor.Collumn5 = "none";
                if (data[row, 5] != null) visitor.Collumn6 = data[row, 5]; else visitor.Collumn6 = "none";
                if (data[row, 6] != null) visitor.Collumn7 = data[row, 6]; else visitor.Collumn7 = "none";
                if (data[row, 7] != null) visitor.Collumn8 = data[row, 7]; else visitor.Collumn8 = "none";
                if (data[row, 8] != null) visitor.Collumn9 = data[row, 8]; else visitor.Collumn9 = "none";
                if (data[row, 9] != null) visitor.Collumn10 = data[row, 9]; else visitor.Collumn10 = "none";
                if (data[row, 10] != null) visitor.Collumn11 = data[row, 10]; else visitor.Collumn11 = "none";
                if (data[row, 11] != null) visitor.Collumn12 = data[row, 11]; else visitor.Collumn12 = "none";
                if (data[row, 12] != null) visitor.Collumn13 = data[row, 12]; else visitor.Collumn13 = "none";
                if (data[row, 13] != null) visitor.Collumn14 = data[row, 13]; else visitor.Collumn14 = "none";
                if (data[row, 14] != null) visitor.Collumn15 = data[row, 14]; else visitor.Collumn15 = "none";
 //               visitor.userId = 50000;
                visitorCollection.Add(visitor);
            }

            //         progress_Bar.Status = "All data added to database";
            //        progress_Bar.Visible = false;
            //        progress_Bar.Progress = 0;
        }

        //public void setDataToCollection(DbSet<Visitor> visitorCollection, Action<Progress_Bar> progressChanged_)
        //{
        //    Progress_Bar progress = new Progress_Bar { Visible = true, Progress = 0, Status = "Add Exel data to database" };

        //    for (int row = 0; row < excelWorksheetRow; row++)
        //    {
        //        Visitor visitor = new Visitor();
        //        progress.Progress = row * 100 / excelWorksheetRow;
        //        progressChanged_(progress);

        //        if (data[row, 0] != null) visitor.Collumn1 = data[row, 0]; else visitor.Collumn1 = "none";
        //        if (data[row, 1] != null) visitor.Collumn2 = data[row, 1]; else visitor.Collumn2 = "none";
        //        if (data[row, 2] != null) visitor.Collumn3 = data[row, 2]; else visitor.Collumn3 = "none";
        //        if (data[row, 3] != null) visitor.Collumn4 = data[row, 3]; else visitor.Collumn4 = "none";
        //        if (data[row, 4] != null) visitor.Collumn5 = data[row, 4]; else visitor.Collumn5 = "none";
        //        if (data[row, 5] != null) visitor.Collumn6 = data[row, 5]; else visitor.Collumn6 = "none";
        //        if (data[row, 6] != null) visitor.Collumn7 = data[row, 6]; else visitor.Collumn7 = "none";
        //        if (data[row, 7] != null) visitor.Collumn8 = data[row, 7]; else visitor.Collumn8 = "none";
        //        if (data[row, 8] != null) visitor.Collumn9 = data[row, 8]; else visitor.Collumn9 = "none";
        //        if (data[row, 9] != null) visitor.Collumn10 = data[row, 9]; else visitor.Collumn10 = "none";
        //        if (data[row, 10] != null) visitor.Collumn11 = data[row, 10]; else visitor.Collumn11 = "none";
        //        if (data[row, 11] != null) visitor.Collumn12 = data[row, 11]; else visitor.Collumn12 = "none";
        //        if (data[row, 12] != null) visitor.Collumn13 = data[row, 12]; else visitor.Collumn13 = "none";
        //        if (data[row, 13] != null) visitor.Collumn14 = data[row, 13]; else visitor.Collumn14 = "none";
        //        if (data[row, 14] != null) visitor.Collumn15 = data[row, 14]; else visitor.Collumn15 = "none";

        //        visitorCollection.Add(visitor);
        //    }
        //}
    }
}
