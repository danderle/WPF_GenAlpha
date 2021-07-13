namespace GenAlpha.Core
{
    /// <summary>
    /// The bullet class for the keyboard shooter game
    /// </summary>
    public class Bullet
    {
        #region Constants

        private const int SPEED = 25;

        #endregion

        #region Properties

        /// <summary>
        /// The actual x position
        /// </summary>
        public int Xposition { get; private set; }

        /// <summary>
        /// The actual y position
        /// </summary>
        public int Yposition { get; private set; }

        /// <summary>
        /// The bullets target
        /// </summary>
        public FallingText Target { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default overloaded constructor
        /// </summary>
        /// <param name="xStartPosition">the starting x position</param>
        /// <param name="yStartPosition">the starting y position</param>
        /// <param name="target">the actual target</param>
        public Bullet(int xStartPosition, int yStartPosition, FallingText target)
        {
            Xposition = xStartPosition;
            Yposition = yStartPosition;
            Target = target;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Moves the bullet by its speed
        /// </summary>
        public void Move()
        {
            Yposition -= SPEED;
        } 

        #endregion
    }
}
