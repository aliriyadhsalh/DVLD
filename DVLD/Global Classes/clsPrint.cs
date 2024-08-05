using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    internal class clsPrint
    {
        //when you need call use this insid button click:
        //clsPrint formPrinter = new clsPrint(this);
        //formPrinter.Print();

        private Form formToPrint;

        public clsPrint(Form form)
        {
            formToPrint = form;
        }

        public void Print()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintPage);

            // Set up the print dialog
            PrintDialog printDialog = new PrintDialog
            {
                Document = printDocument
            };

            // Show the print dialog
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                // Adjust the page settings if the form is larger than A4
                if (formToPrint.Width > printDocument.DefaultPageSettings.PaperSize.Width ||
                    formToPrint.Height > printDocument.DefaultPageSettings.PaperSize.Height)
                {
                    printDocument.DefaultPageSettings.Landscape = true;
                }

                printDocument.Print();
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            // Create a bitmap of the form
            using (Bitmap bitmap = new Bitmap(formToPrint.Width, formToPrint.Height))
            {
                formToPrint.DrawToBitmap(bitmap, new Rectangle(0, 0, formToPrint.Width, formToPrint.Height));

                // Calculate the scale factor to fit the form into the page bounds
                float scaleFactor = Math.Min((float)e.PageBounds.Width / formToPrint.Width, (float)e.PageBounds.Height / formToPrint.Height);

                // Draw the image on the page with the calculated scale factor
                float scaledWidth = formToPrint.Width * scaleFactor;
                float scaledHeight = formToPrint.Height * scaleFactor;

                e.Graphics.DrawImage(bitmap, 0, 0, scaledWidth, scaledHeight);

                // If the form content exceeds one page, set e.HasMorePages to true
                e.HasMorePages = false; // Set to true if you need to print more pages
            }
        }




    }
}
