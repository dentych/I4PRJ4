using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmFoundation.Wpf;
using SharedLib.Models;

namespace KasseApparat
{
    public class ShoppingList : ObservableCollection<PurchasedProduct>, INotifyPropertyChanged
    {
        public ShoppingList()
        {}

        public void AddItem(PurchasedProduct product)
        {
            Add(product);
            Notify("TotalPrice");
        }

        public void AddItem(Product prod)
        {
            if (this.Any(x => x.Name == prod.Name))
            {
                int index = this.IndexOf(this.Single(x => x.Name == prod.Name));
                //Increment item in shoppinglist
                this[index].Quantity++;
            }
            else
            {
                this.Add(new PurchasedProduct(prod, 1));
            }
            Notify("TotalPrice");
        }
        
        public int TotalPrice
        {
            get { return this.Sum(vare => (int) vare.TotalPrice); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void Notify([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

#region Index
        private int _currentIndex = 0;
        public int CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                _currentIndex = value;
                Notify();
            }
        }
        
#endregion

#region MoreButton
        ICommand _ButtonMoreClick;
        public ICommand MoreCommand { get { return _ButtonMoreClick ?? (_ButtonMoreClick = new RelayCommand(MoreCommandExecute, MoreCommandCanExecute)); } }

        private void MoreCommandExecute()
        {
            this[CurrentIndex].Quantity++;
            Notify("TotalPrice");
        }

        bool MoreCommandCanExecute()
        {
            if (CurrentIndex < 0)
                return false;
            else
                return true;
        }
#endregion

#region LessButton
        ICommand _ButtonLessClick;
        public ICommand LessCommand { get { return _ButtonLessClick ?? (_ButtonLessClick = new RelayCommand(LessCommandExecute, LessCommandCanExecute)); } }

        private void LessCommandExecute()
        {
            if (this[CurrentIndex].Quantity - 1 == 0)
            {
                RemoveAt(CurrentIndex);
                Notify("TotalPrice");
                return;
            }
            this[CurrentIndex].Quantity--;
            Notify("TotalPrice");
        }

        bool LessCommandCanExecute()
        {
            if (CurrentIndex < 0)
                return false;
            else
                return true;
        }
#endregion

#region PrevButton
        ICommand _PrevCommand;
        public ICommand PrevCommand { get { return _PrevCommand ?? (_PrevCommand = new RelayCommand(PrevCommandExecute, PrevCommandCanExecute)); } }
         
        void PrevCommandExecute()
        {
            CurrentIndex--;
        }

        bool PrevCommandCanExecute()
        {
            if (CurrentIndex < 1)
                return false;
            else
                return true;
        }
#endregion

#region NextButton
        ICommand _NextCommand;
        public ICommand NextCommand { get { return _NextCommand ?? (_NextCommand = new RelayCommand(NextCommandExecute, NextCommandCanExecute)); } }

        void NextCommandExecute()
        {
            CurrentIndex++;
        }

        bool NextCommandCanExecute()
        {
            if (CurrentIndex < (Count - 1))
                return true;
            else
                return false;
        }
#endregion

#region Delete
        ICommand _ButtonDeleteClick;
        public ICommand DeleteCommand { get { return _ButtonDeleteClick ?? (_ButtonDeleteClick = new RelayCommand(DeleteCommandExecute, DeleteCommandCanExecute)); } }

        private void DeleteCommandExecute()
        {
            RemoveAt(CurrentIndex);
            Notify("TotalPrice");
        }

        bool DeleteCommandCanExecute()
        {
            if (CurrentIndex < 0) return false;
            else return true;
        }
        #endregion

    }
}
