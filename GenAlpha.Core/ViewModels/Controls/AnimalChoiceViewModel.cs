﻿using System;
using System.IO;
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

        /// <summary>
        /// The image path
        /// </summary>
        public string ImagePath { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to check if the animal is correct
        /// </summary>
        public ICommand AnimalFoundCommand { get; set; }

        /// <summary>
        /// The command to speak the animal name
        /// </summary>
        public ICommand SpeakAnimalNameCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="command"></param>
        public AnimalChoiceViewModel(string imagePath, ICommand command)
        {
            ImagePath = imagePath;
            ChoiceName = Path.GetFileNameWithoutExtension(imagePath);
            AnimalFoundCommand = command;
            SpeakAnimalNameCommand = new RelayCommand(SpeakAnimalName);
        }
        #endregion

        #region Command methods

        /// <summary>
        /// The command method to speak the animal name on hover
        /// </summary>
        private async void SpeakAnimalName()
        {
            if (!Speech.IsSpeaking)
            {
                await Speech.SpeakAsync(ChoiceName);
            }
        }

        #endregion
    }
}
