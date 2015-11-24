using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;
using System.Windows.Input;
using MvvmFoundation.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace KasseApparat
{
    /// <summary>
    ///     ProductButtonControl er en klasse der står for at styre produktknapperne. Den indeholder en liste
    ///     der så indeholder endnu en liste hvori knapperne er. Her står ProduktButtonControl så for den
    ///     funktionalitet der er tilknyttet denne liste og dens oprettelse. 
    /// </summary>
    public class ProductButtonControl : INotifyPropertyChanged
    {
#region attributes

        private int _totalPages = 1;
        private int _currentPage = 1;
        private List<Product> _PDL; 
        private List<ProductButtonList> _PageList;
        private ShoppingList _shopList;
#endregion
         
        public ProductButtonControl()
        {
            _PDL = new List<Product>(); //Changed to PCL
            _PageList = new List<ProductButtonList>();
            _shopList = (ShoppingList)Application.Current.MainWindow.FindResource("ShoppingList");

            CreatePageList();
        }

        //Updates the productbuttons, with the products contained in the database

        /// <summary>
        ///     Update står for at opdatere produktknapperne med indholdet i den liste den modtager som parameter.
        ///     Denne funktion kaldes ved skift af kategori.
        /// </summary>
        /// <param name="newButtonList"></param>
        public void Update(List<Product> newButtonList)
        {
            _PDL = newButtonList;
            CalculateTotalpage();
            _currentPage = 1;
            CreatePageList();

            //Notifying for new changes
            Notify(string.Empty);
        }

        /// <summary>
        ///     Denne funktion laver en liste med produkter om til en liste der indholder lister med produktknapper. Derved
        ///     kan en produktliste laves om til knapper i kasseapparatet.
        /// </summary>
        void CreatePageList()
        {
            _PageList.Clear();

            int pages = 0;
            int i = 0;

            while (pages < _totalPages)
            {
                _PageList.Add(new ProductButtonList());

                for (int index = i; i < (index+12); i++)
                {
                    if (_PDL.Count > i) //PCL changed
                    {
                        _PageList[pages].Add(new ButtonContent(_PDL[i])); //PCL changed
                    }
                    else
                    {
                        _PageList[pages].Add(new ButtonContent("", ""));
                    }
                }
                
                pages++;
            }
        }

        /// <summary>
        ///     Udregner den totale mængde af knappe sider. Denne funktion er private og bruges i grænsefladen til vise vise total side mængde
        ///     og i commands til at fortælle når den sidste knappeside vises, og sikrer derved at nextCommand ikke kan trykkes.
        /// </summary>
        void CalculateTotalpage()
        {
            if ((_PDL.Count %12) == 0)
            {
                _totalPages = _PDL.Count /12;
            }
            else
            {
                _totalPages = (_PDL.Count /12)+1;
            }
        }

#region Commands
        ICommand _ButtonPrevClick;
        public ICommand PrevCommand { get { return _ButtonPrevClick ?? (_ButtonPrevClick = new RelayCommand(PrevCommandExecute, PrevCommandCanExecute)); } }

        /// <summary>
        ///     Dette command køres når der trykkes på knappen der viser forrige knappeside. Den decrementerer _currentPage og kalder derefter Notify med en tom 
        ///     string som parameter. Dette gør så at view henter alle properties fra klassen, hvilket i dette tilfælde er nødvendigt.
        /// </summary>
        private void PrevCommandExecute()
        {
            _currentPage--;
            Notify(string.Empty);
        }

        /// <summary>
        ///     Dette command tjekker på om knappen der viser forrige knappeside kan trykkes. Hvis knappesiden der bliver vist er nummer 1 er knappen slået fra.
        /// </summary>
        /// <returns></returns>
        bool PrevCommandCanExecute()
        {
            if (_currentPage == 1)
                return false;
            else
                return true;
        }

        ICommand _ButtonNextClick;
        public ICommand NextCommand { get { return _ButtonNextClick ?? (_ButtonNextClick = new RelayCommand(NextCommandExecute, NextCommandCanExecute)); } }

        /// <summary>
        ///     Dette command incrememterer _currentPage og kalder derefter notify på en tom streng. Derved henter view alle properties på en gang, hvilket er nødvendigt
        ///     da alle knapperne samt den nuværende side skifter sin værdi.
        /// </summary>
        private void NextCommandExecute()
        {
            _currentPage++;
            Notify(string.Empty);
        }

        /// <summary>
        ///     Dette command tjekker på om knappen til at skifte til næste side kan trykkes. Den kan ikke vælges hvis den nuværende side er det samme som den totale
        ///     mængde af sider.
        /// </summary>
        /// <returns></returns>
        bool NextCommandCanExecute()
        {
            if (_currentPage == _totalPages)
                return false;
            else
                return true;
        }



#endregion

#region Properties

        /// <summary>
        ///     Denne property returnerer den nuværende knappeside.
        /// </summary>
        public ProductButtonList CurrentButtonPage
        {
            get { return _PageList[_currentPage-1]; }
        }

        /// <summary>
        ///     Denne property returnerer den totale mængde af sider.
        /// </summary>
        public int TotalPages
        {
            get { return _totalPages; }
            private set { _totalPages = value; }
        }

        /// <summary>
        ///     Denne property returnerer den nuværende side værdi.
        /// </summary>
        public int CurrentPages
        {
            get { return _currentPage; }
        }

#endregion
        
        /// <summary>
        ///     Property changed event handler: Giver grænsefladen besked hvis der er en ændring i en given property.
        ///     Derved ved view at den skal opdatere sig selv og afspejle det indhold der ligger i model laget.
        /// </summary>
        public new event PropertyChangedEventHandler PropertyChanged;

        private void Notify([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
