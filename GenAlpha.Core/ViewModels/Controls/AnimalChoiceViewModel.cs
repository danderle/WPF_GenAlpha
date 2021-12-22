using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// A class for describing the possible animal choices in the animal guess game
    /// </summary>
    public class AnimalChoiceViewModel : BaseViewModel
    {
        #region Properties

        /// <summary>
        /// A flag to let us know if this is the hidden animal
        /// </summary>
        public bool IsHiddenAnimal { get; set; }

        /// <summary>
        /// The name possible choice name
        /// </summary>
        public string ChoiceName { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to check if the animal is correct
        /// </summary>
        public ICommand AnimalFoundCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="command"></param>
        public AnimalChoiceViewModel(string name, ICommand command)
        {
            ChoiceName = name;
            AnimalFoundCommand = command;
        } 

        #endregion
    }
}
