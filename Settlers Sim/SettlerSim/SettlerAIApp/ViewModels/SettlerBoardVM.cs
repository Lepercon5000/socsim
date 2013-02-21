using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SettlerSimLib;

namespace SettlerAIApp.ViewModels
{
    class SettlerBoardVM : INotifyPropertyChanged
    {
        public SettlerBoardVM()
        {
            model = new SettlerBoard();
            hexTiles = new ObservableCollection<HexVM>();
            foreach (IHex hex in model.GameBoard)
                hexTiles.Add(new HexVM(hex, model));
        }

        private SettlerBoard model;

        private ObservableCollection<HexVM> hexTiles;
        public ObservableCollection<HexVM> HexTiles
        {
            get
            {
                return hexTiles;
            }
            set
            {
                hexTiles = value;
                RaiseChange("HexTiles");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaiseChange(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
