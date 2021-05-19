using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace GenAlpha
{
    /// <summary>
    /// Helpers to animate pages in specific ways
    /// </summary>
    public static class PageAnimations
    {
        /// <summary>
        /// Slides a page in from the right
        /// </summary>
        /// <param name="page"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromRight(this Page page, float seconds)
        {
            //Create storyboard
            var sb = new Storyboard();

            //Add slide from right animation
            sb.AddSlideInFromRight(seconds, page.WindowWidth);
            
            //Add fade in
            sb.AddFadeIn(seconds);

            //start animating
            sb.Begin(page);

            //Make page visible
            page.Visibility = Visibility.Visible;

            //Wait for it to finish
            await Task.Delay((int)seconds * 1000);
        }

        /// <summary>
        /// Slides a page out to the left
        /// </summary>
        /// <param name="page"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToLeft(this Page page, float seconds)
        {
            //Create storyboard
            var sb = new Storyboard();

            //Add slide from right animation
            sb.AddSlideOutToLeft(seconds, page.WindowWidth);

            //Add fade in
            sb.AddFadeOut(seconds);

            //start animating
            sb.Begin(page);

            //Make page visible
            page.Visibility = Visibility.Visible;

            //Wait for it to finish
            await Task.Delay((int)seconds * 1000);
        }
    }
}
