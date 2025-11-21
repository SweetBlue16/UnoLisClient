using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.ViewModels.ViewModelEntities
{
    public class BackgroundItemViewModel : ObservableObject
    {
        public string Name { get; }
        public string ImagePath { get; }
        public string VideoName { get; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public BackgroundItemViewModel(string name, string imagePath, string videoName)
        {
            Name = name;
            ImagePath = imagePath;
            VideoName = videoName;
        }
    }
}