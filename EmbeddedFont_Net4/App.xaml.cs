using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace EmbeddedFont_Net4
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);



            {
                //FileStream fs = new FileStream(@"C:\Users\Administrator\Desktop\123.pdf", FileMode.Create);
                //Document doc = new Document(PageSize.A4, 36f, 36f, 36f, 36f);

                //PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                //doc.Open();
                //doc.NewPage();


                //string fontPath = @"C:\Users\Administrator\Desktop\";
                //BaseFont bfSong = BaseFont.CreateFont(fontPath + "EpointSpecialFont2.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                //Font font = new Font(bfSong, 16, 1, BaseColor.BLACK);
                //PdfContentByte cb = writer.DirectContent;
                //cb.BeginText();
                //cb.SetFontAndSize(bfSong, 16);

                //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "测试文本", 100, 100, 0);

                //doc.Add(new iTextSharp.text.Paragraph("Hello World! Hello People! Hello Sky! Hello Sun! Hello Moon! Hello Stars!", font));

                //doc.AddTitle("这里是标题");
                //doc.AddSubject("主题");
                //doc.AddKeywords("关键字");
                //doc.AddCreator("创建者");
                //doc.AddAuthor("作者");


                //cb.EndText();

                //doc.Close();
                //fs.Close();
            }

            MergeFilesHelper.MergeFiles(@"C:\Users\Administrator\Desktop\Merge");


            MessageBox.Show("生成成功！");
            Application.Current.Shutdown();
        }
    }
}
