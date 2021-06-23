using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EmbeddedFont_Net4
{
    /// <summary>
    /// 基于文件流进行读取
    /// </summary>
    public static class MergeFilesHelper
    {
        public static string MergeFiles(string targetPdfFilesDir)
        {
            string outPath = string.Empty;
            //验证文件是否存在
            if (!Directory.Exists(targetPdfFilesDir))
            {
                throw new FileNotFoundException("指定的目录不存在：" + targetPdfFilesDir);
            }

            var filePathList = Directory.EnumerateFiles(targetPdfFilesDir, "*.pdf");
            if (!filePathList.Any())
            {
                return outPath;
            }

            //合并pdf文件

            string runningDir = AppDomain.CurrentDomain.BaseDirectory;

            //outPath = Path.Combine(runningDir, "temp", Guid.NewGuid().ToString() + ".pdf");
            outPath = @"C:\Users\Administrator\Desktop\aa.pdf";

            MergeFiles(outPath, filePathList.ToArray());

            return outPath;
        }


        public static void MergeFiles(string outPath, string[] pdfFiles)
        {

            try
            {

                if (pdfFiles == null || pdfFiles.Length <= 0)
                {
                    return;
                }
                if (pdfFiles.Length == 1)
                {
                    return;
                }

                PdfReader reader;
                Document document;
                PdfWriter writer;

                using (FileStream fs = new FileStream(outPath, FileMode.Create))
                {
                    reader = new PdfReader(pdfFiles[0]);
                    using (document = new Document())
                    {
                        //一个PdfSmartCopy基类
                        writer = new PdfSmartCopy(document, fs);
                        document.Open();

                        for (int k = 0; k < pdfFiles.Length; k++)
                        {
                            reader = new PdfReader(pdfFiles[k]);
                            //将子文件中的页都追加到尾部
                            for (int i = 1; i < reader.NumberOfPages + 1; i++)
                            {
                                ((PdfSmartCopy)writer).AddPage(writer.GetImportedPage(reader, i));
                            }
                            writer.FreeReader(reader);
                        }
                        reader.Close();
                        writer.Close();
                        document.Close();
                    }
                }

            }
            catch (Exception e)
            {
                string strOb = e.Message;
            }
        }
    }

    public static class MergeFilesHelper_Old
    {
        public static void MergeFiles(string destinationFile, string[] sourceFiles)
        {

            try
            {
                int f = 0;
                // we create a reader for a certain document
                PdfReader reader = new PdfReader(sourceFiles[f]);
                // we retrieve the total number of pages
                int n = reader.NumberOfPages;
                //Console.WriteLine("There are " + n + " pages in the original file.");
                // step 1: creation of a document-object
                Document document = new Document(reader.GetPageSizeWithRotation(1));
                // step 2: we create a writer that listens to the document
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationFile, FileMode.Create));
                // step 3: we open the document
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;
                int rotation;
                // step 4: we add content
                while (f < sourceFiles.Length)
                {
                    int i = 0;
                    while (i < n)
                    {
                        i++;
                        document.SetPageSize(reader.GetPageSizeWithRotation(i));
                        document.NewPage();
                        page = writer.GetImportedPage(reader, i);
                        rotation = reader.GetPageRotation(i);
                        if (rotation == 90 || rotation == 270)
                        {
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        }
                        else
                        {
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        }
                        //Console.WriteLine("Processed page " + i);
                    }
                    f++;
                    if (f < sourceFiles.Length)
                    {
                        reader = new PdfReader(sourceFiles[f]);
                        // we retrieve the total number of pages
                        n = reader.NumberOfPages;
                        //Console.WriteLine("There are " + n + " pages in the original file.");
                    }
                }
                // step 5: we close the document
                document.Close();
            }
            catch (Exception e)
            {
                string strOb = e.Message;
            }
        }

        public static void Merge(List<String> InFiles, String OutFile)
        {

            using (FileStream stream = new FileStream(OutFile, FileMode.Create))
            using (Document doc = new Document())
            using (PdfCopy pdf = new PdfCopy(doc, stream))
            {
                doc.Open();

                PdfReader reader = null;
                PdfImportedPage page = null;

                //fixed typo
                InFiles.ForEach(file =>
                {
                    reader = new PdfReader(file);

                    for (int i = 0; i < reader.NumberOfPages; i++)
                    {
                        page = pdf.GetImportedPage(reader, i + 1);
                        pdf.AddPage(page);
                    }

                    pdf.FreeReader(reader);
                    reader.Close();
                    File.Delete(file);
                });
            }
        }
    }
}


