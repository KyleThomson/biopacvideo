using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Linq;

namespace ProjectManager
{
    public class DataCalendar
    {
        // calendar drawing
        private Bitmap calendar;
        private Form calendarForm;
        private Pen calendarPen = new Pen(Brushes.Black);
        private int width; private int height;
        private float objectScale;
        public List<FileType> dataFiles;

        // data related properties
        public List<string> animalNames;
        public List<DateTime> dates;

        public DataCalendar(List<DateTime> dateTimes, List<string> verticalAxis)
        {
            // use constructors to assign data properties
            dates = dateTimes; animalNames = verticalAxis;

            calendarForm = new Form();
            calendarForm.Text = "Recording Calendar";

            //calendar dimensions
            width = (int)(96 * 10);
            height = (int)(96 * 6);

            // initialize display and other display factors
            InitDisplay();
            ObjectScaling();
            ImproveResolution();
            DispalyCalendar();
        }
        private void InitDisplay()
        {
            // initialize bitmap
            calendar = new Bitmap(width, height);

            // create control that calendar can be added to
            PictureBox picture = new PictureBox();
            picture.Image = calendar;
            picture.Dock = DockStyle.Fill;

            // add calendar control to form
            calendarForm.Controls.Add(picture);
        }
        private void ImproveResolution()
        {
            using (Graphics graphics = Graphics.FromImage(calendar))
            {
                // Set smoothing mode for graphics in initialization. This will smooth out edges when drawing round objects.
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                // High quality interpolation makes rescaling of image maintain resolution.
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            }
        }
        private void ObjectScaling()
        {
            // get resolution for calendar
            using (Graphics graphics = Graphics.FromImage(calendar))
            {
                var xResolution = graphics.DpiX * 10;
                var yResolution = graphics.DpiY * 6;

                // calculate target scaling factor to maintain graph aspect ratio on any screen
                objectScale = (float)(calendar.Width / xResolution < calendar.Height / yResolution
                    ? calendar.Width / xResolution : calendar.Height / yResolution);
            }
        }
        private void DrawCalendarBox()
        {
            using (Graphics graphics = Graphics.FromImage(calendar))
            {
                // number of boxes
                var horizBoxes = dates.Count;
                var vertBoxes = animalNames.Count;

                // calendar box bounds
                // upper left corner
                Point startCorner = new Point((int)(calendar.Width * 0.15), (int)(calendar.Height * 0.15));
                Point endCorner = new Point((int)(calendar.Width * 0.90), (int)(calendar.Height * 0.15));
                graphics.DrawLine(calendarPen, startCorner, endCorner);
            }
            new Point((int)(calendar.Width * 0.15), (int)(calendar.Height * 0.15));
        }
        private int[,] AnimalInFile(string animalID)
        {
            int[,] dataOccurence = new int[dates.Count, animalNames.Count];
            int i = 0;
            foreach(FileType fileType in dataFiles)
            {
                int j = 0;
                foreach (string animal in animalNames)
                {
                    if (fileType.AnimalIDs.Contains(animal))
                        dataOccurence[i, j] = 1;
                    j++;
                }
                i++;
            }
            return dataOccurence;
        }
        private Bitmap Resize()
        {
            // get scaled dimensions
            int scaleWidth = (int)(width / objectScale);
            int scaleHeight = (int)(height / objectScale);

            // create a resized image with new dimensions
            var resizedImage = new Rectangle((width - scaleWidth) / 2, (height - scaleHeight) / 2, scaleWidth, scaleHeight);

            // make a new bitmap  - this will get drawn to with new dimensions
            var resizedBitmap = new Bitmap(calendar.Width, calendar.Height);

            // increase resolution? unsure if this is really necessary
            resizedBitmap.SetResolution(calendar.HorizontalResolution, calendar.VerticalResolution);

            // use temp graphics object to make image drawn higher quality
            using (var gfx = Graphics.FromImage(resizedBitmap))
            {
                gfx.CompositingMode = CompositingMode.SourceCopy;
                gfx.CompositingQuality = CompositingQuality.HighQuality;
                gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gfx.SmoothingMode = SmoothingMode.HighQuality;
                gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    gfx.DrawImage(calendar, resizedImage, 0, 0, calendar.Width, calendar.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return resizedBitmap;
        }
        public void DispalyCalendar()
        {
            // this method will handle displaying calendar to user
            var resized = Resize();

            // resize existing form
            calendarForm.Size = new Size((int)resized.Width, (int)(resized.Height));
            using (Graphics graphics = Graphics.FromImage(calendar))
            {
                // draw new image
                graphics.DrawImage(resized, 0, 0);
                calendarForm.Show();

            }


        }
    }
}
